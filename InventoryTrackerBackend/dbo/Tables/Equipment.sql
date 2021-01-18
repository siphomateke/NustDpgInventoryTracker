CREATE TABLE [dbo].[Equipment] (
    [EquipmentId]        INT            IDENTITY (0, 1) NOT NULL,
    [Name]               VARCHAR (50)   NOT NULL,
    [Description]        VARCHAR (500)  NULL,
    [Quantity]           INT            NULL,
    [LocationInHome]     VARCHAR (250)  NULL,
    [Lost]               BIT            NULL,
    [ConditionId]        INT            NULL,
    [Age]                INT            NULL,
    [DateOfPurchase]     DATE           NULL,
    [ReceiptImage]       VARCHAR (4096) NULL,
    [WarrantyExpiryDate] DATE           NULL,
    [WarrantyImage]      VARCHAR (4096) NULL,
    PRIMARY KEY CLUSTERED ([EquipmentId] ASC),
    FOREIGN KEY ([ConditionId]) REFERENCES [dbo].[EquipmentCondition] ([ConditionId])
);

