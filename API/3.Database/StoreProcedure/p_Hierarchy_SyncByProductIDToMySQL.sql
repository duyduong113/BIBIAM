CREATE PROCEDURE [dbo].p_Hierarchy_SyncByProductIDToMySQL	
	@ma_san_pham [varchar](255)
AS
BEGIN
	declare @ma_phan_cap [varchar](255)
	select @ma_phan_cap = ma_cay_phan_cap_1 from Product_Hierarchy where ma_san_pham = @ma_san_pham
	if @ma_phan_cap is null
		return
	exec p_Hierarchy_SyncToMySQL @ma_phan_cap;
	select @ma_phan_cap = ma_cay_phan_cap_2 from Product_Hierarchy where ma_san_pham = @ma_san_pham
	if @ma_phan_cap is null
		return
	exec p_Hierarchy_SyncToMySQL @ma_phan_cap;
	select @ma_phan_cap = ma_cay_phan_cap_3 from Product_Hierarchy where ma_san_pham = @ma_san_pham
	if @ma_phan_cap is null
		return
	exec p_Hierarchy_SyncToMySQL @ma_phan_cap;
	select @ma_phan_cap = ma_cay_phan_cap_4 from Product_Hierarchy where ma_san_pham = @ma_san_pham
	if @ma_phan_cap is null
		return
	exec p_Hierarchy_SyncToMySQL @ma_phan_cap;
	select @ma_phan_cap = ma_cay_phan_cap_5 from Product_Hierarchy where ma_san_pham = @ma_san_pham
	if @ma_phan_cap is null
		return
	exec p_Hierarchy_SyncToMySQL @ma_phan_cap;
	select @ma_phan_cap = ma_cay_phan_cap_6 from Product_Hierarchy where ma_san_pham = @ma_san_pham
	if @ma_phan_cap is null
		return
	exec p_Hierarchy_SyncToMySQL @ma_phan_cap;
	select @ma_phan_cap = ma_cay_phan_cap_7 from Product_Hierarchy where ma_san_pham = @ma_san_pham
	if @ma_phan_cap is null
		return
	exec p_Hierarchy_SyncToMySQL @ma_phan_cap;
	select @ma_phan_cap = ma_cay_phan_cap_8 from Product_Hierarchy where ma_san_pham = @ma_san_pham
	if @ma_phan_cap is null
		return
	exec p_Hierarchy_SyncToMySQL @ma_phan_cap;
	select @ma_phan_cap = ma_cay_phan_cap_9 from Product_Hierarchy where ma_san_pham = @ma_san_pham
	if @ma_phan_cap is null
		return
	exec p_Hierarchy_SyncToMySQL @ma_phan_cap;
	select @ma_phan_cap = ma_cay_phan_cap_10 from Product_Hierarchy where ma_san_pham = @ma_san_pham
	if @ma_phan_cap is null
		return
	exec p_Hierarchy_SyncToMySQL @ma_phan_cap;
END