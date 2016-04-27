using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Objects;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using UI.Models.BLL;
using UI.Models.Model;

namespace UI.Areas.InOut.Controllers
{
    public class CustomerOrderController : Controller
    {
        /// <summary>
        /// 获取客户订单编号
        /// </summary>
        /// <returns></returns>
        public ActionResult GetCustomerOrderID()
        {
            PSSEntities db = new PSSEntities();
            ObjectParameter para = new ObjectParameter("DD", "");
            db.pro_order("CustomerOrder", "COID", "CD", para);
            return Content(para.Value.ToString());
        }
        /// <summary>
        /// 添加客户订单
        /// </summary>
        /// <param name="qp"></param>
        /// <param name="list"></param>
        /// <returns></returns>
        public ActionResult AddCustomerOrder(CustomerOrder qp, List<CustomerOrderDetail> list)
        {
            qp.UserID = Convert.ToInt32(Session["uid"]);
            if (CustomerOrderBLL.AddStocks(qp, list) > 0)
                return Content("add_yes");
            else
                return Content("add_no");
        }
        /// <summary>
        /// 条件查询
        /// </summary>
        /// <param name="QPID">报价单编号</param>
        /// <param name="CusID">客户编号</param>
        /// <param name="QPDate">创建时间</param>
        /// <param name="UsersName">创建人</param>
        /// <param name="QPState">状态</param>
        /// <param name="PageIndex">页数</param>
        /// <returns></returns>
        public ActionResult Find(string COID, string UsersName, string CusID, string CODate, string CORefDate,int COState, int PageIndex)
        {
            int count = 0;
            List<CustomerOrder> list = CustomerOrderBLL.Find(COID, CusID, CODate, CORefDate, UsersName, COState, PageIndex, 10, out count);
            count = count % 10 == 0 ? count / 10 : count / 10 + 1;
            List<object> listed = new List<object>();
            if (list.Count > 0)
            {
                foreach (CustomerOrder item in list)
                {
                    listed.Add(new { COID = item.COID, CusID = item.CusID, CusName = item.Customers.CusName, UserID = item.UserID, UsersName = item.Users.UsersName, CODate = item.CODate, CORefDate = item.CORefDate, COState = item.COState, CODesc = item.CODesc, MaxPageIndex = count , CLAgio = item.Customers.CustomerLevel.CLAgio });
                }
            }
            else
            {
                listed.Add(new { COID = "", MaxPageIndex = 0 });
            }
            return Json(listed); ;
        }
        /// <summary>
        /// 根据客户订单ID查询详单信息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult GetCustomerOrderDetailByQPID(string id)
        {
            List<CustomerOrderDetail> list = CustomerOrderDetailBLL.GetAllPageBySDID(1, 100, id);
            List<object> listed = new List<object>();
            foreach (CustomerOrderDetail item in list)
            {
                listed.Add(new { CODID = item.CODID, COID = item.COID, ProID = item.ProID, ProName = item.Products.ProName, CODAmount = item.CODAmount, CODSale=item.CODSale, CODPrice = item.CODPrice, CODDisPrice = item.CODDisPrice, CODDiscont = item.CODDiscont, CODDesc = item.CODDesc, PUName = item.Products.ProductUnit.PUName, PCName = item.Products.ProductColor.PCName, PSName = item.Products.ProductSpec.PSName });
            }
            return Json(listed);
        }
        /// <summary>
        /// 删除客户订单
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult DelCustomerOrder(string id)
        {
            if (CustomerOrderBLL.DelStocks(id) > 0)
                return Content("del_yes");
            else
                return Content("del_no");
        }
        /// <summary>
        /// 修改客户订单
        /// </summary>
        /// <param name="qp"></param>
        /// <param name="list"></param>
        /// <returns></returns>
        public ActionResult EditCustomerOrder(CustomerOrder qp, List<CustomerOrderDetail> list)
        {
            if (CustomerOrderBLL.EdiStocks(qp, list) > 0)
                return Content("edit_yes");
            else
                return Content("edit_no");
        }
        /// <summary>
        /// 审核客户订单
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult CKCustomerOrder(string id)
        {
            int userid = Convert.ToInt32(Session["uid"]);
            if (CustomerOrderBLL.CKInDepot(id, userid) > 0)
                return Content("ck_yes");
            else
                return Content("ck_no");
        }
        /// <summary>
        /// 根据ID查询客户订单信息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult GetCustomerOrderByID(string id) {
             CustomerOrder item=   CustomerOrderBLL.GetCustomerOrderByID(id);
              return Json( new { COID = item.COID, CusID = item.CusID, CusName = item.Customers.CusName, UserID = item.UserID, UsersName = item.Users.UsersName, CODate = item.CODate, CORefDate = item.CORefDate, COState = item.COState, CODesc = item.CODesc });
        }
    }
}