using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using UI.Models.Model;

namespace UI.Models.DAL
{
    public class SaleDepotDetailDAL
    {
        /// <summary>
        /// 根据客户订单ID查询详单信息
        /// </summary>
        /// <param name="PageIndex"></param>
        /// <param name="PageSize"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        internal static List<SaleDepotDetail> GetAllPageBySDID(int PageIndex, int PageSize, string id)
        {
            PSSEntities db = new PSSEntities();
            List<SaleDepotDetail> list = db.SaleDepotDetail.Where(p => p.SDID.Equals(id)).OrderBy(p => p.SDDID).Skip((PageIndex - 1) * PageSize).Take(PageSize).ToList();
            return list;
        }
    }
}