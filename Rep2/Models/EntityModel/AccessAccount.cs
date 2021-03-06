//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Rep2.Models.EntityModel
{
    using System;
    using System.Collections.Generic;
    
    public partial class AccessAccount
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public AccessAccount()
        {
            this.DataBaseServers = new HashSet<DataBaseServer>();
            this.NetworkAccountServers = new HashSet<DataBaseServer>();
        }
    
        public int AccountID { get; set; }
        public string AccountName { get; set; }
        public string AccountPassword { get; set; }
        public int TypeID { get; set; }
    
        public virtual AccessAccountsType AccessAccountsType { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<DataBaseServer> DataBaseServers { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<DataBaseServer> NetworkAccountServers { get; set; }
    }
}
