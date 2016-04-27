using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using UI.Models.DAL;
using UI.Models.Model;

namespace UI.Models.BLL
{
    public class DepotsBLL
    {
       /// <summary>
       /// 查询所有的仓库信息
       /// </summary>
       /// <returns></returns>
        public static List<Depots> GetAll()
        {
             return DepotsDAL.GetAll();
        }

        public static List<Depots> GetAllPage(int PageIndex, int PageSize)
        {
            return DepotsDAL.GetAllPage(PageIndex,PageSize);
        }

        public static int AddDepots(Depots dep)
        {
            return DepotsDAL.AddDepots(dep);
        }
        public static int DelDepots(string id)
        {
            return DepotsDAL.DelDepots(id);
        }
        public static int EditDepot(Depots dp)
        {
            return DepotsDAL.EditDepot(dp);
        }

     }
}