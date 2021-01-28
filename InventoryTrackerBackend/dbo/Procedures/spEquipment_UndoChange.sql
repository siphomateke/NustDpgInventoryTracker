-- Used by a user to cancel a change they made accidentally
CREATE PROCEDURE [dbo].[spEquipment_UndoChange]
	@UserId INT,
	@EquipmentChangeId INT
AS
	BEGIN TRANSACTION
		DECLARE @EquipmentId INT;
		SELECT @EquipmentId = EquipmentId FROM EquipmentChange 
		WHERE EquipmentChangeId = @EquipmentChangeId;

		-- Make change still exists
		IF @EquipmentId = NULL
		BEGIN
            RAISERROR('This equipment change no longer exists', 16, 1);
		END

		DECLARE @CanEdit BIT;
		EXEC spUser_EnsureCanEditEquipment @UserId, @EquipmentId, @CanEdit OUTPUT;
		IF @CanEdit = 1
			DELETE FROM EquipmentChange WHERE EquipmentChangeId = @EquipmentChangeId;
		ELSE
			RAISERROR('User does not have permission to undo this change', 16, 1);			
	COMMIT;
