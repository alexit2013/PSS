using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using UI.Models.Model;
using UI.Models.DAL;

namespace UI.Models.BLL
{
    public class DevolvesBLL
    {
        /// <summary>
        /// 查询所有
        /// </summary>
        /// <returns></returns>
        public static List<Devolves> GetAll()
        {
            return DevolvesDAL.GetAll();
        }
        /// <summary>
        /// 添加【---事务---】
        /// </summary>
        /// <param name="dep"></param>
        /// <returns></returns>
        public static int AddStocks(Devolves dep, List<DevolveDetail> list)
        {
            return DevolvesDAL.AddStocks(dep,list);
        }
        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static int DelStocks(string id)
        {
            return DevolvesDAL.DelStocks(id);
        }
        /// <summary>
        /// 修改【---事务---】
        /// </summary>
        /// <param name="dp"></param>
        /// <returns></returns>
        public static int EdiStocks(Devolves dep, List<DevolveDetail> list)
        {
            return DevolvesDAL.EdiStocks(dep,list);
        }
        /// <summary>
        /// 根据ID查询详单信息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        internal static Devolves GetByID(string id)
        {
            return DevolvesDAL.GetByID(id);
        }
        /// <summary>
        /// 审核报损单
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static int CKInDepot(string id, int userid)
        {
            return DevolvesDAL.CKInDepot(id,userid);
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
        public static List<Devolves> Find(string DevID, string DevOutID, string DevInID, string UsersName, string DevDate, int DevState, int PageIndex, int PageSize, out int count)
        {
            return DevolvesDAL.Find(DevID,DevOutID,DevInID,UsersName,DevDate,DevState,PageIndex,PageSize,out count);
        }

    }
}