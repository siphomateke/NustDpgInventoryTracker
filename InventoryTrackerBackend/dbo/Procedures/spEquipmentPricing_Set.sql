CREATE PROCEDURE [dbo].[spEquipmentPricing_Set]
	@ShopId             INT = NULL,
    @EquipmentId        INT = NULL,
    @EquipmentPrice     INT = NULL,
    @DatePriceChecked   DATETIME = NULL,
    @IsOriginalPurchase BIT = NULL
AS
	INSERT INTO EquipmentPricing
		(EquipmentId, ShopId, EquipmentPrice, DatePriceChecked, IsOriginalPurchase)
	VALUES
		(@EquipmentId, @ShopId, @EquipmentId, @DatePriceChecked, @IsOriginalPurchase);
