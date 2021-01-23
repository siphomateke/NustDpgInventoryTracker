-- Used by a user to cancel a change they made accidentally
CREATE PROCEDURE [dbo].[spEquipment_UndoChange]
	@UserId INT,
	@EquipmentChangeId INT
AS
	BEGIN TRANSACTION
		DECLARE @EquipmentId INT

		SELECT @EquipmentId = EquipmentId FROM EquipmentChange 
		WHERE EquipmentChangeId = @EquipmentChangeId

		-- Make change still exists
		IF @EquipmentId = NULL
            RAISERROR('This equipment change no longer exists', 16, 1);

		EXEC spUser_EnsureCanEditEquipment @UserId, @EquipmentId

		DELETE FROM EquipmentChange WHERE EquipmentChangeId = @EquipmentChangeId
	COMMIT;
