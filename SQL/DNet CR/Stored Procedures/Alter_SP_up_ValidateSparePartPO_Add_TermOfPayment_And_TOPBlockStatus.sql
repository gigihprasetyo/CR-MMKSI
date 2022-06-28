/****** Object:  StoredProcedure [dbo].[up_ValidateSparePartPO]    Script Date: 23/08/2018 16:42:18 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER ON
GO

---------------------------------------------------------------------------------------------------------------
-- Date Created	: Thursday, August 04, 2011
-- Created By	: DNet Team by using CodeSmith v 2.6
-- Rev History	:
---------------------------------------------------------------------------------------------------------------

ALTER PROCEDURE [dbo].[up_ValidateSparePartPO]
	@Result	varchar(1000),
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
	@IsTransfer BIT	,
	@RowStatus smallint,
	@CreatedBy varchar(20),
	@CreatedTime datetime,
	@LastUpdateBy varchar(20),
	@LastUpdateTime datetime	
AS

SET	@Result = ''
