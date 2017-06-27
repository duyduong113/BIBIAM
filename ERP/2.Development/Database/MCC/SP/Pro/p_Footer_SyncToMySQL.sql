IF EXISTS (SELECT name FROM [dbo].sysobjects 
         WHERE name = 'p_Footer_SyncToMySQL' AND type = 'P')
   DROP PROCEDURE [dbo].[p_Footer_SyncToMySQL]
GO
CREATE PROCEDURE [dbo].[p_Footer_SyncToMySQL]
 @ma_footer [varchar](20)
AS
BEGIN
 if not exists (select * from OPENQUERY(MYSQL_BIBIAM,'SELECT ma_footer FROM bibiam.footer') where ma_footer= @ma_footer)       
 begin
   --Insert Footer
   INSERT INTO openquery(MYSQL_BIBIAM,'SELECT id,ma_footer,url_link,image_link,noi_dung,loai,trang_thai,ten_footer FROM bibiam.footer')
								          SELECT  id,ma_footer,url_link,image_link,noi_dung,loai,trang_thai,ten_footer
												  from Footer WITH (NOLOCK) where ma_footer= @ma_footer
 end
 else
 begin
  update 
  p2
  set 
  p2.url_link =p.url_link,
  p2.image_link=p.image_link,
  p2.noi_dung=p.noi_dung,
  p2.loai =p.loai,
  p2.trang_thai =p.trang_thai,
  p2.ten_footer=p.ten_footer,
  p2.ngay_tao=p.ngay_tao,
  p2.nguoi_tao=p.nguoi_tao,
  p2.ngay_cap_nhat=p.ngay_cap_nhat,
  p2.nguoi_cap_nhat=p.nguoi_cap_nhat
  FROM Footer AS p WITH (NOLOCK)
  JOIN OPENQUERY(MYSQL_BIBIAM, 'SELECT * FROM bibiam.footer' ) AS p2
        ON p2.ma_footer = p.ma_footer
  where p.ma_footer=@ma_footer
 end
END

