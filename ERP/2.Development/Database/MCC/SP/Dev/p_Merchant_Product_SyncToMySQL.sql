IF EXISTS (SELECT name FROM [dbo].sysobjects 
         WHERE name = 'p_Merchant_Product_SyncToMySQL' AND type = 'P')
   DROP PROCEDURE [dbo].p_Merchant_Product_SyncToMySQL
GO
CREATE PROCEDURE [dbo].p_Merchant_Product_SyncToMySQL
	@ma_san_pham [varchar](255),
	@ma_gian_hang [varchar](50)
AS
BEGIN			    
	if not exists (select * from OPENQUERY(MYSQL_BIBIAM_DEV,'select ma_san_pham,ma_gian_hang from merchant_product') where ma_san_pham=@ma_san_pham and ma_gian_hang = @ma_gian_hang)
	begin
			INSERT INTO openquery(MYSQL_BIBIAM_DEV,'select ma_san_pham,ma_loai_san_pham,ma_gian_hang,ten_san_pham,part_no,mo_ta,noi_dung,trong_so,don_gia,gia_si,tu_khoa,tag,slug,url,xuat_xu,thuong_hieu,model,minimum_order,nguoi_xuat_ban,ngay_xuat_ban,trang_thai_xuat_ban,nguoi_duyet,ngay_duyet,trang_thai_duyet,trang_thai,ngay_tao,nguoi_tao,ngay_cap_nhat,nguoi_cap_nhat 
										from merchant_product')
			select ma_san_pham,ma_loai_san_pham,ma_gian_hang,ten_san_pham,part_no,mo_ta,noi_dung,trong_so,don_gia,gia_si,tu_khoa,tag, REPLACE(REPLACE(REPLACE(REPLACE(REPLACE(REPLACE(REPLACE(slug,CHAR(208), '-'),CHAR(10),'-'),CHAR(13),'-'),CHAR(13),'-'),CHAR(3),'-'),CHAR(' '),'-'),CHAR(5),'-') + '-' + CAST(id as varchar(10))
			,url,xuat_xu,thuong_hieu,model,minimum_order,nguoi_xuat_ban,ngay_xuat_ban,trang_thai_xuat_ban,nguoi_duyet,ngay_duyet,trang_thai_duyet,trang_thai,ngay_tao,nguoi_tao,ngay_cap_nhat,nguoi_cap_nhat			
			from merchant_product WITH (NOLOCK) where ma_san_pham=@ma_san_pham and ma_gian_hang = @ma_gian_hang
	end
	else
	begin
				
		update p2 set 
		p2.ma_san_pham =p.ma_san_pham,
		p2.ma_loai_san_pham= p.ma_loai_san_pham,
		p2.ma_gian_hang = p.ma_gian_hang,
		p2.ten_san_pham = p.ten_san_pham,
		p2.part_no = p.part_no,
		p2.mo_ta = p.mo_ta,
		p2.noi_dung = CAST(p.noi_dung as ntext),
		p2.trong_so = p.trong_so,
		p2.don_gia = p.don_gia,
		p2.gia_si =p.gia_si,
		p2.tu_khoa = p.tu_khoa,
		p2.tag = p.tag,
		p2.slug =  REPLACE(REPLACE(REPLACE(REPLACE(REPLACE(REPLACE(REPLACE(p.slug,CHAR(208), '-'),CHAR(10),'-'),CHAR(13),'-'),CHAR(13),'-'),CHAR(3),'-'),CHAR(' '),'-'),CHAR(5),'-') + '-' + CAST(p.id as varchar(10)),
		p2.url = p.url,
		p2.xuat_xu = p.xuat_xu,
		p2.thuong_hieu = p.thuong_hieu,
		p2.model =p.model,
		p2.minimum_order = p.minimum_order,
		p2.nguoi_xuat_ban = p.nguoi_xuat_ban,
		p2.ngay_xuat_ban = p.ngay_xuat_ban,
		p2.trang_thai_xuat_ban = p.trang_thai_xuat_ban,
		p2.nguoi_duyet =p.nguoi_duyet,
		p2.ngay_duyet = p.ngay_duyet,
		p2.trang_thai_duyet = p.trang_thai_duyet,
		p2.trang_thai = p.trang_thai
		FROM merchant_product AS p WITH (NOLOCK)
		JOIN OPENQUERY(MYSQL_BIBIAM_DEV, 'select * from merchant_product' ) AS p2
        ON p2.ma_san_pham = p.ma_san_pham and p2.ma_gian_hang = p.ma_gian_hang
		WHERE p.ma_san_pham=@ma_san_pham and p.ma_gian_hang = @ma_gian_hang
	end	
	
	EXEC [p_BaselineMasterData_SyncToMySQL] @ma_san_pham, @ma_gian_hang
	
END