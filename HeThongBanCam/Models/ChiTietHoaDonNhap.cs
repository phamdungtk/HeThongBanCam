using System;
using System.Collections.Generic;

namespace HeThongBanCam.Models
{
    public partial class ChiTietHoaDonNhap
    {
        public string MaCthoaDonNhap { get; set; } = null!;
        public string? MaHoaDonNhap { get; set; }
        public int? MaCamera { get; set; }
        public int SoLuong { get; set; }
        public int DonGia { get; set; }

        public virtual Camera? MaCameraNavigation { get; set; }
        public virtual HoaDonNhap? MaHoaDonNhapNavigation { get; set; }
    }
}
