using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using UI.Models.Model;

namespace UI.Models.DAL
{
    public class DepotStockDAL
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
        public static List<DepotStock> Find(int ProID, string ProName, int PTID, int PCID, int PSID, int PUID, string DepotID,int PageIndex,int PageSize,out int count) {
            PSSEntities db = new PSSEntities();
            var list = from d in db.DepotStock select d;
            if (ProID>0)
                list = list.Where(d=>d.ProID==ProID);

            if (ProName != null && ProName.Trim().Length > 0)
                list = list.Where(p=>p.Products.ProName.Contains(ProName));

            if (DepotID != null && DepotID.Trim().Length > 0)
                list = list.Where(p => p.DepotID==DepotID);

            if (PTID > 0)
            {
                List<int?> ids = new List<int?>();
                ids.Add(PTID);
                GetPTID(PTID, ids);
                list = list.Where(d =>ids.Contains( d.Products.ProductTypes.PTID));
            }
               

            if (PCID > 0)
                list = list.Where(d => d.Products.ProductColor.PCID == PCID);

            if (PSID > 0)
                list = list.Where(d => d.Products.ProductSpec.PSID == PSID);

            if (PUID > 0)
                list = list.Where(d => d.Products.ProductUnit.PUID == PUID);

            count = list.Count();
            return list.OrderBy(p=>p.DSID).Skip((PageIndex-1)*PageSize).Take(PageSize).ToList();
        }
        /// <summary>
        /// 查询所有
        /// </summary>
        /// <returns></returns>
        public static List<DepotStock> GetAll() {
            PSSEntities db = new PSSEntities();
            return db.DepotStock.Select(s=>s).ToList();
        }

        /// <summary>
        /// 根据类别ID查询所有的子类别ID【递归】
        /// </summary>
        /// <param name="id"></param>
        /// <param name="ptids"></param>
        private static void GetPTID(int id, List<int?> ptids)
        {
            PSSEntities db = new PSSEntities();
            //id=1
            List<ProductTypes> ptlist = db.ProductTypes.Where(pt => pt.PTParentID == id).ToList<ProductTypes>();
            foreach (ProductTypes item in ptlist)
            {
                ptids.Add(item.PTID);
                //递归
                GetPTID(item.PTID, ptids);
            }
        }
        /// <summary>
        /// 根据商品ID查询所有的商品库存信息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static List<DepotStock> FindProByDepots(int id) {
            PSSEntities db = new PSSEntities();
            return db.DepotStock.Where(s => s.ProID == id).ToList();
        }
    }
}