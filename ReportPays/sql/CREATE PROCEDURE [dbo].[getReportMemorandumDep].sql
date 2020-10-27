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
-- Description:	Получение списка статусо со ссылкой на настройки
-- =============================================
ALTER PROCEDURE [dbo].[getReportMemorandumDep] 	
	@dateStart date,
	@dateEnd date,
	@id_Object int,
	@id_dep_bonus int,
	@id_distrType int,
	@id_dep int,
	@id_doctype int,
	@listIdStatusDoc varchar(max)
AS
BEGIN
	

select 	
	mp.id_deps,
	cast(sum(mp.summa) as numeric(17,2)) as summa,
	--lda.id_DistrType,
	--p.id_status_doc,
	dddd.cname as nameDep,
	cast(0.0 as numeric(17,2)) as MinPayment,
	cast(0.0 as numeric(17,2)) as PercentPayment,
	cast(0.0 as numeric(17,2)) as resultBonus
from
	dbo.documents d 
		left join dbo.j_ListDocAttribute lda on lda.id_doc = d.id
		left join dbo.s_persons per on per.id = lda.id_FIOBonus
		left join dbo.posts_in_depart pid on pid.id = per.id_postdep
		inner join dbo.j_multi_penalty mp on mp.id_doc = d.id
		left join dbo.s_departments dddd on dddd.id = mp.id_deps

		left join dbo.j_movedoc md on md.id_doc = d.id
		left join dbo.points p on p.id = md.id_point
		--left join dbo.s_DepartmentBonus db on db.id_departments = mp.id_deps
where
		@dateStart<= cast(d.date_create as date) 
		and cast( d.date_create as date) <=@dateEnd 
		and md.id_line is null 
		and md.is_undo = 0
		and (@id_Object= 0 or lda.id_Objects = @id_Object)
		and (@id_dep_bonus = 0 or pid.id_depart = @id_dep_bonus)
		and (@id_distrType = 0 or lda.id_DistrType = @id_distrType)
		and (@id_dep = 0 or mp.id_deps = @id_dep)
		and (@id_doctype = 0 or d.id_doctype = @id_doctype)
		and (LEN(@listIdStatusDoc) = 0 or p.id_status_doc in (select value from dbo.StringToTable(@listIdStatusDoc,',')))
group by 
	mp.id_deps,dddd.cname



END