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
    
    public partial class CauHinhPMH
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public CauHinhPMH()
        {
            this.PMHs = new HashSet<PMH>();
        }
    
        public int idCauHinhPMH { get; set; }
        public Nullable<int> idDPH { get; set; }
        public string maPMh { get; set; }
        public string seri { get; set; }
        public Nullable<double> menhGia { get; set; }
        public Nullable<double> giaBan { get; set; }
        public Nullable<int> sl { get; set; }
        public Nullable<System.DateTime> time { get; set; }
        public Nullable<int> sldung { get; set; }
        public Nullable<int> trangThai { get; set; }
    
        public virtual DotPhatHanh DotPhatHanh { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<PMH> PMHs { get; set; }
    }
}
