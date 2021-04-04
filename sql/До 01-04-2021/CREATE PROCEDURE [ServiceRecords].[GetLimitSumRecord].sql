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
-- Description:	Получение сумма по предварительной СЗ
-- =============================================
CREATE PROCEDURE [ServiceRecords].[GetLimitSumRecord]
		@id_ListServiceRecords int
AS
BEGIN
	SET NOCOUNT ON;

select 
	Summa 
from 
	ServiceRecords.j_LimitSumRecord
where	
	id_ListServiceRecords = @id_ListServiceRecords

END
