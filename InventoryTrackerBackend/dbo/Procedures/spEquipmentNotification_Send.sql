CREATE PROCEDURE [dbo].[spEquipmentNotification_Send]
	@EquipmentId INT,
	@NotificationTypeName VARCHAR (50),
	@Title VARCHAR (100) = NULL,
	@Description VARCHAR (250) = NULL
AS
	-- Get notification type ID from name
	DECLARE @NotificationTypeId INT;
	SELECT @NotificationTypeId = EquipmentNotificationTypeId
	FROM EquipmentNotificationType
	WHERE Name = @NotificationTypeName;

	-- Get equipment name for use in notification title
	DECLARE @EquipmentName VARCHAR (50);
	SELECT @EquipmentName = Name FROM Equipment WHERE EquipmentId = @EquipmentId;
	IF @EquipmentName is NULL
		SET @EquipmentName = 'Equipment';

	-- Auto generate equipment notification titles from notification type and equipment name
	IF @Title is NULL
		IF @NotificationTypeName = 'NEEDS_REPAIR'
			SET @Title = CONCAT(@EquipmentName + ' needs repairing');
		ELSE IF @NotificationTypeName = 'OUT_OF_STOCK'
			SET @Title = CONCAT(@EquipmentName + ' is out of stock');
		ELSE IF @NotificationTypeName = 'RUNNING_LOW'
			SET @Title = CONCAT(@EquipmentName + '''s stock is running low');
	
	-- Send notification to all users who can view the equipment and thus might be interested
	INSERT INTO EquipmentNotification
		(UserId, EquipmentId, Title, Description, NotificationTypeId)
	SELECT 
		UserViewableCategory.UserId, @EquipmentId, @Title, @Description, @NotificationTypeId
	FROM EquipmentCategory
	INNER JOIN UserViewableCategory
	ON (UserViewableCategory.CategoryId = EquipmentCategory.CategoryId)
	WHERE EquipmentCategory.EquipmentId = @EquipmentId;
