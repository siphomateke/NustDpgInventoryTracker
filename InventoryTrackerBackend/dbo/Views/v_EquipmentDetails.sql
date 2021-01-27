CREATE VIEW [dbo].[v_EquipmentDetails]
	AS SELECT Equipment.*, EquipmentPricing.EquipmentPrice as Price, Shop.Name as Shop, Shop.Town as ShopTown, Country.Name as ShopCountry, EquipmentCondition.Name AS Condition, UserId FROM Equipment
		INNER JOIN EquipmentCategory 
		ON (Equipment.EquipmentId = EquipmentCategory.EquipmentId)
		INNER JOIN UserViewableCategory
		ON (UserViewableCategory.CategoryId = EquipmentCategory.CategoryId)

		-- Condition name
		INNER JOIN EquipmentCondition
		ON (EquipmentCondition.ConditionId = Equipment.ConditionId)

		-- Pricing information when bought
		INNER JOIN EquipmentPricing
		ON (Equipment.EquipmentId = EquipmentPricing.EquipmentId)
		INNER JOIN Shop
		ON (EquipmentPricing.ShopId = Shop.ShopId)
		INNER JOIN Country
		ON (Shop.CountryId = Country.CountryId)
		WHERE IsOriginalPurchase = 1;
