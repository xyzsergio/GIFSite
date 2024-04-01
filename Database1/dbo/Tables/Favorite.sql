CREATE TABLE [dbo].[Favorite] (
    [FavoriteID]    INT      IDENTITY (1, 1) NOT NULL,
    [UserID]        INT      NOT NULL,
    [GIFID]         INT      NOT NULL,
    [FavoritedDate] DATETIME NOT NULL,
    CONSTRAINT [PK_Favorite] PRIMARY KEY CLUSTERED ([FavoriteID] ASC),
    CONSTRAINT [FK_Favorite_GIF] FOREIGN KEY ([GIFID]) REFERENCES [dbo].[GIF] ([GIFID]),
    CONSTRAINT [FK_Favorite_User] FOREIGN KEY ([UserID]) REFERENCES [dbo].[User] ([UserID])
);



