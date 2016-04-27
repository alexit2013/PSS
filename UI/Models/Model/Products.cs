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
    
    public partial class Products
    {
        public Products()
        {
            this.CheckDepotDetail = new HashSet<CheckDepotDetail>();
            this.CustomerOrderDetail = new HashSet<CustomerOrderDetail>();
            this.DepotStock = new HashSet<DepotStock>();
            this.DevolveDetail = new HashSet<DevolveDetail>();
            this.InOutDepotDetail = new HashSet<InOutDepotDetail>();
            this.LostDetail = new HashSet<LostDetail>();
            this.OtherInDepotDetail = new HashSet<OtherInDepotDetail>();
            this.OtherOutDepotDetail = new HashSet<OtherOutDepotDetail>();
            this.PayOffDetail = new HashSet<PayOffDetail>();
            this.ProduceInDepotDeteil = new HashSet<ProduceInDepotDeteil>();
            this.ProduceOutDepotDetail = new HashSet<ProduceOutDepotDetail>();
            this.QuotePriceDetail = new HashSet<QuotePriceDetail>();
            this.SaleDepotDetail = new HashSet<SaleDepotDetail>();
            this.SaleReturnDetail = new HashSet<SaleReturnDetail>();
            this.SplitDetail = new HashSet<SplitDetail>();
            this.Splits = new HashSet<Splits>();
            this.StockDetail = new HashSet<StockDetail>();
            this.StockInDepotDetail = new HashSet<StockInDepotDetail>();
            this.StockReturnDetail = new HashSet<StockReturnDetail>();
        }
    
        public int ProID { get; set; }
        public Nullable<int> PTID { get; set; }
        public Nullable<int> PUID { get; set; }
        public Nullable<int> PCID { get; set; }
        public Nullable<int> PSID { get; set; }
        public string ProName { get; set; }
        public string ProJP { get; set; }
        public string ProTM { get; set; }
        public string ProWorkShop { get; set; }
        public Nullable<int> ProMax { get; set; }
        public Nullable<int> ProMin { get; set; }
        public Nullable<decimal> ProInPrice { get; set; }
        public Nullable<decimal> ProPrice { get; set; }
        public string ProDesc { get; set; }
        public string DepotID { get; set; }
    
        public virtual ICollection<CheckDepotDetail> CheckDepotDetail { get; set; }
        public virtual ICollection<CustomerOrderDetail> CustomerOrderDetail { get; set; }
        public virtual Depots Depots { get; set; }
        public virtual ICollection<DepotStock> DepotStock { get; set; }
        public virtual ICollection<DevolveDetail> DevolveDetail { get; set; }
        public virtual ICollection<InOutDepotDetail> InOutDepotDetail { get; set; }
        public virtual ICollection<LostDetail> LostDetail { get; set; }
        public virtual ICollection<OtherInDepotDetail> OtherInDepotDetail { get; set; }
        public virtual ICollection<OtherOutDepotDetail> OtherOutDepotDetail { get; set; }
        public virtual ICollection<PayOffDetail> PayOffDetail { get; set; }
        public virtual ICollection<ProduceInDepotDeteil> ProduceInDepotDeteil { get; set; }
        public virtual ICollection<ProduceOutDepotDetail> ProduceOutDepotDetail { get; set; }
        public virtual ProductColor ProductColor { get; set; }
        public virtual ProductSpec ProductSpec { get; set; }
        public virtual ProductTypes ProductTypes { get; set; }
        public virtual ProductUnit ProductUnit { get; set; }
        public virtual ICollection<QuotePriceDetail> QuotePriceDetail { get; set; }
        public virtual ICollection<SaleDepotDetail> SaleDepotDetail { get; set; }
        public virtual ICollection<SaleReturnDetail> SaleReturnDetail { get; set; }
        public virtual ICollection<SplitDetail> SplitDetail { get; set; }
        public virtual ICollection<Splits> Splits { get; set; }
        public virtual ICollection<StockDetail> StockDetail { get; set; }
        public virtual ICollection<StockInDepotDetail> StockInDepotDetail { get; set; }
        public virtual ICollection<StockReturnDetail> StockReturnDetail { get; set; }
    }
}
