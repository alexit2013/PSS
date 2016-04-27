using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Objects;
using System.Linq;
using System.Web;
using UI.Models.Model;

namespace UI.Models.DAL
{
    public class CheckDepotDAL
    {
        /// <summary>
        /// 查询所有
        /// </summary>
        /// <returns></returns>
        public static List<CheckDepot> GetAll()
        {
            PSSEntities db = new PSSEntities();
            return db.CheckDepot.Select(d => d).ToList();
        }
        /// <summary>
        /// 添加【---事务---】
        /// </summary>
        /// <param name="dep"></param>
        /// <returns></returns>
        public static int AddStocks(CheckDepot dep, List<CheckDepotDetail> list)
        {
            PSSEntities db = new PSSEntities();
            int fg = 1;
            using (var tx = db.Database.BeginTransaction())
            {
                try
                {
                    db.CheckDepot.Add(dep);
                    if (list != null) db.CheckDepotDetail.AddRange(list);
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
                    db.CheckDepotDetail.RemoveRange(db.CheckDepotDetail.Where(d => d.CDID.Equals(id)));
                    db.CheckDepot.Remove(db.CheckDepot.FirstOrDefault(s => s.CDID.Equals(id)));
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
        public static int EdiStocks(CheckDepot dep, List<CheckDepotDetail> list)
        {
            PSSEntities db = new PSSEntities();
            int fg = 1;
            ///以下开始事务操作
            using (var tx = db.Database.BeginTransaction())
            {
                try
                {
                    //将要修改的创建人找回来，避免修改时没有创建人
                    CheckDepot st = db.CheckDepot.FirstOrDefault(p => p.CDID == dep.CDID);
                    st.DepotID = dep.DepotID;//盘点仓库
                    st.CDDate = dep.CDDate;//盘点时间
                    st.CDDesc = dep.CDDesc;//备注
                    db.CheckDepotDetail.RemoveRange(db.CheckDepotDetail.Where(p => p.CDID == dep.CDID));//删除详单
                    if (list != null) db.CheckDepotDetail.AddRange(list);//如果添加的详单不为空的话，那么就添加详单
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
        internal static CheckDepot GetByID(string id)
        {
            PSSEntities db = new PSSEntities();
            return db.CheckDepot.FirstOrDefault(s => s.CDID.Equals(id));
        }
        /// <summary>
        /// 审核订单【修改后不可修改，直接影响库存数据】
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static int CKDepot(string id, int userid)
        {
            PSSEntities db = new PSSEntities();
            int fg = 1;
            ///以下开始事务操作
            using (var tx = db.Database.BeginTransaction())
            {
                try
                {
                    /*
                      --------------------------------------- 【库存盘点】-------------------------------------------------
                       1、将盘点状态改为3
                       2、直接修改对应仓库中的商品库存为盘点后的实际库存
                    */
                    CheckDepot st = db.CheckDepot.FirstOrDefault(s => s.CDID.Equals(id));
                    st.CDState = 2;
                    List<CheckDepotDetail> list = db.CheckDepotDetail.Where(p => p.CDID.Equals(id)).ToList();//销售出库详单
                    foreach (CheckDepotDetail item in list)
                    {
                        //如果盘点的仓库中不存在该商品
                        if (db.DepotStock.FirstOrDefault(p => p.DepotID.Equals(st.DepotID) && p.ProID == item.ProID) == null)
                        {
                            throw new Exception("商品不存在该仓库中！");
                        }
                        else
                        {//如果指定仓库存在这个商品，修改这个商品的库存为盘点后的库存
                            DepotStock ds = db.DepotStock.FirstOrDefault(d => d.ProID == item.ProID && d.DepotID == st.DepotID);
                            ds.DSAmount =  item.CDDAmount1;
                        }
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
        /// 盘点【将状态改为二】
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static int PDDepot(string id)
        {
            PSSEntities db = new PSSEntities();
            int fg = 1;
            using (var tx=db.Database.BeginTransaction())
            {
                try
                {
                    CheckDepot cd = db.CheckDepot.FirstOrDefault(s => s.CDID == id);
                    cd.CDState =1;
                    List<CheckDepotDetail> list = db.CheckDepotDetail.Where(s => s.CDID == id).ToList();
                    List<DepotStock> dps = db.DepotStock.Where(p => p.DepotID == cd.DepotID).ToList();
                    foreach (var item in list)
                    {
                        foreach (DepotStock d in dps)
                        {
                            if (item.ProID == d.ProID)
                            {
                                item.DevAmount2 = d.DSAmount;
                                break;
                            }
                        }
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
        public static List<CheckDepot> Find(string CDID, string DepotID, string UsersName, string CDDate, int CDState, int PageIndex, int PageSize, out int count)
        {
            PSSEntities db = new PSSEntities();
            var list = from s in db.CheckDepot select s;
            if (CDID != null && CDID.Trim().Length > 0)//入库单编号
                list = list.Where(s => s.CDID.Contains(CDID));

            if (DepotID != null && DepotID.Trim().Length > 0)//盘点仓库
                list = list.Where(s => s.DepotID.Equals(DepotID));

            if (CDDate != null && CDDate.Trim().Length > 0)//入库时间
            {
                DateTime dt = Convert.ToDateTime(CDDate);
                list = list.Where(s => s.CDDate >= dt);
            }
            if (UsersName != null && UsersName.Trim().Length > 0)//创建人
                list = list.Where(s => s.Users.UsersName.Contains(UsersName));

            if (CDState != -1)//订单状态
                list = list.Where(s => s.CDState == CDState);

            count = list.Count();
            return list.OrderBy(s => s.CDID).Skip((PageIndex - 1) * PageSize).Take(PageSize).ToList();
        }
        /// <summary>
        /// 根据仓库ID查询商品信息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        internal static List<Products> GetByDepotID(string id)
        {
            PSSEntities db = new PSSEntities();
            List<DepotStock> dps = db.DepotStock.Where(p => p.DepotID == id).ToList();
            List<int?> proids = new List<int?>();
            foreach (DepotStock item in dps)
            {
                proids.Add(item.ProID);
            }
             return   db.Products.Where(p => proids.Contains(p.ProID)).ToList();
        }
    }
}