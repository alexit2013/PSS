using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Model;

namespace UI.Models.DAL
{
    public class UsersDAL
    {
        /// <summary>
        /// 登陆
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public static Users Login(Users user) {
            PSSEntities db = new PSSEntities();
            
            var lq = db.Users.FirstOrDefault(u => u.UserLoginName == user.UserLoginName && u.UserLoginPwd == user.UserLoginPwd);
            return lq as Users;
        }
        /// <summary>
        /// 修改密码
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public static int EditPwd(Users user)
        {
            PSSEntities db = new PSSEntities();
            db.Entry<Users>(user).State = System.Data.Entity.EntityState.Modified;
            return db.SaveChanges();
        }

    }
}