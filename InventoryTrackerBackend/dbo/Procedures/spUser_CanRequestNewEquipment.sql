CREATE PROCEDURE [dbo].[spUser_CanRequestNewEquipment]
	@UserId INT,
	@CanRequestNewEquipment BIT OUTPUT
AS
	EXEC spUser_HasPermission @UserId, 'CAN_ASK_FOR_EQUIPMENT', @CanRequestNewEquipment OUTPUT;
