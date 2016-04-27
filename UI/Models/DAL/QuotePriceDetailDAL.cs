using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using UI.Models.Model;

namespace UI.Models.DAL
{
    public class QuotePriceDetailDAL
    {
        /// <summary>
        /// 根据报价单信息查询详单信息
        /// </summary>
        /// <param name="PageIndex"></param>
        /// <param name="PageSize"></param>
        /// <param name="SDID"></param>
        /// <returns></returns>
        public static List<QuotePriceDetail> GetAllPageBySDID(int PageIndex, int PageSize, string SDID)
        {
            PSSEntities db = new PSSEntities();
            return db.QuotePriceDetail.Where(d => d.QPID.Equals(SDID)).OrderBy(d => d.QPDID).Skip((PageIndex - 1) * PageSize).Take(PageSize).ToList();
        }
    }
}