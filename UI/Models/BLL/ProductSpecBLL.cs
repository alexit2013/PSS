using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using UI.Models.Model;
using UI.Models.DAL;

namespace UI.Models.BLL
{
    public class ProductSpecBLL
    {
        /// <summary>
        /// 查询所有
        /// </summary>
        /// <returns></returns>
        public static List<ProductSpec> GetAll()
        {
            return ProductSpecDAL.GetAll();
        }
        /// <summary>
        /// 分页查询
        /// </summary>
        /// <param name="PageIndex"></param>
        /// <param name="PageSize"></param>
        /// <returns></returns>
        public static List<ProductSpec> GetAllPage(int PageIndex, int PageSize)
        {
            return ProductSpecDAL.GetAllPage(PageIndex,PageSize);
        }
        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="pro"></param>
        /// <returns></returns>
        public static int Add(ProductSpec pro)
        {
            return ProductSpecDAL.Add(pro);
        }
        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="pro"></param>
        /// <returns></returns>
        public static int Edit(ProductSpec pro)
        {
            return ProductSpecDAL.Edit(pro);
        }
        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static int Delete(int id)
        {
            return ProductSpecDAL.Delete(id);
        }
    }
}