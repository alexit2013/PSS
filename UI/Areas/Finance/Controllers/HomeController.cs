using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using UI.Filter;

namespace UI.Areas.Finance.Controllers
{
    /// <summary>
    /// 财务管理
    /// </summary>
    [Filter.ExceptionFilter]
    //  [Filter.LoginFilter]
    [PopedomsFilter]
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }
        /// <summary>
        /// 应收款汇总
        /// </summary>
        /// <returns></returns>
        public ActionResult MustProceeds() {
            return View();
        }
        /// <summary>
        /// 应汇款汇总
        /// </summary>
        /// <returns></returns>
        public ActionResult MustRemittance() {
            return View();
        }
        /// <summary>
        /// 银行账务
        /// </summary>
        /// <returns></returns>
        public ActionResult BankAccounts() {
            return View();
        }
        /// <summary>
        /// 现金银行
        /// </summary>
        /// <returns></returns>
        public ActionResult BankNewMoney()
        {
            return View();
        }
        /// <summary>
        /// 存取款
        /// </summary>
        /// <returns></returns>
        public ActionResult AccessMoney() {
            return View();
        }
        /// <summary>
        /// 销售收款
        /// </summary>
        /// <returns></returns>
        public ActionResult SaleProceeds() {
            return View();
        }
        /// <summary>
        /// 采购付款
        /// </summary>
        /// <returns></returns>
        public ActionResult ProRemittance() {
            return View();
        }
        /// <summary>
        /// 收入
        /// </summary>
        /// <returns></returns>
        public ActionResult Income() {
            return View();
        }
        /// <summary>
        /// 支出
        /// </summary>
        /// <returns></returns>
        public ActionResult Expenditure() {
            return View();
        }
	}
}