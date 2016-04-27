using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using UI.Models.DAL;
using UI.Models.Model;

namespace UI.Models.BLL
{
    public class DevolveDetailBLL
    {
        /// <summary>
        /// 根据主单ID查询详单信息
        /// </summary>
        /// <param name="id">主单ID</param>
        /// <param name="PageIndex">当前页</param>
        /// <param name="PageSize">页大小</param>
        /// <returns></returns>
        public static List<DevolveDetail> GetDetailByID(string id, int PageIndex, int PageSize)
        {
            return DevolveDetailDAL.GetDetailByID(id,PageIndex,PageSize);
        }

    }
}