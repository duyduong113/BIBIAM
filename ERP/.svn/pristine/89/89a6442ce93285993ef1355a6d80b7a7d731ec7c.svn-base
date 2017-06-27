
IF EXISTS (SELECT name FROM [dbo].sysobjects 
         WHERE name = 'p_Hierarchy_SyncToMySQL_All' AND type = 'P')
   DROP PROCEDURE [dbo].p_Hierarchy_SyncToMySQL_All
GO
 
CREATE pROCEDURE [dbo].p_Hierarchy_SyncToMySQL_All
AS
begin
	declare @id int,@maxID int,@mpc varchar(30)
	begin
	set @id = 1
	set @maxID = (select top 1 id from Hierarchy order by id desc)	
	while (@id <= @maxID)		
		begin
			if exists(select id from Hierarchy where id = @id)
			begin
				select @mpc = ma_phan_cap from Hierarchy where @id = id
				if @mpc is not null
				begin
					exec p_Hierarchy_SyncToMySQL @mpc
				end	
			end
			set @id = @id + 1	
		end
	end
end