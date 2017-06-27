
IF EXISTS (SELECT name FROM [dbo].sysobjects 
         WHERE name = 'p_Merchant_Info_SyncToMySQL_All' AND type = 'P')
   DROP PROCEDURE [dbo].p_Merchant_Info_SyncToMySQL_All
GO
 
CREATE PROCEDURE [dbo].p_Merchant_Info_SyncToMySQL_All
AS
begin
	update p2 set 
	p2.ma_gian_hang = p.ma_gian_hang ,
	p2.ten_gian_hang = p.ten_gian_hang ,
	p2.ten_viet_tat = p.ten_viet_tat ,
	p2.ten_tieng_anh = p.ten_tieng_anh ,
	p2.website = p.website ,
	p2.slug = p.slug + '-' + cast(p.id as varchar(10)),
	p2.dien_thoai = p.dien_thoai ,
	p2.email = p.email ,
	p2.fax = p.fax ,
	p2.dia_chi_tru_so = p.dia_chi_tru_so ,
	p2.ma_tinh_tp = p.ma_tinh_tp ,
	p2.ten_tinh_tp = p.ten_tinh_tp ,
	p2.ma_quan_huyen = p.ma_quan_huyen ,
	p2.ten_quan_huyen = p.ten_quan_huyen ,
	p2.ngay_hoat_dong = p.ngay_hoat_dong ,
	p2.tai_khoan_thanh_toan = p.tai_khoan_thanh_toan ,
	p2.trang_thai_xac_thuc = p.trang_thai_xac_thuc ,
	p2.trang_thai_duyet = p.trang_thai_duyet ,
	p2.trang_thai_xuat_ban = p.trang_thai_xuat_ban ,
	p2.mo_ta = cast (p.mo_ta as ntext)
	FROM Merchant_Info AS p WITH (NOLOCK)
	JOIN OPENQUERY(MYSQL_BIBIAM_DEV, 'select * from merchant_info' ) AS p2
    ON p2.ma_gian_hang = p.ma_gian_hang

	INSERT INTO openquery(MYSQL_BIBIAM_DEV,'select ma_gian_hang, ten_gian_hang, ten_viet_tat, ten_tieng_anh, website, slug, dien_thoai, email, fax, dia_chi_tru_so, ma_tinh_tp, ten_tinh_tp, ma_quan_huyen, ten_quan_huyen, ngay_hoat_dong, ly_do_giay_to_phap_ly, tai_khoan_thanh_toan, dao_tao_quan_ly, trang_thai_xac_thuc, trang_thai_duyet, trang_thai_xuat_ban, nguoi_duyet, ngay_duyet, mo_ta, ngay_tao, nguoi_tao, ngay_cap_nhat, nguoi_cap_nhat
										from Merchant_Info')
	select p.ma_gian_hang, ISNULL(p.ten_gian_hang,'') as ten_gian_hang, p.ten_viet_tat, p.ten_tieng_anh, ISNULL(p.website,'') as website, p.slug + '-' + cast(p.id as varchar(10)) as slug, p.dien_thoai, p.email, p.fax, p.dia_chi_tru_so, p.ma_tinh_tp, p.ten_tinh_tp, p.ma_quan_huyen, p.ten_quan_huyen, p.ngay_hoat_dong, p.ly_do_giay_to_phap_ly, p.tai_khoan_thanh_toan, p.dao_tao_quan_ly, p.trang_thai_xac_thuc, p.trang_thai_duyet, p.trang_thai_xuat_ban, p.nguoi_duyet, p.ngay_duyet, p.mo_ta, p.ngay_tao, p.nguoi_tao, p.ngay_cap_nhat, p.nguoi_cap_nhat
	from Merchant_Info as p WITH (NOLOCK)
	LEFT JOIN OPENQUERY(MYSQL_BIBIAM_DEV, 'select * from merchant_info' ) AS p2
    ON p2.ma_gian_hang = p.ma_gian_hang
	WHERE p2.ma_gian_hang is null
end

