using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using UI.Models.DAL;
using UI.Models.Model;

namespace UI.Models.BLL
{
    public class SaleReturnDetailBLL
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
            return SaleReturnDetailDAL.GetAllPageBySDID(PageIndex, PageSize, id);
        }
    }
}