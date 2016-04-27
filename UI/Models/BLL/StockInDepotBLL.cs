using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using UI.Models.Model;
using UI.Models.DAL;

namespace UI.Models.BLL
{
    public class StockInDepotBLL
    {
        /// <summary>
        /// 查询所有
        /// </summary>
        /// <returns></returns>
        public static List<StockInDepot> GetAll()
        {
            return StockInDepotDAL.GetAll();
        }
        /// <summary>
        /// 分页查询
        /// </summary>
        /// <param name="PageIndex"></param>
        /// <param name="PageSize"></param>
        /// <returns></returns>
        public static List<StockInDepot> GetAllPage(int PageIndex, int PageSize)
        {
            return StockInDepotDAL.GetAllPage(PageIndex, PageSize);
        }
        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="dep"></param>
        /// <returns></returns>
        public static int AddStocks(StockInDepot dep, List<StockInDepotDetail> list)
        {
            return StockInDepotDAL.AddStocks(dep, list);
        }
        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static int DelStocks(string id)
        {
            return StockInDepotDAL.DelStocks(id);
        }
        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="dp"></param>
        /// <returns></returns>
        public static int EdiStocks(StockInDepot dp, List<StockInDepotDetail> list)
        {
            return StockInDepotDAL.EdiStocks(dp, list);
        }
        /// <summary>
        /// 审核采购订单
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static int CKStocks(string id)
        {
            return StockInDepotDAL.CKStocks(id);
        }
      /// <summary>
      /// 条件查询
      /// </summary>
      /// <param name="SIDID"></param>
      /// <param name="UsersName"></param>
      /// <param name="PPName"></param>
      /// <param name="StockID"></param>
      /// <param name="SIDDate"></param>
      /// <param name="SIDDeliver"></param>
      /// <param name="SIDData"></param>
      /// <param name="PageIndex"></param>
      /// <param name="PageSize"></param>
      /// <param name="count"></param>
      /// <returns></returns>
        public static List<StockInDepot> Find(string SIDID, string UsersName, string PPID, string DepotID, string SIDDate,  int SIDData, int PageIndex, int PageSize, out int count)
        {
            return StockInDepotDAL.Find(SIDID, UsersName, PPID, DepotID, SIDDate, SIDData, PageIndex, PageSize, out count);
        }

        internal static StockInDepot GetByID(string id)
        {
            return StockInDepotDAL.GetByID(id);
        }

        public static int CKStockIn(string id,int userid) {
            return StockInDepotDAL.CKStockIn(id,userid);
        }
    }
}