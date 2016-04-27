using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using UI.Models.Model;
using UI.Models.BLL;

namespace UI.Areas.Stock.Controllers
{
    public class StockChangeController : Controller
    {
        public ActionResult Find(string ProID,string BDate,string EDate,string DepotID,int PageIndex) {
            int count = 0;
            List<InOutDepotDetail> list = InOutDepotBLL.Find(ProID, BDate, EDate, DepotID, out count,PageIndex);
            count = count % 10 == 0 ? count / 10 : count / 10 + 1;
            if (count > 0)
            {
                var listed = from s in list
                             select new
                             {
                                 s.InOutDepot.IODNum,   //单据编号
                                 s.InOutDepot.IODDate,  //发生日期
                                 s.ProID,                           //商品编号
                                 s.Products.ProName,    //商品名称
                                 s.Products.ProductUnit.PUName,//商品单位
                                 s.Products.ProductSpec.PSName,//商品规格
                                 s.Products.ProductColor.PCName,//商品颜色
                                 s.IODDPrice,                //商品价格
                                 s.IODDAmount,          //商品数量
                                 s.InOutDepot.Depots.DepotName,//发生出库
                                 s.InOutDepot.IODType,//业务类型
                                 MaxPageIndex=count
                             };
                return Json(listed);
            }
            else
            {
                List<object> listed = new List<object>();
                listed.Add(new { IODID = "", MaxPageIndex = 0 });
                return Json(listed);
            }
        }
    }
}