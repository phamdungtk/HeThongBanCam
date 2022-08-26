using System;
using System.Collections.Generic;

namespace HeThongBanCam.Models
{
    public partial class KhachHang
    {
        public KhachHang()
        {
            DonHangs = new HashSet<DonHang>();
        }

        public int MaKhachHang { get; set; }
        public string TenKhachHang { get; set; } = null!;
        public string? AnhDaiDien { get; set; }
        public string Email { get; set; } = null!;
        public string MatKhau { get; set; } = null!;
        public int GioiTinh { get; set; }
        public string Sdt { get; set; } = null!;
        public string DiaChi { get; set; } = null!;

        public virtual ICollection<DonHang> DonHangs { get; set; }
    }
}
