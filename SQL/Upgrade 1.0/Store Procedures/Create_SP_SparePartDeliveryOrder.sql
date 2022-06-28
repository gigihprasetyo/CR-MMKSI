


SET QUOTED_IDENTIFIER ON 
GO
SET ANSI_NULLS OFF 
GO
---------------------------------------------------------------------------------------------------------------
-- Date Created	: 24 Maret 2018
-- Created By	: Mitrais Team by using CodeSmith v 2.6
-- Rev History	:
---------------------------------------------------------------------------------------------------------------

CREATE PROCEDURE [dbo].up_InsertSparePartDeliveryOrder
	@ID int OUTPUT,
	@Owner varchar(100),
	@Address1 varchar(100),
	@Address2 varchar(100),
	@Address3 varchar(100),
	@Address4 varchar(100),
	@BusinessPhone varchar(60),
	@BU varchar(100),
	@CancellationDate datetime,
	@City varchar(100),
	@CustomerContacts varchar(100),
	@Customer varchar(100),
	@CustomerNo varchar(50),
	@DeliveryAddress varchar(100),
	@DeliveryOrderNo varchar(50),
	@DeliveryType int,
	@ExternalReferenceNo varchar(50),
	@GrandTotal money,
	@Status smallint,
	@MethodofPayment varchar(100),
	@OrderType varchar(100),
	@ReferenceNo varchar(100),
	@Salesperson varchar(100),
	@State smallint,
	@TermofPayment varchar(100),
	@TotalAmountBeforeDiscount money,
	@TotalBaseAmount money,
	@TotalDiscountAmount money,
	@TotalMiscChargeBaseAmount money,
	@TotalMiscChargeConsumptionTaxAmount money,
	@TotalReceipt money,
	@TotalConsumptionTaxAmount money,
	@TransactionDate datetime,
	@RowStatus smallint,
	@CreatedBy varchar(20),
	--@CreatedTime datetime,
	@LastUpdateBy varchar(20)	--@LastUpdateTime datetime	
AS
INSERT	INTO	[dbo].[SparePartDeliveryOrder]
VALUES
(
	@Owner,
	@Address1,
	@Address2,
	@Address3,
	@Address4,
	@BusinessPhone,
	@BU,
	@CancellationDate,
	@City,
	@CustomerContacts,
	@Customer,
	@CustomerNo,
	@DeliveryAddress,
	@DeliveryOrderNo,
	@DeliveryType,
	@ExternalReferenceNo,
	@GrandTotal,
	@Status,
	@MethodofPayment,
	@OrderType,
	@ReferenceNo,
	@Salesperson,
	@State,
	@TermofPayment,
	@TotalAmountBeforeDiscount,
	@TotalBaseAmount,
	@TotalDiscountAmount,
	@TotalMiscChargeBaseAmount,
	@TotalMiscChargeConsumptionTaxAmount,
	@TotalReceipt,
	@TotalConsumptionTaxAmount,
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
-- Date Created	: 24 Maret 2018
-- Created By	: Mitrais Team by using CodeSmith v 2.6
-- Rev History	:
---------------------------------------------------------------------------------------------------------------

CREATE PROCEDURE [dbo].up_RetrieveSparePartDeliveryOrder
	@ID int OUTPUT	
AS
SET TRANSACTION ISOLATION LEVEL READ COMMITTED

SET NOCOUNT ON

SELECT
	[ID],
	[Owner],
	[Address1],
	[Address2],
	[Address3],
	[Address4],
	[BusinessPhone],
	[BU],
	[CancellationDate],
	[City],
	[CustomerContacts],
	[Customer],
	[CustomerNo],
	[DeliveryAddress],
	[DeliveryOrderNo],
	[DeliveryType],
	[ExternalReferenceNo],
	[GrandTotal],
	[Status],
	[MethodofPayment],
	[OrderType],
	[ReferenceNo],
	[Salesperson],
	[State],
	[TermofPayment],
	[TotalAmountBeforeDiscount],
	[TotalBaseAmount],
	[TotalDiscountAmount],
	[TotalMiscChargeBaseAmount],
	[TotalMiscChargeConsumptionTaxAmount],
	[TotalReceipt],
	[TotalConsumptionTaxAmount],
	[TransactionDate],
	[RowStatus],
	[CreatedBy],
	[CreatedTime],
	[LastUpdateBy],
	[LastUpdateTime]	
FROM	[dbo].[SparePartDeliveryOrder]

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
-- Date Created	: 24 Maret 2018
-- Created By	: Mitrais Team by using CodeSmith v 2.6
-- Rev History	:
---------------------------------------------------------------------------------------------------------------

CREATE PROCEDURE [dbo].up_RetrieveSparePartDeliveryOrderList

AS

SET TRANSACTION ISOLATION LEVEL READ COMMITTED
SET NOCOUNT ON


SELECT
		[ID],
		[Owner],
		[Address1],
		[Address2],
		[Address3],
		[Address4],
		[BusinessPhone],
		[BU],
		[CancellationDate],
		[City],
		[CustomerContacts],
		[Customer],
		[CustomerNo],
		[DeliveryAddress],
		[DeliveryOrderNo],
		[DeliveryType],
		[ExternalReferenceNo],
		[GrandTotal],
		[Status],
		[MethodofPayment],
		[OrderType],
		[ReferenceNo],
		[Salesperson],
		[State],
		[TermofPayment],
		[TotalAmountBeforeDiscount],
		[TotalBaseAmount],
		[TotalDiscountAmount],
		[TotalMiscChargeBaseAmount],
		[TotalMiscChargeConsumptionTaxAmount],
		[TotalReceipt],
		[TotalConsumptionTaxAmount],
		[TransactionDate],
		[RowStatus],
		[CreatedBy],
		[CreatedTime],
		[LastUpdateBy],
		[LastUpdateTime]		
		FROM	
		[dbo].[SparePartDeliveryOrder] 

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
-- Date Created	: 24 Maret 2018
-- Created By	: Mitrais Team by using CodeSmith v 2.6
-- Rev History	:
---------------------------------------------------------------------------------------------------------------

CREATE PROCEDURE [dbo].up_UpdateSparePartDeliveryOrder
	@ID int OUTPUT,
	@Owner varchar(100),
	@Address1 varchar(100),
	@Address2 varchar(100),
	@Address3 varchar(100),
	@Address4 varchar(100),
	@BusinessPhone varchar(60),
	@BU varchar(100),
	@CancellationDate datetime,
	@City varchar(100),
	@CustomerContacts varchar(100),
	@Customer varchar(100),
	@CustomerNo varchar(50),
	@DeliveryAddress varchar(100),
	@DeliveryOrderNo varchar(50),
	@DeliveryType int,
	@ExternalReferenceNo varchar(50),
	@GrandTotal money,
	@Status smallint,
	@MethodofPayment varchar(100),
	@OrderType varchar(100),
	@ReferenceNo varchar(100),
	@Salesperson varchar(100),
	@State smallint,
	@TermofPayment varchar(100),
	@TotalAmountBeforeDiscount money,
	@TotalBaseAmount money,
	@TotalDiscountAmount money,
	@TotalMiscChargeBaseAmount money,
	@TotalMiscChargeConsumptionTaxAmount money,
	@TotalReceipt money,
	@TotalConsumptionTaxAmount money,
	@TransactionDate datetime,
	@RowStatus smallint,
	@CreatedBy varchar(20),
	--@CreatedTime datetime,
	@LastUpdateBy varchar(20)	--@LastUpdateTime datetime	
AS

UPDATE	[dbo].[SparePartDeliveryOrder]
SET
	[Owner] = @Owner,
	[Address1] = @Address1,
	[Address2] = @Address2,
	[Address3] = @Address3,
	[Address4] = @Address4,
	[BusinessPhone] = @BusinessPhone,
	[BU] = @BU,
	[CancellationDate] = @CancellationDate,
	[City] = @City,
	[CustomerContacts] = @CustomerContacts,
	[Customer] = @Customer,
	[CustomerNo] = @CustomerNo,
	[DeliveryAddress] = @DeliveryAddress,
	[DeliveryOrderNo] = @DeliveryOrderNo,
	[DeliveryType] = @DeliveryType,
	[ExternalReferenceNo] = @ExternalReferenceNo,
	[GrandTotal] = @GrandTotal,
	[Status] = @Status,
	[MethodofPayment] = @MethodofPayment,
	[OrderType] = @OrderType,
	[ReferenceNo] = @ReferenceNo,
	[Salesperson] = @Salesperson,
	[State] = @State,
	[TermofPayment] = @TermofPayment,
	[TotalAmountBeforeDiscount] = @TotalAmountBeforeDiscount,
	[TotalBaseAmount] = @TotalBaseAmount,
	[TotalDiscountAmount] = @TotalDiscountAmount,
	[TotalMiscChargeBaseAmount] = @TotalMiscChargeBaseAmount,
	[TotalMiscChargeConsumptionTaxAmount] = @TotalMiscChargeConsumptionTaxAmount,
	[TotalReceipt] = @TotalReceipt,
	[TotalConsumptionTaxAmount] = @TotalConsumptionTaxAmount,
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
-- Date Created	: 24 Maret 2018
-- Created By	: Mitrais Team by using CodeSmith v 2.6
-- Rev History	:
---------------------------------------------------------------------------------------------------------------

CREATE PROCEDURE [dbo].up_DeleteSparePartDeliveryOrder
	@ID int OUTPUT	
AS

DELETE
FROM	[dbo].[SparePartDeliveryOrder]
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
-- Date Created	: 24 Maret 2018
-- Created By	: Mitrais Team by using CodeSmith v 2.6
-- Rev History	:
---------------------------------------------------------------------------------------------------------------

CREATE PROCEDURE [dbo].up_ValidateSparePartDeliveryOrder
	@Result	varchar(1000),
	@ID int OUTPUT,
	@Owner varchar(100),
	@Address1 varchar(100),
	@Address2 varchar(100),
	@Address3 varchar(100),
	@Address4 varchar(100),
	@BusinessPhone varchar(60),
	@BU varchar(100),
	@CancellationDate datetime,
	@City varchar(100),
	@CustomerContacts varchar(100),
	@Customer varchar(100),
	@CustomerNo varchar(50),
	@DeliveryAddress varchar(100),
	@DeliveryOrderNo varchar(50),
	@DeliveryType int,
	@ExternalReferenceNo varchar(50),
	@GrandTotal money,
	@Status smallint,
	@MethodofPayment varchar(100),
	@OrderType varchar(100),
	@ReferenceNo varchar(100),
	@Salesperson varchar(100),
	@State smallint,
	@TermofPayment varchar(100),
	@TotalAmountBeforeDiscount money,
	@TotalBaseAmount money,
	@TotalDiscountAmount money,
	@TotalMiscChargeBaseAmount money,
	@TotalMiscChargeConsumptionTaxAmount money,
	@TotalReceipt money,
	@TotalConsumptionTaxAmount money,
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




