using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using UI.Models.Model;

namespace UI.Models.DAL
{
    public class OtherInDepotDetailDAL
    {
        /// <summary>
        /// 根据入库订单查询详单信息
        /// </summary>
        /// <param name="PageIndex"></param>
        /// <param name="PageSize"></param>
        /// <param name="SDID"></param>
        /// <returns></returns>
        public static List<OtherInDepotDetail> GetAllPageBySDID(int PageIndex, int PageSize, string SDID)
        {
            PSSEntities db = new PSSEntities();
            return db.OtherInDepotDetail.Where(d => d.OIDID.Equals(SDID)).OrderBy(d => d.OIDDID).Skip((PageIndex - 1) * PageSize).Take(PageSize).ToList();
        }
    }
}