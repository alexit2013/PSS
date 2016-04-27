
using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Objects;
using System.Linq;
using System.Web;
using UI.Models.Model;

namespace UI.Models.DAL
{
    public class OtherOutDepotDAL
    {
        /// <summary>
        /// 查询所有
        /// </summary>
        /// <returns></returns>
        public static List<OtherOutDepot> GetAll()
        {
            PSSEntities db = new PSSEntities();
            return db.OtherOutDepot.Select(d => d).ToList();
        }
        /// <summary>
        /// 添加【---事务---】
        /// </summary>
        /// <param name="dep"></param>
        /// <returns></returns>
        public static int AddStocks(OtherOutDepot dep, List<OtherOutDepotDetail> list)
        {
            PSSEntities db = new PSSEntities();
            int fg = 1;
            using (var tx = db.Database.BeginTransaction())
            {
                try
                {
                    db.OtherOutDepot.Add(dep);
                    if (list != null) db.OtherOutDepotDetail.AddRange(list);
                    db.SaveChanges();
                    tx.Commit();
                }
                catch (Exception e)
                {
                    fg = 0;
                    tx.Rollback();
                    throw new Exception(e.Message);
                }
            }
            return fg;
        }
        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static int DelStocks(string id)
        {
            PSSEntities db = new PSSEntities();
            int fg = 1;
            using (var tx = db.Database.BeginTransaction())
            {
                try
                {
                    db.StockReturnDetail.RemoveRange(db.StockReturnDetail.Where(d => d.KRID.Equals(id)));
                    db.StockReturn.Remove(db.StockReturn.FirstOrDefault(s => s.KRID.Equals(id)));
                    tx.Commit();
                    db.SaveChanges();
                }
                catch (Exception e)
                {
                    fg = 0;
                    tx.Rollback();
                    throw new Exception(e.Message);
                }
            }
            return fg;
        }
        /// <summary>
        /// 修改【---事务---】
        /// </summary>
        /// <param name="dp"></param>
        /// <returns></returns>
        public static int EdiStocks(OtherOutDepot dep, List<OtherOutDepotDetail> list)
        {
            PSSEntities db = new PSSEntities();
            int fg = 1;
            ///以下开始事务操作
            using (var tx = db.Database.BeginTransaction())
            {
                try
                {
                    //将要修改的创建人找回来，避免修改时没有创建人
                    OtherOutDepot st = db.OtherOutDepot.FirstOrDefault(p => p.OODID == dep.OODID);
                    st.DepotID = dep.DepotID;
                    st.OODDate = dep.OODDate;
                    st.OODDesc = dep.OODDesc;
                    db.OtherOutDepotDetail.RemoveRange(db.OtherOutDepotDetail.Where(p => p.OODID == dep.OODID));
                    if (list != null) db.OtherOutDepotDetail.AddRange(list);
                    db.SaveChanges();
                    tx.Commit();
                }
                catch (Exception e)
                {
                    fg = 0;
                    tx.Rollback();
                    throw new Exception(e.Message);
                }

            }
            return fg;
        }
        /// <summary>
        /// 根据ID查询采购单信息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        internal static OtherOutDepot GetByID(string id)
        {
            PSSEntities db = new PSSEntities();
            return db.OtherOutDepot.FirstOrDefault(s => s.OODID.Equals(id));
        }
        /// <summary>
        /// 审核采购订单
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static int CKInDepot(string id, int userid)
        {
            PSSEntities db = new PSSEntities();
            int fg = 1;
            ///以下开始事务操作
            using (var tx = db.Database.BeginTransaction())
            {
                try
                {
                    OtherOutDepot st = db.OtherOutDepot.FirstOrDefault(s => s.OODID.Equals(id));
                    st.OODState = 1;
                    ObjectParameter para = new ObjectParameter("DD", "");
                    db.pro_order("InOutDepot", "IODNum", "RK", para);
                    string IODNum = para.Value.ToString();
                    //添加出库记录
                    db.InOutDepot.Add(new InOutDepot() { DepotID = st.DepotID, IODType = 2, IODNum = IODNum, IODDate = DateTime.Now, IODUser = userid, IODDesc = st.OODDesc });
                    db.SaveChanges();
                    int inod = db.InOutDepot.Max(i => i.IODID);
                    List<OtherOutDepotDetail> list = db.OtherOutDepotDetail.Where(p => p.OODID.Equals(id)).ToList();//销售出库详单
                    foreach (OtherOutDepotDetail item in list)
                    {

                        if (db.DepotStock.FirstOrDefault(p => p.DepotID.Equals(st.DepotID) && p.ProID == item.ProID) == null)
                        {//如果指定的仓库中不存在该商品
                            throw new Exception("出库仓库不存在该商品，操作失败！");
                        }
                        else
                        {//如果指定仓库存在这个商品，修改这个商品的库存
                            DepotStock ds = db.DepotStock.FirstOrDefault(d => d.ProID == item.ProID && d.DepotID == st.DepotID);
                            if (ds.DSAmount < item.OODDAmount)
                            {
                                throw new Exception("商品库存小于出库数量，操作失败！");
                            }
                            ds.DSAmount = ds.DSAmount - item.OODDAmount;
                        }
                        //添加入库记录详情
                        db.InOutDepotDetail.Add(new InOutDepotDetail() { IODID = inod, ProID = item.ProID, IODDAmount = item.OODDAmount, IODDPrice = item.OODDPrice });
                    }
                    db.SaveChanges();
                    tx.Commit();
                }
                catch (Exception e)
                {
                    tx.Rollback();
                    throw new Exception(e.Message);
                }

            }
            return fg;
        }
        /// <summary>
        /// 条件查询
        /// </summary>
        /// <param name="PIDID"></param>
        /// <param name="DepotID"></param>
        /// <param name="PIDDate"></param>
        /// <param name="UsersName"></param>
        /// <param name="PIDState"></param>
        /// <param name="PageIndex"></param>
        /// <param name="PageSize"></param>
        /// <param name="count"></param>
        /// <returns></returns>
        public static List<OtherOutDepot> Find(string OODID, string DepotID, string OODDate, string UsersName, int OODState, int PageIndex, int PageSize, out int count)
        {
            PSSEntities db = new PSSEntities();
            var list = from s in db.OtherOutDepot select s;
            if (OODID != null && OODID.Trim().Length > 0)//入库单编号
                list = list.Where(s => s.OODID.Contains(OODID));

            if (DepotID != null && DepotID.Trim().Length > 0)//仓库编号
                list = list.Where(s => s.DepotID.Equals(DepotID));

            if (OODDate != null && OODDate.Trim().Length > 0)//入库时间
            {
                DateTime dt = Convert.ToDateTime(OODDate);
                list = list.Where(s => s.OODDate >= dt);
            }
            if (UsersName != null && UsersName.Trim().Length > 0)//创建人
                list = list.Where(s => s.Users.UsersName.Contains(UsersName));

            if (OODState != -1)//订单状态
                list = list.Where(s => s.OODState == OODState);


            count = list.Count();
            return list.OrderBy(s => s.OODID).Skip((PageIndex - 1) * PageSize).Take(PageSize).ToList();
        }

        /// <summary>
        /// 根据时间查询当前月订单个数
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        public static int FindByYCount(DateTime dt)
        {
            PSSEntities db = new PSSEntities();
            string sql = "select * from OtherOutDepot where datepart(MM,OODDate)=" + dt.Month + " and  datepart(YYYY,OODDate) = " + dt.Year;
            List<OtherOutDepot> list = db.Database.SqlQuery<OtherOutDepot>(sql).ToList();
            return list.Count();
        }
    }
}