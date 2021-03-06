USE [dbase1]
GO
/****** Object:  StoredProcedure [ServiceRecords].[getHistoryOrderAndReturn]    Script Date: 14.01.2021 11:26:36 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- ПО "СЗ на ДС"
-- Author:		SAA
-- Create date: 2019-05-22
-- Description:	Получение истории заказов и возврата денег
ALTER PROCEDURE [ServiceRecords].[getHistoryOrderAndReturn] 
			@id_ServiceRecords		[INT]

	
AS
BEGIN
	SET NOCOUNT ON;
	SET XACT_ABORT ON;

	DECLARE @typeSROnTime int;
	select @typeSROnTime = TypeServiceRecordOnTime from [ServiceRecords].[j_ListServiceRecords] where id = @id_ServiceRecords

	DECLARE @table table (indexMonth int)

	IF MONTH(getdate()) in (1,2,3)
		BEGIN  insert into @table (indexMonth) values (1),(2),(3) END
	else IF MONTH(getdate()) in (4,5,6)
		BEGIN insert into @table (indexMonth) values (4),(5),(6) END
	else IF MONTH(getdate()) in (7,8,9)
		BEGIN insert into @table (indexMonth) values (7),(8),(9) END
	else IF MONTH(getdate()) in (10,11,12)
		BEGIN insert into @table (indexMonth) values (10),(11),(12) END

	select maxSumma,
			sumOrderGet,
			sumGet,
			sumGetLastMonth,
			sumOrderReturn,
			sumReturn,
			sumReport,
			(maxSumma - sumOrderGet  + sumReturn) as balanceGet,
			(maxSumma - sumGetInValuta) as balanceGetInValuta,
			case when MONTH(getdate()) = MONTH(DateCreateReport) and YEAR(getdate()) = YEAR(DateCreateReport) then (sumGet - sumOrderReturn) else 
			 (sumGetLastMonth - sumOrderReturn) end as balanceReturn,
			--debtReport - sumOrderReturn + sumReturn as balanceReturn,
			debtReport,
			DateCreateReport,
			Valuta,
			sumGetInValuta,
			sumOrderGetInValuta,
			@typeSROnTime as typeSROnTime,
			isValidateReportDate
			FROM (
			select j_ListServiceRecords.Summa as maxSumma,
			isnull(jP.sumGet, 0) as sumGet,
			isnull(jP.sumGetInValuta, 0) as sumGetInValuta,
			isnull(jP2.sumOrderGet, 0) as sumOrderGet,
			isnull(jP2.sumOrderGetInValuta, 0) as sumOrderGetInValuta,
			isnull(jP3.sumReturn, 0) as sumReturn,
			isnull(jP4.sumOrderReturn, 0) as sumOrderReturn,
			isnull(jR.sumReport, 0)  as sumReport,
			Valuta,
			isnull(jPLastMonth.sumGetLastMonth , 0) as sumGetLastMonth,
			isnull(jR.DebtReport, 0) as debtReport,
			isnull(jR.DateCreateReport , 0) as DateCreateReport			
			,case when jR.DateCreateReport is null then j_ListServiceRecords.DateCreate else jR.DateCreateReport end isValidateReportDate
			
			from [ServiceRecords].[j_ListServiceRecords] j_ListServiceRecords

			-- Сумма получения
			LEFT JOIN (select j_Payments.id_ServiceRecords, sum(isnull(j_Payments.Summa,0)) as sumGet, sum(isnull(j_Payments.SummaInValuta, 0)) as sumGetInValuta
			from [ServiceRecords].[j_Payments] j_Payments
			where j_Payments.id_ServiceRecords = @id_ServiceRecords
			and j_Payments.DateAdd is not null
			and j_Payments.type = 1
			and (
					@typeSROnTime = 1 
				or 
					((@typeSROnTime = 2) and  MONTH(j_Payments.DateAdd) = MONTH(getdate()) and YEAR(j_Payments.DateAdd) = YEAR(getdate()))
				or 
					((@typeSROnTime = 4) and YEAR(j_Payments.DateAdd) = YEAR(getdate()) and MONTH(j_Payments.DateAdd) in (select indexMonth from @table))
				)
			group by  j_Payments.id_ServiceRecords) jP  on jP.id_ServiceRecords = j_ListServiceRecords.id

			--Сумма получения за прошлый месяц
			LEFT JOIN (select j_Payments.id_ServiceRecords, sum(isnull(j_Payments.Summa,0)) as sumGetLastMonth, sum(isnull(j_Payments.SummaInValuta, 0)) as sumGetLastMonthInValuta 
			from [ServiceRecords].[j_Payments] j_Payments
			where j_Payments.id_ServiceRecords = @id_ServiceRecords
			and j_Payments.DateAdd is not null
			and j_Payments.type = 1
			and MONTH(j_Payments.DateAdd) = (select MONTH(MAX(j_Payments.DateAdd)) 
											 from [ServiceRecords].[j_Payments] 
											 where j_Payments.id_ServiceRecords = @id_ServiceRecords and  (MONTH(j_Payments.DateAdd) != MONTH(getdate()) or (MONTH(j_Payments.DateAdd) = MONTH(getdate()) and YEAR(j_Payments.DateAdd) != YEAR(getdate()))))
			and YEAR(j_Payments.DateAdd) = (select YEAR(MAX(j_Payments.DateAdd)) 
											 from [ServiceRecords].[j_Payments] 
											 where j_Payments.id_ServiceRecords = @id_ServiceRecords and  (MONTH(j_Payments.DateAdd) != MONTH(getdate()) or (MONTH(j_Payments.DateAdd) = MONTH(getdate()) and YEAR(j_Payments.DateAdd) != YEAR(getdate()))))
			group by  j_Payments.id_ServiceRecords) jPLastMonth  on jPLastMonth.id_ServiceRecords = j_ListServiceRecords.id

			--Сумма заказа денег на получение
			LEFT JOIN (select j_Payments.id_ServiceRecords, sum(isnull(j_Payments.Summa,0)) as sumOrderGet, sum(isnull(j_Payments.SummaInValuta, 0)) as sumOrderGetInValuta
			from [ServiceRecords].[j_Payments] j_Payments
			where j_Payments.id_ServiceRecords = @id_ServiceRecords
			and j_Payments.type = 1
			and (
					@typeSROnTime = 1 
				or 
					((@typeSROnTime = 2) and  MONTH(j_Payments.DateCreate) = MONTH(getdate()) and YEAR(j_Payments.DateCreate) = YEAR(getdate()))
				or 
					((@typeSROnTime = 4) and YEAR(j_Payments.DateCreate) = YEAR(getdate()) and MONTH(j_Payments.DateCreate) in (select indexMonth from @table))
				)
			group by  j_Payments.id_ServiceRecords) jP2  on jP2.id_ServiceRecords = j_ListServiceRecords.id

			--Сумма возврата 
			LEFT JOIN (select j_Payments.id_ServiceRecords, sum(isnull(j_Payments.Summa,0)) as sumReturn, sum(isnull(j_Payments.SummaInValuta, 0)) as sumReturnInValuta
			from [ServiceRecords].[j_Payments] j_Payments
			where j_Payments.id_ServiceRecords = @id_ServiceRecords
			and j_Payments.DateProtect is not null
			and j_Payments.type = 2
			and (@typeSROnTime = 1 or 
			((@typeSROnTime = 2) and j_Payments.DateProtect > (select top 1 DateCreateReport from [ServiceRecords].[j_Report]
																 where id_ServiceRecords = @id_ServiceRecords
																 order by DateCreateReport desc)
								 and j_Payments.DateProtect < getdate()))

			group by  j_Payments.id_ServiceRecords) jP3  on jP3.id_ServiceRecords = j_ListServiceRecords.id

			--Сумма заказа возврата
			LEFT JOIN (select j_Payments.id_ServiceRecords, sum(isnull(j_Payments.Summa, 0 )) as sumOrderReturn, sum(isnull(j_Payments.SummaInValuta, 0)) as sumOrderReturnInValuta
			from [ServiceRecords].[j_Payments] j_Payments
			where j_Payments.id_ServiceRecords = @id_ServiceRecords
			and j_Payments.type = 2
			and (@typeSROnTime = 1 or 			
			((@typeSROnTime = 2) and j_Payments.DateProtect > (select top 1 DateCreateReport from [ServiceRecords].[j_Report]
																 where id_ServiceRecords = @id_ServiceRecords
																 order by DateCreateReport desc)
								 and j_Payments.DateProtect < getdate()))
			group by  j_Payments.id_ServiceRecords) jP4  on jP4.id_ServiceRecords = j_ListServiceRecords.id

			--Сумма отчета
			LEFT JOIN (select id_ServiceRecords, sum(isnull(SummaReport, 0)) as sumReport, sum(isnull(DebtReport,0)) as DebtReport, max(isnull(DateCreateReport, 0)) as DateCreateReport
			from [ServiceRecords].[j_Report]
			where id_ServiceRecords = @id_ServiceRecords
			and DateCreateReport = (SELECT MAX(DateCreateReport) from [ServiceRecords].[j_Report] where id_ServiceRecords = @id_ServiceRecords)
			group by id_ServiceRecords
			) jR on jR.id_ServiceRecords = j_ListServiceRecords.id


			where id  = @id_ServiceRecords
			)itog

END

