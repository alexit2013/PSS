using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace UI.Filter
{
   public  class ViewFilter:FilterAttribute,IResultFilter
    {

        /// <summary>
        /// 视图加载前
        /// </summary>
        /// <param name="filterContext"></param>
        public void OnResultExecuting(ResultExecutingContext filterContext)
        {
           // filterContext.HttpContext.Response.Write("<script> </script>");
        }

       /// <summary>
       /// 视图加载完成后
       /// </summary>
       /// <param name="filterContext"></param>
        public void OnResultExecuted(ResultExecutedContext filterContext)
        {
           filterContext.HttpContext.Response.Write("<script>new tmm_loeading('tmm-loeading','加载中...');$('#tmm-loeading').modal({closeViaDimmer:0}); </script>");
        }
     
    }
}