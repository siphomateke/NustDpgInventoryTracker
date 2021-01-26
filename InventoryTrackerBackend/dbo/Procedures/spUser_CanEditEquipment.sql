CREATE PROCEDURE [dbo].[spUser_CanEditEquipment]
	@UserId INT,
	@CanEdit BIT OUTPUT
AS
	EXEC spUser_HasPermission @UserId, 'CAN_ASK_FOR_CHANGES', @CanEdit OUTPUT;