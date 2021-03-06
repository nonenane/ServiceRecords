USE [dbase1]
GO
/****** Object:  StoredProcedure [ServiceRecords].[updateServiceRecords]    Script Date: 08.09.2020 11:57:25 ******/
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
-- Update date: 2019-03-06
-- Description:	Добавлен TypeServiceRecordOnTime and Valuta
-- =============================================
ALTER PROCEDURE [ServiceRecords].[updateServiceRecords]
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
			@id						[int],
			@id_Object				[int],
			@TypeServiceRecordOnTime [int],
			@Valuta					[varchar](max),
			@SummaCash				[numeric](11 ,2) = 0,
			@SummaNonCash			[numeric](11 ,2) = 0,
			@Mix					[bit] = 0,
			@id_fond				[int] = null,
			@inType					[int] = null
	
AS
BEGIN
	SET NOCOUNT ON;
	SET XACT_ABORT ON;

	
	UPDATE 
		[ServiceRecords].[j_ListServiceRecords]
	SET
			[Description] = @Description
           --,[CreateServiceRecord]
           ,[TypeServiceRecord]  =@TypeServiceRecord
           --,[ConfirmationD]
           --,[DateConfirmationD]
           --,[PreviosConfirmationD]
           --,[DatePreviosConfirmationD]
           ,[id_Block] = @id_Block
           ,[id_Department] = @id_Department
           ,[Summa] = @Summa
           ,[bCashNonCash] = @bCashNonCash
           ,[DataSumma] = @DataSumma
           ,[bDataSumma] = @bDataSumma
           ,[MonthB] = @MonthB
           ,[Comments] = @Comments
           --,[id_Status]
           --,[DateStatusChange]
		   ,[id_Editor] = @idUser
           ,[DateEdit] = GETDATE()
		   ,[id_Object] = @id_Object
		   ,[TypeServiceRecordOnTime] = @TypeServiceRecordOnTime
		   ,[Valuta] = @Valuta
		   ,[SummaCash] = @SummaCash
		   ,[SummaNonCash] = @SummaNonCash
		   ,[Mix] = @Mix
		   ,[inType] = @inType
	WHERE
		id = @id


	IF @id_fond is not null
		BEGIN

			IF EXISTS (select id from ServiceRecords.j_Fond where id_ServiceRecords = @id)
				BEGIN
					UPDATE ServiceRecords.j_Fond
					SET	id_ServiceRecordsFond = @id_fond,id_Editor = @idUser,DateEdit = GETDATE()
					WHERE id_ServiceRecords = @id
				END
			ELSE
				BEGIN
					INSERT INTO ServiceRecords.j_Fond (id_ServiceRecords,id_ServiceRecordsFond,id_Creator,id_Editor,DateCreate,DateEdit)
					VALUES (@id,@id_fond,@idUser,@idUser,GETDATE(),GETDATE())
				END
		END
	ELSE
		BEGIN
			DELETE FROM ServiceRecords.j_Fond WHERE id_ServiceRecords = @id
		END

END
