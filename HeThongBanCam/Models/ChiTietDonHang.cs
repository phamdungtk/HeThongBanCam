using System;
using System.Collections.Generic;

namespace HeThongBanCam.Models
{
    public partial class ChiTietDonHang
    {
        public int MaChiTietDonHang { get; set; }
        public string? MaDonHang { get; set; }
        public int? MaCamera { get; set; }
        public int SoLuong { get; set; }
        public int? DonGia { get; set; }

        public virtual Camera? MaCameraNavigation { get; set; }
        public virtual DonHang? MaDonHangNavigation { get; set; }
    }
}
