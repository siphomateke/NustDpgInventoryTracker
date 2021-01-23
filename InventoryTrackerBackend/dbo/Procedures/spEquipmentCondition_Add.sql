CREATE PROCEDURE [dbo].[spEquipmentCondition_Add]
	@Name VARCHAR (50),
	@EquipmentConditionId INT = NULL OUTPUT
AS
	SET NOCOUNT ON;
	INSERT INTO EquipmentCondition (Name) VALUES (@Name);

	SET @EquipmentConditionId = IDENT_CURRENT('EquipmentCondition');
