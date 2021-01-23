CREATE PROCEDURE [dbo].[spUser_EnsureCanEditEquipment]
	@UserId INT,
	@EquipmentId INT
AS
	DECLARE @CanEdit BIT
    EXEC @CanEdit = spUser_CanEditEquipment @UserId
    IF @CanEdit = 0
        RAISERROR('User does not have permission to edit equipment.', 20, 1) WITH LOG

    DECLARE @CanView BIT
    EXEC @CanView = spUser_CanViewEquipment @UserId, @EquipmentId
    IF @CanView = 0
        RAISERROR('User does not have permission to edit this equipment.', 20, 1) WITH LOG
RETURN 0
