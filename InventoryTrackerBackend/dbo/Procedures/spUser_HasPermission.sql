CREATE PROCEDURE [dbo].[spUser_HasPermission]
	@UserId INT,
	@PermissionName VARCHAR (50),
	@HasPermission BIT OUTPUT
AS
	-- Get permission ID from name
	DECLARE @PermissionId INT
	SELECT TOP(1) @PermissionId = PermissionId FROM Permission WHERE Name = @PermissionName

	-- Actually check if the user has a role with that permission 
	IF EXISTS(
		SELECT 1 FROM AppUser
		INNER JOIN AppUserRole ON (AppUser.RoleId = AppUserRole.RoleId)
		INNER JOIN RolePermission ON (AppUserRole.RoleId = RolePermission.RoleId)
		WHERE UserId = @UserId AND PermissionId = @PermissionId
	)
		SET @HasPermission = 1
	ELSE
		SET @HasPermission = 0
