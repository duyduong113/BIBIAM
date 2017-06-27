IF EXISTS (SELECT name FROM [dbo].sysobjects 
         WHERE name = 'p_Hierarchy_SyncToMySQL' AND type = 'P')
   DROP PROCEDURE [dbo].p_Hierarchy_SyncToMySQL
GO
 
CREATE PROCEDURE [dbo].p_Hierarchy_SyncToMySQL
	@ma_phan_cap [varchar](50)
AS
BEGIN			    
	if not exists (select * from OPENQUERY(MYSQL_BIBIAM_DEV,'select * from bibiam_dev1.Hierarchy') where ma_phan_cap = @ma_phan_cap)
	begin
			INSERT INTO openquery(MYSQL_BIBIAM_DEV,'select ma_phan_cap,ten_phan_cap,cap,aliasname,loai_phan_cap,ma_phan_cap_cha,trang_thai
													from bibiam_dev1.Hierarchy')
			select ma_phan_cap,ten_phan_cap,cap,aliasname,loai_phan_cap,ma_phan_cap_cha,trang_thai
			from Hierarchy WITH (NOLOCK) where ma_phan_cap = @ma_phan_cap
	end
	else
	begin
	if not exists (select p.id from openquery(MYSQL_BIBIAM_DEV,'select * from bibiam_dev1.Hierarchy') p2 
	RIGHT JOIN (select * from Hierarchy where  ma_phan_cap = @ma_phan_cap) p
				on  p2.ma_phan_cap = p.ma_phan_cap
				where p2.ma_phan_cap = p.ma_phan_cap and
					p2.ten_phan_cap = p.ten_phan_cap and
					p2.cap = p.cap and
					p2.aliasname = p.aliasname and
					p2.loai_phan_cap = p.loai_phan_cap and
					p2.ma_phan_cap_cha = p.ma_phan_cap_cha and
					p2.trang_thai = p.trang_thai)			
		update 
		p2
		set p2.ma_phan_cap = p.ma_phan_cap ,					
		p2.ten_phan_cap = p.ten_phan_cap,					
		p2.cap = p.cap,					
		p2.aliasname = p.aliasname,					
		p2.loai_phan_cap = p.loai_phan_cap,					
		p2.ma_phan_cap_cha = p.ma_phan_cap_cha,					
		p2.trang_thai = p.trang_thai
		FROM Hierarchy AS p WITH (NOLOCK)
		JOIN OPENQUERY(MYSQL_BIBIAM_DEV, 'select * from bibiam_dev1.Hierarchy' ) AS p2
        ON p2.ma_phan_cap = p.ma_phan_cap
		WHERE p.ma_phan_cap = @ma_phan_cap
	end	
END