CREATE PROCEDURE [dbo].[spUser_CanViewEquipment]
	@UserId int,
	@EquipmentId int
AS
	IF EXISTS(
		SELECT 1 FROM  UserViewableCategory
		INNER JOIN EquipmentCategory 
		ON (UserViewableCategory.CategoryId = EquipmentCategory.CategoryId)
		WHERE UserId = @UserId AND EquipmentId = @EquipmentId
	)
		RETURN 1
	ELSE
		RETURN 0
