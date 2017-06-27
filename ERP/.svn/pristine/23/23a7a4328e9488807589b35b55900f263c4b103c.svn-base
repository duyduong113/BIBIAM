
IF EXISTS (SELECT name FROM [dbo].sysobjects 
         WHERE name = 'p_Hierarchy_SyncToMySQL_All' AND type = 'P')
   DROP PROCEDURE [dbo].p_Hierarchy_SyncToMySQL_All
GO
 
CREATE pROCEDURE [dbo].p_Hierarchy_SyncToMySQL_All
AS
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
	JOIN OPENQUERY(MYSQL_BIBIAM_DEV, 'select * from Hierarchy' ) AS p2
    ON p2.ma_phan_cap = p.ma_phan_cap

	INSERT INTO openquery(MYSQL_BIBIAM_DEV,'select ma_phan_cap,ten_phan_cap,cap,aliasname,loai_phan_cap,ma_phan_cap_cha,trang_thai, slug, h.order
													from Hierarchy h')
	select p.ma_phan_cap, p.ten_phan_cap, p.cap, p.aliasname, p.loai_phan_cap, p.ma_phan_cap_cha, p.trang_thai, p.slug + '-' + CAST(p.id as varchar(10)), p.[order]
	from Hierarchy AS p WITH (NOLOCK) 
	LEFT JOIN OPENQUERY(MYSQL_BIBIAM_DEV, 'select * from Hierarchy' ) AS p2
    ON p2.ma_phan_cap = p.ma_phan_cap
end


