CREATE PROCEDURE [dbo].[spEquipment_GetPrices]
	@UserId INT,
	@EquipmentId INT,
	@IncludeOriginalPurchase BIT = 0
AS
	DECLARE @CanView BIT
	EXEC spUser_CanViewEquipment @UserId, @EquipmentId, @CanView OUTPUT
	IF @CanView = 1
		IF @IncludeOriginalPurchase = 1
			SELECT * FROM v_EquipmentPricing
			WHERE EquipmentId = @EquipmentId
		ELSE
			SELECT * FROM v_EquipmentPricing
			WHERE EquipmentId = @EquipmentId AND IsOriginalPurchase = 0
	ELSE
		RAISERROR('User does not have permission to view this equipment.', 16, 1)
