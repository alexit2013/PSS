using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using UI.Models.Model;

namespace UI.Models.DAL
{
    public class PopedomRoleDAL
    {
        /// <summary>
        /// 查询所有权限
        /// </summary>
        /// <returns></returns>
        public static List<Popedoms> GetPopedomsAll() {
            PSSEntities db = new PSSEntities();
            return db.Popedoms.Select(s=>s).ToList();
        }
        public static List<Popedoms> GetPopedomsByRoleID(int roleid)
        {
            PSSEntities db = new PSSEntities();
            List<int?> ids = new List<int?>();
            List<PopedomRole> prs = db.PopedomRole.Where(s=>s.RoleID==roleid).ToList();
            foreach (PopedomRole item in prs)
            {
                ids.Add(item.PopID);
            }
            return db.Popedoms.Where(s =>ids.Contains(s.PopID)).ToList();
        }
        /// <summary>
        /// 修改角色的权限
        /// </summary>
        /// <param name="roleid">角色ID</param>
        /// <param name="pds">权限ID集合</param>
        /// <returns></returns>
        public static int EditRolePopedoms(int roleid,List<int?> pds) {
            PSSEntities db = new PSSEntities();
            int fg = 1;
            using (var tx=db.Database.BeginTransaction())
            {
                try
                {
                    db.PopedomRole.RemoveRange(db.PopedomRole.Where(s=>s.RoleID==roleid));
                    for (int i = 0; i < pds.Count; i++)
                    {
                        db.PopedomRole.Add(new PopedomRole() { RoleID=roleid,PopID=pds[i] });
                    }
                    db.SaveChanges();
                    tx.Commit();
                }
                catch (Exception ex)
                {
                    fg = 0;
                    tx.Rollback();
                    throw new Exception(ex.Message);
                }
            }
            return fg;
        }

    }
}