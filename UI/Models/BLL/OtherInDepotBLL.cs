using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using UI.Models.Model;
using UI.Models.DAL;

namespace UI.Models.BLL
{
    public class OtherInDepotBLL
    {
        /// <summary>
        /// 查询所有
        /// </summary>
        /// <returns></returns>
        public static List<OtherInDepot> GetAll()
        {
            return OtherInDepotDAL.GetAll();
        }

        /// <summary>
        /// 添加【---事务---】
        /// </summary>
        /// <param name="dep"></param>
        /// <returns></returns>
        public static int AddStocks(OtherInDepot dep, List<OtherInDepotDetail> list)
        {
            return OtherInDepotDAL.AddStocks(dep,list);
        }
        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static int DelStocks(string id)
        {
            return OtherInDepotDAL.DelStocks(id);
        }
        /// <summary>
        /// 修改【---事务---】
        /// </summary>
        /// <param name="dp"></param>
        /// <returns></returns>
        public static int EdiStocks(OtherInDepot dep, List<OtherInDepotDetail> list)
        {
            return OtherInDepotDAL.EdiStocks(dep,list);
        }
        /// <summary>
        /// 根据ID查询采购单信息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        internal static OtherInDepot GetByID(string id)
        {
            return OtherInDepotDAL.GetByID(id);
        }
        /// <summary>
        /// 审核采购订单
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static int CKInDepot(string id, int userid)
        {
            return OtherInDepotDAL.CKInDepot(id,userid);
        }
        /// <summary>
        /// 条件查询
        /// </summary>
        /// <param name="PIDID"></param>
        /// <param name="DepotID"></param>
        /// <param name="PIDDate"></param>
        /// <param name="UsersName"></param>
        /// <param name="PIDState"></param>
        /// <param name="PageIndex"></param>
        /// <param name="PageSize"></param>
        /// <param name="count"></param>
        /// <returns></returns>
        public static List<OtherInDepot> Find(string OIDID, string DepotID, string OIDDate, string UsersName, int OIDState, int PageIndex, int PageSize, out int count)
        {
            return OtherInDepotDAL.Find(OIDID,DepotID,OIDDate,UsersName,OIDState,PageIndex,PageSize,out count);
        }
    }
}