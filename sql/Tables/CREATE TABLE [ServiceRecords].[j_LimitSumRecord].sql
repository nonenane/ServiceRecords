SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [ServiceRecords].[j_LimitSumRecord](
	[id_ListServiceRecords] [int] NOT NULL,
	[Summa] [numeric](16,2) NOT NULL,	
) ON [PRIMARY]
GO