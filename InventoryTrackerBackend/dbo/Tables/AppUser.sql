CREATE TABLE [dbo].[AppUser] (
    [UserId]    INT           IDENTITY (0, 1) NOT NULL,
    [FirstName] VARCHAR (50)  NOT NULL,
    [LastName]  VARCHAR (50)  NOT NULL,
    [Email]     VARCHAR (150) NULL,
    [Username]  VARCHAR (100) NOT NULL,
    [Password]  VARCHAR (32)  NOT NULL,
    [RoleId]    INT           NOT NULL,
    PRIMARY KEY CLUSTERED ([UserId] ASC),
    FOREIGN KEY ([RoleId]) REFERENCES [dbo].[AppUserRole] ([RoleId])
);

