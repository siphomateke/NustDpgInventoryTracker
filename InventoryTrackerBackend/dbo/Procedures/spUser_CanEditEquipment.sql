CREATE PROCEDURE [dbo].[spUser_CanEditEquipment]
	@UserId int
AS
	DECLARE @EditPermissionId INT
	SELECT TOP(1) @EditPermissionId = PermissionId FROM Permission WHERE Name = 'CAN_MAKE_CHANGES'
	EXEC spUser_HasPermission @UserId, @EditPermissionId -- CAN_MAKE_CHANGES permission