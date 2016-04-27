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
    
    public partial class Splits
    {
        public Splits()
        {
            this.SplitDetail = new HashSet<SplitDetail>();
        }
    
        public string SplitID { get; set; }
        public string DepotID { get; set; }
        public Nullable<int> ProID { get; set; }
        public Nullable<int> UserID { get; set; }
        public Nullable<System.DateTime> SplitDate { get; set; }
        public string SplitDesc { get; set; }
        public Nullable<int> SplitState { get; set; }
        public Nullable<int> SplitAmount { get; set; }
        public Nullable<decimal> SplitPrice { get; set; }
    
        public virtual Depots Depots { get; set; }
        public virtual Products Products { get; set; }
        public virtual ICollection<SplitDetail> SplitDetail { get; set; }
        public virtual Users Users { get; set; }
    }
}
