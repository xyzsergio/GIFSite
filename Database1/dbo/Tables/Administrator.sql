CREATE TABLE [dbo].[Administrator] (
    [AdminID]      INT NOT NULL,
    [UserID]       INT NOT NULL,
    [PermissionID] INT NOT NULL,
    CONSTRAINT [PK_Administrator] PRIMARY KEY CLUSTERED ([AdminID] ASC),
    CONSTRAINT [FK_Administrator_Permissions] FOREIGN KEY ([PermissionID]) REFERENCES [dbo].[Permissions] ([PermissionID]),
    CONSTRAINT [FK_Administrator_User] FOREIGN KEY ([UserID]) REFERENCES [dbo].[User] ([UserID])
);

