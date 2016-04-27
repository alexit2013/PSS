using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using UI.Models.Model;

namespace UI.Models.DAL
{
    public class StockReturnDetailDAL
    {
        /// <summary>
        ///根据采购退货单ID查询采购退货详单信息
        /// </summary>
        /// <param name="pageIndex">当前页</param>
        /// <param name="pageSize">页大小</param>
        /// <param name="id">采购退货单ID</param>
        /// <returns></returns>
        internal static List<StockReturnDetail> GetAllPageBySDID(int pageIndex, int pageSize, string id)
        {
            PSSEntities db = new PSSEntities();
            return db.StockReturnDetail.Where(s=>s.KRID.Equals(id)).OrderBy(s=>s.KRDID).Skip((pageIndex-1)*pageSize).Take(pageSize).ToList();
        }
    }
}