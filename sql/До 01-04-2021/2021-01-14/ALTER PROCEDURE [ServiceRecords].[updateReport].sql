USE [dbase1]
GO
/****** Object:  StoredProcedure [ServiceRecords].[updateReport]    Script Date: 25.01.2021 17:06:25 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		SAA
-- Create date: 2019-08-26
-- Description:	Обновление отчета
-- =============================================
ALTER PROCEDURE [ServiceRecords].[updateReport] 
	@id_ServiceRecords int,
	@summa_report [numeric](11, 2),
	@debt [numeric](11, 2),
	@CashNonCach int, -- 0 - нал, 1 - безнал
	@id_creator int

AS
BEGIN
	SET NOCOUNT ON;
	SET XACT_ABORT ON;

	DECLARE @TypeServiceRecordOnTime int											-- 1 - разовая, 2 - ежемесячная
		SELECT @TypeServiceRecordOnTime = TypeServiceRecordOnTime from [ServiceRecords].[j_ListServiceRecords]
	where id = @id_ServiceRecords

	-- проверка старых отчетов
		DECLARE @MonthDebtEveryTimeSZ datetime -- месяц старого долга
	SELECT top 1 @MonthDebtEveryTimeSZ = MonthReport from [ServiceRecords].[j_Report]  where id_ServiceRecords = @id_ServiceRecords and MonthReport < getdate()  order by MonthReport desc

	-- РАЗОВАЯ СЗ
	if (@TypeServiceRecordOnTime = 1)
		begin
			update 
				[ServiceRecords].[j_Report] 
			set 
				SummaReport = @summa_report, DebtReport = @debt , StatusReport = 0, DateEdit = getdate() 
			where 
				id_ServiceRecords = @id_ServiceRecords and typeCashNonCash = @CashNonCach
			return;
		end
	else
	-- ЕЖЕМЕСЯЧНАЯ СЗ	
	if(@TypeServiceRecordOnTime = 2)
	BEGIN
		update 
			[ServiceRecords].[j_Report] 
		set 
			SummaReport = @summa_report, DebtReport = @debt , StatusReport = 0, DateEdit = getdate() 
		where 
			id_ServiceRecords = @id_ServiceRecords and typeCashNonCash = @CashNonCach and Month(MonthReport) = Month(@MonthDebtEveryTimeSZ) and Year(MonthReport) = Year(@MonthDebtEveryTimeSZ) 
			
		return;
	END
	else
	-- ЕЖЕКвартальная СЗ	
	IF @TypeServiceRecordOnTime = 4
		BEGIN
				DECLARE @table table (indexMonth int)

				IF MONTH(getdate()) in (1,2,3)
					BEGIN  insert into @table (indexMonth) values (1),(2),(3) END
				else IF MONTH(getdate()) in (4,5,6)
					BEGIN insert into @table (indexMonth) values (4),(5),(6) END
				else IF MONTH(getdate()) in (7,8,9)
					BEGIN insert into @table (indexMonth) values (7),(8),(9) END
				else IF MONTH(getdate()) in (10,11,12)
					BEGIN insert into @table (indexMonth) values (10),(11),(12) END

				update 
					[ServiceRecords].[j_Report] 
				set 
					SummaReport = @summa_report, DebtReport = @debt , StatusReport = 0, DateEdit = getdate() 
				where 
					id_ServiceRecords = @id_ServiceRecords and typeCashNonCash = @CashNonCach and Month(MonthReport) = Month(@MonthDebtEveryTimeSZ) and Year(MonthReport) = Year(@MonthDebtEveryTimeSZ) 
				
				--MONTH(j_Payments.DateAdd) in (select indexMonth from @table)
			return;
		END

	---- Для старых СЗ (для внедрения)
	--IF (not exists (select id from ServiceRecords.j_Report where id_ServiceRecords = @id_ServiceRecords))
	--INSERT INTO ServiceRecords.j_Report (id_ServiceRecords, SummaReport, DebtReport, DateCreateReport, id_CreatorReport, typeCashNonCash, StatusReport, MonthReport, DateEdit)  
	--VALUES (@id_ServiceRecords, @summa_report, @debt, getdate(), @id_creator, @CashNonCach, 0, null, getdate())
END
	
