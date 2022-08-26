using System;
using System.Collections.Generic;

namespace HeThongBanCam.Models
{
    public partial class Camera
    {
        public Camera()
        {
            ChiTietDonHangs = new HashSet<ChiTietDonHang>();
            ChiTietHoaDonNhaps = new HashSet<ChiTietHoaDonNhap>();
        }

        public int MaCamera { get; set; }
        public string TenCamera { get; set; } = null!;
        public int? MaLoai { get; set; }
        public string DoPhanGiai { get; set; } = null!;
        public string Chip { get; set; } = null!;
        public string OngKinh { get; set; } = null!;
        public string TamQuanSat { get; set; } = null!;
        public string? NguonDien { get; set; }
        public string? HinhAnh { get; set; }
        public int? Gia { get; set; }
        public bool? CameraHot { get; set; }
        public int? SoLuong { get; set; }

        public virtual LoaiCamera? MaLoaiNavigation { get; set; }
        public virtual ICollection<ChiTietDonHang> ChiTietDonHangs { get; set; }
        public virtual ICollection<ChiTietHoaDonNhap> ChiTietHoaDonNhaps { get; set; }
    }
}
