using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Objects;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using UI.Models.BLL;
using UI.Models.Model;

namespace UI.Areas.InStock.Controllers
{
    public class StockReturnController : Controller
    {
        /// <summary>
        /// 获取采购退货单编号
        /// </summary>
        /// <returns></returns>
        public ActionResult GetStockReturnID()
        {
            PSSEntities db = new PSSEntities();
            ObjectParameter para = new ObjectParameter("DD", "");
            db.pro_order("StockReturn", "KRID", "CT", para);
            return Content(para.Value.ToString());
        }
        /// <summary>
        /// 添加采购退货单
        /// </summary>
        /// <param name="qp"></param>
        /// <param name="list"></param>
        /// <returns></returns>
        public ActionResult AddStockReturn(StockReturn qp, List<StockReturnDetail> list)
        {
            qp.UserID = Convert.ToInt32(Session["uid"]);
            if (StockReturnBLL.AddStocks(qp, list) > 0)
                return Content("add_yes");
            else
                return Content("add_no");
        }
      /// <summary>
      /// 条件查询采购退货单
      /// </summary>
      /// <param name="KRID">采购退货单编号</param>
      /// <param name="UsersName">退货人</param>
      /// <param name="PPID">供货商</param>
      /// <param name="SIDID">采购入库单编号</param>
      /// <param name="DepotID">仓库编号</param>
      /// <param name="KRDate">退货时间</param>
      /// <param name="KRState">退货单状态</param>
      /// <param name="PageIndex">当前页</param>
      /// <returns></returns>
        public ActionResult Find(string KRID,string UsersName, string PPID, string SIDID, string DepotID, string  KRDate, int KRState, int PageIndex)
        {
            int count = 0;
            List<StockReturn> list = StockReturnBLL.Find(KRID,PPID,SIDID,DepotID,KRDate,UsersName, KRState,PageIndex, 10, out count);
            count = count % 10 == 0 ? count / 10 : count / 10 + 1;
            if (count>0)
            {
                var listed = from s in list
                             select new
                             {
                                 s.KRID,
                                 s.PPID,
                                 s.ProductLend.PPName,
                                 s.SIDID,
                                 s.DepotID,
                                 s.Depots.DepotName,
                                 s.KRDate,
                                 s.UserID,
                                 s.Users.UsersName,
                                 s.KRState,
                                 s.KRDesc,
                                 MaxPageIndex = count
                             };
                return Json(listed);
            }
            else
            {
                List<object> listed = new List<object>();
                listed.Add(new { KRID="",MaxPageIndex=0});
                return Json(listed);
            }
           
        }
      /// <summary>
      /// 根据采购退货单ID查询详单信息
      /// </summary>
      /// <param name="id"></param>
      /// <returns></returns>
        public ActionResult GetStockReturnDetailByQPID(string id)
        {
            List<StockReturnDetail> list = StockReturnDetailBLL.GetAllPageBySDID(1, 100, id);
            var listed = from s in list select new {
              s.ProID,s.KRDID,s.KRID,s.KRDAmount,s.KRDPrice,s.KRDDesc,s.Products.ProName,s.Products.ProductTypes.PTName,s.Products.ProductUnit.PUName,s.Products.ProductSpec.PSName,s.Products.ProductColor.PCName
            };
            return Json(listed);
        }
        /// <summary>
        /// 删除销售出库单
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult DelStockReturn(string id)
        {
            if (StockReturnBLL.DelStocks(id) > 0)
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
        public ActionResult EditStockReturn(StockReturn qp, List<StockReturnDetail> list)
        {
            if (StockReturnBLL.EditStocks(qp, list) > 0)
                return Content("edit_yes");
            else
                return Content("edit_no");
        }
        /// <summary>
        /// 审核
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult CKStockReturn(string id)
        {
            try
            {
                int userid = Convert.ToInt32(Session["uid"]);

                if (StockReturnBLL.CKInDepot(id, userid) > 0)
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