CREATE PROCEDURE [dbo].[spCountry_Add]
	@Name VARCHAR (50),
	@CountryId INT = NULL OUTPUT
AS
	INSERT INTO Country (Name) VALUES (@Name);
	SET @CountryId = IDENT_CURRENT('Country');
