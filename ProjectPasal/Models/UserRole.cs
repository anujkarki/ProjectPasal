//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace ProjectPasal.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class UserRole
    {
        public int Id { get; set; }
        public Nullable<int> UserId { get; set; }
        public Nullable<int> RoleId { get; set; }
    
        public virtual tblRole tblRole { get; set; }
        public virtual tblUser tblUser { get; set; }
    }
}
