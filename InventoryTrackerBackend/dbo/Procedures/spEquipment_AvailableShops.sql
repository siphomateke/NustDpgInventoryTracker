CREATE PROCEDURE [dbo].[spEquipment_AvailableShops]
	@UserId INT,
	@EquipmentId INT
AS
	-- Only show information if user can view equipment
	DECLARE @CanView BIT;
	EXEC spUser_CanViewEquipment @UserId, @EquipmentId, @CanView OUTPUT;

	IF @CanView = 1
		SELECT [Shop].[ShopId], [Shop].[Name], [Shop].[PhoneNumber], [Shop].[Email], [Shop].[Comments], [Shop].[StreetAdress], [Shop].[Town], Shop.Country as Country FROM Equipment
		INNER JOIN EquipmentPricing
		ON (EquipmentPricing.EquipmentId = Equipment.EquipmentId)
		INNER JOIN v_Shop as Shop
		ON (Shop.ShopId = EquipmentPricing.ShopId)
		WHERE Equipment.EquipmentId = @EquipmentId;
	ELSE
		RAISERROR('User does not have permission to view this equipment and its assocated information', 16, 1);
