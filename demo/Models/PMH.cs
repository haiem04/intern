//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace demo.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class PMH
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public PMH()
        {
            this.PMH_ApDung = new HashSet<PMH_ApDung>();
        }
    
        public int idPMH { get; set; }
        public string maSeri { get; set; }
        public Nullable<int> idCauHinhPMH { get; set; }
    
        public virtual CauHinhPMH CauHinhPMH { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<PMH_ApDung> PMH_ApDung { get; set; }
    }
}
