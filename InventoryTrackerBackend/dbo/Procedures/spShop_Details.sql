CREATE PROCEDURE [dbo].[spShop_Details]
	@ShopId Int
AS
	SELECT TOP(1) * FROM v_Shop
	WHERE ShopId = @ShopId
