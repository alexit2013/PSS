using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using UI.Models.Model;
using System.Data.Entity.Core.Objects;

namespace UI.Models.DAL
{
    public class StockInDepotDAL
    {
        /// <summary>
        /// 查询所有
        /// </summary>
        /// <returns></returns>
        public static List<StockInDepot> GetAll()
        {
            PSSEntities db = new PSSEntities();
            return db.StockInDepot.Select(d => d).ToList<StockInDepot>();
        }
        /// <summary>
        /// 分页查询
        /// </summary>
        /// <param name="PageIndex"></param>
        /// <param name="PageSize"></param>
        /// <returns></returns>
        public static List<StockInDepot> GetAllPage(int PageIndex, int PageSize)
        {
            PSSEntities db = new PSSEntities();
            return db.StockInDepot.Select(d => d).OrderBy(d => d.SIDID).Skip((PageIndex - 1) * PageSize).Take(PageSize).ToList<StockInDepot>();
        }
        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="dep"></param>
        /// <returns></returns>
        public static int AddStocks(StockInDepot dep, List<StockInDepotDetail> list)
        {
            PSSEntities db = new PSSEntities();
            using (var tx = db.Database.BeginTransaction())
            {
                try
                {
                    db.StockInDepot.Add(dep);
                    if (list != null)
                        db.StockInDepotDetail.AddRange(list);

                    tx.Commit();
                    return db.SaveChanges();
                }
                catch (Exception e)
                {
                    tx.Rollback();
                    throw new Exception(e.Message);
                }
            }
        }
        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static int DelStocks(string id)
        {
            PSSEntities db = new PSSEntities();
            using (var tx = db.Database.BeginTransaction())
            {
                try
                {
                    db.StockInDepot.Remove(db.StockInDepot.FirstOrDefault(d => d.SIDID.Equals(id)));
                    db.StockInDepotDetail.RemoveRange(db.StockInDepotDetail.Where(s => s.SIDID.Equals(id)));
                    tx.Commit();
                }
                catch (Exception e)
                {
                    tx.Rollback();
                    throw new Exception(e.Message);
                }
            }
            return db.SaveChanges();
        }
        /// <summary>
        /// 修改【---事务---】
        /// </summary>
        /// <param name="dp"></param>
        /// <returns></returns>
        public static int EdiStocks(StockInDepot dp, List<StockInDepotDetail> list)
        {
            PSSEntities db = new PSSEntities();
            int fg = 1;
            ///以下开始事务操作
            using (var tx = db.Database.BeginTransaction())
            {
                try
                {
                    //将要修改的创建人找回来，避免修改时没有创建人
                    StockInDepot st = db.StockInDepot.FirstOrDefault(p => p.SIDID == dp.SIDID);
                    st.PPID = dp.PPID;
                    st.DepotID = dp.DepotID;
                    st.SIDData = dp.SIDData;
                    st.SIDDate = dp.SIDDate;
                    st.SIDDesc = dp.SIDDesc;
                    st.SIDDeliver = dp.SIDDeliver;
                    st.SIDFreight = dp.SIDFreight;
                    db.StockInDepotDetail.RemoveRange(db.StockInDepotDetail.Where(p => p.SIDID == dp.SIDID));
                    if(list!=null)
                        db.StockInDepotDetail.AddRange(list);

                    db.SaveChanges();
                    tx.Commit();
                }
                catch (Exception e)
                {
                    fg = 0;
                    tx.Rollback();
                    throw new Exception(e.Message);
                }

            }
            return fg;
        }
        /// <summary>
        /// 根据ID查询入库单信息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        internal static StockInDepot GetByID(string id)
        {
            PSSEntities db = new PSSEntities();
            return db.StockInDepot.FirstOrDefault(s => s.SIDID.Equals(id));
        }
        /// <summary>
        /// 审核采购订单
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static int CKStocks(string id)
        {
            PSSEntities db = new PSSEntities();
            ///以下开始事务操作
            using (var tx = db.Database.BeginTransaction())
            {
                try
                {
                    //将要修改的创建人找回来，避免修改时没有创建人
                    StockInDepot st = db.StockInDepot.FirstOrDefault(p => p.SIDID == id);
                    st.SIDData = 1;
                    tx.Commit();
                }
                catch (Exception e)
                {
                    tx.Rollback();
                    throw new Exception(e.Message);
                }

            }
            return db.SaveChanges();
        }
        /// <summary>
        /// 条件查询
        /// </summary>
        /// <param name="SIDID"></param>
        /// <param name="UsersName"></param>
        /// <param name="PPName"></param>
        /// <param name="StockID"></param>
        /// <param name="SIDDate"></param>
        /// <param name="SIDDeliver"></param>
        /// <param name="SIDData"></param>
        /// <param name="PageIndex"></param>
        /// <param name="PageSize"></param>
        /// <param name="count"></param>
        /// <returns></returns>
        public static List<StockInDepot> Find(string SIDID, string UsersName, string PPID, string DepotID, string SIDDate, int SIDData, int PageIndex, int PageSize, out int count)
        {
            PSSEntities db = new PSSEntities();
        
            var list = from s in db.StockInDepot select s;
            if (SIDID!=null&&SIDID!="")//入库单编号
                 list =list.Where(s=>s.SIDID.Contains(SIDID));
            
            if (UsersName != null && UsersName != "")//创建人
                list = list.Where(s => s.Users.UsersName.Contains(UsersName));

            if (PPID != null && PPID != "")//供应商名称
                list = list.Where(s => s.PPID.Equals(PPID));

            if (DepotID != null && DepotID != "")//仓库名称
                list = list.Where(s => s.DepotID.Equals(DepotID));

            if (SIDDate != null && SIDDate != "")//收货时间
            { 
                DateTime dt = Convert.ToDateTime(SIDDate);
                 list = list.Where(s => s.SIDDate>=dt);
            }
            if (SIDData != -1)//订单状态
                list = list.Where(s => s.SIDData== SIDData);

            count = list.Count();//查询总个数
            return list.OrderBy(s => s.StockID).Skip((PageIndex - 1) * PageSize).Take(PageSize).ToList();
        }
        /// <summary>
        /// 审核入库单【---事务处理---】
        /// </summary>
        /// <param name="id">入库单ID</param>
        /// <returns>是否成功【0失败、1成功】</returns>
        public static int CKStockIn(string id,int userid) {
            PSSEntities db = new PSSEntities();
            int fg = 1;
            using (var tx=db.Database.BeginTransaction())
            {
                try
                {//------------------------------------------------【入库审核】-------------------------------------------------
                    List<StockInDepotDetail> prolist = db.StockInDepotDetail.Where(s => s.SIDID.Equals(id)).ToList();//根据入库单ID找到采购入库订单详单集合
                    StockInDepot st = db.StockInDepot.FirstOrDefault(s => s.SIDID.Equals(id));//根据入库单ID找到采购入库订单
                    ObjectParameter para = new ObjectParameter("DD", "");//生产入库记录单号
                    db.pro_order("InOutDepot", "IODNum", "RK",para);
                    string IODNum = para.Value.ToString();
                    //添加入库记录
                   db.InOutDepot.Add(new InOutDepot() { DepotID =st.DepotID, IODType =1, IODNum = IODNum , IODDate=DateTime.Now, IODUser=userid, IODDesc=st.SIDDesc });
                    db.SaveChanges();//保存
                    int inod = db.InOutDepot.Max(i => i.IODID);//再获取出刚刚出入库记录编号【循环添加出入库详单的时候需要出入库记录编号，所有要先保存】
                   // db.InOutDepot.
                    foreach (StockInDepotDetail item in prolist)//遍历出库单详单
                    {
                        if (db.DepotStock.FirstOrDefault(p => p.DepotID.Equals(st.DepotID) && p.ProID == item.ProID) == null)
                        {//如果指定的仓库中不存在该商品
                            //【为这个仓库添加一个新商品】
                            db.DepotStock.Add(new DepotStock() { DepotID = st.DepotID, ProID = item.ProID, DSAmount = item.SIDAmount, DSPrice = item.SIDPrice });
                        }
                        else
                        {//如果指定仓库存在这个商品，修改这个商品的库存
                            DepotStock ds = db.DepotStock.FirstOrDefault(d => d.ProID == item.ProID&&d.DepotID==st.DepotID);
                            ds.DSAmount = item.SIDAmount + ds.DSAmount;
                        }
                        //添加入库记录详情
                        db.InOutDepotDetail.Add(new InOutDepotDetail() { IODID =inod, ProID =item.ProID, IODDAmount =item.SIDAmount, IODDPrice =item.SIDPrice});
                    }
                    st.SIDData = 1;//修改入库表状态
                    db.SaveChanges();
                    tx.Commit();
                }
                catch (Exception ex)
                {
                    fg = 0;
                    tx.Rollback();
                    throw new Exception(ex.Message);
                }
            }
            return fg;
        }

        /// <summary>
        /// 根据时间查询当前月的采购入库单个数
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        public static int FindByYCount(DateTime dt,int state)
        {
            PSSEntities db = new PSSEntities();
            string sql = "select * from StockInDepot where datepart(MM,SIDDate)=" + dt.Month + " and  datepart(YYYY,SIDDate) = " + dt.Year+ " and SIDData =" + state;
            List<StockInDepot> list = db.Database.SqlQuery<StockInDepot>(sql).ToList();
            return list.Count();
        }
        /// <summary>
        ///查询审核订单和未审核订单数
        /// </summary>
        /// <param name="dt"></param>
        /// <param name="state"></param>
        /// <returns></returns>
        public static int FindStateCount(int state)
        {
            string sql = "select *  from StockInDepot where  SIDData = " + state;
            PSSEntities db = new PSSEntities();
            int count = db.Database.SqlQuery<StockInDepot>(sql).ToList().Count;
            return count;
        }

    }
}