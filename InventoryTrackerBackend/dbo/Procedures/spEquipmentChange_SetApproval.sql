CREATE PROCEDURE [dbo].[spEquipmentChange_SetApproval]
	@UserId INT,
	@EquipmentChangeId INT,
	@Approved BIT
AS
	-- Make sure user can approve changes
	DECLARE @CanApprove BIT;
	EXEC spUser_CanApproveChanges @UserId, @CanApprove;
	IF @CanApprove = 1
		UPDATE EquipmentChange
		SET ChangeApprovedByUserId = @UserId, ChangeApproved = @Approved
		WHERE EquipmentChangeId = @EquipmentChangeId;

		IF @Approved = 1
		BEGIN
			-- Get ID of original equipment
			DECLARE @EquipmentId INT;
			SELECT @EquipmentId = EquipmentId  FROM EquipmentChange 
			WHERE EquipmentChangeId = @EquipmentChangeId;

			-- Copy changes from pending change to original equipment
			UPDATE Equipment
			SET
				Equipment.Name = EquipmentChange.Name,
				Equipment.Description = EquipmentChange.Description,
				Equipment.Quantity = EquipmentChange.Quantity,
				Equipment.LocationInHome = EquipmentChange.LocationInHome,
				Equipment.Lost = EquipmentChange.Lost,
				Equipment.ConditionId = EquipmentChange.ConditionId,
				Equipment.Age = EquipmentChange.Age,
				Equipment.DateOfPurchase = EquipmentChange.DateOfPurchase,
				Equipment.ReceiptImage = EquipmentChange.ReceiptImage,
				Equipment.WarrantyExpiryDate = EquipmentChange.WarrantyExpiryDate
			FROM 
				Equipment INNER JOIN EquipmentChange
				ON Equipment.EquipmentId = EquipmentChange.EquipmentId
			WHERE Equipment.EquipmentId = @EquipmentId;
		END
	ELSE
		RAISERROR('User does not have permission to approve or reject equipment changes.', 16, 1);
		
