-- Get list of all equipment a user can view
CREATE PROCEDURE [dbo].[spEquipment_List]
	@UserId INT
AS
	DECLARE @CanViewAny BIT;
	EXEC spUser_CanViewAnyEquipment @UserId, @CanViewAny OUTPUT;

	IF @CanViewAny = 1
		SELECT Equipment.*, EquipmentPricing.EquipmentPrice as Price, Shop.Name as Shop, Shop.Town as ShopTown, Country.Name as ShopCountry FROM Equipment
		INNER JOIN EquipmentCategory 
		ON (Equipment.EquipmentId = EquipmentCategory.EquipmentId)
		INNER JOIN UserViewableCategory
		ON (UserViewableCategory.CategoryId = EquipmentCategory.CategoryId)

		-- Pricing information when bought
		INNER JOIN EquipmentPricing
		ON (Equipment.EquipmentId = EquipmentPricing.EquipmentId)
		INNER JOIN Shop
		ON (EquipmentPricing.ShopId = Shop.ShopId)
		INNER JOIN Country
		ON (Shop.CountryId = Country.CountryId)

		WHERE UserId = @UserId AND IsOriginalPurchase = 1;
	ELSE
		RAISERROR('User does not have permission to view equipment', 16, 1);
