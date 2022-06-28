GO
/****** Object:  StoredProcedure [dbo].[up_InsertSparePartPO]    Script Date: 23/08/2018 13:30:11 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER ON
GO

---------------------------------------------------------------------------------------------------------------
-- Date Created	: Friday, December 28, 2007
-- Created By	: DNet Team by using CodeSmith v 2.6
-- Rev History	:
---------------------------------------------------------------------------------------------------------------

ALTER PROCEDURE [dbo].[up_InsertSparePartPO]
	@ID int OUTPUT,
	@PONumber varchar(15),
	@OrderType varchar(1),
	@DealerID int,
	@TermOfPaymentID int,
	@TOPBlockStatusID int,
	@PODate smalldatetime,
	@DeliveryDate datetime,
	@ProcessCode varchar(1),
	@CancelRequestBy varchar(20),
	@IndentTransfer smallint,
	@PickingTicket varchar(100),
	@SentPODate DATETIME,
	@IsTransfer BIT	,
	@RowStatus smallint,
	@CreatedBy varchar(20),
	--@CreatedTime datetime,
	@LastUpdateBy varchar(20)
	--@LastUpdateTime datetime
	
AS
INSERT	INTO	[dbo].[SparePartPO]
(  PONumber, OrderType, DealerID, TermOfPaymentID, TOPBlockStatusID, PODate, DeliveryDate, ProcessCode, CancelRequestBy, IndentTransfer, 
PickingTicket, SentPODate, IsTransfer, RowStatus, CreatedBy, CreatedTime, LastUpdateBy, LastUpdateTime
)
VALUES
(
	dbo.ufn_CreateSparePartPONumber (@OrderType,@DealerID,@PODate,@PONumber),
	@OrderType,
	@DealerID,
	@TermOfPaymentID,
	@TOPBlockStatusID,
	@PODate,
	@DeliveryDate,
	@ProcessCode,
	@CancelRequestBy,
	@IndentTransfer,
	@PickingTicket,
	@SentPODate,
	@IsTransfer,
	@RowStatus,
	@CreatedBy,
	GETDATE(),	
	@LastUpdateBy,
	GETDATE()
)

	
SET @ID = @@IDENTITY
