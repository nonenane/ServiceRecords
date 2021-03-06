USE [dbase1]
GO
/****** Object:  StoredProcedure [ServiceRecords].[getPayment]    Script Date: 29.01.2021 10:58:48 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		SPG
-- Create date: 2014-06-09
-- Description:	Получение Основной таблицы

-- Author: SAA
-- Update date: 2018-09-06
-- Description: Изменен запрос. Вместо id_Creator берется id_MoneyRecipient
--				Добавлено условие p.id_MoneyRecipient = @id_user
--				Добавлено условие на проверку @id_user
-- Author: SAA
-- Update date: 2018-10-01
-- Description: Добавлена выдача p.type для режима "Руководитель"
-- Author: SAA
-- Update date: 2019-06-18
-- Description: Добавлена проверка на столбец CashNonCash
-- =============================================
ALTER PROCEDURE [ServiceRecords].[getPayment] --934
	@id_user int 
	
AS
BEGIN
	SET NOCOUNT ON;

	IF @id_user <> -1
	BEGIN 
		SELECT 
		p.id,
		p.Summa,
		r.Number,
		p.id_MoneyRecipient,
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
		l.FIO,
		r.bCashNonCash,
		p.typeCashNonCash, 
		r.Description,
		p.DataSumma,
		p.id_ServiceRecords,
		r.Summa as maxSumma,
		r.TypeServiceRecordOnTime

	FROM 
		ServiceRecords.j_Payments p
			INNER JOIN ServiceRecords.j_ListServiceRecords r on r.id = p.id_ServiceRecords
			INNER JOIN dbo.ListUsers l on l.id = p.id_Creator
		WHERE ((p.id_MoneyRecipient = @id_user) AND ( p.id_Add is NULL OR cast(p.DataSumma as date) = cast(GETDATE() as date)))

	END
	ELSE
	BEGIN
		SELECT 
			p.id,
			p.Summa,
			CASE 
			WHEN p.type = 1 AND r.bCashNonCash = 0 AND p.typeCashNonCash is null THEN 'Выдача Нал.'
			WHEN p.type = 1 AND r.bCashNonCash = 1 AND p.typeCashNonCash is null THEN 'Выдача Безнал.'
			WHEN p.type = 2 AND r.bCashNonCash = 0 AND p.typeCashNonCash is null THEN 'Возврат Нал.'
			WHEN p.type = 2 AND r.bCashNonCash = 1 AND p.typeCashNonCash is null THEN 'Возврат Безнал.'
			WHEN p.type = 1 AND p.typeCashNonCash = 0  THEN 'Выдача Нал.'
			WHEN p.type = 1 AND p.typeCashNonCash = 1  THEN 'Выдача Безнал.'
			WHEN p.type = 2 AND p.typeCashNonCash = 0  THEN 'Возврат Нал.'
			WHEN p.type = 2 AND p.typeCashNonCash = 1  THEN 'Возврат Безнал.'
			END as nameType
			,r.Number
			,p.id_MoneyRecipient,
			p.type,
			l.FIO,
			r.bCashNonCash,
			p.typeCashNonCash, 
			r.Description,
			p.DataSumma,
			p.id_Creator,
			p.id_ServiceRecords,
			r.TypeServiceRecordOnTime
		FROM 
			ServiceRecords.j_Payments p
				INNER JOIN ServiceRecords.j_ListServiceRecords r on r.id = p.id_ServiceRecords
				INNER JOIN dbo.ListUsers l on l.id = p.id_MoneyRecipient
		WHERE
			(( p.id_Add is NULL) OR cast(p.DataSumma as date) = cast(GETDATE() as date))
			AND ((@id_user = -1 AND ((p.id_Protect IS NULL AND type = 2) OR (p.id_Add IS NULL AND type = 1))))
	END
END
