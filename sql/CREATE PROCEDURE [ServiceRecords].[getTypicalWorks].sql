USE [dbase1]
GO
/****** Object:  StoredProcedure [ServiceRecords].[SetSettings]    Script Date: 08.09.2020 10:32:42 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:		Sporykhin G Y
-- Create date: 2020-09-08
-- Description:	Получение списка типов работ
-- =============================================
CREATE PROCEDURE [ServiceRecords].[getTypicalWorks]
		
AS
BEGIN
	SET NOCOUNT ON;

	SELECT 
		t.id,
		t.cName,
		t.isActive
	FROM
		[ServiceRecords].[s_TypicalWorks] t

END
