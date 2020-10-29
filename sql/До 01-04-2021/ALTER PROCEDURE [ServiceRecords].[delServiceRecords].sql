USE [dbase1]
GO
/****** Object:  StoredProcedure [ServiceRecords].[delServiceRecords]    Script Date: 29.10.2020 12:12:27 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		SPG
-- Create date: 2014-06-09
-- Description:	Получение Основной таблицы
-- =============================================
ALTER PROCEDURE [ServiceRecords].[delServiceRecords]
	@id int
	
AS
BEGIN
	SET NOCOUNT ON;
	SET XACT_ABORT ON;

	UPDATE ServiceRecords.j_Memorandums set id_ListServiceRecords = null where id_ListServiceRecords = @id

	delete from ServiceRecords.j_Fond where id_ServiceRecords = @id or id_ServiceRecordsFond  = @id

	DELETE FROM 
		[ServiceRecords].[j_ListServiceRecords] 
	WHERE 
		id  = @id



END
