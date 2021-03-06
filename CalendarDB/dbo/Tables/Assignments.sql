﻿CREATE TABLE [dbo].[Assignments]
(
	[Id] UNIQUEIDENTIFIER NOT NULL,
	[DueDate] DATETIME2(7) NOT NULL,
	[Description] NVARCHAR(MAX) NULL,
	[Name] NVARCHAR(MAX) NULL,
	[ClassId] UNIQUEIDENTIFIER NULL,
	[UserId] UNIQUEIDENTIFIER NULL,
	[Period] INT NULL,
	CONSTRAINT [PK_Assignments] PRIMARY KEY CLUSTERED (Id ASC),
	CONSTRAINT [FK_Assignments_ToClasses] FOREIGN KEY (ClassId) REFERENCES Classes (Id),
    CONSTRAINT [FK_Assignments_ToUsers] FOREIGN KEY (UserId) REFERENCES Users(Id)
)