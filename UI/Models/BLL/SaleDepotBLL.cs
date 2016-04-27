using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using UI.Models.Model;
using UI.Models.DAL;

namespace UI.Models.BLL
{
    public class SaleDepotBLL
    {
        /// <summary>
        /// 添加销售出库单
        /// </summary>
        /// <param name="qp"></param>
        /// <param name="list"></param>
        /// <returns></returns>
        internal static int AddStocks(SaleDepot qp, List<SaleDepotDetail> list)
        {
            return SaleDepotDAL.AddStocks(qp,list);
        }
        /// <summary>
        /// 查询所有
        /// </summary>
        /// <returns></returns>
        internal static List<SaleDepot> GetAll()
        {
            return SaleDepotDAL.GetAll();
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
        internal static List<SaleDepot> Find(string SDID, string CusID, string DepotID, string SDDate, string UsersName, int SDState, int PageIndex, int PageSize, out int count)
        {
            return SaleDepotDAL.Find(SDID,CusID,DepotID,SDDate,UsersName,SDState,PageIndex,PageSize,out count);
        }
        /// <summary>
        /// 修改销售出库单
        /// </summary>
        /// <param name="qp"></param>
        /// <param name="list"></param>
        /// <returns></returns>
        internal static int EditStocks(SaleDepot qp, List<SaleDepotDetail> list)
        {
            return SaleDepotDAL.EditStocks(qp,list);
        }
        /// <summary>
        /// 根据ID查询销售出库单信息
        /// </summary>
        /// <param name="id"></param>
        internal static SaleDepot GetSaleDepotByID(string id)
        {
            return SaleDepotDAL.GetSaleDepotByID(id);
        }
        /// <summary>
        /// 审核销售出库单
        /// </summary>
        /// <param name="id"></param>
        /// <param name="userid"></param>
        /// <returns></returns>
        internal static int CKInDepot(string id, int userid)
        {
            return SaleDepotDAL.CKInDepot(id,userid);
        }
        /// <summary>
        /// 删除客户订单
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        internal static int DelStocks(string id)
        {
            return SaleDepotDAL.DelStocks(id);
        }
    }
}