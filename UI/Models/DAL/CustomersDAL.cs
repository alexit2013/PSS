using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using UI.Models.Model;

namespace UI.Models.DAL
{
    public class CustomersDAL
    {
        /// <summary>
        /// 查询所有的客户信息
        /// </summary>
        /// <returns></returns>
        public static List<Customers> GetAll() {
            PSSEntities db = new PSSEntities();
            return db.Customers.Select(c => c).ToList<Customers>();
        }
        /// <summary>
        /// 分页查询客户信息
        /// </summary>
        /// <param name="PageIndex">当前页</param>
        /// <param name="PageSize">页大小</param>
        /// <returns></returns>
        public static List<Customers> GetAllPage(int PageIndex,int PageSize) {
            PSSEntities db = new PSSEntities();
            return db.Customers.OrderBy(c=>c.CusID).Skip((PageIndex-1)*PageSize).Take(PageSize).ToList<Customers>();
        }
        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="cus"></param>
        /// <returns></returns>
        public static int Add(Customers cus) {
            PSSEntities db = new PSSEntities();
            db.Customers.Add(cus);
            return db.SaveChanges();
        }
        /// <summary>
        /// 修改客户信息
        /// </summary>
        /// <param name="cus"></param>
        /// <returns></returns>
        public static int Edit(Customers cus)
        {
            PSSEntities db = new PSSEntities();
            db.Entry<Customers>(cus).State = System.Data.Entity.EntityState.Modified;
            return db.SaveChanges();
        }
        /// <summary>
        /// 删除客户信息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static int Delete(string id) {
            PSSEntities db = new PSSEntities();
            db.Customers.Remove(db.Customers.FirstOrDefault(c=>c.CusID==id));
            return db.SaveChanges();
        }


    }
}