using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using UI.Models.Model;
using UI.Models.DAL;

namespace UI.Models.BLL
{
    public class SplitsBLL
    {
        /// <summary>
        /// 查询所有
        /// </summary>
        /// <returns></returns>
        public static List<Splits> GetAll()
        {
            return SplitsDAL.GetAll();
        }
        /// <summary>
        /// 添加【---事务---】
        /// </summary>
        /// <param name="dep"></param>
        /// <returns></returns>
        public static int AddStocks(Splits dep, List<SplitDetail> list)
        {
            return SplitsDAL.AddStocks(dep,list);
        }
        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static int DelStocks(string id)
        {
            return SplitsDAL.DelStocks(id);
        }
        /// <summary>
        /// 修改【---事务---】
        /// </summary>
        /// <param name="dp"></param>
        /// <returns></returns>
        public static int EdiStocks(Splits dep, List<SplitDetail> list)
        {
            return SplitsDAL.EdiStocks(dep,list);
        }
        /// <summary>
        /// 根据ID查询详单信息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        internal static Splits GetByID(string id)
        {
            return SplitsDAL.GetByID(id);
        }
        /// <summary>
        /// 审核订单【修改后不可修改，直接影响库存数据】
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static int CKDepot(string id, int userid)
        {
            return SplitsDAL.CKDepot(id,userid);
        }
        /// <summary>
        /// 条件查询
        /// </summary>
        /// <param name="SplitID">拆分ID</param>
        /// <param name="DepotID">仓库ID</param>
        /// <param name="ProName">商品名称</param>
        /// <param name="UsersName">操作人</param>
        /// <param name="SplitDate">拆分时间</param>
        /// <param name="SplitState">状态</param>
        /// <param name="PageIndex">当前页</param>
        /// <param name="PageSize">页大小</param>
        /// <param name="count">总条数</param>
        /// <returns></returns>
        public static List<Splits> Find(string SplitID, string DepotID, string ProName, string UsersName, string SplitDate, int SplitState, int PageIndex, int PageSize, out int count)
        {
            return SplitsDAL.Find(SplitID,DepotID,ProName,UsersName,SplitDate,SplitState,PageIndex,PageSize,out count);
        }
    }
}