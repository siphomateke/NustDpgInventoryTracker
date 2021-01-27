CREATE VIEW [dbo].[v_Shop]
	AS SELECT [Shop].[ShopId], [Shop].[Name], [Shop].[PhoneNumber], [Shop].[Email], [Shop].[Comments], [Shop].[StreetAdress], [Shop].[Town], Country.Name AS Country FROM Shop
	LEFT JOIN Country
	ON (Shop.CountryId = Country.CountryId)
