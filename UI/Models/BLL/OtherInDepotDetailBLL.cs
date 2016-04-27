using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using UI.Models.Model;
using UI.Models.DAL;

namespace UI.Models.BLL
{
    public class OtherInDepotDetailBLL
    {
        /// <summary>
        /// 根据订单ID查询订单详情
        /// </summary>
        /// <param name="PageIndex"></param>
        /// <param name="PageSize"></param>
        /// <param name="SDID"></param>
        /// <returns></returns>
        public static List<OtherInDepotDetail> GetAllPageBySDID(int PageIndex, int PageSize, string SDID)
        {
            return OtherInDepotDetailDAL.GetAllPageBySDID(PageIndex,PageSize,SDID);
        }
    }
}