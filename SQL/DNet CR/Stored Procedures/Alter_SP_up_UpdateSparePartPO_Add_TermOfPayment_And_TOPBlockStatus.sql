/****** Object:  StoredProcedure [dbo].[up_UpdateSparePartPO]    Script Date: 23/08/2018 16:40:30 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER ON
GO

---------------------------------------------------------------------------------------------------------------
-- Date Created	: Thursday, August 04, 2011
-- Created By	: DNet Team by using CodeSmith v 2.6
-- Rev History	:
---------------------------------------------------------------------------------------------------------------

ALTER PROCEDURE [dbo].[up_UpdateSparePartPO]
	@ID int OUTPUT,
	@PONumber varchar(15),
	@OrderType varchar(1),
	@DealerID smallint,
	@TermOfPaymentID int,
	@TOPBlockStatusID int,
	@PODate smalldatetime,
	@DeliveryDate datetime,
	@ProcessCode varchar(1),
	@CancelRequestBy varchar(20),
	@IndentTransfer tinyint,
	@PickingTicket varchar(100),
	@SentPODate DATETIME,
	@IsTransfer BIT ,
	@RowStatus smallint,
	@CreatedBy varchar(20),
	--@CreatedTime datetime,
	@LastUpdateBy varchar(20)	--@LastUpdateTime datetime	
AS
	declare @tmpPONumber as varchar(15)
	
	set @tmpPONumber = @PONumber 
	set @PONumber= dbo.ufn_UpdateSparePartPONumber(@ID)
	if @PONumber='' begin set @PONumber=@tmpPONumber end 

UPDATE	[dbo].[SparePartPO]
SET
	[PONumber] = @PONumber,
	[OrderType] = @OrderType,
	[DealerID] = @DealerID,
	[TermOfPaymentID] = @TermOfPaymentID,
	[TOPBlockStatusID] = @TOPBlockStatusID,
	[PODate] = @PODate,
	[DeliveryDate] = @DeliveryDate,
	[ProcessCode] = @ProcessCode,
	[CancelRequestBy] = @CancelRequestBy,
	[IndentTransfer] = @IndentTransfer,
	[PickingTicket] = @PickingTicket,
	[SentPODate] = @SentPODate,
	[IsTransfer] = @IsTransfer,
	[RowStatus] = @RowStatus,
	[CreatedBy] = @CreatedBy,
	--[CreatedTime] = @CreatedTime,
	[LastUpdateBy] = @LastUpdateBy,
	[LastUpdateTime] = GETDATE()	
WHERE
	[ID] = @ID
