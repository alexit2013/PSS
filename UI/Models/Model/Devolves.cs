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
    
    public partial class Devolves
    {
        public Devolves()
        {
            this.DevolveDetail = new HashSet<DevolveDetail>();
        }
    
        public string DevID { get; set; }
        public string DevOutID { get; set; }
        public string DevInID { get; set; }
        public Nullable<int> UserID { get; set; }
        public Nullable<System.DateTime> DevDate { get; set; }
        public string DevDesc { get; set; }
        public Nullable<int> DevState { get; set; }
    
        public virtual Depots Depots { get; set; }
        public virtual Depots Depots1 { get; set; }
        public virtual ICollection<DevolveDetail> DevolveDetail { get; set; }
        public virtual Users Users { get; set; }
    }
}
