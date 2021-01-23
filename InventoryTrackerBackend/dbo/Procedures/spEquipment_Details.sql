CREATE PROCEDURE [dbo].[spEquipment_Details]
	@EquipmentId INT,
	@UserId INT
AS
	-- Make sure user can view this equipment
	DECLARE @CanView BIT
	EXEC spUser_CanViewEquipment @UserId, @EquipmentId, @CanView OUTPUT
	IF @CanView = 1
		SELECT * FROM Equipment
	ELSE
		RAISERROR('User does not have permission to view this equipment.', 16, 1)
