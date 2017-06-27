CREATE TABLE [dbo].[Image_Info](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[ma_website] [varchar](50) NOT NULL,
	[ma_thong_tin_anh] [varchar](20) NOT NULL,
	[ten_anh] [nvarchar](512) NOT NULL,
	[loai] [int] NOT NULL,
	[ma_anh_goc] [varchar](512) NOT NULL,
	[mo_ta] [nvarchar](512) NOT NULL,
	[mo_ta_khong_dau] [nvarchar](512) NOT NULL,
	[thu_muc] [nvarchar](512) NOT NULL,
	[url] [nvarchar](512) NOT NULL,
	[duong_dan_day_du] [nvarchar](512) NOT NULL,
	[dung_luong] [float] NOT NULL,
	[chieu_rong] [float] NOT NULL,
	[chieu_cao] [float] NOT NULL,
	[nguoi_duyet] [varchar](32) NOT NULL,
	[ngay_duyet] [datetime] NOT NULL,
	[trang_thai_duyet] [varchar](20) NOT NULL,
	[trang_thai] [varchar](32) NULL,
	[ngay_tao] [datetime] NULL,
	[nguoi_tao] [nvarchar](32) NULL,
	[ngay_cap_nhat] [datetime] NULL,
	[nguoi_cap_nhat] [nvarchar](32) NULL,
 CONSTRAINT [PK_Merchant_Image_Info] PRIMARY KEY CLUSTERED 
(
	[id] ASC,
	[ma_website] ASC,
	[ma_thong_tin_anh] ASC
)
) ON [PRIMARY]

GO

SET ANSI_PADDING OFF
GO


