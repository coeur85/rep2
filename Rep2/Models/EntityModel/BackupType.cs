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
    
    public partial class BackupType
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public BackupType()
        {
            this.ServerBackups = new HashSet<ServerBackup>();
        }
    
        public int typeID { get; set; }
        public string typeName { get; set; }
        public string FilesExtintion { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ServerBackup> ServerBackups { get; set; }
    }
}
