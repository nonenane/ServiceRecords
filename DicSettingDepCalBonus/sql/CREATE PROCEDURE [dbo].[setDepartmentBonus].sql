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
-- Description:	Запись списка отделов для расчёта премий
-- =============================================
ALTER PROCEDURE [dbo].[setDepartmentBonus] 	
		 @id int
		,@id_deps int
        ,@MinPayment numeric(11,2)
        ,@PercentPayment numeric(11,2)
		,@id_user int
		,@isDel bit
AS
BEGIN

IF @isDel = 1 
	BEGIN

		DELETE FROM dbo.s_DepartmentBonus where id = @id

		select @id as id, '' as msg

		return;
	END

IF exists( select top(1) id from dbo.s_DepartmentBonus where id <> @id and id_departments = @id_deps)
	BEGIN
		select -1 as id,'В справочнике уже присутствует\nотдел с таким наименованием\n'as msg 
		return;
	END

IF not exists (select TOP(1) id from dbo.s_DepartmentBonus where id = @id)	
	BEGIN
		INSERT INTO [dbo].[s_DepartmentBonus]
			   ([id_departments]
			   ,[MinPayment]
			   ,[PercentPayment]
			   ,[id_Creator]
			   ,[DateCreate]
			   ,[id_Editor]
			   ,[DateEdit])
		 VALUES
			   (@id_deps
			   ,@MinPayment
			   ,@PercentPayment
			   ,@id_user
			   ,getdate()
			   ,@id_user
			   ,getdate())
		
		select cast(SCOPE_IDENTITY() as int ) as id, '' as msg

	END
ELSE
	BEGIN
		UPDATE 
			dbo.s_DepartmentBonus
		SET 
			MinPayment =@MinPayment,
			PercentPayment = @PercentPayment,
			DateEdit = GETDATE(),
			id_Editor = @id_user
		WHERE
			id = @id
		
		select @id as id , '' as msg

	END

END