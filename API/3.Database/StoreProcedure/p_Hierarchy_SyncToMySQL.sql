CREATE PROCEDURE [dbo].p_Hierarchy_SyncToMySQL	
	@ma_phan_cap [varchar](255)
AS
BEGIN
	if not exists (select * from openquery(MYSQL,'select * from bibiam_dev.hierarchy') 
					where ma_phan_cap = @ma_phan_cap)
	begin	
		INSERT INTO openquery(MYSQL,'select ma_phan_cap,ten_phan_cap,cap,loai_phan_cap,ma_phan_cap_cha,trang_thai,ngay_tao,nguoi_tao,ngay_cap_nhat,
		nguoi_cap_nhat from bibiam_dev.hierarchy')
		select ma_phan_cap,ten_phan_cap,cap,loai_phan_cap,ma_phan_cap_cha,trang_thai,ngay_tao,nguoi_tao,ngay_cap_nhat,
		nguoi_cap_nhat
		from hierarchy WITH (NOLOCK) where ma_phan_cap = @ma_phan_cap
	end
	else
	begin
		if not exists (select p.id from openquery(MYSQL,'select * from bibiam_dev.hierarchy') p2 JOIN Hierarchy p
						on  p2.ma_phan_cap = p.ma_phan_cap
					where p2.ma_phan_cap = @ma_phan_cap and							
							p2.ten_phan_cap=p.ten_phan_cap and
							p2.cap=p.cap and
							--p2.aliasname=p.aliasname and
							p2.loai_phan_cap=p.loai_phan_cap and
							p2.ma_phan_cap_cha=p.ma_phan_cap_cha and
							p2.trang_thai=p.trang_thai and
							DATEDIFF(ss,p2.ngay_tao,p.ngay_tao) = 0 and
							p2.nguoi_tao=p.nguoi_tao and
							p2.nguoi_cap_nhat=p.nguoi_cap_nhat and
							DATEDIFF(ss,p2.ngay_cap_nhat,p.ngay_cap_nhat) = 0)
			update p2
			set p2.ma_phan_cap=p.ma_phan_cap,
			p2.ten_phan_cap=p.ten_phan_cap,
			p2.cap=p.cap,
			--p2.aliasname=p.aliasname,
			p2.loai_phan_cap=p.loai_phan_cap,
			p2.ma_phan_cap_cha=p.ma_phan_cap_cha,
			p2.trang_thai=p.trang_thai,
			p2.ngay_tao=p.ngay_tao,
			p2.nguoi_tao=p.nguoi_tao,
			p2.ngay_cap_nhat=p.ngay_cap_nhat,
			p2.nguoi_cap_nhat=p.nguoi_cap_nhat
			FROM Hierarchy AS p WITH (NOLOCK)
			JOIN OPENQUERY(MYSQL, 'select * from bibiam_dev.hierarchy' ) AS p2
			ON p.ma_phan_cap = p2.ma_phan_cap
			WHERE p.ma_phan_cap = @ma_phan_cap
	end
END