using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using UI.Models.Model;

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
        public static List<Users> Find(string UsersName,string UserLoginName,string RoleName,int PageIndex,int PageSize,int UsersTF,out int  count) {
            PSSEntities db = new PSSEntities();
            var list = from u in db.Users select u;
            if (UsersName!=null&& UsersName.Trim().Length>0)
            {
                list = list.Where(s=>s.UsersName.Contains(UsersName));
            }
            if (UserLoginName != null && UserLoginName.Trim().Length > 0)
            {
                list = list.Where(s => s.UserLoginName.Contains(UserLoginName));
            }
            if (UsersTF != -1 )
            {
                list = list.Where(s=>s.UsersTF==(UsersTF==1));
            }
            if (RoleName != null && RoleName.Trim().Length > 0)
            {
                //根据角色名称模糊查询用户
                List<UsersRole> urs = db.UsersRole.Where(s => s.Roles.RoleName.Contains(RoleName)).ToList();
                List<int?> uids = new List<int?>();
                foreach (UsersRole item in urs)
                {
                    uids.Add(item.UsersID);
                }
                list = list.Where(s => uids.Contains(s.UsersID));
            }
            count = list.Count();
            return list.OrderBy(s=>s.UsersID).Skip((PageIndex-1)*PageSize).Take(PageSize).ToList();
        }
        internal static List<int?> GetRoles(int id)
        {
            List<int?> listname = new List<int?>();
            PSSEntities db = new PSSEntities();
            List<UsersRole> list = db.UsersRole.Where(s => s.UsersID == id).ToList();
            foreach (UsersRole item in list)
            {
                listname.Add(item.RoleID);
            }
            return listname;
        }

        /// <summary>
        /// 添加用户
        /// </summary>
        /// <param name="u">用户对象</param>
        /// <returns></returns>
        public static int Add(Users u)
        {
            PSSEntities db = new PSSEntities();
            db.Users.Add(u);
            return db.SaveChanges();
        }
        /// <summary>
        /// 修改用户信息
        /// </summary>
        /// <param name="u"></param>
        /// <returns></returns>
        public static int Edit(Users u)
        {
            PSSEntities db = new PSSEntities();
            db.Entry<Users>(u).State = System.Data.Entity.EntityState.Modified;
            return db.SaveChanges();
        }
        /// <summary>
        /// 查询用户拥有角色
        /// </summary>
        /// <param name="id">用户ID</param>
        /// <returns></returns>
        public static List<string> GetRole(int id) {
            List<string> listname = new List<string>();
            PSSEntities db = new PSSEntities();
            List<UsersRole> list = db.UsersRole.Where(s => s.UsersID == id).ToList();
            foreach (UsersRole item in list)
            {
                listname.Add(item.Roles.RoleName);
            }
            return listname;
        }
        /// <summary>
        /// 修改用户角色
        /// </summary>
        /// <returns></returns>
        public static int EditUserRole(int userid,List<int?> list) {
            PSSEntities db = new PSSEntities();
            int fs = 1;
            using (var tx=db.Database.BeginTransaction())
            {
                try
                {
                    db.UsersRole.RemoveRange(db.UsersRole.Where(s=>s.UsersID==userid));
                    for (int i = 0; i < list.Count; i++)
                    {
                        db.UsersRole.Add(new UsersRole() { UsersID=userid,RoleID=list[i]});
                    }
                    db.SaveChanges();
                    tx.Commit();
                }
                catch (Exception ex)
                {
                    fs = 0;
                    tx.Rollback();
                    throw new Exception(ex.Message);
                }
            }
            return fs;
        }
    }
}