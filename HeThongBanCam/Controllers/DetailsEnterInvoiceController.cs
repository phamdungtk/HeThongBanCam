using HeThongBanCam.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HeThongBanCam.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DetailsEnterInvoiceController : ControllerBase
    {
        private WebContext db;
        public DetailsEnterInvoiceController(IConfiguration configuration)
        {
            db = new WebContext(configuration);
        }
        [Route("GetAll")]
        [HttpGet]
        public IActionResult GetAll()
        {
            try
            {
                var result = db.ChiTietHoaDonNhaps.ToList();
                return Ok(result);
            }
            catch (Exception ex)
            {
                return Ok("Err");
            }
        }
        [Route("get-DetailsEnterinvoice-by-id/{id}")]
        [HttpGet]
        public IActionResult GetDetailsEnterinvoiceById(string id)
        {
            try
            {
                var cate = db.ChiTietHoaDonNhaps.Where(x => x.MaCthoaDonNhap == id).FirstOrDefault();
                return Ok(new { Invoice = cate });
            }
            catch (Exception ex)
            {
                return Ok("Err");
            }
        }
        [Route("search")]
        [HttpGet]
        public IActionResult getsearch(string macthdn)
        {
            try
            {
                var result = db.ChiTietHoaDonNhaps.Where(x => x.MaCthoaDonNhap.Contains(macthdn)).ToList();
                return Ok(result);
            }
            catch (Exception ex)
            {
                return Ok("Err");
            }
        }
        [Route("delete-DetailsEnterinvoice/{macthdn}")]
        [HttpDelete]
        public IActionResult DeleteDetailsEnterinvoice(string macthdn)
        {
            var obj = db.ChiTietHoaDonNhaps.Where(s => s.MaCthoaDonNhap == macthdn).SingleOrDefault();
            db.ChiTietHoaDonNhaps.Remove(obj);
            db.SaveChanges();
            return Ok(new { data = "OK" });
        }
    }
}
