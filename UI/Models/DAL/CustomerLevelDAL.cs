using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using UI.Models.Model;

namespace UI.Models.DAL
{
    public class CustomerLevelDAL
    {
        /// <summary>
        /// 查询所有的客户等级信息
        /// </summary>
        /// <returns></returns>
        public static List<CustomerLevel> GetAll() {
            PSSEntities db = new PSSEntities();
            return db.CustomerLevel.Select(c=>c).ToList<CustomerLevel>();
        }
        /// <summary>
        /// 分页查询客户等级信息
        /// </summary>
        /// <param name="PageIndex"></param>
        /// <param name="PageSize"></param>
        /// <returns></returns>
        public static List<CustomerLevel> GetAllPage(int PageIndex,int PageSize) {
            PSSEntities db = new PSSEntities();
            return db.CustomerLevel.OrderBy(c => c.CLID).Skip((PageIndex - 1) * PageSize).Take(PageSize).ToList<CustomerLevel>();
        }
        /// <summary>
        /// 添加客户等级
        /// </summary>
        /// <param name="cusl"></param>
        /// <returns></returns>
        public static int Add(CustomerLevel cusl) {
            PSSEntities db = new PSSEntities();
            db.CustomerLevel.Add(cusl);
            return db.SaveChanges();
        }
        /// <summary>
        /// 修改客户等级
        /// </summary>
        /// <param name="cusl"></param>
        /// <returns></returns>
        public static int Edit(CustomerLevel cusl) {
            PSSEntities db = new PSSEntities();
            db.Entry<CustomerLevel>(cusl).State = System.Data.Entity.EntityState.Modified;
            return db.SaveChanges();
        }
        /// <summary>
        /// 删除客户等级
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static int Delete(int id) {
            PSSEntities db = new PSSEntities();
            db.CustomerLevel.Remove(db.CustomerLevel.FirstOrDefault(c=>c.CLID==id));
            return db.SaveChanges();

        }
    }
}