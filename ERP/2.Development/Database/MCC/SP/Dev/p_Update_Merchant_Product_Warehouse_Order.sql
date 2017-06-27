
ALTER PROCEDURE [dbo].[p_Update_Merchant_Product_Warehouse_Order]
@ma_don_hang nvarchar(20)
AS
BEGIN
	select ma_gian_hang,ma_san_pham,sum(so_luong) as so_luong
	into #tmpMerchant_OrderDetail
	from Merchant_OrderDetail
	where ma_don_hang=@ma_don_hang
	group by ma_gian_hang,ma_san_pham

	--Cập nhật kho ở BE
	update w
	set w.book_available= w.book_available - o.so_luong
	from Merchant_Product_Warehouse w
	join #tmpMerchant_OrderDetail o
	on w.ma_gian_hang=o.ma_gian_hang
	and w.ma_san_pham=o.ma_san_pham
end