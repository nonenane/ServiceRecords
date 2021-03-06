USE [dbase1]
GO
/****** Object:  StoredProcedure [ServiceRecords].[setPayments]    Script Date: 29.04.2021 13:47:54 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- ПО "СЗ на ДС"
-- Author:		SPG
-- Create date: 2014-06-09
-- Description:	Получение Основной таблицы

-- Author: SAA
-- Update date: 2018-09-06
-- Description: Добавлен запрос на добавление id_MoneyRecipient
-- Last Description: Добавлено обновление статуса СЗ, добавлено сохранение для новых столбцов Valuta и SummaInValuta

-- Editor:		Molotkova_IS
-- Edit date:	2020-01-31
-- Description:	Добавлен вывод ид и наименования статуса до и после
-- =============================================
ALTER PROCEDURE [ServiceRecords].[setPayments]
			@id_ServiceRecords		[INT],
			@DataSumma				[datetime],
			@Summa					[numeric](11 ,2),	
			@type					[INT],	
			@idUser					[int],
			@idMoneyRecipient		[INT],
			@status					[INT],
			@summaInValuta			[numeric](11 ,2),
			@Valuta					varchar(10),
			@typeCashNonCash		int = 9
	
AS
BEGIN
	SET NOCOUNT ON;
	SET XACT_ABORT ON;
	

	

	declare @status_old int
	declare @name_old varchar(max)
	
	select @status_old = id_Status from [ServiceRecords].[j_ListServiceRecords] where id = @id_ServiceRecords
	select @name_old = cName from [ServiceRecords].[s_Status] where id = @status_old

	if(@status_old<>@status)
		BEGIN
			INSERT INTO [ServiceRecords].[j_ChangeStatusHistory]
				values(@id_ServiceRecords,@status,@idUser,GETDATE(),'')
		END

	if (@typeCashNonCash = 9)
		begin
			select @typeCashNonCash = bCashNonCash from [ServiceRecords].[j_ListServiceRecords] where id = @id_ServiceRecords
		end


	INSERT INTO [ServiceRecords].[j_Payments]
           (
		   [id_ServiceRecords],
		   [DataSumma],
		   [Summa],
		   [type],
		   [DateCreate],
		   [id_Creator],
		   [id_MoneyRecipient],
		   [SummaInValuta],
		   [Valuta],
		   [typeCashNonCash]
		   )
     VALUES
           (
			@id_ServiceRecords,
			@DataSumma,
			@Summa,
			@type,
			GETDATE(),
			@idUser,
			@idMoneyRecipient,
			@summaInValuta,
			@Valuta,
			@typeCashNonCash	
		   )
		
	UPDATE [ServiceRecords].[j_ListServiceRecords] set id_Status = @status where id = @id_ServiceRecords

	SELECT SCOPE_IDENTITY() as id, @status_old as id_prev, @name_old as cName_prev, s.id as id_status, s.cName from [ServiceRecords].[s_Status] s where id = @status

END


