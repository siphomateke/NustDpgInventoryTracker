CREATE PROCEDURE [dbo].[spEquipmentCondition_Add]
	@Name VARCHAR (50)
AS
	SET NOCOUNT ON
	INSERT INTO EquipmentCondition (Name) VALUES (@Name)
	RETURN IDENT_CURRENT('EquipmentCondition')
