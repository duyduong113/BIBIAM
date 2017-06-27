CREATE TABLE [dbo].[Merchant_StockOutHeader](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[ma_phieu_xuat_kho] [varchar](20) NOT NULL,
	[ma_gian_hang] [varchar](20) NOT NULL,
	[ma_kho] [varchar](20) NOT NULL,
	[ma_chung_tu] [varchar](512) NOT NULL,
	[danh_sach_don_hang] [varchar](1000) NOT NULL,
	[trang_thai] [varchar](32) NOT NULL,
	[ngay_xuat_kho] [datetime] NOT NULL,
	[nguoi_xuat_kho] [nvarchar](128) NOT NULL,
	[nguoi_nhan] [nvarchar](128) NOT NULL,
	[ghi_chu] [nvarchar](4000) NULL,
	[nguoi_tao] [nvarchar](512) NOT NULL,
	[ngay_tao] [datetime] NOT NULL,
	[nguoi_cap_nhat] [nvarchar](512) NULL,
	[ngay_cap_nhat] [datetime] NULL,
 CONSTRAINT [PK_Merchant_StockOutHeader] PRIMARY KEY CLUSTERED 
(
	[id] ASC,
	[ma_phieu_xuat_kho] ASC,
	[ma_gian_hang] ASC
)
) ON [PRIMARY]

GO

SET ANSI_PADDING OFF
GO


