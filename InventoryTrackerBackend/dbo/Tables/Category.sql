CREATE TABLE [dbo].[Category] (
    [CategoryId]  INT           IDENTITY (0, 1) NOT NULL,
    [Name]        VARCHAR (100) NOT NULL,
    [Description] VARCHAR (250) NULL,
    PRIMARY KEY CLUSTERED ([CategoryId] ASC)
);

