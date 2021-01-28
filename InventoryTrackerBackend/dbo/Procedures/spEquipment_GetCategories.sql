CREATE PROCEDURE [dbo].[spEquipment_GetCategories]
	@EquipmentId INT,
	@UserId INT
AS
	-- Make sure user can view this equipment
	DECLARE @CanView BIT
	EXEC spUser_CanViewEquipment @UserId, @EquipmentId, @CanView OUTPUT
	IF @CanView = 1
		SELECT Category.* FROM Equipment
		INNER JOIN EquipmentCategory
		ON (Equipment.EquipmentId = EquipmentCategory.EquipmentId)
		INNER JOIN Category
		ON (Category.CategoryId = EquipmentCategory.CategoryId)
		INNER JOIN UserViewableCategory
		ON (UserViewableCategory.CategoryId = EquipmentCategory.CategoryId)
		WHERE UserId = @UserId AND Equipment.EquipmentId = @EquipmentId;
	ELSE
		RAISERROR('User does not have permission to view this equipment.', 16, 1)
