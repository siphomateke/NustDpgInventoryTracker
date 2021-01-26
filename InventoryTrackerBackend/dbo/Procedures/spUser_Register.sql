CREATE PROCEDURE [dbo].[spUser_Register]
	@AdminUserId INT,
	@Username VARCHAR (100),
	@Password VARCHAR (32),
	@RoleId INT,
	@FirstName VARCHAR (50),
	@LastName VARCHAR (50),
	@Email VARCHAR (150) = NULL
AS
	DECLARE @CanAddUsers BIT;
	EXEC spUser_HasPermission @AdminUserId, 'MANAGE_USERS', @CanAddUsers OUTPUT;

	IF @CanAddUsers = 1
		INSERT INTO AppUser
			(Username, Password, RoleId, FirstName, LastName, Email)
		VALUES 
			(@Username, @Password, @RoleId, @FirstName, @LastName, @Email)
	ELSE
		Raiserror('User does not have permission to add new users.', 16, 1);
