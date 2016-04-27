using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Objects;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using UI.Models.Model;
using UI.Models.BLL;

namespace UI.Areas.Stock.Controllers
{
    public class StockBreakageController : Controller
    {
        /// <summary>
        /// 获取编号
        /// </summary>
        /// <returns></returns>
        public ActionResult GetID()
        {
            PSSEntities db = new PSSEntities();
            ObjectParameter para = new ObjectParameter("DD", "");
            db.pro_order("Losts", "LostID", "BS", para);
            return Content(para.Value.ToString());
        }
        /// <summary>
        /// 添加订单
        /// </summary>
        /// <param name="pd"></param>
        /// <param name="list"></param>
        /// <returns></returns>
        public ActionResult Add(Losts pd, List<LostDetail> list)
        {
            pd.UserID = Convert.ToInt32(Session["uid"]);
            if (LostsBLL.AddStocks(pd, list) > 0)
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
        public ActionResult Find(string LostID, string UsersName, string DepotID, string LostDate, int LostState, int PageIndex)
        {
            int count = 0;
            List<Losts> list = LostsBLL.Find(LostID, DepotID, LostDate, UsersName, LostState, PageIndex, 10, out count);
            int MaxPageIndex = count % 10 == 0 ? count / 10 : count / 10 + 1;
            if (list.Count > 0)
            {
                var listed = from s in list
                             select new
                             {
                                 s.LostID,//报损单详单编号
                                 s.DepotID,//报损单编号
                                 s.Depots.DepotName,
                                 s.LostDate,
                                 s.Users.UsersName,
                                 s.LostDesc,
                                 s.LostState,
                                 MaxPageIndex=MaxPageIndex//最大页数
                             };
                return Json(listed);
            }
            else
            {
                List<object> listed = new List<object>();
                listed.Add(new { LostID = "", MaxPageIndex = 0 });
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
            List<LostDetail> list = LostDetailBLL.GetDeteilByID(id, 1, 100);
            if (list.Count>0)
            {
                var listed = from s in list
                             select new {
                                s.LDID,//报损单详单编号
                                s.LostID,//报损单编号
                                s.Products.ProName,//商品名称
                                s.ProID,//商品ID
                                s.Products.ProductUnit.PUName,
                                 s.Products.ProductTypes.PTName,
                                 s.Products.ProductSpec.PSName,
                                 s.Products.ProductColor.PCName,
                                 s.LDAmount,//商品数量
                                 s.LDPrice,//商品单价
                                 s.LDDesc,//备注
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
            if (LostsBLL.DelStocks(id) > 0)
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
        public ActionResult Edit(Losts pd, List<LostDetail> list)
        {
            if (LostsBLL.EdiStocks(pd, list) > 0)
                return Content("edit_yes");
            else
                return Content("edit_no");
        }
        /// <summary>
        /// 审核订单
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult CKOtherDepot(string id)
        {
            int userid = Convert.ToInt32(Session["uid"]);
            try
            {
                if (LostsBLL.CKInDepot(id, userid) > 0)
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