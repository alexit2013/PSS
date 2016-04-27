using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using UI.Models.DAL;
using UI.Models.Model;

namespace UI.Models.BLL
{
    public class OtherOutDepotDetailBLL
    {
        /// <summary>
        /// 根据其他出库单ID查询详单信息
        /// </summary>
        /// <param name="v1"></param>
        /// <param name="v2"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        internal static List<OtherOutDepotDetail> GetAllPageBySDID(int v1, int v2, string id)
        {
            return OtherOutDepotDetailDAL.GetAllPageBySDID(v1,v2,id);
        }
    }
}