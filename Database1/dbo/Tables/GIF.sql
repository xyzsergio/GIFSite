CREATE TABLE [dbo].[GIF] (
    [GIFID]       INT           IDENTITY (1, 1) NOT NULL,
    [Title]       VARCHAR (50)  NOT NULL,
    [Description] VARCHAR (500) NULL,
    [UploadDate]  DATETIME      NULL,
    [Link]        VARCHAR (500) NOT NULL,
    [CategoryID]  INT           NOT NULL,
    [UserID]      INT           NOT NULL,
    CONSTRAINT [PK_GIF] PRIMARY KEY CLUSTERED ([GIFID] ASC),
    CONSTRAINT [FK_GIF_User] FOREIGN KEY ([UserID]) REFERENCES [dbo].[User] ([UserID])
);















