USE [DocumentsDZ]
GO
/****** Object:  StoredProcedure [dbo].[dmGetAddress]    Script Date: 20.10.2020 11:01:15 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Sporykhin G Y
-- Create date: 2020-10-20
-- Description:	Получение списка отделов для расчёта премий
-- =============================================
CREATE PROCEDURE [dbo].[getDepartmentBonus] 	
AS
BEGIN
	
select 
	db.id,
	db.id_departments,
	db.MinPayment,
	db.PercentPayment,
	ltrim(rtrim(d.cname)) as nameDep
from 
	dbo.s_DepartmentBonus db
		left join dbo.s_departments d on d.id = db.id_departments


END