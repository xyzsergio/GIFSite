CREATE TABLE [dbo].[User] (
    [UserID]   INT           IDENTITY (1, 1) NOT NULL,
    [Username] VARCHAR (50)  NOT NULL,
    [Email]    VARCHAR (100) NOT NULL,
    [Password] VARCHAR (100) NOT NULL,
    [LastLoginTime] DATETIME NULL, 
    CONSTRAINT [PK_User] PRIMARY KEY CLUSTERED ([UserID] ASC)
);





