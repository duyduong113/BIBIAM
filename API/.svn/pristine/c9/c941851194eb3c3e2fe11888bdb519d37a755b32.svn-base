CREATE PROCEDURE [dbo].p_Product_Image_SyncToMySQL
	@ma_san_pham [varchar](255)
AS
BEGIN
	
	delete from openquery(MYSQL,'select * from bibiam_dev.Product_Image') where ma_san_pham=@ma_san_pham
		
	INSERT INTO openquery(MYSQL,'select ma_san_pham,ma_thong_tin_anh,ten_anh,ma_anh_goc,loai,url,dung_luong,chieu_rong,chieu_cao,trang_thai,ngay_tao,nguoi_tao,ngay_cap_nhat,nguoi_cap_nhat from bibiam_dev.Product_Image')
	select ma_san_pham,ma_thong_tin_anh,ten_anh,ma_anh_goc,loai,url,dung_luong,chieu_rong,chieu_cao,trang_thai,ngay_tao,nguoi_tao,ngay_cap_nhat,nguoi_cap_nhat
	from Product_Image WITH(NOLOCK) where ma_san_pham=@ma_san_pham
	
END