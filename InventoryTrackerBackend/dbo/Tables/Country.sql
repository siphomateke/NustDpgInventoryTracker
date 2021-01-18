CREATE TABLE [dbo].[Country] (
    [CountryId] INT          IDENTITY (0, 1) NOT NULL,
    [Name]      VARCHAR (50) NULL,
    PRIMARY KEY CLUSTERED ([CountryId] ASC)
);

