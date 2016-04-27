using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using UI.Models.Model;

namespace UI.Models.DAL
{
    public class CustomerOrderDetailDAL
    {
        /// <summary>
        /// 根据客户订单ID查询详单信息
        /// </summary>
        /// <param name="PageIndex"></param>
        /// <param name="PageSize"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        internal static List<CustomerOrderDetail> GetAllPageBySDID(int PageIndex, int PageSize, string id)
        {
            PSSEntities db = new PSSEntities();
            List<CustomerOrderDetail> list = db.CustomerOrderDetail.Where(p=>p.COID.Equals(id)).OrderBy(p=>p.CODID).Skip((PageIndex-1)*PageSize).Take(PageSize).ToList();
            return list;
        }
    }
}