using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using UI.Models.Model;

namespace UI.Models.DAL
{
    public class DepotsDAL
    {
        /// <summary>
        /// 查询所有的仓库信息
        /// </summary>
        /// <returns></returns>
        public static List<Depots> GetAll() {
            PSSEntities db = new PSSEntities();
            return db.Depots.Select(d => d).ToList<Depots>();
        }
        /// <summary>
        /// 分页查询仓库信息
        /// </summary>
        /// <param name="PageIndex"></param>
        /// <param name="PageSize"></param>
        /// <returns></returns>
        public static List<Depots> GetAllPage(int PageIndex,int PageSize)
        {
            PSSEntities db = new PSSEntities();
            return db.Depots.Select(d => d).OrderBy(d => d.DepotID).Skip((PageIndex - 1) * PageSize).Take(PageSize).ToList<Depots>();
        }
        /// <summary>
        /// 添加仓库
        /// </summary>
        /// <param name="dep"></param>
        /// <returns></returns>
        public static int AddDepots( Depots dep) {
            PSSEntities db = new PSSEntities();
            db.Depots.Add(dep);
            return db.SaveChanges();
        }
        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static int DelDepots(string id) {
            PSSEntities db = new PSSEntities();
            db.Depots.Remove(db.Depots.FirstOrDefault(d => d.DepotID.Equals(id)));
            return db.SaveChanges();
        }
        /// <summary>
        /// 修改仓库
        /// </summary>
        /// <param name="dp"></param>
        /// <returns></returns>
        public static int EditDepot(Depots dp) {
            PSSEntities db = new PSSEntities();
            db.Entry<Depots>(dp).State = System.Data.Entity.EntityState.Modified;
            return db.SaveChanges();
        }
    }
}