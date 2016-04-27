using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;


namespace UI.Filter
{
    /// <summary>
    /// 登录过滤器
    /// </summary>
    public class LoginFilter:FilterAttribute,IAuthorizationFilter
    {

        public LoginFilter() { }
        /// <summary>
        /// 重载构造
        /// </summary>
        /// <param name="opcratename"></param>
        public LoginFilter(string opcratename) {
            this.OpcrateName = opcratename;
        }

        public string OpcrateName;
        public void OnAuthorization(AuthorizationContext filterContext)
        {
            if (filterContext.HttpContext.Session["name"]==null)
            {
                //string url=  filterContext.HttpContext.Request.Path.Trim();
                //设置 路由向导
                RouteValueDictionary rvd = new RouteValueDictionary();
                rvd["Conterller"] = "Home";
                rvd["Action"] = "Login";
               // filterContext.HttpContext.Response.Write("<script>alert('权限不足！！！');</script>");
                filterContext.Result = new RedirectToRouteResult(rvd);
            }
          
        }
      
    }
}