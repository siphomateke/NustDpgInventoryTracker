CREATE PROCEDURE [dbo].[spUser_HasPermission]
	@UserId INT,
	@PermissionId INT
AS
	SELECT 1 FROM AppUser
	INNER JOIN AppUserRole ON (AppUser.RoleId = AppUserRole.RoleId)
	INNER JOIN RolePermission ON (AppUserRole.RoleId = RolePermission.RoleId)
	WHERE UserId = @UserId AND PermissionId = @PermissionId
