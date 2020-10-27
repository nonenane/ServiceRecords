USE [dbase1]
GO
/****** Object:  StoredProcedure [ServiceRecords].[getUserDepartment]    Script Date: 19.10.2020 16:17:08 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Alina Soldatkina>
-- Create date: <30.08.2018>
-- Description:	<Получение названия департамента пользователя>
-- =============================================
ALTER PROCEDURE [ServiceRecords].[getUserDepartment] 
	@idUser int

AS
BEGIN
	SET NOCOUNT ON;
	
	SELECT rd.name FROM [dbo].[departments] rd WHERE rd.id = @idUser-- (SELECT rk.id_Departments FROM [dbo].[ListUsers] rk  WHERE id_Access = @idUser) 

END
