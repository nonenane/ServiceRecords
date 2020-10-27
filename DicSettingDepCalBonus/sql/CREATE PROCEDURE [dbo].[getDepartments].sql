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
-- Description:	Получение списка отделов 
-- =============================================
CREATE PROCEDURE [dbo].[getDepartments] 	
AS
BEGIN
	

select 
	d.id,
	ltrim(rtrim(d.cname)) as cName
from 
	dbo.s_departments  d
where 
	IsActive = 1
order by cName asc


END