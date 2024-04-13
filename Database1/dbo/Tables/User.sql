CREATE TABLE [dbo].[User] (
    [UserID]        INT           IDENTITY (1, 1) NOT NULL,
    [Username]      VARCHAR (50)  NOT NULL,
    [Email]         VARCHAR (100) NOT NULL,
    [Password]      VARCHAR (100) NOT NULL,
    [LastLoginTime] DATETIME      NOT NULL,
    [AdminID]       INT           NOT NULL,
    CONSTRAINT [PK_User] PRIMARY KEY CLUSTERED ([UserID] ASC)
);









