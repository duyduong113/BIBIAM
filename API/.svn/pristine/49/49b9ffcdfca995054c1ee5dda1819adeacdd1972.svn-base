

IF EXISTS (SELECT name FROM [dbo].sysobjects 
         WHERE name = 'p_Insert_Product_Image' AND type = 'P')
   DROP PROCEDURE [dbo].p_Insert_Product_Image
GO
  

CREATE PROC p_Insert_Product_Image
@ma_san_pham nvarchar(50),
@ma_thong_tin_anh nvarchar(50),
@UserName nvarchar(50)
as
begin

	insert into Product_Image (ma_san_pham,ma_thong_tin_anh,ten_anh,ma_anh_goc,loai,url,dung_luong,chieu_rong,chieu_cao,nguoi_tao,ngay_tao,nguoi_cap_nhat,ngay_cap_nhat)
	select @ma_san_pham,ma_thong_tin_anh,ten_anh,ma_anh_goc,loai,url,dung_luong,chieu_rong,chieu_cao,@UserName,GETDATE(),@UserName,GETDATE() from Image_Info
	where ma_anh_goc =@ma_thong_tin_anh

end 


