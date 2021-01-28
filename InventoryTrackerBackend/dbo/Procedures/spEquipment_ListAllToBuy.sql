CREATE PROCEDURE [dbo].[spEquipment_ListAllToBuy]
	@UserId INT
AS
	DECLARE @CanApprove BIT;
	EXEC spUser_HasPermission @UserId, 'CAN_APPROVE_NEW_EQUIPMENT', @CanApprove OUTPUT;

	DECLARE @EquipmentToBuy TABLE (Name VARCHAR (50), Description VARCHAR (500), Quantity INT);

	INSERT INTO @EquipmentToBuy
	SELECT Name, Description, Quantity FROM v_EquipmentToBuy;

	IF @CanApprove = 1
	BEGIN
		-- Also show all newly requested equipment from all users
		INSERT INTO @EquipmentToBuy
		EXEC spNewEquipment_List @UserId, 0;
	END
	ELSE
	BEGIN
		-- Also show all newly requested equipment from this user
		INSERT INTO @EquipmentToBuy
		EXEC spNewEquipment_List @UserId, 1;
	END
