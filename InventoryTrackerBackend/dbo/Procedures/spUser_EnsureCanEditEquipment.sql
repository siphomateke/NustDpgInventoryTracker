CREATE PROCEDURE [dbo].[spUser_EnsureCanEditEquipment]
	@UserId INT,
	@EquipmentId INT,
    @CanEdit BIT OUTPUT,
    @CanEditAny BIT = NULL OUTPUT,
    @CanView BIT = NULL OUTPUT
AS
    EXEC spUser_CanEditEquipment @UserId, @CanEditAny OUTPUT;
    EXEC spUser_CanViewEquipment @UserId, @EquipmentId, @CanView OUTPUT;

    IF @CanEditAny = 0 OR @CanView = 0
        SET @CanEdit = 1;

        IF @CanEditAny = 0
            RAISERROR('User does not have permission to edit equipment.', 16, 1);
        IF @CanView = 0
            RAISERROR('User does not have permission to edit this equipment.', 16, 1);
    ELSE
        SET @CanEdit = 0
