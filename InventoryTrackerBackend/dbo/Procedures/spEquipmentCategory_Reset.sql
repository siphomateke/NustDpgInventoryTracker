CREATE PROCEDURE [dbo].[spEquipmentCategory_Reset]
	@EquipmentId int
AS
	SET NOCOUNT ON;

	DELETE FROM EquipmentCategory
	WHERE EquipmentId = @EquipmentId;
