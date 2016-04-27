using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using UI.Models.Model;
using UI.Models.DAL;

namespace UI.Models.BLL
{
    public class ProductTypesBLL
    {
        /// <summary>
        /// 查询所有商品类别信息
        /// </summary>
        /// <returns></returns>
        public static List<ProductTypes> GetAll()
        {
            return ProductTypesDAL.GetAll();
        }
        /// <summary>
        /// 分页查询数据
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        public static List<ProductTypes> GetAll(int index)
        {
            return ProductTypesDAL.GetAll(index);
        }
        /// <summary>
        /// 添加商品类型
        /// </summary>
        /// <param name="pt"></param>
        /// <returns></returns>
        public static int AddProType(ProductTypes pt)
        {
            return ProductTypesDAL.AddProType(pt);
        }
        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static int DelProType(int id)
        {
            return ProductTypesDAL.DelProType(id);
        }
        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="pt"></param>
        /// <returns></returns>
        public static int EidtProType(ProductTypes pt)
        {
            return ProductTypesDAL.EidtProType(pt);
        }

        }
}