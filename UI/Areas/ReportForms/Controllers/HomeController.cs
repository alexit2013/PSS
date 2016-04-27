using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using UI.Filter;

namespace UI.Areas.ReportForms.Controllers
{
    /// <summary>
    /// 统计报表
    /// </summary>
    [PopedomsFilter]
    public class HomeController : Controller
    {
         [Filter.LoginFilter]
        public ActionResult Index()
        {
            return View();
        }
         /// <summary>
         /// 采购订单
         /// </summary>
         /// <returns></returns>
         public ActionResult StocksOrder()
         {
             return View();
         }
         /// <summary>
         /// 客户订单
         /// </summary>
         /// <returns></returns>
         public ActionResult CusOrders()
         {
             return View();
         }
         /// <summary>
         /// 采购入库
         /// </summary>
         /// <returns></returns>
         public ActionResult InStock()
         {
             return View();
         }

         /// <summary>
         /// 进销存
         /// </summary>
         /// <returns></returns>
         public ActionResult PSS()
         {
             return View();
         }
         /// <summary>
         /// 销售出货
         /// </summary>
         /// <returns></returns>
         public ActionResult SaleOut()
         {
             return View();
         }
         /// <summary>
         /// 生产领料
         /// </summary>
         /// <returns></returns>
         public ActionResult ProductionGet()
         {
             return View();
         }
         /// <summary>
         /// 生产入库
         /// </summary>
         /// <returns></returns>
         public ActionResult InStockProduction()
         {
             return View();
         }
         /// <summary>
         /// 采购退货
         /// </summary>
         /// <returns></returns>
         public ActionResult ReturnStock()
         {
             return View();
         }
         /// <summary>
         /// 销售退货
         /// </summary>
         /// <returns></returns>
         public ActionResult ReturnSale()
         {
             return View();
         }

	}
}