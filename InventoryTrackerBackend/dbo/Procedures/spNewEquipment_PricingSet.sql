CREATE PROCEDURE [dbo].[spNewEquipment_PricingSet]
	@UserId INT,
	@NewEquipmentId     INT,
	@ShopId             INT = NULL,
    @EquipmentPrice     INT = NULL,
    @DatePriceChecked   DATETIME = NULL
AS
	DECLARE @CanRequest BIT;
	EXEC spUser_CanRequestNewEquipment @UserId, @CanRequest OUTPUT;

	IF @CanRequest = 0
		RAISERROR('User does not have permission to request new equipment to be bought', 16, 1);
	ELSE
		INSERT INTO NewEquipmentPricing
			(NewEquipmentId, ShopId, EquipmentPrice, DatePriceChecked)
		VALUES
			(@NewEquipmentId, @ShopId, EquipmentPrice, @DatePriceChecked);
