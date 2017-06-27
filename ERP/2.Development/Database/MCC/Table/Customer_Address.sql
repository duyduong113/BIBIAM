USE [MCCDev]
GO

/****** Object:  Table [dbo].[Customer_Address]    Script Date: 02/24/2017 12:11:16 AM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Customer_Address](
	[id] [INT] NOT NULL,
	[ma_gian_hang] [NVARCHAR](50) NULL,
	[ma_user] [NVARCHAR](50) NULL,
	[ma_khach_hang] [NVARCHAR](50) NOT NULL,
	[ten] [NVARCHAR](50) NOT NULL,
	[email] [NVARCHAR](50) NOT NULL,
	[sdt] [NVARCHAR](50) NOT NULL,
	[dia_chi] [NVARCHAR](255) NOT NULL,
	[quan_huyen] [NVARCHAR](50) NOT NULL,
	[tinh] [NVARCHAR](50) NOT NULL,
	[diachi_macdinh] [INT] NULL,
	[stt_diachi_phu] [INT] NULL,
 CONSTRAINT [PK_Customer_Address] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO


