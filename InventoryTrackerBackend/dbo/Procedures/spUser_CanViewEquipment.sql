CREATE PROCEDURE [dbo].[spUser_CanViewEquipment]
	@UserId int,
	@EquipmentId int,
	@CanViewEquipment BIT OUTPUT
AS
	IF EXISTS(
		SELECT 1 FROM  UserViewableCategory
		INNER JOIN EquipmentCategory 
		ON (UserViewableCategory.CategoryId = EquipmentCategory.CategoryId)
		WHERE UserId = @UserId AND EquipmentId = @EquipmentId
	)
		SET @CanViewEquipment = 1
	ELSE
		SET @CanViewEquipment = 0
