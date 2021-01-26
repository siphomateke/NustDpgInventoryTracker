CREATE VIEW [dbo].[v_EquipmentToBuy]
	AS SELECT Equipment.* FROM Equipment	
	INNER JOIN EquipmentCondition
	ON (Equipment.ConditionId = EquipmentCondition.ConditionId)
	WHERE Quantity = 0
	OR Lost = 1
	OR EquipmentCondition.Name = 'TERRIBLE';