CREATE TABLE [dbo].[Permission] (
    [PermissionId] INT           IDENTITY (0, 1) NOT NULL,
    [Name]         VARCHAR (50)  NULL,
    [Description]  VARCHAR (250) NULL,
    PRIMARY KEY CLUSTERED ([PermissionId] ASC)
);

