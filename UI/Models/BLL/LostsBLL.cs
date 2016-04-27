using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using UI.Models.DAL;
using UI.Models.Model;

namespace UI.Models.BLL
{
    public class LostsBLL
    {
        /// <summary>
        /// 查询所有
        /// </summary>
        /// <returns></returns>
        public static List<Losts> GetAll()
        {
            return LostsDAL.GetAll();
        }
        /// <summary>
        /// 添加【---事务---】
        /// </summary>
        /// <param name="dep"></param>
        /// <returns></returns>
        public static int AddStocks(Losts dep, List<LostDetail> list)
        {
            return LostsDAL.AddStocks(dep,list);
        }
        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static int DelStocks(string id)
        {
            return LostsDAL.DelStocks(id);
        }
        /// <summary>
        /// 修改【---事务---】
        /// </summary>
        /// <param name="dp"></param>
        /// <returns></returns>
        public static int EdiStocks(Losts dep, List<LostDetail> list)
        {
            return LostsDAL.EdiStocks(dep,list);
        }
        /// <summary>
        /// 根据ID查询详单信息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        internal static Losts GetByID(string id)
        {
            return LostsDAL.GetByID(id);
        }
        /// <summary>
        /// 审核报损单
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static int CKInDepot(string id, int userid)
        {
            return LostsDAL.CKInDepot(id,userid);
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
        public static List<Losts> Find(string LostID, string DepotID, string LostDate, string UsersName, int LostState, int PageIndex, int PageSize, out int count)
        {
            return LostsDAL.Find(LostID,DepotID,LostDate,UsersName,LostState,PageIndex,PageSize,out count);
        }
    }
}