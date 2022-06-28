


SET QUOTED_IDENTIFIER ON 
GO
SET ANSI_NULLS OFF 
GO
---------------------------------------------------------------------------------------------------------------
-- Date Created	: 22 Maret 2018
-- Created By	: DNet Team by using CodeSmith v 2.6
-- Rev History	:
---------------------------------------------------------------------------------------------------------------

CREATE PROCEDURE [dbo].up_InsertSparePartSalesOrderDetail
	@ID int OUTPUT,
	@SparePartSalesOrderID int,
	@Owner varchar(100),
	@Status smallint,
	@AmountBeforeDiscount money,
	@BaseAmount money,
	@KodeDealer varchar(100),
	@ConsumptionTax1Amount money,
	@ConsumptionTax1 varchar(100),
	@ConsumptionTax2Amount money,
	@ConsumptionTax2 varchar(100),
	@DiscountAmount money,
	@DiscountPercentAge decimal(18, 9),
	@ProductCrossReference varchar(100),
	@ProductDescription varchar(100),
	@Product varchar(100),
	@PromiseDate datetime,
	@QtyDelivered float,
	@QtyOrder float,
	@RequestDate datetime,
	@SalesOrderDetailID varchar(50),
	@SalesOrderNo varchar(100),
	@SalesUnit varchar(100),
	@Site varchar(100),
	@TotalAmount money,
	@TotalConsumptionTaxAmount money,
	@TransactionAmount money,
	@UnitPrice money,
	@Warehouse varchar(100),
	@RowStatus smallint,
	@CreatedBy varchar(20),
	--@CreatedTime datetime,
	@LastUpdateBy varchar(20)	--@LastUpdateTime datetime	
AS
INSERT	INTO	[dbo].[SparePartSalesOrderDetail]
VALUES
(
	@SparePartSalesOrderID,
	@Owner,
	@Status,
	@AmountBeforeDiscount,
	@BaseAmount,
	@KodeDealer,
	@ConsumptionTax1Amount,
	@ConsumptionTax1,
	@ConsumptionTax2Amount,
	@ConsumptionTax2,
	@DiscountAmount,
	@DiscountPercentAge,
	@ProductCrossReference,
	@ProductDescription,
	@Product,
	@PromiseDate,
	@QtyDelivered,
	@QtyOrder,
	@RequestDate,
	@SalesOrderDetailID,
	@SalesOrderNo,
	@SalesUnit,
	@Site,
	@TotalAmount,
	@TotalConsumptionTaxAmount,
	@TransactionAmount,
	@UnitPrice,
	@Warehouse,
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

CREATE PROCEDURE [dbo].up_RetrieveSparePartSalesOrderDetail
	@ID int OUTPUT	
AS
SET TRANSACTION ISOLATION LEVEL READ COMMITTED

SET NOCOUNT ON

SELECT
	[ID],
	[SparePartSalesOrderID],
	[Owner],
	[Status],
	[AmountBeforeDiscount],
	[BaseAmount],
	[KodeDealer],
	[ConsumptionTax1Amount],
	[ConsumptionTax1],
	[ConsumptionTax2Amount],
	[ConsumptionTax2],
	[DiscountAmount],
	[DiscountPercentAge],
	[ProductCrossReference],
	[ProductDescription],
	[Product],
	[PromiseDate],
	[QtyDelivered],
	[QtyOrder],
	[RequestDate],
	[SalesOrderDetailID],
	[SalesOrderNo],
	[SalesUnit],
	[Site],
	[TotalAmount],
	[TotalConsumptionTaxAmount],
	[TransactionAmount],
	[UnitPrice],
	[Warehouse],
	[RowStatus],
	[CreatedBy],
	[CreatedTime],
	[LastUpdateBy],
	[LastUpdateTime]	
FROM	[dbo].[SparePartSalesOrderDetail]

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

CREATE PROCEDURE [dbo].up_RetrieveSparePartSalesOrderDetailList

AS

SET TRANSACTION ISOLATION LEVEL READ COMMITTED
SET NOCOUNT ON


SELECT
		[ID],
		[SparePartSalesOrderID],
		[Owner],
		[Status],
		[AmountBeforeDiscount],
		[BaseAmount],
		[KodeDealer],
		[ConsumptionTax1Amount],
		[ConsumptionTax1],
		[ConsumptionTax2Amount],
		[ConsumptionTax2],
		[DiscountAmount],
		[DiscountPercentAge],
		[ProductCrossReference],
		[ProductDescription],
		[Product],
		[PromiseDate],
		[QtyDelivered],
		[QtyOrder],
		[RequestDate],
		[SalesOrderDetailID],
		[SalesOrderNo],
		[SalesUnit],
		[Site],
		[TotalAmount],
		[TotalConsumptionTaxAmount],
		[TransactionAmount],
		[UnitPrice],
		[Warehouse],
		[RowStatus],
		[CreatedBy],
		[CreatedTime],
		[LastUpdateBy],
		[LastUpdateTime]		
		FROM	
		[dbo].[SparePartSalesOrderDetail] 

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

CREATE PROCEDURE [dbo].up_UpdateSparePartSalesOrderDetail
	@ID int OUTPUT,
	@SparePartSalesOrderID int,
	@Owner varchar(100),
	@Status smallint,
	@AmountBeforeDiscount money,
	@BaseAmount money,
	@KodeDealer varchar(100),
	@ConsumptionTax1Amount money,
	@ConsumptionTax1 varchar(100),
	@ConsumptionTax2Amount money,
	@ConsumptionTax2 varchar(100),
	@DiscountAmount money,
	@DiscountPercentAge decimal(18, 9),
	@ProductCrossReference varchar(100),
	@ProductDescription varchar(100),
	@Product varchar(100),
	@PromiseDate datetime,
	@QtyDelivered float,
	@QtyOrder float,
	@RequestDate datetime,
	@SalesOrderDetailID varchar(50),
	@SalesOrderNo varchar(100),
	@SalesUnit varchar(100),
	@Site varchar(100),
	@TotalAmount money,
	@TotalConsumptionTaxAmount money,
	@TransactionAmount money,
	@UnitPrice money,
	@Warehouse varchar(100),
	@RowStatus smallint,
	@CreatedBy varchar(20),
	--@CreatedTime datetime,
	@LastUpdateBy varchar(20)	--@LastUpdateTime datetime	
AS

UPDATE	[dbo].[SparePartSalesOrderDetail]
SET
	[SparePartSalesOrderID] = @SparePartSalesOrderID,
	[Owner] = @Owner,
	[Status] = @Status,
	[AmountBeforeDiscount] = @AmountBeforeDiscount,
	[BaseAmount] = @BaseAmount,
	[KodeDealer] = @KodeDealer,
	[ConsumptionTax1Amount] = @ConsumptionTax1Amount,
	[ConsumptionTax1] = @ConsumptionTax1,
	[ConsumptionTax2Amount] = @ConsumptionTax2Amount,
	[ConsumptionTax2] = @ConsumptionTax2,
	[DiscountAmount] = @DiscountAmount,
	[DiscountPercentAge] = @DiscountPercentAge,
	[ProductCrossReference] = @ProductCrossReference,
	[ProductDescription] = @ProductDescription,
	[Product] = @Product,
	[PromiseDate] = @PromiseDate,
	[QtyDelivered] = @QtyDelivered,
	[QtyOrder] = @QtyOrder,
	[RequestDate] = @RequestDate,
	[SalesOrderDetailID] = @SalesOrderDetailID,
	[SalesOrderNo] = @SalesOrderNo,
	[SalesUnit] = @SalesUnit,
	[Site] = @Site,
	[TotalAmount] = @TotalAmount,
	[TotalConsumptionTaxAmount] = @TotalConsumptionTaxAmount,
	[TransactionAmount] = @TransactionAmount,
	[UnitPrice] = @UnitPrice,
	[Warehouse] = @Warehouse,
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

CREATE PROCEDURE [dbo].up_DeleteSparePartSalesOrderDetail
	@ID int OUTPUT	
AS

DELETE
FROM	[dbo].[SparePartSalesOrderDetail]
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

CREATE PROCEDURE [dbo].up_ValidateSparePartSalesOrderDetail
	@Result	varchar(1000),
	@ID int OUTPUT,
	@SparePartSalesOrderID int,
	@Owner varchar(100),
	@Status smallint,
	@AmountBeforeDiscount money,
	@BaseAmount money,
	@KodeDealer varchar(100),
	@ConsumptionTax1Amount money,
	@ConsumptionTax1 varchar(100),
	@ConsumptionTax2Amount money,
	@ConsumptionTax2 varchar(100),
	@DiscountAmount money,
	@DiscountPercentAge decimal(18, 9),
	@ProductCrossReference varchar(100),
	@ProductDescription varchar(100),
	@Product varchar(100),
	@PromiseDate datetime,
	@QtyDelivered float,
	@QtyOrder float,
	@RequestDate datetime,
	@SalesOrderDetailID varchar(50),
	@SalesOrderNo varchar(100),
	@SalesUnit varchar(100),
	@Site varchar(100),
	@TotalAmount money,
	@TotalConsumptionTaxAmount money,
	@TransactionAmount money,
	@UnitPrice money,
	@Warehouse varchar(100),
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




