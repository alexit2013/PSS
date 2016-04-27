using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Objects;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using UI.Models.BLL;
using UI.Models.Model;

namespace UI.Areas.Stock.Controllers
{
    public class StockCheckController : Controller
    {
        /// <summary>
        /// 获取编号
        /// </summary>
        /// <returns></returns>
        public ActionResult GetID()
        {
            PSSEntities db = new PSSEntities();
            ObjectParameter para = new ObjectParameter("DD", "");
            db.pro_order("CheckDepot", "CDID", "PD", para);
            return Content(para.Value.ToString());
        }
        /// <summary>
        /// 添加订单
        /// </summary>
        /// <param name="pd"></param>
        /// <param name="list"></param>
        /// <returns></returns>
        public ActionResult Add(CheckDepot pd, List<CheckDepotDetail> list)
        {
            pd.UserID = Convert.ToInt32(Session["uid"]);
            if (CheckDepotBLL.AddStocks(pd, list) > 0)
                return Content("add_yes");
            else
                return Content("add_no");
        }
        /// <summary>
        /// 条件查询
        /// </summary>
        /// <param name="LostID">报损单编号</param>
        /// <param name="UsersName">操作人</param>
        /// <param name="DepotID">仓库编号</param>
        /// <param name="LostDate">操作时间</param>
        /// <param name="LostState">状态</param>
        /// <param name="PageIndex">当前页</param>
        /// <returns></returns>
        public ActionResult Find(string CDID, string DepotID, string UsersName, string CDDate, int CDState, int PageIndex)
        {
            int count = 0;
            List<CheckDepot> list = CheckDepotBLL.Find(CDID, DepotID, UsersName, CDDate, CDState, PageIndex, 10, out count);
            int MaxPageIndex = count % 10 == 0 ? count / 10 : count / 10 + 1;
            if (list.Count > 0)
            {
                var listed = from s in list
                             select new
                             {
                                 s.CDID,//报损单详单编号
                                 s.DepotID,//报损单编号
                                 s.Depots.DepotName,//入库仓库名
                                 s.CDDate,
                                 s.Users.UsersName,
                                 s.CDDesc,
                                 s.CDState,
                                 MaxPageIndex = MaxPageIndex//最大页数
                             };
                return Json(listed);
            }
            else
            {
                List<object> listed = new List<object>();
                listed.Add(new { CDID = "", MaxPageIndex = 0 });
                return Json(listed);
            }
        }
        /// <summary>
        /// 根据主单编号查询详单信息
        /// </summary>
        /// <param name="id">主单编号</param>
        /// <returns></returns>
        public ActionResult GetDeteilByID(string id)
        {
            List<CheckDepotDetail> list = CheckDepotDetailBLL.GetDetailByID(id, 1, 100);
            if (list.Count > 0)
            {
                var listed = from s in list
                             select new
                             {
                                 s.CDDID,//盘点单详单编号
                                 s.CDID,//盘点编号
                                 s.Products.ProName,//商品名称
                                 s.ProID,//商品ID
                                 s.Products.ProductUnit.PUName,
                                 s.Products.ProductTypes.PTName,
                                 s.Products.ProductSpec.PSName,
                                 s.Products.ProductColor.PCName,
                                 s.CDDAmount1,//商品数量
                                 s.DevAmount2,//盘点数量
                                 s.CDDDesc//备注
                             };
                return Json(listed);
            }
            else
            {
                List<object> listed = new List<object>();
                return Json(listed);
            }


        }
        /// <summary>
        /// 删除订单
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult Del(string id)
        {
            if (CheckDepotBLL.DelStocks(id) > 0)
                return Content("del_yes");
            else
                return Content("del_no");
        }
        /// <summary>
        /// 修改订单
        /// </summary>
        /// <param name="pd"></param>
        /// <param name="list"></param>
        /// <returns></returns>
        public ActionResult Edit(CheckDepot pd, List<CheckDepotDetail> list)
        {
            if (CheckDepotBLL.EdiStocks(pd, list) > 0)
                return Content("edit_yes");
            else
                return Content("edit_no");
        }
        /// <summary>
        /// 盘点订单【修改状态为2】
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult PDDepot(string id)
        {
            int userid = Convert.ToInt32(Session["uid"]);
            try
            {
                if (CheckDepotBLL.PDDepot(id) > 0)
                    return Content("ck_yes");
                else
                    return Content("ck_no");
            }
            catch (Exception ex)
            {
                return Content(ex.Message);
            }

        }
        /// <summary>
        /// 审核订单
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult CKDepot(string id)
        {
            int userid = Convert.ToInt32(Session["uid"]);
            try
            {
                if (CheckDepotBLL.CKDepot(id, userid) > 0)
                    return Content("ck_yes");
                else
                    return Content("ck_no");
            }
            catch (Exception ex)
            {
                return Content(ex.Message);
            }

        }
        /// <summary>
        /// 根据仓库ID查询库存商品信息
        /// </summary>
        /// <returns></returns>
        public ActionResult GetProByDepotID(string id) {
            List<Products> list = CheckDepotBLL.GetByDepotID(id);
            List<object> listed = new List<object>();
            foreach (Products item in list)
            {
                listed.Add(new { ProID = item.ProID, PTID = item.PTID, PUID = item.PUID, PCID = item.PCID, PSID = item.PSID, ProName = item.ProName, ProJP = item.ProJP, ProTM = item.ProTM, ProWorkShop = item.ProWorkShop, ProMax = item.ProMax, ProMin = item.ProMin, ProInPrice = item.ProInPrice, ProPrice = item.ProPrice, ProDesc = item.ProDesc, PTName = item.ProductTypes.PTName, PUName = item.ProductUnit.PUName, PCName = item.ProductColor.PCName, PSName = item.ProductSpec.PSName, DepotID = item.Depots.DepotID, DepotName = item.Depots.DepotName });
            }
            return Json(listed);
        }
       
    }
}