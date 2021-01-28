CREATE VIEW [dbo].[v_EquipmentDetails]
	AS SELECT DISTINCT
		[Equipment].[EquipmentId], 
		[Equipment].[Name], 
		[Equipment].[Description], 
		[Equipment].[Quantity], 
		[Equipment].[LocationInHome],
		[Equipment].[Lost],
		[Equipment].[ConditionId],
		EquipmentCondition.Name as Condition,
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
		UserId
	FROM Equipment
		-- Condition name
		LEFT JOIN EquipmentCondition
		ON (EquipmentCondition.ConditionId = Equipment.ConditionId)

		-- Include which users can view each equipment
		LEFT JOIN EquipmentCategory 
		ON (Equipment.EquipmentId = EquipmentCategory.EquipmentId)
		LEFT JOIN UserViewableCategory
		ON (UserViewableCategory.CategoryId = EquipmentCategory.CategoryId)

		-- Pricing information when bought
		LEFT JOIN 
		(SELECT * FROM EquipmentPricing WHERE IsOriginalPurchase = 1) EquipmentPricing
		ON (Equipment.EquipmentId = EquipmentPricing.EquipmentId)
		LEFT JOIN v_Shop as Shop
		ON (EquipmentPricing.ShopId = Shop.ShopId)
