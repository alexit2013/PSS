using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using UI.Models.Model;
using UI.Models.DAL;

namespace UI.Models.BLL
{
    public class ProductsBLL
    {
       
        /// <summary>
        /// 查询所有
        /// </summary>
        /// <returns></returns>
        public static List<Products> GetAll()
        {
            return ProductsDAL.GetAll();
        }
        /// <summary>
        /// 分页查询
        /// </summary>
        /// <param name="PageIndex"></param>
        /// <param name="PageSize"></param>
        /// <returns></returns>
        public static List<Products> GetAllPage(int PageIndex, int PageSize)
        {
            return ProductsDAL.GetAllPage(PageIndex, PageSize);
        }
        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="pro"></param>
        /// <returns></returns>
        public static int Add(Products pro)
        {
            return ProductsDAL.Add(pro);
        }
        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="pro"></param>
        /// <returns></returns>
        public static int Edit(Products pro)
        {
            return ProductsDAL.Edit(pro);
        }
        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static int Delete(int id)
        {
            return ProductsDAL.Delete(id);
        }

        /// <summary>
        /// 根据ID查询
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static Products GetByID(int id)
        {
            return ProductsDAL.GetByID(id);
        }
        /// <summary>
        /// 根据商品类型ID查询这个类型下面的所有商品
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static List<Products> GetAllByPTID(int id, int PageIndex, int PageSize) {
            return ProductsDAL.GetAllByPTID(id,PageIndex,PageSize);
        }

        public static int GetAllByPTIDCount(int id)
        {
            return ProductsDAL.GetAllByPTIDCount( id);
        }
        /// <summary>
        /// 多条件查询商品
        /// </summary>
        /// <param name="ProName">商品名称</param>
        /// <param name="PTID">商品类别ID</param>
        /// <param name="PUID">商品规格ID</param>
        /// <param name="PCID">商品颜色ID</param>
        /// <param name="PSID">商品单位ID</param>
        /// <param name="count">总条数</param>
        /// <param name="PageIndex">当前页</param>
        /// <param name="PageSize">页大小</param>
        /// <returns></returns>
        public static List<Products> Find(string ProName, int PTID, int PUID, int PCID, int PSID, out int count, int PageIndex, int PageSize)
        {
            return ProductsDAL.Find(ProName, PTID, PUID, PCID, PSID, out count, PageIndex, PageSize);
        }


    }
}