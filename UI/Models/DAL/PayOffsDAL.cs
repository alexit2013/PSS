using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Objects;
using System.Linq;
using System.Web;
using UI.Models.Model;

namespace UI.Models.DAL
{
    public class PayOffsDAL
    {
        /// <summary>
        /// 查询所有
        /// </summary>
        /// <returns></returns>
        public static List<PayOffs> GetAll()
        {
            PSSEntities db = new PSSEntities();
            return db.PayOffs.Select(d => d).ToList();
        }
        /// <summary>
        /// 添加【---事务---】
        /// </summary>
        /// <param name="dep"></param>
        /// <returns></returns>
        public static int AddStocks(PayOffs dep, List<PayOffDetail> list)
        {
            PSSEntities db = new PSSEntities();
            int fg = 1;
            using (var tx = db.Database.BeginTransaction())
            {
                try
                {
                    db.PayOffs.Add(dep);
                    if (list != null) db.PayOffDetail.AddRange(list);
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
                    db.PayOffDetail.RemoveRange(db.PayOffDetail.Where(d => d.POID.Equals(id)));
                    db.PayOffs.Remove(db.PayOffs.FirstOrDefault(s => s.POID.Equals(id)));
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
        public static int EdiStocks(PayOffs dep, List<PayOffDetail> list)
        {
            PSSEntities db = new PSSEntities();
            int fg = 1;
            ///以下开始事务操作
            using (var tx = db.Database.BeginTransaction())
            {
                try
                {
                    //将要修改的创建人找回来，避免修改时没有创建人
                    PayOffs st = db.PayOffs.FirstOrDefault(p => p.POID == dep.POID);
                    st.DepotID = dep.DepotID;
                    st.POState = dep.POState;
                    st.PODesc = dep.PODesc;
                    db.PayOffDetail.RemoveRange(db.PayOffDetail.Where(p => p.POID == dep.POID));
                    if (list != null) db.PayOffDetail.AddRange(list);
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
        internal static PayOffs GetByID(string id)
        {
            PSSEntities db = new PSSEntities();
            return db.PayOffs.FirstOrDefault(s => s.POID.Equals(id));
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
                    PayOffs st = db.PayOffs.FirstOrDefault(s => s.POID.Equals(id));
                    st.POState = 1;
                    ObjectParameter para = new ObjectParameter("DD", "");
                    db.pro_order("InOutDepot", "IODNum", "RK", para);
                    string IODNum = para.Value.ToString();
                    //添加出库记录
                    db.InOutDepot.Add(new InOutDepot() { DepotID = st.DepotID, IODType = 1, IODNum = IODNum, IODDate = DateTime.Now, IODUser = userid, IODDesc = st.PODesc });
                    db.SaveChanges();
                    int inod = db.InOutDepot.Max(i => i.IODID);
                    List<PayOffDetail> list = db.PayOffDetail.Where(p => p.POID.Equals(id)).ToList();//销售出库详单
                    foreach (PayOffDetail item in list)
                    {

                        if (db.DepotStock.FirstOrDefault(p => p.DepotID.Equals(st.DepotID) && p.ProID == item.ProID) == null)
                        {//如果指定的仓库中不存在该商品，添加库存
                            db.DepotStock.Add(new DepotStock() { DepotID=st.DepotID, ProID=item.ProID, DSAmount=item.PODAmount, DSPrice=item.PODPrice });
                        }
                        else
                        {//如果指定仓库存在这个商品，修改这个商品的库存
                            DepotStock ds = db.DepotStock.FirstOrDefault(d => d.ProID == item.ProID && d.DepotID == st.DepotID);
                            ds.DSAmount = ds.DSAmount + item.PODAmount;
                        }
                        //添加入库记录详情
                        db.InOutDepotDetail.Add(new InOutDepotDetail() { IODID = inod, ProID = item.ProID, IODDAmount = item.PODAmount, IODDPrice = item.PODPrice });
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
        public static List<PayOffs> Find(string POID, string DepotID, string PODate, string UsersName, int POState, int PageIndex, int PageSize, out int count)
        {
            PSSEntities db = new PSSEntities();
            var list = from s in db.PayOffs select s;
            if (POID != null && POID.Trim().Length > 0)//入库单编号
                list = list.Where(s => s.POID.Contains(POID));

            if (DepotID != null && DepotID.Trim().Length > 0)//仓库编号
                list = list.Where(s => s.DepotID.Equals(DepotID));

            if (PODate != null && PODate.Trim().Length > 0)//入库时间
            {
                DateTime dt = Convert.ToDateTime(PODate);
                list = list.Where(s => s.PODate >= dt);
            }
            if (UsersName != null && UsersName.Trim().Length > 0)//创建人
                list = list.Where(s => s.Users.UsersName.Contains(UsersName));

            if (POState != -1)//订单状态
                list = list.Where(s => s.POState == POState);


            count = list.Count();
            return list.OrderBy(s => s.POID).Skip((PageIndex - 1) * PageSize).Take(PageSize).ToList();
        }
    }
}