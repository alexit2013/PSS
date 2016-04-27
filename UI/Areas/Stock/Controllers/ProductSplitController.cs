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
    public class ProductSplitController : Controller
    {
        /// <summary>
        /// 获取编号
        /// </summary>
        /// <returns></returns>
        public ActionResult GetID()
        {
            PSSEntities db = new PSSEntities();
            ObjectParameter para = new ObjectParameter("DD", "");
            db.pro_order("Splits", "SplitID", "CF", para);
            return Content(para.Value.ToString());
        }
        /// <summary>
        /// 添加订单
        /// </summary>
        /// <param name="pd"></param>
        /// <param name="list"></param>
        /// <returns></returns>
        public ActionResult Add(Splits pd, List<SplitDetail> list)
        {
            pd.UserID = Convert.ToInt32(Session["uid"]);
            if (SplitsBLL.AddStocks(pd, list) > 0)
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
        public ActionResult Find(string SplitID, string DepotID,string ProName, string UsersName, string SplitDate, int SplitState, int PageIndex)
        {
            int count = 0;
            List<Splits> list = SplitsBLL.Find(DepotID, DepotID, ProName, UsersName, SplitDate, SplitState, PageIndex, 10, out count);
            int MaxPageIndex = count % 10 == 0 ? count / 10 : count / 10 + 1;
            if (list.Count > 0)
            {
                var listed = from s in list
                             select new
                             {
                                 s.SplitID,//报损单详单编号
                                 s.DepotID,//报损单编号
                                 s.Depots.DepotName,//入库仓库名
                                 s.ProID,//商品ID
                                 s.Products.ProName,//商品名称
                                 s.SplitDate,//拆分时间
                                 s.SplitDesc,//备注
                                 s.Users.UsersName,//操作人
                                 s.SplitState,//状态
                                 s.SplitAmount,//数量
                                 s.SplitPrice,//价格
                                 MaxPageIndex = MaxPageIndex//最大页数
                             };
                return Json(listed);
            }
            else
            {
                List<object> listed = new List<object>();
                listed.Add(new { SplitID = "", MaxPageIndex = 0 });
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
            List<SplitDetail> list = SplitDetailBLL.GetDetailByID(id, 1, 100);
            if (list.Count > 0)
            {
                var listed = from s in list
                             select new
                             {
                                 s.SplitID,//盘点单详单编号
                                 s.SDID,//盘点编号
                                 s.Products.ProName,//商品名称
                                 s.ProID,//商品ID
                                 s.Products.ProductUnit.PUName,
                                 s.Products.ProductTypes.PTName,
                                 s.Products.ProductSpec.PSName,
                                 s.Products.ProductColor.PCName,
                                 s.SDAmount,//商品数量
                                 s.SDPrice,//价格
                                 s.SDDesc//备注
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
            if (SplitsBLL.DelStocks(id) > 0)
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
        public ActionResult Edit(Splits pd, List<SplitDetail> list)
        {
            if (SplitsBLL.EdiStocks(pd, list) > 0)
                return Content("edit_yes");
            else
                return Content("edit_no");
        }
        /// <summary>
        /// 盘点订单【修改状态为2】
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult CKDepot(string id)
        {
            int userid = Convert.ToInt32(Session["uid"]);
            try
            {
                if (SplitsBLL.CKDepot(id,userid) > 0)
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
        
    }
}