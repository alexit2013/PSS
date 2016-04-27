using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using UI.Models.DAL;
using Model;
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
    }
}