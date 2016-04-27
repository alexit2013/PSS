using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using UI.Models.Model;

namespace UI.Models.DAL
{
    public class StockDetailDAL
    {
        /// <summary>
        /// 查询所有
        /// </summary>
        /// <returns></returns>
        public static List<StockDetail> GetAll()
        {
            PSSEntities db = new PSSEntities();
            return db.StockDetail.Select(d => d).ToList<StockDetail>();
        }

    

        /// <summary>
        /// 分页查询
        /// </summary>
        /// <param name="PageIndex"></param>
        /// <param name="PageSize"></param>
        /// <returns></returns>
        public static List<StockDetail> GetAllPage(int PageIndex, int PageSize)
        {
            PSSEntities db = new PSSEntities();
            return db.StockDetail.Select(d => d).OrderBy(d => d.SDetailID).Skip((PageIndex - 1) * PageSize).Take(PageSize).ToList<StockDetail>();
        }

        public static List<StockDetail> GetAllPageBySDID(int PageIndex, int PageSize,string SDID)
        {
            PSSEntities db = new PSSEntities();
            return db.StockDetail.Where(d => d.StockID.Equals(SDID)).OrderBy(d => d.SDetailID).Skip((PageIndex - 1) * PageSize).Take(PageSize).ToList<StockDetail>();
        }
        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="dep"></param>
        /// <returns></returns>
        public static int AddStockDetail(StockDetail dep)
        {
            PSSEntities db = new PSSEntities();
            db.StockDetail.Add(dep);
            return db.SaveChanges();
        }
        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static int DelStockDetail(int id)
        {
            PSSEntities db = new PSSEntities();
            db.StockDetail.Remove(db.StockDetail.FirstOrDefault(d => d.SDetailID==id));
            return db.SaveChanges();
        }
        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="dp"></param>
        /// <returns></returns>
        public static int EdiStockDetail(StockDetail dp)
        {
            PSSEntities db = new PSSEntities();
            db.Entry<StockDetail>(dp).State = System.Data.Entity.EntityState.Modified;
            return db.SaveChanges();
        }
    }
}