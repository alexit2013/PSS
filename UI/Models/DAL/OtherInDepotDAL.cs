using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Objects;
using System.Linq;
using System.Web;
using UI.Models.Model;

namespace UI.Models.DAL
{
    public class OtherInDepotDAL
    {
        /// <summary>
        /// 查询所有
        /// </summary>
        /// <returns></returns>
        public static List<OtherInDepot> GetAll()
        {
            PSSEntities db = new PSSEntities();
            return db.OtherInDepot.Select(d => d).ToList();
        }
    
        /// <summary>
        /// 添加【---事务---】
        /// </summary>
        /// <param name="dep"></param>
        /// <returns></returns>
        public static int AddStocks(OtherInDepot dep, List<OtherInDepotDetail> list)
        {
            PSSEntities db = new PSSEntities();
            int fg = 1;
            using (var tx = db.Database.BeginTransaction())
            {
                try
                {
                    db.OtherInDepot.Add(dep);
                    if (list != null)db.OtherInDepotDetail.AddRange(list);
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
                    db.OtherInDepotDetail.RemoveRange(db.OtherInDepotDetail.Where(s => s.OIDID.Equals(id)));
                    db.OtherInDepot.Remove(db.OtherInDepot.FirstOrDefault(d => d.OIDID.Equals(id)));
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
        public static int EdiStocks(OtherInDepot dep, List<OtherInDepotDetail> list)
        {
            PSSEntities db = new PSSEntities();
            int fg = 1;
            ///以下开始事务操作
            using (var tx = db.Database.BeginTransaction())
            {
                try
                {
                    //将要修改的创建人找回来，避免修改时没有创建人
                    OtherInDepot st = db.OtherInDepot.FirstOrDefault(p => p.OIDID == dep.OIDID);
                    st.DepotID = dep.DepotID;
                    st.OIDDate = dep.OIDDate;
                    st.OIDDesc = dep.OIDDesc;
                    db.OtherInDepotDetail.RemoveRange(db.OtherInDepotDetail.Where(p => p.OIDID == dep.OIDID));
                    if(list!=null)db.OtherInDepotDetail.AddRange(list);
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
        internal static OtherInDepot GetByID(string id)
        {
            PSSEntities db = new PSSEntities();
            return db.OtherInDepot.FirstOrDefault(s => s.OIDID.Equals(id));
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
                    List<OtherInDepotDetail> prolist = db.OtherInDepotDetail.Where(s => s.OIDID.Equals(id)).ToList();
                    OtherInDepot st = db.OtherInDepot.FirstOrDefault(s => s.OIDID.Equals(id));
                    ObjectParameter para = new ObjectParameter("DD", "");
                    db.pro_order("InOutDepot", "IODNum", "RK", para);
                    string IODNum = para.Value.ToString();
                    //添加入库记录
                    db.InOutDepot.Add(new InOutDepot() { DepotID = st.DepotID, IODType = 1, IODNum = IODNum, IODDate = DateTime.Now, IODUser = userid, IODDesc = st.OIDDesc });
                    db.SaveChanges();
                    int inod = db.InOutDepot.Max(i => i.IODID);
                    // db.InOutDepot.
                    foreach (OtherInDepotDetail item in prolist)
                    {
                        if (db.DepotStock.FirstOrDefault(p => p.DepotID.Equals(st.DepotID) && p.ProID == item.ProID) == null)
                        {//如果指定的仓库中不存在该商品
                            //【为这个仓库添加一个新商品】
                            db.DepotStock.Add(new DepotStock() { DepotID = st.DepotID, ProID = item.ProID, DSAmount = item.OIDDAmount, DSPrice = item.OIDDPrice });
                        }
                        else
                        {//如果指定仓库存在这个商品，修改这个商品的库存
                            DepotStock ds = db.DepotStock.FirstOrDefault(d => d.ProID == item.ProID);
                            ds.DSAmount = item.OIDDAmount + ds.DSAmount;
                        }
                        //添加入库记录详情
                        db.InOutDepotDetail.Add(new InOutDepotDetail() { IODID = inod, ProID = item.ProID, IODDAmount = item.OIDDAmount, IODDPrice = item.OIDDPrice });
                    }
                    st.OIDState = 1;
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
        public static List<OtherInDepot> Find(string OIDID, string DepotID, string OIDDate, string UsersName, int OIDState, int PageIndex, int PageSize, out int count)
        {
            PSSEntities db = new PSSEntities();
            var list = from s in db.OtherInDepot select s;
            if (OIDID != null && OIDID.Trim().Length > 0)//入库单编号
                list = list.Where(s => s.OIDID.Contains(OIDID));

            if (DepotID != null && DepotID.Trim().Length > 0)//仓库编号
                list = list.Where(s => s.DepotID.Equals(DepotID));

            if (OIDDate != null && OIDDate.Trim().Length > 0)//入库时间
            {
                DateTime dt = Convert.ToDateTime(OIDDate);
                list = list.Where(s => s.OIDDate >= dt);
            }
            if (UsersName != null && UsersName.Trim().Length > 0)//创建人
                list = list.Where(s => s.Users.UsersName.Contains(UsersName));

            if (OIDState != -1)//订单状态
                list = list.Where(s => s.OIDState == OIDState);


            count = list.Count();
            return list.OrderBy(s => s.OIDID).Skip((PageIndex - 1) * PageSize).Take(PageSize).ToList();
        }

        /// <summary>
        /// 根据时间查询当前月订单个数
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        public static int FindByYCount(DateTime dt)
        {
            PSSEntities db = new PSSEntities();
            string sql = "select * from OtherInDepot where datepart(MM,OIDDate)=" + dt.Month + " and  datepart(YYYY,OIDDate) = " + dt.Year;
            List<OtherInDepot> list = db.Database.SqlQuery<OtherInDepot>(sql).ToList();
            return list.Count();
        }
    }
}