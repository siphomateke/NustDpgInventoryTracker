CREATE PROCEDURE [dbo].[spUser_CanEditEquipment]
	@UserId int
AS
	RETURN EXEC spUser_HasPermission @UserId, 'CAN_MAKE_CHANGES'