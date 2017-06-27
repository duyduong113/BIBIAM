IF EXISTS (SELECT name FROM [dbo].sysobjects 
         WHERE name = 'p_Merchant_Product_Image_SyncToMySQL' AND type = 'P')
   DROP PROCEDURE [dbo].p_Merchant_Product_Image_SyncToMySQL
GO
 
CREATE PROCEDURE [dbo].p_Merchant_Product_Image_SyncToMySQL
	@ma_san_pham [varchar](50),
	@ma_gian_hang [varchar](50)
AS
BEGIN			    
	

	DELETE FROM OPENQUERY(MYSQL_BIBIAM_DEV,'select ma_san_pham, ma_gian_hang, ma_anh_goc from Merchant_Product_Image') where ma_san_pham = @ma_san_pham and ma_gian_hang = @ma_gian_hang

	INSERT INTO openquery(MYSQL_BIBIAM_DEV,'select ma_san_pham,ma_gian_hang,url,ma_anh_goc
													from Merchant_Product_Image')
	select p.ma_san_pham, p.ma_gian_hang, p.url, p.ma_anh_goc
	FROM Merchant_Product_Image AS p WITH (NOLOCK)
	LEFT JOIN OPENQUERY(MYSQL_BIBIAM_DEV, 'select * from Merchant_Product_Image' ) AS p2
    ON p2.ma_san_pham = p.ma_san_pham 
	and p2.ma_gian_hang = p.ma_gian_hang
	and p2.ma_anh_goc = p.ma_anh_goc
	where p.ma_san_pham = @ma_san_pham and P.ma_gian_hang = @ma_gian_hang
	AND p2.ma_san_pham is null

END