CREATE TABLE [dbo].[BrandManagement](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[ma_thuong_hieu] [varchar](20) NOT NULL,
	[ten_thuong_hieu] [varchar](500) NOT NULL,
	[logo] [varchar](1000) NOT NULL,
	[trang_thai] [varchar](50) NOT NULL,
	[mo_ta] [varchar](1000) NULL,
	[nguoi_tao] [nvarchar](128) NOT NULL,
	[ngay_tao] [datetime] NOT NULL,
	[nguoi_cap_nhat] [nvarchar](128) NULL,
	[ngay_cap_nhat] [datetime] NULL,
 CONSTRAINT [PK_BrandManagement] PRIMARY KEY CLUSTERED 
(
	[id] ASC,
	[ma_thuong_hieu] ASC
)
) ON [PRIMARY]

GO

SET ANSI_PADDING OFF
GO
