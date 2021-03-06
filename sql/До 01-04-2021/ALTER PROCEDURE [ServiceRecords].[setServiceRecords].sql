USE [dbase1]
GO
/****** Object:  StoredProcedure [ServiceRecords].[setServiceRecords]    Script Date: 08.09.2020 11:56:30 ******/
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
-- Description:	Добавлен [id_Object]
-- Author:		SAA
-- Update date: 2019-06-03
-- Description:	Добавлен [TypeServiceRecordOnTime]
-- Update date: 2019-06-18
-- Description:	Добавлены SummaNal и SummaBezNal
-- =============================================
ALTER PROCEDURE [ServiceRecords].[setServiceRecords]
			@Description			[varchar](max),
			@CreateServiceRecord	[datetime],
			@TypeServiceRecord		[int],
			@id_Block				[INT],
			@id_Department			[INT],
			@Summa					[numeric](11 ,2),
			@bCashNonCash			[bit],
			@DataSumma				[datetime] = null,
			@bDataSumma				[bit] = null,
			@MonthB					[datetime],
			@Comments				[varchar](max),
			@idUser					[int],
			@id_Object				[int],
			@TypeServiceRecordOnTime [int],
			@Valuta					[varchar](10),
			@SummaCash				[numeric](11 ,2) = 0,
			@SummaNonCash			[numeric](11 ,2) = 0,
			@Mix					[bit] = 0,
			@id_fond				[int] = null,
			@inType					[int] = null
AS
BEGIN
	SET NOCOUNT ON;
	SET XACT_ABORT ON;


	INSERT INTO [ServiceRecords].[j_ListServiceRecords]
           (
		   --[Number]
           [Description]
           ,[CreateServiceRecord]
           ,[TypeServiceRecord]
           --,[ConfirmationD]
           --,[DateConfirmationD]
           --,[PreviosConfirmationD]
           --,[DatePreviosConfirmationD]
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
           ,[id_Creator]
           ,[DateCreate]
           --,[id_Editor]
           --,[DateEdit]
		   ,[id_Object]
		   ,[TypeServiceRecordOnTime]
		   ,[Valuta]
		   ,[SummaCash]
		   ,[SummaNonCash]
		   ,[Mix]
		   ,[inType])
     VALUES
           (
			@Description,
			@CreateServiceRecord,
			@TypeServiceRecord,
			@id_Block,
			@id_Department,
			@Summa,
			@bCashNonCash,
			@DataSumma,
			@bDataSumma,
			@MonthB,
			@Comments,
			1,
			GETDATE(),
			@idUser,
			GETDATE(),
			@id_Object,
			@TypeServiceRecordOnTime,
			@Valuta,
			@SummaCash,
			@SummaNonCash,
			@Mix,
			@inType
		   )


		   DECLARE @id int;

		   SET @id = SCOPE_IDENTITY();
		   
		   insert into ServiceRecords.j_ChangeStatusHistory(id_ServiceRecords,id_Status,id_Change,DateChange,Comments)
		   values (@id,1,@idUser,GETDATE(),'');


		   IF @id_fond is not null
			BEGIN
				INSERT INTO ServiceRecords.j_Fond (id_ServiceRecords,id_ServiceRecordsFond,id_Creator,id_Editor,DateCreate,DateEdit)
					VALUES (@id,@id_fond,@idUser,@idUser,GETDATE(),GETDATE())
			END


		   SELECT @id as id	

END
