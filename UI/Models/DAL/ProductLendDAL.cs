using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using UI.Models.Model;

namespace UI.Models.DAL
{
    public class ProductLendDAL
    {
        /// <summary>
        /// 查询所有供货商信息
        /// </summary>
        /// <returns></returns>
        public static List<ProductLend> GetAll() {
            PSSEntities db = new PSSEntities();
            return db.ProductLend.Select(p => p).ToList<ProductLend>();
        }
        /// <summary>
        /// 分页查询所有的供货商信息
        /// </summary>
        /// <param name="PageSize"></param>
        /// <param name="PageIndex"></param>
        /// <returns></returns>
        public static List<ProductLend> GetAllPage(int PageSize,int PageIndex)
        {
            PSSEntities db = new PSSEntities();
            return db.ProductLend.Select(p => p).OrderBy(p=>p.PPID).Skip((PageIndex-1)*PageSize).Take(PageSize).ToList<ProductLend>();
        }
        /// <summary>
        /// 添加供货商
        /// </summary>
        /// <param name="pd"></param>
        /// <returns></returns>
        public static int Add(ProductLend pd)
        {
            PSSEntities db = new PSSEntities();
            db.ProductLend.Add(pd);
            return db.SaveChanges();

        }
        /// <summary>
        /// 删除供应商
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static int Delete(string id)
        {
            PSSEntities db = new PSSEntities();
            db.ProductLend.Remove(db.ProductLend.FirstOrDefault(p => p.PPID == id));
            return db.SaveChanges();
        }
        /// <summary>
        /// 修改供货商信息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static int Edit(ProductLend pd)
        {
            PSSEntities db = new PSSEntities();
            db.Entry<ProductLend>(pd).State = System.Data.Entity.EntityState.Modified;
            return db.SaveChanges();
        }
        /// <summary>
        /// 条件查询供货商信息
        /// </summary>
        /// <param name="type"></param>
        /// <param name="content"></param>
        /// <returns></returns>
        public static List<ProductLend> Find(int type,string content,int PageIndex)
        {
           
            int PageSize = 10;
            List<ProductLend> list = null;
            PSSEntities db = new PSSEntities();
            if (type > 0 && content.Length>0){
                switch (type)
                {
                    case 1:
                        list= db.ProductLend.Where(p=>p.PPID.Contains(content)).OrderBy(p => p.PPID).Skip((PageIndex-1)*PageSize).Take(PageSize).ToList<ProductLend>();
                        break;
                    case 2:
                        list = db.ProductLend.Where(p => p.PPName.Contains(content)).OrderBy(p => p.PPID).Skip((PageIndex - 1) * PageSize).Take(PageSize).ToList<ProductLend>();
                        break;
                    case 3:
                        list = db.ProductLend.Where(p => p.PPCompany.Contains(content)).OrderBy(p => p.PPID).Skip((PageIndex - 1) * PageSize).Take(PageSize).ToList<ProductLend>();
                        break;
                    case 4:
                        list = db.ProductLend.Where(p => p.PPMan.Contains(content)).OrderBy(p => p.PPID).Skip((PageIndex - 1) * PageSize).Take(PageSize).ToList<ProductLend>();
                        break;
                    case 5:
                        list = db.ProductLend.Where(p => p.PPTel.Contains(content)).OrderBy(p => p.PPID).Skip((PageIndex - 1) * PageSize).Take(PageSize).ToList<ProductLend>();
                        break;
                    default://
                        list = db.ProductLend.Where(p => p.PPAddress.Contains(content)).OrderBy(p => p.PPID).Skip((PageIndex - 1) * PageSize).Take(PageSize).ToList<ProductLend>();
                        break;
                }
            }
            else if(type == 0 && content != "")
            {//没有选择类型，但是有输入值的时候
                list = db.ProductLend.Where(p => p.PPID.Contains(content)
                || p.PPName.Contains(content)
                || p.PPCompany.Contains(content)
                || p.PPMan.Contains(content)
                || p.PPTel.Contains(content)
                || p.PPAddress.Contains(content)
                ).OrderBy(p => p.PPID).Skip((PageIndex - 1) * PageSize).Take(PageSize).ToList<ProductLend>();
            }
            else
            {
                list = db.ProductLend.OrderBy(p => p.PPID).Skip((PageIndex - 1) * PageSize).Take(PageSize).ToList<ProductLend>();
            }
            return list;
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
            List<ProductLend> list = null;
            PSSEntities db = new PSSEntities();
            if (type > 0 && content.Length > 0)
            {
                switch (type)
                {
                    case 1:
                        list = db.ProductLend.Where(p => p.PPID.Contains(content)).ToList<ProductLend>();
                        break;
                    case 2:
                        list = db.ProductLend.Where(p => p.PPName.Contains(content)).ToList<ProductLend>();
                        break;
                    case 3:
                        list = db.ProductLend.Where(p => p.PPCompany.Contains(content)).ToList<ProductLend>();
                        break;
                    case 4:
                        list = db.ProductLend.Where(p => p.PPMan.Contains(content)).ToList<ProductLend>();
                        break;
                    case 5:
                        list = db.ProductLend.Where(p => p.PPTel.Contains(content)).ToList<ProductLend>();
                        break;
                    default://
                        list = db.ProductLend.Where(p => p.PPAddress.Contains(content)).ToList<ProductLend>();
                        break;
                }
            }
            else if (type == 0 && content != "")
            {//没有选择类型，但是有输入值的时候
                list = db.ProductLend.Where(p => p.PPID.Contains(content)
                || p.PPName.Contains(content)
                || p.PPCompany.Contains(content)
                || p.PPMan.Contains(content)
                || p.PPTel.Contains(content)
                || p.PPAddress.Contains(content)
                ).OrderBy(p => p.PPID).ToList<ProductLend>();
            }
            else
            {
                list = db.ProductLend.Select(pd=>pd).ToList<ProductLend>();
            }
            return list.Count;
        }
    }
}