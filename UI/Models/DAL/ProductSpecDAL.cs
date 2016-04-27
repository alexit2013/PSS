using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using UI.Models.Model;

namespace UI.Models.DAL
{
    public class ProductSpecDAL
    {
       
        /// <summary>
        /// 查询所有
        /// </summary>
        /// <returns></returns>
        public static List<ProductSpec> GetAll()
        {
            PSSEntities db = new PSSEntities();
            return db.ProductSpec.Select(p => p).ToList<ProductSpec>();
        }
        /// <summary>
        /// 分页查询
        /// </summary>
        /// <param name="PageIndex"></param>
        /// <param name="PageSize"></param>
        /// <returns></returns>
        public static List<ProductSpec> GetAllPage(int PageIndex, int PageSize)
        {
            PSSEntities db = new PSSEntities();
            return db.ProductSpec.OrderBy(p => p.PSID).Skip((PageIndex - 1) * PageSize).Take(PageSize).ToList<ProductSpec>();
        }
        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="pro"></param>
        /// <returns></returns>
        public static int Add(ProductSpec pro)
        {
            PSSEntities db = new PSSEntities();
            db.ProductSpec.Add(pro);
            return db.SaveChanges();
        }
        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="pro"></param>
        /// <returns></returns>
        public static int Edit(ProductSpec pro)
        {
            PSSEntities db = new PSSEntities();
            db.Entry<ProductSpec>(pro).State = System.Data.Entity.EntityState.Modified;
            return db.SaveChanges();
        }
        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static int Delete(int id)
        {
            PSSEntities db = new PSSEntities();
            db.ProductSpec.Remove(db.ProductSpec.FirstOrDefault(p => p.PSID == id));
            return db.SaveChanges();
        }
    }
}