using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using UI.Models.DAL;
using UI.Models.Model;

namespace UI.Models.BLL
{
    public class OtherOutDepotBLL
    {
        /// <summary>
        /// 查询所有
        /// </summary>
        /// <returns></returns>
        public static List<OtherOutDepot> GetAll()
        {
            return OtherOutDepotDAL.GetAll();
        }
        /// <summary>
        /// 添加【---事务---】
        /// </summary>
        /// <param name="dep"></param>
        /// <returns></returns>
        public static int AddStocks(OtherOutDepot dep, List<OtherOutDepotDetail> list)
        {
            return OtherOutDepotDAL.AddStocks(dep, list);
        }
        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static int DelStocks(string id)
        {
            return OtherOutDepotDAL.DelStocks(id);
        }
        /// <summary>
        /// 修改【---事务---】
        /// </summary>
        /// <param name="dp"></param>
        /// <returns></returns>
        public static int EdiStocks(OtherOutDepot dep, List<OtherOutDepotDetail> list)
        {
            return OtherOutDepotDAL.EdiStocks(dep, list);
        }
        /// <summary>
        /// 根据ID查询采购单信息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        internal static OtherOutDepot GetByID(string id)
        {
            return OtherOutDepotDAL.GetByID(id);
        }
        /// <summary>
        /// 审核采购订单
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static int CKInDepot(string id, int userid)
        {
            return OtherOutDepotDAL.CKInDepot(id, userid);
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
        public static List<OtherOutDepot> Find(string OODID, string DepotID, string OODDate, string UsersName, int OODState, int PageIndex, int PageSize, out int count)
        {
            return OtherOutDepotDAL.Find(OODID, DepotID, OODDate, UsersName, OODState, PageIndex, PageSize, out count);
        }
    }
}