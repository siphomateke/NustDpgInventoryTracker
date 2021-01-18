CREATE TABLE [dbo].[AppUserRole] (
    [RoleId] INT          IDENTITY (0, 1) NOT NULL,
    [Name]   VARCHAR (50) NULL,
    PRIMARY KEY CLUSTERED ([RoleId] ASC)
);

