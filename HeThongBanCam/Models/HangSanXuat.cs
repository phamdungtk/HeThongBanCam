using System;
using System.Collections.Generic;

namespace HeThongBanCam.Models
{
    public partial class HangSanXuat
    {
        public HangSanXuat()
        {
            LoaiCameras = new HashSet<LoaiCamera>();
        }

        public int MaHang { get; set; }
        public string TenHang { get; set; } = null!;
        public string ThongTin { get; set; } = null!;

        public virtual ICollection<LoaiCamera> LoaiCameras { get; set; }
    }
}
