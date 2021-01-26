CREATE PROCEDURE [dbo].[spUser_CanViewAnyEquipment]
	@UserId int,
	@CanViewAnyEquipment BIT OUTPUT
AS
	EXEC spUser_HasPermission @UserId, 'READ', @CanViewAnyEquipment OUTPUT;
