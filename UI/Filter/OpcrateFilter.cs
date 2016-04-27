using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using UI.Models.Model;

namespace UI.Filter
{
    /// <summary>
    /// 操作日志记录
    /// </summary>
    public class OpcrateFilter : FilterAttribute,IActionFilter
    {
        public OpcrateFilter(string OpcrateName) {
            this.OpcrateName = OpcrateName;
        }

        public string OpcrateName;
        public void OnActionExecuted(ActionExecutedContext filterContext)
        {

            PSSEntities db = new PSSEntities();
            //写入日志
            RouteValueDictionary rvd = filterContext.RouteData.Values;
            string area = rvd["area"] as string;//域名
            string action = rvd["action"] as string;//action名字
            string controller = rvd["controller"] as string;//控制器名字
            string username = filterContext.HttpContext.Session["name"] as string;
            if (area == null)
            {
                area = "NoArea";
            }
            if (username == null)
            {
                username = "NoLogin";
            }
            try
            {
                SystemLog sl = new SystemLog();
                sl.time = DateTime.Now;
                string value = "用户：" + username + "进行" + OpcrateName + "操作!";
                sl.value = value;
                sl.islog = 1;
                db.SystemLog.Add(sl);
                db.SaveChanges();
            }
            catch (Exception ex)
            {
                filterContext.HttpContext.Response.Write(ex.Message);
            }
        }
        /// <summary>
        /// 在进入Action前
        /// </summary>
        /// <param name="filterContext"></param>
        public void OnActionExecuting(ActionExecutingContext filterContext)
        {
        }
    }
}