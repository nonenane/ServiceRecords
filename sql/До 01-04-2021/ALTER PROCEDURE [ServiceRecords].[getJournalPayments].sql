USE [dbase1]
GO
/****** Object:  StoredProcedure [ServiceRecords].[getJournalPayments]    Script Date: 23.10.2020 17:08:48 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		SAA
-- Create date: 2019-03-15
-- Description: Получение журнала выплат/возвратов
-- =============================================
ALTER PROCEDURE [ServiceRecords].[getJournalPayments] 
	@dateTimeStart datetime,
	@dateTimeEnd datetime
	
AS
BEGIN

select @dateTimeEnd = DATEADD(day, 1, @dateTimeEnd);
	SET NOCOUNT ON;

		SELECT 
			p.id,
			p.DataSumma,
			r.Number,
			p.Summa,
			'RUB' as Valuta,
			l2.FIO AS Author,
			p.id_MoneyRecipient,
			l.FIO,
			r.Description,
			p.id_Creator,
			p.id_ServiceRecords,
			case when p.type = 1 then p.DateAdd else p.DateProtect end  as DateAdd,
			case when 
			r.bCashNonCash = 0 AND p.typeCashNonCash is null then 0
			when r.bCashNonCash = 1 AND p.typeCashNonCash is null then 1 
			when p.typeCashNonCash = 0 then 0 
			when p.typeCashNonCash = 1 then 1 
			end AS bCashNonCash,
			CASE 
			WHEN p.type = 1 AND r.bCashNonCash = 0 AND p.typeCashNonCash is null THEN 'Выдача Нал.'
			WHEN p.type = 1 AND r.bCashNonCash = 1 AND p.typeCashNonCash is null THEN 'Выдача Безнал.'
			WHEN p.type = 2 AND r.bCashNonCash = 0 AND p.typeCashNonCash is null THEN 'Возврат Нал.'
			WHEN p.type = 2 AND r.bCashNonCash = 1 AND p.typeCashNonCash is null THEN 'Возврат Безнал.'
			WHEN p.type = 1 AND p.typeCashNonCash = 0  THEN 'Выдача Нал.'
			WHEN p.type = 1 AND p.typeCashNonCash = 1  THEN 'Выдача Безнал.'
			WHEN p.type = 2 AND p.typeCashNonCash = 0  THEN 'Возврат Нал.'
			WHEN p.type = 2 AND p.typeCashNonCash = 1  THEN 'Возврат Безнал.'
			END as Type,
			p.type AS typeOperation,
			r.TypeServiceRecordOnTime,
			r.id_Department,
			r.id_Block

		FROM 
			ServiceRecords.j_Payments p
				INNER JOIN ServiceRecords.j_ListServiceRecords r on r.id = p.id_ServiceRecords
				INNER JOIN dbo.ListUsers l on l.id = p.id_MoneyRecipient
				LEFT JOIN dbo.ListUsers l2 on l2.id = p.id_Creator
		WHERE
			--p.id_Protect IS not NULL AND 
			(p.type = 1 and p.DateAdd >= @dateTimeStart and p.DateAdd <= @dateTimeEnd) or (p.type = 2 and p.DateProtect >= @dateTimeStart and p.DateProtect <= @dateTimeEnd)
END



