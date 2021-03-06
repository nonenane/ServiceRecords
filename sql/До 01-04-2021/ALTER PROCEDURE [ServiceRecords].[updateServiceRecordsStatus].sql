USE [dbase1]
GO
/****** Object:  StoredProcedure [ServiceRecords].[updateServiceRecordsStatus]    Script Date: 25.12.2020 16:05:12 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		SPG
-- Create date: 2014-06-09
-- Description:	Получение Основной таблицы

-- Author:		SAA
-- Update date: 2019-02-11
-- Description:	Закомментиировано добавление номера к СЗ при статусе  7 ( Согласована предварительно с КД)

-- Editor:		Molotkova_IS
-- Edit date:	2020-01-31, 2020-02-17
-- Description:	Добавлен вывод ид и наименования статуса до и после, Добавлен сброс нумерации после НГ

-- Editor:		Khisamutdinova_RZ
-- Edit date:	2020-09-15
-- Description:	Исправлена ошибка записи в [ServiceRecords].[j_ListServiceRecords]
-- При статусе 5,11 - записываются ConfirmationD, DateConfirmationD, Number
-- При статусе 7 - записываются PreviosConfirmationD, DatePreviosConfirmationD
-- =============================================
ALTER PROCEDURE [ServiceRecords].[updateServiceRecordsStatus] --8246,5,934, ''

	@id int,
	@id_status int,
	@id_user int,
	@comment varchar(max) = ''
AS
BEGIN
	SET NOCOUNT ON;
	SET XACT_ABORT ON;

	DECLARE @number int = 0;
	declare @status_old int
	declare @name_old varchar(max)
	declare @typSZ int = 0
	declare @summa numeric(16,2)

	-- Molotkova_IS 17-02-2020
	DECLARE @dateComfD DATETIME
	--DECLARE @prevDateComfD DATETIME
	--
	
	select @status_old = id_Status,@typSZ  = TypeServiceRecord,@summa = Summa from [ServiceRecords].[j_ListServiceRecords] where id = @id
	select @name_old = cName from [ServiceRecords].[s_Status] where id = @status_old

	INSERT INTO [ServiceRecords].[j_ChangeStatusHistory]
	values(@id,@id_status,@id_user,GETDATE(),@comment)

	UPDATE 
		[ServiceRecords].[j_ListServiceRecords]
	set 
		id_Status = @id_status,id_Editor =@id_user,DateEdit = GETDATE(),DateStatusChange = GETDATE()
	WHERE 
		id =@id
    
	IF LEN(@comment)<>0
	BEGIN
		DECLARE @nameUser varchar(max) ='';
		select @nameUser =FIO from dbo.ListUsers where id = @id_user
		SET @comment = @nameUser+': '+ @comment + '\r'
		UPDATE 
			[ServiceRecords].[j_ListServiceRecords]
		SET 
			Comments = Comments+@comment
		WHERE
			id =@id
	END

	if(@id_status = 5) 
	BEGIN


		select @number = isnull(Max(Number),0)+1 from [ServiceRecords].[j_ListServiceRecords] where
		(DateConfirmationD is not null AND DateConfirmationD > DATEADD(yy, DATEDIFF(yy, 0, GETDATE()), 0)) 
		--OR (DatePreviosConfirmationD is not null AND DatePreviosConfirmationD > DATEADD(yy, DATEDIFF(yy, 0, GETDATE()), 0))
	
	-- Molotkova_IS 17-02-2020
		if(@number > 1)
		begin
			select @dateComfD = DateConfirmationD from [ServiceRecords].[j_ListServiceRecords] where Number = @number - 1
			--select @prevDateComfD = DatePreviosConfirmationD from [ServiceRecords].[j_ListServiceRecords] where Number = @number - 1

			if(@dateComfD is not null)
			begin
				if(YEAR(@dateComfD) < YEAR(GETDATE()))
					set @number = 1
			end
	--else if(@prevDateComfD is not null)
	--begin
	--if(YEAR(@prevDateComfD) < YEAR(GETDATE()))
	--	set @number = 1
	--end
		end
	--

		/*
		UPDATE 
			[ServiceRecords].[j_ListServiceRecords]
		SET 
			ConfirmationD = @id_user, DateConfirmationD = GETDATE()
		WHERE
			id =@id

		*/

		UPDATE 
			[ServiceRecords].[j_ListServiceRecords]
		SET 
			ConfirmationD = @id_user, DateConfirmationD = GETDATE(), Number = @number
		WHERE
			id =@id

		--UPDATE x
		----SET x.SubNumber = cast(x.id_ServiceRecords as varchar(max))+' - '+ cast(x.RN as varchar(max))
		--SET x.SubNumber = ISNULL(@number,'')+' - '+ cast(x.RN as varchar(max))
		--FROM (
		--	  SELECT SubNumber, ROW_NUMBER() OVER (ORDER BY DataSumma ASC) AS RN,id_ServiceRecords
		--	  FROM ServiceRecords.j_MultipleReceivingMoney
		--	  WHERE  id_ServiceRecords = @id
		--	  ) x
		exec [ServiceRecords].[updateSubNumberMultipleReceivingMoney] @id,@number
	END

	if(@id_status = 11) --7
	BEGIN
		
		
		select @number = isnull(Max(Number),0)+1 from [ServiceRecords].[j_ListServiceRecords] where
		(DateConfirmationD is not null AND DateConfirmationD > DATEADD(yy, DATEDIFF(yy, 0, GETDATE()), 0)) 
		--OR(DatePreviosConfirmationD is not null AND DatePreviosConfirmationD > DATEADD(yy, DATEDIFF(yy, 0, GETDATE()), 0))
		
	
	-- Molotkova_IS 17-02-2020
		if(@number > 1)
		begin
			select @dateComfD = DateConfirmationD from [ServiceRecords].[j_ListServiceRecords] where Number = @number - 1
			--select @prevDateComfD = DatePreviosConfirmationD from [ServiceRecords].[j_ListServiceRecords] where Number = @number - 1

			if(@dateComfD is not null)
			begin
				if(YEAR(@dateComfD) < YEAR(GETDATE()))
					set @number = 1
			end
		--else if(@prevDateComfD is not null)
		--begin
		--if(YEAR(@prevDateComfD) < YEAR(GETDATE()))
		--	set @number = 1
		--end
		end
		
		UPDATE 
			[ServiceRecords].[j_ListServiceRecords]
		SET 
			ConfirmationD = @id_user, DateConfirmationD = GETDATE(), Number = @number
		WHERE
			id =@id

		exec [ServiceRecords].[updateSubNumberMultipleReceivingMoney] @id,@number
		--UPDATE 
		--	[ServiceRecords].[j_ListServiceRecords]
		--SET 
		--	PreviosConfirmationD = @id_user, DatePreviosConfirmationD =GETDATE(), Number = @number
		--WHERE
		--	id =@id
	END

	--if(@id_status = 11)
	--BEGIN
	--	UPDATE 
	--		[ServiceRecords].[j_ListServiceRecords]
	--	SET 
	--		ConfirmationD = @id_user, DateConfirmationD = GETDATE()
	--	WHERE
	--		id =@id
	--END

	if(@id_status = 7) 
	BEGIN
		
		UPDATE 
			[ServiceRecords].[j_ListServiceRecords]
		SET 
			PreviosConfirmationD = @id_user, DatePreviosConfirmationD = GETDATE()
		WHERE
			id =@id

		if(@typSZ = 1)
			BEGIN
				IF not exists(select * from [ServiceRecords].[j_LimitSumRecord]  where id_ListServiceRecords = @id)
					INSERT INTO [ServiceRecords].[j_LimitSumRecord] (id_ListServiceRecords,Summa)
					values (@id,@summa)

			END
	END

	select @status_old as id_prev, @name_old as cName_prev, id, cName from [ServiceRecords].[s_Status] where id = @id_status

END
