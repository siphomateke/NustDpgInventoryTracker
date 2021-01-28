CREATE VIEW [dbo].[v_EquipmentDetails]
	AS SELECT 
		[Equipment].[EquipmentId], 
		[Equipment].[Name], 
		[Equipment].[Description], 
		[Equipment].[Quantity], 
		[Equipment].[LocationInHome],
		[Equipment].[Lost],
		[Equipment].[ConditionId],
		[Equipment].[Age], 
		[Equipment].[DateOfPurchase], 
		[Equipment].[ReceiptImage],
		[Equipment].[WarrantyExpiryDate],
		[Equipment].[WarrantyImage],
		EquipmentPricing.EquipmentPrice as Price, 
		Shop.ShopId,
		Shop.Name as Shop, 
		Shop.Town as ShopTown, 
		Shop.Country as ShopCountry,
		EquipmentCondition.Name as Condition,
		UserId
	FROM Equipment
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
