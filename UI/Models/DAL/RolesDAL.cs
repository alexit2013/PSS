using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using UI.Models.Model;

namespace UI.Models.DAL
{
    public class RolesDAL
    {
        /// <summary>
        /// 查询所有的角色
        /// </summary>
        /// <returns></returns>
        public static List<Roles> GetAll() {
            PSSEntities db = new PSSEntities();
            return db.Roles.Select(s=>s).ToList();
        }

        public static int Add(Roles r) {
            PSSEntities db = new PSSEntities();
            db.Roles.Add(r);
            return db.SaveChanges();
        }
    }
}