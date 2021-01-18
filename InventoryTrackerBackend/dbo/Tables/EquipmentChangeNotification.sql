CREATE TABLE [dbo].[EquipmentChangeNotification] (
    [EquipmentChangeNotificationId] INT           IDENTITY (0, 1) NOT NULL,
    [UserId]                        INT           NOT NULL,
    [EquipmentChangeId]             INT           NOT NULL,
    [Title]                         VARCHAR (100) NULL,
    [Description]                   VARCHAR (250) NULL,
    PRIMARY KEY CLUSTERED ([EquipmentChangeNotificationId] ASC),
    FOREIGN KEY ([EquipmentChangeId]) REFERENCES [dbo].[EquipmentChange] ([EquipmentChangeId]),
    FOREIGN KEY ([UserId]) REFERENCES [dbo].[AppUser] ([UserId])
);

