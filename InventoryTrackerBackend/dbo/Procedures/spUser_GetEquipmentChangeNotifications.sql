CREATE PROCEDURE [dbo].[spUser_GetEquipmentChangeNotifications]
	@UserId INT
AS
	SELECT * FROM EquipmentChangeNotification
	WHERE UserId = @UserId;
