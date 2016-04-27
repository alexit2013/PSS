using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using UI.Models.Model;

namespace UI.Models.DAL
{
    public class DevolveDetailDAL
    {
        /// <summary>
        /// 根据主单ID查询详单信息
        /// </summary>
        /// <param name="id">主单ID</param>
        /// <param name="PageIndex">单前页</param>
        /// <param name="PageSize">页大小</param>
        /// <returns></returns>
        public static List<DevolveDetail> GetDetailByID(string id,int PageIndex,int PageSize) {
            PSSEntities db = new PSSEntities();
          return  db.DevolveDetail.Where(p=>p.DevID.Equals(id)).OrderBy(p=>p.DevDID).Skip((PageIndex-1)*PageSize).Take(PageSize).ToList();
        }
    }
}