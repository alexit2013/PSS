using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using UI.Models.Model;
using UI.Models.DAL;

namespace UI.Models.BLL
{
    public class StockReturnBLL
    {
        /// <summary>
        /// 添加采购退货单
        /// </summary>
        /// <param name="qp"></param>
        /// <param name="list"></param>
        /// <returns></returns>
        internal static int AddStocks(StockReturn qp, List<StockReturnDetail> list)
        {
            return StockReturnDAL.AddStocks(qp,list);
        }
        /// <summary>
        /// 查询所有
        /// </summary>
        /// <returns></returns>
        internal static List<StockReturn> GetAll()
        {
            return StockReturnBLL.GetAll();
        }
        /// <summary>
        /// 条件查询
        /// </summary>
        /// <param name="COID"></param>
        /// <param name="CusID"></param>
        /// <param name="CODate"></param>
        /// <param name="CORefDate"></param>
        /// <param name="UsersName"></param>
        /// <param name="COState"></param>
        /// <param name="PageIndex"></param>
        /// <param name="PageSize"></param>
        /// <param name="count"></param>
        /// <returns></returns>
        internal static List<StockReturn> Find(string KRID, string PPID, string SIDID, string DepotID, string KRDate, string UsersName, int KRState, int PageIndex, int PageSize, out int count)
        {
            return StockReturnDAL.Find(KRID,PPID,SIDID,DepotID,KRDate,UsersName,KRState,PageIndex,PageSize,out count);
        }
        /// <summary>
        /// 修改采购退货单
        /// </summary>
        /// <param name="qp"></param>
        /// <param name="list"></param>
        /// <returns></returns>
        internal static int EditStocks(StockReturn qp, List<StockReturnDetail> list)
        {
            return StockReturnDAL.EditStocks(qp,list);
        }
        /// <summary>
        /// 根据ID查询采购退货详单信息
        /// </summary>
        /// <param name="id"></param>
        internal static StockReturn GetDByID(string id)
        {
            return StockReturnDAL.GetDByID(id);
        }
        /// <summary>
        /// 审核采购退货单
        /// </summary>
        /// <param name="id"></param>
        /// <param name="userid"></param>
        /// <returns></returns>
        internal static int CKInDepot(string id, int userid)
        {
            return StockReturnDAL.CKInDepot(id,userid);
        }
        /// <summary>
        /// 删除采购退货单
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        internal static int DelStocks(string id)
        {
            return StockReturnDAL.DelStocks(id);
        }
    }
}