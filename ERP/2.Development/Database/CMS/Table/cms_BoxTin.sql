

CREATE TABLE [dbo].[cms_BoxTin](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[ma_box_tin] [varchar](20) NOT NULL,
	[ten_box_tin] [nvarchar](1000) NOT NULL,
	[url_link] [nvarchar](500) NOT NULL,
	[ma_chuyen_muc] [nvarchar](50) NOT NULL,
	[ma_vi_tri] [nvarchar](50) NOT NULL,
	[trang_thai] [varchar](50) NOT NULL,
	[ngay_tao] [datetime] NOT NULL,
	[nguoi_tao] [nvarchar](32) NULL,
	[ngay_cap_nhat] [datetime] NULL,
	[nguoi_cap_nhat] [nvarchar](32) NULL,
	[orders] [int] NULL,
	[ma_website] [varchar](50) NOT NULL,
 CONSTRAINT [PK_cms_BoxTin] PRIMARY KEY CLUSTERED 
(
	[id] ASC,
	[ma_box_tin] ASC
)
) ON [PRIMARY]

GO

SET ANSI_PADDING OFF
GO


