
IF  EXISTS (SELECT * FROM sys.objects 
WHERE object_id = OBJECT_ID(N'[dbo].[p_Footer_SyncToMySQL_All]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].p_Footer_SyncToMySQL_All
GO
CREATE PROCEDURE [dbo].p_Footer_SyncToMySQL_All
AS
BEGIN
    update 
	p2
	set 
	p2.url_link = p.url_link,
	p2.image_link = p.image_link,
	p2.noi_dung = p.noi_dung,
	p2.loai = p.loai,
	p2.trang_thai = p.trang_thai,
	p2.ten_footer = p.ten_footer,
	p2.ngay_tao = p.ngay_tao,
	p2.nguoi_tao = p.nguoi_tao,
	p2.ngay_cap_nhat = p.ngay_cap_nhat,
	p2.nguoi_cap_nhat = p.nguoi_cap_nhat
	FROM Footer AS p WITH (NOLOCK)
	JOIN OPENQUERY(MYSQL_BIBIAM_DEV, 'SELECT * FROM Footer' ) AS p2
	ON p2.ma_footer = p.ma_footer

	INSERT INTO openquery(MYSQL_BIBIAM_DEV,'SELECT ma_footer, url_link, image_link, noi_dung, loai, trang_thai, ten_footer FROM Footer')
	SELECT p.ma_footer, p.url_link, p.image_link, p.noi_dung, p.loai, p.trang_thai, p.ten_footer
	from Footer AS p WITH (NOLOCK)
	LEFT JOIN OPENQUERY(MYSQL_BIBIAM_DEV, 'SELECT * FROM Footer' ) AS p2
	ON p2.ma_footer = p.ma_footer
	WHERE p2.ma_footer IS NULL
end