using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using UI.Models.Model;

namespace UI.Models.DAL
{
    public class SplitsDAL
    {
        /// <summary>
        /// 查询所有
        /// </summary>
        /// <returns></returns>
        public static List<Splits> GetAll()
        {
            PSSEntities db = new PSSEntities();
            return db.Splits.Select(d => d).ToList();
        }
        /// <summary>
        /// 添加【---事务---】
        /// </summary>
        /// <param name="dep"></param>
        /// <returns></returns>
        public static int AddStocks(Splits dep, List<SplitDetail> list)
        {
            PSSEntities db = new PSSEntities();
            int fg = 1;
            using (var tx = db.Database.BeginTransaction())
            {
                try
                {
                    db.Splits.Add(dep);
                    if (list != null) db.SplitDetail.AddRange(list);
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
                    db.SplitDetail.RemoveRange(db.SplitDetail.Where(d => d.SplitID.Equals(id)));
                    db.Splits.Remove(db.Splits.FirstOrDefault(s => s.SplitID.Equals(id)));
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
        public static int EdiStocks(Splits dep, List<SplitDetail> list)
        {
            PSSEntities db = new PSSEntities();
            int fg = 1;
            ///以下开始事务操作
            using (var tx = db.Database.BeginTransaction())
            {
                try
                {
                    //将要修改的创建人找回来，避免修改时没有创建人
                    Splits st = db.Splits.FirstOrDefault(p => p.SplitID == dep.SplitID);
                    st.DepotID = dep.DepotID;//商品仓库
                    st.ProID = dep.ProID;//拆分商品
                    st.SplitDate = dep.SplitDate;//拆分时间
                    st.SplitDesc = dep.SplitDesc;//备注
                    st.SplitAmount = dep.SplitAmount;//拆分数量
                    st.SplitPrice = dep.SplitPrice;//价格
                    db.SplitDetail.RemoveRange(db.SplitDetail.Where(p => p.SplitID == dep.SplitID));//删除详单
                    if (list != null) db.SplitDetail.AddRange(list);//如果添加的详单不为空的话，那么就添加详单
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
        internal static Splits GetByID(string id)
        {
            PSSEntities db = new PSSEntities();
            return db.Splits.FirstOrDefault(s => s.SplitID.Equals(id));
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
                       1、将盘点状态改为1
                       2、直接修改对应仓库中的商品库存为拆分商品后的实际库存
                    */
                    Splits st = db.Splits.FirstOrDefault(s => s.SplitID.Equals(id));
                    st.SplitState = 1;
                    List<SplitDetail> list = db.SplitDetail.Where(p => p.SplitID.Equals(id)).ToList();//销售出库详单
                    DepotStock ds = db.DepotStock.FirstOrDefault(s => s.DepotID == st.DepotID && s.ProID == st.ProID);
                    if (ds==null)
                    {
                        throw new Exception("拆分商品未在当前仓库中！！！");
                    }
                    if (ds.DSAmount<st.SplitAmount)//判断库存是否充足
                    {
                        throw new Exception("拆分数量超出当前商品的库存数量！！");
                    }
                    else
                    {
                        ds.DSAmount -= st.SplitAmount;//减少被拆分商品的库存
                    }
                    foreach (SplitDetail item in list)
                    {
                        //如果拆分后的商品在当前仓库中不存在
                        if (db.DepotStock.FirstOrDefault(p => p.DepotID.Equals(st.DepotID) && p.ProID == item.ProID) == null)
                        {
                            //添加新的商品库存
                            db.DepotStock.Add(new DepotStock() { DepotID =st.DepotID, ProID =item.ProID, DSAmount =item.SDAmount, DSPrice =item.SDPrice });
                        }
                        else
                        {//如果指定仓库存在这个商品，修改这个商品的库存
                            DepotStock dss = db.DepotStock.FirstOrDefault(d => d.ProID == item.ProID && d.DepotID == st.DepotID);
                            dss.DSAmount += item.SDAmount;
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
      /// 条件查询
      /// </summary>
      /// <param name="SplitID">拆分ID</param>
      /// <param name="DepotID">仓库ID</param>
      /// <param name="ProName">商品名称</param>
      /// <param name="UsersName">操作人</param>
      /// <param name="SplitDate">拆分时间</param>
      /// <param name="SplitState">状态</param>
      /// <param name="PageIndex">当前页</param>
      /// <param name="PageSize">页大小</param>
      /// <param name="count">总条数</param>
      /// <returns></returns>
        public static List<Splits> Find(string SplitID, string DepotID, string ProName,string UsersName, string SplitDate, int SplitState, int PageIndex, int PageSize, out int count)
        {
            PSSEntities db = new PSSEntities();
            var list = from s in db.Splits select s;
            if (SplitID != null && SplitID.Trim().Length > 0)//入库单编号
                list = list.Where(s => s.SplitID.Contains(SplitID));

            if (DepotID != null && DepotID.Trim().Length > 0)//盘点仓库
                list = list.Where(s => s.DepotID.Equals(DepotID));

            if (ProName != null && ProName.Trim().Length > 0)//商品名称
                list = list.Where(s => s.Products.ProName.Contains(ProName));

            if (SplitDate != null && SplitDate.Trim().Length > 0)//入库时间
            {
                DateTime dt = Convert.ToDateTime(SplitDate);
                list = list.Where(s => s.SplitDate >= dt);
            }
            if (UsersName != null && UsersName.Trim().Length > 0)//创建人
                list = list.Where(s => s.Users.UsersName.Contains(UsersName));

            if (SplitState != -1)//订单状态
                list = list.Where(s => s.SplitState == SplitState);

            count = list.Count();
            return list.OrderBy(s => s.SplitID).Skip((PageIndex - 1) * PageSize).Take(PageSize).ToList();
        }
       
    }
}