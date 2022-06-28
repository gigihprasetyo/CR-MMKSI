


SET QUOTED_IDENTIFIER ON 
GO
SET ANSI_NULLS OFF 
GO
---------------------------------------------------------------------------------------------------------------
-- Date Created	: 22 Maret 2018
-- Created By	: DNet Team by using CodeSmith v 2.6
-- Rev History	:
---------------------------------------------------------------------------------------------------------------

CREATE PROCEDURE [dbo].up_InsertSparePartSalesOrder
	@ID int OUTPUT,
	@SalesChannel smallint,
	@Owner varchar(100),
	@Status smallint,
	@DealerCode varchar(100),
	@Customer varchar(100),
	@CustomerNo varchar(50),
	@DownPaymentAmount money,
	@DownPaymentAmountReceived money,
	@DownPaymentIsPaid bit,
	@ExternalReferenceNo varchar(50),
	@GrandTotal money,
	@Handling smallint,
	@MethodOfPayment varchar(100),
	@OrderType varchar(100),
	@SalesOrderNo varchar(50),
	@SalesPerson varchar(100),
	@ShipmentType varchar(50),
	@State varchar(50),
	@TermOfPayment varchar(100),
	@TotalAmountBeforeDiscount money,
	@TotalBaseAmount money,
	@TotalConsumptionTaxAmount money,
	@TotalDiscountAmount money,
	@TotalReceipt money,
	@TransactionDate datetime,
	@RowStatus smallint,
	@CreatedBy varchar(20),
	--@CreatedTime datetime,
	@LastUpdateBy varchar(20)	--@LastUpdateTime datetime	
AS
INSERT	INTO	[dbo].[SparePartSalesOrder]
VALUES
(
	@SalesChannel,
	@Owner,
	@Status,
	@DealerCode,
	@Customer,
	@CustomerNo,
	@DownPaymentAmount,
	@DownPaymentAmountReceived,
	@DownPaymentIsPaid,
	@ExternalReferenceNo,
	@GrandTotal,
	@Handling,
	@MethodOfPayment,
	@OrderType,
	@SalesOrderNo,
	@SalesPerson,
	@ShipmentType,
	@State,
	@TermOfPayment,
	@TotalAmountBeforeDiscount,
	@TotalBaseAmount,
	@TotalConsumptionTaxAmount,
	@TotalDiscountAmount,
	@TotalReceipt,
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
-- Date Created	: 22 Maret 2018
-- Created By	: DNet Team by using CodeSmith v 2.6
-- Rev History	:
---------------------------------------------------------------------------------------------------------------

CREATE PROCEDURE [dbo].up_RetrieveSparePartSalesOrder
	@ID int OUTPUT	
AS
SET TRANSACTION ISOLATION LEVEL READ COMMITTED

SET NOCOUNT ON

SELECT
	[ID],
	[SalesChannel],
	[Owner],
	[Status],
	[DealerCode],
	[Customer],
	[CustomerNo],
	[DownPaymentAmount],
	[DownPaymentAmountReceived],
	[DownPaymentIsPaid],
	[ExternalReferenceNo],
	[GrandTotal],
	[Handling],
	[MethodOfPayment],
	[OrderType],
	[SalesOrderNo],
	[SalesPerson],
	[ShipmentType],
	[State],
	[TermOfPayment],
	[TotalAmountBeforeDiscount],
	[TotalBaseAmount],
	[TotalConsumptionTaxAmount],
	[TotalDiscountAmount],
	[TotalReceipt],
	[TransactionDate],
	[RowStatus],
	[CreatedBy],
	[CreatedTime],
	[LastUpdateBy],
	[LastUpdateTime]	
FROM	[dbo].[SparePartSalesOrder]

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
-- Date Created	: 22 Maret 2018
-- Created By	: DNet Team by using CodeSmith v 2.6
-- Rev History	:
---------------------------------------------------------------------------------------------------------------

CREATE PROCEDURE [dbo].up_RetrieveSparePartSalesOrderList

AS

SET TRANSACTION ISOLATION LEVEL READ COMMITTED
SET NOCOUNT ON


SELECT
		[ID],
		[SalesChannel],
		[Owner],
		[Status],
		[DealerCode],
		[Customer],
		[CustomerNo],
		[DownPaymentAmount],
		[DownPaymentAmountReceived],
		[DownPaymentIsPaid],
		[ExternalReferenceNo],
		[GrandTotal],
		[Handling],
		[MethodOfPayment],
		[OrderType],
		[SalesOrderNo],
		[SalesPerson],
		[ShipmentType],
		[State],
		[TermOfPayment],
		[TotalAmountBeforeDiscount],
		[TotalBaseAmount],
		[TotalConsumptionTaxAmount],
		[TotalDiscountAmount],
		[TotalReceipt],
		[TransactionDate],
		[RowStatus],
		[CreatedBy],
		[CreatedTime],
		[LastUpdateBy],
		[LastUpdateTime]		
		FROM	
		[dbo].[SparePartSalesOrder] 

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
-- Date Created	: 22 Maret 2018
-- Created By	: DNet Team by using CodeSmith v 2.6
-- Rev History	:
---------------------------------------------------------------------------------------------------------------

CREATE PROCEDURE [dbo].up_UpdateSparePartSalesOrder
	@ID int OUTPUT,
	@SalesChannel smallint,
	@Owner varchar(100),
	@Status smallint,
	@DealerCode varchar(100),
	@Customer varchar(100),
	@CustomerNo varchar(50),
	@DownPaymentAmount money,
	@DownPaymentAmountReceived money,
	@DownPaymentIsPaid bit,
	@ExternalReferenceNo varchar(50),
	@GrandTotal money,
	@Handling smallint,
	@MethodOfPayment varchar(100),
	@OrderType varchar(100),
	@SalesOrderNo varchar(50),
	@SalesPerson varchar(100),
	@ShipmentType varchar(50),
	@State varchar(50),
	@TermOfPayment varchar(100),
	@TotalAmountBeforeDiscount money,
	@TotalBaseAmount money,
	@TotalConsumptionTaxAmount money,
	@TotalDiscountAmount money,
	@TotalReceipt money,
	@TransactionDate datetime,
	@RowStatus smallint,
	@CreatedBy varchar(20),
	--@CreatedTime datetime,
	@LastUpdateBy varchar(20)	--@LastUpdateTime datetime	
AS

UPDATE	[dbo].[SparePartSalesOrder]
SET
	[SalesChannel] = @SalesChannel,
	[Owner] = @Owner,
	[Status] = @Status,
	[DealerCode] = @DealerCode,
	[Customer] = @Customer,
	[CustomerNo] = @CustomerNo,
	[DownPaymentAmount] = @DownPaymentAmount,
	[DownPaymentAmountReceived] = @DownPaymentAmountReceived,
	[DownPaymentIsPaid] = @DownPaymentIsPaid,
	[ExternalReferenceNo] = @ExternalReferenceNo,
	[GrandTotal] = @GrandTotal,
	[Handling] = @Handling,
	[MethodOfPayment] = @MethodOfPayment,
	[OrderType] = @OrderType,
	[SalesOrderNo] = @SalesOrderNo,
	[SalesPerson] = @SalesPerson,
	[ShipmentType] = @ShipmentType,
	[State] = @State,
	[TermOfPayment] = @TermOfPayment,
	[TotalAmountBeforeDiscount] = @TotalAmountBeforeDiscount,
	[TotalBaseAmount] = @TotalBaseAmount,
	[TotalConsumptionTaxAmount] = @TotalConsumptionTaxAmount,
	[TotalDiscountAmount] = @TotalDiscountAmount,
	[TotalReceipt] = @TotalReceipt,
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
-- Date Created	: 22 Maret 2018
-- Created By	: DNet Team by using CodeSmith v 2.6
-- Rev History	:
---------------------------------------------------------------------------------------------------------------

CREATE PROCEDURE [dbo].up_DeleteSparePartSalesOrder
	@ID int OUTPUT	
AS

DELETE
FROM	[dbo].[SparePartSalesOrder]
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
-- Date Created	: 22 Maret 2018
-- Created By	: DNet Team by using CodeSmith v 2.6
-- Rev History	:
---------------------------------------------------------------------------------------------------------------

CREATE PROCEDURE [dbo].up_ValidateSparePartSalesOrder
	@Result	varchar(1000),
	@ID int OUTPUT,
	@SalesChannel smallint,
	@Owner varchar(100),
	@Status smallint,
	@DealerCode varchar(100),
	@Customer varchar(100),
	@CustomerNo varchar(50),
	@DownPaymentAmount money,
	@DownPaymentAmountReceived money,
	@DownPaymentIsPaid bit,
	@ExternalReferenceNo varchar(50),
	@GrandTotal money,
	@Handling smallint,
	@MethodOfPayment varchar(100),
	@OrderType varchar(100),
	@SalesOrderNo varchar(50),
	@SalesPerson varchar(100),
	@ShipmentType varchar(50),
	@State varchar(50),
	@TermOfPayment varchar(100),
	@TotalAmountBeforeDiscount money,
	@TotalBaseAmount money,
	@TotalConsumptionTaxAmount money,
	@TotalDiscountAmount money,
	@TotalReceipt money,
	@TransactionDate datetime,
	@RowStatus smallint,
	@CreatedBy varchar(20),
	@CreatedTime datetime,
	@LastUpdateBy varchar(20),
	@LastUpdateTime datetime	
AS

SET	@Result = ''

GO

SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO




