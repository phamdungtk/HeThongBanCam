using System;
using System.Collections.Generic;

namespace HeThongBanCam.Models
{
    public partial class NguoiDung
    {
        public int MaNguoiDung { get; set; }
        public byte[] PasswordHash { get; set; }
        public byte[] PasswordSalt { get; set; }
        public string TaiKhoan { get; set; } = string.Empty;
        //public string MatKhau { get; set; } = string.Empty;
    }
}
