using System;
using System.Collections.Generic;

namespace HeThongBanCam.Models
{
    public partial class NguoiDung
    {
        public int MaNguoiDung { get; set; }
        public byte[] PasswordHash { get; set; }
        public byte[] PasswordSalt { get; set; }
        public string TaiKhoan { get; set; } = null!;
        public string TenNguoiDung { get; set; }
        public int? Sdt { get; set; }
        public string? QueQuan { get; set; }
        public string? Gmail { get; set; }
    }
}
