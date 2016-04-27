using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using UI.Models.Model;
using UI.Models.DAL;

namespace UI.Models.BLL
{
    public class CustomerOrderBLL
    {
        /// <summary>
        /// 添加客户订单
        /// </summary>
        /// <param name="qp"></param>
        /// <param name="list"></param>
        /// <returns></returns>
        internal static int AddStocks(CustomerOrder qp, List<CustomerOrderDetail> list)
        {
            return CustomerOrderDAL.AddStocks(qp, list);
        }
        /// <summary>
        /// 条件分页查询客户订单
        /// </summary>
        /// <param name="cOID"></param>
        /// <param name="cusID"></param>
        /// <param name="cODate"></param>
        /// <param name="cORefDate"></param>
        /// <param name="usersName"></param>
        /// <param name="cOState"></param>
        /// <param name="pageIndex"></param>
        /// <param name="v"></param>
        /// <param name="count"></param>
        /// <returns></returns>
        internal static List<CustomerOrder> Find(string cOID, string cusID, string cODate, string cORefDate, string usersName, int cOState, int pageIndex, int v, out int count)
        {
            return CustomerOrderDAL.Find(cOID, cusID, cODate, cORefDate, usersName, cOState, pageIndex, v, out count);
        }

        internal static List<CustomerOrder> GetAll()
        {
            return CustomerOrderDAL.GetAll();
        }

        /// <summary>
        /// 修改客户订单
        /// </summary>
        /// <param name="qp"></param>
        /// <param name="list"></param>
        /// <returns></returns>
        internal static int EdiStocks(CustomerOrder qp, List<CustomerOrderDetail> list)
        {
            return CustomerOrderDAL.EditStocks(qp, list);
        }
        /// <summary>
        /// 审核客户订单
        /// </summary>
        /// <param name="id"></param>
        /// <param name="userid"></param>
        /// <returns></returns>
        internal static int CKInDepot(string id, int userid)
        {
            return CustomerOrderDAL.CKInDepot(id, userid);
        }
        /// <summary>
        /// 删除客户订单
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        internal static int DelStocks(string id)
        {
            return CustomerOrderDAL.DelStocks(id);
        }

        internal static CustomerOrder GetCustomerOrderByID(string id)
        {
          return  CustomerOrderDAL.GetCustomerOrderByID(id);
        }
    }
}