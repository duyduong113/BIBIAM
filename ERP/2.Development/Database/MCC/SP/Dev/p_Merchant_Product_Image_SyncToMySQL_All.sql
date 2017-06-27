
IF EXISTS (SELECT name FROM [dbo].sysobjects 
         WHERE name = 'p_Merchant_Product_Image_SyncToMySQL_All' AND type = 'P')
   DROP PROCEDURE [dbo].p_Merchant_Product_Image_SyncToMySQL_All
GO
 

CREATE PROCEDURE [dbo].p_Merchant_Product_Image_SyncToMySQL_All
AS
begin
	
		update 
		p2
		set p2.ma_san_pham = p.ma_san_pham ,					
		p2.ma_gian_hang = p.ma_gian_hang,					
		p2.url = p.url,					
		p2.ma_anh_goc = p.ma_anh_goc
		FROM Merchant_Product_Image AS p WITH (NOLOCK)
		JOIN OPENQUERY(MYSQL_BIBIAM_DEV, 'select * from bibiam_dev1.Merchant_Product_Image' ) AS p2
        ON p2.ma_san_pham = p.ma_san_pham and p2.ma_gian_hang = p.ma_gian_hang


		INSERT INTO openquery(MYSQL_BIBIAM_DEV,'select ma_san_pham, ma_gian_hang, url, ma_anh_goc
													from bibiam_dev1.Merchant_Product_Image')
		select p.ma_san_pham, p.ma_gian_hang, p.url, p.ma_anh_goc
		from Merchant_Product_Image as p WITH (NOLOCK)
		LEFT JOIN OPENQUERY(MYSQL_BIBIAM_DEV, 'select * from bibiam_dev1.Merchant_Product_Image' ) AS p2
        ON p2.ma_san_pham = p.ma_san_pham and p2.ma_gian_hang = p.ma_gian_hang
		WHERE p2.ma_san_pham IS NULL
end

