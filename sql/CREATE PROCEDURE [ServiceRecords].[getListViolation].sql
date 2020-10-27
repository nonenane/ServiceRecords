USE [dbase1]
GO
/****** Object:  StoredProcedure [ServiceRecords].[SetSettings]    Script Date: 08.09.2020 10:32:42 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:		Sporykhin G Y
-- Create date: 2020-10-15
-- Description:	Получение списка штрафников по штрафу
-- =============================================
CREATE PROCEDURE [ServiceRecords].[getListViolation]
	@id_Memorandums int 
AS
BEGIN
	SET NOCOUNT ON;

	

select 
	l.sumPenalty,
	l.FIOPenalty
from 
	ServiceRecords.j_ListViolation l
where 
	l.id_Memorandums = @id_Memorandums

END
