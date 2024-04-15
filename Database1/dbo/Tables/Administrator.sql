CREATE TABLE [dbo].[Administrator] (
    [AdminID]      INT IDENTITY (1, 1) NOT NULL,
    [UserID]       INT NULL,
    [PermissionID] INT NULL,
    CONSTRAINT [PK_Administrator] PRIMARY KEY CLUSTERED ([AdminID] ASC),
    CONSTRAINT [FK_Administrator_Permissions] FOREIGN KEY ([PermissionID]) REFERENCES [dbo].[Permissions] ([PermissionID]),
    CONSTRAINT [FK_Administrator_User] FOREIGN KEY ([UserID]) REFERENCES [dbo].[User] ([UserID])
);





