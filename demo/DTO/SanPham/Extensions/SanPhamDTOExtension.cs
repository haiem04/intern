using demo.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace demo.DTO
{
    public static class SanPhamDTOExtension
    {
        public static SanPhamDTO ToDTO(this SanPham sp)
        {
            return new SanPhamDTO
            {
                maSP = sp.maSP,
                poscode = sp.poscode,
                barcode = sp.barcode,
                ten = sp.ten,
                nguoiBan = sp.nguoiBan,
                idNhom = sp.idNhom,
                xuatXu = sp.xuatXu,
                thuongHieu = sp.thuongHieu,
                gia = sp.gia,
                qlyDate = sp.qlyDate

            };
        }

        public static SanPham ToModel(this SanPhamDTO spdto)
        {
            return new SanPham
            {
                maSP = spdto.maSP,
                poscode = spdto.poscode,
                barcode = spdto.barcode,
                ten = spdto.ten,
                nguoiBan = spdto.nguoiBan,
                idNhom = spdto.idNhom,
                xuatXu = spdto.xuatXu,
                thuongHieu = spdto.thuongHieu,
                gia = spdto.gia,
                qlyDate = spdto.qlyDate

            };
        }

    }
}