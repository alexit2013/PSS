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
    
    public partial class SaleDepotDetail
    {
        public int SDDID { get; set; }
        public string SDID { get; set; }
        public Nullable<int> ProID { get; set; }
        public Nullable<decimal> SDDPrice { get; set; }
        public Nullable<int> SDDDiscount { get; set; }
        public Nullable<decimal> SDDDisPrice { get; set; }
        public string SDDDesc { get; set; }
        public Nullable<int> SDDAmount { get; set; }
    
        public virtual Products Products { get; set; }
        public virtual SaleDepot SaleDepot { get; set; }
    }
}
