using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using UI.Models.Model;
using UI.Models.DAL;

namespace UI.Models.BLL
{
    public class ProduceInDepotBLL
    {
        /// <summary>
        /// 查询所有
        /// </summary>
        /// <returns></returns>
        public static List<ProduceInDepot> GetAll()
        {
            return ProduceInDepotDAL.GetAll();
        }

        /// <summary>
        /// 分页查询
        /// </summary>
        /// <param name="PageIndex"></param>
        /// <param name="PageSize"></param>
        /// <returns></returns>
        public static List<ProduceInDepot> GetAllPage(int PageIndex, int PageSize)
        {
            return ProduceInDepotDAL.GetAllPage(PageIndex,PageSize);
        }
        /// <summary>
        /// 添加【---事务---】
        /// </summary>
        /// <param name="dep"></param>
        /// <returns></returns>
        public static int AddStocks(ProduceInDepot dep, List<ProduceInDepotDeteil> list)
        {
            return  ProduceInDepotDAL.AddStocks(dep,list);
        }
        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static int DelStocks(string id)
        {
            return ProduceInDepotDAL.DelStocks(id);
        }
        /// <summary>
        /// 修改【---事务---】
        /// </summary>
        /// <param name="dp"></param>
        /// <returns></returns>
        public static int EdiStocks(ProduceInDepot dep, List<ProduceInDepotDeteil> list)
        {
            return ProduceInDepotDAL.EdiStocks(dep,list);
        }
        /// <summary>
        /// 根据ID查询采购单信息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        internal static ProduceInDepot GetByID(string id)
        {
            return ProduceInDepotDAL.GetByID(id);
        }
        /// <summary>
        /// 审核采购订单
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static int CKProduceInDepot(string id,int userid)
        {
            return ProduceInDepotDAL.CKProduceInDepot(id,userid);
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
        public static List<ProduceInDepot> Find(string PIDID, string DepotID, string PIDDate, string UsersName, int PIDState, int PageIndex, int PageSize, out int count)
        {
            return ProduceInDepotDAL.Find(PIDID, DepotID, PIDDate, UsersName, PIDState, PageIndex, 10, out count);
        }

    }
}