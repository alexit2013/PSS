using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using UI.Models.Model;

namespace UI.Models.DAL
{
    public class ProduceOutDepotDetailDAL
    {
        /// <summary>
        /// 根据生产领单ID查询详单信息
        /// </summary>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        internal static List<ProduceOutDepotDetail> GetAllPageBySDID(int pageIndex, int pageSize, string id)
        {
            PSSEntities db = new PSSEntities();
            return db.ProduceOutDepotDetail.Where(s=>s.PODID.Equals(id)).OrderBy(p=>p.PODDID).Skip((pageIndex-1)*pageSize).Take(pageSize).ToList();
        }
    }
}