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
-- Create permissions
INSERT INTO Permissions (PermissionName) VALUES ('Owner'), ('Viewer');

-- Insert into Category
INSERT INTO Category (CategoryName) VALUES 
('Funny'),
('Horror'),
('Weird'),
('Angry'),
('Boring');

-- Create users
INSERT INTO [User] (Username, Email, Password, LastLoginTime, PermissionID)
VALUES ('u1', 'u1@example.com', '$2a$13$qipoG7nz/WbxROHrf40ewOEH9vLQSY1snxWMNjpoHzoAuJ8GAxaMu', GETDATE(), (SELECT PermissionID FROM Permissions WHERE PermissionName = 'Owner')),
       ('u2', 'u2@example.com', '$2a$13$qipoG7nz/WbxROHrf40ewOEH9vLQSY1snxWMNjpoHzoAuJ8GAxaMu', GETDATE(), (SELECT PermissionID FROM Permissions WHERE PermissionName = 'Owner')),
       ('u3', 'u3@example.com', '$2a$13$qipoG7nz/WbxROHrf40ewOEH9vLQSY1snxWMNjpoHzoAuJ8GAxaMu', GETDATE(), (SELECT PermissionID FROM Permissions WHERE PermissionName = 'Viewer')),
       ('u4', 'u4@example.com', '$2a$13$qipoG7nz/WbxROHrf40ewOEH9vLQSY1snxWMNjpoHzoAuJ8GAxaMu', GETDATE(), (SELECT PermissionID FROM Permissions WHERE PermissionName = 'Viewer')),
       ('u5', 'u5@example.com', '$2a$13$qipoG7nz/WbxROHrf40ewOEH9vLQSY1snxWMNjpoHzoAuJ8GAxaMu', GETDATE(), (SELECT PermissionID FROM Permissions WHERE PermissionName = 'Viewer'));

-- Create GIFs
DECLARE @CategoryId INT;
SELECT @CategoryId = CategoryID FROM Category WHERE CategoryName = 'Default';

-- User 1 GIFs
INSERT INTO GIF (Title, Description, UploadDate, Link, CategoryID)
VALUES ('GIF 1', 'Description for GIF 1 by user1', GETDATE(), 'https://i0.wp.com/www.galvanizeaction.org/wp-content/uploads/2022/06/Wow-gif.gif?fit=450%2C250&ssl=1', '1'),
       ('GIF 2', 'Description for GIF 2 by user1', GETDATE(), 'https://media.itsnicethat.com/original_images/giphy-2021-gifs-and-clips-animation-itsnicethat-02.gif', '2'),
       ('GIF 3', 'Description for GIF 3 by user1', GETDATE(), 'https://europe1.discourse-cdn.com/figma/original/3X/7/1/7105e9c010b3d1f0ea893ed5ca3bd58e6cec090e.gif', '3');

-- User 2 GIFs
INSERT INTO GIF (Title, Description, UploadDate, Link, CategoryID)
VALUES ('GIF 1', 'Description for GIF 1 by user2', GETDATE(), 'https://compote.slate.com/images/697b023b-64a5-49a0-8059-27b963453fb1.gif', '4'),
       ('GIF 2', 'Description for GIF 2 by user2', GETDATE(), 'https://media1.giphy.com/media/dvdcBNbAiNVT9Z0iwP/200w.gif?cid=6c09b952zpzyh75ek3gb4zdew06ig3icebczpw5f8q4zqnnw&ep=v1_gifs_search&rid=200w.gif&ct=g', '5'),
       ('GIF 3', 'Description for GIF 3 by user2', GETDATE(), 'https://www.loomly.com/hs-fs/hubfs/Imported_Blog_Media/earth-Apr-03-2024-12-19-31-1897-AM.gif?width=540&height=540&name=earth-Apr-03-2024-12-19-31-1897-AM.gif', '1');

-- User 3 GIFs
INSERT INTO GIF (Title, Description, UploadDate, Link, CategoryID)
VALUES ('GIF 1', 'Description for GIF 1 by user3', GETDATE(), 'https://mir-s3-cdn-cf.behance.net/project_modules/hd/5eeea355389655.59822ff824b72.gif', '2'),
       ('GIF 2', 'Description for GIF 2 by user3', GETDATE(), 'https://www.socialpilot.co/wp-content/uploads/2023/02/gif.gif', '3'),
       ('GIF 3', 'Description for GIF 3 by user3', GETDATE(), 'https://media3.giphy.com/media/ltIFdjNAasOwVvKhvx/200w.gif?cid=6c09b952hgd92aery8i46s7ep7z9h6a470f121wm6ater8gd&ep=v1_gifs_search&rid=200w.gif&ct=g', '4');

-- User 4 GIFs
INSERT INTO GIF (Title, Description, UploadDate, Link, CategoryID)
VALUES ('GIF 1', 'Description for GIF 1 by user4', GETDATE(), 'https://cdn.vox-cdn.com/thumbor/6z9EcmyiAJ00A_eP5tk2DmVeYe0=/0x15:500x348/1200x800/filters:focal(0x15:500x348):no_upscale()/cdn.vox-cdn.com/uploads/chorus_image/image/36992002/tumblr_lmwsamrrxT1qagx30.0.0.gif', '5'),
       ('GIF 2', 'Description for GIF 2 by user4', GETDATE(), 'https://assets.vogue.com/photos/5891a171fb0604bf1f5c467b/master/pass/final-01-gifs-new.gif', '1'),
       ('GIF 3', 'Description for GIF 3 by user4', GETDATE(), 'https://media4.giphy.com/media/StKiS6x698JAl9d6cx/200w.gif?cid=6c09b952h9blxam1zn08qv8grp7xypt1zcgq15tnnqmp55vz&ep=v1_gifs_search&rid=200w.gif&ct=g', '2');

-- User 5 GIFs
INSERT INTO GIF (Title, Description, UploadDate, Link, CategoryID)
VALUES ('GIF 1', 'Description for GIF 1 by user5', GETDATE(), 'https://c.tenor.com/QWAo2echlVEAAAAC/cute.gif', '3'),
       ('GIF 2', 'Description for GIF 2 by user5', GETDATE(), 'https://blogs.mathworks.com/matlab/files/2024/02/animatingFlickbook_1.gif', '4'),
       ('GIF 3', 'Description for GIF 3 by user5', GETDATE(), 'https://media.idownloadblog.com/wp-content/uploads/2016/11/Animated-GIF-Banana.gif', '5');