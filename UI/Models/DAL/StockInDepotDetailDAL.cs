using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using UI.Models.Model;

namespace UI.Models.DAL
{
    public class StockInDepotDetailDAL
    {
        /// <summary>
        /// 根据采购入库单ID查询详单信息
        /// </summary>
        /// <param name="PageIndex"></param>
        /// <param name="PageSize"></param>
        /// <param name="SDID"></param>
        /// <returns></returns>
        public static List<StockInDepotDetail> GetAllPageBySDID(int PageIndex, int PageSize, string SDID)
        {
            PSSEntities db = new PSSEntities();
            return db.StockInDepotDetail.Where(d => d.SIDID.Equals(SDID)).OrderBy(d => d.SIDDID).Skip((PageIndex - 1) * PageSize).Take(PageSize).ToList<StockInDepotDetail>();
        }
        /// <summary>
        /// 删除详单
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        internal static int Delete(string id)
        {
            PSSEntities db = new PSSEntities();
            using (var tx=db.Database.BeginTransaction())
            {
                try
                {
                    db.StockInDepotDetail.RemoveRange(db.StockInDepotDetail.Where(s=>s.SIDID==id));
                    db.StockInDepot.Remove(db.StockInDepot.FirstOrDefault(s => s.SIDID == id));
                    tx.Commit();
                }
                catch (Exception ex)
                {
                    tx.Rollback();
                    throw new Exception(ex.Message);
                }
            }
            return db.SaveChanges();
        }
    }
}