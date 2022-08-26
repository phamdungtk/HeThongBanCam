using HeThongBanCam.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HeThongBanCam.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private WebContext db;
        public ProductsController(IConfiguration configuration)
        {
            db = new WebContext(configuration);
        }
        [Route("Get-All")]
        [HttpGet]
        public IActionResult getall()
        {
            try
            {
                var result = db.Cameras.ToList();
                return Ok(result);
            }
            catch (Exception ex)
            {
                return Ok("Err");
            }
        }
        [Route("Get-All-50k")]
        [HttpGet]
        public IActionResult getall50k()
        {
            try
            {
                var result = db.Cameras.Where(x => x.Gia == 50000).ToList();
                return Ok(result);
            }
            catch (Exception ex)
            {
                return Ok("Err");
            }
        }
        [Route("get-product-by-id/{id}")]
        [HttpGet]
        public IActionResult GetProductById(int id)
        {
            try
            {
                var sp = db.Cameras.Where(x => x.MaCamera == id).FirstOrDefault();             
                return Ok(new { products = sp });
            }
            catch (Exception ex)
            {
                return Ok("Err");
            }
        }
        [Route("search")]
        [HttpGet]
        public IActionResult getsearch(string tensp)
        {
            try
            {
                var result = db.Cameras.Where(x => x.TenCamera.Contains(tensp)).ToList();
                return Ok(result);
            }
            catch (Exception ex)
            {
                return Ok("Err");
            }
        }
        [Route("get-product-related-by-id/{id}")]
        [HttpGet]
        public IActionResult GetProducRelatedtById(int id)
        {
            try
            {
                var result = db.Cameras.Where(x => x.MaCamera == id).FirstOrDefault();
                var result2 = db.Cameras.Where(x => x.MaLoai == result.MaLoai).ToList();

                return Ok(result2);
            }
            catch (Exception ex)
            {
                return Ok("Err");
            }
        }

        [Route("create-Product")]
        [HttpPost, Authorize(Roles = "Admin")]
        public IActionResult createproducts([FromBody] Camera model)
        {
            db.Cameras.Add(model);
            db.SaveChanges();          
            return Ok(new { data = "OK" });
        }
        [Route("update-product")]
        [HttpPost, Authorize(Roles = "Admin")]
        public IActionResult updateproduct([FromBody] Camera model)
        {
            var obj = db.Cameras.Where(s => s.MaCamera == model.MaCamera).SingleOrDefault();
            if (obj != null)
            {
                obj.MaLoaiNavigation = null;
                obj.TenCamera = model.TenCamera;
                obj.DoPhanGiai = model.DoPhanGiai;
                obj.Chip = model.Chip;
                obj.OngKinh = model.OngKinh;
                obj.TamQuanSat = model.TamQuanSat;
                obj.NguonDien = model.NguonDien;
                obj.HinhAnh = model.HinhAnh;
                obj.Gia = model.Gia;
                obj.CameraHot = model.CameraHot;
                obj.SoLuong = model.SoLuong;
                db.SaveChanges();
            }
            return Ok(new { data = "OK" });
        }
        [Route("delete-product/{masp}")]
        [HttpDelete, Authorize(Roles = "Admin")]
        public IActionResult deleteproduct(int masp)
        {
            var obj = db.Cameras.Where(s => s.MaCamera == masp).SingleOrDefault();


            obj.MaLoaiNavigation = null;
            db.Cameras.Remove(obj);
            db.SaveChanges();
            return Ok(new { data = "OK" });
        }
    }
}
