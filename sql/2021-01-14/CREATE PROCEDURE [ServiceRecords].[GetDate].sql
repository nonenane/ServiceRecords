CREATE PROCEDURE [ServiceRecords].[GetDate] 
	
AS
BEGIN
	SET NOCOUNT ON;

select GETDATE() as nowDate

END