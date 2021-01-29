CREATE PROCEDURE [dbo].[spEquipmentChange_List]
	@UserId INT,
	@OwnOnly BIT = 1
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
		FROM v_EquipmentChangeDetails as EquipmentChange
		WHERE UserId = @UserId AND ((@OwnOnly = 1 AND ChangedByUserId = @UserId) OR @OwnOnly = 0);
	ELSE
		RAISERROR('User does not have permission to view equipment', 16, 1);
