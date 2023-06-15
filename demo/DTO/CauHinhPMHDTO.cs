using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace demo.DTO
{
    public class CauHinhPMHDTO
    {
        public int idCauHinhPMH { get; set; }
        public int idDPH { get; set; }
        public string maPMh { get; set; }
        public string seri { get; set; }
        public float menhGia { get; set; }
        public float giaBan { get; set; }
        public int sl { get; set; }
        public DateTime? time { get; set; }
        public int sldung { get; set; }
        public int trangThai { get; set; }
    }
}