using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using UI.Models.Model;

namespace UI.Models.DAL
{
    public class ProductTypesDAL
    {
        
        /// <summary>
        /// 查询所有商品类别信息
        /// </summary>
        /// <returns></returns>
        public static List<ProductTypes> GetAll() {
            PSSEntities db = new PSSEntities();
            var linqlist=  db.ProductTypes.Select(p => p).ToList();
            return linqlist as List<ProductTypes>;
        }
        /// <summary>
        /// 分页查询
        /// </summary>
        /// <param name="index">页数</param>
        /// <returns></returns>
        public static List<ProductTypes> GetAll(int index)
        {
            PSSEntities db = new PSSEntities();
            var linqlist = db.ProductTypes.Select(p => p).OrderBy(p=>p.PTID).Skip(10*(index-1)).Take(10).ToList();
            return linqlist as List<ProductTypes>;
        }
       /// <summary>
       /// 添加类型
       /// </summary>
       /// <param name="pt">商品类别对象</param>
       /// <returns></returns>
        public static int AddProType(ProductTypes pt)
        {
            PSSEntities db = new PSSEntities();
            db.ProductTypes.Add(pt);
            return db.SaveChanges();
        }
        /// <summary>
        /// 删除商品类型
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static int DelProType(int id) {
            PSSEntities db = new PSSEntities();
            //db.ProductTypes.Remove(db.ProductTypes.FirstOrDefault(p => p.PTID == id));
            List<int?> ids = new List<int?>();
            GetPTID(id, ids);
            db.ProductTypes.RemoveRange(db.ProductTypes.Where(p=>ids.Contains(p.PTID)));
            
            return db.SaveChanges();
        }
        /// <summary>
        /// 根据类别ID查询所有的子类别ID【递归】
        /// </summary>
        /// <param name="id"></param>
        /// <param name="ptids"></param>
        private static void GetPTID(int id, List<int?> ptids)
        {
            PSSEntities db = new PSSEntities();
            List<ProductTypes> ptlist = db.ProductTypes.Where(pt => pt.PTParentID == id).ToList<ProductTypes>();
            foreach (ProductTypes item in ptlist)
            {
                ptids.Add(item.PTID);
                GetPTID(item.PTID, ptids);
            }
        }



        /// <summary>
        /// 修改商品类型
        /// </summary>
        /// <param name="pt"></param>
        /// <returns></returns>
        public static int EidtProType(ProductTypes pt) {
            PSSEntities db = new PSSEntities();
            ProductTypes p = db.ProductTypes.FirstOrDefault(s=>s.PTID==pt.PTID);
            p.PTName = pt.PTName;
            p.PTDes = pt.PTDes;
            return db.SaveChanges();
        }
       

     }
}