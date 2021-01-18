CREATE TABLE [dbo].[EquipmentCategory] (
    [EquipmentId] INT NULL,
    [CategoryId]  INT NULL,
    FOREIGN KEY ([CategoryId]) REFERENCES [dbo].[Category] ([CategoryId]),
    FOREIGN KEY ([EquipmentId]) REFERENCES [dbo].[Equipment] ([EquipmentId])
);

