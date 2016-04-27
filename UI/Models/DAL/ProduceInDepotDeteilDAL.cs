using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using UI.Models.Model;

namespace UI.Models.DAL
{
    public class ProduceInDepotDeteilDAL
    {
        /// <summary>
        /// 查询所有
        /// </summary>
        /// <returns></returns>
        public static List<ProduceInDepotDeteil> GetAll()
        {
            PSSEntities db = new PSSEntities();
            return db.ProduceInDepotDeteil.Select(d => d).ToList();
        }
        /// <summary>
        /// 分页查询
        /// </summary>
        /// <param name="PageIndex"></param>
        /// <param name="PageSize"></param>
        /// <returns></returns>
        public static List<ProduceInDepotDeteil> GetAllPage(int PageIndex, int PageSize)
        {
            PSSEntities db = new PSSEntities();
            return db.ProduceInDepotDeteil.Select(d => d).OrderBy(d => d.PIDID).Skip((PageIndex - 1) * PageSize).Take(PageSize).ToList();
        }
        /// <summary>
        /// 根据入库订单查询详单信息
        /// </summary>
        /// <param name="PageIndex"></param>
        /// <param name="PageSize"></param>
        /// <param name="SDID"></param>
        /// <returns></returns>
        public static List<ProduceInDepotDeteil> GetAllPageBySDID(int PageIndex, int PageSize, string SDID)
        {
            PSSEntities db = new PSSEntities();
            return db.ProduceInDepotDeteil.Where(d => d.PIDID.Equals(SDID)).OrderBy(d => d.PIDDID).Skip((PageIndex - 1) * PageSize).Take(PageSize).ToList();
        }
        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="dep"></param>
        /// <returns></returns>
        public static int AddStockDetail(ProduceInDepotDeteil dep)
        {
            PSSEntities db = new PSSEntities();
            db.ProduceInDepotDeteil.Add(dep);
            return db.SaveChanges();
        }
      
    }
}