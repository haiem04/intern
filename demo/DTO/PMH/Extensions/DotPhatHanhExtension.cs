using demo.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace demo.DTO
{
    public static class DotPhatHanhExtension
    {
        public static DotPhatHanhDTO ToDTO(this DotPhatHanh dph)
        {
            return new DotPhatHanhDTO
            {
                idDPH = dph.idDPH,
                tenDPH = dph.tenDPH,
                idLoai = dph.idLoai,
                idDVPH = dph.idDVPH,
                dienGiai = dph.dienGiai,
                trangThai = dph.trangThai
            };
        }

        public static DotPhatHanh ToModel(this DotPhatHanhDTO dphDTO)
        {
            return new DotPhatHanh
            {
                idDPH = dphDTO.idDPH,
                tenDPH = dphDTO.tenDPH,
                idLoai = dphDTO.idLoai,
                idDVPH = dphDTO.idDVPH,
                dienGiai = dphDTO.dienGiai,
                trangThai = dphDTO.trangThai
            };
        }
    }
}