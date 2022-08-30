namespace HeThongBanCam
{
    public class UserDto
    {
        public string Username { get; set; } = string.Empty;
        public byte[]? PasswordHash { get; set; }
        public byte[]? PasswordSalt { get; set; }
        public bool RememberMe { get; set; }
    }
}
