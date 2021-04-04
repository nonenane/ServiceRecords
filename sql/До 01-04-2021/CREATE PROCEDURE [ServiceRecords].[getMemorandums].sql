USE [dbase1]
GO
/****** Object:  StoredProcedure [ServiceRecords].[SetSettings]    Script Date: 08.09.2020 10:32:42 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:		Sporykhin G Y
-- Create date: 2020-10-15
-- Description:	Получение списка штрафов
-- =============================================
ALTER PROCEDURE [ServiceRecords].[getMemorandums]
	@dateStart date,
	@dateEnd date,
	@id_ListServiceRecords int  = null,
	@isAll bit = 1
AS
BEGIN
	SET NOCOUNT ON;

	

select 
	 m.id
	--,cast( case when m.id_ListServiceRecords is not null then 1 else 0 end as bit) as isSelect 
	,cast( 0 as bit) as isSelect 
	,m.no_doc
	,m.date_create
	,m.depPenalty
	,m.cname
	,m.DistrType
	,m.sumPenalty
	,m.SumBonus
	,m.FIOBonus
	,m.isEdit
	,m.id_ListServiceRecords
	,m.id_doc
from 
	ServiceRecords.j_Memorandums m
where
	(@isAll = 1 and @dateStart<=cast(m.date_create as date) and cast(m.date_create as date)<=@dateEnd and (@id_ListServiceRecords=m.id_ListServiceRecords or m.id_ListServiceRecords is null))
	or
	(@isAll = 0 and m.id_ListServiceRecords = @id_ListServiceRecords)

END
