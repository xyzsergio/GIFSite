CREATE TABLE [dbo].[Permissions] (
    [PermissionID]   INT          IDENTITY (1, 1) NOT NULL,
    [PermissionName] VARCHAR (50) NOT NULL,
    CONSTRAINT [PK_Permissions] PRIMARY KEY CLUSTERED ([PermissionID] ASC)
);



