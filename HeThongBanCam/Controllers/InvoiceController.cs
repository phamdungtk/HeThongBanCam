using HeThongBanCam.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HeThongBanCam.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InvoiceController : ControllerBase
    {
        private WebContext db;
        public InvoiceController(IConfiguration configuration)
        {
            db = new WebContext(configuration);
        }
        [Route("GetAll")]
        [HttpGet]
        public IActionResult GetAll()
        {
            try
            {
                var result = db.DonHangs.ToList();
                return Ok(result);
            }
            catch (Exception ex)
            {
                return Ok("Err");
            }
        }
        [Route("get-Invoice-by-id/{id}")]
        [HttpGet]
        public IActionResult GetCagtegoryById(string id)
        {
            try
            {
                var cate = db.DonHangs.Where(x => x.MaDonHang == id).FirstOrDefault();
                return Ok(new { Invoice = cate });
            }
            catch (Exception ex)
            {
                return Ok("Err");
            }
        }
        [Route("search")]
        [HttpGet]
        public IActionResult getsearch(string mahd)
        {
            try
            {
                var result = db.DonHangs.Where(x => x.MaDonHang.Contains(mahd)).ToList();
                return Ok(result);
            }
            catch (Exception ex)
            {
                return Ok("Err");
            }
        }
        [Route("create-Invoice")]
        [HttpPost]
        public IActionResult CreateInvoice([FromBody] DonHang model)
        {
            model.MaDonHang = Guid.NewGuid().ToString();

            if (model.ChiTietDonHangs.Count > 0)
            {
                foreach (var x in model.ChiTietDonHangs)
                {
                    //x.MaChiTiet = Guid.NewGuid().ToString();
                    x.MaDonHang = model.MaDonHang;
                }
            }
            db.DonHangs.Add(model);
            db.SaveChanges();
            return Ok(new { data = "OK" });
        }
        [Route("update-Invoice")]
        [HttpPost]
        public IActionResult UpdateInvoice([FromBody] HoaDonNew model)
        {
            var obj = db.DonHangs.Where(s => s.MaDonHang == model.MaDonHang).SingleOrDefault();
            if (obj != null)
            {
                obj.NgayTao = model.NgayTao;
                obj.TongTien = model.TongTien;
                obj.TenKhachHang = model.TenKhachHang;
                obj.Sdt = model.Sdt;
                obj.DiaChi = model.DiaChi;
                obj.GhiChu = model.GhiChu;
                obj.TrangThaiDonHang = model.TrangThaiDonHang;
                obj.TrangThaiVanChuyen = model.TrangThaiVanChuyen;
                obj.TrangThaiThanhToan = model.TrangThaiThanhToan;
                if (model.ChiTietDonHangs.Count > 0)
                {
                    foreach (var x in model.ChiTietDonHangs)
                    {
                        //if (x.MaDonHang != null)
                        //{
                            var oj = db.ChiTietDonHangs.Where(s => s.MaChiTietDonHang == x.MaChiTietDonHang).SingleOrDefault();
                            if (obj != null)
                            {
                                oj.SoLuong = x.SoLuong;
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
        [Route("thongketong-Invoice")]
        [HttpPost]
        public decimal tkInvoice()
        {    
            decimal TongTienn = 0;
                TongTienn += decimal.Parse(db.ChiTietDonHangs.Sum(s => s.SoLuong * s.DonGia).ToString());
            return TongTienn;
        }
        [Route("thongke-Invoice{Thang}/{Nam}")]
        [HttpPost]
        public decimal tkNgayInvoice( int Thang, int Nam)
        {
            var obj = db.DonHangs.Where(s => s.NgayTao.Month == Thang && s.NgayTao.Year == Nam);
            decimal TongTienn = 0;
            foreach (var item in obj)
            {
            TongTienn += decimal.Parse(item.ChiTietDonHangs.Sum(s => s.SoLuong * s.DonGia).ToString());
            }
            return TongTienn;
        }
        [Route("delete-Invoice/{mahd}")]
        [HttpDelete]
        public IActionResult DeleteInvoice(string mahd)
        {
            var obj = db.DonHangs.Where(s => s.MaDonHang == mahd).SingleOrDefault();
            db.DonHangs.Remove(obj);
            db.SaveChanges();
            return Ok(new { data = "OK" });
        }
        public partial class HoaDonNew
        {
            public string MaDonHang { get; set; } = null!;
            public int? MaKhachHang { get; set; }
            public DateTime NgayTao { get; set; }
            public int? TongTien { get; set; }
            public string? TenKhachHang { get; set; }
            public string? DiaChi { get; set; }
            public string? Sdt { get; set; }
            public string? GhiChu { get; set; }
            public string TrangThaiDonHang { get; set; } = null!;
            public string TrangThaiVanChuyen { get; set; } = null!;
            public string TrangThaiThanhToan { get; set; } = null!;

            public virtual KhachHang? MaKhachHangNavigation { get; set; }
            public virtual ICollection<ChiTietDonHang> ChiTietDonHangs { get; set; }
        }
        public partial class ChiTietHoaDonNew
        {
            public int MaChiTietDonHang { get; set; }
            public string? MaDonHang { get; set; }
            public int? MaCamera { get; set; }
            public int SoLuong { get; set; }
            public int? DonGia { get; set; }
            public virtual Camera? MaCameraNavigation { get; set; }
            public virtual DonHang? MaDonHangNavigation { get; set; }
        }
    }
    
}

