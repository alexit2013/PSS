using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using UI.Models.BLL;
using UI.Models.Model;
using System.Data.Entity.Core.Objects;

namespace UI.Areas.InOut.Controllers
{
    public class QuotePriceController : Controller
    {
        /// <summary>
        /// 获取报价单编号
        /// </summary>
        /// <returns></returns>
        public ActionResult GetQuotePriceID()
        {
            PSSEntities db = new PSSEntities();
            ObjectParameter para = new ObjectParameter("DD", "");
            db.pro_order("QuotePrice", "QPID", "BJ", para);
            return Content(para.Value.ToString());
        }
       /// <summary>
       /// 添加报价单
       /// </summary>
       /// <param name="qp"></param>
       /// <param name="list"></param>
       /// <returns></returns>
        public ActionResult AddQuotePrice(QuotePrice qp,List<QuotePriceDetail> list) {
            qp.UserID = Convert.ToInt32(Session["uid"]);
            if (QuotePriceBLL.AddStocks(qp, list) > 0)
                return Content("add_yes");
            else
                return Content("add_no");
        }
        /// <summary>
        /// 条件查询
        /// </summary>
        /// <param name="QPID">报价单编号</param>
        /// <param name="CusID">客户编号</param>
        /// <param name="QPDate">创建时间</param>
        /// <param name="UsersName">创建人</param>
        /// <param name="QPState">状态</param>
        /// <param name="PageIndex">页数</param>
        /// <returns></returns>
        public ActionResult Find(string QPID,  string UsersName,string CusID, string QPDate, int QPState, int PageIndex)
        {
            int count = 0;
            List<QuotePrice> list = QuotePriceBLL.Find(QPID,CusID,QPDate,UsersName,QPState,PageIndex,10,out count);
            count = count % 10 == 0 ? count / 10 : count / 10 + 1;
            List<object> listed = new List<object>();
            if (list.Count>0)
            {
                foreach (QuotePrice item in list)
                {
                    listed.Add(new { QPID = item.QPID, CusID=item.CusID,CusName=item.Customers.CusName, UserID=item.UserID,UsersName=item.Users.UsersName, QPDate=item.QPDate, QPState=item.QPState, QPDesc=item.QPDesc,MaxPageIndex=count });
                }
            }
            else
            {
                listed.Add(new { QPID ="",MaxPageIndex=0});
            }
            return Json(listed); ;
        }
        /// <summary>
        /// 根据报价单ID查询详单信息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult GetQuotePriceDetailByQPID(string id) {
            List<QuotePriceDetail> list = QuotePriceDetailBLL.GetAllPageBySDID(1,100,id);
            List<object> listed = new List<object>();
            foreach (QuotePriceDetail item in list)
            {
                listed.Add(new { QPDID =item.QPDID , QPID =item.QPID , ProID =item.ProID ,ProName=item.Products.ProName, QPDAmount = item.QPDAmount, QPDPrice = item.QPDPrice, QPDDisPrice = item.QPDDisPrice, QPDDiscont = item.QPDDiscont, QPDDesc=item.QPDDesc, PUName = item.Products.ProductUnit.PUName, PCName = item.Products.ProductColor.PCName, PSName = item.Products.ProductSpec.PSName });
            }
            return Json(listed);
        }
        /// <summary>
        /// 删除报价单
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult DelQuotePrice(string id) {
            if (QuotePriceBLL.DelStocks(id) > 0)
                return Content("del_yes");
            else
                return Content("del_no");
        }
        /// <summary>
        /// 修改报价单
        /// </summary>
        /// <param name="qp"></param>
        /// <param name="list"></param>
        /// <returns></returns>
        public ActionResult EditQuotePrice(QuotePrice qp, List<QuotePriceDetail> list)
        {
            if (QuotePriceBLL.EdiStocks(qp, list) > 0)
                return Content("edit_yes");
            else
                return Content("edit_no");
        }
        /// <summary>
        /// 审核
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult CKQuotePrice(string id) {
            int userid = Convert.ToInt32(Session["uid"]);
            if (QuotePriceBLL.CKInDepot(id,userid) > 0)
                return Content("ck_yes");
            else
                return Content("ck_no");
        }
    }
}