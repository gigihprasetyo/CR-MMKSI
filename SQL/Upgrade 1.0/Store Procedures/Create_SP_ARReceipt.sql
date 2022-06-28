


SET QUOTED_IDENTIFIER ON 
GO
SET ANSI_NULLS OFF 
GO
---------------------------------------------------------------------------------------------------------------
-- Date Created	: 23 Maret 2018
-- Created By	: DNet Team by using CodeSmith v 2.6
-- Rev History	:
---------------------------------------------------------------------------------------------------------------

CREATE PROCEDURE [dbo].up_InsertARReceipt
	@ID int OUTPUT,
	@Owner varchar(100),
	@GeneratedToken varchar(36),
	@ARInvoiceReferenceNo varchar(100),
	@ARReceiptNo varchar(50),
	@ARReceiptReferenceNo varchar(100),
	@Type smallint,
	@BookingFee bit,
	@BU varchar(100),
	@Cancelled bit,
	@CashAndBank varchar(100),
	@Customer varchar(100),
	@CustomerNo varchar(100),
	@EndOrderDate datetime,
	@MethodOfPayment varchar(100),
	@AvailableBalance money,
	@StartOrderDate datetime,
	@State smallint,
	@AppliedToDocument money,
	@TotalAmountBase money,
	@TotalChangeAmount money,
	@TotalOutstandingBalanceBase money,
	@TotalReceiptAmount money,
	@TotalRemainingBalanceBase money,
	@TransactionDate datetime,
	@RowStatus smallint,
	@CreatedBy varchar(100),
	--@CreatedTime datetime,
	@LastUpdateBy varchar(100)	--@LastUpdateTime datetime	
AS
INSERT	INTO	[dbo].[ARReceipt]
VALUES
(
	@Owner,
	@GeneratedToken,
	@ARInvoiceReferenceNo,
	@ARReceiptNo,
	@ARReceiptReferenceNo,
	@Type,
	@BookingFee,
	@BU,
	@Cancelled,
	@CashAndBank,
	@Customer,
	@CustomerNo,
	@EndOrderDate,
	@MethodOfPayment,
	@AvailableBalance,
	@StartOrderDate,
	@State,
	@AppliedToDocument,
	@TotalAmountBase,
	@TotalChangeAmount,
	@TotalOutstandingBalanceBase,
	@TotalReceiptAmount,
	@TotalRemainingBalanceBase,
	@TransactionDate,
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

CREATE PROCEDURE [dbo].up_RetrieveARReceipt
	@ID int OUTPUT	
AS
SET TRANSACTION ISOLATION LEVEL READ COMMITTED

SET NOCOUNT ON

SELECT
	[ID],
	[Owner],
	[GeneratedToken],
	[ARInvoiceReferenceNo],
	[ARReceiptNo],
	[ARReceiptReferenceNo],
	[Type],
	[BookingFee],
	[BU],
	[Cancelled],
	[CashAndBank],
	[Customer],
	[CustomerNo],
	[EndOrderDate],
	[MethodOfPayment],
	[AvailableBalance],
	[StartOrderDate],
	[State],
	[AppliedToDocument],
	[TotalAmountBase],
	[TotalChangeAmount],
	[TotalOutstandingBalanceBase],
	[TotalReceiptAmount],
	[TotalRemainingBalanceBase],
	[TransactionDate],
	[RowStatus],
	[CreatedBy],
	[CreatedTime],
	[LastUpdateBy],
	[LastUpdateTime]	
FROM	[dbo].[ARReceipt]

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

CREATE PROCEDURE [dbo].up_RetrieveARReceiptList

AS

SET TRANSACTION ISOLATION LEVEL READ COMMITTED
SET NOCOUNT ON


SELECT
		[ID],
		[Owner],
		[GeneratedToken],
		[ARInvoiceReferenceNo],
		[ARReceiptNo],
		[ARReceiptReferenceNo],
		[Type],
		[BookingFee],
		[BU],
		[Cancelled],
		[CashAndBank],
		[Customer],
		[CustomerNo],
		[EndOrderDate],
		[MethodOfPayment],
		[AvailableBalance],
		[StartOrderDate],
		[State],
		[AppliedToDocument],
		[TotalAmountBase],
		[TotalChangeAmount],
		[TotalOutstandingBalanceBase],
		[TotalReceiptAmount],
		[TotalRemainingBalanceBase],
		[TransactionDate],
		[RowStatus],
		[CreatedBy],
		[CreatedTime],
		[LastUpdateBy],
		[LastUpdateTime]		
		FROM	
		[dbo].[ARReceipt] 

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

CREATE PROCEDURE [dbo].up_UpdateARReceipt
	@ID int OUTPUT,
	@Owner varchar(100),
	@GeneratedToken varchar(36),
	@ARInvoiceReferenceNo varchar(100),
	@ARReceiptNo varchar(50),
	@ARReceiptReferenceNo varchar(100),
	@Type smallint,
	@BookingFee bit,
	@BU varchar(100),
	@Cancelled bit,
	@CashAndBank varchar(100),
	@Customer varchar(100),
	@CustomerNo varchar(100),
	@EndOrderDate datetime,
	@MethodOfPayment varchar(100),
	@AvailableBalance money,
	@StartOrderDate datetime,
	@State smallint,
	@AppliedToDocument money,
	@TotalAmountBase money,
	@TotalChangeAmount money,
	@TotalOutstandingBalanceBase money,
	@TotalReceiptAmount money,
	@TotalRemainingBalanceBase money,
	@TransactionDate datetime,
	@RowStatus smallint,
	@CreatedBy varchar(100),
	--@CreatedTime datetime,
	@LastUpdateBy varchar(100)	--@LastUpdateTime datetime	
AS

UPDATE	[dbo].[ARReceipt]
SET
	[Owner] = @Owner,
	[GeneratedToken] = @GeneratedToken,
	[ARInvoiceReferenceNo] = @ARInvoiceReferenceNo,
	[ARReceiptNo] = @ARReceiptNo,
	[ARReceiptReferenceNo] = @ARReceiptReferenceNo,
	[Type] = @Type,
	[BookingFee] = @BookingFee,
	[BU] = @BU,
	[Cancelled] = @Cancelled,
	[CashAndBank] = @CashAndBank,
	[Customer] = @Customer,
	[CustomerNo] = @CustomerNo,
	[EndOrderDate] = @EndOrderDate,
	[MethodOfPayment] = @MethodOfPayment,
	[AvailableBalance] = @AvailableBalance,
	[StartOrderDate] = @StartOrderDate,
	[State] = @State,
	[AppliedToDocument] = @AppliedToDocument,
	[TotalAmountBase] = @TotalAmountBase,
	[TotalChangeAmount] = @TotalChangeAmount,
	[TotalOutstandingBalanceBase] = @TotalOutstandingBalanceBase,
	[TotalReceiptAmount] = @TotalReceiptAmount,
	[TotalRemainingBalanceBase] = @TotalRemainingBalanceBase,
	[TransactionDate] = @TransactionDate,
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

CREATE PROCEDURE [dbo].up_DeleteARReceipt
	@ID int OUTPUT	
AS

DELETE
FROM	[dbo].[ARReceipt]
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

CREATE PROCEDURE [dbo].up_ValidateARReceipt
	@Result	varchar(1000),
	@ID int OUTPUT,
	@Owner varchar(100),
	@GeneratedToken varchar(36),
	@ARInvoiceReferenceNo varchar(100),
	@ARReceiptNo varchar(50),
	@ARReceiptReferenceNo varchar(100),
	@Type smallint,
	@BookingFee bit,
	@BU varchar(100),
	@Cancelled bit,
	@CashAndBank varchar(100),
	@Customer varchar(100),
	@CustomerNo varchar(100),
	@EndOrderDate datetime,
	@MethodOfPayment varchar(100),
	@AvailableBalance money,
	@StartOrderDate datetime,
	@State smallint,
	@AppliedToDocument money,
	@TotalAmountBase money,
	@TotalChangeAmount money,
	@TotalOutstandingBalanceBase money,
	@TotalReceiptAmount money,
	@TotalRemainingBalanceBase money,
	@TransactionDate datetime,
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




