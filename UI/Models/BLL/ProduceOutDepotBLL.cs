using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using UI.Models.Model;
using UI.Models.DAL;

namespace UI.Models.BLL
{
    public class ProduceOutDepotBLL
    {
        /// <summary>
        /// 查询所有
        /// </summary>
        /// <returns></returns>
        public static List<ProduceOutDepot> GetAll()
        {
            return ProduceOutDepotDAL.GetAll();
        }
        /// <summary>
        /// 添加【---事务---】
        /// </summary>
        /// <param name="dep"></param>
        /// <returns></returns>
        public static int AddStocks(ProduceOutDepot dep, List<ProduceOutDepotDetail> list)
        {
            return ProduceOutDepotDAL.AddStocks(dep,list);
        }
        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static int DelStocks(string id)
        {
            return ProduceOutDepotDAL.DelStocks(id);
        }
        /// <summary>
        /// 修改【---事务---】
        /// </summary>
        /// <param name="dp"></param>
        /// <returns></returns>
        public static int EdiStocks(ProduceOutDepot dep, List<ProduceOutDepotDetail> list)
        {
            return ProduceOutDepotDAL.EdiStocks(dep,list);
        }
        /// <summary>
        /// 根据ID查询采购单信息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        internal static ProduceOutDepot GetByID(string id)
        {
            return ProduceOutDepotDAL.GetByID(id);
        }
        /// <summary>
        /// 审核采购订单
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static int CKInDepot(string id, int userid)
        {
            return ProduceOutDepotDAL.CKInDepot(id,userid);
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
        public static List<ProduceOutDepot> Find(string PODID, string DepotID, string PODDate, string UsersName, int PODState, int PageIndex, int PageSize, out int count)
        {
            return ProduceOutDepotDAL.Find(PODID,DepotID,PODDate,UsersName,PODState,PageIndex,PageSize,out count);
        }
    }
}