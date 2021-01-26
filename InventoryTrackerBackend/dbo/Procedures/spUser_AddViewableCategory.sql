CREATE PROCEDURE [dbo].[spUser_AddViewableCategory]
	@AdminUserId INT,
	@UserId INT,
	@CategoryId INT
AS
	INSERT INTO UserViewableCategory
		(UserId, CategoryId)
	VALUES 
		(@UserId, @CategoryId);