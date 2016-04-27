using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using UI.Models.Model;
using UI.Models.DAL;

namespace UI.Models.BLL
{
    public class CustomerOrderDetailBLL
    {
        /// <summary>
        /// 根据客户订单ID查询客户订单详单信息
        /// </summary>
        /// <param name="v1"></param>
        /// <param name="v2"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        internal static List<CustomerOrderDetail> GetAllPageBySDID(int PageIndex, int PageSize, string id)
        {
            return CustomerOrderDetailDAL.GetAllPageBySDID(PageIndex,PageSize,id);
        }

    }
}