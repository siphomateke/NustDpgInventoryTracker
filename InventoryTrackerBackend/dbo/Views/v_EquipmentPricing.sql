CREATE VIEW [dbo].[v_EquipmentPricing]
	AS SELECT EquipmentPrice AS Price, DatePriceChecked, EquipmentId, IsOriginalPurchase, Shop.* FROM EquipmentPricing
	INNER JOIN v_Shop as Shop
	ON (Shop.ShopId = EquipmentPricing.ShopId)