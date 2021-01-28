CREATE PROCEDURE [dbo].[spEquipment_Details]
	@EquipmentId INT,
	@UserId INT
AS
	-- Make sure user can view this equipment
	DECLARE @CanView BIT
	EXEC spUser_CanViewEquipment @UserId, @EquipmentId, @CanView OUTPUT
	IF @CanView = 1
		SELECT TOP(1) 
			[EquipmentId],
			[Name], 
			[Description], 
			[Quantity], 
			[LocationInHome], 
			[Lost], 
			[Condition], 
			[Age], 
			[DateOfPurchase], 
			[ReceiptImage], 
			[WarrantyExpiryDate], 
			[WarrantyImage], 
			Price, 
			Shop, 
			ShopTown, 
			ShopCountry,
			ShopId
		FROM v_EquipmentDetails
		WHERE EquipmentId = @EquipmentId
	ELSE
		RAISERROR('User does not have permission to view this equipment.', 16, 1)
