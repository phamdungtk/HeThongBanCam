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
using Microsoft.EntityFrameworkCore;

namespace HeThongBanCam.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : Controller
    {
        private WebContext db;
        public static UserDto user = new UserDto();
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
        private async Task<Responsive> Validate(NguoiDung nd, bool isUpdate = false)
        {
            var message = "";
            if (nd == null)
            {
                message = "Không thể bỏ trống dữ liệu";
                return new Responsive(message);
            }
            if (nd.TaiKhoan == null)
            {
                message = "Không thể bỏ trống dữ liệu";
                return new Responsive(message);
            }
            if (nd.MatKhau == null)
            {
                message = "Không thể bỏ trống dữ liệu";
                return new Responsive(message);
            }
            if (nd.TenNguoiDung == null)
            {
                message = "Không thể bỏ trống dữ liệu";
                return new Responsive(message);
            }
            if (isUpdate == true)
            {
                var entity = db.NguoiDungs.Find(nd.TaiKhoan);
                if (entity != null && entity.TaiKhoan != nd.TaiKhoan)
                {
                    message = "Tài Khoản đã tồn tại";
                    return new Responsive(message);
                }
            }
            if (isUpdate == false)
            {
                var entity = db.NguoiDungs.Find(nd.TaiKhoan);
                if (entity != null)
                {
                    message = "Tài Khoản đã tồn tại";
                    return new Responsive(message);
                }
            }
            return new Responsive("", true);
        }
        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] NguoiDung request)
        {
            //try
            //{
            //    if (request == null)
            //    {
            //        return BadRequest("error");
            //    }
            //    var isvalid = await Validate(request);
            //    if (!isvalid.isOk)
            //    {
            //        return BadRequest(isvalid.Message);
            //    }
            //    else
            //    {
            //        request.TenNguoiDung = request.TenNguoiDung;
            //        db.NguoiDungs.Add(request);

            //        db.SaveChanges();
            //        return Ok(request);
            //    }
            //    //db.Cameras.Add(model);           
            //}
            //catch (Exception ex)
            //{
            //    return BadRequest(ex.Message);
            //}
            request.TenNguoiDung = request.TenNguoiDung;
            db.NguoiDungs.Add(request);

            db.SaveChanges();
            return Ok(request);
        }

        [HttpPost("login")]

        public async Task<ActionResult<string>> Login(NguoiDung request)
        {
            //if (request == null)
            //{
            //    return BadRequest("error");
            //}
            //var isvalid = await Validate(request);
            //if (!isvalid.isOk)
            //{
            //    return BadRequest(isvalid.Message);
            //}
            //else
            //{
            //    var userName = request.TaiKhoan;
            //    var Pass = request.MatKhau;
            //    var user = db.NguoiDungs.SingleOrDefault(x => x.TaiKhoan == userName && x.MatKhau == Pass);
            //    if (user == null)
            //    {
            //        return Ok(new { message = "Tài khoản hoặc mật khẩu không chính xác" });
            //    }

            //    var obj = db.NguoiDungs.Where(s => s.TaiKhoan == request.TaiKhoan && s.MatKhau == request.MatKhau).SingleOrDefault();
            //    if (obj != null)
            //    {
            //        obj.TenNguoiDung = request.TenNguoiDung;
            //        db.SaveChanges();
            //    }
            //    string token = CreateToken(request);
            //    return Ok(token);
            //}
            var userName = request.TaiKhoan;
            var Pass = request.MatKhau;
            var user = db.NguoiDungs.SingleOrDefault(x => x.TaiKhoan == userName && x.MatKhau == Pass);
            if (user == null)
            {
                return Ok(new { message = "Tài khoản hoặc mật khẩu không chính xác" });
            }

            var obj = db.NguoiDungs.Where(s => s.TaiKhoan == request.TaiKhoan && s.MatKhau == request.MatKhau).SingleOrDefault();
            if (obj != null)
            {
                obj.TenNguoiDung = request.TenNguoiDung;
                db.SaveChanges();
            }
            string token = CreateToken(request);
            return Ok(token);
        }    
        private string CreateToken(NguoiDung user)
        {
            List<Claim> claims = new List<Claim>
            {
                new Claim(ClaimTypes.Actor,user.TaiKhoan),
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
        [HttpPost("changepassword")]
        public IActionResult changepassword([FromBody] NguoiDung model)
        {
            var obj = db.NguoiDungs.Where(s => s.TaiKhoan == model.TaiKhoan).SingleOrDefault();
            if (obj != null)
            {
                obj.MatKhau = model.MatKhau;

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