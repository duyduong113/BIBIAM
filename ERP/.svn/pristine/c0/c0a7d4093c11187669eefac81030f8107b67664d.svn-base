IF EXISTS (SELECT name FROM [dbo].sysobjects 
         WHERE name = 'p_Banner_SyncToMySQL' AND type = 'P')
   DROP PROCEDURE [dbo].[p_Banner_SyncToMySQL]
GO
  

CREATE PROCEDURE [dbo].[p_Banner_SyncToMySQL]
	@ma_banner [varchar](20)
AS
BEGIN
	if not exists (select * from OPENQUERY(MYSQL_BIBIAM,'SELECT ma_banner FROM bibiam.banner') where ma_banner=@ma_banner)			    
	begin
			--Insert Banner
			INSERT INTO openquery(MYSQL_BIBIAM,'SELECT ma_banner,ma_chuyen_muc,image_link,url_link,image,loai,vi_tri,trang_thai FROM bibiam.banner')
			select  ma_banner,ma_chuyen_muc,image_link,url_link,image,loai,vi_tri,trang_thai
			from Banner WITH (NOLOCK) where ma_banner=@ma_banner
	end
	else
	begin
		update 
		p2
		set p2.url_link =p.url_link,
		p2.image_link=p.image_link,
		p2.image =p.image,
		p2.ma_chuyen_muc =p.ma_chuyen_muc,
		p2.loai =p.loai,
		p2.vi_tri =p.vi_tri,
		p2.trang_thai =p.trang_thai
		FROM Banner AS p WITH (NOLOCK)
		JOIN OPENQUERY(MYSQL_BIBIAM, 'SELECT * FROM bibiam.banner' ) AS p2
        ON p2.ma_banner = p.ma_banner
		where p.ma_banner=@ma_banner
	end
END
