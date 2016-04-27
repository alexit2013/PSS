using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using UI.Models.Model;
using UI.Models.DAL;

namespace UI.Models.BLL
{
    public class CustomerLevelBLL
    {

        /// <summary>
        /// 查询所有的客户等级信息
        /// </summary>
        /// <returns></returns>
        public static List<CustomerLevel> GetAll()
        {
            return CustomerLevelDAL.GetAll();
        }
        /// <summary>
        /// 分页查询客户等级信息
        /// </summary>
        /// <param name="PageIndex"></param>
        /// <param name="PageSize"></param>
        /// <returns></returns>
        public static List<CustomerLevel> GetAllPage(int PageIndex, int PageSize)
        {
            return CustomerLevelDAL.GetAllPage(PageIndex,PageSize);
        }
        /// <summary>
        /// 添加客户等级
        /// </summary>
        /// <param name="cusl"></param>
        /// <returns></returns>
        public static int Add(CustomerLevel cusl)
        {
            return CustomerLevelDAL.Add(cusl);
        }
        /// <summary>
        /// 修改客户等级
        /// </summary>
        /// <param name="cusl"></param>
        /// <returns></returns>
        public static int Edit(CustomerLevel cusl)
        {
            return CustomerLevelDAL.Edit(cusl);
        }
        /// <summary>
        /// 删除客户等级
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static int Delete(int id)
        {
            return CustomerLevelDAL.Delete(id);
        }

    }
}