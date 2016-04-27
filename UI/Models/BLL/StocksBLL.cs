using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using UI.Models.Model;
using UI.Models.DAL;

namespace UI.Models.BLL
{
    public class StocksBLL
    {
        /// <summary>
        /// 查询所有
        /// </summary>
        /// <returns></returns>
        public static List<Stocks> GetAll()
        {
            return StocksDAL.GetAll();
        }
        /// <summary>
        /// 分页查询
        /// </summary>
        /// <param name="PageIndex"></param>
        /// <param name="PageSize"></param>
        /// <returns></returns>
        public static List<Stocks> GetAllPage(int PageIndex, int PageSize)
        {
            return StocksDAL.GetAllPage(PageIndex, PageSize);
        }
        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="dep"></param>
        /// <returns></returns>
        public static int AddStocks(Stocks dep,List<StockDetail> list)
        {
            return StocksDAL.AddStocks(dep, list);
        }
        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static int DelStocks(string id)
        {
            return StocksDAL.DelStocks(id);
        }
        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="dp"></param>
        /// <returns></returns>
        public static int EdiStocks(Stocks dp, List<StockDetail> list)
        {
            return StocksDAL.EdiStocks(dp,list);
        }
        /// <summary>
        /// 审核采购订单
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static int CKStocks(string id)
        {
            return StocksDAL.CKStocks(id);
        }
        /// <summary>
        /// 多条件查询带分页
        /// </summary>
        /// <param name="StockID"></param>
        /// <param name="UsersName"></param>
        /// <param name="PPID"></param>
        /// <param name="StockDate"></param>
        /// <param name="StockInDate"></param>
        /// <param name="PageIndex"></param>
        /// <param name="PageSize"></param>
        /// <param name="count"></param>
        /// <returns></returns>
        public static List<Stocks> Find(string StockID, string UsersName, string PPID, string StockDate, string StockInDate, int PageIndex, int PageSize, out int count,int? StockState) {
            return StocksDAL.Find(StockID, UsersName, PPID, StockDate, StockInDate, PageIndex, PageSize, out count, StockState);
        }

        internal static Stocks GetByID(string id)
        {
            return StocksDAL.GetByID(id);
        }
    }
}