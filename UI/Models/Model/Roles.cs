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
    
    public partial class Roles
    {
        public Roles()
        {
            this.PopedomRole = new HashSet<PopedomRole>();
            this.UsersRole = new HashSet<UsersRole>();
        }
    
        public int RoleID { get; set; }
        public string RoleName { get; set; }
        public string RoleDesc { get; set; }
    
        public virtual ICollection<PopedomRole> PopedomRole { get; set; }
        public virtual ICollection<UsersRole> UsersRole { get; set; }
    }
}
