CREATE TABLE [dbo].[Category] (
    [CategoryID]   INT          IDENTITY (1, 1) NOT NULL,
    [CategoryName] VARCHAR (50) NOT NULL,
    CONSTRAINT [PK_Category] PRIMARY KEY CLUSTERED ([CategoryID] ASC)
);



