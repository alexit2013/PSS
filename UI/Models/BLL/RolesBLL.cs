using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using UI.Models.Model;
using UI.Models.DAL;

namespace UI.Models.BLL
{
    public class RolesBLL
    {
        /// <summary>
        /// 查询所有的角色
        /// </summary>
        /// <returns></returns>
        public static List<Roles> GetAll()
        {
            return RolesDAL.GetAll();
        }

        public static int Add(Roles r)
        {
            return RolesDAL.Add(r);
        }
    }
}