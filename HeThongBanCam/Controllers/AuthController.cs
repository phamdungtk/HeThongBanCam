using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using HeThongBanCam.Models;
using HeThongBanCam.Helpers;
//using System.Security.Cryptography;
using HeThongBanCam.Services.UserService;
using System.Security.Cryptography;

namespace HeThongBanCam.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : Controller
    {
        private WebContext db;
        public static NguoiDung user = new NguoiDung();
        private IConfiguration _configuration;
        private IUserService _userService;
        public AuthController(IConfiguration configuration, IUserService userService)
        {
            db = new WebContext(configuration);
            _configuration = configuration;
            _userService = userService;
        }
        [HttpGet, Authorize]
        public ActionResult<string> GetMe()
        {
            var TaiKhoan = _userService.GetMyName();
            return Ok(TaiKhoan);
        }
        [Route("Get_All")]
        [HttpGet]
        public IActionResult getall()
        {
            try
            {
                var result = db.NguoiDungs.ToList();
                return Ok(result);
            }
            catch (Exception ex)
            {
                return Ok("Err");
            }
        }
        [HttpPost("register")]
        //public IActionResult Register([FromBody] NguoiDung request)
        public async Task<ActionResult<NguoiDung>> Register(NguoiDung request)
        {
            db.NguoiDungs.Add(request);
            CreatePasswordHash(request.TaiKhoan, out byte[] passwordHash, out byte[] passwordSalt);
            //user.TaiKhoan  = request.TaiKhoan;//viết bên fe dùng gmail tránh trùng lặp tài khoản
            //user.TenNguoiDung = request.TenNguoiDung;
            //user.Sdt = request.Sdt;
            //user.QueQuan = request.QueQuan;
            //user.Gmail = request.Gmail;
            //user.Password = request.MatKhau;
            //if (user.TaiKhoan == request.TaiKhoan)
            //{
            //    return BadRequest("Trùng tài Khoản");
            //}
            request.PasswordHash = passwordHash;
            request.PasswordSalt = passwordSalt;
            db.SaveChanges();
            return Ok(request);
        }
        [HttpPost("login")]
        public IActionResult Login([FromBody] /*async Task<ActionResult<string>>*/ NguoiDung request)
        {
            //if (user.TaiKhoan != request.TaiKhoan)
            //{
            //    return BadRequest("User not found.");
            //}
            //
            var obj = db.NguoiDungs.Where(s => s.TaiKhoan == request.TaiKhoan && s.PasswordHash == request.PasswordHash).SingleOrDefault();
            if (obj != null)
            {
                obj.TenNguoiDung = request.TenNguoiDung;
                db.SaveChanges();
            }
            //var user = db.NguoiDungs.SingleOrDefault(x => x.TenNguoiDung == TenNguoiDung);
            if (!VerifyPasswordHash(request.TaiKhoan, request.PasswordHash, request.PasswordSalt))
            {

                return BadRequest("Wrong password.");
            }
            //return Ok("My crazy token");
            string token = CreateToken(request);
            //var refreshToken = GenerateRefreshToken();
            //SetRefreshToken(refreshToken);
            return Ok(token);
        }
        //[HttpPost("refresh-token")]
        //public async Task<ActionResult<string>> RefreshToken()
        //{
        //    var refreshToken = Request.Cookies["refreshToken"];
        //    if (!user.RefreshToken.Equals(refreshToken))
        //    {
        //        return Unauthorized("Invalid Refresh Token.");
        //    }
        //    else if (user.TokenExpires < DateTime.Now)
        //    {
        //        return Unauthorized("Token expired.");
        //    }
        //    string token = CreateToken(user);
        //    var newRefreshToken = GenerateRefreshToken();
        //    SetRefreshToken(newRefreshToken);
        //    return Ok(refreshToken);
        //}
        //private void SetRefreshToken(RefreshToken newRefreshToken)
        //{
        //    var cookieOptions = new CookieOptions
        //    {
        //        HttpOnly = true,
        //        Expires = newRefreshToken.Expires
        //    };
        //    Response.Cookies.Append("refreshToken", newRefreshToken.Token, cookieOptions);
        //    user.RefreshToken = newRefreshToken.Token;
        //    user.TokenCreated = newRefreshToken.Created;
        //    user.TokenExpires = newRefreshToken.Expires;
        //}
        //private RefreshToken GenerateRefreshToken()
        //{
        //    var refreshToken = new RefreshToken
        //    {
        //        Token = Convert.ToBase64String(RandomNumberGenerator.GetBytes(64)),
        //        Expires = DateTime.Now.AddDays(7),
        //        Created = DateTime.Now
        //    };
        //    return refreshToken;
        //}
        private string CreateToken(NguoiDung user)
        {
            List<Claim> claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, user.TenNguoiDung),
                new Claim(ClaimTypes.Role, "Admin")
            };
            var key = new SymmetricSecurityKey(System.Text.ASCIIEncoding.UTF8.GetBytes(
                _configuration.GetSection("AppSettings:Token").Value));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);
            var token = new JwtSecurityToken(
                claims: claims,
                expires: DateTime.Now.AddDays(1),
                signingCredentials: creds);
            var jwt = new JwtSecurityTokenHandler().WriteToken(token);
            return jwt;
        }
        private void CreatePasswordHash(string TenNguoiDung, out byte[] passwordHash, out byte[] passwordSalt)
        {
            using (var hmac = new HMACSHA256())

            {
                byte[] storedHash = new byte[hmac.HashSize / 8];
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(System.Text.ASCIIEncoding.UTF8.GetBytes(TenNguoiDung));
                string encodedData = Convert.ToBase64String(passwordHash);
                //return encodedData;
            }
        }
        private string HMACSHA256(string message, string secret)
        {
            secret = secret ?? "";
            var encoding = new System.Text.ASCIIEncoding();
            byte[] keyByte = encoding.GetBytes(secret);
            byte[] messageBytes = encoding.GetBytes(message);
            using (var hmacsha256 = new HMACSHA256(keyByte))
            {
                byte[] hashmessage = hmacsha256.ComputeHash(messageBytes);
                string sbinary = "";
                for (int i = 0; i < hashmessage.Length; i++)
                {
                    sbinary += hashmessage[i].ToString("x2"); // hex format
                }
                return sbinary;
            }
        }
        private bool VerifyPasswordHash(string TenNguoiDung, byte[] passwordHash, byte[] passwordSalt)
        {
            using (var hmac = new HMACSHA256(passwordSalt))
            {
                var computedHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(TenNguoiDung));
                return computedHash.SequenceEqual(passwordHash);
            }
        }

        [HttpPost("changepassword")]
        public IActionResult changepassword([FromBody] NguoiDung model)
        {
            var obj = db.NguoiDungs.Where(s => s.TaiKhoan == model.TaiKhoan).SingleOrDefault();
            if (obj != null)
            {
                obj.PasswordHash = model.PasswordHash;// sửa dữ liệu từ byte sang string 

                db.SaveChanges();
            }
            return Ok(new { data = "OK" });
        }
        [Route("delete-auth/{id}")]
        [HttpDelete, Authorize(Roles = "Admin")]
        public IActionResult deleteproduct(int id)
        {
            var obj = db.NguoiDungs.Where(s => s.MaNguoiDung == id).SingleOrDefault();
            //obj.MaLoaiNavigation = null;
            db.NguoiDungs.Remove(obj);
            db.SaveChanges();
            return Ok(new { data = "OK" });
        }

    }
}