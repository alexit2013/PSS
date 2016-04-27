using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using UI.Models.Model;

namespace UI.Models.DAL
{
    public class ProductColorDAL
    {
        
        /// <summary>
        /// 查询所有
        /// </summary>
        /// <returns></returns>
        public static List<ProductColor> GetAll()
        {
            PSSEntities db = new PSSEntities();
            return db.ProductColor.Select(p => p).ToList<ProductColor>();
        }
        /// <summary>
        /// 分页查询
        /// </summary>
        /// <param name="PageIndex"></param>
        /// <param name="PageSize"></param>
        /// <returns></returns>
        public static List<ProductColor> GetAllPage(int PageIndex, int PageSize)
        {
            PSSEntities db = new PSSEntities();
            return db.ProductColor.OrderBy(p => p.PCID).Skip((PageIndex - 1) * PageSize).Take(PageSize).ToList<ProductColor>();
        }
        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="pro"></param>
        /// <returns></returns>
        public static int Add(ProductColor pro)
        {
            PSSEntities db = new PSSEntities();
            db.ProductColor.Add(pro);
            return db.SaveChanges();
        }
        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="pro"></param>
        /// <returns></returns>
        public static int Edit(ProductColor pro)
        {
            PSSEntities db = new PSSEntities();
            db.Entry<ProductColor>(pro).State = System.Data.Entity.EntityState.Modified;
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
            db.ProductColor.Remove(db.ProductColor.FirstOrDefault(p => p.PCID == id));
            return db.SaveChanges();
        }
    }
}