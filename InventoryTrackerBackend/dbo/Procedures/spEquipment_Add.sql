CREATE PROCEDURE [dbo].[spEquipment_Add]
    @Name              VARCHAR (50),
    @Description       VARCHAR (500)  = NULL,
    @Quantity          INT            = NULL,
    @LocationInHome    VARCHAR (250)  = NULL,
    @Lost              BIT            = NULL,
    @ConditionId       INT            = NULL,
    @Age               INT            = NULL,
    @DateOfPurchase    DATE           = NULL,
    @ReceiptImage      VARCHAR (4096) = NULL,
    @WarrantyExpiryDate DATE           = NULL,
    @WarrantyImage     VARCHAR (4096) = NULL,

    -- Optional default price information:
    @ShopId             INT = NULL,
    @EquipmentPrice     INT = NULL,
    @DatePriceChecked   DATETIME = NULL,
    @IsOriginalPurchase BIT = NULL
AS
    -- TODO: Use transaction?
	INSERT INTO Equipment 
        (Name, Description, Quantity, LocationInHome, Lost, ConditionId, Age, DateOfPurchase, ReceiptImage, WarrantyExpiryDate, WarrantyImage)
    VALUES 
        (@Name, @Description, @Quantity, @LocationInHome, @Lost, @ConditionId, @Age, @DateOfPurchase, @ReceiptImage, @WarrantyExpiryDate, @WarrantyImage);

    DECLARE @EquipmentId AS INT = IDENT_CURRENT('Category');
    -- Add a single row for price information. 
    -- If more information about shop prices is known, they should be added later
    EXEC spEquipmentPricing_Set @ShopId, @EquipmentId, @EquipmentPrice, @DatePriceChecked, @IsOriginalPurchase;
