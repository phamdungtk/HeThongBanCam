using HeThongBanCam.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HeThongBanCam.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InvoiceDetailsController : ControllerBase
    {
        private WebContext db;
        public InvoiceDetailsController(IConfiguration configuration)
        {
            db = new WebContext(configuration);
        }
        [Route("GetAll")]
        [HttpGet]
        public IActionResult GetAll()
        {
            try
            {
                var result = db.ChiTietDonHangs.ToList();
                return Ok(result);
            }
            catch (Exception ex)
            {
                return Ok("Err");
            }
        }
        [Route("get-InvoiceDetails-by-id/{maDonHang}")]
        [HttpGet]
        public IActionResult GetInvoiceDetailsById(string maDonHang)
        {
            try
            {
                var cate = db.ChiTietDonHangs.Where(x => x.MaDonHang == maDonHang).FirstOrDefault();
                return Ok(new { Invoice = cate });
            }
            catch (Exception ex)
            {
                return Ok("Err");
            }
        }
        //[Route("search")]
        //[HttpGet]
        //public IActionResult getsearch(int macthdn)
        //{
        //    try
        //    {
        //        var result = db.ChiTietDonHangs.Where(x => x.MaChiTietDonHang.Contains(macthdn)).ToList();
        //        return Ok(result);
        //    }
        //    catch (Exception ex)
        //    {
        //        return Ok("Err");
        //    }
        //}
        [Route("delete-InvoiceDetails/{macthdn}")]
        [HttpDelete]
        public IActionResult DeleteInvoiceDetails(int macthdn)
        {
            var obj = db.ChiTietDonHangs.Where(s => s.MaChiTietDonHang == macthdn).SingleOrDefault();
            db.ChiTietDonHangs.Remove(obj);
            db.SaveChanges();
            return Ok(new { data = "OK" });
        }
    }
}
