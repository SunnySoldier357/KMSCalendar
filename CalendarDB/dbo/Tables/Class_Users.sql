CREATE TABLE [dbo].[Class_Users]
(
	[ClassId] UNIQUEIDENTIFIER NOT NULL,
    [UserId] UNIQUEIDENTIFIER NOT NULL,
    [Period] INT NOT NULL,
    CONSTRAINT [PK_Class_Users] PRIMARY KEY CLUSTERED (ClassId, UserId, [Period]),
    CONSTRAINT [FK_Class_Users_ToClasses] FOREIGN KEY (ClassId) REFERENCES Classes(Id),
    CONSTRAINT [FK_Class_Users_ToUsers] FOREIGN KEY (UserId) REFERENCES Users(Id)
)