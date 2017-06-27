
IF EXISTS (SELECT name FROM [dbo].sysobjects 
         WHERE name = 'p_Merchant_Order_SyncToMySQL' AND type = 'P')
   DROP PROCEDURE [dbo].[p_Merchant_Order_SyncToMySQL]
GO
 
CREATE PROCEDURE [dbo].[p_Merchant_Order_SyncToMySQL]
	@ma_don_hang [varchar](255),
	@ma_gian_hang [varchar](50)
AS
BEGIN			    
	if not exists (select * from OPENQUERY(MYSQL_BIBIAM_DEV,'select ma_don_hang,ma_gian_hang from bibiam_dev1.order_merchant') where ma_don_hang=@ma_don_hang and ma_gian_hang = @ma_gian_hang)
	begin
			--Insert Order Header
			INSERT INTO openquery(MYSQL_BIBIAM_DEV,'select ma_don_hang,ma_gian_hang,ma_khach_hang,dia_chi_giao_hang,quan_huyen_giao,
													tinh_thanh_giao,so_dien_thoai_giao,so_loai_san_pham,so_luong_san_pham,so_luong_khuyen_mai,
													tong_tien_don_hang,tong_tien_khuyen_mai,tong_tien_thanh_toan,hinh_thuc_thanh_toan,
													trang_thai_don_hang,ngay_xac_nhan,ngay_lay_hang,ngay_giao_hang,ngay_tao,nguoi_tao,
													ngay_cap_nhat,nguoi_cap_nhat
													from bibiam_dev1.order_merchant')
			select  ma_don_hang,ma_gian_hang,ma_khach_hang,dia_chi_giao_hang,quan_huyen_giao,
													tinh_thanh_giao,so_dien_thoai_giao,so_loai_san_pham,so_luong_san_pham,so_luong_khuyen_mai,
													tong_tien_don_hang,tong_tien_khuyen_mai,tong_tien_thanh_toan,hinh_thuc_thanh_toan,
													trang_thai_don_hang,ngay_xac_nhan,ngay_lay_hang,ngay_giao_hang,ngay_tao,nguoi_tao,
													ngay_cap_nhat,nguoi_cap_nhat
			from Merchant_OrderHeader WITH (NOLOCK) where ma_don_hang=@ma_don_hang and ma_gian_hang = @ma_gian_hang
			
			--Insert Order Detail
			INSERT INTO openquery(MYSQL_BIBIAM_DEV,'select ma_don_hang,ma_san_pham,ma_gian_hang,so_luong,khuyen_mai
													sp_khuyen_mai,ngay_tao,nguoi_tao,ngay_cap_nhat,nguoi_cap_nhat 
													FROM bibiam_dev1.orderdetail')
			select  ma_don_hang,ma_san_pham,ma_gian_hang,so_luong,khuyen_mai
													sp_khuyen_mai,ngay_tao,nguoi_tao,ngay_cap_nhat,nguoi_cap_nhat
			from Merchant_OrderDetail WITH (NOLOCK) where ma_don_hang=@ma_don_hang and ma_gian_hang = @ma_gian_hang
	end
END