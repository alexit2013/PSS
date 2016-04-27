using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using UI.Models.Model;

namespace UI.Models.DAL
{
    public class CustomerOrderDAL
    {
        /// <summary>
        /// 添加客户订单
        /// </summary>
        /// <param name="qp"></param>
        /// <param name="list"></param>
        /// <returns></returns>
        internal static int AddStocks(CustomerOrder qp, List<CustomerOrderDetail> list)
        {
            PSSEntities db = new PSSEntities();
            int fg = 1;
            using (var tx = db.Database.BeginTransaction())
            {
                try
                {
                    db.CustomerOrder.Add(qp);
                    if (list != null)
                        db.CustomerOrderDetail.AddRange(list);

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
        /// 查询所有
        /// </summary>
        /// <returns></returns>
        internal static List<CustomerOrder> GetAll()
        {
            PSSEntities db = new PSSEntities();
            return db.CustomerOrder.Select(p => p).ToList();
        }
        /// <summary>
        /// 条件查询
        /// </summary>
        /// <param name="COID"></param>
        /// <param name="CusID"></param>
        /// <param name="CODate"></param>
        /// <param name="CORefDate"></param>
        /// <param name="UsersName"></param>
        /// <param name="COState"></param>
        /// <param name="PageIndex"></param>
        /// <param name="PageSize"></param>
        /// <param name="count"></param>
        /// <returns></returns>
        internal static List<CustomerOrder> Find(string COID, string CusID, string CODate, string CORefDate, string UsersName, int COState, int PageIndex, int PageSize,out int count)
        {
            PSSEntities db = new PSSEntities();
            var list = from c in db.CustomerOrder select c;
            if (COID!=null&&COID.Trim().Length>0){
                list = list.Where(p=>p.COID.Contains(COID));
            }
            if (CusID != null && CusID.Trim().Length > 0)
            {
                list = list.Where(p => p.CusID.Equals(CusID));
            }
            if (CODate != null && CODate.Trim().Length > 0)
            {
                DateTime dt = Convert.ToDateTime(CODate);
                list = list.Where(p => p.CODate>=dt);
            }
            if (CORefDate != null && CORefDate.Trim().Length > 0)
            {
                DateTime dt = Convert.ToDateTime(CORefDate);
                list = list.Where(p => p.CORefDate >= dt);
            }
            if (UsersName != null && UsersName.Trim().Length > 0)
            {
                list = list.Where(p => p.Users.UsersName.Contains(UsersName));
            }
            if (COState != -1)
            {
                list = list.Where(p=>p.COState==COState);
            }
            count = list.Count();
            return list.OrderBy(p=>p.COID).Skip((PageIndex-1)*PageSize).Take(PageSize).ToList();
        }
        /// <summary>
        /// 修改客户订单
        /// </summary>
        /// <param name="qp"></param>
        /// <param name="list"></param>
        /// <returns></returns>
        internal static int EditStocks(CustomerOrder qp, List<CustomerOrderDetail> list)
        {
            PSSEntities db = new PSSEntities();
            int fg = 1;
            using (var tx=db.Database.BeginTransaction())
            {
                try
                {
                    CustomerOrder c = db.CustomerOrder.FirstOrDefault(p=>p.COID.Equals(qp.COID));
                    c.CusID = qp.CusID;
                    c.CODate = qp.CODate;
                    c.CORefDate = qp.CORefDate;
                    c.CODesc = qp.CODesc;
                    db.CustomerOrderDetail.RemoveRange(db.CustomerOrderDetail.Where(s=>s.COID.Equals(qp.COID)));
                    if(list!=null)
                         db.CustomerOrderDetail.AddRange(list);

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
        /// 根据ID查询客户订单信息
        /// </summary>
        /// <param name="id"></param>
        internal static CustomerOrder GetCustomerOrderByID(string id)
        {
            PSSEntities db = new PSSEntities();
            return db.CustomerOrder.FirstOrDefault(p=>p.COID.Equals(id));
        }

        /// <summary>
        /// 审核客户订单
        /// </summary>
        /// <param name="id"></param>
        /// <param name="userid"></param>
        /// <returns></returns>
        internal static int CKInDepot(string id, int userid)
        {
            PSSEntities db = new PSSEntities();
            int fg = 1;
            using (var tx = db.Database.BeginTransaction())
            {
                try
                {
                    CustomerOrder co = db.CustomerOrder.FirstOrDefault(p=>p.COID.Equals(id));
                    co.COState = 1;
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
        /// 删除客户订单
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        internal static int DelStocks(string id)
        {
            PSSEntities db = new PSSEntities();
            int fg = 1;
            using (var tx=db.Database.BeginTransaction())
            {
                try
                {
                    db.CustomerOrderDetail.RemoveRange(db.CustomerOrderDetail.Where(p=>p.COID.Equals(id)));
                    db.CustomerOrder.Remove(db.CustomerOrder.FirstOrDefault(p => p.COID.Equals(id)));
                    db.SaveChanges();
                    tx.Commit();
                }
                catch (Exception ex)
                {
                    fg =0;
                    tx.Rollback();
                    throw new Exception(ex.Message);
                }
            }
            return fg;
        }

        /// <summary>
        /// 根据时间查询当前月的采购单个数
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        public static int FindByYCount(DateTime dt)
        {
            PSSEntities db = new PSSEntities();
            string sql = "select * from CustomerOrder where datepart(MM,CODate)=" + dt.Month + " and  datepart(YYYY,CODate) = " + dt.Year;
            List<CustomerOrder> list = db.Database.SqlQuery<CustomerOrder>(sql).ToList();
            return list.Count();
        }
        public static int FindByYCount1(DateTime dt)
        {
            PSSEntities db = new PSSEntities();
            string sql = "select * from CustomerOrder where datepart(MM,CORefDate)=" + dt.Month + " and  datepart(YYYY,CORefDate) = " + dt.Year;
            List<CustomerOrder> list = db.Database.SqlQuery<CustomerOrder>(sql).ToList();
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
            string sql = "select *  from CustomerOrder where  COState = " + state;
            PSSEntities db = new PSSEntities();
            int count = db.Database.SqlQuery<CustomerOrder>(sql).ToList().Count;
            return count;
        }
    }
}