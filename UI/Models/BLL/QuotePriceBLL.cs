using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using UI.Models.Model;
using UI.Models.DAL;

namespace UI.Models.BLL
{
    public class QuotePriceBLL
    {
        /// <summary>
        /// 查询所有
        /// </summary>
        /// <returns></returns>
        public static List<QuotePrice> GetAll()
        {
            return QuotePriceDAL.GetAll(); 
        }
        /// <summary>
        /// 添加【---事务---】
        /// </summary>
        /// <param name="dep"></param>
        /// <returns></returns>
        public static int AddStocks(QuotePrice dep, List<QuotePriceDetail> list)
        {
            return QuotePriceDAL.AddStocks(dep,list);
        }
        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static int DelStocks(string id)
        {
            return QuotePriceDAL.DelStocks(id);
        }
        /// <summary>
        /// 修改【---事务---】
        /// </summary>
        /// <param name="dp"></param>
        /// <returns></returns>
        public static int EdiStocks(QuotePrice dep, List<QuotePriceDetail> list)
        {
            return QuotePriceDAL.EdiStocks(dep,list);
        }
        /// <summary>
        /// 根据ID查询采购单信息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        internal static QuotePrice GetByID(string id)
        {
            return QuotePriceDAL.GetByID(id);
        }
        /// <summary>
        /// 审核采购订单
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static int CKInDepot(string id, int userid)
        {
            return QuotePriceDAL.CKInDepot(id,userid);
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
        public static List<QuotePrice> Find(string QPID, string CusID, string QPDate, string UsersName, int QPState, int PageIndex, int PageSize, out int count)
        {
            return QuotePriceDAL.Find(QPID,CusID,QPDate,UsersName,QPState,PageIndex,PageSize,out count);
        }
    }
}