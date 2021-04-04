CREATE TABLE [ServiceRecords].[s_TypicalWorks](
	[id]			int				IDENTITY(1,1) NOT NULL,
	[cName]			varchar(1024)	not null,
	[isActive]		bit				not null default 1
 CONSTRAINT [PK_s_TypicalWorks] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = ON, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO


INSERT INTO [ServiceRecords].[s_TypicalWorks] (cName,isActive)
values ('Закупка компьютерного оборудования',1),('Другое',1)



--NEW 2020-10-15
ALTER TABLE ServiceRecords.s_TypicalWorks ADD isBonus bit not null default 0


INSERT INTO ServiceRecords.s_TypicalWorks (cName,isActive,isBonus)
	VALUES ('Премии по ДЗ',1,1)