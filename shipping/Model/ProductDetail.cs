﻿namespace shipping.Model
{
    public class ProductDetail
    {
        public SanPham SanPham { get; set; }
        public DanhMuc DanhMuc { get; set; } 
        public PhanLoaiDanhMuc PhanLoai { get; set; }
        public List<BienTheSanPhamDTO> BienTheSanPham { get; set; }

    }
    public class BienTheSanPhamDTO
    {
        public BienTheSanPham bienthe {  get; set; }
        public List<GiaTriBienTheSanPhamDto> GiaTriBienTheSanPham { get; set; } = new();
    }
    public class GiaTriBienTheSanPhamDto
    {
        public string TenThuocTinh { get; set; }
        public string TenGiaTri { get; set; }
    }
    public class RequestbodyDTO
    {
        public string? TenSanPham { get; set;}
        public List<string>? TenDanhMuc {  get; set; }
        public List<string>? TenPhanLoai { get; set; }
    }
}
