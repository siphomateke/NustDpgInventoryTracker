﻿CREATE TABLE [dbo].[EquipmentChange] (
    [EquipmentChangeId]      INT            IDENTITY (0, 1) NOT NULL,
    [EquipmentId]            INT            NOT NULL,
    [ChangedByUserId]        INT            NOT NULL,
    [ChangeApprovedByUserId] INT            NULL,
    [ChangeApproved]         BIT            NOT NULL,
    [ChangeDate]             DATE           NOT NULL,
    [Name]                   VARCHAR (50)   NULL,
    [Description]            VARCHAR (500)  NULL,
    [Quantity]               INT            NULL,
    [LocationInHome]         VARCHAR (250)  NULL,
    [Lost]                   BIT            NULL,
    [ConditionId]            INT            NULL,
    [Age]                    INT            NULL,
    [DateOfPurchase]         DATE           NULL,
    [ReceiptImage]           VARCHAR (4096) NULL,
    [WarrantyExpiryDate]     DATE           NULL,
    [WarrantyImage]          VARCHAR (4096) NULL,
    PRIMARY KEY CLUSTERED ([EquipmentChangeId] ASC),
    FOREIGN KEY ([ChangeApprovedByUserId]) REFERENCES [dbo].[AppUser] ([UserId]),
    FOREIGN KEY ([ChangedByUserId]) REFERENCES [dbo].[AppUser] ([UserId]),
    FOREIGN KEY ([ConditionId]) REFERENCES [dbo].[EquipmentCondition] ([ConditionId]),
    FOREIGN KEY ([EquipmentId]) REFERENCES [dbo].[Equipment] ([EquipmentId])
);
