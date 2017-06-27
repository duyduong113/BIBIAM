﻿
IF  EXISTS (SELECT * FROM sys.objects 
WHERE object_id = OBJECT_ID(N'[dbo].[p_Merchant_Product_Warehouse_StockIn_SyncToMySQL]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].p_Merchant_Product_Warehouse_StockIn_SyncToMySQL
GO


CREATE PROCEDURE [dbo].p_Merchant_Product_Warehouse_StockIn_SyncToMySQL
@ma_phieu_nhap_kho nvarchar(20)
AS
BEGIN
   
	
	select ma_gian_hang,ma_san_pham,sum(so_luong_thuc_te) as so_luong
	into #tmpMerchant_StockInDetail
	from Merchant_StockInDetail
	where ma_phieu_nhap_kho=@ma_phieu_nhap_kho
	group by ma_gian_hang,ma_san_pham

	--Cập nhật kho ở BE
	update w
	set w.stock_onhand= w.stock_onhand + si.so_luong, w.stock_available= w.stock_available + si.so_luong,
	ngay_cap_nhat = GETDATE()
	from Merchant_Product_Warehouse w
	join #tmpMerchant_StockInDetail si
	on w.ma_gian_hang=si.ma_gian_hang
	and w.ma_san_pham=si.ma_san_pham
	
	--Thêm kho ở BE
	insert into Merchant_Product_Warehouse
	select si.ma_san_pham,si.ma_gian_hang,si.so_luong,si.so_luong, GETDATE(),'system', GETDATE() ,'system' from Merchant_Product_Warehouse w
	right join #tmpMerchant_StockInDetail si
	on w.ma_gian_hang=si.ma_gian_hang
	and w.ma_san_pham=si.ma_san_pham
	where w.ma_gian_hang is null

	--Cập nhật kho ở FE
	update w
	set w.stock_onhand= w.stock_onhand + si.so_luong, w.stock_available= w.stock_available + si.so_luong,
	ngay_cap_nhat = GETDATE()
	from openquery (MYSQL_BIBIAM_DEV,'select * from Merchant_Product_Warehouse') w
	join #tmpMerchant_StockInDetail si
	on w.ma_gian_hang=si.ma_gian_hang
	and w.ma_san_pham=si.ma_san_pham

	--Thêm kho ở FE
	INSERT INTO openquery(MYSQL_BIBIAM_DEV,'select ma_san_pham,ma_gian_hang,stock_available,stock_onhand,nguoi_tao,ngay_tao,nguoi_cap_nhat,ngay_cap_nhat
													from Merchant_Product_Warehouse')
	select si.ma_san_pham,si.ma_gian_hang,si.so_luong,si.so_luong,'system', GETDATE(),'system', GETDATE() 
	from openquery (MYSQL_BIBIAM_DEV,'select * from Merchant_Product_Warehouse') w
	right join #tmpMerchant_StockInDetail si
	on w.ma_gian_hang=si.ma_gian_hang
	and w.ma_san_pham=si.ma_san_pham
	where w.ma_gian_hang is null

end