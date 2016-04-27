using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using UI.Models.Model;
using UI.Models.BLL;
using UI.Filter;

namespace UI.Areas.InStock.Controllers
{
   // [PopedomsFilter]
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
        public ActionResult StocksOrder() {
            List<Depots> dplist = DepotsBLL.GetAll();
            List<ProductLend> prolendlist = ProductLendBLL.GetAll();
            ViewData["dplist"] = dplist;
            ViewData["prolendlist"] = prolendlist;

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

            int count = StocksBLL.GetAll().Count;
            int MaxPageIndex = count % 10 == 0 ? count / 10 : count / 10 + 1;
            ViewData["count"] = count;
            ViewData["MaxPageIndex"] = MaxPageIndex;
            return View();
        }
        /// <summary>
        /// 采购入库
        /// </summary>
        /// <returns></returns>
        public ActionResult InStock() {
            List<Depots> dplist = DepotsBLL.GetAll();
            List<ProductLend> prolendlist = ProductLendBLL.GetAll();
            ViewData["dplist"] = dplist;
            ViewData["prolendlist"] = prolendlist;
            //采购单总条数
            int count = StocksBLL.GetAll().Count;
            int MaxPageIndex = count % 10 == 0 ? count / 10 : count / 10 + 1;
            //入库单总数量
            int count1 = StockInDepotBLL.GetAll().Count;
            int MaxPageIndex1 = count1 % 10 == 0 ? count1 / 10 : count1 / 10 + 1;
            ViewData["count"] = count;
            ViewData["MaxPageIndex"] = MaxPageIndex;
            ViewData["count1"] = count1;
            ViewData["MaxPageIndex1"] = MaxPageIndex1;
            ViewData["PageIndex"] = 1;
            ViewData["PageIndex1"] = 1;


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
        /// 采购退货
        /// </summary>
        /// <returns></returns>
        public ActionResult ReturnStock() {
            List<ProductLend> plist = ProductLendBLL.GetAll();
            List<Depots> dplist = DepotsBLL.GetAll();
            ViewData["plist"] = plist;
            ViewData["dplist"] = dplist;
            return View();
        }
        /// <summary>
        /// 生产入库
        /// </summary>
        /// <returns></returns>
        public ActionResult InStockProduction() {
            List<Depots> dplist = DepotsBLL.GetAll();
            List<ProductLend> prolendlist = ProductLendBLL.GetAll();
            ViewData["dplist"] = dplist;
            ViewData["prolendlist"] = prolendlist;
            int count = ProduceInDepotBLL.GetAll().Count();
            ViewData["count"] = count;
            count = count % 10 == 0 ? count / 10 : count / 10 + 1;
            ViewData["MaxPageIndex"] = count;

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
        /// 其它入库
        /// </summary>
        /// <returns></returns>
        public ActionResult InStockOther() {
            List<Depots> dplist = DepotsBLL.GetAll();
            List<ProductLend> prolendlist = ProductLendBLL.GetAll();
            ViewData["dplist"] = dplist;
            ViewData["prolendlist"] = prolendlist;
            int count = OtherInDepotBLL.GetAll().Count();
            ViewData["count"] = count;
            count = count % 10 == 0 ? count / 10 : count / 10 + 1;
            ViewData["MaxPageIndex"] = count;

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
        /// 采购付款
        /// </summary>
        /// <returns></returns>
        public ActionResult StockPayment() {
            return View();
        }
        /// <summary>
        /// 多条件商品查询
        /// </summary>
        /// <param name="ProName">商品名称</param>
        /// <param name="PTID">商品类型</param>
        /// <param name="PUID">商品规格</param>
        /// <param name="PCID">商品颜色</param>
        /// <param name="PSID">商品单位</param>
        /// <param name="PageIndex">当前页</param>
        /// <returns></returns>
        public ActionResult FindProducts(string ProName, int PTID, int PUID, int PCID, int PSID, int PageIndex) {
            int count = 0;
            List<Products> list = ProductsBLL.Find(ProName, PTID, PUID, PCID, PSID, out count, PageIndex, 5);
            int MaxPageIndex = count % 5 == 0 ? count / 5 : count / 5 + 1;
            if (list.Count > 0)
            {
                var listed = from s in list
                             select new
                             {
                                 s.ProID,
                                 s.ProName,
                                 s.ProductTypes.PTName,
                                 s.ProductUnit.PUName,
                                 s.ProductSpec.PSName,
                                 s.ProductColor.PCName,
                                 s.ProPrice,
                                 MaxPageIndex = MaxPageIndex//最大页数
                             };
                return Json(listed);
            }
            else
            {
                List<object> listed = new List<object>();
                listed.Add(new { ProID = "", MaxPageIndex = 0 });
                return Json(listed);
            }
        }
	}
}