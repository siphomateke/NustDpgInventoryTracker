CREATE TRIGGER TR_EquipmentChange_AfterInsert
ON EquipmentChange
AFTER INSERT
AS
BEGIN
	SET NOCOUNT ON;

	DECLARE @NotificationTitle VARCHAR (100) = 'Equipment change';
	DECLARE @NotificationMessage VARCHAR (250) = ' made changes to some equipment that require approval.';

	-- Get username of user who changed equipment
	DECLARE @Username VARCHAR(100);
	SELECT TOP(1) @Username = Username FROM AppUser
	WHERE AppUser.UserId = (SELECT ChangedByUserId FROM inserted);

	-- If the user has since been deleted, use a different name for them in the notification message.
	IF @Username IS NULL
		SET @Username = '<missing user>';

	-- Notify all users who can approve equipment change notifications
	INSERT INTO EquipmentChangeNotification
	(
		[UserId],
		[EquipmentChangeId],
		[Title],
		[Description]
	)
	SELECT 
		v_ApproverUsers.UserId,
		(SELECT EquipmentChangeId FROM inserted),
		@NotificationTitle,
		CONCAT(@Username, @NotificationMessage)
	FROM v_ApproverUsers;
END
