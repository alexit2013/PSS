using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using UI.Models.Model;

namespace UI.Models.DAL
{
    public class ProductUnitDAL
    {
       
        /// <summary>
        /// 查询所有
        /// </summary>
        /// <returns></returns>
        public static List<ProductUnit> GetAll()
        {
            PSSEntities db = new PSSEntities();
            return db.ProductUnit.Select(p => p).ToList<ProductUnit>();
        }
        /// <summary>
        /// 分页查询
        /// </summary>
        /// <param name="PageIndex"></param>
        /// <param name="PageSize"></param>
        /// <returns></returns>
        public static List<ProductUnit> GetAllPage(int PageIndex, int PageSize)
        {
            PSSEntities db = new PSSEntities();
            return db.ProductUnit.OrderBy(p => p.PUID).Skip((PageIndex - 1) * PageSize).Take(PageSize).ToList<ProductUnit>();
        }
        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="pro"></param>
        /// <returns></returns>
        public static int Add(ProductUnit pro)
        {
            PSSEntities db = new PSSEntities();
            db.ProductUnit.Add(pro);
            return db.SaveChanges();
        }
        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="pro"></param>
        /// <returns></returns>
        public static int Edit(ProductUnit pro)
        {
            PSSEntities db = new PSSEntities();
            db.Entry<ProductUnit>(pro).State = System.Data.Entity.EntityState.Modified;
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
            db.ProductUnit.Remove(db.ProductUnit.FirstOrDefault(p => p.PUID == id));
            return db.SaveChanges();
        }
    }
}