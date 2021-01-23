CREATE PROCEDURE [dbo].[spUser_CanApproveChanges]
	@UserId INT,
	@CanApprove BIT OUTPUT
AS
	IF EXISTS(SELECT 1 FROM  v_ApproverUsers WHERE UserId = @UserId)
		SET @CanApprove = 1
	ELSE
		SET @CanApprove = 0