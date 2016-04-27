using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Objects;
using System.Linq;
using System.Web;
using UI.Models.Model;

namespace UI.Models.DAL
{
    public class ProduceInDepotDAL
    {
        /// <summary>
        /// 查询所有
        /// </summary>
        /// <returns></returns>
        public static List<ProduceInDepot> GetAll()
        {
            PSSEntities db = new PSSEntities();
            return db.ProduceInDepot.Select(d => d).ToList();
        }

        /// <summary>
        /// 分页查询
        /// </summary>
        /// <param name="PageIndex"></param>
        /// <param name="PageSize"></param>
        /// <returns></returns>
        public static List<ProduceInDepot> GetAllPage(int PageIndex, int PageSize)
        {
            PSSEntities db = new PSSEntities();
            return db.ProduceInDepot.Select(d => d).OrderBy(d => d.PIDID).Skip((PageIndex - 1) * PageSize).Take(PageSize).ToList();
        }
        /// <summary>
        /// 添加【---事务---】
        /// </summary>
        /// <param name="dep"></param>
        /// <returns></returns>
        public static int AddStocks(ProduceInDepot dep, List<ProduceInDepotDeteil> list)
        {
            PSSEntities db = new PSSEntities();
            int fg = 1;
            using (var tx = db.Database.BeginTransaction())
            {
                try
                {
                    db.ProduceInDepot.Add(dep);
                    if (list != null)
                        db.ProduceInDepotDeteil.AddRange(list);

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
                    db.ProduceInDepotDeteil.RemoveRange(db.ProduceInDepotDeteil.Where(s => s.PIDID.Equals(id)));
                    db.ProduceInDepot.Remove(db.ProduceInDepot.FirstOrDefault(d => d.PIDID.Equals(id)));
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
        public static int EdiStocks(ProduceInDepot dep, List<ProduceInDepotDeteil> list)
        {
            PSSEntities db = new PSSEntities();
            int fg = 1;
            ///以下开始事务操作
            using (var tx = db.Database.BeginTransaction())
            {
                try
                {
                    //将要修改的创建人找回来，避免修改时没有创建人
                    ProduceInDepot st = db.ProduceInDepot.FirstOrDefault(p => p.PIDID == dep.PIDID);
                    st.DepotID = dep.DepotID;
                    st.PIDDate = dep.PIDDate;
                    st.PDIDesc = dep.PDIDesc;
                    db.ProduceInDepotDeteil.RemoveRange(db.ProduceInDepotDeteil.Where(p => p.PIDID == dep.PIDID));
                    if(list!=null)
                        db.ProduceInDepotDeteil.AddRange(list);

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
        internal static ProduceInDepot GetByID(string id)
        {
            PSSEntities db = new PSSEntities();
            return db.ProduceInDepot.FirstOrDefault(s => s.PIDID.Equals(id));
        }
        /// <summary>
        /// 审核采购订单
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static int CKProduceInDepot(string id,int userid)
        {
            PSSEntities db = new PSSEntities();
            int fg = 1;
            ///以下开始事务操作
            using (var tx = db.Database.BeginTransaction())
            {
                try
                {
                    List<ProduceInDepotDeteil> prolist = db.ProduceInDepotDeteil.Where(s => s.PIDID.Equals(id)).ToList();
                    ProduceInDepot st = db.ProduceInDepot.FirstOrDefault(s => s.PIDID.Equals(id));
                    ObjectParameter para = new ObjectParameter("DD", "");
                    db.pro_order("InOutDepot", "IODNum", "RK", para);
                    string IODNum = para.Value.ToString();
                    //添加入库记录
                    db.InOutDepot.Add(new InOutDepot() { DepotID = st.DepotID, IODType = 1, IODNum = IODNum, IODDate = DateTime.Now, IODUser = userid, IODDesc = st.PDIDesc });
                    db.SaveChanges();
                    int inod = db.InOutDepot.Max(i => i.IODID);
                    // db.InOutDepot.
                    foreach (ProduceInDepotDeteil item in prolist)
                    {
                        if (db.DepotStock.FirstOrDefault(p => p.DepotID.Equals(st.DepotID) && p.ProID == item.ProID) == null)
                        {//如果指定的仓库中不存在该商品
                            //【为这个仓库添加一个新商品】
                            db.DepotStock.Add(new DepotStock() { DepotID = st.DepotID, ProID = item.ProID, DSAmount = item.PIDDAmount, DSPrice = item.PIDDPrice });
                        }
                        else
                        {//如果指定仓库存在这个商品，修改这个商品的库存
                            DepotStock ds = db.DepotStock.FirstOrDefault(d => d.ProID == item.ProID && d.DepotID == st.DepotID);
                            ds.DSAmount = item.PIDDAmount + ds.DSAmount;
                        }
                        //添加入库记录详情
                        db.InOutDepotDetail.Add(new InOutDepotDetail() { IODID = inod, ProID = item.ProID, IODDAmount = item.PIDDAmount, IODDPrice = item.PIDDPrice });
                    }
                    st.PIDState = 1;
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
        public static List<ProduceInDepot> Find(string PIDID, string DepotID, string PIDDate,string UsersName, int PIDState, int PageIndex, int PageSize, out int count)
        {
            PSSEntities db = new PSSEntities();
            var list = from s in db.ProduceInDepot select s;
            if (PIDID != null && PIDID.Trim().Length > 0)//入库单编号
                list = list.Where(s=>s.PIDID.Contains(PIDID));

            if (DepotID != null && DepotID.Trim().Length > 0)//仓库编号
                list = list.Where(s => s.DepotID.Equals(DepotID));

            if (PIDDate != null && PIDDate.Trim().Length > 0)//入库时间
            {
                DateTime dt = Convert.ToDateTime(PIDDate);
                list = list.Where(s => s.PIDDate>=dt);
             }
            if (UsersName != null && UsersName.Trim().Length > 0)//创建人
                list = list.Where(s => s.Users.UsersName.Contains(UsersName));

            if (PIDState != -1)//订单状态
                list = list.Where(s => s.PIDState== PIDState);


            count = list.Count();
            return list.OrderBy(s => s.PIDID).Skip((PageIndex - 1) * PageSize).Take(PageSize).ToList();
        }
        /// <summary>
        /// 根据时间查询当前月订单个数
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        public static int FindByYCount(DateTime dt, int state)
        {
            PSSEntities db = new PSSEntities();
            string sql = "select * from ProduceInDepot where datepart(MM,PIDDate)=" + dt.Month + " and  datepart(YYYY,PIDDate) = " + dt.Year + " and PIDState =" + state;
            List<ProduceInDepot> list = db.Database.SqlQuery<ProduceInDepot>(sql).ToList();
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
            string sql = "select *  from ProduceInDepot where  PIDState = " + state;
            PSSEntities db = new PSSEntities();
            int count = db.Database.SqlQuery<ProduceInDepot>(sql).ToList().Count;
            return count;
        }
    }
}