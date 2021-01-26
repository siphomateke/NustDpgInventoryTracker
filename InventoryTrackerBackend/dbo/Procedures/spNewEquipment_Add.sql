-- Allows users to request new equipment to be bought
CREATE PROCEDURE [dbo].[spNewEquipment_Add]
	@UserId INT,
	@Name VARCHAR (50),
	@Description VARCHAR (500),
	@Quantity INT,
	@NewEquipmentId INT = NULL OUTPUT
AS
	DECLARE @CanRequest BIT;
	EXEC spUser_CanRequestNewEquipment @UserId, @CanRequest OUTPUT;

	IF @CanRequest = 0
		RAISERROR('User does not have permission to request new equipment to be bought', 16, 1);
	ELSE
		-- Actually make the request for new equipment
		INSERT INTO NewEquipment
			(AddedByUserId, Name, Description, Quantity, Approved, ApprovedByUserId)
		VALUES
			(@UserId, @Name, @Description, @Quantity, NULL, NULL);

		SET @NewEquipmentId = IDENT_CURRENT('NewEquipment');
