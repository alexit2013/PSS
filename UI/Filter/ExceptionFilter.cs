using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using System.IO;
using UI.Models.Model;

namespace UI.Filter
{
    /// <summary>
    ///系统错误日志
    /// </summary>
    public class ExceptionFilter:ActionFilterAttribute,IExceptionFilter
    {
        public void OnException(ExceptionContext filterContext)
        {
            PSSEntities db = new PSSEntities();
            //自定义异常处理
            filterContext.ExceptionHandled = true;
            //获取错误消息
            string messge = filterContext.Exception.Message;
            //获取操作员信息
           RouteValueDictionary rvd= filterContext.RouteData.Values;
           string area = rvd["area"] as string;//域名
           
           string action = rvd["action"] as string;//action名字
           string controller = rvd["controller"] as string;//控制器名字
           string username = filterContext.HttpContext.Session["name"]as string;
            if (area==null)
            {
                area = "NoArea";
            }
            if (username==null)
            {
                username = "NoLogin";
            }
            //FileStream fs = new FileStream("F:\\error.txt", FileMode.Append);
            // StreamWriter sw=new StreamWriter(fs);
            // sw.WriteLine("Exception----------------------------------------------------------");
            // sw.WriteLine(string.Format("{0}：用户：{1}对{2}域中的{3}控制器的{4}方法进行了操作",DateTime.Now,username,area,controller,action));
            // sw.WriteLine(string.Format("错误消息：{0}",messge));
            // sw.WriteLine("");
            // sw.Close();
            // fs.Close();
            try
            {
                SystemLog sl = new SystemLog();
                sl.time = DateTime.Now;
                string value = "用户：" + username + "对" + area + "域中的" + controller + "控制器的" + action + "方法进行了操作!错误消息：" + messge;
                sl.value = value;
                sl.islog = 0;
                db.SystemLog.Add(sl);
                db.SaveChanges();
            }
            catch (Exception ex)
            {
                filterContext.HttpContext.Response.Write(ex.Message);
            }
          

            filterContext.HttpContext.Response.Redirect("/Home/Error500");
            //filterContext.HttpContext.Response.Write("<script>location.href='/Home/Error500'</script>");
        }
    }
}