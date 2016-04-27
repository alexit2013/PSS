using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Objects;
using System.Linq;
using System.Web;
using UI.Models.Model;

namespace UI.Models.DAL
{
    public class ProduceOutDepotDAL
    {
        /// <summary>
        /// 查询所有
        /// </summary>
        /// <returns></returns>
        public static List<ProduceOutDepot> GetAll()
        {
            PSSEntities db = new PSSEntities();
            return db.ProduceOutDepot.Select(d => d).ToList();
        }
        /// <summary>
        /// 添加【---事务---】
        /// </summary>
        /// <param name="dep"></param>
        /// <returns></returns>
        public static int AddStocks(ProduceOutDepot dep, List<ProduceOutDepotDetail> list)
        {
            PSSEntities db = new PSSEntities();
            int fg = 1;
            using (var tx = db.Database.BeginTransaction())
            {
                try
                {
                    db.ProduceOutDepot.Add(dep);
                    if (list != null) db.ProduceOutDepotDetail.AddRange(list);
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
                    db.ProduceOutDepotDetail.RemoveRange(db.ProduceOutDepotDetail.Where(s => s.PODID.Equals(id)));
                    db.ProduceOutDepot.Remove(db.ProduceOutDepot.FirstOrDefault(d => d.PODID.Equals(id)));
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
        public static int EdiStocks(ProduceOutDepot dep, List<ProduceOutDepotDetail> list)
        {
            PSSEntities db = new PSSEntities();
            int fg = 1;
            ///以下开始事务操作
            using (var tx = db.Database.BeginTransaction())
            {
                try
                {
                    //将要修改的创建人找回来，避免修改时没有创建人
                    ProduceOutDepot st = db.ProduceOutDepot.FirstOrDefault(p => p.PODID == dep.PODID);
                    st.DepotID = dep.DepotID;
                    st.PODDate = dep.PODDate;
                    st.PODDesc = dep.PODDesc;
                    db.ProduceOutDepotDetail.RemoveRange(db.ProduceOutDepotDetail.Where(p => p.PODID == dep.PODID));
                    if (list != null) db.ProduceOutDepotDetail.AddRange(list);
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
        internal static ProduceOutDepot GetByID(string id)
        {
            PSSEntities db = new PSSEntities();
            return db.ProduceOutDepot.FirstOrDefault(s => s.PODID.Equals(id));
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
                    ProduceOutDepot st = db.ProduceOutDepot.FirstOrDefault(s => s.PODID.Equals(id));
                    st.PODState = 1;
                    ObjectParameter para = new ObjectParameter("DD", "");
                    db.pro_order("InOutDepot", "IODNum", "RK", para);
                    string IODNum = para.Value.ToString();
                    //添加出库记录
                    db.InOutDepot.Add(new InOutDepot() { DepotID = st.DepotID, IODType = 2, IODNum = IODNum, IODDate = DateTime.Now, IODUser = userid, IODDesc = st.PODDesc });
                    db.SaveChanges();
                    int inod = db.InOutDepot.Max(i => i.IODID);
                    List<ProduceOutDepotDetail> list = db.ProduceOutDepotDetail.Where(p => p.PODID.Equals(id)).ToList();//销售出库详单
                    foreach (ProduceOutDepotDetail item in list)
                    {
                      
                        if (db.DepotStock.FirstOrDefault(p => p.DepotID.Equals(st.DepotID) && p.ProID == item.ProID) == null)
                        {//如果指定的仓库中不存在该商品
                            throw new Exception("未知错误，不存在该商品，操作失败！");
                        }
                        else
                        {//如果指定仓库存在这个商品，修改这个商品的库存
                            DepotStock ds = db.DepotStock.FirstOrDefault(d => d.ProID == item.ProID && d.DepotID == st.DepotID);
                            if (ds.DSAmount<item.PODDAmount)
                            {
                                throw new Exception("商品库存小于出库数量，操作失败！");
                            }
                            ds.DSAmount = ds.DSAmount - item.PODDAmount;
                        }
                        //添加入库记录详情
                        db.InOutDepotDetail.Add(new InOutDepotDetail() { IODID = inod, ProID = item.ProID, IODDAmount = item.PODDAmount, IODDPrice = item.PODDPrice });
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
        public static List<ProduceOutDepot> Find(string PODID, string DepotID, string PODDate, string UsersName, int PODState, int PageIndex, int PageSize, out int count)
        {
            PSSEntities db = new PSSEntities();
            var list = from s in db.ProduceOutDepot select s;
            if (PODID != null && PODID.Trim().Length > 0)//入库单编号
                list = list.Where(s => s.PODID.Contains(PODID));

            if (DepotID != null && DepotID.Trim().Length > 0)//仓库编号
                list = list.Where(s => s.DepotID.Equals(DepotID));

            if (PODDate != null && PODDate.Trim().Length > 0)//入库时间
            {
                DateTime dt = Convert.ToDateTime(PODDate);
                list = list.Where(s => s.PODDate >= dt);
            }
            if (UsersName != null && UsersName.Trim().Length > 0)//创建人
                list = list.Where(s => s.Users.UsersName.Contains(UsersName));

            if (PODState != -1)//订单状态
                list = list.Where(s => s.PODState == PODState);


            count = list.Count();
            return list.OrderBy(s => s.PODID).Skip((PageIndex - 1) * PageSize).Take(PageSize).ToList();
        }
        /// <summary>
        /// 根据时间查询当前月订单个数
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        public static int FindByYCount(DateTime dt, int state)
        {
            PSSEntities db = new PSSEntities();
            string sql = "select * from ProduceOutDepot where datepart(MM,PODDate)=" + dt.Month + " and  datepart(YYYY,PODDate) = " + dt.Year + " and PODState =" + state;
            List<ProduceOutDepot> list = db.Database.SqlQuery<ProduceOutDepot>(sql).ToList();
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
            string sql = "select *  from ProduceOutDepot where  PODState = " + state;
            PSSEntities db = new PSSEntities();
            int count = db.Database.SqlQuery<ProduceOutDepot>(sql).ToList().Count;
            return count;
        }
    }
}