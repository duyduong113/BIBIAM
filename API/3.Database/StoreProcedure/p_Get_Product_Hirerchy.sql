

IF EXISTS (SELECT name FROM [dbo].sysobjects 
         WHERE name = 'p_Get_Product_Hirerchy' AND type = 'P')
   DROP PROCEDURE [dbo].p_Get_Product_Hirerchy
GO
  

CREATE PROC [p_Get_Product_Hirerchy]
as
begin
	select a.id,b.ma_san_pham,b.ten_san_pham,a.ma_cay_phan_cap_1,a.ma_cay_phan_cap_2,a.ma_cay_phan_cap_3,a.ma_cay_phan_cap_4,a.ma_cay_phan_cap_5,a.ma_cay_phan_cap_6,a.ma_cay_phan_cap_7,a.ma_cay_phan_cap_8,a.ma_cay_phan_cap_9,a.ma_cay_phan_cap_10,a.ngay_tao,a.nguoi_tao,a.nguoi_cap_nhat,a.ngay_cap_nhat,a.trang_thai from Product_Hierarchy a right join Product_Info b on a.ma_san_pham = b.ma_san_pham order by b.ten_san_pham desc
end