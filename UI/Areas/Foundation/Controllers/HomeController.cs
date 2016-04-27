using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using UI.Models.Model;
using UI.Models.BLL;
using UI.Filter;
using System.Data.Entity.Core.Objects;

namespace UI.Areas.Foundation.Controllers
{
    [ExceptionFilter]
    public class HomeController : Controller
    {
        [LoginFilter]
        [PopedomsFilter]
        public ActionResult Index()
        {
            return View();
        }

        #region ---------------------------【商品类型】----------------------------------

        /// <summary>
        /// 商品类型页
        /// </summary>
        /// <returns></returns>
        [ViewFilter]
        public ActionResult ProductType()
        {
            List<ProductTypes> list = ProductTypesBLL.GetAll();
            ViewData["list"] = list;
            ViewData["count"] = list.Count;
            ViewData["MaxPageIndex"] = list.Count % 10 == 0 ? list.Count / 10 : list.Count / 10 + 1;
            ViewData["PageIndex"] = 1;
            ViewData["list1"] = ProductTypesBLL.GetAll(Convert.ToInt32(1));
            return View();
        }
        /// <summary>
        /// 商品类型树
        /// </summary>
        /// <returns></returns>
        public ActionResult ProductTypeTree()
        {
            List<ProductTypes> listed = ProductTypesBLL.GetAll();
            List<object> list = new List<object>();
            foreach (ProductTypes item in listed)
            {
                list.Add(new { PTID = item.PTID, PTName = item.PTName, PTParentID = item.PTParentID, PTDes = item.PTDes });
            }
            return Json(list, JsonRequestBehavior.AllowGet);
        }
        /// <summary>
        /// 翻页查询
        /// </summary>
        /// <returns></returns>
        public ActionResult ProductType1(int PageIndex)
        {
            ViewData["count"] = ProductTypesBLL.GetAll().Count;
            ViewData["PageIndex"] = PageIndex;
            List<ProductTypes> listed = ProductTypesBLL.GetAll(Convert.ToInt32(PageIndex));
            List<object> list = new List<object>();
            foreach (ProductTypes item in listed)
            {
                list.Add(new { PTID=item.PTID,PTName=item.PTName, PTParentID=item.PTParentID,PTDes=item.PTDes});
            }
            return Json(list, JsonRequestBehavior.AllowGet);
        }
        /// <summary>
        /// 添加商品类型
        /// </summary>
        /// <param name="pt"></param>
        /// <returns></returns>
        public ActionResult AddProductType(ProductTypes pt)
        {
            int count = ProductTypesBLL.AddProType(pt);
            if (count > 0)
                TempData["fg"] = "add_yes";
            else
                TempData["fg"] = "add_no";

            return RedirectToAction("ProductType");
        }
        /// <summary>
        /// 删除商品类型
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        public ActionResult DeleteType(int id)
        {
            if (ProductTypesBLL.DelProType(id) > 0)
                TempData["fg"] = "del_yes";
            else
                TempData["fg"] = "del_no";

            return RedirectToAction("ProductType");
        }
        /// <summary>
        /// 修改商品类型
        /// </summary>
        /// <param name="pt"></param>
        /// <returns></returns>
        [OpcrateFilter("修改商品类型")]
        public ActionResult EditProType(ProductTypes pt)
        {
            
            if (ProductTypesBLL.EidtProType(pt) > 0)
                TempData["fg"] = "edit_yes";
            else
                TempData["fg"] = "edit_no";

            return RedirectToAction("ProductType");
        }

        #endregion

        #region ---------------------------【仓库设置】----------------------------------
        /// <summary>
        /// 仓库设置
        /// </summary>
        /// <r1eturns></returns>
        [ViewFilter]
        public ActionResult SetWarehouse()
        {
            List<Depots> list = DepotsBLL.GetAllPage(1, 10);
            ViewData["depotList"] = list;
            ViewData["count"] = DepotsBLL.GetAll().Count;
            ViewData["PageIndex"] = 1;
            return View();
        }
        /// <summary>
        /// 仓库翻页
        /// </summary>
        /// <param name="PageIndex"></param>
        /// <returns></returns>
        public ActionResult SetWarehousePage(int PageIndex)
        {
            List<Depots> list = DepotsBLL.GetAllPage(PageIndex, 10);
            ViewData["depotList"] = list;
            ViewData["count"] = DepotsBLL.GetAll().Count;
            ViewData["PageIndex"] = PageIndex;
            return View("SetWarehouse");
        }
        /// <summary>
        /// 添加仓库
        /// </summary>
        /// <param name="dep"></param>
        /// <returns></returns>
        [OpcrateFilter("添加仓库")]
        public ActionResult AddDepots(Depots dep)
        {
            if (DepotsBLL.AddDepots(dep) > 0)
                TempData["fg"] = "add_yes";
            else
                TempData["fg"] = "add_no";

            return RedirectToAction("SetWarehouse");
        }
        /// <summary>
        /// 删除仓库
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [OpcrateFilter("删除仓库")]
        public ActionResult DeleteDepot(string id)
        {
            if (DepotsBLL.DelDepots(id) > 0)
                TempData["fg"] = "del_yes";
            else
                TempData["fg"] = "del_no";

            return RedirectToAction("SetWarehouse");
        }
        /// <summary>
        /// 修改仓库
        /// </summary>
        /// <param name="dp"></param>
        /// <returns></returns>
        [OpcrateFilter("修改仓库")]
        public ActionResult EditDepot(Depots dp)
        {
            if (DepotsBLL.EditDepot(dp) > 0)
                TempData["fg"] = "edit_yes";
            else
                TempData["fg"] = "edit_no";

            return RedirectToAction("SetWarehouse");
        }
        public ActionResult GetDPID() {
            ObjectParameter para = new ObjectParameter("DD","");
            PSSEntities db = new PSSEntities();
            db.pro_order("Depots", "DepotID", "CK", para);
            return Content(para.Value.ToString());
        }

        #endregion

        #region ---------------------------【供应商页】----------------------------------

        /// <summary>
        /// 供应商
        /// </summary>
        /// <returns></returns>
        [ViewFilter]
        public ActionResult ProductLend()
        {
            int PageSize = 10;
            ViewData["prolendlist"] = ProductLendBLL.GetAllPage(10, 1);
            int count = ProductLendBLL.GetAll().Count;
            ViewData["count"] = count;
            ViewData["MaxPageIndex"] = count % 10 == 0 ? (count / PageSize) : (count / PageSize + 1);
            ViewData["PageIndex"] = 1;
            return View();
        }
        /// <summary>
        /// 分页查询供应商信息
        /// </summary>
        /// <param name="PageIndex"></param>
        /// <returns></returns>
        public ActionResult ProductLendPage(int PageIndex)
        {
            int PageSize = 10;
            int count = ProductLendBLL.GetAll().Count;
            ViewData["count"] = count;
            ViewData["PageIndex"] = PageIndex;
            ViewData["MaxPageIndex"] = count % 10 == 0 ? (count / PageSize) : (count / PageSize + 1);
            List<ProductLend> list = ProductLendBLL.GetAllPage(10, PageIndex);
            var listed = from s in list
            select
            new {
                s.PPID,
                s.PPName,
                s.PPAddress,
                s.PPBank,
                s.PPCompany,
                s.PPDesc,
                s.PPEmail,
                s.PPFax,
                s.PPGoods,
                s.PPMan,
                s.PPTel,
                s.PPUrl
             };
            return Json(listed);
        }
        /// <summary>
        /// 添加供货商
        /// </summary>
        /// <param name="pd"></param>
        /// <returns></returns>
        [OpcrateFilter("添加供货商")]
        public ActionResult AddProductLend(ProductLend pd)
        {
            if (ProductLendBLL.Add(pd) > 0)
                TempData["fg"] = "add_yes";
            else
                TempData["fg"] = "add_no";

            return RedirectToAction("ProductLend");
        }
        /// <summary>
        /// 删除供应商信息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [OpcrateFilter("删除供应商")]
        public ActionResult DelProductLend(string id)
        {
            if (ProductLendBLL.Delete(id) > 0)
                TempData["fg"] = "del_yes";
            else
                TempData["fg"] = "del_no";

            return RedirectToAction("ProductLend");
        }
        /// <summary>
        /// 修改供应商
        /// </summary>
        /// <param name="pd"></param>
        /// <returns></returns>
        [OpcrateFilter("修改供应商")]
        public ActionResult EditProductLend(ProductLend pd)
        {
            if (ProductLendBLL.Edit(pd) > 0)
                TempData["fg"] = "edit_yes";
            else
                TempData["fg"] = "edit_no";

            return RedirectToAction("ProductLend");
        }
        /// <summary>
        /// 条件查询供货商信息
        /// </summary>
        /// <param name="type"></param>
        /// <param name="content"></param>
        /// <param name="PageIndex"></param>
        /// <returns></returns>
        public ActionResult ProLendFind(int type, string content, int PageIndex)
        {
            List<ProductLend> list = ProductLendBLL.Find(type, content, PageIndex);
            var listed = from s in list
                         select
                         new
                         {
                             s.PPID,
                             s.PPName,
                             s.PPAddress,
                             s.PPBank,
                             s.PPCompany,
                             s.PPDesc,
                             s.PPEmail,
                             s.PPFax,
                             s.PPGoods,
                             s.PPMan,
                             s.PPTel,
                             s.PPUrl
                         };
            return Json(listed, JsonRequestBehavior.AllowGet);
        }
        /// <summary>
        /// 条件查询供货商的个数
        /// </summary>
        /// <param name="type"></param>
        /// <param name="content"></param>
        /// <param name="PageIndex"></param>
        /// <returns></returns>
        public ActionResult ProLendFindCount(int type, string content, int PageIndex)
        {
            int count = ProductLendBLL.FindCount(type, content, PageIndex);
            int page = count % 10 == 0 ? count / 10 : count / 10 + 1;
            return Json(new { Count = count, Page = page });
        }

        public ActionResult GetProLendID()
        {
            ObjectParameter para = new ObjectParameter("DD", "");
            PSSEntities db = new PSSEntities();
            db.pro_order("ProductLend", "PPID", "GY", para);
            return Content(para.Value.ToString());
        }
        #endregion

        #region ---------------------------【客户资料】----------------------------------
        /// <summary>
        /// 客户资料
        /// </summary>
        /// <returns></returns>
        [ViewFilter]
        public ActionResult Customers()
        {
            int count = CustomersBLL.GetAll().Count;
            ViewData["count"] = count;
            ViewData["MaxPageIndex"] = count % 10 == 0 ? count / 10 : count / 10 + 1;
            ViewData["PageIndex"] = 1;
            List<CustomerLevel> levelList= CustomerLevelBLL.GetAll();
            ViewData["levelList"] = levelList;
            ViewData["slist"] = new SelectList(levelList, "CLID", "CLName");
            if (TempData["tabIndex"] ==null)
            {
                TempData["tabIndex"] = 1;
            }
            return View();
        }
        /// <summary>
        /// 分页查询
        /// </summary>
        /// <param name="PageIndex"></param>
        /// <returns></returns>
        public ActionResult CustomersPage(int PageIndex)
        {
            //对象中存在对象属性，不能序列化
            List<Customers> listed = CustomersBLL.GetAllPage(PageIndex, 10);
            List<object> list = new List<object>();
            foreach (Customers item in listed)
            {
                list.Add(new { CusID = item.CusID, CLID=item.CLID, CusName=item.CusName, CusCompany=item.CusCompany, CusMan=item.CusMan, CusDesc=item.CusDesc, CLName=item.CustomerLevel.CLName, CLAgio=item.CustomerLevel.CLAgio });
            }
            return Json(list,JsonRequestBehavior.AllowGet);
        }
        /// <summary>
        /// 删除客户
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult DeleteCus(string id) {
            if (CustomersBLL.Delete(id) > 0)
                TempData["fg"] = "del_yes";
            else
                TempData["fg"] = "del_no";

            TempData["tabIndex"] = 1;
            return RedirectToAction("Customers");
        }
        /// <summary>
        /// 添加客户
        /// </summary>
        /// <param name="cus"></param>
        /// <returns></returns>
        public ActionResult AddCus(Customers cus) {
            if (CustomersBLL.Add(cus) > 0)
                TempData["fg"] = "add_yes";
            else
                TempData["fg"] = "add_no";
            TempData["tabIndex"] = 1;
            return RedirectToAction("Customers");
        }
        /// <summary>
        /// 编辑客户
        /// </summary>
        /// <param name="cus"></param>
        /// <returns></returns>
        public ActionResult EditCus(Customers cus) {
            if (CustomersBLL.Edit(cus) > 0)
                TempData["fg"] = "edit_yes";
            else
                TempData["fg"] = "edit_no";

            TempData["tabIndex"] = 1;
            return RedirectToAction("Customers");
        }
        /// <summary>
        /// 添加客户等级
        /// </summary>
        /// <param name="cusl"></param>
        /// <returns></returns>
        public ActionResult AddCusLevel(CustomerLevel cusl) {
            if (CustomerLevelBLL.Add(cusl) > 0)
                TempData["fg"] = "add_yes";
            else
                TempData["fg"] = "add_no";

            TempData["tabIndex"] = 2;
            return RedirectToAction("Customers");
        }
        /// <summary>
        /// 删除客户等级 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult DeleteCusLevel(int id) {
            if (CustomerLevelBLL.Delete(id) > 0)
                TempData["fg"] = "del_yes";
            else
                TempData["fg"] = "del_no";

            TempData["tabIndex"] = 2;
            return RedirectToAction("Customers");
        }
        /// <summary>
        /// 修改客户等级
        /// </summary>
        /// <param name="cusl"></param>
        /// <returns></returns>
        public ActionResult EditCusLevel(CustomerLevel cusl) {
            if(CustomerLevelBLL.Edit(cusl)> 0)
                TempData["fg"] = "edit_yes";
            else
                TempData["fg"] = "edit_no";

            TempData["tabIndex"] = 2;
            return RedirectToAction("Customers");
        }
        public ActionResult GetCusID()
        {
            ObjectParameter para = new ObjectParameter("DD", "");
            PSSEntities db = new PSSEntities();
            db.pro_order("Customers", "CusID", "KH", para);
            return Content(para.Value.ToString());
        }

        #endregion

        #region ---------------------------【员工设置】----------------------------------
        /// <summary>
        /// 员工设置
        /// </summary>
        /// <returns></returns>
        public ActionResult SetEmp()
        {
            return View();
        }
        #endregion

        #region ---------------------------【银行账户】----------------------------------
        /// <summary>
        /// 银行账户
        /// </summary>
        /// <returns></returns>
        public ActionResult BankAccount()
        {
            return View();
        }
        #endregion

        #region ---------------------------【数据导入】----------------------------------
        /// <summary>
        /// 数据导入
        /// </summary>
        /// <returns></returns>
        public ActionResult ImportData()
        {
            return View();
        }
        #endregion

        #region ---------------------------【商品资料】----------------------------------

        /// <summary>
        /// 商品资料
        /// </summary>
        /// <returns></returns>
        public ActionResult Products()
        {
            int count = ProductsBLL.GetAll().Count;
            ViewData["PageIndex"] = 1;
            ViewData["count"] = count;
            ViewData["MaxPageIndex"] = count % 10 == 0 ? count / 10 : count / 10 + 1;
            ViewData["PTList"] = ProductTypesBLL.GetAll();//所有商品类别
            ViewData["PUList"] = ProductUnitBLL.GetAll();//所有商品单位
            ViewData["PCList"] = ProductColorBLL.GetAll();//所有商品颜色
            ViewData["PSList"] = ProductSpecBLL.GetAll();//所有商品规格
            ViewData["DPList"] = DepotsBLL.GetAll();//所有仓库信息
            return View();
        }
       /// <summary>
       /// 根据商品ID查询商品
       /// </summary>
       /// <param name="id"></param>
       /// <returns></returns>
        public ActionResult ProductsGetByID(int id) {
            Products item = ProductsBLL.GetByID(id);
            object pro = new { ProID = item.ProID, PTID = item.PTID, PUID = item.PUID, PCID = item.PCID, PSID = item.PSID, ProName = item.ProName, ProJP = item.ProJP, ProTM = item.ProTM, ProWorkShop = item.ProWorkShop, ProMax = item.ProMax, ProMin = item.ProMin, ProInPrice = item.ProInPrice, ProPrice = item.ProPrice, ProDesc = item.ProDesc, PTName = item.ProductTypes.PTName, PUName = item.ProductUnit.PUName, PCName = item.ProductColor.PCName, PSName = item.ProductSpec.PSName, DepotID = item.Depots.DepotID, DepotName = item.Depots.DepotName };
            if (pro!=null)
            {
                return Json(pro);
            }else
            {
                return Content("notfind");
            }
        }
        /// <summary>
        /// 根据商品类型查询这个商品类型下面的所有商品
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult ProductsGetByPTID(int id,int PageIndex)
        {
            List<Products> listed = ProductsBLL.GetAllByPTID(id,PageIndex,10);
            List<object> list = new List<object>();
            foreach (Products item in listed) {
                 list.Add( new { ProID = item.ProID, PTID = item.PTID, PUID = item.PUID, PCID = item.PCID, PSID = item.PSID, ProName = item.ProName, ProJP = item.ProJP, ProTM = item.ProTM, ProWorkShop = item.ProWorkShop, ProMax = item.ProMax, ProMin = item.ProMin, ProInPrice = item.ProInPrice, ProPrice = item.ProPrice, ProDesc = item.ProDesc, PTName = item.ProductTypes.PTName, PUName = item.ProductUnit.PUName, PCName = item.ProductColor.PCName, PSName = item.ProductSpec.PSName, DepotID = item.Depots.DepotID, DepotName = item.Depots.DepotName });
            }
             return Json(list);
        }
        /// <summary>
        /// 根据商品类别ID查询商品的个数
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult ProductsGetByPTIDCount(int id)
        {
            int count= ProductsBLL.GetAllByPTIDCount(id);
            int page = count % 10 == 0 ? count / 10 : count / 10 + 1;
            return Json(new {Count=count,Page=page });
        }
        /// <summary>
        /// 分页查询商品信息
        /// </summary>
        /// <param name="PageIndex"></param>
        /// <returns></returns>
        public ActionResult ProductsPage(int PageIndex) {
            List<Products> listed = ProductsBLL.GetAllPage(PageIndex,10);
            List<object> list = new List<object>();
            foreach (Products item in listed)
            {
                list.Add(new { ProID=item.ProID, PTID=item.PTID, PUID=item.PUID, PCID=item.PCID, PSID=item.PSID, ProName=item.ProName, ProJP=item.ProJP, ProTM=item.ProTM, ProWorkShop=item.ProWorkShop, ProMax=item.ProMax, ProMin=item.ProMin, ProInPrice=item.ProInPrice, ProPrice=item.ProPrice, ProDesc=item.ProDesc,PTName=item.ProductTypes.PTName,PUName=item.ProductUnit.PUName,PCName=item.ProductColor.PCName,PSName=item.ProductSpec.PSName, DepotID=item.Depots.DepotID, DepotName=item.Depots.DepotName });
            }
            return Json(list);
        }
        /// <summary>
        /// 添加商品
        /// </summary>
        /// <param name="pro"></param>
        /// <returns></returns>
        [OpcrateFilter("添加商品操作")]
        [PopedomsFilter]
        public ActionResult AddProducts(Products pro) {
            if (ProductsBLL.Add(pro) > 0)
                return Content("add_yes");
            else
                return Content("add_no");
        }
        /// <summary>
        /// 修改商品
        /// </summary>
        /// <param name="pro"></param>
        /// <returns></returns>
        [OpcrateFilter("修改商品")]
        public ActionResult EditProducts(Products pro)
        {
            if (ProductsBLL.Edit(pro) > 0)
                return Content("edit_yes");
            else
                return Content("edit_no");
        }
        /// <summary>
        /// 删除商品
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [OpcrateFilter("删除商品")]
        public ActionResult DelProducts(int id)
        {
            if (ProductsBLL.Delete(id) > 0)
                return Content("del_yes");
            else
                return Content("del_no");
        }
        /// <summary>
        /// 商品单位
        /// </summary>
        /// <returns></returns>
        public ActionResult ProductUnitPage() {
            List<ProductUnit> listed = ProductUnitBLL.GetAll();
            List<object> list = new List<object>();
            foreach (ProductUnit item in listed)
            {
                list.Add(new  { PUID= item.PUID, PUName=item.PUName });
            }
            return Json(list);
        }
        /// <summary>
        /// 添加商品单位
        /// </summary>
        /// <param name="prou"></param>
        /// <returns></returns>
        [OpcrateFilter("添加商品单位")]
        public ActionResult AddProcutUnit(ProductUnit prou) {
            if (ProductUnitBLL.Add(prou) > 0)
                return Content("add_yes");
            else
                return Content("add_no");
        }
        /// <summary>
        /// 修改商品单位
        /// </summary>
        /// <param name="prou"></param>
        /// <returns></returns>
        [OpcrateFilter("修改商品单位")]
        public ActionResult EditProcutUnit(ProductUnit prou) {
            if (ProductUnitBLL.Edit(prou) > 0)
                return Content("edit_yes");
            else
                return Content("edit_no");
        }
        /// <summary>
        /// 商品单位删除
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [OpcrateFilter("删除商品单位")]
        public ActionResult DelProductUnit(int id) {
            if (ProductUnitBLL.Delete(id) > 0)
                return Content("del_yes");
            else
                return Content("del_no");
        }
        /// <summary>
        /// 商品规格
        /// </summary>
        /// <returns></returns>
        public ActionResult ProductSpecPage() {
            List<ProductSpec> listed = ProductSpecBLL.GetAll();
            List<object> list = new List<object>();
            foreach (ProductSpec item in listed)
            {
                list.Add(new { PSID = item.PSID, PSName = item.PSName });
            }
            return Json(list);
        }
        /// <summary>
        /// 添加商品规格
        /// </summary>
        /// <param name="prou"></param>
        /// <returns></returns>
        [OpcrateFilter("添加商品规格")]
        public ActionResult AddProductSpec(ProductSpec prou)
        {
            if (ProductSpecBLL.Add(prou) > 0)
                return Content("add_yes");
            else
                return Content("add_no");
        }
        /// <summary>
        /// 修改商品规格
        /// </summary>
        /// <param name="prou"></param>
        /// <returns></returns>
        [OpcrateFilter("修改商品规格")]
        public ActionResult EditProductSpec(ProductSpec prou)
        {
            if (ProductSpecBLL.Edit(prou) > 0)
                return Content("edit_yes");
            else
                return Content("edit_no");
        }
        /// <summary>
        /// 商品单位规格
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult DelProductSpec(int id)
        {
            if (ProductSpecBLL.Delete(id) > 0)
                return Content("del_yes");
            else
                return Content("del_no");
        }
        /// <summary>
        /// 商品颜色
        /// </summary>
        /// <returns></returns>
       
        public ActionResult ProductColorPage()
        {
            List<ProductColor> listed = ProductColorBLL.GetAll();
            List<object> list = new List<object>();
            foreach (ProductColor item in listed)
            {
                list.Add(new { PCID = item.PCID, PCName = item.PCName });
            }
            return Json(list);
        }
        /// <summary>
        /// 添加商品颜色
        /// </summary>
        /// <param name="prou"></param>
        /// <returns></returns>
        [OpcrateFilter("添加商品颜色")]
        public ActionResult AddProductColor(ProductColor prou)
        {
            if (ProductColorBLL.Add(prou) > 0)
                return Content("add_yes");
            else
                return Content("add_no");
        }
        /// <summary>
        /// 修改商品颜色
        /// </summary>
        /// <param name="prou"></param>
        /// <returns></returns>
        [OpcrateFilter("修改商品颜色")]
        public ActionResult EditProductColor(ProductColor prou)
        {
            if (ProductColorBLL.Edit(prou) > 0)
                return Content("edit_yes");
            else
                return Content("edit_no");
        }
        /// <summary>
        /// 删除商品颜色
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [OpcrateFilter("删除商品颜色")]
        public ActionResult DelProductColor(int id)
        {
            if (ProductColorBLL.Delete(id) > 0)
                return Content("del_yes");
            else
                return Content("del_no");
        }

        #endregion
    }
}