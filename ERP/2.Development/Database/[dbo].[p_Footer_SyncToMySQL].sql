USE [MCCDev]
GO
/****** Object:  StoredProcedure [dbo].[p_Footer_SyncToMySQL]    Script Date: 3/2/2017 6:40:11 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
  

ALTER PROCEDURE [dbo].[p_Footer_SyncToMySQL]
 @ma_footer [varchar](20)
AS
BEGIN
 if not exists (select * from OPENQUERY(MYSQL_BIBIAM_DEV,'SELECT ma_footer FROM bibiam_dev1.footer') where ma_footer=@ma_footer)       
 begin
   --Insert Footer
   INSERT INTO openquery(MYSQL_BIBIAM_DEV,'SELECT ma_footer,url_link,image_link,noi_dung,loai,trang_thai,ten_footer FROM bibiam_dev1.footer')
   select  ma_footer,url_link,image_link,noi_dung,loai,trang_thai,ten_footer
   from Footer WITH (NOLOCK) where ma_footer=@ma_footer
 end
 else
 begin
  update 
  p2
  set p2.url_link =p.url_link,
  p2.noi_dung=p.noi_dung,
  p2.image_link=p.image_link,
  p2.loai =p.loai,
  p2.trang_thai =p.trang_thai
  FROM Footer AS p WITH (NOLOCK)
  JOIN OPENQUERY(MYSQL_BIBIAM_DEV, 'SELECT * FROM bibiam_dev1.footer' ) AS p2
        ON p2.ma_footer = p.ma_footer
  where p.ma_footer=@ma_footer
 end
END