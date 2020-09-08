USE [dbase1]
GO
/****** Object:  StoredProcedure [ServiceRecords].[SetSettings]    Script Date: 08.09.2020 10:32:42 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:		SPG
-- Create date: 2017-11-10
-- Description:	Сохранение настроек в prog_config
-- =============================================
ALTER PROCEDURE [ServiceRecords].[SetSettingsMulti]
		@id_prog int,
		@id_value char(4),
		@type_value char(2),
		@value_name varchar(32),
		@value varchar(64),
		@comment varchar(64) = '',
		@isDel bit

AS
BEGIN
	SET NOCOUNT ON;


	IF @isDel = 1 
		BEGIN
			DELETE from dbo.prog_config 
			where [id_prog] = @id_prog
			and [id_value] = @id_value		 
			and value = @value

			return;
		END


	if NOT exists 
		(select * from dbo.prog_config 
		 where [id_prog] = @id_prog
		 and [id_value] = @id_value		 
		 and value = @value
		)
	begin
			
		INSERT INTO [dbo].[prog_config]
				   ([id_prog]
				   ,[id_value]
				   ,[type_value]
				   ,[value_name]
				   ,[value]
				   ,[comment])
			 VALUES
				   (@id_prog
				   ,@id_value
				   ,@type_value
				   ,@value_name
				   ,@value
				   ,@comment)

	end

END
