using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using UI.Models.DAL;
using UI.Models.Model;

namespace UI.Models.BLL
{
    public class ProductLendBLL
    {
        /// <summary>
        /// 查询所有供货商信息
        /// </summary>
        /// <returns></returns>
        public static List<ProductLend> GetAll()
        {

            return ProductLendDAL.GetAll();
        }
        /// <summary>
        /// 分页查询所有的供货商信息
        /// </summary>
        /// <param name="PageSize"></param>
        /// <param name="PageIndex"></param>
        /// <returns></returns>
        public static List<ProductLend> GetAllPage(int PageSize, int PageIndex)
        {
            return ProductLendDAL.GetAllPage(PageSize, PageIndex);
        }
        /// <summary>
        /// 添加供货商信息
        /// </summary>
        /// <param name="pd"></param>
        /// <returns></returns>
        public static int Add(ProductLend pd) {
            return ProductLendDAL.Add(pd);
        }
        /// <summary>
        /// 删除供应商
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static int Delete(string id)
        {
            return ProductLendDAL.Delete(id);
        }
        /// <summary>
        /// 修改供货商信息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static int Edit(ProductLend pd)
        {
            return ProductLendDAL.Edit(pd);
        }
        /// <summary>
        /// 条件查询
        /// </summary>
        /// <param name="type"></param>
        /// <param name="content"></param>
        /// <returns></returns>
        public static List<ProductLend> Find(int type, string content,int PageIndex)
        {
            return ProductLendDAL.Find(type, content,PageIndex);
        }

        /// <summary>
        /// 添加查询的总数量
        /// </summary>
        /// <param name="type"></param>
        /// <param name="content"></param>
        /// <param name="PageIndex"></param>
        /// <returns></returns>
        public static int FindCount(int type, string content, int PageIndex)
        {
            return ProductLendDAL.FindCount(type,content,PageIndex);
        }
    }
}