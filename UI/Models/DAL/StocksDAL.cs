using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using UI.Models.Model;


namespace UI.Models.DAL
{
    public class StocksDAL
    {
      /// <summary>
      /// 查询所有
      /// </summary>
      /// <returns></returns>
        public static List<Stocks> GetAll()
        {
            PSSEntities db = new PSSEntities();
            return db.Stocks.Select(d => d).ToList<Stocks>();
        }
    
      /// <summary>
      /// 分页查询
      /// </summary>
      /// <param name="PageIndex"></param>
      /// <param name="PageSize"></param>
      /// <returns></returns>
        public static List<Stocks> GetAllPage(int PageIndex, int PageSize)
        {
            PSSEntities db = new PSSEntities();
             return db.Stocks.Select(d => d).OrderBy(d => d.StockID).Skip((PageIndex - 1) * PageSize).Take(PageSize).ToList<Stocks>();
        }
      /// <summary>
      /// 添加【---事务---】
      /// </summary>
      /// <param name="dep"></param>
      /// <returns></returns>
        public static int AddStocks(Stocks dep,List<StockDetail> list)
        {
            PSSEntities db = new PSSEntities();
            int fg = 1;
            using (var tx=db.Database.BeginTransaction())
            {
                try
                {
                    db.Stocks.Add(dep);
                    if(list!=null)
                        db.StockDetail.AddRange(list);

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
            using (var tx = db.Database.BeginTransaction())
            {
              try
                {
                    db.Stocks.Remove(db.Stocks.FirstOrDefault(d => d.StockID.Equals(id)));
                    db.StockDetail.RemoveRange(db.StockDetail.Where(s=>s.StockID.Equals(id)));
                    tx.Commit();
                }
                catch (Exception e)
                {
                    tx.Rollback();
                    throw new Exception(e.Message);
                }
            }
            return db.SaveChanges();
        }
      /// <summary>
      /// 修改【---事务---】
      /// </summary>
      /// <param name="dp"></param>
      /// <returns></returns>
        public static int EdiStocks(Stocks dp, List<StockDetail> list)
        {
            PSSEntities db = new PSSEntities();
            int fg = 1;
           ///以下开始事务操作
            using (var tx=db.Database.BeginTransaction())
            {
                try
                {
                    //将要修改的创建人找回来，避免修改时没有创建人
                    Stocks st = db.Stocks.FirstOrDefault(p=>p.StockID==dp.StockID);
                    st.PPID = dp.PPID;
                    st.StockDate = dp.StockDate;
                    st.StockInDate = dp.StockInDate;
                    st.StockDesc = dp.StockDesc;
                    db.StockDetail.RemoveRange(db.StockDetail.Where(p => p.StockID == dp.StockID));
                    if(list!=null)
                         db.StockDetail.AddRange(list);

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
        internal static Stocks GetByID(string id)
        {
            PSSEntities db = new PSSEntities();
            return db.Stocks.FirstOrDefault(s=>s.StockID.Equals(id));
        }
        /// <summary>
        /// 审核采购订单
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static int CKStocks(string id)
        {
            PSSEntities db = new PSSEntities();
            ///以下开始事务操作
            using (var tx = db.Database.BeginTransaction())
            {
                try
                {
                    //将要修改的创建人找回来，避免修改时没有创建人
                    Stocks st = db.Stocks.FirstOrDefault(p => p.StockID == id);
                    st.StockState = 1;
                    tx.Commit();
                }
                catch (Exception e)
                {
                    tx.Rollback();
                    throw new Exception(e.Message);
                }

            }
            return db.SaveChanges();
        }
        public static List<Stocks> Find(string StockID, string UsersName, string PPID, string StockDate, string StockInDate,int PageIndex,int PageSize,out int count,int? StockState) {
            PSSEntities db = new PSSEntities();
           
          
            var list = from s in db.Stocks select s;
            if (StockID!=null&&StockID.Trim().Length>0)
            {
                list = list.Where(p=>p.StockID.Contains(StockID));
            }
            if (UsersName != null && UsersName.Trim().Length > 0)
            {
                list = list.Where(s=>s.Users.UsersName.Contains(UsersName));
            }
            if (StockState!=null&& StockState>-1)
            {
                list= list.Where(s => s.StockState == StockState);
            }
            if (PPID != null && PPID.Trim().Length > 0)
            {
                list = list.Where(s=>s.PPID==PPID);
            }
            if (StockDate != null && StockDate.Trim().Length > 0)
            {
                DateTime dt1 = Convert.ToDateTime(StockDate);
                list = list.Where(s=>s.StockDate>=dt1);
            }
            if (StockInDate != null && StockInDate.Trim().Length > 0)
            {
                DateTime dt2 = Convert.ToDateTime(StockInDate);
                list = list.Where(s => s.StockInDate >= dt2);
            }
            count = list.Count();
            return list.OrderBy(s=>s.StockID).Skip((PageIndex-1)*PageSize).Take(PageSize).ToList<Stocks>();
        }
        /// <summary>
        /// 根据时间查询当前月的采购单个数
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        public static int FindByYCount(DateTime dt) {
            PSSEntities db = new PSSEntities();
            string sql = "select * from stocks where datepart(MM,stockdate)="+dt.Month+ " and  datepart(YYYY,stockdate) = "+dt.Year;
            List<Stocks> list=  db.Database.SqlQuery<Stocks>(sql).ToList();
            return list.Count();
        }
        public static int FindByYCount1(DateTime dt)
        {
            PSSEntities db = new PSSEntities();
            string sql = "select * from stocks where datepart(MM,StockInDate)=" + dt.Month + " and  datepart(YYYY,StockInDate) = " + dt.Year;
            List<Stocks> list = db.Database.SqlQuery<Stocks>(sql).ToList();
            return list.Count();
        }
        /// <summary>
        ///查询审核订单和未审核订单数
        /// </summary>
        /// <param name="dt"></param>
        /// <param name="state"></param>
        /// <returns></returns>
        public static int FindStateCount(int state) {
            string sql= "select *  from stocks where  StockState = "+state;
            PSSEntities db = new PSSEntities();
            int count = db.Database.SqlQuery<Stocks>(sql).ToList().Count;
            return count;
        }
    }
}