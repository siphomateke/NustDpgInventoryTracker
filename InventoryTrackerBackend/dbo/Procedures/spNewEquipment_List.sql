CREATE PROCEDURE [dbo].[spNewEquipment_List]
	@UserId INT,
	-- Whether to only show new equipment requested by this user
	@OwnOnly BIT = 1
AS
	IF @OwnOnly = 1
	BEGIN
		SELECT Name, Description, Quantity FROM NewEquipment WHERE AddedByUserId = @UserId;
	END
	ELSE
	BEGIN
		DECLARE @CanApprove BIT;
		EXEC spUser_HasPermission @UserId, 'CAN_APPROVE_NEW_EQUIPMENT', @CanApprove OUTPUT;

		IF @CanApprove = 1
			SELECT Name, Description, Quantity FROM NewEquipment;
		ELSE
			RAISERROR('User does not have permission to view all new equipment requests.', 16, 1);
	END
