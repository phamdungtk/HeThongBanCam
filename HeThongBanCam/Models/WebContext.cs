using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace HeThongBanCam.Models
{
    public partial class WebContext : DbContext
    {
        public WebContext(IConfiguration configuration)
        {
        }

        public WebContext(DbContextOptions<WebContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Camera> Cameras { get; set; } = null!;
        public virtual DbSet<ChiTietDonHang> ChiTietDonHangs { get; set; } = null!;
        public virtual DbSet<ChiTietHoaDonNhap> ChiTietHoaDonNhaps { get; set; } = null!;
        public virtual DbSet<DonHang> DonHangs { get; set; } = null!;
        public virtual DbSet<HangSanXuat> HangSanXuats { get; set; } = null!;
        public virtual DbSet<HoaDonNhap> HoaDonNhaps { get; set; } = null!;
        public virtual DbSet<KhachHang> KhachHangs { get; set; } = null!;
        public virtual DbSet<LoaiCamera> LoaiCameras { get; set; } = null!;
        public virtual DbSet<NguoiDung> NguoiDungs { get; set; } = null!;
        public virtual DbSet<NhaCungCap> NhaCungCaps { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Data Source=DUNGXUAN\\SQLEXPRESS;Initial Catalog=Web;Integrated Security=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Camera>(entity =>
            {
                entity.HasKey(e => e.MaCamera)
                    .HasName("PK__Camera__C7F02A383052D2AA");

                entity.ToTable("Camera");

                entity.Property(e => e.Chip).HasMaxLength(20);

                entity.Property(e => e.DoPhanGiai).HasMaxLength(30);

                entity.Property(e => e.HinhAnh).HasMaxLength(70);

                entity.Property(e => e.NguonDien).HasMaxLength(30);

                entity.Property(e => e.OngKinh).HasMaxLength(40);

                entity.Property(e => e.TamQuanSat).HasMaxLength(30);

                entity.Property(e => e.TenCamera).HasMaxLength(100);

                entity.HasOne(d => d.MaLoaiNavigation)
                    .WithMany(p => p.Cameras)
                    .HasForeignKey(d => d.MaLoai)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FK__Camera__MaLoai__29572725");
            });

            modelBuilder.Entity<ChiTietDonHang>(entity =>
            {
                entity.HasKey(e => e.MaChiTietDonHang)
                    .HasName("PK__ChiTietD__4B0B45DD8D1C0381");

                entity.ToTable("ChiTietDonHang");

                entity.Property(e => e.MaDonHang).HasMaxLength(50);

                entity.HasOne(d => d.MaCameraNavigation)
                    .WithMany(p => p.ChiTietDonHangs)
                    .HasForeignKey(d => d.MaCamera)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FK__ChiTietDo__MaCam__34C8D9D1");

                entity.HasOne(d => d.MaDonHangNavigation)
                    .WithMany(p => p.ChiTietDonHangs)
                    .HasForeignKey(d => d.MaDonHang)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FK__ChiTietDo__MaDon__33D4B598");
            });

            modelBuilder.Entity<ChiTietHoaDonNhap>(entity =>
            {
                entity.HasKey(e => e.MaCthoaDonNhap)
                    .HasName("PK__ChiTietH__A2AE208AEF86F765");

                entity.ToTable("ChiTietHoaDonNhap");

                entity.Property(e => e.MaCthoaDonNhap)
                    .HasMaxLength(50)
                    .HasColumnName("MaCTHoaDonNhap");

                entity.Property(e => e.MaHoaDonNhap).HasMaxLength(50);

                entity.HasOne(d => d.MaCameraNavigation)
                    .WithMany(p => p.ChiTietHoaDonNhaps)
                    .HasForeignKey(d => d.MaCamera)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FK__ChiTietHo__MaCam__3D5E1FD2");

                entity.HasOne(d => d.MaHoaDonNhapNavigation)
                    .WithMany(p => p.ChiTietHoaDonNhaps)
                    .HasForeignKey(d => d.MaHoaDonNhap)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FK__ChiTietHo__MaHoa__3C69FB99");
            });

            modelBuilder.Entity<DonHang>(entity =>
            {
                entity.HasKey(e => e.MaDonHang)
                    .HasName("PK__DonHang__129584AD93B8E842");

                entity.ToTable("DonHang");

                entity.Property(e => e.MaDonHang).HasMaxLength(50);

                entity.Property(e => e.DiaChi).HasMaxLength(70);

                entity.Property(e => e.GhiChu).HasMaxLength(50);

                entity.Property(e => e.NgayTao).HasColumnType("date");

                entity.Property(e => e.Sdt)
                    .HasMaxLength(10)
                    .HasColumnName("SDT");

                entity.Property(e => e.TenKhachHang).HasMaxLength(50);

                entity.Property(e => e.TrangThaiDonHang).HasMaxLength(30);

                entity.Property(e => e.TrangThaiThanhToan).HasMaxLength(30);

                entity.Property(e => e.TrangThaiVanChuyen).HasMaxLength(30);

                entity.HasOne(d => d.MaKhachHangNavigation)
                    .WithMany(p => p.DonHangs)
                    .HasForeignKey(d => d.MaKhachHang)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FK__DonHang__MaKhach__30F848ED");
            });

            modelBuilder.Entity<HangSanXuat>(entity =>
            {
                entity.HasKey(e => e.MaHang)
                    .HasName("PK__HangSanX__19C0DB1D95CEAC5B");

                entity.ToTable("HangSanXuat");

                entity.Property(e => e.TenHang).HasMaxLength(30);
            });

            modelBuilder.Entity<HoaDonNhap>(entity =>
            {
                entity.HasKey(e => e.MaHoaDonNhap)
                    .HasName("PK__HoaDonNh__448838B505D2C0E0");

                entity.ToTable("HoaDonNhap");

                entity.Property(e => e.MaHoaDonNhap).HasMaxLength(50);

                entity.Property(e => e.MaNcc).HasColumnName("MaNCC");

                entity.Property(e => e.NgayNhap).HasColumnType("datetime");

                entity.HasOne(d => d.MaNccNavigation)
                    .WithMany(p => p.HoaDonNhaps)
                    .HasForeignKey(d => d.MaNcc)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FK__HoaDonNha__MaNCC__398D8EEE");
            });

            modelBuilder.Entity<KhachHang>(entity =>
            {
                entity.HasKey(e => e.MaKhachHang)
                    .HasName("PK__KhachHan__88D2F0E564CF4126");

                entity.ToTable("KhachHang");

                entity.HasIndex(e => e.TenKhachHang, "UQ__KhachHan__92A7EFABCDF7168C")
                    .IsUnique();

                entity.Property(e => e.AnhDaiDien).HasMaxLength(70);

                entity.Property(e => e.DiaChi).HasMaxLength(100);

                entity.Property(e => e.Email).HasMaxLength(50);

                entity.Property(e => e.MatKhau).HasMaxLength(20);

                entity.Property(e => e.Sdt)
                    .HasMaxLength(10)
                    .HasColumnName("SDT");

                entity.Property(e => e.TenKhachHang).HasMaxLength(30);
            });

            modelBuilder.Entity<LoaiCamera>(entity =>
            {
                entity.HasKey(e => e.MaLoai)
                    .HasName("PK__LoaiCame__730A57599976FDC2");

                entity.ToTable("LoaiCamera");

                entity.Property(e => e.TenLoai).HasMaxLength(30);

                entity.HasOne(d => d.MaHangNavigation)
                    .WithMany(p => p.LoaiCameras)
                    .HasForeignKey(d => d.MaHang)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FK__LoaiCamer__MaHan__267ABA7A");
            });

            modelBuilder.Entity<NguoiDung>(entity =>
            {
                entity.HasKey(e => e.MaNguoiDung)
                    .HasName("PK__NguoiDun__C539D7626776344B");

                entity.ToTable("NguoiDung");

                entity.Property(e => e.TaiKhoan).HasMaxLength(50);
            });

            modelBuilder.Entity<NhaCungCap>(entity =>
            {
                entity.HasKey(e => e.MaNcc)
                    .HasName("PK__NhaCungC__3A185DEBDCF81B58");

                entity.ToTable("NhaCungCap");

                entity.Property(e => e.MaNcc).HasColumnName("MaNCC");

                entity.Property(e => e.Email).HasMaxLength(30);

                entity.Property(e => e.Sdt)
                    .HasMaxLength(10)
                    .HasColumnName("SDT");

                entity.Property(e => e.TenNcc).HasColumnName("TenNCC");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
