CREATE TABLE [dbo].[EquipmentNotificationType] (
    [EquipmentNotificationTypeId] INT           IDENTITY (0, 1) NOT NULL,
    [Name]                        VARCHAR (50)  NULL,
    [Description]                 VARCHAR (200) NULL,
    PRIMARY KEY CLUSTERED ([EquipmentNotificationTypeId] ASC)
);

