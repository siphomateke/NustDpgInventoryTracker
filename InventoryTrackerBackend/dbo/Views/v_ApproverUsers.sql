-- All users that can approve changes
CREATE VIEW [dbo].[v_ApproverUsers]
	AS SELECT AppUser.* FROM AppUser
	INNER JOIN AppUserRole ON (AppUser.UserId = AppUserRole.RoleId)
	INNER JOIN RolePermission ON (AppUserRole.RoleId = RolePermission.RoleId)
	INNER JOIN Permission ON (RolePermission.PermissionId = Permission.PermissionId)
	WHERE Permission.Name = 'CAN_APPROVE_CHANGES';