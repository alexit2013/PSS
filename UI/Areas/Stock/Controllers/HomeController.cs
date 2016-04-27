using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using UI.Models.DAL;
using UI.Models.BLL;
using UI.Models.Model;
using UI.Filter;

namespace UI.Areas.Stock.Controllers
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
        /// 库存调拨
        /// </summary>
        /// <returns></returns>
       public ActionResult StockAllot() {
            ViewData["dlist"] = DepotsBLL.GetAll();//仓库
            return View();
       }
        /// <summary>
        /// 商品拆分
        /// </summary>
        /// <returns></returns>
       public ActionResult ProductSplit() {
            ViewData["dlist"] = DepotsBLL.GetAll();//仓库
            return View();
       }
        /// <summary>
        /// 库存报损
        /// </summary>
        /// <returns></returns>
       public ActionResult StockBreakage() {
            ViewData["dlist"] = DepotsBLL.GetAll();//仓库
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
        /// 库存溢出
        /// </summary>
        /// <returns></returns>
       public ActionResult StockOverflow() {
            ViewData["dlist"] = DepotsBLL.GetAll();//仓库
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
        /// 当前库存
        /// </summary>
        public ActionResult NewStock(){
            ViewData["ptlist"] = ProductTypesBLL.GetAll();//商品类别
            ViewData["pclist"] = ProductColorBLL.GetAll();//商品颜色
            ViewData["pslist"] = ProductSpecBLL.GetAll();//商品规格
            ViewData["pulist"] = ProductUnitBLL.GetAll();//商品单位
            ViewData["dplist"] = DepotsBLL.GetAll();//仓库
            int count = DepotStockDAL.GetAll().Count;
            ViewData["count"] = count;
            count = count % 10 == 0 ? count / 10 : count / 10 + 1;
            ViewData["MaxPageIndex"] = count;
            ViewData["PageIndex"] = 1;
            return View();
        }
        /// <summary>
        /// 库存异动
        /// </summary>
        /// <returns></returns>
        public ActionResult StockChange() {
            ViewData["dplist"] = DepotsBLL.GetAll();//仓库
            List<InOutDepotDetail> list = InOutDepotBLL.GetAll();
            int MaxPageIndex = list.Count % 10 == 0 ? list.Count / 10 : list.Count / 10 + 1;
            ViewData["MaxPageIndex"] = MaxPageIndex;
            ViewData["count"] = list.Count;
            return View();
        }
       /// <summary>
       /// 库存盘点
       /// </summary>
       /// <returns></returns>
        public ActionResult StockCheck() {
            ViewData["dlist"] = DepotsBLL.GetAll();//仓库
            return View();
        }
        /// <summary>
        /// 商品组装
        /// </summary>
        /// <returns></returns>
        public ActionResult ProductAssemble() {
            return View();
        }

	}
}