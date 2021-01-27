CREATE VIEW [dbo].[v_EquipmentDetails]
	AS SELECT Equipment.*, EquipmentPricing.EquipmentPrice as Price, Shop.Name as Shop, Shop.Town as ShopTown, Shop.Country as ShopCountry, EquipmentCondition.Name AS Condition, UserId FROM Equipment
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
		INNER JOIN v_Shop as Shop
		ON (EquipmentPricing.ShopId = Shop.ShopId)
		WHERE IsOriginalPurchase = 1;
