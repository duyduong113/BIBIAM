IF EXISTS (SELECT name FROM [dbo].sysobjects 
         WHERE name = 'p_Hierarchy_SyncToMySQL' AND type = 'P')
   DROP PROCEDURE [dbo].p_Hierarchy_SyncToMySQL
GO
 
CREATE PROCEDURE [dbo].p_Hierarchy_SyncToMySQL
	@ma_phan_cap [varchar](50)
AS
BEGIN			    
	if not exists (select * from OPENQUERY(MYSQL_BIBIAM,'select * from Hierarchy') where ma_phan_cap = @ma_phan_cap)
	begin
			INSERT INTO openquery(MYSQL_BIBIAM,'select ma_phan_cap, ten_phan_cap, cap, aliasname, loai_phan_cap, ma_phan_cap_cha, trang_thai, slug, h.order
													from Hierarchy h')
			select ma_phan_cap, ten_phan_cap, cap, aliasname, loai_phan_cap, ma_phan_cap_cha, trang_thai, slug + '-' + CAST(id as varchar(10)), [order]
			from Hierarchy WITH (NOLOCK) where ma_phan_cap = @ma_phan_cap
	end
	else
	begin
		update 
		p2
		set p2.ma_phan_cap = p.ma_phan_cap ,					
		p2.ten_phan_cap = p.ten_phan_cap,					
		p2.cap = p.cap,					
		p2.aliasname = p.aliasname,					
		p2.loai_phan_cap = p.loai_phan_cap,					
		p2.ma_phan_cap_cha = p.ma_phan_cap_cha,					
		p2.trang_thai = p.trang_thai,
		p2.[order] = p.[order],
		p2.slug = p.slug + '-' + CAST(p.id as varchar(10))
		FROM Hierarchy AS p WITH (NOLOCK)
		JOIN OPENQUERY(MYSQL_BIBIAM, 'select * from Hierarchy' ) AS p2
        ON p2.ma_phan_cap = p.ma_phan_cap
		WHERE p.ma_phan_cap = @ma_phan_cap
	end	
END