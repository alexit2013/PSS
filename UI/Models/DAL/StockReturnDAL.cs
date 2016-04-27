using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Objects;
using System.Linq;
using System.Web;
using UI.Models.Model;

namespace UI.Models.DAL
{
    public class StockReturnDAL
    {
        /// <summary>
        /// 添加采购退货单
        /// </summary>
        /// <param name="qp"></param>
        /// <param name="list"></param>
        /// <returns></returns>
        internal static int AddStocks(StockReturn qp, List<StockReturnDetail> list)
        {
            PSSEntities db = new PSSEntities();
            int fg = 1;
            using (var tx = db.Database.BeginTransaction())
            {
                try
                {
                    db.StockReturn.Add(qp);
                    if(list!=null)
                        db.StockReturnDetail.AddRange(list);

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
        internal static List<StockReturn> GetAll()
        {
            PSSEntities db = new PSSEntities();
            return db.StockReturn.Select(p => p).ToList();
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
        internal static List<StockReturn> Find(string KRID, string PPID, string SIDID, string DepotID, string KRDate, string UsersName,int KRState, int PageIndex, int PageSize, out int count)
        {
            PSSEntities db = new PSSEntities();
            var list = from c in db.StockReturn select c;
            if (KRID != null && KRID.Trim().Length > 0)
            {
                list = list.Where(p => p.KRID.Contains(KRID));
            }
            if (PPID != null && PPID.Trim().Length > 0)
            {
                list = list.Where(p => p.PPID.Equals(PPID));
            }
            if (SIDID != null && SIDID.Trim().Length > 0)
            {
                list = list.Where(p => p.SIDID.Equals(SIDID));
            }
            if (DepotID != null && DepotID.Trim().Length > 0)
            {
                list = list.Where(p => p.DepotID.Equals(DepotID));
            }
            if (KRDate != null && KRDate.Trim().Length > 0)
            {
                DateTime dt = Convert.ToDateTime(KRDate);
                list = list.Where(p => p.KRDate >= dt);
            }
            if (UsersName != null && UsersName.Trim().Length > 0)
            {
                list = list.Where(p => p.Users.UsersName.Contains(UsersName));
            }
            if (KRState != -1)
            {
                list = list.Where(p => p.KRState == KRState);
            }
            count = list.Count();
            return list.OrderBy(p => p.KRID).Skip((PageIndex - 1) * PageSize).Take(PageSize).ToList();
        }
        /// <summary>
        /// 修改采购退货单
        /// </summary>
        /// <param name="qp"></param>
        /// <param name="list"></param>
        /// <returns></returns>
        internal static int EditStocks(StockReturn qp, List<StockReturnDetail> list)
        {
            PSSEntities db = new PSSEntities();
            int fg = 1;
            using (var tx = db.Database.BeginTransaction())
            {
                try
                {
                    StockReturn c = db.StockReturn.FirstOrDefault(p => p.KRID.Equals(qp.KRID));
                    c.PPID = qp.PPID;
                    c.DepotID = qp.DepotID;
                    c.SIDID = qp.SIDID;
                    c.KRDate = qp.KRDate;
                    c.SIDID = qp.SIDID;
                    c.KRDesc = qp.KRDesc;
                    db.StockReturnDetail.RemoveRange(db.StockReturnDetail.Where(s => s.KRID.Equals(qp.KRID)));
                    if(list!=null)
                        db.StockReturnDetail.AddRange(list);

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
        internal static StockReturn GetDByID(string id)
        {
            PSSEntities db = new PSSEntities();
            return db.StockReturn.FirstOrDefault(p => p.KRID.Equals(id));
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
                    StockReturn co = db.StockReturn.FirstOrDefault(p => p.KRID.Equals(id));
                    co.KRState = 1;
                    ObjectParameter para = new ObjectParameter("DD", "");
                    db.pro_order("InOutDepot", "IODNum", "RK", para);
                    string IODNum = para.Value.ToString();
                    //添加入库记录
                    db.InOutDepot.Add(new InOutDepot() { DepotID = co.DepotID, IODType = 2, IODNum = IODNum, IODDate = DateTime.Now, IODUser = userid, IODDesc = co.KRDesc });
                    db.SaveChanges();
                    int inod = db.InOutDepot.Max(i => i.IODID);
                    List<StockReturnDetail> list = db.StockReturnDetail.Where(p => p.KRID.Equals(id)).ToList();//销售出库详单
                    List<StockInDepotDetail> cuslist = db.StockInDepotDetail.Where(p => p.SIDID.Equals(co.SIDID)).ToList();//客户订单详单
                    foreach (StockReturnDetail item in list)
                    {
                        //修改客户订单已销数量
                        foreach (StockInDepotDetail cus in cuslist) {
                            if (cus.ProID == item.ProID) {
                                if (Convert.ToInt32(cus.SIDAmount) <Convert.ToInt32(item.KRDAmount))
                                    throw new Exception("退货商品数量超出入库数量，操作失败！！！");
                            }
                        }
                        if (db.DepotStock.FirstOrDefault(p => p.DepotID.Equals(co.DepotID) && p.ProID == item.ProID) == null)
                        {//如果指定的仓库中不存在该商品
                            throw new Exception("未知错误，不存在该商品，操作失败！");
                        }
                        else
                        {//如果指定仓库存在这个商品，修改这个商品的库存
                            DepotStock ds = db.DepotStock.FirstOrDefault(d => d.ProID == item.ProID && d.DepotID == co.DepotID);
                            ds.DSAmount = ds.DSAmount - item.KRDAmount;
                        }
                        //添加入库记录详情
                        db.InOutDepotDetail.Add(new InOutDepotDetail() { IODID = inod, ProID = item.ProID, IODDAmount = item.KRDAmount, IODDPrice = item.KRDPrice });
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
                    db.StockReturnDetail.RemoveRange(db.StockReturnDetail.Where(p => p.KRID.Equals(id)));
                    db.StockReturn.Remove(db.StockReturn.FirstOrDefault(p => p.KRID.Equals(id)));
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
            string sql = "select * from StockReturn where datepart(MM,KRDate)=" + dt.Month + " and  datepart(YYYY,KRDate) = " + dt.Year + " and KRState =" + state;
            List<StockReturn> list = db.Database.SqlQuery<StockReturn>(sql).ToList();
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
            string sql = "select *  from StockReturn where  KRState = " + state;
            PSSEntities db = new PSSEntities();
            int count = db.Database.SqlQuery<StockReturn>(sql).ToList().Count;
            return count;
        }
    }
}