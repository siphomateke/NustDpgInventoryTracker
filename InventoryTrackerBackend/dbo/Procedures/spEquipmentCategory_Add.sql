CREATE PROCEDURE [dbo].[spEquipmentCategory_Add]
	@EquipmentId int,
	@CategoryId int
AS
	SET NOCOUNT ON
	INSERT INTO EquipmentCategory VALUES (@EquipmentId, @CategoryId)
