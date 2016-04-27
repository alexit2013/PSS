using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using UI.Models.Model;

namespace UI.Models.DAL
{
    public class SaleReturnDetailDAL
    {
        /// <summary>
        /// 根据销售退货单ID查询详单信息
        /// </summary>
        /// <param name="PageIndex"></param>
        /// <param name="PageSize"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        internal static List<SaleReturnDetail> GetAllPageBySDID(int PageIndex, int PageSize, string id)
        {
            PSSEntities db = new PSSEntities();
            return db.SaleReturnDetail.Where(s => s.SRID.Equals(id)).OrderBy(s => s.SRDID).Skip((PageIndex - 1) * PageSize).Take(PageSize).ToList();
        }
    }
}