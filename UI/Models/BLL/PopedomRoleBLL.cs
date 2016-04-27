using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using UI.Models.DAL;
using UI.Models.Model;

namespace UI.Models.BLL
{
    public class PopedomRoleBLL
    {
        /// <summary>
        /// 查询所有权限
        /// </summary>
        /// <returns></returns>
        public static List<Popedoms> GetPopedomsAll()
        {
            return PopedomRoleDAL.GetPopedomsAll();
        }
        /// <summary>
        /// 修改角色的权限
        /// </summary>
        /// <param name="roleid">角色ID</param>
        /// <param name="pds">权限ID集合</param>
        /// <returns></returns>
        public static int EditRolePopedoms(int roleid, List<int?> pds)
        {
            return PopedomRoleDAL.EditRolePopedoms(roleid,pds);
        }
        /// <summary>
        /// 根据角色ID获取权限
        /// </summary>
        /// <param name="roleid"></param>
        /// <returns></returns>
        public static List<Popedoms> GetPopedomsByRoleID(int roleid)
        {
            return PopedomRoleDAL.GetPopedomsByRoleID(roleid);
        }
    }
}