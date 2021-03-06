 USE [dbase1]
GO
/****** Object:  StoredProcedure [ServiceRecords].[getServiceRecordsBody]    Script Date: 08.09.2020 11:23:27 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		SPG
-- Create date: 2014-06-09
-- Description:	Получение Основной таблицы

-- Author:		SAA
-- Update date: 2019-02-11
-- Description:	Добавлен вывод нового столбца id_Object

-- Author:		SAA
-- Update date: 2019-03-06
-- Description:	Добавлен вывод нового столбца TypeServiceRecordOnTime and Valuta

-- Editor:		Molotkova_IS
-- Edit date:	2020-01-31
-- Description: Добавлен вывод наименования объекта
-- =============================================
ALTER PROCEDURE [ServiceRecords].[getServiceRecordsBody]
	@id int
	
AS
BEGIN
	SET NOCOUNT ON;
	SET XACT_ABORT ON;


	SELECT
       [Number]
      ,[Description]
      ,[CreateServiceRecord]
      ,[TypeServiceRecord]
      ,[ConfirmationD]
      ,[DateConfirmationD]
      ,[PreviosConfirmationD]
      ,[DatePreviosConfirmationD]
      ,[id_Block]
      ,[id_Department]
      ,[Summa]
      ,[bCashNonCash]
      ,[DataSumma]
      ,[bDataSumma]
      ,[MonthB]
      ,[Comments]
      ,[id_Status]
      ,[DateStatusChange]
      ,r.[id_Creator]
      ,r.[DateCreate]
      ,r.[id_Editor]
      ,r.[DateEdit],
	   db.name as nameBlock,
	   d.name as nameDeps,
	   s.cName as nameStatus,
	   r.[id_Object],
	   ob.name_Object,
	   r.TypeServiceRecordOnTime,
	   r.Valuta,
       r.SummaCash,
	   r.SummaNonCash,
	   r.Mix,
	   f.id_ServiceRecordsFond,
	   r.inType
	FROM
		[ServiceRecords].[j_ListServiceRecords] r
		LEFT JOIN dbo.departments db on db.id = r.id_Block
		LEFT JOIN dbo.departments d on d.id = r.id_Department
		left join ServiceRecords.s_Status s on s.id = r.id_Status
		LEFT JOIN [ServiceRecords].[s_Objects] ob on ob.id_Object = r.id_Object
		LEFT JOIN ServiceRecords.j_Fond f on f.id_ServiceRecords = r.id
	WHERE
		r.id = @id



END
