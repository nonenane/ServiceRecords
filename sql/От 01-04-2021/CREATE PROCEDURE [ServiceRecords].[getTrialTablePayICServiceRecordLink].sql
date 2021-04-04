USE [dbase1]
GO
/****** Object:  StoredProcedure [ServiceRecords].[SetSettings]    Script Date: 08.09.2020 10:32:42 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:		Sporykhin G Y
-- Create date: 2021-04-01
-- Description:	Получение данных по ИС по СЗ на ДС
-- =============================================
ALTER PROCEDURE [ServiceRecords].[getTrialTablePayICServiceRecordLink]
			@id_serviceRecord int
AS
BEGIN
	SET NOCOUNT ON;


select
	a.id,
	a.FIO,
	a.namePost,
	a.Salary as OldSalary,
	a.MinSalary,
	a.MaxSalary,
	a.normaDays,
	a.Salary as nowSalary,
	hourWorkOnDay,
	a.WorkedHours,
	a.TimeIn,
	a.id_Kadr,
	a.periodPay,
	ROUND((a.Salary/(a.normaDays*a.hourWorkOnDay))*a.WorkedHours,2)  as Payment,	
	a.WorkedDays,
	0.0 as minuteWork
from (
select 
	t.id,
	isnull(trim(k.lastname)+' ','')+isnull(substring(trim(k.firstname),1,1)+'.','')+isnull(substring(trim(k.secondname),1,1)+'.','') as FIO,
	p.cName as namePost,
	t.Salary,
	i.TimeIn,
	t.WorkedHours,
	t.WorkedDays,
	t.Payment,
	case 		
		when WorkMode.id = 1 then [WorkTime].[f_GetNormaDays](dateadd(day,1,(EOMONTH(i.TimeIn,-1))), EOMONTH(i.TimeIn))  
		when WorkMode.id = 2 then datediff(day, dateadd(day,1,(EOMONTH(i.TimeIn,-1))), EOMONTH(i.TimeIn)) + 1
	end as normaDays,
	trialSal.MinSalary,
	trialSal.MaxSalary,
	'' as periodPay,
	round(datediff(minute, workGraph.TimeStart,workGraph.TimeStop)/60.00,2) as hourWorkOnDay,
	t.id_Kadr

from 
	WorkTime.j_TrialTabel t
	left join dbo.s_kadr k  on k.id = t.id_Kadr
	left join WorkTime.j_InOutTime i on i.id = t.id_InOutTime
	LEFT JOIN dbo.s_Posts p on p.id = k.id_posts	
	LEFT JOIN Personnel.s_WorkGraphMode WorkGraphMode on WorkGraphMode.id_kadr = t.id_Kadr
	LEFT jOIN Personnel.s_WorkMode WorkMode on WorkMode.id = WorkGraphMode.id_WorkMode
	LEFT JOIN Personnel.s_WorkGraph workGraph on  workGraph.id = WorkGraphMode.id_WorkGraph
	LEFT JOIN dbo.Departments_vs_Posts depVsPost on depVsPost.id_Departments = k.id_departments and depVsPost.id_Posts = k.id_posts
	LEFT JOIN WorkTime.s_TrialSalary trialSal on trialSal.id_Departments_vs_Posts = depVsPost.id

where 
	t.id_tTrialTabel  in (select id from WorkTime.j_tTrialTabel where id_ListServiceRecords = @id_serviceRecord)) as a

END
