using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using UI.Models.Model;
using UI.Models.DAL;

namespace UI.Models.BLL
{
    public class ProduceInDepotDeteilBLL
    {
        public static List<ProduceInDepotDeteil> GetProductInDepotDeteilByID(int PageIndex,int PageSize,string id) {
            return ProduceInDepotDeteilDAL.GetAllPageBySDID(PageIndex,PageSize, id);
        }
    }
}