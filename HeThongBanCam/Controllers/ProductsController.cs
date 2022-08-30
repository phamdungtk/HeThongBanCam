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
        [HttpGet("get-lsp")]
        public IActionResult GetLsp([FromQuery] int lsp)
        {
            try
            {
                var result = db.Cameras.Where(x => x.MaLoai == lsp).ToList();
                if (result.Count > 0)
                {
                    return Ok(result);
                }
                return BadRequest();
            }
            catch (Exception e)
            {

                return BadRequest(e.Message);
            }

        }
        //Gán cho idcha trong sql
        //[HttpGet("get-parent-tree-lsp")]
        //public IActionResult GetParentNode()
        //{
        //    var lsp = db.LoaiCameras.Select(x => new LoaiCamera()
        //    {
        //       MaLoai= x.MaLoai,
        //        TenLoai = x.TenLoai,
        //        MaHang = x.MaHang,
        //        MoTa = x.MoTa
        //    }).ToList();


        //    List<LoaiCamera> result = new List<LoaiCamera>();
        //    int count = lsp.Where(x => x.MaLoai == null).Count();
        //    result = lsp.Where(c => c.MaLoai == null).Select(c => new LoaiCamera() { MaLoai = c.MaLoai, TenLoai = c.TenLoai, MoTa = c.MoTa, MaHang = c.MaHang, LSP = GetLSP(lsp, c.MaLoai) }).ToList();
        //    return Ok(result);
        //}

        //public static List<LoaiCamera> GetLSP(List<LoaiCamera> lsp, int maloai)
        //{
        //    return lsp.Where(c => c.MaLoai == maloai)
        //            .Select(c => new LoaiCamera { MaLoai = c.MaLoai, TenLoai = c.TenLoai, MoTa = c.MoTa,MaHang=c.MaHang,   LSP = GetLSP(lsp, c.MaLoai) })
        //            .ToList();
        //}
        private async Task<Responsive> Validate(Camera cam, bool isUpdate = false)
        {
            var message = "";
            if (cam == null)
            {
                message = "Không thể bỏ trống dữ liệu";
                return new Responsive(message);
            }
            if (cam.MaCamera == null)
            {
                message = "Không thể bỏ trống dữ liệu";
                return new Responsive(message);
            }
            if (isUpdate == true)
            {
                var entity = db.Cameras.Find(cam.MaCamera);
                if (entity != null && entity.MaCamera != cam.MaCamera)
                {
                    message = "Mã Cam đã tồn tại";
                    return new Responsive(message);
                }
            }
            if (isUpdate == false)
            {
                var entity = db.Cameras.Find(cam.MaCamera);
                if (entity != null)
                {
                    message = "Mã Cam đã tồn tại";
                    return new Responsive(message);
                }
            }
            return new Responsive("", true);
        }
        [Route("create-Product")]
        [HttpPost/*, Authorize(Roles = "Admin")*/]
        public async Task<IActionResult> Create([FromBody] Camera model)
        {
            try
            {
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
                    db.Cameras.Add(model);
                    var reuslt = db.SaveChanges();
                    return Ok(new { data = reuslt, status = 201 });
                }
                //db.Cameras.Add(model);           
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            //db.Cameras.Add(model);
            //db.SaveChanges();          
            //return Ok(new { data = "OK" });
        }
        [Route("update-product")]
        [HttpPost/*, Authorize(Roles = "Admin")*/]
        public async Task<IActionResult> Update([FromQuery] int id, [FromBody] Camera model)
        {
            try
            {
                var entity = db.Cameras.FirstOrDefault(x => x.MaCamera == id);
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
                    entity.MaLoaiNavigation = null;
                    entity.MaCamera = model.MaCamera;
                    entity.TenCamera = model.TenCamera;
                    entity.DoPhanGiai = model.DoPhanGiai;
                    entity.Chip = model.Chip;
                    entity.OngKinh = model.OngKinh;
                    entity.TamQuanSat = model.TamQuanSat;
                    entity.NguonDien = model.NguonDien;
                    entity.HinhAnh = model.HinhAnh;
                    entity.Gia = model.Gia;
                    entity.CameraHot = model.CameraHot;
                    entity.SoLuong = model.SoLuong;
                    db.Cameras.Update(entity);
                    var result = db.SaveChanges();
                    return Ok(new { data = result, status = 201 });
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("delete")]
        //[HttpDelete, Authorize(Roles = "Admin")]
        public IActionResult Delete([FromQuery] int id)
        {
            try
            {
                var reuslt = db.Cameras.FirstOrDefault(x => x.MaCamera == id);
                if (reuslt == null)
                {
                    return BadRequest("can not find by id");
                }
                else
                {
                    db.Cameras.Remove(reuslt);
                    var res = db.SaveChanges();
                    return Ok(new { data = res, status = 201 });
                }
               
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}
