CREATE TABLE [dbo].[Permissions] (
    [PermissionID]   INT          NOT NULL,
    [PermissionName] VARCHAR (50) NOT NULL,
    CONSTRAINT [PK_Permissions] PRIMARY KEY CLUSTERED ([PermissionID] ASC)
);

