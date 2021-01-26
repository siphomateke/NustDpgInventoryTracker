CREATE PROCEDURE [dbo].[spUser_Login]
	@Username VARCHAR (100),
	@Password VARCHAR (32)
AS
	SELECT * FROM AppUser WHERE Username = @Username AND Password = @Password
