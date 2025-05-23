﻿namespace shipping.Model
{
    public class TrangThaiTong
    {
        public enum TrangThai
        {
            HoatDong,
            NgungHoatDong,
        }
        public enum TrangThaiSP
        {
            HetHang,
            ViPham, 
            DangAn,
            ChuaDang,
            DangHoatDong
        }
        public enum StoreStatus
        {
            HoatDong,
            NgungHoatDong,
            Pending,
            Reject,
            ViPham
        }
    }
}
