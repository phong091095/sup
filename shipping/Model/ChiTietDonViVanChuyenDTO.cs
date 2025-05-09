﻿using System.ComponentModel.DataAnnotations;

namespace shipping.Model
{
    public class ChiTietDonViVanChuyenDTO
    {
        //Danh sách Chi tiết
        public List<ChiTietDVVanChuyen> dsdetail { get; set; }   = new List<ChiTietDVVanChuyen> { };
        // Thuộc tính từ DonViVanChuyen
        public DonViVanChuyen dvvc { get; set; } = new DonViVanChuyen();
    }
    public enum TrangThai
    {
        HoatDong,
        NgungHoatDong
    }
}
