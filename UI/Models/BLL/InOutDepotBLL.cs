using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using UI.Models.Model;
using UI.Models.DAL;
namespace UI.Models.BLL
{
    public class InOutDepotBLL
    {
        /// <summary>
        ///  查询出入库记录
        /// </summary>
        /// <param name="ProID">商品ID</param>
        /// <param name="BDate">开始时间</param>
        /// <param name="EDate">结束时间</param>
        /// <param name="DepotID">仓库编号</param>
        /// <param name="count">查询个数</param>
        /// <returns></returns>
        public static List<InOutDepotDetail> Find(string ProID, string BDate, string EDate, string DepotID, out int count,int PageIndex)
        {
            return InOutDepotDAL.Find(ProID,BDate,EDate,DepotID,out count,PageIndex);
        }
        /// <summary>
        /// 查询所有出入库记录
        /// </summary>
        /// <returns></returns>
        public static List<InOutDepotDetail> GetAll()
        {
            return InOutDepotDAL.GetAll();
        }

      }
}