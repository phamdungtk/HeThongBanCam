using System;
using System.Collections.Generic;

namespace HeThongBanCam.Models
{
    public partial class NguoiDung
    {
        public int MaNguoiDung { get; set; }
        public string? TaiKhoan { get; set; }
        public string? MatKhau { get; set; }
        public string? TenNguoiDung { get; set; }
        public int? Sdt { get; set; }
        public string? QueQuan { get; set; }
        public string? Gmail { get; set; }
        public string? Salt { get; set; }
    }
}
