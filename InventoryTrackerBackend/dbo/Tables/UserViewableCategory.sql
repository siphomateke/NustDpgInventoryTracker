CREATE TABLE [dbo].[UserViewableCategory] (
    [UserId]     INT NOT NULL,
    [CategoryId] INT NOT NULL,
    FOREIGN KEY ([CategoryId]) REFERENCES [dbo].[Category] ([CategoryId]),
    FOREIGN KEY ([UserId]) REFERENCES [dbo].[AppUser] ([UserId])
);

