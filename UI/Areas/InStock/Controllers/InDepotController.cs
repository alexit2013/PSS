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
    /// 其它入库
    /// </summary>
    public class InDepotController : Controller
    {
        /// <summary>
        /// 获取生产入库编号
        /// </summary>
        /// <returns></returns>
        public ActionResult GetProductID() {
            PSSEntities db = new PSSEntities();
            ObjectParameter para = new ObjectParameter("DD","");
            db.pro_order("ProduceInDepot", "PIDID", "SR", para);
            return Content(para.Value.ToString());
        }
        /// <summary>
        /// 添加生产入库单
        /// </summary>
        /// <param name="pd"></param>
        /// <param name="list"></param>
        /// <returns></returns>
        public ActionResult AddProductDepot(ProduceInDepot pd,List<ProduceInDepotDeteil> list) {
            pd.PIDUser = Convert.ToInt32(Session["uid"]);
            if (ProduceInDepotBLL.AddStocks(pd, list) > 0)
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
        public ActionResult Find(string PIDID,string UsersName,string DepotID,string PIDDate,int PIDState,int PageIndex) {
            int count = 0;
            List<ProduceInDepot> list = ProduceInDepotBLL.Find(PIDID, DepotID, PIDDate, UsersName, PIDState, PageIndex, 10, out count);
            List<object> listed = new List<object>();
            if (list.Count!=0)
            {
                count = count % 10 == 0 ? count / 10 : count / 10 + 1;
                foreach (ProduceInDepot item in list)
                {
                    listed.Add(new { PIDID =item.PIDID , DepotID =item.DepotID , DepotName=item.Depots.DepotName, PIDDate =item.PIDDate , PIDUser = item.PIDUser ,UsersName=item.Users.UsersName, PIDState =item.PIDState , PDIDesc =item.PDIDesc,MaxPageIndex=count });
                }
            }
            else
            {
                listed.Add(new { PIDID="",MaxPageIndex=0 });
            }
            return Json(listed);
        }
        /// <summary>
        /// 根据
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult GetProductInDepotDeteilByID(string id) {
            List<ProduceInDepotDeteil> list = ProduceInDepotDeteilBLL.GetProductInDepotDeteilByID(1,100,id);
            List<object> listed = new List<object>();
            foreach (ProduceInDepotDeteil item in list)
            {
                listed.Add(new { PIDDID =item.PIDDID , ProID =item.ProID ,ProName=item.Products.ProName, PIDID =item.PIDID , PIDDAmount =item.PIDDAmount , PIDDPrice =item.PIDDPrice , PIDDDesc =item.PIDDDesc,PUName=item.Products.ProductUnit.PUName,PCName=item.Products.ProductColor.PCName,PSName=item.Products.ProductSpec.PSName });
            }
            return Json(listed);
        }
        /// <summary>
        /// 删除生产入库单
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult DelProductDepot(string id) {
            if (ProduceInDepotBLL.DelStocks(id) > 0)
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
        public ActionResult EditProductInDepot(ProduceInDepot pd, List<ProduceInDepotDeteil> list) {
            if (ProduceInDepotBLL.EdiStocks(pd, list) > 0)
                return Content("edit_yes");
            else
                return Content("edit_no");
        }
        /// <summary>
        /// 审核生产入库单
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult CKProductDepot(string id)
        {
            int userid = Convert.ToInt32(Session["uid"]);
            if (ProduceInDepotBLL.CKProduceInDepot(id,userid) > 0)
                return Content("ck_yes");
            else
                return Content("ck_no");
        }
    }

}