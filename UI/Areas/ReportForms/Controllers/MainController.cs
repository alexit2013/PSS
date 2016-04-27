using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using UI.Models.DAL;

namespace UI.Areas.ReportForms.Controllers
{
    public class MainController : Controller
    {
        #region 获取近12个月的时间
        public ActionResult GetNow12Y() {
            List<string> dts = new List<string>();
            DateTime dt = DateTime.Now.AddYears(-1);
            
            for (int i = 1; i <=12; i++)
            {
                DateTime d=  dt.AddMonths(i);
                string y = d.Year.ToString();
                string m = d.Month.ToString();
                dts.Add(y+"年"+m+"月");
            }
            return Json(dts);
        }
        #endregion

        #region 采购订单
        /// <summary>
        /// 查询近12个月的采购单个数
        /// </summary>
        /// <returns></returns>
        public ActionResult GetStocksCountByMtoth() {
            List<int> dts = new List<int>();
            DateTime dt = DateTime.Now.AddYears(-1);

            for (int i = 1; i <= 12; i++)
            {
                DateTime d = dt.AddMonths(i);
                dts.Add(StocksDAL.FindByYCount(d));
            }
            return Json(dts);
        }
        /// <summary>
        /// 查询近12个月的采购单个数
        /// </summary>
        /// <returns></returns>
        public ActionResult GetStocksCountByMtoth1()
        {
            List<int> dts = new List<int>();
            DateTime dt = DateTime.Now.AddYears(-1);

            for (int i = 1; i <= 12; i++)
            {
                DateTime d = dt.AddMonths(i);
                dts.Add(StocksDAL.FindByYCount1(d));
            }
            return Json(dts);
        }
        /// <summary>
        /// 查询采购单审核数和未审核数
        /// </summary>
        /// <returns></returns>
        public ActionResult GetStocksStateCount() {
            List<int> list = new List<int>();
            list.Add(StocksDAL.FindStateCount(0));
            list.Add(StocksDAL.FindStateCount(1));
            return Json(list);
        }
        #endregion

        #region 客户订单
        /// <summary>
        /// 查询近12个月的未完成客户订单个数
        /// </summary>
        /// <returns></returns>
        public ActionResult GetCusOrderCountByMtoth()
        {
            List<int> dts = new List<int>();
            DateTime dt = DateTime.Now.AddYears(-1);
            for (int i = 1; i <= 12; i++)
            {
                DateTime d = dt.AddMonths(i);
                dts.Add(CustomerOrderDAL.FindByYCount(d));
            }
            return Json(dts);
        }
        /// <summary>
        /// 查询近12个月已完成的客户订单个数
        /// </summary>
        /// <returns></returns>
        public ActionResult GetCusOrderCountByMtoth1()
        {
            List<int> dts = new List<int>();
            DateTime dt = DateTime.Now.AddYears(-1);

            for (int i = 1; i <= 12; i++)
            {
                DateTime d = dt.AddMonths(i);
                dts.Add(CustomerOrderDAL.FindByYCount1(d));
            }
            return Json(dts);
        }
        /// <summary>
        /// 查询采购单审核数和未审核数
        /// </summary>
        /// <returns></returns>
        public ActionResult GetCusOrderStateCount()
        {
            List<int> list = new List<int>();
            list.Add(CustomerOrderDAL.FindStateCount(0));
            list.Add(CustomerOrderDAL.FindStateCount(1));
            return Json(list);
        }
        #endregion

        #region 采购入库

        /// <summary>
        /// 查询近12个月的未审核采购入库单个数
        /// </summary>
        /// <returns></returns>
        public ActionResult GetStockInDepotCountByMtoth()
        {
            List<int> dts = new List<int>();
            DateTime dt = DateTime.Now.AddYears(-1);
            for (int i = 1; i <= 12; i++)
            {
                DateTime d = dt.AddMonths(i);
                dts.Add(StockInDepotDAL.FindByYCount(d,0));
            }
            return Json(dts);
        }
        /// <summary>
        /// 查询近12个月的已审核采购入库单个数
        /// </summary>
        /// <returns></returns>
        public ActionResult GetStockInDepotCountByMtoth1()
        {
            List<int> dts = new List<int>();
            DateTime dt = DateTime.Now.AddYears(-1);

            for (int i = 1; i <= 12; i++)
            {
                DateTime d = dt.AddMonths(i);
                dts.Add(StockInDepotDAL.FindByYCount(d,1));
            }
            return Json(dts);
        }
        /// <summary>
        /// 查询采购单审核数和未审核数
        /// </summary>
        /// <returns></returns>
        public ActionResult GetStockInDepotStateCount()
        {
            List<int> list = new List<int>();
            list.Add(StockInDepotDAL.FindStateCount(0));
            list.Add(StockInDepotDAL.FindStateCount(1));
            return Json(list);
        }
        #endregion

        #region 销售出库

        public ActionResult GetSaleDepotCountByMtoth()
        {
            List<int> dts = new List<int>();
            DateTime dt = DateTime.Now.AddYears(-1);
            for (int i = 1; i <= 12; i++)
            {
                DateTime d = dt.AddMonths(i);
                dts.Add(SaleDepotDAL.FindByYCount(d, 0));
            }
            return Json(dts);
        }
      
        public ActionResult GetSaleDepotCountByMtoth1()
        {
            List<int> dts = new List<int>();
            DateTime dt = DateTime.Now.AddYears(-1);

            for (int i = 1; i <= 12; i++)
            {
                DateTime d = dt.AddMonths(i);
                dts.Add(SaleDepotDAL.FindByYCount(d, 1));
            }
            return Json(dts);
        }
      
        public ActionResult GetSaleDepotStateCount()
        {
            List<int> list = new List<int>();
            list.Add(SaleDepotDAL.FindStateCount(0));
            list.Add(SaleDepotDAL.FindStateCount(1));
            return Json(list);
        }

        #endregion

        #region 生产领料

        public ActionResult GetProduceOutDepotCountByMtoth()
        {
            List<int> dts = new List<int>();
            DateTime dt = DateTime.Now.AddYears(-1);
            for (int i = 1; i <= 12; i++)
            {
                DateTime d = dt.AddMonths(i);
                dts.Add(ProduceOutDepotDAL.FindByYCount(d, 0));
            }
            return Json(dts);
        }

        public ActionResult GetProduceOutDepotCountByMtoth1()
        {
            List<int> dts = new List<int>();
            DateTime dt = DateTime.Now.AddYears(-1);

            for (int i = 1; i <= 12; i++)
            {
                DateTime d = dt.AddMonths(i);
                dts.Add(ProduceOutDepotDAL.FindByYCount(d, 1));
            }
            return Json(dts);
        }

        public ActionResult GetProduceOutDepotStateCount()
        {
            List<int> list = new List<int>();
            list.Add(ProduceOutDepotDAL.FindStateCount(0));
            list.Add(ProduceOutDepotDAL.FindStateCount(1));
            return Json(list);
        }

        #endregion

        #region 生产入库

        public ActionResult GetProduceInDepotCountByMtoth()
        {
            List<int> dts = new List<int>();
            DateTime dt = DateTime.Now.AddYears(-1);
            for (int i = 1; i <= 12; i++)
            {
                DateTime d = dt.AddMonths(i);
                dts.Add(ProduceInDepotDAL.FindByYCount(d, 0));
            }
            return Json(dts);
        }

        public ActionResult GetProduceInDepotCountByMtoth1()
        {
            List<int> dts = new List<int>();
            DateTime dt = DateTime.Now.AddYears(-1);

            for (int i = 1; i <= 12; i++)
            {
                DateTime d = dt.AddMonths(i);
                dts.Add(ProduceInDepotDAL.FindByYCount(d, 1));
            }
            return Json(dts);
        }

        public ActionResult GetProduceInDepotStateCount()
        {
            List<int> list = new List<int>();
            list.Add(ProduceInDepotDAL.FindStateCount(0));
            list.Add(ProduceInDepotDAL.FindStateCount(1));
            return Json(list);
        }

        #endregion

        #region 采购退货

        public ActionResult GetStockReturnCountByMtoth()
        {
            List<int> dts = new List<int>();
            DateTime dt = DateTime.Now.AddYears(-1);
            for (int i = 1; i <= 12; i++)
            {
                DateTime d = dt.AddMonths(i);
                dts.Add(StockReturnDAL.FindByYCount(d, 0));
            }
            return Json(dts);
        }

        public ActionResult GetStockReturnCountByMtoth1()
        {
            List<int> dts = new List<int>();
            DateTime dt = DateTime.Now.AddYears(-1);

            for (int i = 1; i <= 12; i++)
            {
                DateTime d = dt.AddMonths(i);
                dts.Add(StockReturnDAL.FindByYCount(d, 1));
            }
            return Json(dts);
        }

        public ActionResult GetStockReturnStateCount()
        {
            List<int> list = new List<int>();
            list.Add(StockReturnDAL.FindStateCount(0));
            list.Add(StockReturnDAL.FindStateCount(1));
            return Json(list);
        }

        #endregion

        #region 销售退货

        public ActionResult GetSaleReturnCountByMtoth()
        {
            List<int> dts = new List<int>();
            DateTime dt = DateTime.Now.AddYears(-1);
            for (int i = 1; i <= 12; i++)
            {
                DateTime d = dt.AddMonths(i);
                dts.Add(SaleReturnDAL.FindByYCount(d, 0));
            }
            return Json(dts);
        }

        public ActionResult GetSaleReturnCountByMtoth1()
        {
            List<int> dts = new List<int>();
            DateTime dt = DateTime.Now.AddYears(-1);

            for (int i = 1; i <= 12; i++)
            {
                DateTime d = dt.AddMonths(i);
                dts.Add(SaleReturnDAL.FindByYCount(d, 1));
            }
            return Json(dts);
        }

        public ActionResult GetSaleReturnStateCount()
        {
            List<int> list = new List<int>();
            list.Add(SaleReturnDAL.FindStateCount(0));
            list.Add(SaleReturnDAL.FindStateCount(1));
            return Json(list);
        }

        #endregion

        #region 进销存
        /// <summary>
        /// 查询所有的入库数据
        /// </summary>
        /// <returns></returns>
        public ActionResult StockIn() {
            List<int> dts = new List<int>();
            DateTime dt = DateTime.Now.AddYears(-1);
            for (int i = 1; i <= 12; i++)
            {
                DateTime d = dt.AddMonths(i);
                int cr = StockInDepotDAL.FindByYCount(d, 0) + StockInDepotDAL.FindByYCount(d, 1);//采购入库数
                int sr = ProduceInDepotDAL.FindByYCount(d, 0) + ProduceInDepotDAL.FindByYCount(d, 1);//生产入库
                int qr = OtherInDepotDAL.FindByYCount(d);//其它入库
                int st = SaleReturnDAL.FindByYCount(d, 0) + SaleReturnDAL.FindByYCount(d, 1);//销售退货
                dts.Add(cr+sr+qr+st);
            }
            return Json(dts);
        }
        /// <summary>
        /// 查询所有的出库数据
        /// </summary>
        /// <returns></returns>
        public ActionResult StockOut()
        {
            List<int> dts = new List<int>();
            DateTime dt = DateTime.Now.AddYears(-1);
            for (int i = 1; i <= 12; i++)
            {
                DateTime d = dt.AddMonths(i);
                int cr = SaleDepotDAL.FindByYCount(d, 0) + SaleDepotDAL.FindByYCount(d, 1);//销售出库数
                int sr = ProduceOutDepotDAL.FindByYCount(d, 0) + ProduceOutDepotDAL.FindByYCount(d, 1);//生产领料数
                int qr = OtherOutDepotDAL.FindByYCount(d);//其它出库
                int st = StockReturnDAL.FindByYCount(d, 0) + StockReturnDAL.FindByYCount(d, 1);//采购退货
                dts.Add(cr + sr + qr + st);
            }
            return Json(dts);
        }
        /// <summary>
        /// 采购退货
        /// </summary>
        /// <returns></returns>
        public ActionResult StockCT()
        {
            List<int> dts = new List<int>();
            DateTime dt = DateTime.Now.AddYears(-1);
            for (int i = 1; i <= 12; i++)
            {
                DateTime d = dt.AddMonths(i);
                int st = StockReturnDAL.FindByYCount(d, 0) + StockReturnDAL.FindByYCount(d, 1);//采购退货
                dts.Add(st);
            }
            return Json(dts);
        }
        /// <summary>
        /// 销售退货
        /// </summary>
        /// <returns></returns>
        public ActionResult StockST()
        {
            List<int> dts = new List<int>();
            DateTime dt = DateTime.Now.AddYears(-1);
            for (int i = 1; i <= 12; i++)
            {
                DateTime d = dt.AddMonths(i);
                int st = StockReturnDAL.FindByYCount(d, 0) + StockReturnDAL.FindByYCount(d, 1);//销售退货
                dts.Add(st);
            }
            return Json(dts);
        }

        /// <summary>
        /// 库存调拨
        /// </summary>
        /// <returns></returns>
        public ActionResult StockDB()
        {
            List<int> dts = new List<int>();
            DateTime dt = DateTime.Now.AddYears(-1);
            for (int i = 1; i <= 12; i++)
            {
                DateTime d = dt.AddMonths(i);
                int st = DevolvesDAL.FindByYCount(d);
                dts.Add(st);
            }
            return Json(dts);
        }

        #endregion
    }
}