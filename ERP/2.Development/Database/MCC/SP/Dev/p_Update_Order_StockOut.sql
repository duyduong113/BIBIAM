USE [MCCDev]
GO
/****** Object:  StoredProcedure [dbo].[p_Update_Order_StockOut]    Script Date: 05/01/2017 11:58:35 AM Author: BaoVT ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[p_Update_Order_StockOut]
	@ma_don_hang [varchar](50),
	@ma_phieu_xuat_kho [varchar](50)
AS
BEGIN			    
	update Merchant_OrderDetail 
	set trang_thai_xuat_kho='DA_XUAT_KHO'
	where (exists (select ma_san_pham from Merchant_StockOutDetail where ma_phieu_xuat_kho=@ma_phieu_xuat_kho)) and ma_don_hang=@ma_don_hang
END