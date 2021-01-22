CREATE PROCEDURE [dbo].[spCountry_Add]
	@Name VARCHAR (50)
AS
	INSERT INTO Country (Name) VALUES (@Name)
	RETURN IDENT_CURRENT('Country')
