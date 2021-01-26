CREATE TRIGGER [TR_Equipment_AfterUpdate]
ON Equipment
AFTER UPDATE
AS
BEGIN
	SET NOCOUNT ON;

	-- Quantity level at which the user will be notified that the equipment's stock is running low.
	DECLARE @MinQuanity INT = 3;

	IF EXISTS(SELECT ConditionId FROM inserted) OR EXISTS(SELECT Quantity FROM inserted)
		DECLARE @EquipmentId INT;
		SELECT @EquipmentId = EquipmentId FROM inserted;

		IF EXISTS(SELECT ConditionId FROM inserted)
			-- Get name of condition
			DECLARE @ConditionId INT; 
			SELECT @ConditionId = ConditionId FROM inserted;
			DECLARE @Condition VARCHAR (50);
			SELECT @Condition = Name FROM EquipmentCondition WHERE ConditionId = @ConditionId;

			IF @Condition = 'TERRIBLE'
				EXEC spEquipmentNotification_Send @EquipmentId, 'NEEDS_REPAIR';

		IF EXISTS(SELECT Quantity FROM inserted)
			DECLARE @Quantity INT;
			SELECT @Quantity = Quantity FROM inserted;

			IF @Quantity < @MinQuanity
				EXEC spEquipmentNotification_Send @EquipmentId, 'RUNNING_LOW';
			ELSE IF @Quantity = 0
				EXEC spEquipmentNotification_Send @EquipmentId, 'OUT_OF_STOCK';
			
END
