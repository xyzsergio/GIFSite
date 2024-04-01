CREATE TABLE [dbo].[Like] (
    [LikeID]   INT      IDENTITY (1, 1) NOT NULL,
    [UserID]   INT      NOT NULL,
    [GIFID]    INT      NOT NULL,
    [LikeDate] DATETIME NOT NULL,
    CONSTRAINT [PK_Like] PRIMARY KEY CLUSTERED ([LikeID] ASC),
    CONSTRAINT [FK_Like_GIF] FOREIGN KEY ([GIFID]) REFERENCES [dbo].[GIF] ([GIFID]),
    CONSTRAINT [FK_Like_User] FOREIGN KEY ([UserID]) REFERENCES [dbo].[User] ([UserID])
);



