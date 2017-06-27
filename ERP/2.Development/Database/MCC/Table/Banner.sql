
CREATE TABLE [dbo].[Banner](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[ma_banner] [varchar](20) NULL,
	[url_link] [nvarchar](500) NULL,
	[image_link] [nvarchar](500) NOT NULL,
	[image] [nvarchar](200) NULL,
	[ma_chuyen_muc] [nvarchar](50) NOT NULL,
	[loai] [nvarchar](200) NULL,
	[vi_tri] [nvarchar](50) NOT NULL,
	[trang_thai] [nvarchar](50) NOT NULL,
	[ngay_tao] [datetime] NULL,
	[nguoi_tao] [nvarchar](32) NULL,
	[ngay_cap_nhat] [datetime] NULL,
	[nguoi_cap_nhat] [nvarchar](32) NULL,
 CONSTRAINT [PK_Banner] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO


