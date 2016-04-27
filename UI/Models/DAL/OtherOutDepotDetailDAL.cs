using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using UI.Models.Model;

namespace UI.Models.DAL
{
    public class OtherOutDepotDetailDAL
    {
        internal static List<OtherOutDepotDetail> GetAllPageBySDID(int pageIndex, int pageSize, string id)
        {
            PSSEntities db = new PSSEntities();
            return db.OtherOutDepotDetail.Where(s => s.OODID.Equals(id)).OrderBy(p => p.OODID).Skip((pageIndex - 1) * pageSize).Take(pageSize).ToList();
        }
    }
}