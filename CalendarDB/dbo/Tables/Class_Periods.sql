CREATE TABLE [dbo].[Class_Periods]
(
	[ClassId] UNIQUEIDENTIFIER NOT NULL,
    [Period] INT NOT NULL,
    CONSTRAINT [PK_Class_Periods] PRIMARY KEY CLUSTERED (ClassId, [Period]),
    CONSTRAINT [FK_Class_Periods_ToClasses] FOREIGN KEY (ClassId) REFERENCES Classes(Id)
)