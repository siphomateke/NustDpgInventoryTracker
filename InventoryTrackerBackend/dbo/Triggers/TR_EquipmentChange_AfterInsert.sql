CREATE TRIGGER TR_EquipmentChange_AfterInsert
ON EquipmentChange
AFTER INSERT
AS
BEGIN
	SET NOCOUNT ON;

	DECLARE @NotificationTitle VARCHAR (100) = 'Equipment change';
	DECLARE @NotificationMessage VARCHAR (250) = ' made changes to some equipment that require approval.';

	DECLARE @ChangedByUserId INT;
	SELECT @ChangedByUserId = ChangedByUserId FROM inserted;

	-- If the user who authored the change can approve and directly make changes, auto approve the change
	DECLARE @CanDirectlyChange BIT
	EXEC spUser_HasPermission @ChangedByUserId, 'CAN_MAKE_CHANGES', @CanDirectlyChange OUTPUT;
	DECLARE @CanApprove BIT;
	EXEC spUser_HasPermission @ChangedByUserId, 'CAN_APPROVE_CHANGES', @CanApprove OUTPUT;
	PRINT(CONCAT('Change requested. User can direct change: ', @CanDirectlyChange, ', User can approve: ', @CanApprove));
	IF @CanDirectlyChange = 1 AND @CanApprove = 1 BEGIN
		DECLARE @EquipmentChangeId INT;
		SELECT @EquipmentChangeId = EquipmentChangeId FROM inserted

		-- Immediately set equipment change as approved
		PRINT('Automatically approved changes');
		EXEC spEquipmentChange_SetApproval 
			@UserId = @ChangedByUserId, 
			@EquipmentChangeId = @EquipmentChangeId,
			@Approved = 1
	END
	-- If change author cannot approve changes, notify those who can
	ELSE BEGIN
		-- Get username of user who changed equipment
		DECLARE @Username VARCHAR(100);
		SELECT TOP(1) @Username = Username FROM AppUser
		WHERE AppUser.UserId = @ChangedByUserId;

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
END
