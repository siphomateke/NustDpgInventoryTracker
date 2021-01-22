CREATE PROCEDURE spCategory_Add
	@Name VARCHAR (100),
    @Description VARCHAR (250) = NULL
AS
	SET NOCOUNT ON
	INSERT INTO Category (Name, Description) VALUES (@Name, @Description)
	-- Return ID of added Category so it can be linked to equipment
	RETURN IDENT_CURRENT('Category')
