using HeThongBanCam.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HeThongBanCam.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SupplierController : ControllerBase
    {
        private WebContext db;
        public SupplierController(IConfiguration configuration)
        {
            db = new WebContext(configuration);
        }
        [Route("Get_All")]
        [HttpGet]
        public IActionResult getall()
        {
            try
            {
                var result = db.NhaCungCaps.ToList();
                return Ok(result);
            }
            catch (Exception ex)
            {
                return Ok("Err");
            }
        }
        [Route("get-Supplier-by-id/{id}")]
        [HttpGet]
        public IActionResult GetSupplierById(int id)
        {
            try
            {
                var cate = db.NhaCungCaps.Where(x => x.MaNcc == id).FirstOrDefault();
                return Ok(new { NCC = cate });
            }
            catch (Exception ex)
            {
                return Ok("Err");
            }
        }
        [Route("search")]
        [HttpGet]
        public IActionResult getsearch(string tenncc)
        {
            try
            {
                var result = db.NhaCungCaps.Where(x => x.TenNcc.Contains(tenncc)).ToList();
                return Ok(result);
            }
            catch (Exception ex)
            {
                return Ok("Err");
            }
        }
        [Route("create-Supplier")]
        [HttpPost]
        public IActionResult CreateSupplier([FromBody] NhaCungCap model)
        {

            //model.MaLoai = new Random().Next(1000);
            db.NhaCungCaps.Add(model);
            db.SaveChanges();
            return Ok(new { data = "OK" });
        }
        [Route("update-Supplier")]
        [HttpPost]
        public IActionResult UpdateSupplier([FromBody] NhaCungCap model)
        {
            var obj = db.NhaCungCaps.Where(s => s.MaNcc == model.MaNcc).SingleOrDefault();
            if (obj != null)
            {
                obj.HoaDonNhaps = null;
                obj.TenNcc = model.TenNcc;
                obj.DiaChi = model.DiaChi;
                obj.Sdt = model.Sdt;
                obj.Email = model.Email;
                db.SaveChanges();
            }
            return Ok(new { data = "OK" });
        }
        [Route("delete-Supplier/{mancc}")]
        [HttpDelete]
        public IActionResult DeleteSupplier(int mancc)
        {
            var obj = db.NhaCungCaps.Where(s => s.MaNcc == mancc).SingleOrDefault();
            db.NhaCungCaps.Remove(obj);
            db.SaveChanges();
            return Ok(new { data = "OK" });
        }
    }
}
