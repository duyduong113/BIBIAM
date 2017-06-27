
CREATE TABLE [dbo].[Customer](
	[id] [INT] IDENTITY(1,1) NOT NULL
	[ma_khach_hang] [NVARCHAR](50) NOT NULL,
	[ma_user] [NVARCHAR](50) NULL,
	[email] [NVARCHAR](30) NULL,
	[sdt] [NVARCHAR](50) NULL,
	[hoten] [NVARCHAR](50) NULL,
	[ngay_tao] [DATETIME] NULL,
	[nguoi_tao] [NVARCHAR](50) NULL,
	[ngay_cap_nhat] [DATETIME] NULL,
	[nguoi_cap_nhat] [NVARCHAR](50) NULL,
 CONSTRAINT [PK_Customer] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)
) ON [PRIMARY]

GO


