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
-- Insert into Category
INSERT INTO Category (CategoryName) VALUES 
('Funny'),
('Horror'),
('Weird'),
('Angry'),
('Boring');

-- Insert into Permissions
INSERT INTO Permissions (PermissionName) VALUES 
('Owner'),
('Viewer');

-- Insert into Users
INSERT INTO [User] (Username, Email, Password, LastLoginTime, PermissionID)
VALUES 
('u1', 'u1@gmail.com', '$2a$13$qipoG7nz/WbxROHrf40ewOEH9vLQSY1snxWMNjpoHzoAuJ8GAxaMu', GETDATE(), (SELECT PermissionID FROM Permissions WHERE PermissionName = 'Owner')),
('u2', 'u2@gmail.com', '$2a$13$qipoG7nz/WbxROHrf40ewOEH9vLQSY1snxWMNjpoHzoAuJ8GAxaMu', GETDATE(), (SELECT PermissionID FROM Permissions WHERE PermissionName = 'Owner')),
('u3', 'u3@gmail.com', '$2a$13$qipoG7nz/WbxROHrf40ewOEH9vLQSY1snxWMNjpoHzoAuJ8GAxaMu', GETDATE(), (SELECT PermissionID FROM Permissions WHERE PermissionName = 'Viewer')),
('u4', 'u4@gmail.com', '$2a$13$qipoG7nz/WbxROHrf40ewOEH9vLQSY1snxWMNjpoHzoAuJ8GAxaMu', GETDATE(), (SELECT PermissionID FROM Permissions WHERE PermissionName = 'Viewer')),
('u5', 'u5@gmail.com', '$2a$13$qipoG7nz/WbxROHrf40ewOEH9vLQSY1snxWMNjpoHzoAuJ8GAxaMu', GETDATE(), (SELECT PermissionID FROM Permissions WHERE PermissionName = 'Viewer'));

-- Insert into GIFs for each user
-- User 1 GIFs
INSERT INTO GIF (Title, Description, UploadDate, Link, CategoryID, UserID)
VALUES ('Cloudy Chance', 'Its cloudy with a chance of meat balls!', GETDATE(), 'https://i0.wp.com/www.galvanizeaction.org/wp-content/uploads/2022/06/Wow-gif.gif?fit=450%2C250&ssl=1', 1, (SELECT UserID FROM [User] WHERE Username = 'u1')),
       ('Duck Dance', 'We do be dancing', GETDATE(), 'https://media.itsnicethat.com/original_images/giphy-2021-gifs-and-clips-animation-itsnicethat-02.gif', 2, (SELECT UserID FROM [User] WHERE Username = 'u1')),
       ('Jake Dance', 'Come along with me...', GETDATE(), 'https://europe1.discourse-cdn.com/figma/original/3X/7/1/7105e9c010b3d1f0ea893ed5ca3bd58e6cec090e.gif', 3, (SELECT UserID FROM [User] WHERE Username = 'u1'));

-- User 2 GIFs
INSERT INTO GIF (Title, Description, UploadDate, Link, CategoryID, UserID)
VALUES ('HomerBush', 'Duoh!!!', GETDATE(), 'https://compote.slate.com/images/697b023b-64a5-49a0-8059-27b963453fb1.gif', 4, (SELECT UserID FROM [User] WHERE Username = 'u2')),
       ('Baby Dance', 'Boyfriend Aquired!!', GETDATE(), 'https://media1.giphy.com/media/dvdcBNbAiNVT9Z0iwP/200w.gif?cid=6c09b952zpzyh75ek3gb4zdew06ig3icebczpw5f8q4zqnnw&ep=v1_gifs_search&rid=200w.gif&ct=g', 5, (SELECT UserID FROM [User] WHERE Username = 'u2')),
       ('Walking World', 'For when you thought it was over?', GETDATE(), 'https://www.loomly.com/hs-fs/hubfs/Imported_Blog_Media/earth-Apr-03-2024-12-19-31-1897-AM.gif?width=540&height=540&name=earth-Apr-03-2024-12-19-31-1897-AM.gif', 1, (SELECT UserID FROM [User] WHERE Username = 'u2'));

-- User 3 GIFs
INSERT INTO GIF (Title, Description, UploadDate, Link, CategoryID, UserID)
VALUES ('Walking Cat', 'A Classic...', GETDATE(), 'https://mir-s3-cdn-cf.behance.net/project_modules/hd/5eeea355389655.59822ff824b72.gif', 2, (SELECT UserID FROM [User] WHERE Username = 'u3')),
       ('Dancing Baby', 'Sometimes you just need to dance.', GETDATE(), 'https://www.socialpilot.co/wp-content/uploads/2023/02/gif.gif', 3, (SELECT UserID FROM [User] WHERE Username = 'u3')),
       ('Dispecible Minion', 'He do be laughing tho...', GETDATE(), 'https://media3.giphy.com/media/ltIFdjNAasOwVvKhvx/200w.gif?cid=6c09b952hgd92aery8i46s7ep7z9h6a470f121wm6ater8gd&ep=v1_gifs_search&rid=200w.gif&ct=g', 4, (SELECT UserID FROM [User] WHERE Username = 'u3'));

-- User 4 GIFs
INSERT INTO GIF (Title, Description, UploadDate, Link, CategoryID, UserID)
VALUES ('PikaPika', 'Whys he crying?', GETDATE(), 'https://cdn.vox-cdn.com/thumbor/6z9EcmyiAJ00A_eP5tk2DmVeYe0=/0x15:500x348/1200x800/filters:focal(0x15:500x348):no_upscale()/cdn.vox-cdn.com/uploads/chorus_image/image/36992002/tumblr_lmwsamrrxT1qagx30.0.0.gif', 5, (SELECT UserID FROM [User] WHERE Username = 'u4')),
       ('PissedGuy', 'Not the azalliuz', GETDATE(), 'https://assets.vogue.com/photos/5891a171fb0604bf1f5c467b/master/pass/final-01-gifs-new.gif', 1, (SELECT UserID FROM [User] WHERE Username = 'u4')),
       ('Ayo', 'Here for the Grits!', GETDATE(), 'https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcRHbZUXhPEao0N0aCya8Ung4sPEYpOSyTcXNQ&s', 2, (SELECT UserID FROM [User] WHERE Username = 'u4'));

-- User 5 GIFs
INSERT INTO GIF (Title, Description, UploadDate, Link, CategoryID, UserID)
VALUES ('Dont Worry', 'You should be worry!', GETDATE(), 'https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcT29CacKOtV1pwjypYf7T5IvQ8hGF_nioVvCg&s', 3, (SELECT UserID FROM [User] WHERE Username = 'u5')),
       ('In Danger', 'This is why you keep getting kidnaped', GETDATE(), 'https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcS5bQ9UxeusmbVfFFBqJiK_IgbvPbhU_sRQsg&s', 4, (SELECT UserID FROM [User] WHERE Username = 'u5')),
       ('Frolicks', 'Your an addict!!', GETDATE(), 'https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcTmATFOCsSVintugZzryajasWF22CVV8RcC5A&s', 5, (SELECT UserID FROM [User] WHERE Username = 'u5'));
