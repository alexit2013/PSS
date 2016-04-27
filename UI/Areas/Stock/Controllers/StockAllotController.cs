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
    public class StockAllotController : Controller
    {
        /// <summary>
        /// 获取编号
        /// </summary>
        /// <returns></returns>
        public ActionResult GetID()
        {
            PSSEntities db = new PSSEntities();
            ObjectParameter para = new ObjectParameter("DD", "");
            db.pro_order("Devolves", "DevID", "DB", para);
            return Content(para.Value.ToString());
        }
        /// <summary>
        /// 添加订单
        /// </summary>
        /// <param name="pd"></param>
        /// <param name="list"></param>
        /// <returns></returns>
        public ActionResult Add(Devolves pd, List<DevolveDetail> list)
        {
            pd.UserID = Convert.ToInt32(Session["uid"]);
            if (DevolvesBLL.AddStocks(pd, list) > 0)
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
        public ActionResult Find(string DevID, string DevOutID, string DevInID,string UsersName, string DevDate, int DevState, int PageIndex)
        {
            int count = 0;
            List<Devolves> list = DevolvesBLL.Find(DevID, DevOutID, DevInID, UsersName, DevDate,DevState, PageIndex, 10, out count);
            int MaxPageIndex = count % 10 == 0 ? count / 10 : count / 10 + 1;
            if (list.Count > 0)
            {
                var listed = from s in list
                             select new
                             {
                                 s.DevID,//报损单详单编号
                                 s.DevOutID,//报损单编号
                                 s.DevInID,//报损单编号
                                 OutDepotName = s.Depots1.DepotName,//出库仓库名
                                 InDepotName = s.Depots.DepotName,//入库仓库名
                                 s.DevDate,
                                 s.Users.UsersName,
                                 s.DevDesc,
                                 s.DevState,
                                 MaxPageIndex = MaxPageIndex//最大页数
                             };
                return Json(listed);
            }
            else
            {
                List<object> listed = new List<object>();
                listed.Add(new { DevID = "", MaxPageIndex = 0 });
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
            List<DevolveDetail> list = DevolveDetailBLL.GetDetailByID(id, 1, 100);
            if (list.Count > 0)
            {
                var listed = from s in list
                             select new
                             {
                                 s.DevDID,//报损单详单编号
                                 s.DevID,//报损单编号
                                 s.Products.ProName,//商品名称
                                 s.ProID,//商品ID
                                 s.Products.ProductUnit.PUName,
                                 s.Products.ProductTypes.PTName,
                                 s.Products.ProductSpec.PSName,
                                 s.Products.ProductColor.PCName,
                                 s.DevDAmount,//商品数量
                                 s.DevDDesc,//备注
                                 s.Products.ProInPrice//商品单价
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
            if (DevolvesBLL.DelStocks(id) > 0)
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
        public ActionResult Edit(Devolves pd, List<DevolveDetail> list)
        {
            if (DevolvesBLL.EdiStocks(pd, list) > 0)
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
                if (DevolvesBLL.CKInDepot(id, userid) > 0)
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