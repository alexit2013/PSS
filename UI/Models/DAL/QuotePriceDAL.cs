using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Objects;
using System.Linq;
using System.Web;
using UI.Models.Model;

namespace UI.Models.DAL
{
    public class QuotePriceDAL
    {
        /// <summary>
        /// 查询所有
        /// </summary>
        /// <returns></returns>
        public static List<QuotePrice> GetAll()
        {
            PSSEntities db = new PSSEntities();
            return db.QuotePrice.Select(d => d).ToList();
        }
        /// <summary>
        /// 添加【---事务---】
        /// </summary>
        /// <param name="dep"></param>
        /// <returns></returns>
        public static int AddStocks(QuotePrice dep, List<QuotePriceDetail> list)
        {
            PSSEntities db = new PSSEntities();
            int fg = 1;
            using (var tx = db.Database.BeginTransaction())
            {
                try
                {
                    db.QuotePrice.Add(dep);
                    if (list != null) db.QuotePriceDetail.AddRange(list);
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
                    db.QuotePriceDetail.RemoveRange(db.QuotePriceDetail.Where(s => s.QPID.Equals(id)));
                    db.QuotePrice.Remove(db.QuotePrice.FirstOrDefault(d => d.QPID.Equals(id)));
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
        public static int EdiStocks(QuotePrice dep, List<QuotePriceDetail> list)
        {
            PSSEntities db = new PSSEntities();
            int fg = 1;
            ///以下开始事务操作
            using (var tx = db.Database.BeginTransaction())
            {
                try
                {
                    //将要修改的创建人找回来，避免修改时没有创建人
                    QuotePrice st = db.QuotePrice.FirstOrDefault(p => p.QPID == dep.QPID);
                    st.CusID = dep.CusID;
                    st.QPDate = dep.QPDate;
                    st.QPDesc = dep.QPDesc;
                    db.QuotePriceDetail.RemoveRange(db.QuotePriceDetail.Where(p => p.QPID == dep.QPID));
                    if (list != null) db.QuotePriceDetail.AddRange(list);
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
        internal static QuotePrice GetByID(string id)
        {
            PSSEntities db = new PSSEntities();
            return db.QuotePrice.FirstOrDefault(s => s.QPID.Equals(id));
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
                    QuotePrice st = db.QuotePrice.FirstOrDefault(s => s.QPID.Equals(id));
                    st.QPState = 1;
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
        public static List<QuotePrice> Find(string QPID, string CusID, string QPDate, string UsersName, int QPState, int PageIndex, int PageSize, out int count)
        {
            PSSEntities db = new PSSEntities();
            var list = from s in db.QuotePrice select s;
            if (QPID != null && QPID.Trim().Length > 0)//入库单编号
                list = list.Where(s => s.QPID.Contains(QPID));

            if (CusID != null && CusID.Trim().Length > 0)//仓库编号
                list = list.Where(s => s.CusID.Equals(CusID));

            if (QPDate != null && QPDate.Trim().Length > 0)//入库时间
            {
                DateTime dt = Convert.ToDateTime(QPDate);
                list = list.Where(s => s.QPDate >= dt);
            }
            if (UsersName != null && UsersName.Trim().Length > 0)//创建人
                list = list.Where(s => s.Users.UsersName.Contains(UsersName));

            if (QPState != -1)//订单状态
                list = list.Where(s => s.QPState == QPState);


            count = list.Count();
            return list.OrderBy(s => s.QPID).Skip((PageIndex - 1) * PageSize).Take(PageSize).ToList();
        }
    }
}