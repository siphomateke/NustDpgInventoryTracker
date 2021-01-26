CREATE PROCEDURE [dbo].[spEquipmentPricing_Set]
	@EquipmentId        INT,
	@ShopId             INT = NULL,
    @EquipmentPrice     INT = NULL,
    @DatePriceChecked   DATETIME = NULL,
    @IsOriginalPurchase BIT = NULL
AS
	INSERT INTO EquipmentPricing
		(EquipmentId, ShopId, EquipmentPrice, DatePriceChecked, IsOriginalPurchase)
	VALUES
		(@EquipmentId, @ShopId, @EquipmentPrice, @DatePriceChecked, @IsOriginalPurchase);
