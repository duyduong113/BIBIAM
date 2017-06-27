
CREATE TABLE [dbo].[Footer](
	[id] [INT] IDENTITY(1,1) NOT NULL,
	[ma_footer] [VARCHAR](20) NOT NULL,
	[url_link] [NVARCHAR](500) NOT NULL,
	[image_link] [NVARCHAR](500) NOT NULL,
	[noi_dung] [NVARCHAR](MAX) NULL,
	[loai] [VARCHAR](50) NULL,
	[trang_thai] [VARCHAR](50) NOT NULL,
	[ngay_tao] [DATETIME] NOT NULL,
	[nguoi_tao] [NVARCHAR](32) NOT NULL,
	[ngay_cap_nhat] [DATETIME] NULL,
	[nguoi_cap_nhat] [NVARCHAR](32) NULL,
	[ten_footer] [NVARCHAR](500) NULL,
 CONSTRAINT [PK_Footer] PRIMARY KEY CLUSTERED 
(
	[id] ASC,
	[ma_footer] ASC
)) ON [PRIMARY] 

GO

SET ANSI_PADDING OFF
GO


