using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using UI.Models.Model;
using UI.Models.BLL;

namespace UI.Areas.System.Controllers
{
    public class UsersController : Controller
    {
        /// <summary>
        /// 条件查询用户
        /// </summary>
        /// <param name="UsersName">用户名称</param>
        /// <param name="UserLoginName">登陆名</param>
        /// <param name="RoleName">角色名称</param>
        /// <param name="PageIndex">当前页</param>
        /// <returns></returns>
        public ActionResult Find(string UsersName, string UserLoginName, string RoleName, int PageIndex,int UsersTF)
        {
            int count = 0;
            List<Users> list = UserBLL.Find(UsersName, UserLoginName, RoleName, PageIndex, 10,UsersTF, out count);
            count = count % 10 == 0 ? count / 10 : count / 10 + 1;
            if (list.Count>0)
            {
                var listed = from s in list
                             select new
                             {
                                 s.UsersID,
                                 s.UsersName,
                                 s.UserLoginName,
                                 s.UserLoginPwd,
                                 s.UsersTF,
                                 MaxPageIndex = count
                             };
                return Json(listed);
            }
            else
            {
                List<object> listed = new List<object>();
                listed.Add(new { UsersID="",MaxPageIndex=0 });
                return Json(listed);
            }
        }
        /// <summary>
        /// 添加用户
        /// </summary>
        /// <param name="u"></param>
        /// <returns></returns>
        public ActionResult Add(Users u) {
            try
            {
                if (UserBLL.Add(u) > 0)
                    return Content("add_yes");
                else
                    return Content("add_no");
            }
            catch (Exception ex)
            {
                return Content(ex.Message);
            }
        }
        /// <summary>
        /// 修改用户
        /// </summary>
        /// <param name="u"></param>
        /// <returns></returns>
        public ActionResult Edit(Users u)
        {
            try
            {
                if (UserBLL.Edit(u) > 0)
                    return Content("edit_yes");
                else
                    return Content("edit_no");
            }
            catch (Exception ex)
            {
                return Content(ex.Message);
            }
        }
        /// <summary>
        /// 根据用户ID查询
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult GetRole(int id) {
            List<string> list = UserBLL.GetRole(id);
            return Json(list);
        }
        /// <summary>
        /// 根据用户ID查询用户角色 ID
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult GetRoles(int id)
        {
            List<int?> list = UserBLL.GetRoles(id);
            return Json(list);
        }
        /// <summary>
        /// 查询所有的角色
        /// </summary>
        /// <returns></returns>
        public ActionResult GetRoleAll() {
            List<Roles> list = RolesBLL.GetAll();
            var listed = from s in list select new {
                    s.RoleID,
                    s.RoleName,
                    s.RoleDesc
            };
            return Json(listed);
        }
        /// <summary>
        /// 添加角色
        /// </summary>
        /// <param name="r"></param>
        /// <returns></returns>
        public ActionResult AddRole(Roles r) {
            try
            {
                if (RolesBLL.Add(r) > 0)
                    return Content("add_yes");
                else
                    return Content("add_no");
            }
            catch (Exception ex)
            {
                return Content(ex.Message);
            }
        }
        /// <summary>
        /// 修改用户角色
        /// </summary>
        /// <returns></returns>
        public ActionResult EditUserRoles(int userid,List<int?> roles)
        {
            try
            {
                if (UserBLL.EditUserRole(userid, roles) > 0)
                    return Content("edit_yes");
                else
                    return Content("edit_no");
            }
            catch (Exception ex)
            {
                return Content(ex.Message);
            }
           
        }
        /// <summary>
        /// 获取所有权限
        /// </summary>
        /// <returns></returns>
        public ActionResult GetPopedomsAll() {
            List<Popedoms> list = PopedomRoleBLL.GetPopedomsAll();
            var listed = from s in list select new {
                s.PopID,
                s.PopParentID,
                s.PopName,
                s.PopDesc,
                s.PopIsMenu,
            };
            return Json(listed);
        }
        /// <summary>
        /// 根据角色ID查询权限
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult GetPopedomsByRoleID(int id) {
            List<Popedoms> list = PopedomRoleBLL.GetPopedomsByRoleID(id);
            var listed = from s in list
                         select new
                         {
                             s.PopID,
                             s.PopParentID,
                             s.PopName,
                             s.PopDesc,
                             s.PopIsMenu,
                         };
            return Json(listed);
        }
        public ActionResult GetPopedomsByRoleIDStr(int id)
        {
            List<Popedoms> list = PopedomRoleBLL.GetPopedomsByRoleID(id);
            var listed = from s in list
                         select new
                         {
                             s.PopName
                         };
            return Json(listed);
        }
        public ActionResult EditRolePopedoms(int id,List<int?> list) {
            try
            {
                if (PopedomRoleBLL.EditRolePopedoms(id,list) > 0)
                    return Content("edit_yes");
                else
                    return Content("edit_no");
            }
            catch (Exception ex)
            {
                return Content(ex.Message);
            }
        }
    }
}