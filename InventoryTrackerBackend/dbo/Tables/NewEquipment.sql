CREATE TABLE [dbo].[NewEquipment] (
    [NewEquipmentId]   INT           IDENTITY (0, 1) NOT NULL,
    [AddedByUserId]    INT           NULL,
    [Name]             VARCHAR (50)  NULL,
    [Description]      VARCHAR (250) NULL,
    [Quantity]         INT           NULL,
    [Approved]         BIT           NULL,
    [ApprovedByUserId] INT           NULL,
    PRIMARY KEY CLUSTERED ([NewEquipmentId] ASC),
    FOREIGN KEY ([AddedByUserId]) REFERENCES [dbo].[AppUser] ([UserId]),
    FOREIGN KEY ([ApprovedByUserId]) REFERENCES [dbo].[AppUser] ([UserId])
);

