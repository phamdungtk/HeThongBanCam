using HeThongBanCam.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HeThongBanCam.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private WebContext db;
        public CustomerController(IConfiguration configuration)
        {
            db = new WebContext(configuration);
        }
        [Route("Get_All")]
        [HttpGet]
        public IActionResult getall()
        {
            try
            {
                var result = db.KhachHangs.ToList();
                return Ok(result);
            }
            catch (Exception ex)
            {
                return Ok("Err");
            }
        }
        [Route("create-Customer")]
        [HttpPost]
        public IActionResult CreateCustomer([FromBody] KhachHang model)
        {

            //model.MaLoai = new Random().Next(1000);
            db.KhachHangs.Add(model);
            db.SaveChanges();
            return Ok(new { data = "OK" });
        }
        [Route("update-Customer")]
        [HttpPost]
        public IActionResult UpdateCustomer([FromBody] KhachHang model)
        {
            var obj = db.KhachHangs.Where(s => s.MaKhachHang == model.MaKhachHang).SingleOrDefault();
            if (obj != null)
            {
                obj.TenKhachHang = model.TenKhachHang;
                obj.AnhDaiDien = model.AnhDaiDien;

                obj.Email = model.Email;
                obj.MatKhau = model.MatKhau;
                obj.GioiTinh = model.GioiTinh;
                obj.Sdt = model.Sdt;
                obj.DiaChi = model.DiaChi;
                db.SaveChanges();
            }
            return Ok(new { data = "OK" });
        }
        [Route("delete-Customer/{makh}")]
        [HttpDelete]
        public IActionResult DeleteCustomer(int makh)
        {
            var obj = db.KhachHangs.Where(s => s.MaKhachHang == makh).SingleOrDefault();
            db.KhachHangs.Remove(obj);
            db.SaveChanges();
            return Ok(new { data = "OK" });
        }
    }
}
