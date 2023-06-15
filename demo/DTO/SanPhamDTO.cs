using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace demo.DTO
{
    public class SanPhamDTO
    {

        public int maSP { get; set; }
        public string poscode { get; set; }
        public string barcode { get; set; }
        public string ten { get; set; }
        public string nguoiBan { get; set; }
        public int? idNhom { get; set; }
        public string xuatXu { get; set; }
        public string thuongHieu { get; set; }
        public double? gia { get; set; }
        public string qlyDate { get; set; }



    }
}