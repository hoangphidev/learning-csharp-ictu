USE [db_sinhvien]
GO
/****** Object:  Table [dbo].[tb_sinhvien]    Script Date: 10/15/2019 6:48:55 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tb_sinhvien](
	[MaSV] [nvarchar](20) NOT NULL,
	[HoSV] [nvarchar](50) NOT NULL,
	[TenSV] [nvarchar](50) NOT NULL,
	[NgaySinh] [date] NOT NULL,
	[GioiTinh] [nvarchar](10) NOT NULL,
	[MaKhoa] [nvarchar](50) NOT NULL,
 CONSTRAINT [PK_tb_sinhvien] PRIMARY KEY CLUSTERED 
(
	[MaSV] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
INSERT [dbo].[tb_sinhvien] ([MaSV], [HoSV], [TenSV], [NgaySinh], [GioiTinh], [MaKhoa]) VALUES (N'DTC1', N'Phi', N'Hoàng', CAST(N'1998-08-13' AS Date), N'Nam', N'CNTT')
INSERT [dbo].[tb_sinhvien] ([MaSV], [HoSV], [TenSV], [NgaySinh], [GioiTinh], [MaKhoa]) VALUES (N'DTC3', N'Nguyễn Mạnh', N'Huỳnh', CAST(N'1994-03-27' AS Date), N'Nam', N'KTYS')
INSERT [dbo].[tb_sinhvien] ([MaSV], [HoSV], [TenSV], [NgaySinh], [GioiTinh], [MaKhoa]) VALUES (N'DTC5', N'Nguyễn', N'Thuý', CAST(N'1998-01-10' AS Date), N'Nữ', N'HTTT')
