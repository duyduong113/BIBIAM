CREATE PROC p_Merchant_Product_Image_DelMySQL
	@ma_san_pham [varchar](255),
	@ma_gian_hang [varchar](50)
AS
BEGIN
	if exists (select * from OPENQUERY(MYSQL_BIBIAM_DEV,'select * from Merchant_Product_Image') where ma_san_pham = @ma_san_pham and ma_gian_hang = @ma_gian_hang)
	BEGIN
		delete from OPENQUERY(MYSQL_BIBIAM_DEV, 'SELECT * from Merchant_Product_Image') where ma_gian_hang=@ma_gian_hang AND ma_san_pham=@ma_san_pham
	END
END