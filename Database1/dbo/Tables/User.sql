CREATE TABLE [dbo].[User] (
    [UserID]   INT           NOT NULL,
    [Username] VARCHAR (50)  NOT NULL,
    [Email]    VARCHAR (100) NOT NULL,
    [Password] NCHAR (50)    NOT NULL,
    CONSTRAINT [PK_User] PRIMARY KEY CLUSTERED ([UserID] ASC)
);

