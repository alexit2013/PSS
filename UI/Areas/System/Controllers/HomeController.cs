using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using UI.Filter;
using UI.Models.BLL;
using UI.Models.Model;

namespace UI.Areas.System.Controllers
{
    public class HomeController : Controller
    {
        [LoginFilter]
        [PopedomsFilter]
        public ActionResult Index()
        {
            return View();
        }
        /// <summary>
        /// 操作员
        /// </summary>
        /// <returns></returns>
         public ActionResult Admin() {
             return View();
         }
        /// <summary>
        /// 修改密码
        /// </summary>
        /// <returns></returns>
         public ActionResult SetAdminPWD() {
             return View();
         }
        /// <summary>
        /// 数据备份
        /// </summary>
        /// <returns></returns>
         public ActionResult ReData() {
             return View();
         }
        /// <summary>
        /// 打印设置
        /// </summary>
        /// <returns></returns>
         public ActionResult SetPrint() {
             return View();
         }
        /// <summary>
        /// 系统设置
        /// </summary>
        /// <returns></returns>
         public ActionResult SetSystem() {
             return View();
         }
        /// <summary>
        /// 系统日志
        /// </summary>
        /// <returns></returns>
         public ActionResult SystemLog() {
            PSSEntities db = new PSSEntities();
            List<SystemLog> list= db.SystemLog.Select(s => s).OrderByDescending(s=>s.time).ToList();
            ViewData["sllist"] = list;
             return View();
         }
        /// <summary>
        /// 根据时间查询系统日志
        /// </summary>
        /// <param name="date_start"></param>
        /// <param name="date_end"></param>
        /// <returns></returns>
        public ActionResult GetSysLog(string date_start,string date_end) {
            PSSEntities db = new PSSEntities();
            var list = from c in db.SystemLog select c;
            list = list.OrderByDescending(s=>s.time);
            if (date_start!=null&&date_start.Trim().Length>0)
            {
                DateTime dt = Convert.ToDateTime(date_start);
                list = list.Where(s => s.time >= dt);
            }
            if (date_end != null && date_end.Trim().Length > 0)
            {
                DateTime dt = Convert.ToDateTime(date_end);
                list = list.Where(s => s.time <= dt);
            }
            ViewData["sllist"] = list.ToList();
            return View("SystemLog");
        }
        /// <summary>
        /// 权限管理
        /// </summary>
        /// <returns></returns>
        public ActionResult SetPopedoms()
        {
            return View();
        }
        /// <summary>
        /// 数据备份
        /// </summary>
        /// <returns></returns>
        public ActionResult SaveDataBase() {
            try
            {
                SystemBLL.SaveDataBase();
                return Content("yes");
            }
            catch (Exception ex)
            {
                return Content(ex.Message);
            }
        }
        /// <summary>
        /// 数据还原
        /// </summary>
        /// <returns></returns>
        public ActionResult RestoreDataBase()
        {
            try
            {
                SystemBLL.RestoreDataBase();
                return Content("yes");
            }
            catch (Exception ex)
            {
                return Content(ex.Message);
            }
        }


    }
}