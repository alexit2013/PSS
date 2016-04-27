using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using UI.Models.Model;
using UI.Models.DAL;

namespace UI.Models.BLL
{
    public class SaleReturnBLL
    {
        /// <summary>
        /// 添加销售退货单
        /// </summary>
        /// <param name="qp"></param>
        /// <param name="list"></param>
        /// <returns></returns>
        internal static int AddStocks(SaleReturn qp, List<SaleReturnDetail> list)
        {
            return SaleReturnDAL.AddStocks(qp,list);
        }
        /// <summary>
        /// 查询所有
        /// </summary>
        /// <returns></returns>
        internal static List<SaleReturn> GetAll()
        {
            return SaleReturnDAL.GetAll();
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
        internal static List<SaleReturn> Find(string SRID, string CusID, string SDID, string DepotID, string SRDate, string UsersName, int SRState, int PageIndex, int PageSize, out int count)
        {
            return SaleReturnDAL.Find(SRID,CusID,SDID,DepotID,SRDate,UsersName,SRState,PageIndex,PageSize,out count);
        }
        /// <summary>
        /// 修改销售退货单
        /// </summary>
        /// <param name="qp"></param>
        /// <param name="list"></param>
        /// <returns></returns>
        internal static int EditStocks(SaleReturn qp, List<SaleReturnDetail> list)
        {
            return SaleReturnDAL.EditStocks(qp,list);
        }
        /// <summary>
        /// 根据ID查询采购退货详单信息
        /// </summary>
        /// <param name="id"></param>
        internal static SaleReturn GetDByID(string id)
        {
            return SaleReturnDAL.GetDByID(id);
        }
        /// <summary>
        /// 审核采购退货单
        /// </summary>
        /// <param name="id"></param>
        /// <param name="userid"></param>
        /// <returns></returns>
        internal static int CKInDepot(string id, int userid)
        {
            return SaleReturnDAL.CKInDepot(id,userid);
        }
        /// <summary>
        /// 删除采购退货单
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        internal static int DelStocks(string id)
        {
            return SaleReturnDAL.DelStocks(id);
        }
    }
}