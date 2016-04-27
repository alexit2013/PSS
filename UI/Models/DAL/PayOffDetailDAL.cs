using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using UI.Models.Model;

namespace UI.Models.DAL
{
    public class PayOffDetailDAL
    {
        /// <summary>
        /// 根据主单ID查询详单信息
        /// </summary>
        /// <param name="id">主单ID</param>
        /// <param name="PageIndex">当前页</param>
        /// <param name="PageSize">页大小</param>
        /// <returns></returns>
        public static List<PayOffDetail> GetDeteilByID(string id, int PageIndex, int PageSize)
        {
            PSSEntities db = new PSSEntities();
            return db.PayOffDetail.Where(s => s.POID.Equals(id)).OrderBy(s => s.PODID).Skip((PageIndex - 1) * PageSize).Take(PageSize).ToList();
        }

    }
}