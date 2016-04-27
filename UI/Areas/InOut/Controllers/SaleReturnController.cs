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
    public class SaleReturnController : Controller
    {
        /// <summary>
      /// 获取销售退货单ID
      /// </summary>
      /// <returns></returns>
        public ActionResult GetSaleReturnID()
        {
            PSSEntities db = new PSSEntities();
            ObjectParameter para = new ObjectParameter("DD", "");
            db.pro_order("SaleReturn", "SRID", "XT", para);
            return Content(para.Value.ToString());
        }
        /// <summary>
        /// 添加销售退货单
        /// </summary>
        /// <param name="qp"></param>
        /// <param name="list"></param>
        /// <returns></returns>
        public ActionResult AddSaleReturn(SaleReturn qp, List<SaleReturnDetail> list)
        {
            qp.UserID = Convert.ToInt32(Session["uid"]);
            if (SaleReturnBLL.AddStocks(qp, list) > 0)
                return Content("add_yes");
            else
                return Content("add_no");
        }
        /// <summary>
        /// 条件查询销售退货单
        /// </summary>
        /// <param name="SRID"></param>
        /// <param name="UsersName"></param>
        /// <param name="SDID"></param>
        /// <param name="CusID"></param>
        /// <param name="DepotID"></param>
        /// <param name="SRDate"></param>
        /// <param name="SRState"></param>
        /// <param name="PageIndex"></param>
        /// <returns></returns>
        public ActionResult Find(string SRID, string UsersName, string SDID, string CusID, string DepotID, string SRDate, int SRState, int PageIndex)
        {
            int count = 0;
            List<SaleReturn> list = SaleReturnBLL.Find(SRID,CusID,SDID,DepotID,SRDate,UsersName,SRState, PageIndex, 10, out count);
            count = count % 10 == 0 ? count / 10 : count / 10 + 1;
            if (count > 0)
            {
                var listed = from s in list
                             select new
                             {
                                s.SRID,
                                s.SDID,
                                s.CusID,
                                s.Customers.CusName,
                                s.DepotID,
                                s.Depots.DepotName,
                                s.SRDate,
                                s.UserID,
                                s.Users.UsersName,
                                s.SRState,
                                s.SRDesc,
                                MaxPageIndex = count
                             };
                return Json(listed);
            }
            else
            {
                List<object> listed = new List<object>();
                listed.Add(new { SRID = "", MaxPageIndex = 0 });
                return Json(listed);
            }

        }
        /// <summary>
        /// 根据销售退货单ID查询销售退货详单信息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult GetSaleReturnDetailByQPID(string id)
        {
            List<SaleReturnDetail> list = SaleReturnDetailBLL.GetAllPageBySDID(1, 100, id);
            var listed = from s in list
                         select new
                         {
                             s.ProID,
                             s.SRDID,
                             s.SRID,
                             s.SRDAmount,
                             s.SRDPrice,
                             s.SRDDesc,
                             s.Products.ProName,
                             s.Products.ProductTypes.PTName,
                             s.Products.ProductUnit.PUName,
                             s.Products.ProductSpec.PSName,
                             s.Products.ProductColor.PCName
                         };
            return Json(listed);
        }
        /// <summary>
        /// 删除销售退货单
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult DelSaleReturn(string id)
        {
            if (SaleReturnBLL.DelStocks(id) > 0)
                return Content("del_yes");
            else
                return Content("del_no");
        }
        /// <summary>
        /// 修改销售退货单
        /// </summary>
        /// <param name="qp"></param>
        /// <param name="list"></param>
        /// <returns></returns>
        public ActionResult EditSaleReturn(SaleReturn qp, List<SaleReturnDetail> list)
        {
            if (SaleReturnBLL.EditStocks(qp, list) > 0)
                return Content("edit_yes");
            else
                return Content("edit_no");
        }
        /// <summary>
        /// 审核销售退货单
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult CKSaleReturn(string id)
        {
            try
            {
                int userid = Convert.ToInt32(Session["uid"]);

                if (SaleReturnBLL.CKInDepot(id, userid) > 0)
                    return Content("ck_yes");
                else
                    return Content("ck_no");
            }
            catch (Exception ex)
            {
                return Content(ex.Message);
            }

        }
    }
}