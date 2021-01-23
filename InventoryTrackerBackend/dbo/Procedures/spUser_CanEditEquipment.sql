CREATE PROCEDURE [dbo].[spUser_CanEditEquipment]
	@UserId INT,
	@CanEdit BIT OUTPUT
AS
	EXEC spUser_HasPermission @UserId, 'CAN_MAKE_CHANGES', @CanEdit OUTPUT;