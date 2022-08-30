using HeThongBanCam.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HeThongBanCam.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ManufacturerController : ControllerBase
    {
        private WebContext db;
        public ManufacturerController(IConfiguration configuration)
        {
            db = new WebContext(configuration);
        }
        [Route("Get_All")]
        [HttpGet]
        public IActionResult getall()
        {
            try
            {
                var result = db.HangSanXuats.ToList();
                return Ok(result);
            }
            catch (Exception ex)
            {
                return Ok("Err");
            }
        }
        [HttpGet("get-mahang-loai-sp")]
        public IActionResult Gethangxs([FromQuery] int MaHang)
        {
            try
            {
                var result = (from s in db.HangSanXuats
                              join spdr in db.LoaiCameras
                              on s.MaHang equals spdr.MaHang
                              join dr in db.Cameras
                              on spdr.MaLoai equals dr.MaLoai
                              where s.MaHang == MaHang

                              select new { s, spdr, dr }).Select(x => new LoaiCamera()
                              {
                                  MaLoai = x.spdr.MaLoai,
                                  MaHang = x.spdr.MaHang,
                                  //TenLoai = x.spdr.TenLoai,
                                  MoTa = x.spdr.MoTa,
                                  TenLoai = x.dr.TenCamera,
                              }).ToList();
                return Ok(result);
            }
            catch (Exception)
            {

                throw;
            }
        }
        [Route("get-Manufacturer-by-id/{id}")]
        [HttpGet]
        public IActionResult GetCagtegoryById(int id)
        {
            try
            {
                var cate = db.HangSanXuats.Where(x => x.MaHang == id).FirstOrDefault();
                return Ok(new { Manufacturer = cate });
            }
            catch (Exception ex)
            {
                return Ok("Err");
            }
        }
        [Route("search")]
        [HttpPost]
        public IActionResult getsearch(string tenhang)
        {
            try
            {
                var result = db.HangSanXuats.Where(x => x.TenHang.Contains(tenhang)).ToList();
                return Ok(result);
            }
            catch (Exception ex)
            {
                return Ok("Err");
            }
        }
        [Route("create-Manufacturer")]
        [HttpPost]
        public IActionResult CreateManufacturer([FromBody] HangSanXuat model)
        {

            //model.MaLoai = new Random().Next(1000);
            db.HangSanXuats.Add(model);
            db.SaveChanges();
            return Ok(new { data = "OK" });
        }
        [Route("update-Manufacturer")]
        [HttpPost]
        public IActionResult UpdateManufacturer([FromBody] HangSanXuat model)
        {
            var obj = db.HangSanXuats.Where(s => s.MaHang == model.MaHang).SingleOrDefault();
            if (obj != null)
            {
                obj.LoaiCameras = null;
                obj.MaHang = model.MaHang;
                db.SaveChanges();
            }
            return Ok(new { data = "OK" });
        }
        [Route("delete-Manufacturer/{mahang}")]
        [HttpDelete]
        public IActionResult DeleteManufacturer(int mahang)
        {
            var obj = db.HangSanXuats.Where(s => s.MaHang == mahang).SingleOrDefault();
            db.HangSanXuats.Remove(obj);
            db.SaveChanges();
            return Ok(new { data = "OK" });
        }
    }
}
