using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Objects;
using System.Linq;
using System.Web;
using UI.Models.Model;

namespace UI.Models.DAL
{
    public class SaleDepotDAL
    {
        /// <summary>
        /// 添加销售出库单
        /// </summary>
        /// <param name="qp"></param>
        /// <param name="list"></param>
        /// <returns></returns>
        internal static int AddStocks(SaleDepot qp, List<SaleDepotDetail> list)
        {
            PSSEntities db = new PSSEntities();
            int fg = 1;
            using (var tx = db.Database.BeginTransaction())
            {
                try
                {
                    db.SaleDepot.Add(qp);
                    if(list!=null)
                         db.SaleDepotDetail.AddRange(list);

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
        internal static List<SaleDepot> GetAll()
        {
            PSSEntities db = new PSSEntities();
            return db.SaleDepot.Select(p => p).ToList();
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
        internal static List<SaleDepot> Find(string SDID, string CusID, string DepotID, string SDDate, string UsersName, int SDState, int PageIndex, int PageSize, out int count)
        {
            PSSEntities db = new PSSEntities();
            var list = from c in db.SaleDepot select c;
            if (SDID != null && SDID.Trim().Length > 0)
            {
                list = list.Where(p => p.SDID.Contains(SDID));
            }
            if (CusID != null && CusID.Trim().Length > 0)
            {
                list = list.Where(p => p.CusID.Equals(CusID));
            }
            if (DepotID != null && DepotID.Trim().Length > 0)
            {
                list = list.Where(p => p.DepotID.Equals(DepotID));
            }
            if (SDDate != null && SDDate.Trim().Length > 0)
            {
                DateTime dt = Convert.ToDateTime(SDDate);
                list = list.Where(p => p.SDDate >= dt);
            }
            if (UsersName != null && UsersName.Trim().Length > 0)
            {
                list = list.Where(p => p.Users.UsersName.Contains(UsersName));
            }
            if (SDState != -1)
            {
                list = list.Where(p => p.SDState == SDState);
            }
            count = list.Count();
            return list.OrderBy(p => p.SDID).Skip((PageIndex - 1) * PageSize).Take(PageSize).ToList();
        }
        /// <summary>
        /// 修改销售出库单
        /// </summary>
        /// <param name="qp"></param>
        /// <param name="list"></param>
        /// <returns></returns>
        internal static int EditStocks(SaleDepot qp, List<SaleDepotDetail> list)
        {
            PSSEntities db = new PSSEntities();
            int fg = 1;
            using (var tx = db.Database.BeginTransaction())
            {
                try
                {
                    SaleDepot c = db.SaleDepot.FirstOrDefault(p => p.SDID.Equals(qp.SDID));
                    c.CusID = qp.CusID;
                    c.DepotID = qp.DepotID;
                    c.SDState = qp.SDState;
                    c.SDDesc = qp.SDDesc;
                    db.SaleDepotDetail.RemoveRange(db.SaleDepotDetail.Where(s => s.SDID.Equals(qp.SDID)));
                    if (list != null)
                        db.SaleDepotDetail.AddRange(list);

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
        /// 根据ID查询销售出库单信息
        /// </summary>
        /// <param name="id"></param>
        internal static SaleDepot GetSaleDepotByID(string id)
        {
            PSSEntities db = new PSSEntities();
            return db.SaleDepot.FirstOrDefault(p => p.SDID.Equals(id));
        }
        /// <summary>
        /// 审核销售出库单
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
                    SaleDepot co = db.SaleDepot.FirstOrDefault(p => p.SDID.Equals(id));
                    co.SDState = 1;
                    ObjectParameter para = new ObjectParameter("DD", "");
                    db.pro_order("InOutDepot", "IODNum", "RK", para);
                    string IODNum = para.Value.ToString();
                    //添加入库记录
                    db.InOutDepot.Add(new InOutDepot() { DepotID = co.DepotID, IODType = 2, IODNum = IODNum, IODDate = DateTime.Now, IODUser = userid, IODDesc = co.SDDesc });
                    db.SaveChanges();
                    int inod = db.InOutDepot.Max(i => i.IODID);
                    List<SaleDepotDetail> list = db.SaleDepotDetail.Where(p=>p.SDID.Equals(id)).ToList();//销售出库详单
                    List<CustomerOrderDetail> cuslist = db.CustomerOrderDetail.Where(p=>p.COID.Equals(co.COID)).ToList();//客户订单详单
                    foreach (SaleDepotDetail item in list)
                    {
                        //修改客户订单已销数量
                        foreach (CustomerOrderDetail cus in cuslist)
                        {
                            if (cus.ProID==item.ProID)
                            {
                                cus.CODSale = item.SDDAmount;
                            }
                        }
                        if (db.DepotStock.FirstOrDefault(p => p.DepotID.Equals(co.DepotID) && p.ProID == item.ProID) == null)
                        {//如果指定的仓库中不存在该商品
                            throw new Exception("不存在该商品，操作失败！");
                        }
                        else
                        {//如果指定仓库存在这个商品，修改这个商品的库存
                            DepotStock ds = db.DepotStock.FirstOrDefault(d => d.ProID == item.ProID && d.DepotID == co.DepotID);
                            ds.DSAmount = ds.DSAmount - item.SDDAmount;
                        }
                        //添加入库记录详情
                        db.InOutDepotDetail.Add(new InOutDepotDetail() { IODID = inod, ProID = item.ProID, IODDAmount = item.SDDAmount, IODDPrice = item.SDDPrice });
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
        /// 删除客户订单
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
                    db.SaleDepotDetail.RemoveRange(db.SaleDepotDetail.Where(p => p.SDID.Equals(id)));
                    db.SaleDepot.Remove(db.SaleDepot.FirstOrDefault(p => p.SDID.Equals(id)));
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
        /// 根据时间查询当前月销售出库单个数
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        public static int FindByYCount(DateTime dt, int state)
        {
            PSSEntities db = new PSSEntities();
            string sql = "select * from SaleDepot where datepart(MM,SDDate)=" + dt.Month + " and  datepart(YYYY,SDDate) = " + dt.Year + " and SDState =" + state;
            List<SaleDepot> list = db.Database.SqlQuery<SaleDepot>(sql).ToList();
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
            string sql = "select *  from SaleDepot where  SDState = " + state;
            PSSEntities db = new PSSEntities();
            int count = db.Database.SqlQuery<SaleDepot>(sql).ToList().Count;
            return count;
        }


    }
}