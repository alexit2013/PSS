//------------------------------------------------------------------------------
// <auto-generated>
//     此代码已从模板生成。
//
//     手动更改此文件可能导致应用程序出现意外的行为。
//     如果重新生成代码，将覆盖对此文件的手动更改。
// </auto-generated>
//------------------------------------------------------------------------------

namespace UI.Models.Model
{
    using System;
    using System.Collections.Generic;
    
    public partial class ProductLend
    {
        public ProductLend()
        {
            this.StockInDepot = new HashSet<StockInDepot>();
            this.StockReturn = new HashSet<StockReturn>();
            this.Stocks = new HashSet<Stocks>();
        }
    
        public string PPID { get; set; }
        public string PPName { get; set; }
        public string PPCompany { get; set; }
        public string PPMan { get; set; }
        public string PPTel { get; set; }
        public string PPAddress { get; set; }
        public string PPFax { get; set; }
        public string PPEmail { get; set; }
        public string PPUrl { get; set; }
        public string PPBank { get; set; }
        public string PPGoods { get; set; }
        public string PPDesc { get; set; }
    
        public virtual ICollection<StockInDepot> StockInDepot { get; set; }
        public virtual ICollection<StockReturn> StockReturn { get; set; }
        public virtual ICollection<Stocks> Stocks { get; set; }
    }
}