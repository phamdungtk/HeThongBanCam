using System;
using System.Collections.Generic;

namespace HeThongBanCam.Models
{
    public partial class LoaiCamera
    {
        public LoaiCamera()
        {
            Cameras = new HashSet<Camera>();
        }

        public int MaLoai { get; set; }
        public int? MaHang { get; set; }
        public string TenLoai { get; set; } = null!;
        public string MoTa { get; set; } = null!;

        public virtual HangSanXuat? MaHangNavigation { get; set; }
        public virtual ICollection<Camera> Cameras { get; set; }
    }
}
