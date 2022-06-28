/****** Object:  StoredProcedure [dbo].[up_InsertIndentPartHeader]    Script Date: 27/08/2018 16:44:05 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER ON
GO


---------------------------------------------------------------------------------------------------------------
-- Date Created	: Friday, December 12, 2008
-- Created By	: DNet Team by using CodeSmith v 2.6
-- Rev History	:
---------------------------------------------------------------------------------------------------------------

ALTER PROCEDURE [dbo].[up_InsertIndentPartHeader]
	@ID int OUTPUT,
	@DealerID int,
	@TermOfPaymentID int,
	@TOPBlockStatusID int,
	@RequestNo varchar(13),
	@RequestDate datetime,
	@MaterialType int,
	@Status tinyint,
	@StatusKTB tinyint,
	@SubmitFile varchar(50),
	@PaymentType tinyint,
	@Price money,
	@KTBConfirmedDate datetime,
	@DescID tinyint,
	@ChassisNumber varchar(20),
	@RowStatus smallint,
	@CreatedBy varchar(20),
	--@CreatedTime datetime,
	@LastUpdateBy varchar(20)	--@LastUpdateTime datetime	
AS

set @DealerID = ISNULL(@DealerID,0)
if(@DealerID=0)
begin
	set @ID=-1
end 
else
begin
	Declare @IPNoGenerated  varchar(18)
	set @IPNoGenerated=dbo.ufn_CreateIndentPartNumber(@RequestDate,@DealerId)

	INSERT	INTO	[dbo].[IndentPartHeader]
	VALUES
	(
		@DealerID,
		@IPNoGenerated,
		@RequestDate,
		@MaterialType,
		@TermOfPaymentID,
		@TOPBlockStatusID,
		@Status,
		@StatusKTB,
		@SubmitFile,
		@PaymentType,
		@Price,
		@KTBConfirmedDate,
		@DescID,
		@ChassisNumber,
		@RowStatus,
		@CreatedBy,
		GETDATE(),	
		@LastUpdateBy,
		GETDATE())

		
	SET @ID = @@IDENTITY

end
