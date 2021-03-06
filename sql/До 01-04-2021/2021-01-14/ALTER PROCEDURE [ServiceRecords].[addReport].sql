USE [dbase1]
GO
/****** Object:  StoredProcedure [ServiceRecords].[addReport]    Script Date: 27.01.2021 10:17:21 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		SAA
-- Create date: 2019-08-26
-- Description:	Создание отчета
-- =============================================
ALTER PROCEDURE [ServiceRecords].[addReport]
	@id_ServiceRecords int,
	@debt [numeric](11, 2),
	@CashNonCach int, -- 0 - нал, 1 - безнал
	@id_creator int,
	@operation int, -- 1-получение, 2-возврат ДС
	@summa_return [numeric](11, 2) = 0
	,@id_payment int

AS
BEGIN
	SET NOCOUNT ON;
	SET XACT_ABORT ON;

	DECLARE @table table (indexMonth int)
	DECLARE @DateCretePayment date
	select @DateCretePayment = p.DataSumma from ServiceRecords.j_Payments p where p.id = @id_payment



	IF MONTH(@DateCretePayment) in (1,2,3)
		BEGIN  insert into @table (indexMonth) values (1),(2),(3) END
	else IF MONTH(@DateCretePayment) in (4,5,6)
		BEGIN insert into @table (indexMonth) values (4),(5),(6) END
	else IF MONTH(@DateCretePayment) in (7,8,9)
		BEGIN insert into @table (indexMonth) values (7),(8),(9) END
	else IF MONTH(@DateCretePayment) in (10,11,12)
		BEGIN insert into @table (indexMonth) values (10),(11),(12) END

	

	DECLARE @TypeServiceRecordOnTime int											-- 1 - разовая, 2 - ежемесячная
		SELECT @TypeServiceRecordOnTime = TypeServiceRecordOnTime from [ServiceRecords].[j_ListServiceRecords]
	where id = @id_ServiceRecords

	DECLARE @DebtReport [numeric](11, 2)
	select top 1 @DebtReport = DebtReport from [ServiceRecords].[j_Report] where id_ServiceRecords = @id_ServiceRecords AND typeCashNonCash = @CashNonCach and (MonthReport is null or MonthReport  < getdate()) order by DateCreateReport desc

	DECLARE @MonthDebtEveryTimeSZ datetime -- месяц старого долга
	SELECT top 1 @MonthDebtEveryTimeSZ = MonthReport from [ServiceRecords].[j_Report]  where id_ServiceRecords = @id_ServiceRecords AND typeCashNonCash = @CashNonCach and MonthReport < getdate() and DebtReport > 0 

	-- создаем новый отчет, если по СЗ еще не было отчетов
	IF(not exists (
			select  
				id 
			from 
				[ServiceRecords].[j_Report] 
			where 
				id_ServiceRecords = @id_ServiceRecords 
				AND typeCashNonCash = @CashNonCach 
				AND (
						@TypeServiceRecordOnTime = 1 
					OR 
						(@TypeServiceRecordOnTime = 2 and MONTH(DatePayments) = MONTH(@DateCretePayment) and YEAR(DatePayments) = YEAR(getdate()))
					OR 
						(@TypeServiceRecordOnTime = 4 and MONTH(DatePayments) in (select indexMonth from @table) and YEAR(DatePayments) = YEAR(getdate()))
					)
		) and @operation = 1
		)
	BEGIN
		if (@TypeServiceRecordOnTime = 1)
			insert into [ServiceRecords].[j_Report] (id_ServiceRecords, SummaReport, DebtReport, DateCreateReport, id_CreatorReport, typeCashNonCash, StatusReport, DateEdit,DatePayments) 
			VALUES (@id_ServiceRecords, @summa_return, @debt, getdate(), @id_creator, @CashNonCach, 0, getdate(),@DateCretePayment)
		else 
			insert into [ServiceRecords].[j_Report] (id_ServiceRecords, SummaReport, DebtReport, DateCreateReport, id_CreatorReport, typeCashNonCash, StatusReport, MonthReport, DateEdit,DatePayments) 
			VALUES (@id_ServiceRecords, @summa_return, @debt, getdate(), @id_creator, @CashNonCach, 0, getdate(), getdate(),@DateCretePayment)
	END

		-- если отчет есть и произведено получение ДС
	ELSE IF ( @operation = 1)
		BEGIN
			update [ServiceRecords].[j_Report] set DebtReport = @DebtReport + @debt, StatusReport = 0, DateEdit = getdate() where id_ServiceRecords = @id_ServiceRecords AND typeCashNonCash = @CashNonCach
		END

	-- если отчет есть и произведен возврат ДС
	ELSE IF (@TypeServiceRecordOnTime = 1 and @DebtReport > 0 and @operation = 2)
		BEGIN
			update [ServiceRecords].[j_Report] set DebtReport = @DebtReport - @summa_return, StatusReport = 0, DateEdit = getdate() where id_ServiceRecords = @id_ServiceRecords AND typeCashNonCash = @CashNonCach
		END

	-- вычитаем из долга за предыдущий месяц
	ELSE IF (@TypeServiceRecordOnTime = 2 and @DebtReport > 0 and @operation = 2)
		BEGIN
			update [ServiceRecords].[j_Report] set DebtReport = @DebtReport - @summa_return, StatusReport = 0, DateEdit = getdate() 
											  where id_ServiceRecords = @id_ServiceRecords AND 
											  typeCashNonCash = @CashNonCach and MONTH(DateCreateReport) = MONTH(@MonthDebtEveryTimeSZ)
		END

	-- вычитаем из долг за квартал
	ELSE IF (@TypeServiceRecordOnTime = 4 and @DebtReport > 0 and @operation = 2)
		BEGIN
			update [ServiceRecords].[j_Report] set DebtReport = @DebtReport - @summa_return, StatusReport = 0, DateEdit = getdate() 
											  where id_ServiceRecords = @id_ServiceRecords AND 
											  typeCashNonCash = @CashNonCach and MONTH(DateCreateReport) = MONTH(@MonthDebtEveryTimeSZ)
		END

END
	
