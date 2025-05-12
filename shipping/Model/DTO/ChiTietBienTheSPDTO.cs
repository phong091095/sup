namespace shipping.Model.DTO
{
    public class ChiTietBienTheSPDTO
    {
        public int IDChiTietBTSanPham { get; set; }

        public string IDBienTheSanPham { get; set; } = default!;
        public int IDGiaTriBienTheSanPham { get; set; }
        public string MoTa { get; set; } = default!;
    }
}
