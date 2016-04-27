using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using UI.Models.Model;

namespace UI.Models.DAL
{
    public class InOutDepotDAL
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
        public static List<InOutDepotDetail> Find(string  ProID,string BDate,string EDate,string DepotID,out int count,int PageIndex) {
            PSSEntities db = new PSSEntities();
            var list = from s in db.InOutDepotDetail
                       select s;
            if (ProID!=null&&ProID.Trim().Length>0)
            {
                int id = Convert.ToInt32(ProID);
                list = list.Where(c=>c.ProID==id);
            }
            if (BDate!=null&&BDate.Trim().Length>0)
            {
                DateTime dt = Convert.ToDateTime(BDate);
                list = list.Where(c=>c.InOutDepot.IODDate>dt);
            }
            if (EDate != null && EDate.Trim().Length > 0)
            {
                DateTime dt = Convert.ToDateTime(EDate);
                list = list.Where(c => c.InOutDepot.IODDate < dt);
            }
            if (DepotID != null && DepotID.Trim().Length > 0)
            {
              
                list = list.Where(c => c.InOutDepot.DepotID.Equals(DepotID));
            }
            count = list.Count();
            return list.OrderBy(p=>p.IODDID).Skip((PageIndex-1)*10).Take(10).ToList();
        }
        /// <summary>
        /// 查询所有出入库记录
        /// </summary>
        /// <returns></returns>
        public static List<InOutDepotDetail> GetAll() {
            PSSEntities db = new PSSEntities();
            return db.InOutDepotDetail.Select(s=>s).ToList();
        }
    }
}