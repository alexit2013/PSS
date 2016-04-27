using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Text;


namespace UI.Option.HtmlPage
{
    public static class HtmlPage1
    {
        /// <summary>
        /// 分页标签
        /// </summary>
        /// <param name="htmlHelper"></param>
        /// <param name="ControllerName">控制器名字</param>
        /// <param name="ActionName">Action名字</param>
        /// <param name="RowCount">总数据条数</param>
        /// <param name="SelWhere">查询条件</param>
        /// <param name="PageSize">页大小</param>
        /// <param name="PageIndex">当前页</param>
        /// <param name="ClassName">样式</param>
        /// <returns></returns>
        public static MvcHtmlString GroupPage1(this HtmlHelper htmlHelper, string ControllerName, string ActionName, object RowCount, string SelWhere, object PageSize, object PageIndex, string ClassName)
        {
            int PageCount = Convert.ToInt32(RowCount) % Convert.ToInt32(PageSize) == 0 ? Convert.ToInt32(RowCount) / Convert.ToInt32(PageSize) : Convert.ToInt32(RowCount) / Convert.ToInt32(PageSize) + 1;
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("<ul data-am-widget='pagination' class='am-pagination am-pagination-default'> ");
            sb.AppendLine("<li class='am-pagination-first '><a href='#' onclick='shouye1()'>第一页</a></li>");
            sb.AppendLine("<li class='am-pagination-first '> <a href='#'  onclick='shangyiye1()' >上一页</a></li>");
            sb.AppendLine("<li class='am-pagination-select'> <select  id='SelPage1' onchange='change1(this)' /> ");
            for (int i = 1; i <= PageCount; i++)
            {
                sb.AppendLine("<option value = '" + i + "' class=''" + (Convert.ToInt32(PageIndex) == i ? "selected='selected'" : "") + ">" + i + " / " + PageCount + "</option>");
            }
            sb.AppendLine("</select> </li>");
            sb.AppendLine("<li class='am-pagination-first '><a href='#' onclick='xiayiye1()' >下一页</a></li>");
            sb.AppendLine("<li class='am-pagination-first '><a href='#' onclick='weiye1()'  >最末页</a></li></ul>");
            //sb.AppendLine("<br/>");
            //sb.AppendLine(" 共有数据"+RowCount+"条，共有"+PageCount+"页，当前第"+PageIndex+"页,每"+PageSize+"页条数据");

            return new MvcHtmlString(sb.ToString());
        }
    }
}