using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using UI.Models.Model;
using UI.Models.DAL;

namespace UI.Models.BLL
{
    public class StockDetailBLL
    {
        /// <summary>
        /// 查询所有
        /// </summary>
        /// <returns></returns>
        public static List<StockDetail> GetAll()
        {
            return StockDetailDAL.GetAll();
        }

        /// <summary>
        /// 分页查询
        /// </summary>
        /// <param name="PageIndex"></param>
        /// <param name="PageSize"></param>
        /// <returns></returns>
        public static List<StockDetail> GetAllPage(int PageIndex, int PageSize)
        {
            return StockDetailDAL.GetAllPage(PageIndex,PageSize);
        }
        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="dep"></param>
        /// <returns></returns>
        public static int AddStockDetail(StockDetail dep)
        {
            return StockDetailDAL.AddStockDetail(dep);
        }
        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static int DelStockDetail(int id)
        {
            return StockDetailDAL.DelStockDetail(id);
        }
        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="dp"></param>
        /// <returns></returns>
        public static int EdiStockDetail(StockDetail dp)
        {
            return StockDetailDAL.EdiStockDetail(dp);
        }
        public static List<StockDetail> GetAllPageBySDID(int PageIndex, int PageSize, string SDID)
        {
            return StockDetailDAL.GetAllPageBySDID(PageIndex,PageSize,SDID);
        }
    }
}