using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using UI.Models.Model;
using UI.Models.DAL;

namespace UI.Models.BLL
{
    public class SaleDepotDetailBLL
    {
        internal static List<SaleDepotDetail> GetAllPageBySDID(int PageIndex, int PageSize, string id)
        {
              return  SaleDepotDetailDAL.GetAllPageBySDID(PageIndex,PageSize,id);
        }
    }
}