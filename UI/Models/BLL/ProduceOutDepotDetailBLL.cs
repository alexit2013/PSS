using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using UI.Models.DAL;
using UI.Models.Model;

namespace UI.Models.BLL
{
    public class ProduceOutDepotDetailBLL
    {
        internal static List<ProduceOutDepotDetail> GetAllPageBySDID(int PageIndex, int PageSize, string id)
        {
            return ProduceOutDepotDetailDAL.GetAllPageBySDID(PageIndex, PageSize, id);
        }
    }
}