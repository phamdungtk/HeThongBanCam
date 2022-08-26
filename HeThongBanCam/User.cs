namespace HeThongBanCam
{
    public class User
    {
        public string TaiKhoan { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public string TenNguoiDung { get; set; } = string.Empty;
        public int Sdt { get; set; } 
        public string QueQuan { get; set; } = string.Empty;
        public string Gmail { get; set; } = string.Empty;
        public byte[] PasswordHash { get; set; }
        public byte[] PasswordSalt { get; set; }
        public string RefreshToken { get; set; } = string.Empty;
        public DateTime TokenCreated { get; set; }
        public DateTime TokenExpires { get; set; }
    }
}
