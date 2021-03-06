USE [dbase1]
GO
/****** Object:  StoredProcedure [ServiceRecords].[getReportForCheck]    Script Date: 14.01.2021 15:30:39 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		SAA
-- Create date: 2019-04-09
-- Description:	Получение отчетов  для их проверки
-- =============================================
ALTER PROCEDURE [ServiceRecords].[getReportForCheck] 

	@dateTimeStart datetime,
	@dateTimeEnd datetime,
	@id_Block int  = 0,
	@isFartForward bit = 0
	
AS
BEGIN
	SET NOCOUNT ON;

CREATE table  #tmp  (id_Department int)

IF @id_Block <> 0
	BEGIN
		insert into  #tmp
		select id_Department from ServiceRecords.Block_vs_Department where id_Block = @id_Block
		UNION 
		select id_Department from ServiceRecords.Block_vs_Department where id_Department = @id_Block
		group by id_Department
	END


	select @dateTimeEnd = DATEADD(day, 1, @dateTimeEnd);

	--Наличные
	SELECT 
			[j_Report].StatusReport as StatusReport,
			[j_ListServiceRecords].id, 
			[j_ListServiceRecords].id_Status,
			[j_ListServiceRecords].Number, 
			[j_ListServiceRecords].Description,
			[j_ListServiceRecords].Summa, 
			[j_ListServiceRecords].Mix,
			[j_Report].id as id_report,
			case when (MONTH(jR.DateCreateReport) = MONTH(getdate()) and YEAR(jR.DateCreateReport) = YEAR(getdate())) or TypeServiceRecordOnTime = 1 then isnull(jP.sumGet, 0) -isnull(jP3.sumReturn, 0)
			else  isnull(jPLastMonth.sumGetLastMonth, 0) -isnull(jP3.sumReturn, 0) end  as sumGet,
			isnull([j_Report].SummaReport, 0) as SummaReport,
			isnull([j_Report].DebtReport, 0) as debtReport,
			[j_ListServiceRecords].Valuta, 
			[j_Report].DateEdit,
			case when [j_Report].typeCashNonCash = 0 then 'Нал.' else 'БезНал.' end as typeCashNonCash,
			[j_ListServiceRecords].id_Creator,
			[j_ListServiceRecords].inType,
			case 
				when [j_ListServiceRecords].id_Status = 14 then 'Ожидание отчета'
				when [j_Report].StatusReport = 0 then 'Отчет предоставлен'
				when [j_Report].StatusReport = 1 then 'Отчет подтвержден'
				when [j_Report].StatusReport = 2 then 'Отчет отклонен'
			end as nameStatusReport
			,id_Department
			,id_Block

	FROM [ServiceRecords].[j_ListServiceRecords] 
			JOIN [ServiceRecords].[j_Report] ON [j_Report].id_ServiceRecords = [j_ListServiceRecords].id 
			JOIN (SELECT jr.id from [ServiceRecords].[j_Report] jr
					join [ServiceRecords].[j_Report]jr2 on jr.id = jr2.id 
						where jr.DateCreateReport = (select top 1 DateCreateReport from [ServiceRecords].[j_Report] where id_ServiceRecords = jr2.id_ServiceRecords and typeCashNonCash = jr2.typeCashNonCash order by DateCreateReport desc)
			) jPDate on jPDate.id = [j_Report].id
			-- Сумма получения нал
			LEFT JOIN (select j_Payments.id_ServiceRecords, sum(isnull(j_Payments.Summa,0)) as sumGet, sum(isnull(j_Payments.SummaInValuta, 0)) as sumGetInValuta
			from [ServiceRecords].[j_Payments] j_Payments
			JOIN [ServiceRecords].[j_ListServiceRecords] jlr  on j_Payments.id_ServiceRecords = jlr.id
			where 
			j_Payments.DateAdd is not null
			and j_Payments.type = 1
			and j_Payments.typeCashNonCash = 0
			and (jlr.TypeServiceRecordOnTime = 1 or ((jlr.TypeServiceRecordOnTime = 2) and  MONTH(j_Payments.DateAdd) = MONTH(getdate()) and YEAR(j_Payments.DateAdd) = YEAR(getdate())))
			group by  j_Payments.id_ServiceRecords) jP  on jP.id_ServiceRecords = j_ListServiceRecords.id
			--Сумма получения за прошлый месяц
			LEFT JOIN (select j_Payments.id_ServiceRecords, sum(isnull(j_Payments.Summa,0)) as sumGetLastMonth, sum(isnull(j_Payments.SummaInValuta, 0)) as sumGetLastMonthInValuta 
			from [ServiceRecords].[j_Payments] j_Payments
			JOIN [ServiceRecords].[j_ListServiceRecords] jlr  on j_Payments.id_ServiceRecords = jlr.id
			where 
			j_Payments.DateAdd is not null
			and j_Payments.type = 1
			and j_Payments.typeCashNonCash = 0
			and MONTH(j_Payments.DateAdd) = (select MONTH(MAX(j_Payments.DateAdd)) 
											 from [ServiceRecords].[j_Payments] 
											 where j_Payments.id_ServiceRecords = jlr.id and  (MONTH(j_Payments.DateAdd) != MONTH(getdate()) or (MONTH(j_Payments.DateAdd) = MONTH(getdate()) and YEAR(j_Payments.DateAdd) != YEAR(getdate()))))
			and YEAR(j_Payments.DateAdd) = (select YEAR(MAX(j_Payments.DateAdd)) 
											 from [ServiceRecords].[j_Payments] 
											 where j_Payments.id_ServiceRecords = jlr.id and  (MONTH(j_Payments.DateAdd) != MONTH(getdate()) or (MONTH(j_Payments.DateAdd) = MONTH(getdate()) and YEAR(j_Payments.DateAdd) != YEAR(getdate()))))
			group by  j_Payments.id_ServiceRecords) jPLastMonth  on jPLastMonth.id_ServiceRecords = j_ListServiceRecords.id

			--Сумма возврата нал
			LEFT JOIN (select j_Payments.id_ServiceRecords, 
			sum(isnull(j_Payments.Summa,0)) as sumReturn, 
			sum(isnull(j_Payments.SummaInValuta, 0)) as sumReturnInValuta
			from [ServiceRecords].[j_Payments] j_Payments
			JOIN [ServiceRecords].[j_ListServiceRecords] jlr  on j_Payments.id_ServiceRecords = jlr.id
			where 
			j_Payments.DateProtect is not null
			and j_Payments.type = 2
			and j_Payments.typeCashNonCash = 0
			and (j_Payments.DateProtect > (select top 1 DateCreateReport from [ServiceRecords].[j_Report]
																 where id_ServiceRecords = jlr.id
																 order by DateCreateReport desc)
								 and j_Payments.DateProtect < getdate() or jlr.TypeServiceRecordOnTime = 1)
			group by  j_Payments.id_ServiceRecords) jP3  on jP3.id_ServiceRecords = j_ListServiceRecords.id
			
			--Сумма отчета
			LEFT JOIN (select id_ServiceRecords, sum(isnull(SummaReport, 0)) as sumReport, max(isnull(DateCreateReport, 0)) as DateCreateReport
			from [ServiceRecords].[j_Report]
			JOIN [ServiceRecords].[j_ListServiceRecords] jlr  on j_Report.id_ServiceRecords = jlr.id
			where [j_Report].typeCashNonCash = 0
			and MONTH(DateCreateReport) = (SELECT MONTH(MAX(DateCreateReport)) from [ServiceRecords].[j_Report] where id_ServiceRecords = jlr.id)
			and YEAR(DateCreateReport) = (SELECT YEAR(MAX(DateCreateReport)) from [ServiceRecords].[j_Report] where id_ServiceRecords = jlr.id)
			group by id_ServiceRecords
			) jR on jR.id_ServiceRecords = j_ListServiceRecords.id



	WHERE 
	(@isFartForward =0 and
	[j_Report].DateEdit >= @dateTimeStart
	and [j_Report].DateEdit <= @dateTimeEnd
	and [j_ListServiceRecords].id_Status in ( 14, 15, 19, 20)
	and [j_ListServiceRecords].Number is not null
	and [j_Report].typeCashNonCash = 0
	and ([j_ListServiceRecords].id_Department in (select ttt.id_Department from #tmp ttt) or @id_Block = 0))
	or
	(
			@isFartForward = 1 
		and 
			[j_ListServiceRecords].id_Status in (15)
		and 
			([j_ListServiceRecords].id_Department in (select ttt.id_Department from #tmp ttt) or @id_Block = 0)
		and 
			[j_Report].typeCashNonCash = 0
		and 
			[j_ListServiceRecords].TypeServiceRecordOnTime in (2,4)
	)
	
	--Безналичные
	union

		SELECT 
			[j_Report].StatusReport as StatusReport,
			[j_ListServiceRecords].id, 
			[j_ListServiceRecords].id_Status,
			[j_ListServiceRecords].Number, 
			[j_ListServiceRecords].Description,
			[j_ListServiceRecords].Summa, 
			[j_ListServiceRecords].Mix,
			[j_Report].id as id_report,
			case when (MONTH(jR.DateCreateReport) = MONTH(getdate()) and YEAR(jR.DateCreateReport) = YEAR(getdate())) or TypeServiceRecordOnTime = 1 then isnull(jP.sumGet, 0) -isnull(jP3.sumReturn, 0)
			else  isnull(jPLastMonth.sumGetLastMonth, 0) -isnull(jP3.sumReturn, 0) end  as sumGet,
			isnull([j_Report].SummaReport, 0) as SummaReport,
			isnull([j_Report].DebtReport, 0) as debtReport,
			[j_ListServiceRecords].Valuta, 
			[j_Report].DateEdit,
			case when [j_Report].typeCashNonCash = 0 then 'Нал.' else 'БезНал.' end as typeCashNonCash,
			[j_ListServiceRecords].id_Creator,
			[j_ListServiceRecords].inType,
			case 
				when [j_ListServiceRecords].id_Status = 14 then 'Ожидание отчета'
				when [j_Report].StatusReport = 0 then 'Отчет предоставлен'
				when [j_Report].StatusReport = 1 then 'Отчет подтвержден'
				when [j_Report].StatusReport = 2 then 'Отчет отклонен'
			end as nameStatusReport
			,id_Department
			,id_Block

	FROM [ServiceRecords].[j_ListServiceRecords] 
			JOIN [ServiceRecords].[j_Report] ON [j_Report].id_ServiceRecords = [j_ListServiceRecords].id 
			JOIN (SELECT jr.id from [ServiceRecords].[j_Report] jr
					join [ServiceRecords].[j_Report] jr2 on jr.id = jr2.id 
						where jr.DateCreateReport = (select top 1 DateCreateReport from [ServiceRecords].[j_Report] where id_ServiceRecords = jr2.id_ServiceRecords and typeCashNonCash = jr2.typeCashNonCash order by DateCreateReport desc)
			) jPDate on jPDate.id = [j_Report].id
			-- Сумма получения нал
			LEFT JOIN (select j_Payments.id_ServiceRecords, sum(isnull(j_Payments.Summa,0)) as sumGet, sum(isnull(j_Payments.SummaInValuta, 0)) as sumGetInValuta
			from [ServiceRecords].[j_Payments] j_Payments
			JOIN [ServiceRecords].[j_ListServiceRecords] jlr  on j_Payments.id_ServiceRecords = jlr.id
			where 
			j_Payments.DateAdd is not null
			and j_Payments.type = 1
			and j_Payments.typeCashNonCash = 1
			and (jlr.TypeServiceRecordOnTime = 1 or ((jlr.TypeServiceRecordOnTime = 2) and  MONTH(j_Payments.DateAdd) = MONTH(getdate()) and YEAR(j_Payments.DateAdd) = YEAR(getdate())))
			group by  j_Payments.id_ServiceRecords) jP  on jP.id_ServiceRecords = j_ListServiceRecords.id
			--Сумма получения за прошлый месяц
			LEFT JOIN (select j_Payments.id_ServiceRecords, sum(isnull(j_Payments.Summa,0)) as sumGetLastMonth, sum(isnull(j_Payments.SummaInValuta, 0)) as sumGetLastMonthInValuta 
			from [ServiceRecords].[j_Payments] j_Payments
			JOIN [ServiceRecords].[j_ListServiceRecords] jlr  on j_Payments.id_ServiceRecords = jlr.id
			where 
			j_Payments.DateAdd is not null
			and j_Payments.type = 1
			and j_Payments.typeCashNonCash = 1
			and MONTH(j_Payments.DateAdd) = (select MONTH(MAX(j_Payments.DateAdd)) 
											 from [ServiceRecords].[j_Payments] 
											 where j_Payments.id_ServiceRecords = jlr.id and  (MONTH(j_Payments.DateAdd) != MONTH(getdate()) or (MONTH(j_Payments.DateAdd) = MONTH(getdate()) and YEAR(j_Payments.DateAdd) != YEAR(getdate()))))
			and YEAR(j_Payments.DateAdd) = (select YEAR(MAX(j_Payments.DateAdd)) 
											 from [ServiceRecords].[j_Payments] 
											 where j_Payments.id_ServiceRecords = jlr.id and  (MONTH(j_Payments.DateAdd) != MONTH(getdate()) or (MONTH(j_Payments.DateAdd) = MONTH(getdate()) and YEAR(j_Payments.DateAdd) != YEAR(getdate()))))
			group by  j_Payments.id_ServiceRecords) jPLastMonth  on jPLastMonth.id_ServiceRecords = j_ListServiceRecords.id
			--Сумма возврата нал
			LEFT JOIN (select j_Payments.id_ServiceRecords, 
			sum(isnull(j_Payments.Summa,0)) as sumReturn, 
			sum(isnull(j_Payments.SummaInValuta, 0)) as sumReturnInValuta
			from [ServiceRecords].[j_Payments] j_Payments
			JOIN [ServiceRecords].[j_ListServiceRecords] jlr  on j_Payments.id_ServiceRecords = jlr.id
			where 
			j_Payments.DateProtect is not null
			and j_Payments.type = 2
			and j_Payments.typeCashNonCash = 1
			and (j_Payments.DateProtect > (select top 1 DateCreateReport from [ServiceRecords].[j_Report]
																 where id_ServiceRecords = jlr.id
																 order by DateCreateReport desc)
								 and j_Payments.DateProtect < getdate() or jlr.TypeServiceRecordOnTime = 1)
			group by  j_Payments.id_ServiceRecords) jP3  on jP3.id_ServiceRecords = j_ListServiceRecords.id
			
			--Сумма отчета
			LEFT JOIN (select id_ServiceRecords, sum(isnull(SummaReport, 0)) as sumReport, max(isnull(DateCreateReport, 0)) as DateCreateReport
			from [ServiceRecords].[j_Report]
			JOIN [ServiceRecords].[j_ListServiceRecords] jlr  on j_Report.id_ServiceRecords = jlr.id
			where [j_Report].typeCashNonCash = 1
			and MONTH(DateCreateReport) = (SELECT MONTH(MAX(DateCreateReport)) from [ServiceRecords].[j_Report] where id_ServiceRecords = jlr.id)
			and YEAR(DateCreateReport) = (SELECT YEAR(MAX(DateCreateReport)) from [ServiceRecords].[j_Report] where id_ServiceRecords = jlr.id)
			group by id_ServiceRecords
			) jR on jR.id_ServiceRecords = j_ListServiceRecords.id

	WHERE 
	(
	@isFartForward = 0 and 
	[j_Report].DateEdit >= @dateTimeStart
	and [j_Report].DateEdit <= @dateTimeEnd
	and [j_ListServiceRecords].id_Status in ( 14, 15, 19,20)
	and [j_ListServiceRecords].Number is not null
	and [j_Report].typeCashNonCash = 1
	and ([j_ListServiceRecords].id_Department in (select ttt.id_Department from #tmp ttt) or @id_Block = 0)
	)
	or	
	(
			@isFartForward = 1 
		and 
			[j_ListServiceRecords].id_Status in (15)
		and 
			([j_ListServiceRecords].id_Department in (select ttt.id_Department from #tmp ttt) or @id_Block = 0)
		and 
			[j_Report].typeCashNonCash = 1
		and 
			[j_ListServiceRecords].TypeServiceRecordOnTime in (2,4)
	)
	order by [j_ListServiceRecords].Number

	DROP TABLE #tmp

END


