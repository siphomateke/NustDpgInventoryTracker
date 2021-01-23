CREATE PROCEDURE [dbo].[spEquipment_RequestChange]
	@EquipmentId INT,
    @UserId INT,
    @Name                  VARCHAR (50)   = NULL,
    @Description           VARCHAR (500)  = NULL,
    @Quantity              INT            = NULL,
    @LocationInHome        VARCHAR (250)  = NULL,
    @Lost                  BIT            = NULL,
    @ConditionId           INT            = NULL,
    @Age                   INT            = NULL,
    @DateOfPurchase        DATE           = NULL,
    @ReceiptImage          VARCHAR (4096) = NULL,
    @WarrantyExpiryDate    DATE           = NULL,
    @WarrantyImage         VARCHAR (4096) = NULL
AS
    BEGIN TRANSACTION;
        DECLARE @CanEdit BIT
        EXEC @CanEdit = spUser_CanEditEquipment @UserId
        DECLARE @CanView BIT
        EXEC @CanView = spUser_CanViewEquipment @UserId, @EquipmentId

        PRINT(@CanEdit)

        IF @CanEdit = 0 OR @CanView = 0
            IF @CanEdit = 0
                RAISERROR('User does not have permission to edit equipment.', 16, 1)
            IF @CanView = 0
                RAISERROR('User does not have permission to edit this equipment.', 16, 1)
        ELSE
            DECLARE @ChangeDate DATE = GETDATE()

            INSERT INTO EquipmentChange
                (
                    EquipmentId,
                    ChangedByUserId,
                    ChangeApprovedByUserId,
                    ChangeApproved,
                    ChangeDate,
                    Name,
                    Description,
                    Quantity,
                    LocationInHome,
                    Lost,
                    ConditionId,
                    Age,
                    DateOfPurchase,
                    ReceiptImage,
                    WarrantyExpiryDate,
                    WarrantyImage       
                )
            VALUES
                (
                    @EquipmentId,
                    @UserId,
                    NULL,
                    0,
                    @ChangeDate,
                    @Name,
                    @Description,
                    @Quantity,
                    @LocationInHome,
                    @Lost,
                    @ConditionId,
                    @Age,
                    @DateOfPurchase,
                    @ReceiptImage,
                    @WarrantyExpiryDate,
                    @WarrantyImage
                )

            RETURN IDENT_CURRENT('EquipmentChange');
    COMMIT;  
