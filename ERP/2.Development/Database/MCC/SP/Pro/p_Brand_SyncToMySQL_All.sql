IF EXISTS (SELECT name FROM [dbo].sysobjects 
         WHERE name = 'p_Brand_SyncToMySQL_All' AND type = 'P')
   DROP PROCEDURE [dbo].p_Brand_SyncToMySQL_All
GO
CREATE PROCEDURE [dbo].p_Brand_SyncToMySQL_All
AS
BEGIN
    EXECUTE ('TRUNCATE TABLE brandmanagement' ) AT MYSQL_BIBIAM
	declare @id int,@maxID int,@ma varchar(30)
	begin
	set @id = 1
	set @maxID = (select top 1 id from BrandManagement order by id desc)	
	while (@id <= @maxID)		
		begin
			if exists(select id from BrandManagement where id = @id)
			begin
				select @ma = ma_thuong_hieu from BrandManagement where @id = id
				if @ma is not null
				begin
					exec p_Brand_SyncToMySQL @ma
				end	
			end
			set @id = @id + 1	
		end
	end
end