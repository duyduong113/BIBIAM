ALTER PROCEDURE [dbo].[p_ResumeSeeker_24H_GetByResumeID]		
	@ResumeID  varchar(30) = 886252 -- 0
	--@SeekerID
AS
DECLARE @Sql nvarchar(max)
DECLARE @w nvarchar(max)
Set @w=' where pk_tin_tim_viec='+@ResumeID+''
SELECT @Sql =
		' SELECT 
		    b.*
		    ,ISNULL(u.OriginalUserID,'''') AS tai_khoan_duyet_tin
		    ,'''' AS tin_tim_viec 
			,'''' AS ten_nganh_nghe
			,'''' AS ten_loai_ho_so
			,ISNULL(map1.SourceName,'''') AS ho_so_truoc_cham_soc
			,ISNULL(map2.SourceName,'''') AS ho_so_sau_cham_soc
			,'''' AS phan_loai_NTV
			,'''' AS kinh_nghiem_lam_viec
			,'''' AS muc_luong
			,'''' AS chuc_vu_hien_tai
			,'''' AS chuc_vu
			,'''' AS trinh_do
			,''VL24h'' AS ChannelID
			,ISNULL(seeker.UserLogIn,'''') AS UserLogIn
			,ISNULL(seeker.Status,'''') AS Status
			,ISNULL(seeker.ProvinceID,'''') AS ProvinceID
		   FROM
		  ( SELECT  
				 ISNULL(a.pk_tin_tim_viec , 0) AS pk_tin_tim_viec
				,ISNULL(a.fk_nganh_nghe_cap1 , 0) AS fk_nganh_nghe_cap1
				,ISNULL(a.fk_nguoi_dang_tin , 0) AS fk_nguoi_dang_tin
				,ISNULL(a.c_ho_ten, '''') AS c_ho_ten
				,ISNULL(a.c_trinh_do, 0) AS c_trinh_do
				,ISNULL(a.c_trinh_do_ngoai_ngu, '''') AS c_trinh_do_ngoai_ngu
				,ISNULL(a.c_trinh_do_tin_hoc, '''') AS c_trinh_do_tin_hoc
				,ISNULL(a.c_kinh_nghiem, '''') AS c_kinh_nghiem
				,ISNULL(a.c_bang_cap_khac, '''') AS c_bang_cap_khac
				,ISNULL(a.c_gioi_tinh, 0) AS c_gioi_tinh
				,CASE WHEN ISDATE(a.c_ngay_sinh) = 1 THEN a.c_ngay_sinh ELSE ''1900-01-01'' END AS c_ngay_sinh
				,ISNULL(a.c_tinh_trang_hon_nhan, 0) AS c_tinh_trang_hon_nhan
				,ISNULL(a.c_anh_dai_dien, '''') AS  c_anh_dai_dien
				,ISNULL(a.c_tieu_de, '''') AS  c_tieu_de
				,ISNULL(a.c_so_nam_kinh_nghiem, 0) AS c_so_nam_kinh_nghiem
				,ISNULL(a.c_muc_tieu_nghe_nghiep, '''') AS c_muc_tieu_nghe_nghiep
				,ISNULL(a.c_thoi_gian_lam_viec, 0)  AS c_thoi_gian_lam_viec
				,ISNULL(a.c_ten_tinh, '''') AS c_ten_tinh
				,ISNULL(a.c_muc_luong, '''') AS c_muc_luong
				,ISNULL(a.c_nguoi_lien_he, '''') AS c_nguoi_lien_he
				,ISNULL(a.c_dia_chi_lien_he, '''') AS c_dia_chi_lien_he
				,ISNULL(a.c_dien_thoai_lien_he, '''') AS c_dien_thoai_lien_he
				,ISNULL(a.c_email_lien_he, '''') AS c_email_lien_he
				,ISNULL(a.c_website, '''') AS c_website
				,CASE WHEN ISDATE(a.c_ngay_tao) = 1 THEN a.c_ngay_tao ELSE ''1900-01-01'' END AS c_ngay_tao
				,ISNULL(a.c_trang_thai , 0) AS c_trang_thai
				,ISNULL(a.c_ly_do_khong_duyet, '''') AS c_ly_do_khong_duyet
				,ISNULL(a.c_nganh_hoc, '''') AS c_nganh_hoc
				,ISNULL(a.is_vip,0) AS is_vip
				,ISNULL(a.c_cach_lien_he,0) AS c_cach_lien_he
				,ISNULL(a.c_chuc_vu, 0) AS c_chuc_vu
				,ISNULL(a.c_chuc_vu_hien_tai, 0) AS c_chuc_vu_hien_tai
				,ISNULL(a.c_thong_bao_lua_dao, 0) AS c_thong_bao_lua_dao
				,ISNULL(a.fk_truong_hoc, 0) AS fk_truong_hoc
				,ISNULL(a.c_loai_tot_nghiep, 0) AS c_loai_tot_nghiep
				,ISNULL(a.c_nam_tot_nghiep,0) AS c_nam_tot_nghiep
				,ISNULL(a.c_ma_ngoai_ngu, '''') AS c_ma_ngoai_ngu
				,ISNULL(a.c_cap_do_ngoai_ngu, '''') AS c_cap_do_ngoai_ngu
				,ISNULL(a.c_ky_nang, '''') AS c_ky_nang
				,ISNULL(a.c_nguon_tham_khao, '''') AS c_nguon_tham_khao
				,ISNULL(a.c_luot_xem, '''') AS c_luot_xem
				,CASE WHEN ISDATE(a.c_tu_ngay) = 1 THEN a.c_tu_ngay ELSE ''1900-01-01'' END AS c_tu_ngay
				,CASE WHEN ISDATE(a.c_den_ngay) = 1 THEN a.c_den_ngay ELSE ''1900-01-01'' END AS c_den_ngay
			    ,CASE WHEN ISDATE(a.c_ngay_sua) = 1 THEN a.c_ngay_sua ELSE ''1900-01-01'' END AS c_ngay_sua
			    ,CASE WHEN ISDATE(a.c_ngay_duyet) = 1 THEN a.c_ngay_duyet ELSE ''1900-01-01'' END AS c_ngay_duyet
				,CASE WHEN ISDATE(a.c_ngay_lam_moi) = 1 THEN a.c_ngay_lam_moi ELSE ''1900-01-01'' END AS c_ngay_lam_moi
				,ISNULL(a.fk_tai_khoan_duyet_tin, 0) AS fk_tai_khoan_duyet_tin
				,ISNULL(a.fk_tintimviec_goc ,0) AS fk_tintimviec_goc
				,ISNULL(a.c_so_lan_gia_han, 0) AS c_so_lan_gia_han
				,ISNULL(a.c_so_lan_lam_moi, 0) AS c_so_lan_lam_moi
				,ISNULL(a.c_so_lan_copy, 0) AS c_so_lan_copy
				,ISNULL(a.c_tieu_de_khong_dau , '''') AS c_tieu_de_khong_dau
				,ISNULL(a.c_trang_thai_truoc_khi_cam, 0) AS c_trang_thai_truoc_khi_cam
				,ISNULL(a.c_trang_thai_sua , 0) AS c_trang_thai_sua
				,ISNULL(a.c_ghi_chu, '''') AS c_ghi_chu
				,ISNULL(a.c_loai_ho_so , 0) AS c_loai_ho_so
				,ISNULL(a.c_ho_so_truoc_cham_soc , 0) AS c_ho_so_truoc_cham_soc
				,ISNULL(a.c_ho_so_sau_cham_soc , 0) AS c_ho_so_sau_cham_soc
				,ISNULL(a.c_loai_trinh_do_ho_so , 0) AS c_loai_trinh_do_ho_so
				,ISNULL(a.c_loai_ho_tro, 0) AS c_loai_ho_tro
				,ISNULL(a.c_from_mobile, 0) AS c_from_mobile
				,ISNULL(a.c_canonical, '''') AS c_canonical
				,ISNULL(a.c_facebook_url, '''') AS c_facebook_url
				,ISNULL(a.c_file_dinh_kem, '''') AS c_file_dinh_kem
				,ISNULL(a.is_search_allowed, '''') AS is_search_allowed
						
		FROM	OPENQUERY(VL24HTEST,'' SELECT	
									pk_tin_tim_viec 
									,fk_nganh_nghe_cap1 
									,fk_nguoi_dang_tin 
									,CONVERT(CAST(CONVERT(c_ho_ten USING latin1) AS BINARY) USING utf8) c_ho_ten
									,c_trinh_do
									,c_trinh_do_ngoai_ngu
									,CONVERT(CAST(CONVERT(c_trinh_do_tin_hoc USING latin1) AS BINARY) USING utf8) c_trinh_do_tin_hoc
									,CONVERT(CAST(CONVERT(c_kinh_nghiem USING latin1) AS BINARY) USING utf8) c_kinh_nghiem
									,CONVERT(CAST(CONVERT(c_bang_cap_khac USING latin1) AS BINARY) USING utf8) c_bang_cap_khac
									,c_gioi_tinh
									,CAST(c_ngay_sinh AS CHAR) c_ngay_sinh
									,c_tinh_trang_hon_nhan
									,c_anh_dai_dien
								    ,CONVERT(CAST(CONVERT(c_tieu_de USING latin1) AS BINARY) USING utf8) c_tieu_de									
									,c_so_nam_kinh_nghiem
								    ,CONVERT(CAST(CONVERT(c_muc_tieu_nghe_nghiep USING latin1) AS BINARY) USING utf8) c_muc_tieu_nghe_nghiep
									,c_thoi_gian_lam_viec
									,CONVERT(CAST(CONVERT(c_ten_tinh USING latin1) AS BINARY) USING utf8) c_ten_tinh
									,c_muc_luong
									,CONVERT(CAST(CONVERT(c_nguoi_lien_he USING latin1) AS BINARY) USING utf8) c_nguoi_lien_he
									,CONVERT(CAST(CONVERT(c_dia_chi_lien_he USING latin1) AS BINARY) USING utf8) c_dia_chi_lien_he
									,c_dien_thoai_lien_he
									,c_email_lien_he
									,c_website
									,CAST(c_ngay_tao AS CHAR) c_ngay_tao
									,c_trang_thai 
									,CONVERT(CAST(CONVERT(c_ly_do_khong_duyet USING latin1) AS BINARY) USING utf8) c_ly_do_khong_duyet
									,CONVERT(CAST(CONVERT(c_nganh_hoc USING latin1) AS BINARY) USING utf8) c_nganh_hoc
									,is_vip
									,c_cach_lien_he
									,c_chuc_vu
									,c_chuc_vu_hien_tai
									,c_thong_bao_lua_dao
									,fk_truong_hoc
									,c_loai_tot_nghiep
									,c_nam_tot_nghiep
									,c_ma_ngoai_ngu
									,c_cap_do_ngoai_ngu
									,CONVERT(CAST(CONVERT(c_ky_nang USING latin1) AS BINARY) USING utf8) c_ky_nang
									,c_nguon_tham_khao
									,c_luot_xem
									,CAST(c_tu_ngay AS CHAR) c_tu_ngay
									,CAST(c_den_ngay AS CHAR) c_den_ngay
								    ,CAST(c_ngay_sua AS CHAR) c_ngay_sua
									,CAST(c_ngay_duyet AS CHAR) c_ngay_duyet
									,CAST(c_ngay_lam_moi AS CHAR) c_ngay_lam_moi
									,fk_tai_khoan_duyet_tin
									,fk_tintimviec_goc 
									,c_so_lan_gia_han
									,c_so_lan_lam_moi
									,c_so_lan_copy
									,c_tieu_de_khong_dau 
									,c_trang_thai_truoc_khi_cam
									,c_trang_thai_sua 
									,CONVERT(CAST(CONVERT(c_ghi_chu USING latin1) AS BINARY) USING utf8) c_ghi_chu
									,c_loai_ho_so 
									,c_ho_so_truoc_cham_soc 
									,c_ho_so_sau_cham_soc 
									,c_loai_trinh_do_ho_so 
									,c_loai_ho_tro
									,c_from_mobile
									,c_canonical
									,c_facebook_url
									,c_file_dinh_kem
									,is_search_allowed
							FROM	t_tintimviec where pk_tin_tim_viec= '''''+ @ResumeID +'''''

						 '' ) a  ) b 
				LEFT JOIN  Mapping_VL24h map ON map.SourceID=b.fk_nganh_nghe_cap1 and  map.DataType=''CareerList''
				LEFT JOIN  Mapping_VL24h map1 ON map1.SourceID=b.c_ho_so_truoc_cham_soc and map1.DataType=''TakecareList''
				LEFT JOIN  Mapping_VL24h map2 ON map2.SourceID=b.c_ho_so_sau_cham_soc and map2.DataType=''TakecareList'' 
			    LEFT JOIN  Auth_User u ON u.OriginalID=b.fk_tai_khoan_duyet_tin
			    LEFT JOIN Sync_VL24H_Seeker seeker ON seeker.SeekerID=b.fk_nguoi_dang_tin
				' 
EXEC (@Sql)