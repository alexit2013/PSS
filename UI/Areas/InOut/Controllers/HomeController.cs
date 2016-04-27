using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using UI.Models.Model;
using UI.Models.BLL;
using UI.Filter;

namespace UI.Areas.InOut.Controllers
{

    [PopedomsFilter]
    public class HomeController : Controller
    {
        [Filter.LoginFilter]
        public ActionResult Index()
        {
            return View();
        }
        /// <summary>
        /// 报价单
        /// </summary>
        /// <returns></returns>
        public ActionResult Quotation() {
            List<Customers> cus = CustomersBLL.GetAll();
            ViewData["clist"] = cus;
            int count = QuotePriceBLL.GetAll().Count;
            int MaxPageIndex = count % 10 == 0 ? count / 10 : count / 10 + 1;
            ViewData["count"] = count;
            ViewData["MaxPageIndex"] = MaxPageIndex;

            //商品类别
            List<ProductTypes> ptlist = ProductTypesBLL.GetAll();
            //商品规格
            List<ProductSpec> pslist = ProductSpecBLL.GetAll();
            //商品单位
            List<ProductUnit> pulist = ProductUnitBLL.GetAll();
            //商品颜色
            List<ProductColor> pclist = ProductColorBLL.GetAll();
            ViewData["ptlist"] = ptlist;
            ViewData["pslist"] = pslist;
            ViewData["pulist"] = pulist;
            ViewData["pclist"] = pclist;

            return View();
        }
        /// <summary>
        /// 客户订单
        /// </summary>
        /// <returns></returns>
        public ActionResult CusOrders() {
            List<Customers> cus = CustomersBLL.GetAll();
            ViewData["clist"] = cus;
            int count = QuotePriceBLL.GetAll().Count;
            int MaxPageIndex = count % 10 == 0 ? count / 10 : count / 10 + 1;
            ViewData["count"] = count;
            ViewData["MaxPageIndex"] = MaxPageIndex;

            //商品类别
            List<ProductTypes> ptlist = ProductTypesBLL.GetAll();
            //商品规格
            List<ProductSpec> pslist = ProductSpecBLL.GetAll();
            //商品单位
            List<ProductUnit> pulist = ProductUnitBLL.GetAll();
            //商品颜色
            List<ProductColor> pclist = ProductColorBLL.GetAll();
            ViewData["ptlist"] = ptlist;
            ViewData["pslist"] = pslist;
            ViewData["pulist"] = pulist;
            ViewData["pclist"] = pclist;

            int count1 = CustomerOrderBLL.GetAll().Count;
            int MaxPageIndex1 = count1 % 10 == 0 ? count1 / 10 : count1 / 10 + 1;
            ViewData["count1"] = count1;
            ViewData["MaxPageIndex1"] = MaxPageIndex1;
            return View();
        }
        /// <summary>
        /// 销售出货
        /// </summary>
        /// <returns></returns>
        public ActionResult SaleOut() {
            List<Customers> cus = CustomersBLL.GetAll();
            ViewData["clist"] = cus;
            ViewData["dlist"] = DepotsBLL.GetAll();
            int count = SaleDepotBLL.GetAll().Count;
            int MaxPageIndex = count % 10 == 0 ? count / 10 : count / 10 + 1;
            ViewData["count"] = count;
            ViewData["MaxPageIndex"] = MaxPageIndex;
            int count1 = CustomerOrderBLL.GetAll().Count;
            int MaxPageIndex1 = count1 % 10 == 0 ? count1 / 10 : count1 / 10 + 1;
            ViewData["count1"] = count1;
            ViewData["MaxPageIndex1"] = MaxPageIndex1;
            return View();
        }
        /// <summary>
        /// 当前库存
        /// </summary>
        /// <returns></returns>
        public ActionResult NewStock() {
           
            return View();
        }
        /// <summary>
        /// 销售退货
        /// </summary>
        /// <returns></returns>
        public ActionResult ReturnSale() {
            List<Customers> clist = CustomersBLL.GetAll();
            List<Depots> dplist = DepotsBLL.GetAll();
            ViewData["clist"] = clist;
            ViewData["dplist"] = dplist;

            //商品类别
            List<ProductTypes> ptlist = ProductTypesBLL.GetAll();
            //商品规格
            List<ProductSpec> pslist = ProductSpecBLL.GetAll();
            //商品单位
            List<ProductUnit> pulist = ProductUnitBLL.GetAll();
            //商品颜色
            List<ProductColor> pclist = ProductColorBLL.GetAll();
            ViewData["ptlist"] = ptlist;
            ViewData["pslist"] = pslist;
            ViewData["pulist"] = pulist;
            ViewData["pclist"] = pclist;
            return View();
        }
        /// <summary>
        /// 销售收款
        /// </summary>
        /// <returns></returns>
        public ActionResult SaleCollection() {
            return View();
        }
        /// <summary>
        /// 生产领料
        /// </summary>
        /// <returns></returns>
        public ActionResult ProductionGet() {
            List<Depots> cus = DepotsBLL.GetAll();
            ViewData["dlist"] = cus;
            int count = ProduceOutDepotBLL.GetAll().Count;
            int MaxPageIndex = count % 10 == 0 ? count / 10 : count / 10 + 1;
            ViewData["count"] = count;
            ViewData["MaxPageIndex"] = MaxPageIndex;

            //商品类别
            List<ProductTypes> ptlist = ProductTypesBLL.GetAll();
            //商品规格
            List<ProductSpec> pslist = ProductSpecBLL.GetAll();
            //商品单位
            List<ProductUnit> pulist = ProductUnitBLL.GetAll();
            //商品颜色
            List<ProductColor> pclist = ProductColorBLL.GetAll();
            ViewData["ptlist"] = ptlist;
            ViewData["pslist"] = pslist;
            ViewData["pulist"] = pulist;
            ViewData["pclist"] = pclist;
            return View();
        }
        /// <summary>
        /// 其它出库
        /// </summary>
        /// <returns></returns>
        public ActionResult OutOther() {
            List<Depots> cus = DepotsBLL.GetAll();
            ViewData["dlist"] = cus;
            int count = OtherOutDepotBLL.GetAll().Count;
            int MaxPageIndex = count % 10 == 0 ? count / 10 : count / 10 + 1;
            ViewData["count"] = count;
            ViewData["MaxPageIndex"] = MaxPageIndex;
            //商品类别
            List<ProductTypes> ptlist = ProductTypesBLL.GetAll();
            //商品规格
            List<ProductSpec> pslist = ProductSpecBLL.GetAll();
            //商品单位
            List<ProductUnit> pulist = ProductUnitBLL.GetAll();
            //商品颜色
            List<ProductColor> pclist = ProductColorBLL.GetAll();
            ViewData["ptlist"] = ptlist;
            ViewData["pslist"] = pslist;
            ViewData["pulist"] = pulist;
            ViewData["pclist"] = pclist;
            return View();
        }
	}
}