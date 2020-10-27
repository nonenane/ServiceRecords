USE [dbase1]
GO
/****** Object:  StoredProcedure [ServiceRecords].[SetSettings]    Script Date: 08.09.2020 10:32:42 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:		Sporykhin G Y
-- Create date: 2020-09-08
-- Description:	Получение списка оборудования по СЗ
-- =============================================
ALTER PROCEDURE [ServiceRecords].[getListHardwareForServiceRecord]
			@id_ServiceRecord int 
AS
BEGIN
	SET NOCOUNT ON;

select 	
	s.id,
	--cast(s.id as varchar(1024))+' № '+isnull(cast(s.Number as varchar(1024)),'')+isnull(' от '+convert(varchar(1024),s.DateConfirmationD,104),'') as number,
	'№ '+isnull(cast(s.Number as varchar(1024)),'')+isnull(' от '+convert(varchar(1024),s.DateConfirmationD,104),'') as number,
	h.InventoryNumber,
	h.EAN,
	h.cName,
	case 
		when h.TypeComponentsHardware = 0 then 'Оборудование'
		when h.TypeComponentsHardware = 1 then 'Комплектующие'
	end as nameType,
	isnull(ltrim(rtrim(l.cName)),'') as nameLoc,
	isnull(k.lastname+' ','')+isnull(k.firstname+' ','')+isnull(k.secondname,'') as fio,
	case 
		when h.Status = 1 then  'Поставлено на баланс МОЛ' 
		when h.Status = 2 then  'Списано и утилизировано' 
		when h.Status = 3 then  'Поставлено на баланс ЗИП' 
		when h.Status = 4 then  'Снято с баланса' 
	end as nameStatus
from 
	ServiceRecords.j_ListServiceRecords s
	inner join hardware.j_Hardware h on h.id_ListServiceRecords = s.id
	left join hardware.s_Location l on l.id = h.id_Location	
	left join hardware.s_Responsible r on r.id = h.id_Responsible
	left join dbo.s_kadr k on k.id = r.id_Kadr
where
	s.id = @id_ServiceRecord
order by
	h.InventoryNumber asc

END
