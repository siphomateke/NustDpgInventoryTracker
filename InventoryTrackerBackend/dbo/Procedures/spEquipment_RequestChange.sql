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
    @WarrantyImage         VARCHAR (4096) = NULL,
    @EquipmentChangeId INT = NULL OUTPUT
AS
    BEGIN TRANSACTION
        DECLARE @ChangeDate DATE = GETDATE();

        DECLARE @CanEdit BIT;
        EXEC spUser_EnsureCanEditEquipment @UserId, @EquipmentId, @CanEdit OUTPUT;
        IF @CanEdit = 1
        BEGIN
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
                );
            SET @EquipmentChangeId = IDENT_CURRENT('EquipmentChange');
        END
        ELSE
        BEGIN
            RAISERROR('User does not have permission to edit this equipment', 16, 1);
        END
    COMMIT;
