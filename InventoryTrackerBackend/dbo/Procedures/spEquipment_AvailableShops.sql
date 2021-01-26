CREATE PROCEDURE [dbo].[spEquipment_AvailableShops]
	@UserId INT,
	@EquipmentId INT
AS
	-- Only show information if user can view equipment
	DECLARE @CanView BIT;
	EXEC spUser_CanViewEquipment @UserId, @EquipmentId, @CanView OUTPUT;

	IF @CanView = 1
		SELECT [Shop].[ShopId], [Shop].[Name], [Shop].[PhoneNumber], [Shop].[Email], [Shop].[Comments], [Shop].[StreetAdress], [Shop].[Town], Country.Name as Country FROM Equipment
		INNER JOIN EquipmentPricing
		ON (EquipmentPricing.EquipmentId = Equipment.EquipmentId)
		INNER JOIN Shop
		ON (Shop.ShopId = EquipmentPricing.ShopId)
		INNER JOIN Country
		ON (Country.CountryId = Shop.CountryId)
		WHERE Equipment.EquipmentId = @EquipmentId;
	ELSE
		RAISERROR('User does not have permission to view this equipment and its assocated information', 16, 1);
