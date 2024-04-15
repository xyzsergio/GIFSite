CREATE TABLE [dbo].[GIF] (
    [GIFID]       INT           IDENTITY (1, 1) NOT NULL,
    [Title]       VARCHAR (50)  NOT NULL,
    [Description] VARCHAR (500) NULL,
    [UploadDate]  DATETIME      NULL,
    [FilePath]    VARCHAR (100) NOT NULL,
    CONSTRAINT [PK_GIF] PRIMARY KEY CLUSTERED ([GIFID] ASC)
);





