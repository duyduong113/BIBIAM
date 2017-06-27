IF EXISTS (SELECT name FROM [dbo].sysobjects 
         WHERE name = 'p_Brand_SyncToMySQL' AND type = 'P')
   DROP PROCEDURE [dbo].p_Brand_SyncToMySQL
GO
CREATE PROCEDURE [dbo].[p_Brand_SyncToMySQL]
	@ma_thuong_hieu [varchar](20)
AS
BEGIN
	if not exists (select * from OPENQUERY(MYSQL_BIBIAM,'SELECT ma_thuong_hieu FROM bibiam.brandmanagement') where ma_thuong_hieu=@ma_thuong_hieu)			    
	begin
			--Insert Banner
			INSERT INTO openquery(MYSQL_BIBIAM,'SELECT id,ma_thuong_hieu,ten_thuong_hieu,logo,trang_thai,mo_ta,nguoi_tao,ngay_tao,nguoi_cap_nhat,ngay_cap_nhat  FROM bibiam.brandmanagement')
			select id,ma_thuong_hieu,ten_thuong_hieu,logo,trang_thai,mo_ta,nguoi_tao,ngay_tao,nguoi_cap_nhat,ngay_cap_nhat
			from BrandManagement WITH (NOLOCK) where ma_thuong_hieu=@ma_thuong_hieu
	end
	else
	begin
		update 
		p2
		set p2.ten_thuong_hieu =p.ten_thuong_hieu,
		p2.logo =p.logo,
		p2.trang_thai =p.trang_thai,
		p2.mo_ta=p.mo_ta,
		p2.nguoi_cap_nhat =p.nguoi_cap_nhat,
		p2.ngay_cap_nhat =p.ngay_cap_nhat
		FROM BrandManagement AS p WITH (NOLOCK)
		JOIN OPENQUERY(MYSQL_BIBIAM, 'SELECT * FROM bibiam.brandmanagement' ) AS p2
        ON p2.ma_thuong_hieu = p.ma_thuong_hieu
		where p.ma_thuong_hieu=@ma_thuong_hieu
	end
END
