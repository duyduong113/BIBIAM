CREATE PROC p_Merchant_Product_Warehouse_SyncToMySQL
@ma_san_pham [varchar](50),
@ma_gian_hang [varchar](50)
AS
begin
	if not exists (select * from OPENQUERY(MYSQL_BIBIAM_DEV,'select ma_san_pham, ma_gian_hang from Merchant_Product_Warehouse') where ma_san_pham = @ma_san_pham and ma_gian_hang = @ma_gian_hang)
	begin
			INSERT INTO openquery(MYSQL_BIBIAM_DEV,'select ma_san_pham,ma_gian_hang,stock_available,stock_onhand,ngay_tao,nguoi_tao
													from Merchant_Product_Warehouse')
			select ma_san_pham,ma_gian_hang,stock_available,stock_onhand,ngay_tao,nguoi_tao
			from Merchant_Product_Warehouse WITH (NOLOCK) where ma_san_pham = @ma_san_pham and ma_gian_hang = @ma_gian_hang
	end
	else
	begin
		update p2 set 
		p2.ma_san_pham = p.ma_san_pham ,					
		p2.ma_gian_hang = p.ma_gian_hang,					
		p2.stock_available = p.stock_available,					
		p2.stock_onhand = p.stock_onhand,
		p2.ngay_cap_nhat = p.ngay_cap_nhat,
		p2.ngay_cap_nhat=p.nguoi_cap_nhat
		FROM Merchant_Product_Warehouse AS p WITH (NOLOCK)
		JOIN OPENQUERY(MYSQL_BIBIAM_DEV, 'select * from Merchant_Product_Warehouse' ) AS p2
        ON p2.ma_san_pham = p.ma_san_pham and p2.ma_gian_hang = p.ma_gian_hang
		WHERE p.ma_san_pham = @ma_san_pham and p.ma_gian_hang = @ma_gian_hang
	end	
end