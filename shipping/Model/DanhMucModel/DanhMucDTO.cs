namespace shipping.Model.DanhMucModel
{
    public class DanhMucDTO
    {

        public string TenDanhMuc { get; set; } = default!;
        public int CapDanhMuc { get; set; }
        public string? TrangThai { get; set; }
        public bool IsLeaf { get; set; }
    }
}
