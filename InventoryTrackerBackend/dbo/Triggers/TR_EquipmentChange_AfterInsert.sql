CREATE TRIGGER TR_EquipmentChange_AfterInsert
ON EquipmentChange
AFTER INSERT
AS
BEGIN
	SET NOCOUNT ON

	DECLARE @NotificationTitle VARCHAR = 'Equipment change'
	DECLARE @NotificationMessage VARCHAR = ' made changes to some equipment that require approval.'

	-- Get username of user who changed equipment
	DECLARE @Username VARCHAR(100)
	SELECT TOP(1) @Username = Username FROM AppUser
	WHERE AppUser.UserId = (SELECT ChangedByUserId FROM inserted)

	-- TODO: Make sure user still exists

	-- Get all users that should be notified about equipment changes
	INSERT INTO EquipmentChangeNotification
	(
		[UserId],
		[EquipmentChangeId],
		[Title],
		[Description]
	)
	SELECT 
		AppUser.UserId,
		(SELECT EquipmentChangeId FROM inserted),
		@NotificationTitle,
		CONCAT(@Username, @NotificationMessage)
	FROM AppUser
	INNER JOIN AppUserRole ON (AppUser.UserId = AppUserRole.RoleId)
	INNER JOIN RolePermission ON (AppUserRole.RoleId = RolePermission.RoleId)
	INNER JOIN Permission ON (RolePermission.PermissionId = Permission.PermissionId)
	WHERE Permission.Name = 'CAN_APPROVE_CHANGES'

	-- EXEC spUser_HasPermission SELECT inserted.ChangedByUserId, 'CAN_APPROVE_CHANGES'
END
