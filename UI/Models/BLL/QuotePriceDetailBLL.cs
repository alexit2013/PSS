using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using UI.Models.Model;
using UI.Models.DAL;

namespace UI.Models.BLL
{
    public class QuotePriceDetailBLL
    {
        public static List<QuotePriceDetail> GetAllPageBySDID(int PageIndex, int PageSize, string SDID)
        {
            return QuotePriceDetailDAL.GetAllPageBySDID(PageIndex,PageSize,SDID);
        }
    }
}