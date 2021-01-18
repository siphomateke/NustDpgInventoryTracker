CREATE TABLE [dbo].[RolePermission] (
    [RoleId]       INT NULL,
    [PermissionId] INT NULL,
    FOREIGN KEY ([PermissionId]) REFERENCES [dbo].[Permission] ([PermissionId]),
    FOREIGN KEY ([RoleId]) REFERENCES [dbo].[AppUserRole] ([RoleId])
);

