CREATE TABLE [dbo].[Shop] (
    [ShopId]       INT           IDENTITY (0, 1) NOT NULL,
    [Name]         VARCHAR (50)  NULL,
    [PhoneNumber]  INT           NULL,
    [Email]        VARCHAR (50)  NULL,
    [Comments]     VARCHAR (250) NULL,
    [StreetAdress] VARCHAR (250) NULL,
    [Town]         VARCHAR (50)  NULL,
    [CountryId]    INT           NULL,
    PRIMARY KEY CLUSTERED ([ShopId] ASC),
    FOREIGN KEY ([CountryId]) REFERENCES [dbo].[Country] ([CountryId])
);

