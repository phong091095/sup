namespace shipping.Model.DTO
{
    public class DanhMucDTO
    {
        public int IDDanhMuc { get; set; } = 0;

        public string? TenDanhMuc { get; set; } = default!;
        public int CapDanhMuc { get; set; }
        public string? Path { get; set; }
        public string? TrangThai { get; set; }
        public bool IsLeaf { get; set; }
    }
}
