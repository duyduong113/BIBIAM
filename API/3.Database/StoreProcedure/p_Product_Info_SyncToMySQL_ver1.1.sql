CREATE PROCEDURE [dbo].p_Product_Info_SyncToMySQL
	@id int,
	@ma_san_pham [varchar](255)
AS
BEGIN
	
		
	if not exists (select * from openquery(MYSQL,'select * from bibiam_dev.product_info') where ma_san_pham=@ma_san_pham)
	begin
			INSERT INTO openquery(MYSQL,'select ma_san_pham,ma_loai_san_pham,ten_san_pham,mo_ta,tu_khoa,tag,slug,trong_so,nguoi_duyet,ngay_duyet,
			trang_thai_duyet,nguoi_xuat_ban,ngay_xuat_ban,trang_thai_xuat_ban,trang_thai,ngay_tao,nguoi_tao,ngay_cap_nhat,
			nguoi_cap_nhat,url from bibiam_dev.product_info')
			select ma_san_pham,ma_loai_san_pham,ten_san_pham,mo_ta,tu_khoa,tag,slug,trong_so,nguoi_duyet,ngay_duyet,
			trang_thai_duyet,nguoi_xuat_ban,ngay_xuat_ban,trang_thai_xuat_ban,trang_thai,ngay_tao,nguoi_tao,ngay_cap_nhat,
			nguoi_cap_nhat,url
			from Product_Info WITH (NOLOCK) where id=@id
	end
	else
	begin
		update --openquery(MYSQL, 'SELECT * FROM bibiam_dev1.product_info')
		p2
		set p2.ma_loai_san_pham=p.ma_loai_san_pham,p2.ten_san_pham=p.ten_san_pham,
		p2.mo_ta=p.mo_ta,
		p2.tu_khoa=p.tu_khoa,
		p2.tag=p.tag,p2.slug=p.slug,p2.trong_so=p.trong_so,p2.nguoi_duyet=p.nguoi_duyet,p2.ngay_duyet=p.ngay_duyet,
		p2.trang_thai_duyet=p.trang_thai_duyet,p2.nguoi_xuat_ban=p.nguoi_xuat_ban,p2.ngay_xuat_ban=p.ngay_xuat_ban,
		p2.trang_thai_xuat_ban=p.trang_thai_xuat_ban,p2.trang_thai=p.trang_thai,p2.ngay_cap_nhat=p.ngay_cap_nhat,
		p2.nguoi_cap_nhat=p.nguoi_cap_nhat,p2.url=p.url
		FROM Product_Info AS p WITH (NOLOCK)
		JOIN OPENQUERY(MYSQL, 'select * from bibiam_dev.product_info' ) AS p2
        ON p.ma_san_pham = p2.ma_san_pham
		WHERE p.id=@id
	end
	
END