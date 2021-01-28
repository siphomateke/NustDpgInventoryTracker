CREATE VIEW [dbo].[v_EquipmentChangeDetails]
	AS SELECT 
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
		INNER JOIN EquipmentCategory 
		ON (EquipmentChange.EquipmentId = EquipmentCategory.EquipmentId)
		INNER JOIN UserViewableCategory
		ON (UserViewableCategory.CategoryId = EquipmentCategory.CategoryId)

		-- Condition name
		INNER JOIN EquipmentCondition
		ON (EquipmentCondition.ConditionId = EquipmentChange.ConditionId)

		INNER JOIN AppUser as ChangedByUser
		ON (ChangedByUser.UserId = EquipmentChange.ChangedByUserId)

		INNER JOIN AppUser as ChangeApprovedByUser
		ON (ChangeApprovedByUser.UserId = EquipmentChange.ChangeApprovedByUserId)

		-- Pricing information when bought
		INNER JOIN EquipmentPricing
		ON (EquipmentChange.EquipmentId = EquipmentPricing.EquipmentId)
		INNER JOIN v_Shop as Shop
		ON (EquipmentPricing.ShopId = Shop.ShopId)
		WHERE IsOriginalPurchase = 1;
