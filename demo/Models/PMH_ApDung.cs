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
    
    public partial class PMH_ApDung
    {
        public int id { get; set; }
        public Nullable<int> idPMH { get; set; }
        public Nullable<int> idDVAD { get; set; }
    
        public virtual DonViApDung DonViApDung { get; set; }
        public virtual PMH PMH { get; set; }
    }
}
