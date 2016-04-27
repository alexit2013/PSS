using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using UI.Models.Model;
using UI.Models.DAL;

namespace UI.Models.BLL
{
    public class StockInDepotDetailBLL
    {
        /// <summary>
        /// 根据采购入库单ID查询详单信息
        /// </summary>
        /// <param name="PageIndex"></param>
        /// <param name="PageSize"></param>
        /// <param name="SDID"></param>
        /// <returns></returns>
        public static List<StockInDepotDetail> GetAllPageBySDID(int PageIndex, int PageSize, string SDID)
        {
            return StockInDepotDetailDAL.GetAllPageBySDID(PageIndex,PageSize,SDID);
        }

        internal static int Delete(string id)
        {
            return StockInDepotDetailDAL.Delete(id);
        }
    }
}