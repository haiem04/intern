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
    
    public partial class SanPham
    {
        public int maSP { get; set; }
        public string poscode { get; set; }
        public string barcode { get; set; }
        public string ten { get; set; }
        public string nguoiBan { get; set; }
        public Nullable<int> idNhom { get; set; }
        public string xuatXu { get; set; }
        public string thuongHieu { get; set; }
        public Nullable<double> gia { get; set; }
        public string qlyDate { get; set; }
    
        public virtual nhom nhom { get; set; }
    }
}