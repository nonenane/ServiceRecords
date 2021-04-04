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
-- Description:	Редактирование штрафа и добавление к СЗ
-- =============================================
ALTER PROCEDURE [ServiceRecords].[setMemorandums]
		@id int,
		@id_ListServiceRecords int = null,
		@SumBonus numeric(7,2),
		@isEdit bit,
		@id_user int,
		@isDel bit,
		@isUpdateData bit

AS
BEGIN
	SET NOCOUNT ON;

	IF @isDel = 0
		BEGIN
		
			IF @isUpdateData = 1
				BEGIN
					UPDATE 
						ServiceRecords.j_Memorandums	
					SET
						SumBonus = @SumBonus,
						isEdit = @isEdit,
						DateEdit = GETDATE(),
						id_Editor = @id_user,
						id_ListServiceRecords = @id_ListServiceRecords
					where
						id = @id 
				END
			ELSE
				BEGIN
					UPDATE 
						ServiceRecords.j_Memorandums	
					SET
						id_ListServiceRecords = @id_ListServiceRecords
					WHERE
						id= @id
				END
		END
	ELSE
		BEGIN
			UPDATE ServiceRecords.j_Memorandums set id_ListServiceRecords = null where id_ListServiceRecords = @id_ListServiceRecords
		END
	

END
