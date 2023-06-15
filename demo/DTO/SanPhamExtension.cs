using demo.Models;

namespace demo.DTO
{
    public static class SanPhamExtension
    {
        public static SanPhamDTO ToDTO(this SanPham sanPham)
        {
            return new SanPhamDTO
            {
             
                maSP = sanPham.maSP,
                poscode = sanPham.poscode,
                barcode = sanPham.barcode,
                ten = sanPham.ten,
                nguoiBan = sanPham.nguoiBan,
                idNhom = sanPham.idNhom.Value,
                xuatXu = sanPham.xuatXu,
                thuongHieu = sanPham.thuongHieu,
                gia = (double)sanPham.gia,
                qlyDate = sanPham.qlyDate
            };
        }


    }
}