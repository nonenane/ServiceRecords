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
-- Description:	Обновление данных по человеку в ИС
-- =============================================
CREATE PROCEDURE [ServiceRecords].[setTrialTablePayICServiceRecordLink]
			@id_serviceRecord int,
			@idTrialTabel int,
			@Payment numeric(10,2),
			@Salary numeric(10,2),
			@id_kadr int,
			@id_user int,
			@isDel bit
AS
BEGIN
	SET NOCOUNT ON;
IF @isDel = 0
	BEGIN
		UPDATE 
			WorkTime.j_TrialTabel 
		SET 
			Payment = @Payment,
			Salary = @Salary,
			id_Editor = @id_user, 
			DateEdit = GETDATE() 
		WHERE 
			id = @idTrialTabel
	END
ELSE
	BEGIN
		DELETE FROM WorkTime.j_TrialTabel where id_Kadr = @id_kadr and id_tTrialTabel  in (select id from WorkTime.j_tTrialTabel where id_ListServiceRecords = @id_serviceRecord)		
	END
END
