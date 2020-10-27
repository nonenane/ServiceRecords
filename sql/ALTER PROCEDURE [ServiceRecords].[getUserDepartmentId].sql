USE [dbase1]
GO
/****** Object:  StoredProcedure [ServiceRecords].[getUserDepartmentId]    Script Date: 19.10.2020 16:19:32 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Alina Soldatkina>
-- Create date: <30.08.2018>
-- Description:	<Получение названия департамента пользователя>
-- =============================================
ALTER PROCEDURE [ServiceRecords].[getUserDepartmentId] 
	@idUser int

AS
BEGIN
	SET NOCOUNT ON;
	
	--SELECT rk.id_Departments FROM [dbo].[ListUsers] rk  WHERE id_Access = @idUser;
	select @idUser as id_Departments

END
