CREATE PROCEDURE [dbo].p_Product_Price_SyncToMySQL	
	@ma_san_pham [varchar](255)
AS
BEGIN
	
	DECLARE PRICE_CURSOR CURSOR	FOR (SELECT id, ma_gia_san_pham FROM PRODUCT_PRICE WHERE ma_san_pham = @ma_san_pham)
	DECLARE @ID INT,@MA_GIA_SAN_PHAM [varchar](255)
	OPEN PRICE_CURSOR
	FETCH NEXT FROM PRICE_CURSOR INTO @ID, @MA_GIA_SAN_PHAM
	WHILE @@FETCH_STATUS = 0
	BEGIN
		if not exists (select * from OPENQUERY(MYSQL, 'SELECT * FROM BIBIAM_DEV.PRODUCT_PRICE') where ma_gia_san_pham = @MA_GIA_SAN_PHAM)
		begin
				INSERT INTO openquery(MYSQL,'select ma_gia_san_pham,ma_san_pham,gia_mua,gia_ban_le,gia_ban_si,gia_khuyen_mai,gia_luu_kho,ngay_bat_dau,ngay_ket_thuc,nguoi_duyet,ngay_duyet,trang_thai_duyet,
				nguoi_xuat_ban,ngay_xuat_ban,trang_thai_xuat_ban,trang_thai,ngay_tao,nguoi_tao,ngay_cap_nhat,nguoi_cap_nhat from bibiam_dev.Product_Price')
				select ma_gia_san_pham,ma_san_pham,gia_mua,gia_ban_le,gia_ban_si,gia_khuyen_mai,gia_luu_kho,ngay_bat_dau,ngay_ket_thuc,nguoi_duyet,ngay_duyet,trang_thai_duyet,
				nguoi_xuat_ban,ngay_xuat_ban,trang_thai_xuat_ban,trang_thai,ngay_tao,nguoi_tao,ngay_cap_nhat,nguoi_cap_nhat
				from Product_Price WITH (NOLOCK) where ma_gia_san_pham=@MA_GIA_SAN_PHAM
		end
		else
		begin
			update p2
			set p2.ma_san_pham=p.ma_san_pham,p2.gia_mua=p.gia_mua,p2.gia_ban_le=p.gia_ban_le,p2.gia_ban_si=p.gia_ban_si,
			p2.gia_khuyen_mai=p.gia_khuyen_mai,p2.gia_luu_kho=p.gia_luu_kho,p2.ngay_bat_dau=p.ngay_bat_dau,p2.ngay_ket_thuc=p.ngay_ket_thuc,
			p2.nguoi_duyet=p.nguoi_duyet,p2.ngay_duyet=p.ngay_duyet,p2.trang_thai=p.trang_thai,p2.trang_thai_duyet=p.trang_thai_duyet,	p2.nguoi_xuat_ban=p.nguoi_xuat_ban,p2.ngay_xuat_ban=p.ngay_xuat_ban,
			p2.trang_thai_xuat_ban=p.trang_thai_xuat_ban,p2.ngay_tao=p.ngay_tao,p2.nguoi_tao=p.nguoi_tao,p2.ngay_cap_nhat=p.ngay_cap_nhat,p2.nguoi_cap_nhat=p.nguoi_cap_nhat
			FROM Product_Price AS p WITH (NOLOCK)
			JOIN OPENQUERY(MYSQL, 'select * from bibiam_dev.Product_Price' ) AS p2
			ON p.ma_gia_san_pham = p2.ma_gia_san_pham
			WHERE p.ma_gia_san_pham=@MA_GIA_SAN_PHAM
		end
		FETCH NEXT FROM PRICE_CURSOR INTO @ID, @MA_GIA_SAN_PHAM
	END
	CLOSE PRICE_CURSOR 
	DEALLOCATE PRICE_CURSOR
END