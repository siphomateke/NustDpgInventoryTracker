CREATE PROCEDURE [dbo].[spNewEquipment_SetApproval]
	@UserId INT,
	@NewEquipmentId INT
AS
	DECLARE @CanApprove BIT;
	EXEC spUser_HasPermission @UserId, 'CAN_APPROVE_NEW_EQUIPMENT', @CanApprove OUTPUT;

	IF @CanApprove = 1
		UPDATE NewEquipment
		SET Approved = 1
		WHERE NewEquipmentId = @NewEquipmentId
	ELSE
		RAISERROR('User does not have permission to approve new equipment requests.', 16, 1);
