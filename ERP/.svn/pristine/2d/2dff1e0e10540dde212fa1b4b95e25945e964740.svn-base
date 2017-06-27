
IF EXISTS (SELECT name FROM [dbo].sysobjects 
         WHERE name = 'p_ProductInfo_to_MerchantProduct' AND type = 'P')
   DROP PROCEDURE [dbo].p_ProductInfo_to_MerchantProduct
GO
  

create PROCEDURE [dbo].p_ProductInfo_to_MerchantProduct
	@id int,
	@ma_san_pham [varchar](255),
	@ma_gian_hang [varchar](255)
AS
BEGIN	
		
	if not exists (select * from Merchant_Product where ma_san_pham=@ma_san_pham and ma_gian_hang = @ma_gian_hang)
	begin
			INSERT INTO Merchant_Product
			select ma_san_pham,ma_loai_san_pham,ten_san_pham,mo_ta,tu_khoa,tag,slug,trong_so,nguoi_duyet,ngay_duyet,
			trang_thai_duyet,nguoi_xuat_ban,ngay_xuat_ban,trang_thai_xuat_ban,trang_thai,ngay_tao,nguoi_tao,ngay_cap_nhat,
			nguoi_cap_nhat,url,@ma_gian_hang 
			from ERPAPD..Product_Info where id=@id
	end
		
END
