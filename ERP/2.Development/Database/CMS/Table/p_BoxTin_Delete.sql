USE [CMSDev]
GO
/****** Object:  StoredProcedure [dbo].[p_BoxTin_Delete]    Script Date: 03/05/2017 1:32:56 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[p_BoxTin_Delete]
@id int
as
begin
	delete from cms_BoxTin 
	where id=@id
end