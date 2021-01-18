CREATE TABLE [dbo].[EquipmentPricing] (
    [ShopId]             INT      NULL,
    [EquipmentId]        INT      NULL,
    [EquipmentPrice]     INT      NULL,
    [DatePriceChecked]   DATETIME NULL,
    [IsOriginalPurchase] BIT      NULL,
    FOREIGN KEY ([EquipmentId]) REFERENCES [dbo].[Equipment] ([EquipmentId]),
    FOREIGN KEY ([ShopId]) REFERENCES [dbo].[Shop] ([ShopId])
);

