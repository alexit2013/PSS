using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using UI.Models.Model;
using UI.Models.DAL;

namespace UI.Models.BLL
{
    public class SystemBLL
    {
        /// <summary>
        /// 数据备份
        /// </summary>
        public static void SaveDataBase() {
             SystemDAL.SaveDataBase();
        }
        /// <summary>
        /// 数据还原
        /// </summary>
        public static void RestoreDataBase()
        {
             SystemDAL.RestoreDataBase();
        }


    }
}