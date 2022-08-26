using System;
using System.Collections.Generic;

namespace HeThongBanCam.Models
{
    public partial class NhaCungCap
    {
        public NhaCungCap()
        {
            HoaDonNhaps = new HashSet<HoaDonNhap>();
        }

        public int MaNcc { get; set; }
        public string TenNcc { get; set; } = null!;
        public string DiaChi { get; set; } = null!;
        public string Sdt { get; set; } = null!;
        public string Email { get; set; } = null!;

        public virtual ICollection<HoaDonNhap> HoaDonNhaps { get; set; }
    }
}
