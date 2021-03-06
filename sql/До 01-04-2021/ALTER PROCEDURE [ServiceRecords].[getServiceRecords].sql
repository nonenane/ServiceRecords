USE [dbase1]
GO
/****** Object:  StoredProcedure [ServiceRecords].[getServiceRecords]    Script Date: 08.09.2020 16:57:05 ******/
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
-- Description:	добавлен вывод id объекта
-- Author:		SAA
-- Update date: 2019-03-06
-- Description:	добавлен вывод типа СЗ
-- Author:		SAA
-- Update date: 2019-04-15
-- Description:	Изменен вывод статуса

-- Editor:		Molotkova_IS
-- Edit date:	2020-01-30
-- Description:	Добавлен вывод наименования отдела
-- =============================================
ALTER PROCEDURE [ServiceRecords].[getServiceRecords]
	@dateStart date, 
	@dateEnd date,
	@isReport bit
	
AS
BEGIN
	SET NOCOUNT ON;
	SET XACT_ABORT ON;

	set @isReport = 0;


	DECLARE @table table (id_ServiceRecords int)
	DECLARE @tableScane table (id_ServiceRecords int)
	DECLARE @tablePayment table (id_ServiceRecords int,DataSummaPay datetime)
	DECLARE @tableScaneCloseNote table (id_ServiceRecords int)

	INSERT INTO @table
	select id_ServiceRecords from [ServiceRecords].[j_Payments] where (id_Protect IS NULL AND type = 2) OR (id_Add IS NULL AND type = 1) GROUP BY id_ServiceRecords

	INSERT INTO @tableScane
	select id_ServiceRecords from ServiceRecords.j_Scan GROUP BY id_ServiceRecords

	INSERT INTO @tableScaneCloseNote
	select id_ServiceRecords from ServiceRecords.j_Scan where TypeScan = 3 GROUP BY id_ServiceRecords

	INSERT INTO @tablePayment
	select id_ServiceRecords,max(DataSumma) as DataSumma  from [ServiceRecords].[j_Payments] where  (id_Add IS NOT NULL AND type = 1) GROUP BY id_ServiceRecords


	select id_ServiceRecords INTO #tmpFondLink from ServiceRecords.j_Fond group by id_ServiceRecords

	declare @nowDate date = dateadd(month,-1,getdate())

		select id_ServiceRecords into #tmpReportLink from(
		select id_ServiceRecords,max(DateCreateReport) as DateCreateReport from ServiceRecords.j_Report group by id_ServiceRecords) as a
		where YEAR(@nowDate)=YEAR(a.DateCreateReport) and month(@nowDate)=month(a.DateCreateReport)



	SELECT
		r.id,
		r.DateCreate,
		r.Number,
		ltrim(rtrim(db.name)) +'/'+
		ltrim(rtrim(d.name)) as nameBlock,
		r.Description,
		r.Summa,
		--CASE WHEN r.bDataSumma = 1 THEN  '=' ELSE '' END as typeSumma,
		--CASE 
		--	WHEN r.id_Status = 13 OR sPay.id_ServiceRecords is null THEN  s.cName 
		--	WHEN r.id_Status<>13 AND sPay.id_ServiceRecords is NOT null AND sClosedScane.id_ServiceRecords is NULL THEN  'Ожидание отчета'
		--	WHEN r.id_Status<>13 AND sPay.id_ServiceRecords is NOT null AND sClosedScane.id_ServiceRecords is NOT NULL THEN  'Отчет предоставлен'
		--END   as nameStatus,
		s.cName  as nameStatus,
		r.DateStatusChange,
		l.FIO,
		r.id_Block,
		r.id_Department,
		ltrim(rtrim(d.name)) as nameDep,
		r.id_Status as id_Status,
		--CASE 
		--	WHEN r.id_Status = 13 OR sPay.id_ServiceRecords is null THEN  r.id_Status 
		--	WHEN r.id_Status<>13 AND sPay.id_ServiceRecords is NOT null AND sClosedScane.id_ServiceRecords is NULL THEN  14
		--	WHEN r.id_Status<>13 AND sPay.id_ServiceRecords is NOT null AND sClosedScane.id_ServiceRecords is NOT NULL THEN  15
		--END   as id_Status,
		CAST(CASE WHEN tPay.id_ServiceRecords is null THEN 1 ELSE 0 END as bit) as AddPayment,
		r.TypeServiceRecord,
		r.DataSumma,
		CAST(CASE WHEN tScane.id_ServiceRecords is not null THEN 1 ELSE 0 END as bit) as isScane,
		CAST(CASE WHEN sPay.id_ServiceRecords is not null THEN 1 ELSE 0 END as bit) as isAddReportMoney,
		CAST(CASE WHEN sClosedScane.id_ServiceRecords is not null THEN 1 ELSE 0 END as bit) as isClosedDoc,
		r.id_Creator,
		sPay.DataSummaPay,
		r.id_Object,
		r.TypeServiceRecordOnTime,
		r.Valuta,
		r.SummaCash,
		r.SummaNonCash,
		r.Mix,
		isnull(debt.DebtReport, 0) as DebtReport,
		fl.id_ServiceRecords,
		r.inType,
		cast(case when rl.id_ServiceRecords is not null then 1 else 0 end as bit) as isReportPreMonth
		,r.bCashNonCash				
	FROM
		[ServiceRecords].[j_ListServiceRecords] r
			LEFT JOIN dbo.departments db on db.id = r.id_Block
			LEFT JOIN dbo.departments d on d.id = r.id_Department
			LEFT JOIN ServiceRecords.s_Status s on s.id = r.id_Status
			LEFT JOIN dbo.ListUsers l on l.id = r.id_Editor
			LEFT JOIN @table tPay on tPay.id_ServiceRecords = r.id
			LEFT JOIN @tableScane tScane on tScane.id_ServiceRecords = r.id
			LEFT JOIN @tablePayment sPay on sPay.id_ServiceRecords = r.id
			LEFT JOIN @tableScaneCloseNote sClosedScane on sClosedScane.id_ServiceRecords = r.id
			lEFT JOIN (select j_Report1.id_ServiceRecords, sum(j_Report1.DebtReport) as DebtReport
						FROM ServiceRecords.j_Report j_Report1
						JOIN ServiceRecords.j_Report j_Report2 on j_Report1.id = j_Report2.id
						where j_Report1.DateCreateReport = (select top 1 DateCreateReport  
															from ServiceRecords.j_Report 
															where id_ServiceRecords = j_Report2.id_ServiceRecords 
															order by DateCreateReport desc)
						group by j_Report1.id_ServiceRecords
			) as debt on debt.id_ServiceRecords = r.id
			left join #tmpFondLink fl on fl.id_ServiceRecords = r.id
			left join #tmpReportLink rl on rl.id_ServiceRecords = r.id			

	WHERE
		(@isReport = 0 and @dateStart<=cast(r.CreateServiceRecord as date) AND cast(r.CreateServiceRecord as date)<=@dateEnd)
		or
		(@isReport = 1  and rl.id_ServiceRecords is not null and r.id_Status in (14,19))


	DROP TABLE #tmpFondLink,#tmpReportLink

END




