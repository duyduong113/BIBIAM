
IF  EXISTS (SELECT * FROM sys.objects 
WHERE object_id = OBJECT_ID(N'[dbo].[p_Footer_SyncToMySQL_All]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].p_Footer_SyncToMySQL_All
GO
CREATE PROCEDURE [dbo].p_Footer_SyncToMySQL_All
AS
BEGIN
    EXECUTE ('TRUNCATE TABLE footer' ) AT MYSQL_BIBIAM_DEV
	declare @id int,@maxID int,@ma_footer varchar(30)
	begin
	set @id = 1
	set @maxID = (select top 1 id from Footer order by id desc)	
	while (@id <= @maxID)		
		begin
			if exists(select id from Footer where id = @id)
			begin
				select @ma_footer = ma_footer from Footer where @id = id
				if @ma_footer is not null
				begin
					exec p_Footer_SyncToMySQL @ma_footer
				end	
			end
			set @id = @id + 1	
		end
	end
end