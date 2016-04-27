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
    public class SaleDepotController : Controller
    {
        /// <summary>
        /// 获取客户订单编号
        /// </summary>
        /// <returns></returns>
        public ActionResult GetSaleDepotID()
        {
            PSSEntities db = new PSSEntities();
            ObjectParameter para = new ObjectParameter("DD", "");
            db.pro_order("SaleDepot", "SDID", "XC", para);
            return Content(para.Value.ToString());
        }
        /// <summary>
        /// 添加客户订单
        /// </summary>
        /// <param name="qp"></param>
        /// <param name="list"></param>
        /// <returns></returns>
        public ActionResult AddSaleDepot(SaleDepot qp, List<SaleDepotDetail> list)
        {
            qp.UserID = Convert.ToInt32(Session["uid"]);
            if (SaleDepotBLL.AddStocks(qp, list) > 0)
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
        public ActionResult Find(string SDID, string UsersName, string DepotID, string CusID, string SDDate, int COState, int PageIndex)
        {
            int count = 0;
            List<SaleDepot> list = SaleDepotBLL.Find(SDID, CusID,DepotID, SDDate, UsersName, COState, PageIndex, 10, out count);
            count = count % 10 == 0 ? count / 10 : count / 10 + 1;
            List<object> listed = new List<object>();
            if (list.Count > 0)
            {
                foreach (SaleDepot item in list)
                {
                    listed.Add(new { COID=item.COID, SDID = item.SDID, CusID = item.CusID, CusName = item.Customers.CusName, DepotName=item.Depots.DepotName, UserID = item.UserID, UsersName = item.Users.UsersName, SDDate = item.SDDate, SDState = item.SDState, SDDesc = item.SDDesc, MaxPageIndex = count, CLAgio = item.Customers.CustomerLevel.CLAgio });
                }
            }
            else
            {
                listed.Add(new { SDID = "", MaxPageIndex = 0 });
            }
            return Json(listed); ;
        }
        /// <summary>
        /// 根据客户订单ID查询详单信息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult GetSaleDepotDetailByQPID(string id)
        {
            List<SaleDepotDetail> list = SaleDepotDetailBLL.GetAllPageBySDID(1, 100, id);
            List<object> listed = new List<object>();
            foreach (SaleDepotDetail item in list)
            {
                listed.Add(new { SDDID = item.SDDID, SDID = item.SDID, ProID = item.ProID, ProName = item.Products.ProName, SDDAmount = item.SDDAmount, SDDPrice = item.SDDPrice, SDDDisPrice = item.SDDDisPrice, SDDDiscount = item.SDDDiscount, SDDDesc = item.SDDDesc, PUName = item.Products.ProductUnit.PUName, PCName = item.Products.ProductColor.PCName, PSName = item.Products.ProductSpec.PSName });
            }
            return Json(listed);
        }
        /// <summary>
        /// 删除客户订单
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult DelSaleDepot(string id)
        {
            if (SaleDepotBLL.DelStocks(id) > 0)
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
        public ActionResult EditCustomerOrder(SaleDepot qp, List<SaleDepotDetail> list)
        {
            if (SaleDepotBLL.EditStocks(qp, list) > 0)
                return Content("edit_yes");
            else
                return Content("edit_no");
        }
        /// <summary>
        /// 审核客户订单
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult CKSaleDepot(string id)
        {
            int userid = Convert.ToInt32(Session["uid"]);
            try
            {
                if (SaleDepotBLL.CKInDepot(id, userid) > 0)
                    return Content("ck_yes");
                else
                    return Content("ck_no");
            }
            catch (Exception ex)
            {
                return Content(ex.Message);
            }
            
        }
        /// <summary>
        /// 根据ID查询客户订单信息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult GetSaleDepotByID(string id)
        {
            SaleDepot item = SaleDepotBLL.GetSaleDepotByID(id);
            return Json(new { SDID = item.SDID, CusID = item.CusID, CusName = item.Customers.CusName, UserID = item.UserID, UsersName = item.Users.UsersName, SDDate = item.SDDate, SDState = item.SDState, SDDesc = item.SDDesc, CLAgio = item.Customers.CustomerLevel.CLAgio });
        }
    }
}