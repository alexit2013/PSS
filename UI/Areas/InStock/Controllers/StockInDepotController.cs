using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using UI.Models.BLL;
using UI.Models.Model;
using System.Threading;
using System.Data.Entity.Core.Objects;

namespace UI.Areas.InStock.Controllers
{
    public class StockInDepotController : Controller
    {
        /// <summary>
        /// 采购订单分页
        /// </summary>
        /// <param name="PageIndex"></param>
        /// <returns></returns>
        public ActionResult StockSPage(int PageIndex)
        {
            List<Stocks> list = StocksBLL.GetAllPage(PageIndex,10);
            List<object> listed = new List<object>();
            foreach (Stocks item in list)
            {
                listed.Add(new { StockID=item.StockID,PPID=item.PPID,PPName=item.ProductLend.PPName, StockDate=item.StockDate, StockInDate=item.StockInDate, StockUser=item.StockUser,UsersName=item.Users.UsersName, StockState=item.StockState, StockDesc=item.StockDesc });
            }
            return Json(listed);
        }
        /// <summary>
        /// 查询所有的商品信息
        /// </summary>
        /// <returns></returns>
        public ActionResult GetProductsAll() {
            List<Products> listed = ProductsBLL.GetAll();
            List<object> list = new List<object>();
            foreach (Products item in listed)
            {
                list.Add(new { ProID = item.ProID, PTID = item.PTID, PUID = item.PUID, PCID = item.PCID, PSID = item.PSID, ProName = item.ProName, ProJP = item.ProJP, ProTM = item.ProTM, ProWorkShop = item.ProWorkShop, ProMax = item.ProMax, ProMin = item.ProMin, ProInPrice = item.ProInPrice, ProPrice = item.ProPrice, ProDesc = item.ProDesc, PTName = item.ProductTypes.PTName, PUName = item.ProductUnit.PUName, PCName = item.ProductColor.PCName, PSName = item.ProductSpec.PSName, DepotID = item.Depots.DepotID, DepotName = item.Depots.DepotName });
            }
            return Json(list);
        }
        /// <summary>
        /// 添加采购订单
        /// </summary>
        /// <param name="st"></param>
        /// <param name="list"></param>
        /// <returns></returns>
        public ActionResult AddStocks(Stocks st, List<StockDetail> list) {
            st.StockUser = Convert.ToInt32(Session["uid"]);
            if (StocksBLL.AddStocks(st, list) > 0)
                return Content("add_yes");
           else
                return Content("add_yes");
        }
        /// <summary>
        /// 根据采购订单ID查询采购订单详单【分页】
        /// </summary>
        /// <param name="PageIndex"></param>
        /// <param name="SDID"></param>
        /// <returns></returns>
        public ActionResult GetPageStocks(int PageIndex,String SDID)
        {
            List<StockDetail> list = StockDetailBLL.GetAllPageBySDID(PageIndex,10,SDID);
            List<object> listed = new List<object>();
            foreach (StockDetail item in list)
            {
                listed.Add(new { SDetailID=item.SDetailID, ProID=item.ProID,ProName=item.Products.ProName, StockID=item.StockID, SDetailAmount=item.SDetailAmount, SDetailPrice=item.SDetailPrice, SDetailDepAmount=item.SDetailDepAmount, SDetailDesc=item.SDetailDesc });
            }
            return Json(listed);
        }
        /// <summary>
        /// 删除采购订单
        /// </summary>
        /// <returns></returns>
        public ActionResult DelStocks(string id) {
            if (StocksBLL.DelStocks(id) > 0)
                return Content("del_yes");
            else
            return Content("del_no");
        }
        /// <summary>
        /// 根据采购订单ID查询订单详情
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult GetStockDetailByStockID(string id) {
            List<StockDetail> list= StockDetailBLL.GetAllPageBySDID(1,1000,id);
            List<object> listed = new List<object>();
            foreach (StockDetail item in list)
            {
                listed.Add(new { SDetailID =item.SDetailID , ProID =item.ProID , StockID =item.StockID , SDetailAmount =item.SDetailAmount , SDetailPrice =item.SDetailPrice , SDetailDepAmount =item.SDetailDepAmount , SDetailDesc =item.SDetailDesc , ProName = item.Products.ProName, PTName=item.Products.ProductTypes.PTName, PUName=item.Products.ProductUnit.PUName, PCName=item.Products.ProductColor.PCName, PSName=item.Products.ProductSpec.PSName});
            }
            return Json(listed);
        }
        /// <summary>
        /// 根据ID查询采购订单信息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult GetStockByID(string id) {
            Stocks item = StocksBLL.GetByID(id);
            return Json(new { StockID = item.StockID, PPID = item.PPID, PPName = item.ProductLend.PPName, StockDate = item.StockDate, StockInDate = item.StockInDate, StockUser = item.StockUser, UsersName = item.Users.UsersName, StockState = item.StockState, StockDesc = item.StockDesc });
        }
        /// <summary>
        /// 修改采购订单信息
        /// </summary>
        /// <param name="st"></param>
        /// <param name="list"></param>
        /// <returns></returns>
        public ActionResult EditStocks(Stocks st, List<StockDetail> list)
        {
            if (StocksBLL.EdiStocks(st, list) > 0)
                return Content("edit_yes");
            else
                return Content("edit_no");
          
        }
        /// <summary>
        /// 审核订单
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult CKStocks(string id)
        {
            if (StocksBLL.CKStocks(id) > 0)
                return Content("ck_yes");
            else
                return Content("ck_no");

        }
        /// <summary>
        /// 条件查询
        /// </summary>
        /// <param name="StockID"></param>
        /// <param name="UsersName"></param>
        /// <param name="PPID"></param>
        /// <param name="StockDate"></param>
        /// <param name="StockInDate"></param>
        /// <param name="PageIndex"></param>
        /// <returns></returns>
        public ActionResult Find(string StockID,string UsersName,string PPID,string StockDate,string StockInDate,int PageIndex,int? StockState) {
            
            int count = 0;
            int PageSize = 10;
            List<Stocks> list= StocksBLL.Find(StockID, UsersName, PPID, StockDate, StockInDate, PageIndex, PageSize, out count,StockState);
            count = count % 10 == 0 ? count / 10 : count / 10 + 1;
            List<object> listed = new List<object>();
            foreach (Stocks item in list)
            {
                listed.Add(new { StockID = item.StockID, PPID = item.PPID, PPName = item.ProductLend.PPName, StockDate = item.StockDate, StockInDate = item.StockInDate, StockUser = item.StockUser, UsersName = item.Users.UsersName, StockState = item.StockState, StockDesc = item.StockDesc,MaxPageIndex=count });
            }
            if (listed.Count==0)
            {
                listed.Add(new { StockID="", MaxPageIndex = "0"});
            }
            return Json(listed);
        }
        /// <summary>
        /// 获取stocksID
        /// </summary>
        /// <returns></returns>
        public ActionResult GetStocksID()
        {
            ObjectParameter para = new ObjectParameter("DD", "");
            PSSEntities db = new PSSEntities();
            db.pro_order("Stocks", "StockID", "GG", para);
            return Content(para.Value.ToString());
        }
        /// <summary>
        /// 获取采购订单编号
        /// </summary>
        /// <returns></returns>
        public ActionResult GetStockInID() {
            ObjectParameter para = new ObjectParameter("DD", "");
            PSSEntities db = new PSSEntities();
            db.pro_order("StockInDepot", "SIDID", "CR", para);
            return Content(para.Value.ToString());
        }
        /// <summary>
        /// 保存入库订单
        /// </summary>
        /// <returns></returns>
        public ActionResult AddStockIn(StockInDepot sti,List<StockInDepotDetail> list) {
            sti.SIDUser = Convert.ToInt32(Session["uid"]);
            if (StockInDepotBLL.AddStocks(sti,list)> 0)
                return Content("add_yes");
            else
                return Content("add_no");
        }
        /// <summary>
        /// 条件查询
        /// </summary>
        /// <param name="SIDID"></param>
        /// <param name="UsersName"></param>
        /// <param name="PPName"></param>
        /// <param name="StockID"></param>
        /// <param name="SIDDate"></param>
        /// <param name="SIDDeliver"></param>
        /// <param name="SIDData"></param>
        /// <param name="PageIndex"></param>
        /// <returns></returns>
        public ActionResult FindStockIn( int PageIndex,string SIDID, string UsersName, string PPID, string DepotID, string SIDDate,int SIDData)//,
        {
            int count = 0;
            int PageSize = 10;
            List<StockInDepot> list = StockInDepotBLL.Find(SIDID,UsersName, PPID, DepotID, SIDDate,SIDData,PageIndex, PageSize, out count);
            count = count % 10 == 0 ? count / 10 : count / 10 + 1;
            List<object> listed = new List<object>();
            foreach (StockInDepot item in list)
            {
                listed.Add(new { SIDID=item.SIDID, PPID=item.PPID,PPName=item.ProductLend.PPName, DepotID=item.DepotID, DepotName=item.Depots.DepotName, StockID=item.StockID, SIDDate=item.SIDDate, SIDDeliver=item.SIDDeliver, SIDFreight=item.SIDFreight, SIDUser=item.SIDUser,UsersName=item.Users.UsersName, SIDData=item.SIDData, SIDDesc=item.SIDDesc,MaxPageIndex=count });
            }
            if (listed.Count == 0)
            {
                listed.Add(new { SIDID = "", MaxPageIndex = "0" });
            }
            return Json(listed);
        }
        /// <summary>
        /// 根据入库单信息查询入库详单
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult GetStockInDepotDetailByStockID(string id)
        {
            List<StockInDepotDetail> list = StockInDepotDetailBLL.GetAllPageBySDID(1, 1000, id);
            List<object> listed = new List<object>();
            foreach (StockInDepotDetail item in list)
            {
                listed.Add(new { SIDDID=item.SIDDID, ProID=item.ProID,ProName=item.Products.ProName, PUName=item.Products.ProductUnit.PUName, PSName=item.Products.ProductSpec.PSName, PCName=item.Products.ProductColor.PCName, SIDID =item.SIDID, SIDAmount=item.SIDAmount, SIDPrice=item.SIDPrice, SIDDesc=item.SIDDesc });
            }
            return Json(listed);
        }
        /// <summary>
        /// 删除入库单
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult DelStockInDepotDetail(string id)
        {
            if (StockInDepotDetailBLL.Delete(id) > 0)
                return Content("del_yes");
            else
                return Content("del_no");
        }
        /// <summary>
        /// 修改入库单
        /// </summary>
        /// <param name="sti"></param>
        /// <param name="list"></param>
        /// <returns></returns>
        public ActionResult EditStockIn(StockInDepot sti, List<StockInDepotDetail> list)
        {
            if (StockInDepotBLL.EdiStocks(sti, list) > 0)
                return Content("edit_yes");
            else
                return Content("edit_no");
        }
        /// <summary>
        /// 审核入库单
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult CKStockIn(string id)
        {
            if (StockInDepotBLL.CKStockIn(id,Convert.ToInt32(Session["uid"])) > 0)
                return Content("ck_yes");
            else
                return Content("ck_no");

        }
    }
}