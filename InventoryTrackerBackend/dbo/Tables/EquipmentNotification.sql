CREATE TABLE [dbo].[EquipmentNotification] (
    [EquipmentNotificationId] INT           IDENTITY (0, 1) NOT NULL,
    [UserId]                  INT           NOT NULL,
    [EquipmentId]             INT           NOT NULL,
    [Title]                   VARCHAR (100) NULL,
    [Description]             VARCHAR (250) NULL,
    [NotificationTypeId]      INT           NOT NULL,
    PRIMARY KEY CLUSTERED ([EquipmentNotificationId] ASC),
    FOREIGN KEY ([EquipmentId]) REFERENCES [dbo].[Equipment] ([EquipmentId]),
    FOREIGN KEY ([NotificationTypeId]) REFERENCES [dbo].[EquipmentNotificationType] ([EquipmentNotificationTypeId]),
    FOREIGN KEY ([UserId]) REFERENCES [dbo].[AppUser] ([UserId])
);

