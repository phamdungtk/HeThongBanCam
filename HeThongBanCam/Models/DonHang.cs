using System;
using System.Collections.Generic;

namespace HeThongBanCam.Models
{
    public partial class DonHang
    {
        public DonHang()
        {
            ChiTietDonHangs = new HashSet<ChiTietDonHang>();
        }

        public string MaDonHang { get; set; } = null!;
        public int? MaKhachHang { get; set; }
        public DateTime NgayTao { get; set; }
        public int? TongTien { get; set; }
        public string? TenKhachHang { get; set; }
        public string? DiaChi { get; set; }
        public string? Sdt { get; set; }
        public string? GhiChu { get; set; }
        public string TrangThaiDonHang { get; set; } = null!;
        public string TrangThaiVanChuyen { get; set; } = null!;
        public string TrangThaiThanhToan { get; set; } = null!;

        public virtual KhachHang? MaKhachHangNavigation { get; set; }
        public virtual ICollection<ChiTietDonHang> ChiTietDonHangs { get; set; }
    }
}
