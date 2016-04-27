using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using UI.Models.DAL;
using UI.Models.Model;

namespace UI.Models.BLL
{
    public class DepotStockBLL
    {  
        /// <summary>
        /// 库存查询
        /// </summary>
        /// <param name="ProID">商品ID</param>
        /// <param name="ProName">商品名称</param>
        /// <param name="PTID">商品类别</param>
        /// <param name="PCID">商品颜色</param>
        /// <param name="PSID">商品规格</param>
        /// <param name="PUID">商品单位</param>
        /// <param name="DepotID">仓库</param>
        /// <param name="count">查询结果个数</param>
        /// <returns></returns>
        public static List<DepotStock> Find(int ProID, string ProName, int PTID, int PCID, int PSID, int PUID, string DepotID, int PageIndex, int PageSize, out int count)
        {
            return DepotStockDAL.Find(ProID,ProName,PTID,PCID,PSID,PUID,DepotID,PageIndex,PageSize,out count);
        }
        /// <summary>
        /// 根据商品ID查询所有的商品库存信息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static List<DepotStock> FindProByDepots(int id)
        {
            return DepotStockDAL.FindProByDepots(id);
        }
    }
}