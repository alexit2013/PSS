using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Objects;
using System.Linq;
using System.Web;
using UI.Models.Model;

namespace UI.Models.DAL
{
    public class SaleReturnDAL
    {
        /// <summary>
        /// 添加销售退货单
        /// </summary>
        /// <param name="qp"></param>
        /// <param name="list"></param>
        /// <returns></returns>
        internal static int AddStocks(SaleReturn qp, List<SaleReturnDetail> list)
        {
            PSSEntities db = new PSSEntities();
            int fg = 1;
            using (var tx = db.Database.BeginTransaction())
            {
                try
                {
                    db.SaleReturn.Add(qp);
                    if (list != null)
                        db.SaleReturnDetail.AddRange(list);
                    db.SaveChanges();
                    tx.Commit();
                }
                catch (Exception ex)
                {
                    fg = 0;
                    tx.Rollback();
                    throw new Exception(ex.Message);
                }
            }
            return fg;
        }
        /// <summary>
        /// 查询所有
        /// </summary>
        /// <returns></returns>
        internal static List<SaleReturn> GetAll()
        {
            PSSEntities db = new PSSEntities();
            return db.SaleReturn.Select(p => p).ToList();
        }
        /// <summary>
        /// 条件查询
        /// </summary>
        /// <param name="COID"></param>
        /// <param name="CusID"></param>
        /// <param name="CODate"></param>
        /// <param name="CORefDate"></param>
        /// <param name="UsersName"></param>
        /// <param name="COState"></param>
        /// <param name="PageIndex"></param>
        /// <param name="PageSize"></param>
        /// <param name="count"></param>
        /// <returns></returns>
        internal static List<SaleReturn> Find(string SRID, string CusID, string SDID, string DepotID, string SRDate, string UsersName, int SRState, int PageIndex, int PageSize, out int count)
        {
            PSSEntities db = new PSSEntities();
            var list = from c in db.SaleReturn select c;
            if (SRID != null && SRID.Trim().Length > 0)
            {
                list = list.Where(p => p.SRID.Contains(SRID));
            }
            if (CusID != null && CusID.Trim().Length > 0)
            {
                list = list.Where(p => p.CusID.Equals(CusID));
            }
            if (SDID != null && SDID.Trim().Length > 0)
            {
                list = list.Where(p => p.SDID.Equals(SDID));
            }
            if (DepotID != null && DepotID.Trim().Length > 0)
            {
                list = list.Where(p => p.DepotID.Equals(DepotID));
            }
            if (SRDate != null && SRDate.Trim().Length > 0)
            {
                DateTime dt = Convert.ToDateTime(SRDate);
                list = list.Where(p => p.SRDate >= dt);
            }
            if (UsersName != null && UsersName.Trim().Length > 0)
            {
                list = list.Where(p => p.Users.UsersName.Contains(UsersName));
            }
            if (SRState != -1)
            {
                list = list.Where(p => p.SRState == SRState);
            }
            count = list.Count();
            return list.OrderBy(p => p.SRID).Skip((PageIndex - 1) * PageSize).Take(PageSize).ToList();
        }
        /// <summary>
        /// 修改销售退货单
        /// </summary>
        /// <param name="qp"></param>
        /// <param name="list"></param>
        /// <returns></returns>
        internal static int EditStocks(SaleReturn qp, List<SaleReturnDetail> list)
        {
            PSSEntities db = new PSSEntities();
            int fg = 1;
            using (var tx = db.Database.BeginTransaction())
            {
                try
                {
                    SaleReturn c = db.SaleReturn.FirstOrDefault(p => p.SRID.Equals(qp.SRID));
                    c.CusID = qp.CusID;
                    c.DepotID = qp.DepotID;
                    c.SDID = qp.SDID;
                    c.SRDate = qp.SRDate;
                    c.SRDesc = qp.SRDesc;
                    db.SaleReturnDetail.RemoveRange(db.SaleReturnDetail.Where(s => s.SRID.Equals(qp.SRID)));
                    if (list != null)
                        db.SaleReturnDetail.AddRange(list);

                    db.SaveChanges();
                    tx.Commit();
                }
                catch (Exception ex)
                {
                    fg = 0;
                    tx.Rollback();
                    throw new Exception(ex.Message);
                }
            }
            return fg;
        }
        /// <summary>
        /// 根据ID查询采购退货详单信息
        /// </summary>
        /// <param name="id"></param>
        internal static SaleReturn GetDByID(string id)
        {
            PSSEntities db = new PSSEntities();
            return db.SaleReturn.FirstOrDefault(p => p.SRID.Equals(id));
        }
        /// <summary>
        /// 审核采购退货单
        /// </summary>
        /// <param name="id"></param>
        /// <param name="userid"></param>
        /// <returns></returns>
        internal static int CKInDepot(string id, int userid)
        {
            PSSEntities db = new PSSEntities();
            int fg = 1;
            using (var tx = db.Database.BeginTransaction())
            {
                try
                {
                    SaleReturn co = db.SaleReturn.FirstOrDefault(p => p.SRID.Equals(id));
                    co.SRState = 1;
                    ObjectParameter para = new ObjectParameter("DD", "");
                    db.pro_order("InOutDepot", "IODNum", "RK", para);
                    string IODNum = para.Value.ToString();
                    //添加出入库记录
                    db.InOutDepot.Add(new InOutDepot() { DepotID = co.DepotID, IODType = 2, IODNum = IODNum, IODDate = DateTime.Now, IODUser = userid, IODDesc = co.SRDesc });
                    db.SaveChanges();
                    int inod = db.InOutDepot.Max(i => i.IODID);
                    List<SaleReturnDetail> list = db.SaleReturnDetail.Where(p => p.SRID.Equals(id)).ToList();//销售出库详单
                    List<SaleDepotDetail> cuslist = db.SaleDepotDetail.Where(p => p.SDID.Equals(co.SDID)).ToList();//客户订单详单
                    foreach (SaleReturnDetail item in list)
                    {
                        //修改客户订单已销数量
                        foreach (SaleDepotDetail cus in cuslist)
                        {
                            if (cus.ProID == item.ProID)
                            {
                                if (Convert.ToInt32(cus.SDDDiscount) < Convert.ToInt32(item.SRDAmount))
                                    throw new Exception("退货商品数量超出出货数量，操作失败！！！");
                            }
                        }
                        if (db.DepotStock.FirstOrDefault(p => p.DepotID.Equals(co.DepotID) && p.ProID == item.ProID) == null)
                        {//如果指定的仓库中不存在该商品  为这个仓库添加这个商品
                            db.DepotStock.Add(new DepotStock() { DepotID=co.DepotID,ProID=item.ProID, DSAmount=item.SRDAmount,DSPrice=item.SRDPrice });
                        }
                        else
                        {//如果指定仓库存在这个商品，修改这个商品的库存
                            DepotStock ds = db.DepotStock.FirstOrDefault(d => d.ProID == item.ProID && d.DepotID == co.DepotID);
                            ds.DSAmount = ds.DSAmount + item.SRDAmount;
                        }
                        //添加入库记录详情
                        db.InOutDepotDetail.Add(new InOutDepotDetail() { IODID = inod, ProID = item.ProID, IODDAmount = item.SRDAmount, IODDPrice = item.SRDPrice });
                    }
                    db.SaveChanges();
                    tx.Commit();
                }
                catch (Exception ex)
                {
                    fg = 0;
                    tx.Rollback();
                    throw new Exception(ex.Message);
                }
            }
            return fg;
        }
        /// <summary>
        /// 删除采购退货单
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        internal static int DelStocks(string id)
        {
            PSSEntities db = new PSSEntities();
            int fg = 1;
            using (var tx = db.Database.BeginTransaction())
            {
                try
                {
                    db.SaleReturnDetail.RemoveRange(db.SaleReturnDetail.Where(p => p.SRID.Equals(id)));
                    db.SaleReturn.Remove(db.SaleReturn.FirstOrDefault(p => p.SRID.Equals(id)));
                    db.SaveChanges();
                    tx.Commit();
                }
                catch (Exception ex)
                {
                    fg = 0;
                    tx.Rollback();
                    throw new Exception(ex.Message);
                }
            }
            return fg;
        }

        /// <summary>
        /// 根据时间查询当前月订单个数
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        public static int FindByYCount(DateTime dt, int state)
        {
            PSSEntities db = new PSSEntities();
            string sql = "select * from SaleReturn where datepart(MM,SRDate)=" + dt.Month + " and  datepart(YYYY,SRDate) = " + dt.Year + " and SRState =" + state;
            List<SaleReturn> list = db.Database.SqlQuery<SaleReturn>(sql).ToList();
            return list.Count();
        }
        /// <summary>
        ///查询审核订单和未审核订单数
        /// </summary>
        /// <param name="dt"></param>
        /// <param name="state"></param>
        /// <returns></returns>
        public static int FindStateCount(int state)
        {
            string sql = "select *  from SaleReturn where  SRState = " + state;
            PSSEntities db = new PSSEntities();
            int count = db.Database.SqlQuery<SaleReturn>(sql).ToList().Count;
            return count;
        }
    }
}