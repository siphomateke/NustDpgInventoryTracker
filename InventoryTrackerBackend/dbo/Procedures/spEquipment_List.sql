-- Get list of all equipment a user can view
CREATE PROCEDURE [dbo].[spEquipment_List]
	@UserId INT
AS
	DECLARE @CanViewAny BIT;
	EXEC spUser_CanViewAnyEquipment @UserId, @CanViewAny OUTPUT;

	IF @CanViewAny = 1
		SELECT 
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
			ShopCountry
		FROM v_EquipmentDetails
		WHERE UserId = @UserId;
	ELSE
		RAISERROR('User does not have permission to view equipment', 16, 1);
