
--drop table [SEO_MerchantProduct]


CREATE TABLE [dbo].[SEO_MerchantProduct](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[ma_san_pham] [varchar](50) NOT NULL,
	[ma_gian_hang] [varchar](50) NOT NULL,
	[og_title] [nvarchar](100) NULL,
	[og_description] [nvarchar](100) NULL,
	[og_image] [nvarchar](100) NULL,
	[og_keyword] [nvarchar](100) NULL,
	[robot] [nvarchar](100) NULL,
	[ngay_tao] [datetime] NULL,
	[nguoi_tao] [nvarchar](32) NULL,
	[ngay_cap_nhat] [datetime] NULL,
	[nguoi_cap_nhat] [nvarchar](32) NULL,
 CONSTRAINT [PK_SEO_MerchantProduct] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)
) ON [PRIMARY]