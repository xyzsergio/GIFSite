CREATE TABLE [dbo].[Playlist] (
    [PlaylistID]   INT          NOT NULL,
    [UserID]       INT          NOT NULL,
    [PlaylistName] VARCHAR (50) NOT NULL,
    [GIFID]        INT          NOT NULL,
    [DateAdded]    DATETIME     NOT NULL,
    CONSTRAINT [PK_Playlist] PRIMARY KEY CLUSTERED ([PlaylistID] ASC),
    CONSTRAINT [FK_Playlist_GIF] FOREIGN KEY ([GIFID]) REFERENCES [dbo].[GIF] ([GIFID]),
    CONSTRAINT [FK_Playlist_User] FOREIGN KEY ([UserID]) REFERENCES [dbo].[User] ([UserID])
);

