using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using UI.Models.Model;

namespace UI.Models.DAL
{
    public class SystemDAL
    {
        /// <summary>
        /// 数据备份
        /// </summary>
        public static void SaveDataBase()
        {
            try
            {
                //masterEntities db = new masterEntities();
                ////db.Database.ExecuteSqlCommand(@"backup database pss to disk ='I:\pss_diff.bak' with format, name='tmm'
                ////                                                            backup log pss to disk='I:\pss_log.bak' with format,name='tmm'");
                //db.proc_SaveDataBase();
                //db.SaveChanges();   
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// 数据 还原
        /// </summary>
        public static void RestoreDataBase()
        {
            try
            {
                //masterEntities db = new masterEntities();
                ////db.Database.ExecuteSqlCommand(@"restore database pss from disk='I:\pss_diff.bak'");
                //db.proc_RestoreDataBase();
                //db.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
           
        }
    }
}