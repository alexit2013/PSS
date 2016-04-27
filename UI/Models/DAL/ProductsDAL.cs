using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using UI.Models.Model;

namespace UI.Models.DAL
{
    public class ProductsDAL
    {
    
        /// <summary>
        /// 查询所有
        /// </summary>
        /// <returns></returns>
        public static List<Products> GetAll() {
            PSSEntities db = new PSSEntities();
            return db.Products.Select(p => p).ToList<Products>();
        }
        /// <summary>
        /// 分页查询
        /// </summary>
        /// <param name="PageIndex"></param>
        /// <param name="PageSize"></param>
        /// <returns></returns>
        public static List<Products> GetAllPage(int PageIndex,int PageSize) {
            PSSEntities db = new PSSEntities();
            return db.Products.OrderBy(p => p.ProID).Skip((PageIndex - 1) * PageSize).Take(PageSize).ToList<Products>();
        }
        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="pro"></param>
        /// <returns></returns>
        public static int Add(Products pro) {
            PSSEntities db = new PSSEntities();
            db.Products.Add(pro);
            return db.SaveChanges();
        }
        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="pro"></param>
        /// <returns></returns>
        public static int Edit(Products pro)
        {
            PSSEntities db = new PSSEntities();
            db.Entry<Products>(pro).State = System.Data.Entity.EntityState.Modified;
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
            db.Products.Remove(db.Products.FirstOrDefault(p=>p.ProID==id));
            return db.SaveChanges();
        }
        /// <summary>
        /// 根据ID查询
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static Products GetByID(int id) {
            PSSEntities db = new PSSEntities();
            return db.Products.FirstOrDefault(p=>p.ProID.Equals(id));
        }

        /// <summary>
        /// 查询商品类型下商品的总个数
        /// </summary>
        /// <param name="id"></param>
        /// <param name="PageIndex"></param>
        /// <param name="PageSize"></param>
        /// <returns></returns>
        public static int GetAllByPTIDCount(int id)
        {
            PSSEntities db = new PSSEntities();
            List<int?> ptids = new List<int?>();
            ptids.Add(id);
            GetPTID(id, ptids);
            return db.Products.Count(p => ptids.Contains(p.PTID));
        }
        /// <summary>
        /// 根据类别ID查询商品信息
        /// </summary>
        /// <returns></returns>
        public static List<Products> GetAllByPTID(int id,int PageIndex,int PageSize) {
            PSSEntities db = new PSSEntities();
            List<int?> ptids = new List<int?>();
            ptids.Add(id);
            GetPTID(id, ptids);
            List<Products> list = new List<Products>();
            list= db.Products.Where(p => ptids.Contains(p.PTID)).OrderBy(p=>p.ProID).Skip((PageIndex - 1) * PageSize).Take(PageSize).ToList<Products>();
            return list;
        }

        /// <summary>
        /// 根据类别ID查询所有的子类别ID【递归】
        /// </summary>
        /// <param name="id"></param>
        /// <param name="ptids"></param>
        private static void GetPTID( int id, List<int?> ptids) {
            PSSEntities db = new PSSEntities();
            //id=1
            List<ProductTypes> ptlist = db.ProductTypes.Where(pt => pt.PTParentID == id).ToList<ProductTypes>();
            foreach (ProductTypes item in ptlist)
            {
                ptids.Add(item.PTID);
                //递归
                GetPTID(item.PTID,ptids);
            }
        }
        /// <summary>
        /// 多条件查询商品
        /// </summary>
        /// <param name="ProName">商品名称</param>
        /// <param name="PTID">商品类别ID</param>
        /// <param name="PUID">商品规格ID</param>
        /// <param name="PCID">商品颜色ID</param>
        /// <param name="PSID">商品单位ID</param>
        /// <param name="count">总条数</param>
        /// <param name="PageIndex">当前页</param>
        /// <param name="PageSize">页大小</param>
        /// <returns></returns>
        public static List<Products> Find(string ProName,int PTID,int PUID,int PCID,int PSID,out int  count,int PageIndex,int PageSize) {
            PSSEntities db = new PSSEntities();
            var list = db.Products.Select(s=>s);
            if (ProName!=null&&ProName.Trim().Length>0)
            {
                list = list.Where(s=>s.ProName.Contains(ProName));
            }
            if (PTID!=-1&&PTID>0)
            {
                List<int?> ids = new List<int?>();
                ids.Add(PTID);
                GetPTID(PTID, ids);
                list = list.Where(s => ids.Contains(s.PTID));
            }
            if (PUID != -1 && PUID > 0)
            {
                list = list.Where(s=>s.PUID==PUID);
            }
            if (PCID != -1 && PCID > 0)
            {
                list = list.Where(s => s.PCID == PCID);
            }
            if (PSID != -1 && PSID > 0)
            {
                list = list.Where(s => s.PSID == PSID);
            }
            count = list.Count();
            return list.OrderBy(s=>s.ProID).Skip((PageIndex-1)*PageSize).Take(PageSize).ToList();
        }
    }
}