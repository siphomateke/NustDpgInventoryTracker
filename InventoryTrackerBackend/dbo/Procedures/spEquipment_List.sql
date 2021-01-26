-- Get list of all equipment a user can view
CREATE PROCEDURE [dbo].[spEquipment_List]
	@UserId INT
AS
	DECLARE @CanViewAny BIT;
	EXEC spUser_CanViewAnyEquipment @UserId, @CanViewAny OUTPUT;

	IF @CanViewAny = 1
		SELECT Equipment.* FROM Equipment
		INNER JOIN EquipmentCategory 
		ON (Equipment.EquipmentId = EquipmentCategory.EquipmentId)
		INNER JOIN UserViewableCategory
		ON (UserViewableCategory.CategoryId = EquipmentCategory.CategoryId)
		WHERE UserId = @UserId;
	ELSE
		RAISERROR('User does not have permission to view equipment', 16, 1);
