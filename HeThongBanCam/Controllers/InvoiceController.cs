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
        private async Task<Responsive> Validate(DonHang dh, bool isUpdate = false)
        {
            var message = "";
            if (dh == null)
            {
                message = "Không thể bỏ trống dữ liệu";
                return new Responsive(message);
            }
            if (dh.MaDonHang == null)
            {
                message = "Không thể bỏ trống dữ liệu";
                return new Responsive(message);
            }
            if (isUpdate == true)
            {
                var entity = db.DonHangs.Find(dh.MaDonHang);
                if (entity != null && entity.MaDonHang != dh.MaDonHang)
                {
                    message = "Mã Cam đã tồn tại";
                    return new Responsive(message);
                }
            }
            if (isUpdate == false)
            {
                var entity = db.DonHangs.Find(dh.MaDonHang);
                if (entity != null)
                {
                    message = "Mã đã tồn tại";
                    return new Responsive(message);
                }
            }
            return new Responsive("", true);
        }
        [Route("create-Invoice")]
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] DonHang model)
        {
            try
            {
                model.MaDonHang = Guid.NewGuid().ToString();
                if (model == null)
                {
                    return BadRequest("error");
                }
                var isvalid = await Validate(model);
                if (!isvalid.isOk)
                {
                    return BadRequest(isvalid.Message);
                }
                else
                {
                    if (model.ChiTietDonHangs.Count > 0)
                    {
                        foreach (var x in model.ChiTietDonHangs)
                        {
                            //x.MaChiTiet = Guid.NewGuid().ToString();
                            x.MaDonHang = model.MaDonHang;
                        }
                    }
                    db.DonHangs.Add(model);
                    var reuslt = db.SaveChanges();
                    return Ok(new { data = reuslt, status = 201 });
                }
                //db.Cameras.Add(model);           
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            //model.MaDonHang = Guid.NewGuid().ToString();

            //if (model.ChiTietDonHangs.Count > 0)
            //{
            //    foreach (var x in model.ChiTietDonHangs)
            //    {
            //        //x.MaChiTiet = Guid.NewGuid().ToString();
            //        x.MaDonHang = model.MaDonHang;
            //    }
            //}
            //db.DonHangs.Add(model);
            //db.SaveChanges();
            //return Ok(new { data = "OK" });
        }
        [Route("update-Invoice")]
        [HttpPost]
        public async Task<IActionResult> Update([FromQuery] string id, [FromBody] DonHang model)
        {
            try
            {
                var entity = db.DonHangs.FirstOrDefault(x => x.MaDonHang == id);
                if (entity == null)
                {
                    return BadRequest("error");
                }
                var isValid = await Validate(model, true);
                if (!isValid.isOk)
                {
                    return BadRequest(isValid.Message);
                }
                else
                {
                    entity.NgayTao = model.NgayTao;
                    //entity.TongTien = model.TongTien;
                    entity.TenKhachHang = model.TenKhachHang;
                    entity.Sdt = model.Sdt;
                    entity.DiaChi = model.DiaChi;
                    entity.GhiChu = model.GhiChu;
                    entity.TrangThaiDonHang = model.TrangThaiDonHang;
                    entity.TrangThaiVanChuyen = model.TrangThaiVanChuyen;
                    entity.TrangThaiThanhToan = model.TrangThaiThanhToan;
                    if (model.ChiTietDonHangs.Count > 0)
                    {
                        foreach (var x in model.ChiTietDonHangs)
                        {
                            if (x.MaDonHang != null)
                            {
                                var oj = db.ChiTietDonHangs.Where(s => s.MaChiTietDonHang == x.MaChiTietDonHang).SingleOrDefault();
                            if (entity != null)
                            {
                                oj.SoLuong = x.SoLuong;
                                db.SaveChanges();
                            }
                            }
                            else
                            {
                                var oj = db.ChiTietDonHangs.Where(s => s.MaChiTietDonHang == x.MaChiTietDonHang).SingleOrDefault();
                                db.ChiTietDonHangs.Remove(oj);
                                db.SaveChanges();
                            }
                        }
                    }
                    
                  
                    db.DonHangs.Update(entity);
                    var result = db.SaveChanges();
                    return Ok(new { data = result, status = 201 });
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        //public IActionResult UpdateInvoice([FromBody] HoaDonNew model)
        //{
        //    var obj = db.DonHangs.Where(s => s.MaDonHang == model.MaDonHang).SingleOrDefault();
        //    if (obj != null)
        //    {
        //        obj.NgayTao = model.NgayTao;
        //        obj.TongTien = model.TongTien;
        //        obj.TenKhachHang = model.TenKhachHang;
        //        obj.Sdt = model.Sdt;
        //        obj.DiaChi = model.DiaChi;
        //        obj.GhiChu = model.GhiChu;
        //        obj.TrangThaiDonHang = model.TrangThaiDonHang;
        //        obj.TrangThaiVanChuyen = model.TrangThaiVanChuyen;
        //        obj.TrangThaiThanhToan = model.TrangThaiThanhToan;
        //        if (model.ChiTietDonHangs.Count > 0)
        //        {
        //            foreach (var x in model.ChiTietDonHangs)
        //            {
        //                //if (x.MaDonHang != null)
        //                //{
        //                    var oj = db.ChiTietDonHangs.Where(s => s.MaChiTietDonHang == x.MaChiTietDonHang).SingleOrDefault();
        //                    if (obj != null)
        //                    {
        //                        oj.SoLuong = x.SoLuong;
        //                        db.SaveChanges();
        //                    }
        //                //}
        //                //else
        //                //{
        //                //    var oj = db.ChiTietDonHangs.Where(s => s.MaChiTietDonHang == x.MaChiTietDonHang).SingleOrDefault();
        //                //    db.ChiTietDonHangs.Remove(oj);
        //                //    db.SaveChanges();
        //                //}
        //            }
        //        }
        //    }

        //    return Ok(new { data = "OK" });
        //}
        [Route("thongketong-Invoice")]
        [HttpPost]
        public decimal tkInvoice()
        {    
            decimal TongTienn = 0;
                TongTienn += decimal.Parse(db.ChiTietDonHangs.Sum(s => s.SoLuong * s.DonGia).ToString());
            return TongTienn ;
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
        [HttpDelete("delete")]
        //[HttpDelete, Authorize(Roles = "Admin")]
        public IActionResult Delete([FromQuery] string id)
        {
            try
            {
                var reuslt = db.DonHangs.FirstOrDefault(x => x.MaDonHang == id);
                if (reuslt == null)
                {
                    return BadRequest("can not find by id");
                }
                else
                {
                    db.DonHangs.Remove(reuslt);
                    var res = db.SaveChanges();
                    return Ok(new { data = res, status = 201 });
                }

            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
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

