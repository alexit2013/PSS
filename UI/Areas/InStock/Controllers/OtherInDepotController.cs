using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using UI.Models.Model;
using UI.Models.BLL;
using System.Data.Entity.Core.Objects;

namespace UI.Areas.InStock.Controllers
{
    /// <summary>
    /// 生产入库
    /// </summary>
    public class OtherInDepotController : Controller
    {
        /// <summary>
        /// 获取生产入库编号
        /// </summary>
        /// <returns></returns>
        public ActionResult GetOtherID()
        {
            PSSEntities db = new PSSEntities();
            ObjectParameter para = new ObjectParameter("DD", "");
            db.pro_order("OtherInDepot", "OIDID", "RK", para);
            return Content(para.Value.ToString());
        }
        /// <summary>
        /// 添加生产入库单
        /// </summary>
        /// <param name="pd"></param>
        /// <param name="list"></param>
        /// <returns></returns>
        public ActionResult AddOtherDepot(OtherInDepot pd, List<OtherInDepotDetail> list)
        {
            pd.OIDUser = Convert.ToInt32(Session["uid"]);
            if (OtherInDepotBLL.AddStocks(pd, list) > 0)
                return Content("add_yes");
            else
                return Content("add_no");
        }
        /// <summary>
        /// 生产入库订单条件查询
        /// </summary>
        /// <param name="PIDID"></param>
        /// <param name="UsersName"></param>
        /// <param name="DepotID"></param>
        /// <param name="PIDDate"></param>
        /// <param name="PIDState"></param>
        /// <param name="PageIndex"></param>
        /// <returns></returns>
        public ActionResult Find(string OIDID, string UsersName, string DepotID, string OIDDate, int OIDState, int PageIndex)
        {
            int count = 0;
            List<OtherInDepot> list = OtherInDepotBLL.Find(OIDID, DepotID, OIDDate, UsersName, OIDState, PageIndex, 10, out count);
            List<object> listed = new List<object>();
            if (list.Count != 0)
            {
                count = count % 10 == 0 ? count / 10 : count / 10 + 1;
                foreach (OtherInDepot item in list)
                {
                    listed.Add(new { OIDID = item.OIDID, DepotID = item.DepotID, DepotName = item.Depots.DepotName, OIDDate = item.OIDDate, OIDUser = item.OIDUser, UsersName = item.Users.UsersName, OIDState = item.OIDState, OIDDesc = item.OIDDesc, MaxPageIndex = count });
                }
            }
            else
            {
                listed.Add(new { OIDID = "", MaxPageIndex = 0 });
            }
            return Json(listed);
        }
        /// <summary>
        /// 根据
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult GetOtherDepotDeteilByID(string id)
        {
            List<OtherInDepotDetail> list = OtherInDepotDetailBLL.GetAllPageBySDID(1, 100, id);
            List<object> listed = new List<object>();
            foreach (OtherInDepotDetail item in list)
            {
                listed.Add(new { OIDDID = item.OIDDID, ProID = item.ProID, ProName = item.Products.ProName, OIDID = item.OIDID, OIDDAmount = item.OIDDAmount, OIDDPrice = item.OIDDPrice, OIDDDesc = item.OIDDDesc, PUName = item.Products.ProductUnit.PUName, PCName = item.Products.ProductColor.PCName, PSName = item.Products.ProductSpec.PSName });
            }
           return Json(listed);
        }
        /// <summary>
        /// 删除生产入库单
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult DelOtherDepot(string id)
        {
            if (OtherInDepotBLL.DelStocks(id) > 0)
                return Content("del_yes");
            else
                return Content("del_no");
        }
        /// <summary>
        /// 修改生产入库单
        /// </summary>
        /// <param name="pd"></param>
        /// <param name="list"></param>
        /// <returns></returns>
        public ActionResult EditOtherInDepot(OtherInDepot pd, List<OtherInDepotDetail> list)
        {
            if (OtherInDepotBLL.EdiStocks(pd, list) > 0)
                return Content("edit_yes");
            else
                return Content("edit_no");
        }
        /// <summary>
        /// 审核生产入库单
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult CKOtherDepot(string id)
        {
            int userid = Convert.ToInt32(Session["uid"]);
            if (OtherInDepotBLL.CKInDepot(id, userid) > 0)
                return Content("ck_yes");
            else
                return Content("ck_no");
        }
    }
}