CREATE TABLE [dbo].[Merchant_Catalog](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[ma_gian_hang] [varchar](20) NOT NULL,
	[ma_catalog] [varchar](20) NOT NULL,
	[ten_catalog] [nvarchar](512) NOT NULL,
	[mo_ta] [nvarchar](512) NULL,
	[ma_ta_khong_dau] [nvarchar](512) NULL,
	[thu_muc] [nvarchar](512) NOT NULL,
	[url] [nvarchar](512) NOT NULL,
	[duong_dan_day_du] [nvarchar](512) NOT NULL,
	[dung_luong] [float] NOT NULL,
	[ngay_duyet] [datetime] NULL,
	[nguoi_duyet] [nvarchar](32) NULL,
	[trang_thai_duyet] [varchar](20) NULL,
	[trang_thai] [varchar](32) NULL,
	[ngay_tao] [datetime] NOT NULL,
	[nguoi_tao] [nvarchar](32) NULL,
	[ngay_cap_nhat] [datetime] NULL,
	[nguoi_cap_nhat] [nvarchar](32) NULL,
 CONSTRAINT [PK_Merchant_Catalog] PRIMARY KEY CLUSTERED 
(
	[id] ASC,
	[ma_gian_hang] ASC,
	[ma_catalog] ASC
)
) ON [PRIMARY]

GO

SET ANSI_PADDING OFF
GO


