using System;
using System.Collections.Generic;

namespace HeThongBanCam.Models
{
    public partial class HoaDonNhap
    {
        public HoaDonNhap()
        {
            ChiTietHoaDonNhaps = new HashSet<ChiTietHoaDonNhap>();
        }

        public string MaHoaDonNhap { get; set; } = null!;
        public int? MaNcc { get; set; }
        public int TongTien { get; set; }
        public DateTime? NgayNhap { get; set; }

        public virtual NhaCungCap? MaNccNavigation { get; set; }
        public virtual ICollection<ChiTietHoaDonNhap> ChiTietHoaDonNhaps { get; set; }
    }
}
