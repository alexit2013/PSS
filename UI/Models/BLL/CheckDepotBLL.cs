using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using UI.Models.Model;
using UI.Models.DAL;


namespace UI.Models.BLL
{
    public class CheckDepotBLL
    {
    

        /// <summary>
        /// 查询所有
        /// </summary>
        /// <returns></returns>
        public static List<CheckDepot> GetAll()
        {
            return CheckDepotDAL.GetAll();
        }
        /// <summary>
        /// 添加【---事务---】
        /// </summary>
        /// <param name="dep"></param>
        /// <returns></returns>
        public static int AddStocks(CheckDepot dep, List<CheckDepotDetail> list)
        {
            return CheckDepotDAL.AddStocks(dep,list);
        }
        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static int DelStocks(string id)
        {
            return CheckDepotDAL.DelStocks(id);
        }
        /// <summary>
        /// 修改【---事务---】
        /// </summary>
        /// <param name="dp"></param>
        /// <returns></returns>
        public static int EdiStocks(CheckDepot dep, List<CheckDepotDetail> list)
        {
            return CheckDepotDAL.EdiStocks(dep,list);
        }
        /// <summary>
        /// 根据ID查询详单信息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        internal static CheckDepot GetByID(string id)
        {
            return CheckDepotDAL.GetByID(id);
        }
        /// <summary>
        /// 审核订单【修改后不可修改，直接影响库存数据】
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static int CKDepot(string id, int userid)
        {
            return CheckDepotDAL.CKDepot(id,userid);
        }
        /// <summary>
        /// 盘点【将状态改为二】
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static int PDDepot(string id)
        {
            return CheckDepotDAL.PDDepot(id);
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
        public static List<CheckDepot> Find(string CDID, string DepotID, string UsersName, string CDDate, int CDState, int PageIndex, int PageSize, out int count)
        {
            return CheckDepotDAL.Find(CDID,DepotID,UsersName,CDDate,CDState,PageIndex,PageSize,out count);
        }

        /// <summary>
        /// 根据仓库ID查询商品信息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        internal static List<Products> GetByDepotID(string id)
        {
            return CheckDepotDAL.GetByDepotID(id);
        }
    }
}