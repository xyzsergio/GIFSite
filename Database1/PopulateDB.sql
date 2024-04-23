/*
Post-Deployment Script Template							
--------------------------------------------------------------------------------------
 This file contains SQL statements that will be appended to the build script.		
 Use SQLCMD syntax to include a file in the post-deployment script.			
 Example:      :r .\myfile.sql								
 Use SQLCMD syntax to reference a variable in the post-deployment script.		
 Example:      :setvar TableName MyTable							
               SELECT * FROM [$(TableName)]					
--------------------------------------------------------------------------------------
*/
SET IDENTITY_INSERT [dbo].[GIF] ON 
GO
INSERT [dbo].[GIF] ([GIFID], [Title], [Description], [UploadDate], [FilePath]) VALUES (1, N'Gif1', N'Description for Gif1', CAST(N'2000-01-01T00:00:00.000' AS DateTime), N'Website link or file path from uploaded file will go here')
GO
INSERT [dbo].[GIF] ([GIFID], [Title], [Description], [UploadDate], [FilePath]) VALUES (2, N'Gif 1', N'Description for Gif 1', CAST(N'2011-01-01T00:00:00.000' AS DateTime), N'Insert file path here')
GO
SET IDENTITY_INSERT [dbo].[GIF] OFF
GO
SET IDENTITY_INSERT [dbo].[User] ON 
GO
INSERT [dbo].[User] ([UserID], [Username], [Email], [Password], [LastLoginTime], [AdminID]) VALUES (1, N'JohnCena', N'johncena@gmail.com', N'$2a$13$eECWe3FYlN6R7xBEHTzeVunygplM7uLGjtc4MrfkxDwdP34fmGs3i', CAST(N'2024-04-14T22:29:16.060' AS DateTime), 1)
GO
SET IDENTITY_INSERT [dbo].[User] OFF
GO
SET IDENTITY_INSERT [dbo].[Permissions] ON 
GO
INSERT [dbo].[Permissions] ([PermissionID], [PermissionName]) VALUES (1, N'Basic')
GO
SET IDENTITY_INSERT [dbo].[Permissions] OFF
GO
SET IDENTITY_INSERT [dbo].[Administrator] ON 
GO
INSERT [dbo].[Administrator] ([AdminID], [UserID], [PermissionID]) VALUES (1, 1, 1)
GO
SET IDENTITY_INSERT [dbo].[Administrator] OFF
GO
