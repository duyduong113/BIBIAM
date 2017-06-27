CREATE PROCEDURE p_Product_SyncFullToMySQL
	@id int,
	@ma_san_pham [varchar](255)
AS
BEGIN
	exec p_Product_Info_SyncToMySQL @id,@ma_san_pham;	
	exec p_Product_Price_SyncToMySQL @ma_san_pham;
	exec p_Product_Image_SyncToMySQL @ma_san_pham;
	exec p_Hierarchy_SyncByProductIDToMySQL @ma_san_pham;
	declare @id_pro_hier int
	select @id_pro_hier = id from Product_Hierarchy where ma_san_pham = @ma_san_pham
	exec p_Product_Hierarchy_SyncToMySQL @id_pro_hier, @ma_san_pham;		
END