CREATE TABLE [dbo].[GIF] (
    [GIFID]       INT           NOT NULL,
    [Title]       VARCHAR (50)  NOT NULL,
    [Discription] VARCHAR (500) NULL,
    [UploadDate]  DATETIME      NULL,
    [FilePath]    VARCHAR (100) NOT NULL,
    CONSTRAINT [PK_GIF] PRIMARY KEY CLUSTERED ([GIFID] ASC)
);

