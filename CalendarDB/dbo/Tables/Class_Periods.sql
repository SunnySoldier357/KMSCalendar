CREATE TABLE [dbo].[Class_Periods]
(
	[ClassId] UNIQUEIDENTIFIER NOT NULL,
    [Period] INT NOT NULL,
    CONSTRAINT [FK_Class_Periods_ToClasses] FOREIGN KEY (ClassId) REFERENCES Classes(Id)
)