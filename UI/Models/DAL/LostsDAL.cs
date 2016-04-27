using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Objects;
using System.Linq;
using System.Web;
using UI.Models.Model;


namespace UI.Models.DAL
{
    public class LostsDAL
    {
        /// <summary>
        /// 查询所有
        /// </summary>
        /// <returns></returns>
        public static List<Losts> GetAll()
        {
            PSSEntities db = new PSSEntities();
            return db.Losts.Select(d => d).ToList();
        }
        /// <summary>
        /// 添加【---事务---】
        /// </summary>
        /// <param name="dep"></param>
        /// <returns></returns>
        public static int AddStocks(Losts dep, List<LostDetail> list)
        {
            PSSEntities db = new PSSEntities();
            int fg = 1;
            using (var tx = db.Database.BeginTransaction())
            {
                try
                {
                    db.Losts.Add(dep);
                    if (list != null) db.LostDetail.AddRange(list);
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
                    db.LostDetail.RemoveRange(db.LostDetail.Where(d => d.LostID.Equals(id)));
                    db.Losts.Remove(db.Losts.FirstOrDefault(s => s.LostID.Equals(id)));
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
        public static int EdiStocks(Losts dep, List<LostDetail> list)
        {
            PSSEntities db = new PSSEntities();
            int fg = 1;
            ///以下开始事务操作
            using (var tx = db.Database.BeginTransaction())
            {
                try
                {
                    //将要修改的创建人找回来，避免修改时没有创建人
                    Losts st = db.Losts.FirstOrDefault(p => p.LostID == dep.LostID);
                    st.DepotID = dep.DepotID;
                    st.LostDate = dep.LostDate;
                    st.LostDesc = dep.LostDesc;
                    db.LostDetail.RemoveRange(db.LostDetail.Where(p => p.LostID == dep.LostID));
                    if (list != null) db.LostDetail.AddRange(list);
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
        /// 根据ID查询详单信息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        internal static Losts GetByID(string id)
        {
            PSSEntities db = new PSSEntities();
            return db.Losts.FirstOrDefault(s => s.LostID.Equals(id));
        }
        /// <summary>
        /// 审核报损单
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
                    Losts st = db.Losts.FirstOrDefault(s => s.LostID.Equals(id));
                    st.LostState = 1;
                    ObjectParameter para = new ObjectParameter("DD", "");
                    db.pro_order("InOutDepot", "IODNum", "RK", para);
                    string IODNum = para.Value.ToString();
                    //添加出库记录
                    db.InOutDepot.Add(new InOutDepot() { DepotID = st.DepotID, IODType = 2, IODNum = IODNum, IODDate = DateTime.Now, IODUser = userid, IODDesc = st.LostDesc });
                    db.SaveChanges();
                    int inod = db.InOutDepot.Max(i => i.IODID);
                    List<LostDetail> list = db.LostDetail.Where(p => p.LostID.Equals(id)).ToList();//销售出库详单
                    foreach (LostDetail item in list)
                    {

                        if (db.DepotStock.FirstOrDefault(p => p.DepotID.Equals(st.DepotID) && p.ProID == item.ProID) == null)
                        {//如果指定的仓库中不存在该商品
                            throw new Exception("报损仓库不存在该商品，操作失败！");
                        }
                        else
                        {//如果指定仓库存在这个商品，修改这个商品的库存
                            DepotStock ds = db.DepotStock.FirstOrDefault(d => d.ProID == item.ProID);
                            if (ds.DSAmount < item.LDAmount)
                            {
                                throw new Exception("商品库存小于报损数量，操作失败！");
                            }
                            ds.DSAmount = ds.DSAmount - item.LDAmount;
                        }
                        //添加入库记录详情
                        db.InOutDepotDetail.Add(new InOutDepotDetail() { IODID = inod, ProID = item.ProID, IODDAmount = item.LDAmount, IODDPrice = item.LDPrice });
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
        public static List<Losts> Find(string LostID, string DepotID, string LostDate, string UsersName, int LostState, int PageIndex, int PageSize, out int count)
        {
            PSSEntities db = new PSSEntities();
            var list = from s in db.Losts select s;
            if (LostID != null && LostID.Trim().Length > 0)//入库单编号
                list = list.Where(s => s.LostID.Contains(LostID));

            if (DepotID != null && DepotID.Trim().Length > 0)//仓库编号
                list = list.Where(s => s.DepotID.Equals(DepotID));

            if (LostDate != null && LostDate.Trim().Length > 0)//入库时间
            {
                DateTime dt = Convert.ToDateTime(LostDate);
                list = list.Where(s => s.LostDate >= dt);
            }
            if (UsersName != null && UsersName.Trim().Length > 0)//创建人
                list = list.Where(s => s.Users.UsersName.Contains(UsersName));

            if (LostState != -1)//订单状态
                list = list.Where(s => s.LostState == LostState);


            count = list.Count();
            return list.OrderBy(s => s.LostID).Skip((PageIndex - 1) * PageSize).Take(PageSize).ToList();
        }
    }
}