


SET QUOTED_IDENTIFIER ON 
GO
SET ANSI_NULLS OFF 
GO
---------------------------------------------------------------------------------------------------------------
-- Date Created	: 24 Maret 2018
-- Created By	: Mitrais Team by using CodeSmith v 2.6
-- Rev History	:
---------------------------------------------------------------------------------------------------------------

CREATE PROCEDURE [dbo].up_InsertSparePartDeliveryOrderDetail
	@ID int OUTPUT,
	@SparePartDeliveryOrderID int,
	@Owner varchar(100),
	@AmountBeforeDiscount money,
	@BaseAmount money,
	@BaseQtyDelivered float,
	@BaseQtyOrder float,
	@BatchNo varchar(100),
	@BU varchar(100),
	@ConsumptionTax1Amount money,
	@ConsumptionTax1 varchar(100),
	@ConsumptionTax2Amount money,
	@ConsumptionTax2 varchar(100),
	@DeliveryOrderDetail varchar(100),
	@DeliveryOrderNo varchar(100),
	@DiscountAmount money,
	@DiscountBaseAmount money,
	@DiscountPercentage float,
	@Location varchar(100),
	@ProductCrossReference varchar(100),
	@ProductDescription varchar(100),
	@Product varchar(100),
	@PromiseDate datetime,
	@QtyDelivered float,
	@QtyOrder float,
	@RequestDate datetime,
	@RunningNumber int,
	@SalesOrderDetail varchar(100),
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
INSERT	INTO	[dbo].[SparePartDeliveryOrderDetail]
VALUES
(
	@SparePartDeliveryOrderID,
	@Owner,
	@AmountBeforeDiscount,
	@BaseAmount,
	@BaseQtyDelivered,
	@BaseQtyOrder,
	@BatchNo,
	@BU,
	@ConsumptionTax1Amount,
	@ConsumptionTax1,
	@ConsumptionTax2Amount,
	@ConsumptionTax2,
	@DeliveryOrderDetail,
	@DeliveryOrderNo,
	@DiscountAmount,
	@DiscountBaseAmount,
	@DiscountPercentage,
	@Location,
	@ProductCrossReference,
	@ProductDescription,
	@Product,
	@PromiseDate,
	@QtyDelivered,
	@QtyOrder,
	@RequestDate,
	@RunningNumber,
	@SalesOrderDetail,
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
-- Date Created	: 24 Maret 2018
-- Created By	: Mitrais Team by using CodeSmith v 2.6
-- Rev History	:
---------------------------------------------------------------------------------------------------------------

CREATE PROCEDURE [dbo].up_RetrieveSparePartDeliveryOrderDetail
	@ID int OUTPUT	
AS
SET TRANSACTION ISOLATION LEVEL READ COMMITTED

SET NOCOUNT ON

SELECT
	[ID],
	[SparePartDeliveryOrderID],
	[Owner],
	[AmountBeforeDiscount],
	[BaseAmount],
	[BaseQtyDelivered],
	[BaseQtyOrder],
	[BatchNo],
	[BU],
	[ConsumptionTax1Amount],
	[ConsumptionTax1],
	[ConsumptionTax2Amount],
	[ConsumptionTax2],
	[DeliveryOrderDetail],
	[DeliveryOrderNo],
	[DiscountAmount],
	[DiscountBaseAmount],
	[DiscountPercentage],
	[Location],
	[ProductCrossReference],
	[ProductDescription],
	[Product],
	[PromiseDate],
	[QtyDelivered],
	[QtyOrder],
	[RequestDate],
	[RunningNumber],
	[SalesOrderDetail],
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
FROM	[dbo].[SparePartDeliveryOrderDetail]

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

CREATE PROCEDURE [dbo].up_RetrieveSparePartDeliveryOrderDetailList

AS

SET TRANSACTION ISOLATION LEVEL READ COMMITTED
SET NOCOUNT ON


SELECT
		[ID],
		[SparePartDeliveryOrderID],
		[Owner],
		[AmountBeforeDiscount],
		[BaseAmount],
		[BaseQtyDelivered],
		[BaseQtyOrder],
		[BatchNo],
		[BU],
		[ConsumptionTax1Amount],
		[ConsumptionTax1],
		[ConsumptionTax2Amount],
		[ConsumptionTax2],
		[DeliveryOrderDetail],
		[DeliveryOrderNo],
		[DiscountAmount],
		[DiscountBaseAmount],
		[DiscountPercentage],
		[Location],
		[ProductCrossReference],
		[ProductDescription],
		[Product],
		[PromiseDate],
		[QtyDelivered],
		[QtyOrder],
		[RequestDate],
		[RunningNumber],
		[SalesOrderDetail],
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
		[dbo].[SparePartDeliveryOrderDetail] 

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

CREATE PROCEDURE [dbo].up_UpdateSparePartDeliveryOrderDetail
	@ID int OUTPUT,
	@SparePartDeliveryOrderID int,
	@Owner varchar(100),
	@AmountBeforeDiscount money,
	@BaseAmount money,
	@BaseQtyDelivered float,
	@BaseQtyOrder float,
	@BatchNo varchar(100),
	@BU varchar(100),
	@ConsumptionTax1Amount money,
	@ConsumptionTax1 varchar(100),
	@ConsumptionTax2Amount money,
	@ConsumptionTax2 varchar(100),
	@DeliveryOrderDetail varchar(100),
	@DeliveryOrderNo varchar(100),
	@DiscountAmount money,
	@DiscountBaseAmount money,
	@DiscountPercentage float,
	@Location varchar(100),
	@ProductCrossReference varchar(100),
	@ProductDescription varchar(100),
	@Product varchar(100),
	@PromiseDate datetime,
	@QtyDelivered float,
	@QtyOrder float,
	@RequestDate datetime,
	@RunningNumber int,
	@SalesOrderDetail varchar(100),
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

UPDATE	[dbo].[SparePartDeliveryOrderDetail]
SET
	[SparePartDeliveryOrderID] = @SparePartDeliveryOrderID,
	[Owner] = @Owner,
	[AmountBeforeDiscount] = @AmountBeforeDiscount,
	[BaseAmount] = @BaseAmount,
	[BaseQtyDelivered] = @BaseQtyDelivered,
	[BaseQtyOrder] = @BaseQtyOrder,
	[BatchNo] = @BatchNo,
	[BU] = @BU,
	[ConsumptionTax1Amount] = @ConsumptionTax1Amount,
	[ConsumptionTax1] = @ConsumptionTax1,
	[ConsumptionTax2Amount] = @ConsumptionTax2Amount,
	[ConsumptionTax2] = @ConsumptionTax2,
	[DeliveryOrderDetail] = @DeliveryOrderDetail,
	[DeliveryOrderNo] = @DeliveryOrderNo,
	[DiscountAmount] = @DiscountAmount,
	[DiscountBaseAmount] = @DiscountBaseAmount,
	[DiscountPercentage] = @DiscountPercentage,
	[Location] = @Location,
	[ProductCrossReference] = @ProductCrossReference,
	[ProductDescription] = @ProductDescription,
	[Product] = @Product,
	[PromiseDate] = @PromiseDate,
	[QtyDelivered] = @QtyDelivered,
	[QtyOrder] = @QtyOrder,
	[RequestDate] = @RequestDate,
	[RunningNumber] = @RunningNumber,
	[SalesOrderDetail] = @SalesOrderDetail,
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
-- Date Created	: 24 Maret 2018
-- Created By	: Mitrais Team by using CodeSmith v 2.6
-- Rev History	:
---------------------------------------------------------------------------------------------------------------

CREATE PROCEDURE [dbo].up_DeleteSparePartDeliveryOrderDetail
	@ID int OUTPUT	
AS

DELETE
FROM	[dbo].[SparePartDeliveryOrderDetail]
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

CREATE PROCEDURE [dbo].up_ValidateSparePartDeliveryOrderDetail
	@Result	varchar(1000),
	@ID int OUTPUT,
	@SparePartDeliveryOrderID int,
	@Owner varchar(100),
	@AmountBeforeDiscount money,
	@BaseAmount money,
	@BaseQtyDelivered float,
	@BaseQtyOrder float,
	@BatchNo varchar(100),
	@BU varchar(100),
	@ConsumptionTax1Amount money,
	@ConsumptionTax1 varchar(100),
	@ConsumptionTax2Amount money,
	@ConsumptionTax2 varchar(100),
	@DeliveryOrderDetail varchar(100),
	@DeliveryOrderNo varchar(100),
	@DiscountAmount money,
	@DiscountBaseAmount money,
	@DiscountPercentage float,
	@Location varchar(100),
	@ProductCrossReference varchar(100),
	@ProductDescription varchar(100),
	@Product varchar(100),
	@PromiseDate datetime,
	@QtyDelivered float,
	@QtyOrder float,
	@RequestDate datetime,
	@RunningNumber int,
	@SalesOrderDetail varchar(100),
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




