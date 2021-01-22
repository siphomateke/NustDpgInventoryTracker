CREATE PROCEDURE [dbo].[spShop_Add]
    @Name         VARCHAR (50),
    @PhoneNumber  INT = NULL,
    @Email        VARCHAR (50) = NULL,
    @Comments     VARCHAR (250) = NULL,
    @StreetAdress VARCHAR (250) = NULL,
    @Town         VARCHAR (50) = NULL,
    @CountryId    INT  = NULL
AS
	INSERT INTO Shop (Name, PhoneNumber, Email, Comments, StreetAdress, Town, CountryId)
    VALUES (@Name, @PhoneNumber, @Email, @Comments, @StreetAdress, @Town, @CountryId)
    RETURN IDENT_CURRENT('Shop')
RETURN 0
