USE [dbase1]
GO
/****** Object:  StoredProcedure [ServiceRecords].[updatePayments]    Script Date: 04.05.2021 9:34:15 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:			SAA
-- Create date:		2018-06-13
-- Description:		Редактирование заказа денег
-- Editor:			Molotkova_IS
-- Edit date:		2020-02-04
-- Description:		Добавлен вывод id и ФИО автора
-- =============================================
ALTER PROCEDURE [ServiceRecords].[updatePayments]
			@idOrder				[INT],
			@DataSumma				[datetime],
			@Summa					[numeric](11 ,2),	
			@type					[INT],	
			@idUser					[int],
			@idMoneyRecipient		[INT],
			@status					[INT],
			@summaInValuta			[numeric](11 ,2),
			@isChangeUserMoneyTake	bit = 0

	
AS
BEGIN
	SET NOCOUNT ON;
	SET XACT_ABORT ON;

	IF(@isChangeUserMoneyTake = 1)
		BEGIN
				update 
					[ServiceRecords].[j_Payments] 
				set  
					[id_MoneyRecipient] = @idMoneyRecipient
				where 
					id = @idOrder and [j_Payments].DateAdd is null and  [j_Payments].DateProtect is null
	
				SELECT 
					@idOrder as id, jp.id_Creator, lu.FIO
				FROM 
					[ServiceRecords].[j_Payments] jp
					LEFT JOIN [dbo].[ListUsers] lu on lu.id = jp.id_Creator
				WHERE 
				jp.id = @idOrder and jp.DateAdd is null and  jp.DateProtect is null

			return
		END

	if (exists (select id from [ServiceRecords].[j_Payments] where id =@idOrder and [j_Payments].DateAdd is null and  [j_Payments].DateProtect is null))
	BEGIN
			update 
				[ServiceRecords].[j_Payments] 
			set  
				[DataSumma] = @DataSumma, 
				[Summa] = @Summa,
				[type] = @type, 
				[DateCreate] = GETDATE(), 
				[id_Creator] = @idUser, 
				[id_MoneyRecipient] = @idMoneyRecipient, 
				[SummaInValuta] = @summaInValuta 
			where 
				id = @idOrder and [j_Payments].DateAdd is null and  [j_Payments].DateProtect is null
	
			SELECT 
				CAST(SCOPE_IDENTITY() as int) as id, jp.id_Creator, lu.FIO
			FROM 
				[ServiceRecords].[j_Payments] jp
				LEFT JOIN [dbo].[ListUsers] lu on lu.id = jp.id_Creator
			WHERE 
			jp.id = @idOrder and jp.DateAdd is null and  jp.DateProtect is null
	END
	else 
		select 'ошибка' as error

	

END
