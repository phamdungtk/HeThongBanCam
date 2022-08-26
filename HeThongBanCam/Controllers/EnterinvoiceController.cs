using HeThongBanCam.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HeThongBanCam.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EnterinvoiceController : ControllerBase
    {
        private WebContext db;
        public EnterinvoiceController(IConfiguration configuration)
        {
            db = new WebContext(configuration);
        }
        [Route("GetAll")]
        [HttpGet]
        public IActionResult GetAll()
        {
            try
            {
                var result = db.HoaDonNhaps.ToList();
                return Ok(result);
            }
            catch (Exception ex)
            {
                return Ok("Err");
            }
        }
        [Route("get-Enterinvoice-by-id/{id}")]
        [HttpGet]
        public IActionResult GetEnterinvoiceById(string id)
        {
            try
            {
                var cate = db.HoaDonNhaps.Where(x => x.MaHoaDonNhap == id).FirstOrDefault();
                return Ok(new { Invoice = cate });
            }
            catch (Exception ex)
            {
                return Ok("Err");
            }
        }
        [Route("search")]
        [HttpGet]
        public IActionResult getsearch(string mahdn)
        {
            try
            {
                var result = db.HoaDonNhaps.Where(x => x.MaHoaDonNhap.Contains(mahdn)).ToList();
                return Ok(result);
            }
            catch (Exception ex)
            {
                return Ok("Err");
            }
        }
        [Route("create-Enterinvoice")]
        [HttpPost]
        public IActionResult CreateEnterinvoice([FromBody] HoaDonNhap model)
        {
            model.MaHoaDonNhap = Guid.NewGuid().ToString();
            if (model.ChiTietHoaDonNhaps.Count > 0)
            {
                foreach (var x in model.ChiTietHoaDonNhaps)
                {
                    x.MaCthoaDonNhap = Guid.NewGuid().ToString();
                    x.MaHoaDonNhap = model.MaHoaDonNhap;
                }
            }
            db.HoaDonNhaps.Add(model);
            db.SaveChanges();
            return Ok(new { data = "OK" });
        }
        [Route("update-Enterinvoice")]
        [HttpPost]
        public IActionResult UpdateEnterinvoice([FromBody] HoaDonNhapNew model)
        {
            var obj = db.HoaDonNhaps.Where(s => s.MaHoaDonNhap == model.MaHoaDonNhap).SingleOrDefault();
            if (obj != null)
            {   
                obj.TongTien = model.TongTien;
                obj.NgayNhap = model.NgayNhap;
               
                if (model.ChiTietHoaDonNhaps.Count > 0)
                {
                    foreach (var x in model.ChiTietHoaDonNhaps)
                    {
                        //if (x.MaDonHang != null)
                        //{
                        var oj = db.ChiTietHoaDonNhaps.Where(s => s.MaCthoaDonNhap == x.MaCthoaDonNhap).SingleOrDefault();
                        if (obj != null)
                        {
                            oj.SoLuong = x.SoLuong;
                            oj.DonGia = x.DonGia;
                            db.SaveChanges();
                        }
                        //}
                        //else
                        //{
                        //    var oj = db.ChiTietDonHangs.Where(s => s.MaChiTietDonHang == x.MaChiTietDonHang).SingleOrDefault();
                        //    db.ChiTietDonHangs.Remove(oj);
                        //    db.SaveChanges();
                        //}
                    }
                }
            }

            return Ok(new { data = "OK" });
        }
        [Route("delete-Enterinvoice/{mahdn}")]
        [HttpDelete]
        public IActionResult DeleteEnterinvoice(string mahdn)
        {
            var obj = db.HoaDonNhaps.Where(s => s.MaHoaDonNhap == mahdn).SingleOrDefault();
            db.HoaDonNhaps.Remove(obj);
            db.SaveChanges();
            return Ok(new { data = "OK" });
        }
        public partial class HoaDonNhapNew
        {
            public string MaHoaDonNhap { get; set; } = null!;
            public int? MaNcc { get; set; }
            public int TongTien { get; set; }
            public DateTime? NgayNhap { get; set; }

            public virtual NhaCungCap? MaNccNavigation { get; set; }
            public virtual ICollection<ChiTietHoaDonNhap> ChiTietHoaDonNhaps { get; set; }
        }
        public partial class ChiTietHoaDonNhapNew
        {
            public string MaCthoaDonNhap { get; set; } = null!;
            public string? MaHoaDonNhap { get; set; }
            public int? MaCamera { get; set; }
            public int SoLuong { get; set; }
            public int DonGia { get; set; }

            public virtual Camera? MaCameraNavigation { get; set; }
            public virtual HoaDonNhap? MaHoaDonNhapNavigation { get; set; }
        }
    }
}
