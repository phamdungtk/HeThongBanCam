USE [master]
GO
/****** Object:  Database [Web]    Script Date: 9/5/2022 11:43:28 AM ******/
CREATE DATABASE [Web]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'Web', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL15.SQLEXPRESS\MSSQL\DATA\Web.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'Web_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL15.SQLEXPRESS\MSSQL\DATA\Web_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
 WITH CATALOG_COLLATION = DATABASE_DEFAULT
GO
ALTER DATABASE [Web] SET COMPATIBILITY_LEVEL = 150
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [Web].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [Web] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [Web] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [Web] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [Web] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [Web] SET ARITHABORT OFF 
GO
ALTER DATABASE [Web] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [Web] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [Web] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [Web] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [Web] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [Web] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [Web] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [Web] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [Web] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [Web] SET  DISABLE_BROKER 
GO
ALTER DATABASE [Web] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [Web] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [Web] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [Web] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [Web] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [Web] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [Web] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [Web] SET RECOVERY SIMPLE 
GO
ALTER DATABASE [Web] SET  MULTI_USER 
GO
ALTER DATABASE [Web] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [Web] SET DB_CHAINING OFF 
GO
ALTER DATABASE [Web] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [Web] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [Web] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [Web] SET ACCELERATED_DATABASE_RECOVERY = OFF  
GO
ALTER DATABASE [Web] SET QUERY_STORE = OFF
GO
USE [Web]
GO
/****** Object:  Table [dbo].[Camera]    Script Date: 9/5/2022 11:43:28 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Camera](
	[MaCamera] [int] IDENTITY(1,1) NOT NULL,
	[TenCamera] [nvarchar](100) NOT NULL,
	[MaLoai] [int] NULL,
	[DoPhanGiai] [nvarchar](30) NOT NULL,
	[Chip] [nvarchar](20) NOT NULL,
	[OngKinh] [nvarchar](40) NOT NULL,
	[TamQuanSat] [nvarchar](30) NOT NULL,
	[NguonDien] [nvarchar](30) NULL,
	[HinhAnh] [nvarchar](70) NULL,
	[Gia] [int] NULL,
	[CameraHot] [bit] NULL,
	[SoLuong] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[MaCamera] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ChiTietDonHang]    Script Date: 9/5/2022 11:43:28 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ChiTietDonHang](
	[MaChiTietDonHang] [int] IDENTITY(1,1) NOT NULL,
	[MaDonHang] [nvarchar](50) NULL,
	[MaCamera] [int] NULL,
	[SoLuong] [int] NOT NULL,
	[DonGia] [int] NULL,
 CONSTRAINT [PK__ChiTietD__4B0B45DD8D1C0381] PRIMARY KEY CLUSTERED 
(
	[MaChiTietDonHang] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ChiTietHoaDonNhap]    Script Date: 9/5/2022 11:43:28 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ChiTietHoaDonNhap](
	[MaCTHoaDonNhap] [nvarchar](50) NOT NULL,
	[MaHoaDonNhap] [nvarchar](50) NULL,
	[MaCamera] [int] NULL,
	[SoLuong] [int] NOT NULL,
	[DonGia] [int] NOT NULL,
 CONSTRAINT [PK__ChiTietH__A2AE208AEF86F765] PRIMARY KEY CLUSTERED 
(
	[MaCTHoaDonNhap] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[DonHang]    Script Date: 9/5/2022 11:43:28 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[DonHang](
	[MaDonHang] [nvarchar](50) NOT NULL,
	[MaKhachHang] [int] NULL,
	[NgayTao] [date] NOT NULL,
	[TongTien] [int] NULL,
	[TenKhachHang] [nvarchar](50) NULL,
	[DiaChi] [nvarchar](70) NULL,
	[SDT] [nvarchar](10) NULL,
	[GhiChu] [nvarchar](50) NULL,
	[TrangThaiDonHang] [nvarchar](30) NOT NULL,
	[TrangThaiVanChuyen] [nvarchar](30) NOT NULL,
	[TrangThaiThanhToan] [nvarchar](30) NOT NULL,
 CONSTRAINT [PK__DonHang__129584AD93B8E842] PRIMARY KEY CLUSTERED 
(
	[MaDonHang] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[HangSanXuat]    Script Date: 9/5/2022 11:43:28 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[HangSanXuat](
	[MaHang] [int] IDENTITY(1,1) NOT NULL,
	[TenHang] [nvarchar](30) NOT NULL,
	[ThongTin] [nvarchar](max) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[MaHang] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[HoaDonNhap]    Script Date: 9/5/2022 11:43:28 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[HoaDonNhap](
	[MaHoaDonNhap] [nvarchar](50) NOT NULL,
	[MaNCC] [int] NULL,
	[TongTien] [int] NOT NULL,
	[NgayNhap] [datetime] NULL,
 CONSTRAINT [PK__HoaDonNh__448838B505D2C0E0] PRIMARY KEY CLUSTERED 
(
	[MaHoaDonNhap] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[KhachHang]    Script Date: 9/5/2022 11:43:28 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[KhachHang](
	[MaKhachHang] [int] IDENTITY(1,1) NOT NULL,
	[TenKhachHang] [nvarchar](30) NOT NULL,
	[AnhDaiDien] [nvarchar](70) NULL,
	[Email] [nvarchar](50) NOT NULL,
	[MatKhau] [nvarchar](20) NOT NULL,
	[GioiTinh] [int] NOT NULL,
	[SDT] [nvarchar](10) NOT NULL,
	[DiaChi] [nvarchar](100) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[MaKhachHang] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[LoaiCamera]    Script Date: 9/5/2022 11:43:28 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[LoaiCamera](
	[MaLoai] [int] IDENTITY(1,1) NOT NULL,
	[MaHang] [int] NULL,
	[TenLoai] [nvarchar](30) NOT NULL,
	[MoTa] [nvarchar](max) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[MaLoai] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[NguoiDung]    Script Date: 9/5/2022 11:43:28 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[NguoiDung](
	[MaNguoiDung] [int] IDENTITY(1,1) NOT NULL,
	[TaiKhoan] [nvarchar](50) NULL,
	[MatKhau] [nvarchar](max) NULL,
	[TenNguoiDung] [nvarchar](max) NULL,
	[Sdt] [int] NULL,
	[QueQuan] [nvarchar](max) NULL,
	[Gmail] [nvarchar](max) NULL,
	[Salt] [nvarchar](max) NULL,
 CONSTRAINT [PK__NguoiDun__C539D7626776344B] PRIMARY KEY CLUSTERED 
(
	[MaNguoiDung] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[NhaCungCap]    Script Date: 9/5/2022 11:43:28 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[NhaCungCap](
	[MaNCC] [int] IDENTITY(1,1) NOT NULL,
	[TenNCC] [nvarchar](max) NOT NULL,
	[DiaChi] [nvarchar](max) NOT NULL,
	[SDT] [nvarchar](10) NOT NULL,
	[Email] [nvarchar](30) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[MaNCC] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[Camera] ON 

INSERT [dbo].[Camera] ([MaCamera], [TenCamera], [MaLoai], [DoPhanGiai], [Chip], [OngKinh], [TamQuanSat], [NguonDien], [HinhAnh], [Gia], [CameraHot], [SoLuong]) VALUES (53, N'aaaaaaaaaaaaaaaaaaaaa', 14, N'aaaaaaaaa', N'aaaaaaaaa', N'aaaaaaaaaaa', N'a', N'aaaaaaaaa', N'274836506_324059016423866_8351376526099161900_n.jpg', 111111, NULL, 11111)
INSERT [dbo].[Camera] ([MaCamera], [TenCamera], [MaLoai], [DoPhanGiai], [Chip], [OngKinh], [TamQuanSat], [NguonDien], [HinhAnh], [Gia], [CameraHot], [SoLuong]) VALUES (54, N'3 m', 14, N'string', N'string', N'string', N'string', N'string', N'string', 0, 1, 0)
INSERT [dbo].[Camera] ([MaCamera], [TenCamera], [MaLoai], [DoPhanGiai], [Chip], [OngKinh], [TamQuanSat], [NguonDien], [HinhAnh], [Gia], [CameraHot], [SoLuong]) VALUES (59, N'string', 14, N'string', N'string', N'string', N'string', N'string', N'string', 0, 1, 0)
INSERT [dbo].[Camera] ([MaCamera], [TenCamera], [MaLoai], [DoPhanGiai], [Chip], [OngKinh], [TamQuanSat], [NguonDien], [HinhAnh], [Gia], [CameraHot], [SoLuong]) VALUES (60, N'string', 14, N'string', N'string', N'string', N'string', N'string', N'string', 0, 1, 0)
SET IDENTITY_INSERT [dbo].[Camera] OFF
GO
SET IDENTITY_INSERT [dbo].[ChiTietDonHang] ON 

INSERT [dbo].[ChiTietDonHang] ([MaChiTietDonHang], [MaDonHang], [MaCamera], [SoLuong], [DonGia]) VALUES (15, N'97792523-d568-4fd0-8c2a-9787725d54c0', 53, 4, 111111)
INSERT [dbo].[ChiTietDonHang] ([MaChiTietDonHang], [MaDonHang], [MaCamera], [SoLuong], [DonGia]) VALUES (16, N'9335c63b-0765-4b71-95a3-5a0e118eb3c7', 53, 1, 111111)
SET IDENTITY_INSERT [dbo].[ChiTietDonHang] OFF
GO
INSERT [dbo].[DonHang] ([MaDonHang], [MaKhachHang], [NgayTao], [TongTien], [TenKhachHang], [DiaChi], [SDT], [GhiChu], [TrangThaiDonHang], [TrangThaiVanChuyen], [TrangThaiThanhToan]) VALUES (N'9335c63b-0765-4b71-95a3-5a0e118eb3c7', 1, CAST(N'2022-08-24' AS Date), 444444, N'Dũng Xuânnn', N'An Thanh  Tứ Kỳ Hải Dương', N'0374314346', N'ko', N'Chưa xác nhận', N'Chưa xác nhận', N'Chưa thanh toán')
INSERT [dbo].[DonHang] ([MaDonHang], [MaKhachHang], [NgayTao], [TongTien], [TenKhachHang], [DiaChi], [SDT], [GhiChu], [TrangThaiDonHang], [TrangThaiVanChuyen], [TrangThaiThanhToan]) VALUES (N'97792523-d568-4fd0-8c2a-9787725d54c0', 1, CAST(N'2022-08-24' AS Date), 444444, N'Dũng Xuân', N'An Thanh  Tứ Kỳ Hải Dương', N'0374314346', N'ko', N'Chưa xác nhận', N'Chưa xác nhận', N'Chưa thanh toán')
GO
SET IDENTITY_INSERT [dbo].[HangSanXuat] ON 

INSERT [dbo].[HangSanXuat] ([MaHang], [TenHang], [ThongTin]) VALUES (1, N'KFCCC', N'Đỉnh')
SET IDENTITY_INSERT [dbo].[HangSanXuat] OFF
GO
INSERT [dbo].[HoaDonNhap] ([MaHoaDonNhap], [MaNCC], [TongTien], [NgayNhap]) VALUES (N'1f8492ff-92cf-4af6-8670-3abfa4ca4c2f', 2, 1110, CAST(N'2022-08-18T06:58:48.287' AS DateTime))
GO
SET IDENTITY_INSERT [dbo].[KhachHang] ON 

INSERT [dbo].[KhachHang] ([MaKhachHang], [TenKhachHang], [AnhDaiDien], [Email], [MatKhau], [GioiTinh], [SDT], [DiaChi]) VALUES (1, N'Dũng Xuân', N'abc.jpg', N'phamdung', N'123', 1, N'034946', N'string')
SET IDENTITY_INSERT [dbo].[KhachHang] OFF
GO
SET IDENTITY_INSERT [dbo].[LoaiCamera] ON 

INSERT [dbo].[LoaiCamera] ([MaLoai], [MaHang], [TenLoai], [MoTa]) VALUES (14, 1, N'eeeaaaaaaaaaaa', N'eee')
SET IDENTITY_INSERT [dbo].[LoaiCamera] OFF
GO
SET IDENTITY_INSERT [dbo].[NguoiDung] ON 

INSERT [dbo].[NguoiDung] ([MaNguoiDung], [TaiKhoan], [MatKhau], [TenNguoiDung], [Sdt], [QueQuan], [Gmail], [Salt]) VALUES (46, N'abcsoft@gmail.com', N'123', N'aaaaaaaaa', 374314346, N'Tứ Kỳ Hải Dương', N'abcsoft@gmail.com', NULL)
INSERT [dbo].[NguoiDung] ([MaNguoiDung], [TaiKhoan], [MatKhau], [TenNguoiDung], [Sdt], [QueQuan], [Gmail], [Salt]) VALUES (47, NULL, N'A665A45920422F9D417E4867EFDC4FB8A04A1F3FFF1FA07E998E86F7F7A27AE3', NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[NguoiDung] ([MaNguoiDung], [TaiKhoan], [MatKhau], [TenNguoiDung], [Sdt], [QueQuan], [Gmail], [Salt]) VALUES (48, N'admin', N'A665A45920422F9D417E4867EFDC4FB8A04A1F3FFF1FA07E998E86F7F7A27AE3', N'dung', 374314346, N'Hải Dương', N'abc@gmail.com', NULL)
INSERT [dbo].[NguoiDung] ([MaNguoiDung], [TaiKhoan], [MatKhau], [TenNguoiDung], [Sdt], [QueQuan], [Gmail], [Salt]) VALUES (49, N'admin1', N'E4CFC9FE8A99CB3ED434B364F8BD24E158E7773AB0DA0EAC8B288301BFB58B3F', N'string', 374314347, N'Tứ Kỳ', N'qqqqq', NULL)
INSERT [dbo].[NguoiDung] ([MaNguoiDung], [TaiKhoan], [MatKhau], [TenNguoiDung], [Sdt], [QueQuan], [Gmail], [Salt]) VALUES (50, N'string', N'258902A59DC50B6FC1169F43200418430488F01FF0CBD88A872F3938772EE578', N'string', 0, N'string', N'string', N'9/5/2022 11:35:31 AM')
SET IDENTITY_INSERT [dbo].[NguoiDung] OFF
GO
SET IDENTITY_INSERT [dbo].[NhaCungCap] ON 

INSERT [dbo].[NhaCungCap] ([MaNCC], [TenNCC], [DiaChi], [SDT], [Email]) VALUES (1, N'Thiết Bị An Ninh An Toàn VITECK- Công Ty TNHH Giải Pháp Công Nghệ VITECK', N'180 Trần Quốc Toản, KP. 4, P. Bình Đa, TP. Biên Hòa, Đồng Nai', N'0966788876', N'info@viteck.vn')
INSERT [dbo].[NhaCungCap] ([MaNCC], [TenNCC], [DiaChi], [SDT], [Email]) VALUES (2, N'Khôi Ngô Security - Công Ty TNHH Khôi Ngô', N'22A Kỳ Đồng, P. 9, Q. 3, Tp. Hồ Chí Minh (TPHCM)', N'0908375212', N'contact@khoingo.net')
INSERT [dbo].[NhaCungCap] ([MaNCC], [TenNCC], [DiaChi], [SDT], [Email]) VALUES (3, N'Camera Kiến Hưng - Công Ty TNHH TM & DV Công Nghệ Kiến Hưng', N'VP Miền Bắc: Số 31, Ngõ 44, P. Mai Dịch, Q. Cầu Giấy, Hà Nội', N'0868796078', N'hatientrong886@gmail.com')
INSERT [dbo].[NhaCungCap] ([MaNCC], [TenNCC], [DiaChi], [SDT], [Email]) VALUES (4, N'Camera Quang Mai - Công Ty TNHH MTV Tin Học Viễn Thông Quang Mai', N'485/46 Phan Văn Trị, P. 5, Q. Gò Vấp, Tp. Hồ Chí Minh (TPHCM)', N'0903517025', N'tuyetphuongkt@quangmai.net')
SET IDENTITY_INSERT [dbo].[NhaCungCap] OFF
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [UQ__KhachHan__92A7EFABCDF7168C]    Script Date: 9/5/2022 11:43:28 AM ******/
ALTER TABLE [dbo].[KhachHang] ADD UNIQUE NONCLUSTERED 
(
	[TenKhachHang] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
ALTER TABLE [dbo].[Camera]  WITH CHECK ADD FOREIGN KEY([MaLoai])
REFERENCES [dbo].[LoaiCamera] ([MaLoai])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[ChiTietDonHang]  WITH CHECK ADD  CONSTRAINT [FK__ChiTietDo__MaCam__34C8D9D1] FOREIGN KEY([MaCamera])
REFERENCES [dbo].[Camera] ([MaCamera])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[ChiTietDonHang] CHECK CONSTRAINT [FK__ChiTietDo__MaCam__34C8D9D1]
GO
ALTER TABLE [dbo].[ChiTietDonHang]  WITH CHECK ADD  CONSTRAINT [FK__ChiTietDo__MaDon__33D4B598] FOREIGN KEY([MaDonHang])
REFERENCES [dbo].[DonHang] ([MaDonHang])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[ChiTietDonHang] CHECK CONSTRAINT [FK__ChiTietDo__MaDon__33D4B598]
GO
ALTER TABLE [dbo].[ChiTietHoaDonNhap]  WITH CHECK ADD  CONSTRAINT [FK__ChiTietHo__MaCam__3D5E1FD2] FOREIGN KEY([MaCamera])
REFERENCES [dbo].[Camera] ([MaCamera])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[ChiTietHoaDonNhap] CHECK CONSTRAINT [FK__ChiTietHo__MaCam__3D5E1FD2]
GO
ALTER TABLE [dbo].[ChiTietHoaDonNhap]  WITH CHECK ADD  CONSTRAINT [FK__ChiTietHo__MaHoa__3C69FB99] FOREIGN KEY([MaHoaDonNhap])
REFERENCES [dbo].[HoaDonNhap] ([MaHoaDonNhap])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[ChiTietHoaDonNhap] CHECK CONSTRAINT [FK__ChiTietHo__MaHoa__3C69FB99]
GO
ALTER TABLE [dbo].[DonHang]  WITH CHECK ADD  CONSTRAINT [FK__DonHang__MaKhach__30F848ED] FOREIGN KEY([MaKhachHang])
REFERENCES [dbo].[KhachHang] ([MaKhachHang])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[DonHang] CHECK CONSTRAINT [FK__DonHang__MaKhach__30F848ED]
GO
ALTER TABLE [dbo].[HoaDonNhap]  WITH CHECK ADD  CONSTRAINT [FK__HoaDonNha__MaNCC__398D8EEE] FOREIGN KEY([MaNCC])
REFERENCES [dbo].[NhaCungCap] ([MaNCC])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[HoaDonNhap] CHECK CONSTRAINT [FK__HoaDonNha__MaNCC__398D8EEE]
GO
ALTER TABLE [dbo].[LoaiCamera]  WITH CHECK ADD FOREIGN KEY([MaHang])
REFERENCES [dbo].[HangSanXuat] ([MaHang])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
/****** Object:  StoredProcedure [dbo].[Add_Camera]    Script Date: 9/5/2022 11:43:28 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

create proc [dbo].[Add_Camera]
@TenCamera nvarchar(100),
@MaLoai int,
@DoPhanGiai nvarchar(30),
@Chip nvarchar(20),
@OngKinh nvarchar(40),
@TamQuanSat nvarchar(30),
@NguonDien nvarchar(30),
@HinhAnh nvarchar(70),
@Gia int,
@CameraHot int
as
	begin
		insert into Camera(TenCamera,MaLoai,DoPhanGiai,Chip,OngKinh,TamQuanSat,NguonDien,HinhAnh,Gia,CameraHot) 
		values(@TenCamera,@MaLoai,@DoPhanGiai,@Chip,@OngKinh,@TamQuanSat,@NguonDien,@HinhAnh,@Gia,@CameraHot)
	end
GO
/****** Object:  StoredProcedure [dbo].[ADD_ChiTietHoaDonNhap]    Script Date: 9/5/2022 11:43:28 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create proc [dbo].[ADD_ChiTietHoaDonNhap]
@MaCTHoaDonNhap nvarchar(30),
@MaHoaDonNhap nvarchar(30),
@MaCamera int,
@SoLuong int,
@DonGia int
as
begin
	insert into ChiTietHoaDonNhap(MaCTHoaDonNhap,MaHoaDonNhap,MaCamera,SoLuong,DonGia) 
	values(@MaCTHoaDonNhap,@MaHoaDonNhap,@MaCamera,@SoLuong,@DonGia)

	update HoaDonNhap
	set TongTien=(select sum(SoLuong*DonGia) 
					from ChiTietHoaDonNhap 
					where ChiTietHoaDonNhap.MaHoaDonNhap=@MaHoaDonNhap
					group by ChiTietHoaDonNhap.MaHoaDonNhap
					)
	where HoaDonNhap.MaHoaDonNhap=@MaHoaDonNhap
end
GO
/****** Object:  StoredProcedure [dbo].[Add_HangSanXuat]    Script Date: 9/5/2022 11:43:28 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

create proc [dbo].[Add_HangSanXuat]
@TenHang nvarchar(50),
@ThongTin nvarchar(max)
as
	begin
		insert into HangSanXuat(TenHang,ThongTin) values(@TenHang,@ThongTin)
	end

GO
/****** Object:  StoredProcedure [dbo].[ADD_HoaDonNhap]    Script Date: 9/5/2022 11:43:28 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
---------------------------

create proc [dbo].[ADD_HoaDonNhap]
@MaHoaDonNhap nvarchar(30),
@MaNCC int,
@TongTien int,
@NgayNhap date
as
	begin
		insert into HoaDonNhap(MaHoaDonNhap,MaNCC,TongTien,NgayNhap)
		values(@MaHoaDonNhap,@MaNCC,@TongTien,@NgayNhap)
	end
GO
/****** Object:  StoredProcedure [dbo].[Add_LoaiCamera]    Script Date: 9/5/2022 11:43:28 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

create proc [dbo].[Add_LoaiCamera]
@MaHang int,
@TenLoai nvarchar(50),
@MoTa nvarchar(max)
as
	begin
		insert into LoaiCamera(MaHang,TenLoai,MoTa) values(@MaHang,@TenLoai,@MoTa)
	end

GO
/****** Object:  StoredProcedure [dbo].[Add_NhaCungCap]    Script Date: 9/5/2022 11:43:28 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

create proc [dbo].[Add_NhaCungCap]
@TenNCC nvarchar(max),
@DiaChi nvarchar(max),
@SDT nvarchar(10),
@Email nvarchar(30)
as
	begin
		insert into NhaCungCap(TenNCC,DiaChi,SDT,Email) values(@TenNCC,@DiaChi,@SDT,@Email)
	end

GO
/****** Object:  StoredProcedure [dbo].[AddCategory]    Script Date: 9/5/2022 11:43:28 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create proc [dbo].[AddCategory](@category_name nvarchar(255),@category_status int)
as
begin
insert into categories_product(category_name,category_status)values(@category_name,@category_status)
end
GO
/****** Object:  StoredProcedure [dbo].[adddetail_order]    Script Date: 9/5/2022 11:43:28 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE proc [dbo].[adddetail_order](@orderdate nvarchar(50),@photo nvarchar(100),@product_name nvarchar(50),@price int,@so_luong int)
as
begin
declare @id_order int;
set @id_order=(
select id from orderr where orderdate=@orderdate
)
insert into detail_order
values(@id_order,@photo,@product_name,@price,@so_luong)
end
GO
/****** Object:  StoredProcedure [dbo].[addorder]    Script Date: 9/5/2022 11:43:28 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE proc [dbo].[addorder](@customer_name nvarchar(50),@phone varchar(10),@dia_chi nvarchar(200),@ask nvarchar(100),@orderdate nvarchar(50),@sum_price int)
as
begin
insert into orderr
values (@customer_name,@phone,@dia_chi,@ask,@sum_price,@orderdate,0)
end
GO
/****** Object:  StoredProcedure [dbo].[AddProduct]    Script Date: 9/5/2022 11:43:28 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create proc [dbo].[AddProduct](@id_category int,@product_name nvarchar(255),@big_photo nvarchar(255),@small_photo nvarchar(255),@so_luong int,@price int,@monitor nvarchar(255),@operating_system nvarchar(255),@front_camera nvarchar(255),@behind_camera nvarchar(255),@CPU nvarchar(255),@RAM nvarchar(255),@ROM nvarchar(255),@memory_stick nvarchar(255),@SIM nvarchar(255),@PIN nvarchar(255),@Mota nvarchar(255))
as
begin
declare @category_name nvarchar(255);
set @category_name=(
 select categories_product.category_name from  categories_product where id=@id_category
 );
 insert into product
 (id_category,
 category_name,
 product_name,
 big_photo,
 small_photo,
 so_luong,
 count_sold,
 price,
 monitor,
 operating_system,
 front_camera,
 behind_camera,
 CPU,RAM,ROM,
 memory_stick,
 SIM,PIN,Mota)
 values(@id_category,@category_name,@product_name,@big_photo,@small_photo,@so_luong,'0',@price,@monitor,@operating_system,@front_camera,@behind_camera,@CPU,@RAM,@ROM,@memory_stick,@SIM,@PIN,@Mota) end
GO
/****** Object:  StoredProcedure [dbo].[CancelThisOrder]    Script Date: 9/5/2022 11:43:28 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

create proc [dbo].[CancelThisOrder]
@MaDonHang nvarchar(30)
as
	begin
		update DonHang
		set TrangThaiDonHang=N'Đã hủy'
		where MaDonHang=@MaDonHang
	end
GO
/****** Object:  StoredProcedure [dbo].[Comfirm_Recover]    Script Date: 9/5/2022 11:43:28 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

create proc [dbo].[Comfirm_Recover]
@MaDonHang nvarchar(30)
as
	begin
		update DonHang
		set TrangThaiDonHang=N'Đổi trả'
		where MaDonHang=@MaDonHang
	end
GO
/****** Object:  StoredProcedure [dbo].[ComfirmThisOrder]    Script Date: 9/5/2022 11:43:28 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--Xác thực đơn hàng
create proc [dbo].[ComfirmThisOrder]
@MaDonHang nvarchar(30)
as
	begin
		update DonHang
		set TrangThaiDonHang=N'Đã xác thực'
		where MaDonHang=@MaDonHang
	end
GO
/****** Object:  StoredProcedure [dbo].[Create_ChiTietDH]    Script Date: 9/5/2022 11:43:28 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

create proc [dbo].[Create_ChiTietDH]
@MaDonHang nvarchar(50),
@MaCamera int,
@SoLuong int,
@DonGia int
as
begin
 INSERT INTO ChiTietDonHang
                        (MaDonHang, 
                         MaCamera, 
                         SoLuong,
						 DonGia
                        )
						values(@MaDonHang,@MaCamera,@SoLuong,@DonGia)
                         

end
GO
/****** Object:  StoredProcedure [dbo].[Delete_Camera]    Script Date: 9/5/2022 11:43:28 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

create proc [dbo].[Delete_Camera]
@MaCamera int
as
	begin
		delete from Camera where MaCamera=@MaCamera
	end
GO
/****** Object:  StoredProcedure [dbo].[Delete_HangSanXuat]    Script Date: 9/5/2022 11:43:28 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

create proc [dbo].[Delete_HangSanXuat]
@MaHang int
as
	begin
		delete from HangSanXuat where MaHang=@MaHang
	end
GO
/****** Object:  StoredProcedure [dbo].[Delete_LoaiCamera]    Script Date: 9/5/2022 11:43:28 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

create proc [dbo].[Delete_LoaiCamera]
@MaLoai int
as
	begin
		delete from LoaiCamera where MaLoai=@MaLoai
	end
GO
/****** Object:  StoredProcedure [dbo].[Delete_NhaCungCap]    Script Date: 9/5/2022 11:43:28 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

create proc [dbo].[Delete_NhaCungCap]
@MaNCC int
as
	begin
		delete from NhaCungCap where MaNCC=@MaNCC
	end
GO
/****** Object:  StoredProcedure [dbo].[DeleteCategory]    Script Date: 9/5/2022 11:43:28 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create proc [dbo].[DeleteCategory](@id int)
as
begin
delete from categories_product where id=@id
end
GO
/****** Object:  StoredProcedure [dbo].[DeleteProduct]    Script Date: 9/5/2022 11:43:28 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create proc [dbo].[DeleteProduct](@id int)
as begin delete from product where id=@id end
GO
/****** Object:  StoredProcedure [dbo].[FindProduct]    Script Date: 9/5/2022 11:43:28 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create proc [dbo].[FindProduct](@name nvarchar(50))
as
begin
select * from product where product_name Like '%'+@name+'%'
end
GO
/****** Object:  StoredProcedure [dbo].[Get_MenuLoai]    Script Date: 9/5/2022 11:43:28 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


create proc [dbo].[Get_MenuLoai]
as
	begin
		select H.MaHang,TenHang,MaLoai,TenLoai 
		from HangSanXuat H inner join LoaiCamera L on H.MaHang=L.MaHang
	end
GO
/****** Object:  StoredProcedure [dbo].[Get2Trieu]    Script Date: 9/5/2022 11:43:28 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create proc [dbo].[Get2Trieu](@id int)
as 
begin
select * from product where id_category=@id and price<2000000
end
GO
/****** Object:  StoredProcedure [dbo].[GetAllCategory]    Script Date: 9/5/2022 11:43:28 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create proc [dbo].[GetAllCategory]
as
begin
select * from categories_product where category_status=0
end
GO
/****** Object:  StoredProcedure [dbo].[GetAllCategoryQL]    Script Date: 9/5/2022 11:43:28 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create proc [dbo].[GetAllCategoryQL]
as
begin
select * from categories_product
end
GO
/****** Object:  StoredProcedure [dbo].[GetAllProduct]    Script Date: 9/5/2022 11:43:28 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create proc [dbo].[GetAllProduct]
as
begin
select * from product
end
GO
/****** Object:  StoredProcedure [dbo].[GetCateByID]    Script Date: 9/5/2022 11:43:28 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create proc [dbo].[GetCateByID](@id int)
as
begin
select * from categories_product where id=@id
end
GO
/****** Object:  StoredProcedure [dbo].[GetdetailByID]    Script Date: 9/5/2022 11:43:28 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create proc [dbo].[GetdetailByID](@id int)
as
begin select * from detail_order where id=@id
end
GO
/****** Object:  StoredProcedure [dbo].[GetDetailOrder]    Script Date: 9/5/2022 11:43:28 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE proc [dbo].[GetDetailOrder](@id_order int)
as
begin
select * from detail_order where id_order=@id_order
end
GO
/****** Object:  StoredProcedure [dbo].[GetNB]    Script Date: 9/5/2022 11:43:28 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE proc [dbo].[GetNB]
as begin
select  top (8) * from product order by count_sold desc end
GO
/****** Object:  StoredProcedure [dbo].[GetNew]    Script Date: 9/5/2022 11:43:28 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE proc [dbo].[GetNew]
as
begin
select top(8) * from product order by id desc
end
GO
/****** Object:  StoredProcedure [dbo].[GetOrder]    Script Date: 9/5/2022 11:43:28 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE proc [dbo].[GetOrder]
as
begin select * from orderr 
end
GO
/****** Object:  StoredProcedure [dbo].[GetorderByID]    Script Date: 9/5/2022 11:43:28 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create proc [dbo].[GetorderByID](@id int)
as
begin
select * from orderr where id=@id
end
GO
/****** Object:  StoredProcedure [dbo].[GetOrderST]    Script Date: 9/5/2022 11:43:28 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create proc [dbo].[GetOrderST](@trang_thai int)
as
begin
select * from  orderr where trang_thai=@trang_thai
end
GO
/****** Object:  StoredProcedure [dbo].[GetProByID]    Script Date: 9/5/2022 11:43:28 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create proc [dbo].[GetProByID](@id int)
as
begin
select product.small_photo,product.product_name,product.count_sold,product.price,product.id,product.id_category from product where id_category=@id
end
GO
/****** Object:  StoredProcedure [dbo].[GetProductID]    Script Date: 9/5/2022 11:43:28 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create proc [dbo].[GetProductID](@id int)
as begin
select * from product where id=@id
end
GO
/****** Object:  StoredProcedure [dbo].[GetSP_ByMaLoai]    Script Date: 9/5/2022 11:43:28 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

create proc [dbo].[GetSP_ByMaLoai]
@MaLoai int
as
	begin
		select *
		from Camera
		where MaLoai=@MaLoai
	end
GO
/****** Object:  StoredProcedure [dbo].[GetUserID]    Script Date: 9/5/2022 11:43:28 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE proc [dbo].[GetUserID](@username nvarchar(50),@mat_khau nvarchar(50))
as
begin
select * from users where username=@username and mat_khau=@mat_khau
end
GO
/****** Object:  StoredProcedure [dbo].[Register]    Script Date: 9/5/2022 11:43:28 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


create proc [dbo].[Register]
@TenKhachHang nvarchar(30),
@Email nvarchar(50),
@MatKhau nvarchar(20),
@GioiTinh int,
@SDT nvarchar(10),
@DiaChi nvarchar(100)
as
	begin
		insert into KhachHang(TenKhachHang,Email,MatKhau,GioiTinh,SDT,DiaChi) 
		values(@TenKhachHang,@Email,@MatKhau,@GioiTinh,@SDT,@DiaChi)
	end
GO
/****** Object:  StoredProcedure [dbo].[RestoreOrder]    Script Date: 9/5/2022 11:43:28 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

--Khôi phục đơn hàng
create proc [dbo].[RestoreOrder]
@MaDonHang nvarchar(30)
as
	begin
		update DonHang
		set TrangThaiDonHang=N'Chưa xác thực'
		where MaDonHang=@MaDonHang
	end
GO
/****** Object:  StoredProcedure [dbo].[Search_Product]    Script Date: 9/5/2022 11:43:28 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

create proc [dbo].[Search_Product]
@KeyWord nvarchar(100)
as
	begin
			select *
			from Camera
			where TenCamera LIKE '%'+@KeyWord+'%'or CONVERT(nvarchar,Gia)=@KeyWord or DoPhanGiai LIKE '%'+@KeyWord+'%'
	end
GO
/****** Object:  StoredProcedure [dbo].[SP_CTHoaDonNhap_ID]    Script Date: 9/5/2022 11:43:28 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


create proc [dbo].[SP_CTHoaDonNhap_ID]
@MaHoaDonNhap int
as
	begin
		select MaCTHoaDonNhap,MaCamera,SoLuong,DonGia
		from HoaDonNhap H inner join ChiTietHoaDonNhap C on H.MaHoaDonNhap=C.MaHoaDonNhap
		where H.MaHoaDonNhap=@MaHoaDonNhap
	end
GO
/****** Object:  StoredProcedure [dbo].[sp_DonHang]    Script Date: 9/5/2022 11:43:28 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create proc [dbo].[sp_DonHang]
as
	begin
		select * from DonHang
	end
GO
/****** Object:  StoredProcedure [dbo].[SP_DonHang_get_by_id]    Script Date: 9/5/2022 11:43:28 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

--select*from DonHang
-------
create PROCEDURE [dbo].[SP_DonHang_get_by_id]
@MaDonHang NVARCHAR(50)
AS
    BEGIN
        SELECT h.MaDonHang, 
               h.TrangThaiDonHang, 
               h.TrangThaiVanChuyen,
			   h.TrangThaiThanhToan,
			   h.TenKhachHang,
			   h.DiaChi,
			   h.TongTien,
			   h.SDT,
			   h.GhiChu,
			   h.NgayTao
        FROM DonHang h
      where  h.MaDonHang = @MaDonHang;
    END;

GO
/****** Object:  StoredProcedure [dbo].[SP_Get_ChiTietSP]    Script Date: 9/5/2022 11:43:28 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

create proc [dbo].[SP_Get_ChiTietSP]
@MaCamera int
as
	begin
		select * 
		from Camera
		where MaCamera=@MaCamera
	end
GO
/****** Object:  StoredProcedure [dbo].[SP_Get_HangSanXuat]    Script Date: 9/5/2022 11:43:28 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


create proc [dbo].[SP_Get_HangSanXuat]
as
	begin
		select*from HangSanXuat
	end
GO
/****** Object:  StoredProcedure [dbo].[SP_Get_NhaCungCap]    Script Date: 9/5/2022 11:43:28 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

create proc [dbo].[SP_Get_NhaCungCap]
as
	begin
		select *
		from NhaCungCap
	end

select NgayTao, DoanhThu=sum(SoLuong*DonGia)
from DonHang D inner join ChiTietDonHang C on D.MaDonHang=C.MaDonHang
group by NgayTao
GO
/****** Object:  StoredProcedure [dbo].[SP_Get_SanPhamHot]    Script Date: 9/5/2022 11:43:28 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

--trang home
create proc [dbo].[SP_Get_SanPhamHot]
as
	begin
		select * 
		from Camera
		where CameraHot=1
	end
GO
/****** Object:  StoredProcedure [dbo].[SP_GetAll_SanPham]    Script Date: 9/5/2022 11:43:28 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

create proc [dbo].[SP_GetAll_SanPham]
as
	begin
		select *
		from Camera
		where CameraHot=0
	end
GO
/****** Object:  StoredProcedure [dbo].[SP_GetID_Camera]    Script Date: 9/5/2022 11:43:28 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


create proc [dbo].[SP_GetID_Camera]
@MaCamera int
as
	begin
		select*from Camera where MaCamera=@MaCamera
	end
GO
/****** Object:  StoredProcedure [dbo].[SP_GetID_HangSanXuat]    Script Date: 9/5/2022 11:43:28 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

create proc [dbo].[SP_GetID_HangSanXuat]
@MaHang int
as
	begin
		select*from HangSanXuat where MaHang=@MaHang
	end
GO
/****** Object:  StoredProcedure [dbo].[SP_GetID_LoaiCamera]    Script Date: 9/5/2022 11:43:28 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

create proc [dbo].[SP_GetID_LoaiCamera]
@MaLoai int
as
	begin
		select*from LoaiCamera where MaLoai=@MaLoai
	end
GO
/****** Object:  StoredProcedure [dbo].[SP_GetID_NhaCungCap]    Script Date: 9/5/2022 11:43:28 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

create proc [dbo].[SP_GetID_NhaCungCap]
@MaNCC int
as
	begin
		select*from NhaCungCap where MaNCC=@MaNCC
	end
GO
/****** Object:  StoredProcedure [dbo].[SP_GetSP_ByMaLoai]    Script Date: 9/5/2022 11:43:28 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

create proc [dbo].[SP_GetSP_ByMaLoai]
@MaLoai int
as
	begin
		select *
		from Camera
		where MaLoai=@MaLoai
	end
GO
/****** Object:  StoredProcedure [dbo].[SP_PAGE_Camera]    Script Date: 9/5/2022 11:43:28 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create proc [dbo].[SP_PAGE_Camera]

@Pageindex int,
@Pagesize int

as
	begin
		select TenHang,TenLoai,MaCamera,TenCamera,DoPhanGiai,Chip,OngKinh,TamQuanSat,NguonDien,HinhAnh,Gia,CameraHot 
		from Camera C inner join LoaiCamera L on C.MaLoai=L.MaLoai inner join HangSanXuat H on L.MaHang=H.MaHang 
		order by MaCamera asc offset @Pagesize*(@Pageindex-1) Rows Fetch next @Pagesize rows only
		select COUNT(TenCamera) as totalcount from Camera
	end
GO
/****** Object:  StoredProcedure [dbo].[sp_page_DonHangChuaXacThuc]    Script Date: 9/5/2022 11:43:28 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create proc [dbo].[sp_page_DonHangChuaXacThuc]
@Pageindex int,
@Pagesize int

as
	begin
		select *from DonHang where TrangThaiDonHang=N'Chưa xác thực' order by MaDonHang asc offset @Pagesize*(@Pageindex-1) Rows Fetch next @Pagesize rows only
		select COUNT(MaDonHang) as totalcount from DonHang
	end
GO
/****** Object:  StoredProcedure [dbo].[sp_page_DonHangDaGiao]    Script Date: 9/5/2022 11:43:28 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

--phân trang đơn hàng đã giao
create proc [dbo].[sp_page_DonHangDaGiao]

@Pageindex int,
@Pagesize int

as
	begin
		select *from DonHang where TrangThaiDonHang=N'Đã giao' order by MaDonHang asc offset @Pagesize*(@Pageindex-1) Rows Fetch next @Pagesize rows only
		select COUNT(MaDonHang) as totalcount from DonHang
	end
GO
/****** Object:  StoredProcedure [dbo].[sp_page_DonHangDaHuy]    Script Date: 9/5/2022 11:43:28 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

create proc [dbo].[sp_page_DonHangDaHuy]
@Pageindex int,
@Pagesize int

as
	begin
		select *from DonHang where TrangThaiDonHang=N'Đã hủy' order by MaDonHang asc offset @Pagesize*(@Pageindex-1) Rows Fetch next @Pagesize rows only
		select COUNT(MaDonHang) as totalcount from DonHang
	end
GO
/****** Object:  StoredProcedure [dbo].[sp_page_DonHangDaXacThuc]    Script Date: 9/5/2022 11:43:28 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--exec sp_page_DonHangChuaXacThuc 1,1
create proc [dbo].[sp_page_DonHangDaXacThuc]

@Pageindex int,
@Pagesize int

as
	begin
		select *from DonHang where TrangThaiDonHang=N'Đã xác thực' order by MaDonHang asc offset @Pagesize*(@Pageindex-1) Rows Fetch next @Pagesize rows only
		select COUNT(MaDonHang) as totalcount from DonHang
	end
GO
/****** Object:  StoredProcedure [dbo].[SP_PAGE_HangSanXuat]    Script Date: 9/5/2022 11:43:28 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create proc [dbo].[SP_PAGE_HangSanXuat]

@Pageindex int,
@Pagesize int

as
	begin
		select * from HangSanXuat order by MaHang asc offset @Pagesize*(@Pageindex-1) Rows Fetch next @Pagesize rows only
		select COUNT(TenHang) as totalcount from HangSanXuat
	end
GO
/****** Object:  StoredProcedure [dbo].[SP_PAGE_HoaDonNhap]    Script Date: 9/5/2022 11:43:28 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-----------

create proc [dbo].[SP_PAGE_HoaDonNhap]

@Pageindex int,
@Pagesize int

as
	begin
		select MaHoaDonNhap,TenNCC,N.MaNCC,TongTien,NgayNhap
		from HoaDonNhap H inner join NhaCungCap N on H.MaNCC=N.MaNCC
		order by MaHoaDonNhap asc offset @Pagesize*(@Pageindex-1) Rows Fetch next @Pagesize rows only
		select COUNT(MaHoaDonNhap) as totalcount from HoaDonNhap
	end
GO
/****** Object:  StoredProcedure [dbo].[SP_PAGE_LoaiCamera]    Script Date: 9/5/2022 11:43:28 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


create proc [dbo].[SP_PAGE_LoaiCamera]

@Pageindex int,
@Pagesize int

as
	begin
		select * from LoaiCamera order by MaLoai asc offset @Pagesize*(@Pageindex-1) Rows Fetch next @Pagesize rows only
		select COUNT(TenLoai) as totalcount from LoaiCamera
	end
GO
/****** Object:  StoredProcedure [dbo].[SP_PAGE_NhaCungCap]    Script Date: 9/5/2022 11:43:28 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-------------

create proc [dbo].[SP_PAGE_NhaCungCap]

@Pageindex int,
@Pagesize int

as
	begin
		select * from NhaCungCap order by MaNCC asc offset @Pagesize*(@Pageindex-1) Rows Fetch next @Pagesize rows only
		select COUNT(MaNCC) as totalcount from NhaCungCap
	end
GO
/****** Object:  StoredProcedure [dbo].[sp_user_get_by_username_password]    Script Date: 9/5/2022 11:43:28 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


--Người dùng
create PROCEDURE [dbo].[sp_user_get_by_username_password]
(@taikhoan varchar(30), @matkhau varchar(60))
AS
    BEGIN
        SELECT  MaNguoiDung               , 
					 TenNguoiDung           ,
					 Anh          ,
					 TaiKhoan 
        FROM NguoiDung
      where  TaiKhoan = @taikhoan and MatKhau = @matkhau ;
    END;
GO
/****** Object:  StoredProcedure [dbo].[Update_Camera]    Script Date: 9/5/2022 11:43:28 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


create proc [dbo].[Update_Camera]
@MaCamera int,
@TenCamera nvarchar(100),
@MaLoai int,
@DoPhanGiai nvarchar(30),
@Chip nvarchar(20),
@OngKinh nvarchar(40),
@TamQuanSat nvarchar(30),
@NguonDien nvarchar(30),
@HinhAnh nvarchar(70),
@Gia int
as
	begin
		update Camera 
		set TenCamera=@TenCamera, 
		MaLoai=@MaLoai, 
		DoPhanGiai=@DoPhanGiai,
		Chip=@Chip,
		OngKinh=@OngKinh,
		TamQuanSat=@TamQuanSat,
		NguonDien=@NguonDien,
		HinhAnh=@HinhAnh,
		Gia=@Gia
		where MaCamera=@MaCamera
	end
GO
/****** Object:  StoredProcedure [dbo].[Update_HangSanXuat]    Script Date: 9/5/2022 11:43:28 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create proc [dbo].[Update_HangSanXuat]
@MaHang int,
@TenHang nvarchar(50),
@ThongTin nvarchar(MAX)
as
	begin
		update HangSanXuat set TenHang=@TenHang, ThongTin=@ThongTin 
		where MaHang=@MaHang
	end
GO
/****** Object:  StoredProcedure [dbo].[Update_LoaiCamera]    Script Date: 9/5/2022 11:43:28 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create proc [dbo].[Update_LoaiCamera]
@MaLoai int,
@MaHang int,
@TenLoai nvarchar(50),
@MoTa nvarchar(MAX)
as
	begin
		update LoaiCamera set MaHang=@MaHang, TenLoai=@TenLoai, MoTa=@MoTa 
		where MaLoai=@MaLoai
	end
GO
/****** Object:  StoredProcedure [dbo].[Update_NhaCungCap]    Script Date: 9/5/2022 11:43:28 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create proc [dbo].[Update_NhaCungCap]
@MaNCC int,
@TenNCC nvarchar(max),
@DiaChi nvarchar(max),
@SDT nvarchar(10),
@Email nvarchar(30)
as
	begin
		update NhaCungCap 
		set TenNCC=@TenNCC,
			DiaChi=@DiaChi,
			SDT=@SDT,
			Email=@Email
		where MaNCC=@MaNCC
	end
GO
/****** Object:  StoredProcedure [dbo].[Update_Status]    Script Date: 9/5/2022 11:43:28 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

---

create PROCEDURE [dbo].[Update_Status]
@MaDonHang NVARCHAR(50),
@TrangThaiDonHang nvarchar(30),
@TrangThaiVanChuyen nvarchar(30),
@TrangThaiThanhToan nvarchar(30)
AS
    BEGIN
		update DonHang
		set TrangThaiDonHang=@TrangThaiDonHang,
			TrangThaiVanChuyen=@TrangThaiVanChuyen,
			TrangThaiThanhToan=@TrangThaiThanhToan
		where MaDonHang=@MaDonHang
	END
GO
/****** Object:  StoredProcedure [dbo].[UpdateCate]    Script Date: 9/5/2022 11:43:28 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
 create proc [dbo].[UpdateCate](@id int,@category_name nvarchar(255),@category_status int)
  as
  begin
  update categories_product set category_name=@category_name,category_status=@category_status where id=@id
  end
GO
/****** Object:  StoredProcedure [dbo].[updatedetailorder]    Script Date: 9/5/2022 11:43:28 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create proc [dbo].[updatedetailorder](@id int,@so_luong int)
as
begin
update detail_order set so_luong=@so_luong where id=@id
declare @id_order int
set @id_order=(
select detail_order.id_order from detail_order where id=@id
)
declare @sum_price int
set @sum_price=(
   select SUM(price*so_luong) from detail_order where id_order=@id_order
)
update orderr set sum_price=@sum_price where id=@id_order
end
GO
/****** Object:  StoredProcedure [dbo].[updateorder]    Script Date: 9/5/2022 11:43:28 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create proc [dbo].[updateorder](@id int,@customer_name nvarchar(50),@phone varchar(10),@dia_chi nvarchar(200),@ask nvarchar(100),@trang_thai int)
as
begin
update orderr set customer_name=@customer_name,phone=@phone,dia_chi=@dia_chi,ask=@ask,trang_thai=@trang_thai where id=@id
end
GO
/****** Object:  StoredProcedure [dbo].[UpdateProduct]    Script Date: 9/5/2022 11:43:28 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE proc [dbo].[UpdateProduct](@id int,
@id_category int,
@product_name nvarchar(255),
@big_photo nvarchar(255),
@small_photo nvarchar(255),
@so_luong int,
@price int,
@monitor nvarchar(255),
@operating_system nvarchar(255),
@front_camera nvarchar(255),
@behind_camera nvarchar(255),
@CPU nvarchar(255),
@RAM nvarchar(255),
@ROM nvarchar(255),
@memory_stick nvarchar(255),
@SIM nvarchar(255),
@PIN nvarchar(255),
@Mota nvarchar(255) 
)
as
begin
declare @category_name nvarchar(255);
set @category_name=(
 select categories_product.category_name from  categories_product where id=@id_category
 );
update product set id_category=@id_category,
category_name=@category_name,
product_name=@product_name,
big_photo=@big_photo,
small_photo=@small_photo,
so_luong=@so_luong,
price=@price,
monitor=@monitor,
operating_system=@operating_system,
front_camera=@front_camera,
behind_camera=@behind_camera,
CPU=@CPU,
RAM=@RAM,
ROM=@ROM,
memory_stick=@memory_stick,
SIM=@SIM,
PIN=@PIN,
Mota=@Mota
where id=@id
end
GO
/****** Object:  StoredProcedure [dbo].[UpdateStatusCate]    Script Date: 9/5/2022 11:43:28 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create proc [dbo].[UpdateStatusCate](@id int,@category_status int)
as 
begin
if @category_status=0
begin
update categories_product set category_status=1 where id=@id
end
else
begin
update categories_product set category_status=0 where id=@id
end
end
GO
USE [master]
GO
ALTER DATABASE [Web] SET  READ_WRITE 
GO
