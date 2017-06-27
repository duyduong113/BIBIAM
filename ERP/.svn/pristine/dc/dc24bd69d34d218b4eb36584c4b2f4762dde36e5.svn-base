IF EXISTS (SELECT name FROM [dbo].sysobjects 
         WHERE name = 'p_Merchant_Product_Hierarchy_SyncToMySQL' AND type = 'P')
   DROP PROCEDURE [dbo].p_Merchant_Product_Hierarchy_SyncToMySQL
GO
 
CREATE PROCEDURE [dbo].p_Merchant_Product_Hierarchy_SyncToMySQL
	@ma_san_pham [varchar](255),
	@ma_gian_hang [varchar](50)
AS
BEGIN			    
	if not exists (select * from OPENQUERY(MYSQL_BIBIAM_DEV,'select ma_san_pham,ma_gian_hang from bibiam_dev1.Merchant_Product_Hierarchy') where ma_san_pham=@ma_san_pham and ma_gian_hang = @ma_gian_hang)
	begin
			INSERT INTO openquery(MYSQL_BIBIAM_DEV,'select ma_san_pham,ma_gian_hang,ma_cay_phan_cap_1,ma_cay_phan_cap_2,ma_cay_phan_cap_3,ma_cay_phan_cap_4,ma_cay_phan_cap_5,ma_cay_phan_cap_6,ma_cay_phan_cap_7,ma_cay_phan_cap_8,ma_cay_phan_cap_9,ma_cay_phan_cap_10,trang_thai,ngay_tao,nguoi_tao,ngay_cap_nhat,nguoi_cap_nhat
										from bibiam_dev1.Merchant_Product_Hierarchy')
			select ma_san_pham,ma_gian_hang,ma_cay_phan_cap_1,ma_cay_phan_cap_2,ma_cay_phan_cap_3,ma_cay_phan_cap_4,ma_cay_phan_cap_5,ma_cay_phan_cap_6,ma_cay_phan_cap_7,ma_cay_phan_cap_8,ma_cay_phan_cap_9,ma_cay_phan_cap_10,trang_thai,ngay_tao,nguoi_tao,ngay_cap_nhat,nguoi_cap_nhat
			from Merchant_Product_Hierarchy WITH (NOLOCK) where ma_san_pham=@ma_san_pham and ma_gian_hang = @ma_gian_hang
	end
	else
	begin
	if not exists (select p.id from openquery(MYSQL_BIBIAM_DEV,'select * from bibiam_dev1.merchant_product_hierarchy') p2 RIGHT JOIN (select * from merchant_product_hierarchy where ma_san_pham = @ma_san_pham and ma_gian_hang = @ma_gian_hang) p
						on  p2.ma_san_pham = p.ma_san_pham and p2.ma_gian_hang = p.ma_gian_hang
					where p2.ma_san_pham = p.ma_san_pham and
							p2.ma_gian_hang = p.ma_gian_hang and 
							p2.ma_cay_phan_cap_1 = p.ma_cay_phan_cap_1 and
							p2.ma_cay_phan_cap_2 = p.ma_cay_phan_cap_2 and
							p2.ma_cay_phan_cap_3 = p.ma_cay_phan_cap_3 and
							p2.ma_cay_phan_cap_4 = p.ma_cay_phan_cap_4 or (p2.ma_cay_phan_cap_4 is null and p.ma_cay_phan_cap_4 is null) and
							p2.ma_cay_phan_cap_5 = p.ma_cay_phan_cap_5 or (p2.ma_cay_phan_cap_5 is null and p.ma_cay_phan_cap_5 is null) and
							p2.ma_cay_phan_cap_6 = p.ma_cay_phan_cap_6 or (p2.ma_cay_phan_cap_6 is null and p.ma_cay_phan_cap_6 is null) and
							p2.ma_cay_phan_cap_7 = p.ma_cay_phan_cap_7 or (p2.ma_cay_phan_cap_7 is null and p.ma_cay_phan_cap_7 is null) and
							p2.ma_cay_phan_cap_8 = p.ma_cay_phan_cap_8 or (p2.ma_cay_phan_cap_8 is null and p.ma_cay_phan_cap_8 is null) and
							p2.ma_cay_phan_cap_9 = p.ma_cay_phan_cap_9 or (p2.ma_cay_phan_cap_9 is null and p.ma_cay_phan_cap_9 is null) and
							p2.ma_cay_phan_cap_10 = p.ma_cay_phan_cap_10 or (p2.ma_cay_phan_cap_10 is null and p.ma_cay_phan_cap_10 is null) and
							p2.trang_thai = p.trang_thai)						
		update --openquery(MYSQL_BIBIAM_DEV, 'SELECT * FROM bibiam_dev1.merchant_product')
		p2
		set p2.ma_san_pham = p.ma_san_pham,
		p2.ma_gian_hang = p.ma_gian_hang, 
		p2.ma_cay_phan_cap_1 = p.ma_cay_phan_cap_1,
		p2.ma_cay_phan_cap_2 = p.ma_cay_phan_cap_2,
		p2.ma_cay_phan_cap_3 = p.ma_cay_phan_cap_3,
		p2.ma_cay_phan_cap_4 = p.ma_cay_phan_cap_4,
		p2.ma_cay_phan_cap_5 = p.ma_cay_phan_cap_5,
		p2.ma_cay_phan_cap_6 = p.ma_cay_phan_cap_6,
		p2.ma_cay_phan_cap_7 = p.ma_cay_phan_cap_7,
		p2.ma_cay_phan_cap_8 = p.ma_cay_phan_cap_8,
		p2.ma_cay_phan_cap_9 = p.ma_cay_phan_cap_9,
		p2.ma_cay_phan_cap_10 = p.ma_cay_phan_cap_10,
		p2.trang_thai = p.trang_thai
		FROM merchant_product_hierarchy AS p WITH (NOLOCK)
		JOIN OPENQUERY(MYSQL_BIBIAM_DEV, 'select * from bibiam_dev1.merchant_product_hierarchy' ) AS p2
        ON p2.ma_san_pham = p.ma_san_pham and p2.ma_gian_hang = p.ma_gian_hang
		WHERE p.ma_san_pham=@ma_san_pham and p.ma_gian_hang = @ma_gian_hang
	end	
END