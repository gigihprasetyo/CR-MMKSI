GO
/****** Object:  StoredProcedure [dbo].[up_RetrieveSparePartPO]    Script Date: 23/08/2018 16:38:01 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER ON
GO

---------------------------------------------------------------------------------------------------------------
-- Date Created	: Thursday, August 04, 2011
-- Created By	: DNet Team by using CodeSmith v 2.6
-- Rev History	:
---------------------------------------------------------------------------------------------------------------

ALTER PROCEDURE [dbo].[up_RetrieveSparePartPO]
	@ID int OUTPUT	
AS
SET TRANSACTION ISOLATION LEVEL READ COMMITTED

SET NOCOUNT ON

SELECT
	[ID],
	[PONumber],
	[OrderType],
	[TermOfPaymentID],
	[TOPBlockStatusID],
	[DealerID],
	[PODate],
	[DeliveryDate],
	[ProcessCode],
	[CancelRequestBy],
	[IndentTransfer],
	[PickingTicket],
	[SentPODate],
	[IsTransfer],
	[RowStatus],
	[CreatedBy],
	[CreatedTime],
	[LastUpdateBy],
	[LastUpdateTime]	
FROM	[dbo].[SparePartPO]

WHERE
	[ID] = @ID

SET NOCOUNT OFF
