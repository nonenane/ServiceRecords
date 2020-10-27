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
CREATE PROCEDURE [dbo].[getStatusDocVsProgConfig] 	
	@id_prog int
AS
BEGIN
	

select
	cast(0 as bit) as isSelect,
	s.id,
	ltrim(rtrim(s.cname)) as cName
from 
	dbo.s_status_doc s
where 
	s.id in (select value from dbo.prog_config where id_value = 'tdop' and id_prog = @id_prog)


END