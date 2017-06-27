  

--p_Product_Hierarchy_SyncToMySQL 36,'GIA0000000012'

create PROCEDURE [dbo].p_Product_Hierarchy_SyncToMySQL
	@id int,
	@ma_san_pham [varchar](255)
AS
BEGIN
	
		
	if not exists (select * from openquery(MYSQL,'select * from bibiam_dev.Product_Hierarchy') where ma_san_pham=@ma_san_pham)
	begin
			INSERT INTO openquery(MYSQL,'select ma_san_pham,ma_cay_phan_cap_1,ma_cay_phan_cap_2,ma_cay_phan_cap_3,ma_cay_phan_cap_4,ma_cay_phan_cap_5,ma_cay_phan_cap_6,ma_cay_phan_cap_7,ma_cay_phan_cap_8,
			ma_cay_phan_cap_9,ma_cay_phan_cap_10,trang_thai,ngay_tao,nguoi_tao,ngay_cap_nhat,nguoi_cap_nhat from bibiam_dev.Product_Hierarchy')
			select ma_san_pham,ma_cay_phan_cap_1,ma_cay_phan_cap_2,ma_cay_phan_cap_3,ma_cay_phan_cap_4,ma_cay_phan_cap_5,ma_cay_phan_cap_6,ma_cay_phan_cap_7,ma_cay_phan_cap_8,
			ma_cay_phan_cap_9,ma_cay_phan_cap_10,trang_thai,ngay_tao,nguoi_tao,ngay_cap_nhat,nguoi_cap_nhat
			from Product_Hierarchy WITH (NOLOCK) where id=@id
	end
	else
	begin
		update p2
		set p2.ma_cay_phan_cap_1=p.ma_cay_phan_cap_1,p2.ma_cay_phan_cap_2=p.ma_cay_phan_cap_2,p2.ma_cay_phan_cap_3=p.ma_cay_phan_cap_3,p2.ma_cay_phan_cap_4=p.ma_cay_phan_cap_4,
		p2.ma_cay_phan_cap_5=p.ma_cay_phan_cap_5,p2.ma_cay_phan_cap_6=p.ma_cay_phan_cap_6,p2.ma_cay_phan_cap_7=p.ma_cay_phan_cap_7,p2.ma_cay_phan_cap_8=p.ma_cay_phan_cap_8,
		p2.ma_cay_phan_cap_9=p.ma_cay_phan_cap_9,p2.ma_cay_phan_cap_10=p.ma_cay_phan_cap_10,p2.trang_thai=p.trang_thai,p2.ngay_tao=p.ngay_tao,
		p2.nguoi_tao=p.nguoi_tao,p2.ngay_cap_nhat=p.ngay_cap_nhat,p2.nguoi_cap_nhat=p.nguoi_cap_nhat
		FROM Product_Hierarchy AS p WITH (NOLOCK)
		JOIN OPENQUERY(MYSQL, 'select * from bibiam_dev.Product_Hierarchy' ) AS p2
        ON p.ma_san_pham = p2.ma_san_pham
		WHERE p.id=@id
	end
	
END