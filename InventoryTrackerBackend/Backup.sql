DROP DATABASE InventoryTracker;
CREATE DATABASE InventoryTracker;

USE InventoryTracker;

CREATE TABLE EquipmentCondition(
    ConditionId INT IDENTITY (0,1) PRIMARY KEY,
    Name VARCHAR (50),
);

CREATE TABLE Permission(
    PermissionId INT IDENTITY (0,1) PRIMARY KEY,
    Name VARCHAR(50),
    Description VARCHAR(250),
);

CREATE TABLE AppUserRole(
    RoleId INT IDENTITY (0,1) PRIMARY KEY,
    Name VARCHAR(50),
);

CREATE TABLE RolePermission(
    RoleId INT FOREIGN KEY REFERENCES AppUserRole(RoleId),
    PermissionId INT FOREIGN KEY REFERENCES Permission(PermissionId),
);

CREATE TABLE AppUser(
    UserId INT IDENTITY (0,1) PRIMARY KEY,
    FirstName VARCHAR(50) NOT NULL,
    LastName VARCHAR(50) NOT NULL,
    Email VARCHAR(150),
    Username VARCHAR(100) NOT NULL,
    Password VARCHAR(32) NOT NULL,
    RoleId INT NOT NULL FOREIGN KEY REFERENCES AppUserRole(RoleId),
);

CREATE TABLE Category(
    CategoryId INT IDENTITY (0,1) PRIMARY KEY,
    Name VARCHAR(100) NOT NULL,
    Description VARCHAR(250)
);

CREATE TABLE UserViewableCategory(
    UserId INT NOT NULL FOREIGN KEY REFERENCES AppUser(UserId),
    CategoryId INT NOT NULL FOREIGN KEY REFERENCES Category(CategoryId),
);

CREATE TABLE Equipment (
    EquipmentId INT IDENTITY (0,1) PRIMARY KEY,
    Name VARCHAR(50) NOT NULL,
    Description VARCHAR(500),
    Quantity INT,
    LocationInHome VARCHAR(250),
    Lost BIT,
    ConditionId INT FOREIGN KEY REFERENCES EquipmentCondition(ConditionId),
    Age INT,
    DateOfPurchase DATE,
    ReceiptImage VARCHAR(4096),
    WarrantyExpiryDate DATE,
    WarrantyImage VARCHAR(4096)
);

CREATE TABLE EquipmentCategory (
    EquipmentId INT FOREIGN KEY REFERENCES Equipment(EquipmentId),
    CategoryId INT FOREIGN KEY REFERENCES Category(CategoryId)
);

CREATE TABLE EquipmentChange (
    EquipmentChangeId INT IDENTITY (0,1) PRIMARY KEY,
    EquipmentId INT NOT NULL FOREIGN KEY REFERENCES Equipment(EquipmentId),
    ChangedByUserId INT NOT NULL FOREIGN KEY REFERENCES AppUser(UserId),
    ChangeApprovedByUserId INT FOREIGN KEY REFERENCES AppUser(UserId),
    ChangeApproved BIT NOT NULL,
    ChangeDate DATE NOT NULL,
    Name VARCHAR(50),
    Description VARCHAR(500),
    Quantity INT,
    LocationInHome VARCHAR(250),
    Lost BIT,
    ConditionId INT FOREIGN KEY REFERENCES EquipmentCondition(ConditionId),
    Age INT,
    DateOfPurchase DATE,
    ReceiptImage VARCHAR(4096),
    WarrantyExpiryDate DATE,
    WarrantyImage VARCHAR(4096)
);

CREATE TABLE EquipmentChangeNotification(
    EquipmentChangeNotificationId INT IDENTITY (0,1) PRIMARY KEY,
    UserId INT NOT NULL FOREIGN KEY REFERENCES AppUser(UserId),
    EquipmentChangeId INT NOT NULL FOREIGN KEY REFERENCES EquipmentChange(EquipmentChangeId),
    Title VARCHAR(100),
    Description VARCHAR(250)
);

CREATE TABLE EquipmentNotificationType(
    EquipmentNotificationTypeId INT IDENTITY (0,1) PRIMARY KEY,
    Name VARCHAR(50),
    Description VARCHAR(200)
);

CREATE TABLE EquipmentNotification(
    EquipmentNotificationId INT IDENTITY (0,1) PRIMARY KEY,
    UserId INT NOT NULL FOREIGN KEY REFERENCES AppUser(UserId),
    EquipmentId INT NOT NULL FOREIGN KEY REFERENCES Equipment(EquipmentId),
    Title VARCHAR(100),
    Description VARCHAR(250),
    NotificationTypeId INT NOT NULL FOREIGN KEY REFERENCES EquipmentNotificationType(EquipmentNotificationTypeId),
);

CREATE TABLE Country(
    CountryId INT IDENTITY (0,1) PRIMARY KEY,
    Name VARCHAR (50),
);

CREATE TABLE Shop(
    ShopId INT IDENTITY (0,1) PRIMARY KEY,
    Name VARCHAR(50),
    PhoneNumber INT,
    Email VARCHAR(50),
    Comments VARCHAR(250),
    StreetAdress VARCHAR(250),
    Town VARCHAR(50),
    CountryId INT FOREIGN KEY REFERENCES Country(CountryId),
);

CREATE TABLE EquipmentPricing(
    ShopId INT FOREIGN KEY REFERENCES Shop(ShopId),
    EquipmentId INT FOREIGN KEY REFERENCES Equipment(EquipmentId),
    EquipmentPrice INT,
    DatePriceChecked DATETIME,
    IsOriginalPurchase BIT,
);

CREATE TABLE NewEquipment(
    NewEquipmentId INT IDENTITY (0,1) PRIMARY KEY,
    AddedByUserId INT FOREIGN KEY REFERENCES AppUser(UserId),
    Name VARCHAR(50),
    Description VARCHAR(250),
    Quantity INT,
    Approved BIT,
    ApprovedByUserId INT FOREIGN KEY REFERENCES AppUser(UserId),
);

CREATE TABLE NewEquipmentPricing(
    NewEquipmentId INT FOREIGN KEY REFERENCES NewEquipment(NewEquipmentId),
    ShopId INT FOREIGN KEY REFERENCES Shop(ShopId),
    EquipmentPrice INT,
    DatePriceChecked DATETIME,
);

BEGIN TRY;
    BEGIN TRANSACTION;
		DISABLE TRIGGER ALL ON DATABASE;

		INSERT INTO Permission
			(Name, Description)
		VALUES
			('READ', 'Can view information about equipment.'),
			('DELETE', 'Can delete equipment data.'),
			('CAN_MAKE_CHANGES', 'Can directly update equipment data.'),
			('CAN_APPROVE_CHANGES', 'Can approve requests to change equipment data.'),
			('CAN_ASK_FOR_CHANGES', 'Can make changes to equipment data after approval.'),
			('MODIFY_PERMISSIONS', 'Can change users'' or roles'' permissions'),
			('MANAGE_USERS', 'Can add, remove or update users.'),
			('CAN_ASK_FOR_EQUIPMENT', 'Can request for new equipment to be bought.'),
			('CAN_APPROVE_NEW_EQUIPMENT', 'Can mark requests for new equipment as resolved.')

		INSERT INTO AppUserRole
			(Name)
		VALUES
			('OWNER'),
			('ADMIN'),
			('GUEST');

		INSERT INTO RolePermission
			(RoleId, PermissionId)
		VALUES
			(0, 0),
			(0, 1),
			(0, 2),
			(0, 3),
			(0, 4),
			(0, 5),
			(0, 6),
			(1, 0),
			(1, 1),
			(1, 2),
			(1, 3),
			(1, 4),
			(2, 0),
			(2, 4);

		INSERT INTO AppUser
			(FirstName, LastName, Email, Username, Password, RoleId)
		VALUES
			('Sipho', 'Mateke', 'siphomateke@gmail.com', 'sipho', 'password', 0),
			('Jeremy', 'Renner', 'jeremy.renner@gmail.com', 'jeremy', 'luke27', 1),
			('Vaquem', 'Windex', 'ms.windex@gmail.com', 'windex5000', 'broom4life', 2),
			('Spade', 'Diggory', 'cedricdiggory@gmail.com', 'cedric', 'avadacadavra', 2);

		INSERT INTO Category
			(Name, Description)
		VALUES
			('Construction', NULL),
			('Gardening', NULL),
			('Camping', NULL),
			('Sports', NULL),
			('Office', NULL),
			('Cleaning', NULL);

		INSERT INTO UserViewableCategory
			(UserId, CategoryId)
		VALUES
			(0, 0),
			(0, 1),
			(0, 2),
			(0, 3),
			(0, 4),
			(0, 5),
			(1, 0),
			(1, 1),
			(1, 2),
			(1, 3),
			(1, 4),
			(1, 5),
			(2, 5),
			(3, 1);

		INSERT INTO EquipmentCondition
			(Name)
		VALUES
			('TERRIBLE'),
			('POOR'),
			('DECENT'),
			('GOOD'),
			('EXCELLENT'),
			('PERFECT');

		INSERT INTO Equipment
			(Name, Description, Quantity, LocationInHome, Lost, ConditionId, Age, DateOfPurchase, ReceiptImage, WarrantyExpiryDate, WarrantyImage)
		VALUES
			('Shovel', NULL, 5, 'Garden shed', 0, 3, 5, '2020/12/05', '/www/images/receipt-1238dan3n1li.jpg', '2025/11/04', '/www/images/warranty127x9a-242J.png'),
			('Hoe', 'Tool for digging', 5, 'Garden shed', 0, 5, 0, '2020/12/12', '/www/images/receipt-123asdml.jpg', '2025/12/04', '/www/images/warrantyasmkQWd242J.png'),
			('Spade', NULL, 1, 'Large kitchen cupboard', 1, 4, 730, NULL, NULL, NULL, NULL),
			('Windex', NULL, 5, 'Pantry', 0, 4, 300, NULL, NULL, NULL, NULL);

		INSERT INTO EquipmentCategory
			(EquipmentId, CategoryId)
		VALUES
			(0, 1),
			(3, 5);

		INSERT INTO NewEquipment
			(AddedByUserId, Name, Description, Quantity, Approved, ApprovedByUserId)
		VALUES
			(3, 'Lawn mower', NULL, 1, 0, NULL),
			(3, 'Hoe', 'Tool for digging', 2, 1, 0);

		INSERT INTO Country
			(Name)
		VALUES
			('Namibia'),
			('South Africa'),
			('Germany'),
			('Zambia'),
			('Zimbabwe');

		INSERT INTO Shop
			(Name, PhoneNumber, Email, Comments, StreetAdress, Town, CountryId)
		VALUES
			('Game', 0861426333, 'gamecentre@gmail.com', 'Has good deals on cheap products', 'David Hosea Rd', 'Windhoek', 0),
			('Megabuild', 061256960, 'megabuild@gmail.com', NULL, 'Lazarett St', 'Windhoek', 0),
			('Build It', 061255350, 'buildit123@gmail.com', 'Offers all construction equipments', 'Eugene St ', 'Windhoek', 0);

		PRINT('got this far');

		INSERT INTO EquipmentChange
			(EquipmentId, ChangedByUserId, ChangeApprovedByUserId, ChangeApproved, ChangeDate, Name, Description, Quantity, LocationInHome, Lost, ConditionId, Age, DateOfPurchase, ReceiptImage, WarrantyExpiryDate, WarrantyImage)
		VALUES
			(0, 0, 0, 1, '2020/04/19', 'Self-drill srews', 'good', 12, 'Kitchen cupboards', 0, 0, NULL, '2020/03/12', NULL, '2021/11/28', NULL),
			(1, 1, 1, 1, '2020/09/09', 'Lawn mower', 'Terrible', 1, 'Garage', 0, 1, 3, '2017/12/05', NULL, '2020/06/17', NULL),
			(2, 2, 2, 1, '2020/11/28', 'Spade', 'Good', 3, 'Garage', 0, 2, 1, '2019/09/11', NULL, '2020/05/12', NULL);

		INSERT INTO EquipmentNotificationType
			(Name, Description)
		VALUES
			('NEEDS_REPAIR', 'Sent to remind the user that a piece of equipment needs repairing.'),
			('OUT_OF_STOCK', 'Sent when the stock for some equipment is depleted.'),
			('RUNNING_LOW', 'Informs the user that some equipment will need topping up soon.');

		INSERT INTO EquipmentNotification
			(UserId, EquipmentId, Title, Description, NotificationTypeId)
		VALUES
			(0, 0, 'Shovel needs repairing', NULL, 0),
			(1, 1, 'Out of hoes', 'You are out of hoes. New ones can be bought at game.', 1);

		INSERT INTO EquipmentChangeNotification
			(UserId, EquipmentChangeId, Title, Description)
		VALUES
			(0, 0, 'Shovel has been modified', 'Data about some equipment was changed and needs approval.'),
			(0, 1, 'Lukia changed some details about lawn mower', NULL),
			(0, 2, 'Spade was modified', NULL);

		INSERT INTO EquipmentPricing
			(ShopId, EquipmentId, EquipmentPrice, DatePriceChecked, IsOriginalPurchase)
		VALUES
			(0, 0, 53.47, '2020/12/05', 1),
			(1, 0, 102.5, '2020/12/30', 0),
			(0, 1, 200, '2020/12/12', 1),
			(2, 1, 324.99, '2020/12/27', 0);

		INSERT INTO NewEquipmentPricing
			(ShopId, EquipmentPrice, DatePriceChecked)
		VALUES
			(0, 62.99, '2020/12/17'),
			(1, 95, '2021/01/22'),
			(2, 348.96, '2020/03/06');

		ENABLE TRIGGER ALL ON DATABASE;
    COMMIT TRAN; -- Transaction Success!
END TRY
BEGIN CATCH
    IF @@TRANCOUNT > 0
        ROLLBACK TRAN;
		PRINT('rolled back');

		DECLARE @ErrorMessage NVARCHAR(4000) = ERROR_MESSAGE()
        DECLARE @ErrorSeverity INT = ERROR_SEVERITY()
        DECLARE @ErrorState INT = ERROR_STATE()

    -- Use RAISERROR inside the CATCH block to return error  
    -- information about the original error that caused  
    -- execution to jump to the CATCH block.
    RAISERROR (@ErrorMessage, @ErrorSeverity, @ErrorState);
END CATCH