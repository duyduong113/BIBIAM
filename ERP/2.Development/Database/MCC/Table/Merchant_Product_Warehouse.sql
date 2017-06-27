
--drop table [Merchant_Product_Warehouse]


CREATE TABLE [dbo].[Merchant_Product_Warehouse](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[ma_san_pham] [nvarchar](50) NOT NULL,
	[ma_gian_hang] [nvarchar](50) NOT NULL,
	[stock_available] [int] NOT NULL DEFAULT 0,
	[stock_onhand] [int] NOT NULL DEFAULT 0,
	[ngay_tao] [datetime] NULL,
	[nguoi_tao] [nvarchar](32) NULL,
	[ngay_cap_nhat] [datetime] NULL,
	[nguoi_cap_nhat] [nvarchar](32) NULL,
 CONSTRAINT [PK_Merchant_Product_Warehouse] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)
) ON [PRIMARY]
