

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
using System.Security.Cryptography;
using HeThongBanCam.Services.UserService;

namespace HeThongBanCam.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : Controller
    {
        private WebContext db;

        public static User user = new User();
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
            var userName = _userService.GetMyName();
            return Ok(userName);
        }
        [Route("Get-All")]
        [HttpGet/*, Authorize(Roles = "Admin")*/]
        public IActionResult getalla()
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
        public async Task<ActionResult<User>> Register(NguoiDung request)
        {
            db.NguoiDungs.Add(request);
            CreatePasswordHash(request.TaiKhoan, out byte[] passwordHash, out byte[] passwordSalt);

            user.Username  = request.TaiKhoan;//viết bên fe dùng gmail tránh trùng lặp tài khoản
            //user.Password = request.MatKhau;
            //if (user.Username == request.TaiKhoan)
            //{
            //    return BadRequest("Trùng tài Khoản");
            //}
            request.PasswordHash = passwordHash;
            request.PasswordSalt = passwordSalt;
            db.SaveChanges();
            return Ok(user);
        }
        [HttpPost("login")]
        public async Task<ActionResult<string>> Login(NguoiDung request)
        {

            //if (user.Username != request.TaiKhoan)
            //{
            //    return BadRequest("User not found.");
            //}
            //
            if (!VerifyPasswordHash(request.TaiKhoan, request.PasswordHash, request.PasswordSalt))
            {
                return BadRequest("Wrong password.");
            }
            //return Ok("My crazy token");
            string token = CreateToken(user);

            var refreshToken = GenerateRefreshToken();
            SetRefreshToken(refreshToken);

            return Ok(token);
        }
        [HttpPost("refresh-token")]
        public async Task<ActionResult<string>> RefreshToken()
        {
            var refreshToken = Request.Cookies["refreshToken"];

            if (!user.RefreshToken.Equals(refreshToken))
            {
                return Unauthorized("Invalid Refresh Token.");
            }
            else if (user.TokenExpires < DateTime.Now)
            {
                return Unauthorized("Token expired.");
            }

            string token = CreateToken(user);
            var newRefreshToken = GenerateRefreshToken();
            SetRefreshToken(newRefreshToken);

            return Ok(token);
        }

        private void SetRefreshToken(RefreshToken newRefreshToken)
        {
            var cookieOptions = new CookieOptions
            {
                HttpOnly = true,
                Expires = newRefreshToken.Expires
            };
            Response.Cookies.Append("refreshToken", newRefreshToken.Token, cookieOptions);

            user.RefreshToken = newRefreshToken.Token;
            user.TokenCreated = newRefreshToken.Created;
            user.TokenExpires = newRefreshToken.Expires;
        }
        private RefreshToken GenerateRefreshToken()
        {
            var refreshToken = new RefreshToken
            {
                Token = Convert.ToBase64String(RandomNumberGenerator.GetBytes(64)),
                Expires = DateTime.Now.AddDays(7),
                Created = DateTime.Now
            };

            return refreshToken;
        }
        private string CreateToken(User user)
        {
            List<Claim> claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, user.Username),
                new Claim(ClaimTypes.Role, "Admin")
            };

            var key = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(
                _configuration.GetSection("AppSettings:Token").Value));

            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

            var token = new JwtSecurityToken(
                claims: claims,
                expires: DateTime.Now.AddDays(1),
                signingCredentials: creds);

            var jwt = new JwtSecurityTokenHandler().WriteToken(token);

            return jwt;
        }
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
        private void CreatePasswordHash(string MatKhau, out byte[] passwordHash, out byte[] passwordSalt)
        {
            using (var hmac = new HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(MatKhau));
            }
        }
        private bool VerifyPasswordHash(string MatKhau, byte[] passwordHash, byte[] passwordSalt)
        {
            using (var hmac = new HMACSHA512(passwordSalt))
            {
                var computedHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(MatKhau));
                return computedHash.SequenceEqual(passwordHash);
            }
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

