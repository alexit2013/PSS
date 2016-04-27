using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using UI.Models.Model;
using UI.Models.DAL;

namespace UI.Models.BLL
{
    public class StockReturnDetailBLL
    {
        /// <summary>
        ///根据采购退货单ID查询采购退货详单信息
        /// </summary>
        /// <param name="pageIndex">当前页</param>
        /// <param name="pageSize">页大小</param>
        /// <param name="id">采购退货单ID</param>
        /// <returns></returns>
        public static List<StockReturnDetail> GetAllPageBySDID(int PageIndex,int PageSize,string id) {
            return StockReturnDetailDAL.GetAllPageBySDID(PageIndex, PageSize, id);
        }
    }
}