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
    public class ProduceOutDepotController : Controller
    {
        /// <summary>
        /// 获取报价单编号
        /// </summary>
        /// <returns></returns>
        public ActionResult GetProduceOutDepotID()
        {
            PSSEntities db = new PSSEntities();
            ObjectParameter para = new ObjectParameter("DD", "");
            db.pro_order("ProduceOutDepot", "PODID", "SL", para);
            return Content(para.Value.ToString());
        }
        /// <summary>
        /// 添加报价单
        /// </summary>
        /// <param name="qp"></param>
        /// <param name="list"></param>
        /// <returns></returns>
        public ActionResult AddProduceOutDepot(ProduceOutDepot qp, List<ProduceOutDepotDetail> list)
        {
            qp.UserID = Convert.ToInt32(Session["uid"]);
            if (ProduceOutDepotBLL.AddStocks(qp, list) > 0)
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
        public ActionResult Find(string PODID, string UsersName, string DepotID, string PODDate, int PODState, int PageIndex)
        {
            int count = 0;
            List<ProduceOutDepot> list = ProduceOutDepotBLL.Find(PODID, DepotID, PODDate, UsersName, PODState, PageIndex, 10, out count);
            count = count % 10 == 0 ? count / 10 : count / 10 + 1;
            List<object> listed = new List<object>();
            if (list.Count > 0)
            {
                foreach (ProduceOutDepot item in list)
                {
                    listed.Add(new { PODID = item.PODID, DepotID = item.DepotID, DepotName = item.Depots.DepotName, UserID = item.UserID, UsersName = item.Users.UsersName, PODDate = item.PODDate, PODState = item.PODState, PODDesc = item.PODDesc, MaxPageIndex = count });
                }
            }
            else
            {
                listed.Add(new { PODID = "", MaxPageIndex = 0 });
            }
            return Json(listed);
        }
        /// <summary>
        /// 根据销售出库单ID查询详单信息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult GetProduceOutDepotDetailByQPID(string id)
        {
            List<ProduceOutDepotDetail> list = ProduceOutDepotDetailBLL.GetAllPageBySDID(1, 100, id);
            List<object> listed = new List<object>();
            foreach (ProduceOutDepotDetail item in list)
            {
                listed.Add(new { PODDID = item.PODDID, PODID = item.PODID, ProID = item.ProID, ProName = item.Products.ProName, PODDAmount = item.PODDAmount, PODDPrice = item.PODDPrice, PODDDesc = item.PODDDesc, PUName = item.Products.ProductUnit.PUName, PCName = item.Products.ProductColor.PCName, PSName = item.Products.ProductSpec.PSName });
            }
            return Json(listed);
        }
        /// <summary>
        /// 删除销售出库单
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult DelProduceOutDepot(string id)
        {
            if (ProduceOutDepotBLL.DelStocks(id) > 0)
                return Content("del_yes");
            else
                return Content("del_no");
        }
        /// <summary>
        /// 修改生产领料单
        /// </summary>
        /// <param name="qp"></param>
        /// <param name="list"></param>
        /// <returns></returns>
        public ActionResult EditProduceOutDepot(ProduceOutDepot qp, List<ProduceOutDepotDetail> list)
        {
            if (ProduceOutDepotBLL.EdiStocks(qp, list) > 0)
                return Content("edit_yes");
            else
                return Content("edit_no");
        }
        /// <summary>
        /// 审核
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult CKProduceOutDepot(string id)
        {
            int userid = Convert.ToInt32(Session["uid"]);
            if (ProduceOutDepotBLL.CKInDepot(id, userid) > 0)
                return Content("ck_yes");
            else
                return Content("ck_no");
        }
    }
}