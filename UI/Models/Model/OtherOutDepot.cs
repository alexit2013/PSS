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
    
    public partial class OtherOutDepot
    {
        public OtherOutDepot()
        {
            this.OtherOutDepotDetail = new HashSet<OtherOutDepotDetail>();
        }
    
        public string OODID { get; set; }
        public string DepotID { get; set; }
        public Nullable<System.DateTime> OODDate { get; set; }
        public Nullable<int> UserID { get; set; }
        public Nullable<int> OODState { get; set; }
        public string OODDesc { get; set; }
    
        public virtual Depots Depots { get; set; }
        public virtual Users Users { get; set; }
        public virtual ICollection<OtherOutDepotDetail> OtherOutDepotDetail { get; set; }
    }
}
