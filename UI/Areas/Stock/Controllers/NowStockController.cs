using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using UI.Models.BLL;
using UI.Models.Model;

namespace UI.Areas.Stock.Controllers
{
    /// <summary>
    /// 库存管理
    /// </summary>
    public class NowStockController : Controller
    {
        /// <summary>
        /// 查询
        /// </summary>
        /// <returns></returns>
        public ActionResult Find(string  ProID,string ProName,int PTID,int PCID,int PSID,int PUID,string DepotID,int PageIndex) {
            int count = 0;
             ProID=  ProID == "" ? "0" : ProID;
           List <DepotStock> list= DepotStockBLL.Find(Convert.ToInt32(ProID), ProName, PTID, PCID, PSID, PUID, DepotID, PageIndex, 10, out count);
            count = count % 10 == 0 ? count / 10 : count / 10 + 1;
            Session["find_count"] = count;
              var  listed = from p in list
                select new
                {
                    p.ProID,
                    p.Products.ProName,
                    p.Products.ProductTypes.PTName,
                    p.Products.ProductUnit.PUName,
                    p.Products.ProductSpec.PSName,
                    p.Products.ProductColor.PCName,
                    p.Depots.DepotName,
                    p.DSAmount,
                    p.Products.ProMax,
                    p.Products.ProMin,
                    p.DSPrice,
                    p.Products.ProInPrice,
                    p.Products.ProDesc
                };
            return Json(listed);
        }
        /// <summary>
        /// 查询后个数
        /// </summary>
        /// <returns></returns>
        public ActionResult FindCount() {
            return Content(Session["find_count"].ToString());
        }
        public ActionResult  FindProByDepots(int id)
        {
            List<DepotStock> list = DepotStockBLL.FindProByDepots(id);
            Dictionary<string, object> dic = new Dictionary<string, object>();
            List<string> l1 = new List<string>();//仓库名称
            List<string> l2 = new List<string>();//库存数量
            foreach (DepotStock item in list)
            {
                l1.Add(item.Depots.DepotName);
                l2.Add(item.DSAmount.ToString());
            }
            dic.Add("dpname",l1);
            dic.Add("procount", l2);
            return Json(dic);
        }
    }
}