
/**
 *
 * ━━━━━━神兽出没━━━━━━
 * 　　　┏┓　　　┏┓
 * 　　┏┛┻━━━┛┻┓
 * 　　┃　　　　　　　┃
 * 　　┃　　　━　　　┃
 * 　　┃　┳┛　┗┳　┃
 * 　　┃　　　　　　　┃
 * 　　┃　　　┻　　　┃
 * 　　┃　　　　　　　┃
 * 　　┗━┓　　　┏━┛Code is far away from bug with the animal protecting
 * 　　　　┃　　　┃    神兽保佑,代码无bug
 * 　　　　┃　　　┃
 * 　　　　┃　　　┗━━━┓
 * 　　　　┃　　　　　　　┣┓
 * 　　　　┃　　　　　　　┏┛
 * 　　　　┗┓┓┏━┳┓┏┛
 * 　　　　　┃┫┫　┃┫┫
 * 　　　　　┗┻┛　┗┻┛
 *
 * ━━━━━━感觉萌萌哒━━━━━━
 */

/**
 * 　　　　　　　　┏┓　　　┏┓
 * 　　　　　　　┏┛┻━━━┛┻┓
 * 　　　　　　　┃　　　　　　　┃ 　
 * 　　　　　　　┃　　　━　　　┃
 * 　　　　　　　┃　＞　　　＜　┃
 * 　　　　　　　┃　　　　　　　┃
 * 　　　　　　　┃...　⌒　...　┃
 * 　　　　　　　┃　　　　　　　┃
 * 　　　　　　　┗━┓　　　┏━┛
 * 　　　　　　　　　┃　　　┃　Code is far away from bug with the animal protecting　　　　　　　　　　
 * 　　　　　　　　　┃　　　┃   神兽保佑,代码无bug
 * 　　　　　　　　　┃　　　┃　　　　　　　　　　　
 * 　　　　　　　　　┃　　　┃  　　　　　　
 * 　　　　　　　　　┃　　　┃
 * 　　　　　　　　　┃　　　┃　　　　　　　　　　　
 * 　　　　　　　　　┃　　　┗━━━┓
 * 　　　　　　　　　┃　　　　　　　┣┓
 * 　　　　　　　　　┃　　　　　　　┏┛
 * 　　　　　　　　　┗┓┓┏━┳┓┏┛
 * 　　　　　　　　　　┃┫┫　┃┫┫
 * 　　　　　　　　　　┗┻┛　┗┻┛
 */

/**
 *　　　　　　　　┏┓　　　┏┓+ +
 *　　　　　　　┏┛┻━━━┛┻┓ + +
 *　　　　　　　┃　　　　　　　┃ 　
 *　　　　　　　┃　　　━　　　┃ ++ + + +
 *　　　　　　 ████━████ ┃+
 *　　　　　　　┃　　　　　　　┃ +
 *　　　　　　　┃　　　┻　　　┃
 *　　　　　　　┃　　　　　　　┃ + +
 *　　　　　　　┗━┓　　　┏━┛
 *　　　　　　　　　┃　　　┃　　　　　　　　　　　
 *　　　　　　　　　┃　　　┃ + + + +
 *　　　　　　　　　┃　　　┃　　　　Code is far away from bug with the animal protecting　　　　　　　
 *　　　　　　　　　┃　　　┃ + 　　　　神兽保佑,代码无bug　　
 *　　　　　　　　　┃　　　┃
 *　　　　　　　　　┃　　　┃　　+　　　　　　　　　
 *　　　　　　　　　┃　 　　┗━━━┓ + +
 *　　　　　　　　　┃ 　　　　　　　┣┓
 *　　　　　　　　　┃ 　　　　　　　┏┛
 *　　　　　　　　　┗┓┓┏━┳┓┏┛ + + + +
 *　　　　　　　　　　┃┫┫　┃┫┫
 *　　　　　　　　　　┗┻┛　┗┻┛+ + + +
 */
 

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Threading;
using UI.Models.Model;
using UI.Models.BLL;
using UI.Filter;

namespace UI.Controllers
{
    /**
     * 
     * 主页Home控制器
     * 一、登录
     * 二、修改密码
     * 三、显示主页类容
     *
     * 试试
     * */
  
    [ExceptionFilter]
    public class HomeController : Controller
    {
       /// <summary>
       /// 登录页面
       /// </summary>
       /// <returns></returns>
       [HttpGet]
       public ActionResult Login() {
           Session["pwderror"] = null;
            return View();
        }
        [LoginFilter]
        public ActionResult Index1() {
           return View();
       }
        /// <summary>
        /// 主页
        /// </summary>
        /// <returns></returns>
        [LoginFilter]
        public ActionResult Index()
        {
            return View();
        }

     
        
        /// <summary>
        /// 登录
        /// </summary>
        /// <param name="u"></param>
        /// <returns></returns>
        [HttpPost]
        [AcceptVerbs(HttpVerbs.Post)]
        [OpcrateFilter("登录")]
        public ActionResult Login(Users user)
        {
            //修改
            // db.Entry<Users>(user).State = System.Data.Entity.EntityState.Modified;
            Thread.Sleep(100);//延迟线程
            Session["pwderror"] = null;
            user = UserBLL.Login(user);
            if (user==null)  {
                //返回登陆页面
                return Content("login_no");
            }  else {
                if (!Convert.ToBoolean( user.UsersTF))
                {
                    return Content("login_tf");
                }
                else
                {
                    Session["lname"] = user.UserLoginName;
                    Session["name"] = user.UsersName;
                    Session["uid"] = user.UsersID;
                    return Content("login_yes");
                }
            }
        }
        /// <summary>
        /// 修改密码
        /// </summary>
        /// <param name="u"></param>
        /// <returns></returns>
        [OpcrateFilter("修改密码")]
        public ActionResult EditPwd(string name,string pwd,string newpwd)
        {
            Users user = new Users() {
                    UserLoginName=name,
                    UserLoginPwd=pwd
            };
            int fg = 0;
            user = UserBLL.Login(user);
            if (user!=null) {
                user.UserLoginPwd = newpwd;
                fg= UserBLL.EditPwd(user);
            }
            return Content(fg.ToString());
        }

        /// <summary>
        /// 注销登录
        /// </summary>
        /// <returns></returns>
        [OpcrateFilter("注销登陆")]
        public ActionResult Loginout()
        {
            Session["name"] = null;
            return Content("<script> parent.document.location.href='/Home/Login';</script>");
        }
        #region DefaultPage
        /// <summary>
        /// 头部
        /// </summary>
        /// <returns></returns>
        [LoginFilter]
        public ActionResult head() {
            return View();
        }
        /// <summary>
        /// 左边菜单栏
        /// </summary>
        /// <returns></returns>
        [LoginFilter]
        public ActionResult Left() {
           // ViewData["name"] = Session["name"];
            int userid = Convert.ToInt32(Session["uid"]);
            PSSEntities db = new PSSEntities();
            List<UsersRole> rlist = db.UsersRole.Where(s => s.UsersID == userid).ToList();
            List<PopedomRole> prlist = new List<PopedomRole>();
            foreach (UsersRole item in rlist)
            {
                prlist.AddRange(db.PopedomRole.Where(s => s.RoleID == item.RoleID).ToList());
            }
            Session["toolList"] = prlist;
            return View();
        }

        /// <summary>
        /// 隐藏菜单
        /// </summary>
        /// <returns></returns>
        [LoginFilter]
        public ActionResult Size() {
           return View();
       }
        /// <summary>
        /// 页脚
        /// </summary>
        /// <returns></returns>
        [LoginFilter]
        public ActionResult foot(){
            return View();
        }
        /// <summary>
        /// 框架集default页面
        /// </summary>
        /// <returns></returns>
        [LoginFilter]
        public ActionResult maindefault(){
            PSSEntities db = new PSSEntities();
            //销售未完成数
           

            List<StockDetail> list2 = db.StockDetail.Where(s => (s.SDetailDepAmount - s.SDetailAmount) >= 0).ToList();
            List<string> ids2 = new List<string>();
            foreach (StockDetail item in list2)
            {
                ids2.Add(item.StockID);
            }
            int count1 = db.Stocks.Where(s => ids2.Contains(s.StockID)).Count();
            int count2 = db.Stocks.Where(s=>s.StockState==0).Count();
            int count3 = db.StockInDepot.Where(s=>s.SIDData==0).Count();

            List<CustomerOrderDetail> list1 = db.CustomerOrderDetail.Where(s => (s.CODSale - s.CODDiscont) >= 0).ToList();
            List<string> ids1 = new List<string>();
            foreach (CustomerOrderDetail item in list1)
            {
                ids1.Add(item.COID);
            }
            int count4 = db.CustomerOrder.Where(s => ids1.Contains(s.COID)).Count();
            int count5= db.CustomerOrder.Where(s => s.COState == 0).Count();
            int count6 = db.SaleDepot.Where(s => s.SDState == 0).Count();
            int count7 = db.Devolves.Where(s=>s.DevState==0).Count();
            int count8 = db.DepotStock.Where(s => s.Products.ProMin >= s.DSAmount).Count();
            int count9 = db.DepotStock.Where(s => s.Products.ProMax <= s.DSAmount).Count();
            ViewData["c1"] = count1;
            ViewData["c2"] = count2;
            ViewData["c3"] = count3;
            ViewData["c4"] = count4;
            ViewData["c5"] = count5;
            ViewData["c6"] = count6;
            ViewData["c7"] = count7;
            ViewData["c8"] = count8 + count9;
            return View();
       }
      
        /// <summary>
        /// 404页面
        /// </summary>
        /// <returns></returns>
        public ActionResult Error404(){
            return View();
        }
        /// <summary>
        /// 500页面
        /// </summary>
        /// <returns></returns>
        public ActionResult Error500()
        {
            return View();
        }
        #endregion



        private string getstring() {
            string str = string.Empty;
            for (int i = 0; i < 80000; i++)
                str += i.ToString();
            return str;
            
        }

    }
}
