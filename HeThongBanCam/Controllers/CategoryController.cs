using HeThongBanCam.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HeThongBanCam.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private WebContext db;
        public CategoryController(IConfiguration configuration)
        {
            db = new WebContext(configuration);
        }
        [Route("Get_All")]
        [HttpGet]
        public IActionResult getall()
        {
            try
            {
                var result = db.LoaiCameras.ToList();
                return Ok(result);
            }
            catch (Exception ex)
            {
                return Ok("Err");
            }
        }
        [Route("get-category-by-id/{id}")]
        [HttpGet]
        public IActionResult GetCagtegoryById(int id)
        {
            try
            {
                var result = db.LoaiCameras.Where(x => x.MaLoai == id).ToList();
                return Ok(result);

            }
            catch (Exception ex)
            {
                return Ok("Err");
            }
        }
        [Route("search")]
        [HttpPost]
        public IActionResult GetListSanPhamById(TimKiemSanPham model)
        {
            try
            {
                var result = db.Cameras.Where(x => x.TenCamera.Contains(model.tensp) && x.MaLoai == model.maloai).Skip(model.pageNumber * (model.page - 1)).Take(model.pageNumber).ToList();
                return Ok(result);
            }
            catch (Exception ex)
            {
                return Ok("Err");
            }
        }
        [Route("create-category")]
        [HttpPost]
        public IActionResult CreateItem([FromBody] LoaiCamera model)
        {
           
            //model.MaLoai = new Random().Next(1000);
            db.LoaiCameras.Add(model);
            db.SaveChanges();
            return Ok(new { data = "OK" });
        }
        [Route("update-category")]
        [HttpPost]
        public IActionResult Updatecategory([FromBody] LoaiCamera model)
        {
            var obj = db.LoaiCameras.Where(s => s.MaLoai == model.MaLoai).SingleOrDefault();
            if (obj != null)
            {
                obj.Cameras = null;
                obj.TenLoai = model.TenLoai;
                db.SaveChanges();
            }
            return Ok(new { data = "OK" });
        }
        [Route("delete-category/{maloai}")]
        [HttpDelete]
        public IActionResult Deletecategory(int maloai)
        {
            var obj = db.LoaiCameras.Where(s => s.MaLoai == maloai).SingleOrDefault();
            db.LoaiCameras.Remove(obj);
            db.SaveChanges();
            return Ok(new { data = "OK" });
        }
    }
}
