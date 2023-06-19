using demo.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace demo.DTO.PMH.Extensions
{
    public static class CauHinhPMHExtension
    {
        public static CauHinhPMHDTO ToDTO(this CauHinhPMH item)
        {
            return new CauHinhPMHDTO
            {
                idCauHinhPMH = item.idCauHinhPMH,
                idDPH = item.idDPH,
                maPMh = item.maPMh,
                seri = item.seri,
                menhGia = item.menhGia,
                giaBan = item.giaBan,
                sl = item.sl,
                time = item.time,
                sldung = item.sldung,
                trangThai = item.trangThai
            };
        }
        public static CauHinhPMH ToModel(this CauHinhPMHDTO item)
        {
            return new CauHinhPMH
            {
                idCauHinhPMH = item.idCauHinhPMH,
                idDPH = item.idDPH,
                maPMh = item.maPMh,
                seri = item.seri,
                menhGia = item.menhGia,
                giaBan = item.giaBan,
                sl = item.sl,
                time = item.time,
                sldung = item.sldung,
                trangThai = item.trangThai
            };
        }
    }
}