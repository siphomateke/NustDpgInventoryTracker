CREATE PROCEDURE [dbo].[spUser_CanViewEquipment]
	@UserId int,
	@EquipmentId int,
	@CanViewEquipment BIT OUTPUT
AS
	DECLARE @CanViewAny BIT;
	EXEC spUser_CanViewAnyEquipment @UserId, @CanViewAny OUTPUT;

	IF @CanViewAny = 1 AND EXISTS(
		SELECT 1 FROM  UserViewableCategory
		INNER JOIN EquipmentCategory 
		ON (UserViewableCategory.CategoryId = EquipmentCategory.CategoryId)
		WHERE UserId = @UserId AND EquipmentId = @EquipmentId
	)
		SET @CanViewEquipment = 1
	ELSE
		SET @CanViewEquipment = 0
