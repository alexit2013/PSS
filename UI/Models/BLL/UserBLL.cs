using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using UI.Models.DAL;
using UI.Models.Model;

namespace UI.Models.BLL
{
    public class UserBLL
    { 
        /// <summary>
        /// 登陆
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public static Users Login(Users user) {
            return   UsersDAL.Login(user);
        }
        /// <summary>
        /// 修改密码
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public static int EditPwd(Users user)
        {
            return UsersDAL.EditPwd(user);
        }
        /// <summary>
        /// 条件查询
        /// </summary>
        /// <param name="UsersName">用户名</param>
        /// <param name="UserLoginName">登陆名</param>
        /// <param name="RoleName">角色名</param>
        /// <param name="PageIndex">当前页</param>
        /// <param name="PageSize">页大小</param>
        /// <param name="count">总条数</param>
        /// <returns></returns>
        public static List<Users> Find(string UsersName, string UserLoginName, string RoleName, int PageIndex, int PageSize, int UsersTF, out int  count)
        {
            return UsersDAL.Find(UsersName,UserLoginName,RoleName,PageIndex,PageSize,UsersTF, out count);
        }
        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="u"></param>
        /// <returns></returns>
        public static int Add(Users u)
        {
            return UsersDAL.Add(u);
        }
        /// <summary>
        /// 修改用户信息
        /// </summary>
        /// <param name="u"></param>
        /// <returns></returns>
        public static int Edit(Users u)
        {
            return UsersDAL.Edit(u);
        }
        /// <summary>
        /// 查询用户拥有角色
        /// </summary>
        /// <param name="id">用户ID</param>
        /// <returns></returns>
        public static List<string> GetRole(int id)
        {
            return UsersDAL.GetRole(id);
        }

        public static List<int?> GetRoles(int id)
        {
            return UsersDAL.GetRoles(id);
        }

        public static int EditUserRole(int userid, List<int?> list)
        {
            return UsersDAL.EditUserRole(userid,list);
        }
    }
}