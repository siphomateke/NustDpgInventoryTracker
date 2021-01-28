CREATE PROCEDURE [dbo].[spEquipmentChange_List]
	@UserId INT
AS
	DECLARE @CanViewAny BIT;
	EXEC spUser_CanViewAnyEquipment @UserId, @CanViewAny OUTPUT;

	IF @CanViewAny = 1
		SELECT 
			[EquipmentChangeId], 
			[ChangedByUserId],
			ChangedByUserUsername,
			[ChangeApprovedByUserId], 
			ChangedApprovedByUsername,
			[ChangeApproved], 
			[ChangeDate], 
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
		FROM v_EquipmentChangeDetails
		WHERE UserId = @UserId AND ChangedByUserId = @UserId;
	ELSE
		RAISERROR('User does not have permission to view equipment', 16, 1);
