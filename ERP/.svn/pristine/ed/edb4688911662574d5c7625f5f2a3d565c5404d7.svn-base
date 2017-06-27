
IF EXISTS (SELECT name FROM [dbo].sysobjects 
         WHERE name = 'p_Merchant_Info_SyncToMySQL_All' AND type = 'P')
   DROP PROCEDURE [dbo].p_Merchant_Info_SyncToMySQL_All
GO
 
CREATE PROCEDURE [dbo].p_Merchant_Info_SyncToMySQL_All
AS
begin
	declare @id int,@maxID int, @mgh varchar(30)
	begin
	set @id = 1
	set @maxID = (select top 1 id from Merchant_Info order by id desc)	
	while (@id <= @maxID)		
		begin
			set @mgh = (select ma_gian_hang from Merchant_Info where @id = id)
			if @mgh is not null 
			begin
				exec p_Merchant_Info_SyncToMySQL @mgh						
			end	
			set @id = @id + 1	
		end
	end
end