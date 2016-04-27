using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using UI.Models.Model;
using UI.Models.DAL;

namespace UI.Models.BLL
{
    public class CustomersBLL
    {
        /// <summary>
        /// 查询所有的客户信息
        /// </summary>
        /// <returns></returns>
        public static List<Customers> GetAll()
        {
            return CustomersDAL.GetAll();
        }
        /// <summary>
        /// 分页查询客户信息
        /// </summary>
        /// <param name="PageIndex">当前页</param>
        /// <param name="PageSize">页大小</param>
        /// <returns></returns>
        public static List<Customers> GetAllPage(int PageIndex, int PageSize)
        {
            return CustomersDAL.GetAllPage(PageIndex, PageSize);
        }
        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="cus"></param>
        /// <returns></returns>
        public static int Add(Customers cus)
        {
            return CustomersDAL.Add(cus);
        }
        /// <summary>
        /// 修改客户信息
        /// </summary>
        /// <param name="cus"></param>
        /// <returns></returns>
        public static int Edit(Customers cus)
        {
            return CustomersDAL.Edit(cus);
        }
        /// <summary>
        /// 删除客户信息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static int Delete(string id)
        {
            return CustomersDAL.Delete(id);
        }
    }
}