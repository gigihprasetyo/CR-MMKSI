


SET QUOTED_IDENTIFIER ON 
GO
SET ANSI_NULLS OFF 
GO
---------------------------------------------------------------------------------------------------------------
-- Date Created	: 23 Maret 2018
-- Created By	: DNet Team by using CodeSmith v 2.6
-- Rev History	:
---------------------------------------------------------------------------------------------------------------

CREATE PROCEDURE [dbo].up_InsertARReceiptDetail
	@ID int OUTPUT,
	@ARReceiptID int,
	@Owner varchar(100),
	@DetailNo varchar(50),
	@ARReceiptNo varchar(100),
	@BU varchar(100),
	@ChangeAmount money,
	@Customer varchar(100),
	@Description varchar(100),
	@DifferenceValue float,
	@InvoiceNo varchar(100),
	@OrderDate datetime,
	@OrderNo varchar(100),
	@OrderNoSO varchar(100),
	@OrderNoUVSO varchar(100),
	@OrderNoWO varchar(100),
	@OutstandingBalance money,
	@PaidBackToCustomer bit,
	@ReceiptAmount money,
	@RemainingBalance money,
	@SourceType smallint,
	@TransactionDocument varchar(100),
	@RowStatus smallint,
	@CreatedBy varchar(100),
	--@CreatedTime datetime,
	@LastUpdateBy varchar(100)	--@LastUpdateTime datetime	
AS
INSERT	INTO	[dbo].[ARReceiptDetail]
VALUES
(
	@ARReceiptID,
	@Owner,
	@DetailNo,
	@ARReceiptNo,
	@BU,
	@ChangeAmount,
	@Customer,
	@Description,
	@DifferenceValue,
	@InvoiceNo,
	@OrderDate,
	@OrderNo,
	@OrderNoSO,
	@OrderNoUVSO,
	@OrderNoWO,
	@OutstandingBalance,
	@PaidBackToCustomer,
	@ReceiptAmount,
	@RemainingBalance,
	@SourceType,
	@TransactionDocument,
	@RowStatus,
	@CreatedBy,
	GETDATE(),	
	@LastUpdateBy,
	GETDATE())

	
SET @ID = @@IDENTITY

GO

SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO


SET QUOTED_IDENTIFIER ON 
GO
SET ANSI_NULLS OFF 
GO
---------------------------------------------------------------------------------------------------------------
-- Date Created	: 23 Maret 2018
-- Created By	: DNet Team by using CodeSmith v 2.6
-- Rev History	:
---------------------------------------------------------------------------------------------------------------

CREATE PROCEDURE [dbo].up_RetrieveARReceiptDetail
	@ID int OUTPUT	
AS
SET TRANSACTION ISOLATION LEVEL READ COMMITTED

SET NOCOUNT ON

SELECT
	[ID],
	[ARReceiptID],
	[Owner],
	[DetailNo],
	[ARReceiptNo],
	[BU],
	[ChangeAmount],
	[Customer],
	[Description],
	[DifferenceValue],
	[InvoiceNo],
	[OrderDate],
	[OrderNo],
	[OrderNoSO],
	[OrderNoUVSO],
	[OrderNoWO],
	[OutstandingBalance],
	[PaidBackToCustomer],
	[ReceiptAmount],
	[RemainingBalance],
	[SourceType],
	[TransactionDocument],
	[RowStatus],
	[CreatedBy],
	[CreatedTime],
	[LastUpdateBy],
	[LastUpdateTime]	
FROM	[dbo].[ARReceiptDetail]

WHERE
	[ID] = @ID

SET NOCOUNT OFF

GO

SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO


SET QUOTED_IDENTIFIER ON 
GO
SET ANSI_NULLS OFF 
GO
---------------------------------------------------------------------------------------------------------------
-- Date Created	: 23 Maret 2018
-- Created By	: DNet Team by using CodeSmith v 2.6
-- Rev History	:
---------------------------------------------------------------------------------------------------------------

CREATE PROCEDURE [dbo].up_RetrieveARReceiptDetailList

AS

SET TRANSACTION ISOLATION LEVEL READ COMMITTED
SET NOCOUNT ON


SELECT
		[ID],
		[ARReceiptID],
		[Owner],
		[DetailNo],
		[ARReceiptNo],
		[BU],
		[ChangeAmount],
		[Customer],
		[Description],
		[DifferenceValue],
		[InvoiceNo],
		[OrderDate],
		[OrderNo],
		[OrderNoSO],
		[OrderNoUVSO],
		[OrderNoWO],
		[OutstandingBalance],
		[PaidBackToCustomer],
		[ReceiptAmount],
		[RemainingBalance],
		[SourceType],
		[TransactionDocument],
		[RowStatus],
		[CreatedBy],
		[CreatedTime],
		[LastUpdateBy],
		[LastUpdateTime]		
		FROM	
		[dbo].[ARReceiptDetail] 

SET NOCOUNT OFF

GO

SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO


SET QUOTED_IDENTIFIER ON 
GO
SET ANSI_NULLS OFF 
GO
---------------------------------------------------------------------------------------------------------------
-- Date Created	: 23 Maret 2018
-- Created By	: DNet Team by using CodeSmith v 2.6
-- Rev History	:
---------------------------------------------------------------------------------------------------------------

CREATE PROCEDURE [dbo].up_UpdateARReceiptDetail
	@ID int OUTPUT,
	@ARReceiptID int,
	@Owner varchar(100),
	@DetailNo varchar(50),
	@ARReceiptNo varchar(100),
	@BU varchar(100),
	@ChangeAmount money,
	@Customer varchar(100),
	@Description varchar(100),
	@DifferenceValue float,
	@InvoiceNo varchar(100),
	@OrderDate datetime,
	@OrderNo varchar(100),
	@OrderNoSO varchar(100),
	@OrderNoUVSO varchar(100),
	@OrderNoWO varchar(100),
	@OutstandingBalance money,
	@PaidBackToCustomer bit,
	@ReceiptAmount money,
	@RemainingBalance money,
	@SourceType smallint,
	@TransactionDocument varchar(100),
	@RowStatus smallint,
	@CreatedBy varchar(100),
	--@CreatedTime datetime,
	@LastUpdateBy varchar(100)	--@LastUpdateTime datetime	
AS

UPDATE	[dbo].[ARReceiptDetail]
SET
	[ARReceiptID] = @ARReceiptID,
	[Owner] = @Owner,
	[DetailNo] = @DetailNo,
	[ARReceiptNo] = @ARReceiptNo,
	[BU] = @BU,
	[ChangeAmount] = @ChangeAmount,
	[Customer] = @Customer,
	[Description] = @Description,
	[DifferenceValue] = @DifferenceValue,
	[InvoiceNo] = @InvoiceNo,
	[OrderDate] = @OrderDate,
	[OrderNo] = @OrderNo,
	[OrderNoSO] = @OrderNoSO,
	[OrderNoUVSO] = @OrderNoUVSO,
	[OrderNoWO] = @OrderNoWO,
	[OutstandingBalance] = @OutstandingBalance,
	[PaidBackToCustomer] = @PaidBackToCustomer,
	[ReceiptAmount] = @ReceiptAmount,
	[RemainingBalance] = @RemainingBalance,
	[SourceType] = @SourceType,
	[TransactionDocument] = @TransactionDocument,
	[RowStatus] = @RowStatus,
	[CreatedBy] = @CreatedBy,
	--[CreatedTime] = @CreatedTime,
	[LastUpdateBy] = @LastUpdateBy,
	[LastUpdateTime] = GETDATE()	
WHERE
	[ID] = @ID

GO

SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO


SET QUOTED_IDENTIFIER ON 
GO
SET ANSI_NULLS OFF 
GO
---------------------------------------------------------------------------------------------------------------
-- Date Created	: 23 Maret 2018
-- Created By	: DNet Team by using CodeSmith v 2.6
-- Rev History	:
---------------------------------------------------------------------------------------------------------------

CREATE PROCEDURE [dbo].up_DeleteARReceiptDetail
	@ID int OUTPUT	
AS

DELETE
FROM	[dbo].[ARReceiptDetail]
WHERE
	[ID] = @ID

GO

SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO


SET QUOTED_IDENTIFIER ON 
GO
SET ANSI_NULLS OFF 
GO
---------------------------------------------------------------------------------------------------------------
-- Date Created	: 23 Maret 2018
-- Created By	: DNet Team by using CodeSmith v 2.6
-- Rev History	:
---------------------------------------------------------------------------------------------------------------

CREATE PROCEDURE [dbo].up_ValidateARReceiptDetail
	@Result	varchar(1000),
	@ID int OUTPUT,
	@ARReceiptID int,
	@Owner varchar(100),
	@DetailNo varchar(50),
	@ARReceiptNo varchar(100),
	@BU varchar(100),
	@ChangeAmount money,
	@Customer varchar(100),
	@Description varchar(100),
	@DifferenceValue float,
	@InvoiceNo varchar(100),
	@OrderDate datetime,
	@OrderNo varchar(100),
	@OrderNoSO varchar(100),
	@OrderNoUVSO varchar(100),
	@OrderNoWO varchar(100),
	@OutstandingBalance money,
	@PaidBackToCustomer bit,
	@ReceiptAmount money,
	@RemainingBalance money,
	@SourceType smallint,
	@TransactionDocument varchar(100),
	@RowStatus smallint,
	@CreatedBy varchar(100),
	@CreatedTime datetime,
	@LastUpdateBy varchar(100),
	@LastUpdateTime datetime	
AS

SET	@Result = ''

GO

SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO




