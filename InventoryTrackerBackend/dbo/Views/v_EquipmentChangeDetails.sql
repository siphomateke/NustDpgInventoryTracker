CREATE VIEW [dbo].[v_EquipmentChangeDetails]
	AS SELECT DISTINCT
		[EquipmentChange].[EquipmentChangeId], 
		[EquipmentChange].[ChangedByUserId],
		ChangedByUser.Username as ChangedByUserUsername,
		[EquipmentChange].[ChangeApprovedByUserId], 
		ChangedByUser.Username as ChangedApprovedByUsername,
		[EquipmentChange].[ChangeApproved], 
		[EquipmentChange].[ChangeDate], 
		[EquipmentChange].[EquipmentId], 
		[EquipmentChange].[Name], 
		[EquipmentChange].[Description], 
		[EquipmentChange].[Quantity], 
		[EquipmentChange].[LocationInHome],
		[EquipmentChange].[Lost],
		[EquipmentChange].[ConditionId],
		[EquipmentChange].[Age], 
		[EquipmentChange].[DateOfPurchase], 
		[EquipmentChange].[ReceiptImage],
		[EquipmentChange].[WarrantyExpiryDate],
		[EquipmentChange].[WarrantyImage],
		EquipmentPricing.EquipmentPrice as Price, 
		Shop.ShopId,
		Shop.Name as Shop, 
		Shop.Town as ShopTown, 
		Shop.Country as ShopCountry,
		EquipmentCondition.Name as Condition,
		UserViewableCategory.UserId as UserId
	FROM EquipmentChange
		-- Include which users can view each equipment
		LEFT JOIN EquipmentCategory 
		ON (EquipmentChange.EquipmentId = EquipmentCategory.EquipmentId)
		LEFT JOIN UserViewableCategory
		ON (UserViewableCategory.CategoryId = EquipmentCategory.CategoryId)

		-- Condition name
		LEFT JOIN EquipmentCondition
		ON (EquipmentCondition.ConditionId = EquipmentChange.ConditionId)

		-- Pricing information when bought
		LEFT JOIN 
		(SELECT * FROM EquipmentPricing WHERE IsOriginalPurchase = 1) EquipmentPricing
		ON (EquipmentChange.EquipmentId = EquipmentPricing.EquipmentId)
		LEFT JOIN v_Shop as Shop
		ON (EquipmentPricing.ShopId = Shop.ShopId)
		
		-- Uesr data
		LEFT JOIN AppUser as ChangedByUser
		ON (ChangedByUser.UserId = EquipmentChange.ChangedByUserId)
		LEFT JOIN AppUser as ChangeApprovedByUser
		ON (ChangeApprovedByUser.UserId = EquipmentChange.ChangeApprovedByUserId);
