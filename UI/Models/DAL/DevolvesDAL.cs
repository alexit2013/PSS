using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Objects;
using System.Linq;
using System.Web;
using UI.Models.Model;

namespace UI.Models.DAL
{
    public class DevolvesDAL
    {
        /// <summary>
        /// 查询所有
        /// </summary>
        /// <returns></returns>
        public static List<Devolves> GetAll()
        {
            PSSEntities db = new PSSEntities();
            return db.Devolves.Select(d => d).ToList();
        }
        /// <summary>
        /// 添加【---事务---】
        /// </summary>
        /// <param name="dep"></param>
        /// <returns></returns>
        public static int AddStocks(Devolves dep, List<DevolveDetail> list)
        {
            PSSEntities db = new PSSEntities();
            int fg = 1;
            using (var tx = db.Database.BeginTransaction())
            {
                try
                {
                    db.Devolves.Add(dep);
                    if (list != null) db.DevolveDetail.AddRange(list);
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
                    db.DevolveDetail.RemoveRange(db.DevolveDetail.Where(d => d.DevID.Equals(id)));
                    db.Devolves.Remove(db.Devolves.FirstOrDefault(s => s.DevID.Equals(id)));
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
        public static int EdiStocks(Devolves dep, List<DevolveDetail> list)
        {
            PSSEntities db = new PSSEntities();
            int fg = 1;
            ///以下开始事务操作
            using (var tx = db.Database.BeginTransaction())
            {
                try
                {
                    //将要修改的创建人找回来，避免修改时没有创建人
                    Devolves st = db.Devolves.FirstOrDefault(p => p.DevID == dep.DevID);
                    st.DevInID = dep.DevInID;//调入仓库
                    st.DevOutID = dep.DevOutID;//调出仓库
                    st.DevDesc = dep.DevDesc;//描述
                    st.DevDate = dep.DevDate;//操作时间
                    db.DevolveDetail.RemoveRange(db.DevolveDetail.Where(p => p.DevID == dep.DevID));//删除详单
                    if (list != null) db.DevolveDetail.AddRange(list);//如果添加的详单不为空的话，那么就添加详单
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
        internal static Devolves GetByID(string id)
        {
            PSSEntities db = new PSSEntities();
            return db.Devolves.FirstOrDefault(s => s.DevID.Equals(id));
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
                    /*
                      --------------------------------------- 【库存调拨】-------------------------------------------------

                       1、将仓库A中的商品调入到仓库里B中去
                       2、修改A、B仓库中调拨商品的商品数量，
                       3、分别为A、B仓库添加对应的出入库记录与详情

                    */
                    Devolves st = db.Devolves.FirstOrDefault(s => s.DevID.Equals(id));
                    st.DevState = 1;
                    ObjectParameter para = new ObjectParameter("DD", "");
                    db.pro_order("InOutDepot", "IODNum", "RK", para);
                    string IODNum = para.Value.ToString();
                    //为A仓库添加出库记录
                    db.InOutDepot.Add(new InOutDepot() { DepotID = st.DevOutID, IODType =2, IODNum = IODNum, IODDate = DateTime.Now, IODUser = userid, IODDesc = st.DevDesc });
                    db.SaveChanges();
                    int outinod = db.InOutDepot.Max(i => i.IODID);
                    //为B仓库添加入库记录
                    db.InOutDepot.Add(new InOutDepot() { DepotID = st.DevInID, IODType =1 , IODNum = IODNum, IODDate = DateTime.Now, IODUser = userid, IODDesc = st.DevDesc });
                    db.SaveChanges();
                    int ininod = db.InOutDepot.Max(i => i.IODID);
                    List<DevolveDetail> list = db.DevolveDetail.Where(p => p.DevID.Equals(id)).ToList();//销售出库详单
                    foreach (DevolveDetail item in list)
                    {
                        //如果调出的仓库中不存在该商品
                        if (db.DepotStock.FirstOrDefault(p => p.DepotID.Equals(st.DevOutID) && p.ProID == item.ProID) == null)
                        {
                            throw new Exception("调出商品不存在仓库中！");
                        }
                        else
                        {//如果指定仓库存在这个商品，修改这个商品的库存
                            DepotStock ds = db.DepotStock.FirstOrDefault(d => d.ProID == item.ProID && d.DepotID == st.DevOutID);
                            ds.DSAmount = ds.DSAmount - item.DevDAmount;
                        }
                        //如果调入仓库中不存在该商品
                        if (db.DepotStock.FirstOrDefault(p => p.DepotID.Equals(st.DevOutID) && p.ProID == item.ProID) == null)
                        {
                            //为调入仓库添加新的商品库存
                            db.DepotStock.Add(new DepotStock() { DepotID=st.DevInID, ProID=item.ProID, DSAmount=item.DevDAmount });
                        }
                        else
                        {//如果指定仓库存在这个商品，修改这个商品的库存
                            DepotStock ds = db.DepotStock.FirstOrDefault(d => d.ProID == item.ProID&&d.DepotID==st.DevOutID);
                            ds.DSAmount = ds.DSAmount + item.DevDAmount;
                        }

                        //添加出库记录详情
                        db.InOutDepotDetail.Add(new InOutDepotDetail() { IODID = outinod, ProID = item.ProID, IODDAmount = item.DevDAmount });
                        //添加入库记录详情
                        db.InOutDepotDetail.Add(new InOutDepotDetail() { IODID = ininod, ProID = item.ProID, IODDAmount = item.DevDAmount });
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
        public static List<Devolves> Find(string DevID, string DevOutID, string DevInID, string UsersName, string DevDate,int DevState, int PageIndex, int PageSize, out int count)
        {
            PSSEntities db = new PSSEntities();
            var list = from s in db.Devolves select s;
            if (DevID != null && DevID.Trim().Length > 0)//入库单编号
                list = list.Where(s => s.DevID.Contains(DevID));

            if (DevOutID != null && DevOutID.Trim().Length > 0)//调出仓库
                list = list.Where(s => s.DevOutID.Equals(DevOutID));

            if (DevInID != null && DevInID.Trim().Length > 0)//调入仓库
                list = list.Where(s => s.DevInID.Equals(DevInID));

            if (DevDate != null && DevDate.Trim().Length > 0)//入库时间
            {
                DateTime dt = Convert.ToDateTime(DevDate);
                list = list.Where(s => s.DevDate >= dt);
            }
            if (UsersName != null && UsersName.Trim().Length > 0)//创建人
                list = list.Where(s => s.Users.UsersName.Contains(UsersName));

            if (DevState != -1)//订单状态
                list = list.Where(s => s.DevState == DevState);

            count = list.Count();
            return list.OrderBy(s => s.DevID).Skip((PageIndex - 1) * PageSize).Take(PageSize).ToList();
        }

        /// <summary>
        /// 根据时间查询当前月订单个数
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        public static int FindByYCount(DateTime dt)
        {
            PSSEntities db = new PSSEntities();
            string sql = "select * from Devolves where datepart(MM,DevDate)=" + dt.Month + " and  datepart(YYYY,DevDate) = " + dt.Year;
            List<Devolves> list = db.Database.SqlQuery<Devolves>(sql).ToList();
            return list.Count();
        }
    }
}