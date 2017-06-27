

IF EXISTS (SELECT name FROM [dbo].sysobjects 
         WHERE name = 'p_Merchant_Product_Hierarchy_SyncToMySQL_All' AND type = 'P')
   DROP PROCEDURE [dbo].p_Merchant_Product_Hierarchy_SyncToMySQL_All
GO
 
CREATE PROCEDURE [dbo].p_Merchant_Product_Hierarchy_SyncToMySQL_All
AS
begin
	declare @id int,@maxID int,@msp varchar(30), @mgh varchar(30)
	begin
	set @id = 1
	set @maxID = (select top 1 id from Merchant_Product_Hierarchy order by id desc)	
	while (@id <= @maxID)		
		begin
			if exists(select id from Merchant_Product_Hierarchy where id = @id)
			begin
				select @msp = ma_san_pham, @mgh = ma_gian_hang from Merchant_Product_Hierarchy where @id = id
				if @msp is not null 
				begin
					exec p_Merchant_Product_Hierarchy_SyncToMySQL @msp, @mgh
				end	
			end
			set @id = @id + 1	
		end
	end
end