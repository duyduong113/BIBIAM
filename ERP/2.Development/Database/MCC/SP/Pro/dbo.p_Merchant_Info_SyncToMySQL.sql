﻿
IF EXISTS (SELECT name FROM [dbo].sysobjects 
         WHERE name = 'p_Merchant_Info_SyncToMySQL' AND type = 'P')
   DROP PROCEDURE [dbo].p_Merchant_Info_SyncToMySQL
GO
 
CREATE PROCEDURE [dbo].p_Merchant_Info_SyncToMySQL
	@ma_gian_hang [varchar](50)
AS
BEGIN			    
	if not exists (select * from OPENQUERY(MYSQL_BIBIAM_DEV,'select ma_gian_hang from bibiam_dev1.Merchant_Info') where ma_gian_hang = @ma_gian_hang)
	begin
			INSERT INTO openquery(MYSQL_BIBIAM_DEV,'select ma_gian_hang,ten_gian_hang,ten_viet_tat,ten_tieng_anh,website,dien_thoai,email,fax,dia_chi_tru_so,ma_tinh_tp,ten_tinh_tp,ma_quan_huyen,ten_quan_huyen,ngay_hoat_dong,ly_do_giay_to_phap_ly,tai_khoan_thanh_toan,dao_tao_quan_ly,trang_thai_xac_thuc,trang_thai_duyet,trang_thai_xuat_ban,nguoi_duyet,ngay_duyet,mo_ta,ngay_tao,nguoi_tao,ngay_cap_nhat,nguoi_cap_nhat
										from bibiam_dev1.Merchant_Info')
			select ma_gian_hang,ten_gian_hang,ten_viet_tat,ten_tieng_anh,website,dien_thoai,email,fax,dia_chi_tru_so,ma_tinh_tp,ten_tinh_tp,ma_quan_huyen,ten_quan_huyen,ngay_hoat_dong,ly_do_giay_to_phap_ly,tai_khoan_thanh_toan,dao_tao_quan_ly,trang_thai_xac_thuc,trang_thai_duyet,trang_thai_xuat_ban,nguoi_duyet,ngay_duyet,mo_ta,ngay_tao,nguoi_tao,ngay_cap_nhat,nguoi_cap_nhat
			from Merchant_Info WITH (NOLOCK) where ma_gian_hang = @ma_gian_hang
	end
	else
	begin
	if not exists (select p.id from openquery(MYSQL_BIBIAM_DEV,'select * from bibiam_dev1.Merchant_Info') p2 
	RIGHT JOIN (select * from Merchant_Info where  ma_gian_hang = @ma_gian_hang) p
						on  p2.ma_gian_hang = p.ma_gian_hang
					where p2.ma_gian_hang = p.ma_gian_hang and
							p2.ten_gian_hang = p.ten_gian_hang and
							p2.ten_viet_tat = p.ten_viet_tat and
							p2.ten_tieng_anh = p.ten_tieng_anh and
							p2.website = p.website and
							p2.dien_thoai = p.dien_thoai and
							p2.email = p.email and
							p2.fax = p.fax and
							p2.dia_chi_tru_so = p.dia_chi_tru_so and
							p2.ma_tinh_tp = p.ma_tinh_tp and
							p2.ten_tinh_tp = p.ten_tinh_tp and
							p2.ma_quan_huyen = p.ma_quan_huyen and
							p2.ten_quan_huyen = p.ten_quan_huyen and
							DATEDIFF(ss,p2.ngay_hoat_dong,p.ngay_hoat_dong) = 0 and
							p2.trang_thai_xac_thuc = p.trang_thai_xac_thuc and
							p2.trang_thai_duyet = p.trang_thai_duyet and
							p2.trang_thai_xuat_ban = p.trang_thai_xuat_ban and
							p2.tai_khoan_thanh_toan = p.tai_khoan_thanh_toan and						
							((cast (p2.mo_ta as nvarchar(MAX)) = p.mo_ta) or (p.mo_ta is null and p2.mo_ta is null)))				
		update 
		p2
		set p2.ma_gian_hang = p.ma_gian_hang ,
							p2.ten_gian_hang = p.ten_gian_hang ,
							p2.ten_viet_tat = p.ten_viet_tat ,
							p2.ten_tieng_anh = p.ten_tieng_anh ,
							p2.website = p.website ,
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
		JOIN OPENQUERY(MYSQL_BIBIAM_DEV, 'select * from bibiam_dev1.merchant_info' ) AS p2
        ON p2.ma_gian_hang = p.ma_gian_hang
		WHERE p.ma_gian_hang = @ma_gian_hang
	end	
END