using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using UI.Models.Model;

namespace UI.Filter
{
    public class PopedomsFilter : FilterAttribute, IAuthorizationFilter
    {
        public void OnAuthorization(AuthorizationContext filterContext)
        {
            #region 基本操作页面路径
            List<string> BaseUrl = new List<string>();
            BaseUrl.Add("/home/error404");
            BaseUrl.Add("/home/error500");
            BaseUrl.Add("/home/foot");
            BaseUrl.Add("/home/head");
            BaseUrl.Add("/home/index");
            BaseUrl.Add("/home/index1");
            BaseUrl.Add("/home/left");
            BaseUrl.Add("/home/login");
            BaseUrl.Add("/home/maindefault");
            BaseUrl.Add("/home/size");
            BaseUrl.Add("/home/loginout");
            BaseUrl.Add("/home/editpwd");
            #endregion
            string RequstURL = filterContext.HttpContext.Request.RawUrl.Trim();
            if (RequstURL.Contains("?"))
            {
                RequstURL = RequstURL.Substring(0,RequstURL.IndexOf("?"));
            }
            int userid = Convert.ToInt32(filterContext.HttpContext.Session["uid"]);
            if(!BaseUrl.Contains(RequstURL.ToLower()))
            {
                if (!ExistsPopedoms(RequstURL, userid))
                {
                    //filterContext
                    //filterContext.HttpContext.Response.Write("<script>alert('权限不足！操作失败！');</script>");
                    RouteValueDictionary rvd = new RouteValueDictionary();
                    rvd["Conterller"] = "Home";
                    rvd["Action"] = "Error500";
                    filterContext.Result = new RedirectToRouteResult(rvd);
                    return;
                }

            }
        }

        /// <summary>
        /// 权限验证
        /// </summary>
        /// <param name="RequstURL">请求路径</param>
        /// <param name="userid">用户ID</param>
        /// <returns></returns>
        private static bool ExistsPopedoms(string RequstURL, int userid)
        {
            bool fs = false;
            PSSEntities db = new PSSEntities();
            List<UsersRole> rlist = db.UsersRole.Where(s => s.UsersID == userid).ToList();
            List<PopedomRole> prlist = new List<PopedomRole>();
            foreach (UsersRole item in rlist)
            {
                prlist.AddRange(db.PopedomRole.Where(s => s.RoleID == item.RoleID).ToList());
            }
            foreach (PopedomRole item in prlist)
            {
                if (item.Popedoms.PopValue.ToLower() == RequstURL.ToLower())
                {
                    fs = true;
                    break;
                }
            }
            return fs;
        }
    }
}