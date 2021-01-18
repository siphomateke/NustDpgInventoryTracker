CREATE TABLE [dbo].[NewEquipmentPricing] (
    [NewEquipmentId]   INT      NULL,
    [ShopId]           INT      NULL,
    [EquipmentPrice]   INT      NULL,
    [DatePriceChecked] DATETIME NULL,
    FOREIGN KEY ([NewEquipmentId]) REFERENCES [dbo].[NewEquipment] ([NewEquipmentId]),
    FOREIGN KEY ([ShopId]) REFERENCES [dbo].[Shop] ([ShopId])
);

