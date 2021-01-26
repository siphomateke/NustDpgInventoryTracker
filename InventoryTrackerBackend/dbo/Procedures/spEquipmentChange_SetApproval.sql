CREATE PROCEDURE [dbo].[spEquipmentChange_SetApproval]
	@UserId INT,
	@EquipmentChangeId INT,
	@Approved BIT
AS
	-- Make sure specified user can approve changes
	DECLARE @CanApprove BIT;
	EXEC spUser_CanApproveChanges @UserId, @CanApprove;
	IF @CanApprove = 1 BEGIN
		BEGIN TRANSACTION;
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

			-- Set the change as being successfully approved or rejected
			UPDATE EquipmentChange
			SET ChangeApprovedByUserId = @UserId, ChangeApproved = @Approved
			WHERE EquipmentChangeId = @EquipmentChangeId;

			-- Delete equipment change notifications now that the change has been handled
			BEGIN TRY
				-- Only delete notification if it hasn't already been dismissed
				IF EXISTS(SELECT 1 FROM EquipmentChangeNotification WHERE EquipmentChangeId = @EquipmentChangeId)
					DELETE FROM EquipmentChangeNotification WHERE EquipmentChangeId = @EquipmentChangeId
			END TRY
			BEGIN CATCH
				-- Errors encountered when deleting notifications aren't critical and can be mostly ignored.
				-- Just write a log for debugging purposes.
				PRINT(CONCAT('Error deleting equipment change notification with ID ', @EquipmentChangeId, ' for equipment ', @EquipmentId));
			END CATCH
		COMMIT;
	END
	ELSE BEGIN
		RAISERROR('User does not have permission to approve or reject equipment changes.', 16, 1);
	END
		
