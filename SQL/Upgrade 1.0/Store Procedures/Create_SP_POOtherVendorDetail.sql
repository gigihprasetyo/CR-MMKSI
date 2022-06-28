
/****** Object:  Stored Procedure [dbo].[up_UpdatePOOtherVendorDetail]    Script Date: 20 Maret 2018 ******/
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'[dbo].[up_InsertPOOtherVendorDetail]') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
DROP PROCEDURE [dbo].[up_InsertPOOtherVendorDetail]
GO


/****** Object:  Stored Procedure [dbo].[up_RetrievePOOtherVendorDetail]    Script Date: 20 Maret 2018 ******/
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'[dbo].[up_RetrievePOOtherVendorDetail]') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
DROP PROCEDURE [dbo].[up_RetrievePOOtherVendorDetail]
GO


/****** Object:  Stored Procedure [dbo].[up_RetrievePOOtherVendorDetailList]    Script Date: 20 Maret 2018 ******/
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'[dbo].[up_RetrievePOOtherVendorDetailList]') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
DROP PROCEDURE [dbo].[up_RetrievePOOtherVendorDetailList]
GO


/****** Object:  Stored Procedure [dbo].[up_UpdatePOOtherVendorDetail]    Script Date: 20 Maret 2018 ******/
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'[dbo].[up_UpdatePOOtherVendorDetail]') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
DROP PROCEDURE [dbo].[up_UpdatePOOtherVendorDetail]
GO


/****** Object:  Stored Procedure [dbo].[up_DeletePOOtherVendorDetail]    Script Date: 20 Maret 2018 ******/
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'[dbo].[up_DeletePOOtherVendorDetail]') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
DROP PROCEDURE [dbo].[up_DeletePOOtherVendorDetail]
GO


/****** Object:  Stored Procedure [dbo].[up_ValidatePOOtherVendorDetail]    Script Date: 20 Maret 2018 ******/
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'[dbo].[up_ValidatePOOtherVendorDetail]') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
DROP PROCEDURE [dbo].[up_ValidatePOOtherVendorDetail]
GO



SET QUOTED_IDENTIFIER ON 
GO
SET ANSI_NULLS OFF 
GO
---------------------------------------------------------------------------------------------------------------
-- Date Created	: 20 Maret 2018
-- Created By	: Mitrais Team by using CodeSmith v 2.6
-- Rev History	:
---------------------------------------------------------------------------------------------------------------

CREATE PROCEDURE [dbo].up_InsertPOOtherVendorDetail
	@ID int OUTPUT,
	@POOtherVendorID int,
	@Owner varchar(100),
	@DealerCode varchar(100),
	@CloseLine bit,
	@CloseReason varchar(100),
	@Completed bit,
	@ConsumptionTax1Amount money,
	@ConsumptionTax1 varchar(100),
	@ConsumptionTax2Amount money,
	@ConsumptionTax2 varchar(100),
	@Department varchar(100),
	@Description varchar(100),
	@DiscountAmount money,
	@DiscountPercentage float,
	@EventData varchar(100),
	@FormSource smallint,
	@BaseQtyOrder float,
	@BaseQtyReceipt float,
	@BaseQtyReturn float,
	@InventoryUnit varchar(100),
	@ProductCrossReference varchar(100),
	@ProductDescription varchar(100),
	@Product varchar(100),
	@ProductSubstitute varchar(100),
	@ProductVariant varchar(100),
	@ProductVolume float,
	@ProductWeight float,
	@PromisedDate datetime,
	@PurchaseFor smallint,
	@PurchaseOrderNo varchar(100),
	@PurchaseRequisitionDetail varchar(100),
	@PurchaseUnit varchar(100),
	@QtyOrder float,
	@QtyReceipt float,
	@QtyReturn float,
	@RecallProduct bit,
	@ReferenceNo varchar(100),
	@RequiredDate datetime,
	@SalesOrderDetail varchar(100),
	@ScheduledShippingDate datetime,
	@ServicePartsAndMaterial varchar(100),
	@ShippingDate datetime,
	@Site varchar(100),
	@StockNumber varchar(100),
	@TitleRegistrationFee money,
	@TotalAmount money,
	@TotalAmountBeforeDiscount money,
	@TotalBaseAmount money,
	@TotalConsumptionTaxAmount money,
	@TotalVolume float,
	@TotalWeight float,
	@TransactionAmount money,
	@UnitCost money,
	@Warehouse varchar(100),
	@RowStatus smallint,
	@CreatedBy varchar(20),
	--@CreatedTime datetime,
	@LastUpdateBy varchar(20)	--@LastUpdateTime datetime	
AS
INSERT	INTO	[dbo].[POOtherVendorDetail]
VALUES
(
	@POOtherVendorID,
	@Owner,
	@DealerCode,
	@CloseLine,
	@CloseReason,
	@Completed,
	@ConsumptionTax1Amount,
	@ConsumptionTax1,
	@ConsumptionTax2Amount,
	@ConsumptionTax2,
	@Department,
	@Description,
	@DiscountAmount,
	@DiscountPercentage,
	@EventData,
	@FormSource,
	@BaseQtyOrder,
	@BaseQtyReceipt,
	@BaseQtyReturn,
	@InventoryUnit,
	@ProductCrossReference,
	@ProductDescription,
	@Product,
	@ProductSubstitute,
	@ProductVariant,
	@ProductVolume,
	@ProductWeight,
	@PromisedDate,
	@PurchaseFor,
	@PurchaseOrderNo,
	@PurchaseRequisitionDetail,
	@PurchaseUnit,
	@QtyOrder,
	@QtyReceipt,
	@QtyReturn,
	@RecallProduct,
	@ReferenceNo,
	@RequiredDate,
	@SalesOrderDetail,
	@ScheduledShippingDate,
	@ServicePartsAndMaterial,
	@ShippingDate,
	@Site,
	@StockNumber,
	@TitleRegistrationFee,
	@TotalAmount,
	@TotalAmountBeforeDiscount,
	@TotalBaseAmount,
	@TotalConsumptionTaxAmount,
	@TotalVolume,
	@TotalWeight,
	@TransactionAmount,
	@UnitCost,
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
-- Date Created	: 20 Maret 2018
-- Created By	: Mitrais Team by using CodeSmith v 2.6
-- Rev History	:
---------------------------------------------------------------------------------------------------------------

CREATE PROCEDURE [dbo].up_RetrievePOOtherVendorDetail
	@ID int OUTPUT	
AS
SET TRANSACTION ISOLATION LEVEL READ COMMITTED

SET NOCOUNT ON

SELECT
	[ID],
	[POOtherVendorID],
	[Owner],
	[DealerCode],
	[CloseLine],
	[CloseReason],
	[Completed],
	[ConsumptionTax1Amount],
	[ConsumptionTax1],
	[ConsumptionTax2Amount],
	[ConsumptionTax2],
	[Department],
	[Description],
	[DiscountAmount],
	[DiscountPercentage],
	[EventData],
	[FormSource],
	[BaseQtyOrder],
	[BaseQtyReceipt],
	[BaseQtyReturn],
	[InventoryUnit],
	[ProductCrossReference],
	[ProductDescription],
	[Product],
	[ProductSubstitute],
	[ProductVariant],
	[ProductVolume],
	[ProductWeight],
	[PromisedDate],
	[PurchaseFor],
	[PurchaseOrderNo],
	[PurchaseRequisitionDetail],
	[PurchaseUnit],
	[QtyOrder],
	[QtyReceipt],
	[QtyReturn],
	[RecallProduct],
	[ReferenceNo],
	[RequiredDate],
	[SalesOrderDetail],
	[ScheduledShippingDate],
	[ServicePartsAndMaterial],
	[ShippingDate],
	[Site],
	[StockNumber],
	[TitleRegistrationFee],
	[TotalAmount],
	[TotalAmountBeforeDiscount],
	[TotalBaseAmount],
	[TotalConsumptionTaxAmount],
	[TotalVolume],
	[TotalWeight],
	[TransactionAmount],
	[UnitCost],
	[Warehouse],
	[RowStatus],
	[CreatedBy],
	[CreatedTime],
	[LastUpdateBy],
	[LastUpdateTime]	
FROM	[dbo].[POOtherVendorDetail]

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
-- Date Created	: 20 Maret 2018
-- Created By	: Mitrais Team by using CodeSmith v 2.6
-- Rev History	:
---------------------------------------------------------------------------------------------------------------

CREATE PROCEDURE [dbo].up_RetrievePOOtherVendorDetailList

AS

SET TRANSACTION ISOLATION LEVEL READ COMMITTED
SET NOCOUNT ON


SELECT
		[ID],
		[POOtherVendorID],
		[Owner],
		[DealerCode],
		[CloseLine],
		[CloseReason],
		[Completed],
		[ConsumptionTax1Amount],
		[ConsumptionTax1],
		[ConsumptionTax2Amount],
		[ConsumptionTax2],
		[Department],
		[Description],
		[DiscountAmount],
		[DiscountPercentage],
		[EventData],
		[FormSource],
		[BaseQtyOrder],
		[BaseQtyReceipt],
		[BaseQtyReturn],
		[InventoryUnit],
		[ProductCrossReference],
		[ProductDescription],
		[Product],
		[ProductSubstitute],
		[ProductVariant],
		[ProductVolume],
		[ProductWeight],
		[PromisedDate],
		[PurchaseFor],
		[PurchaseOrderNo],
		[PurchaseRequisitionDetail],
		[PurchaseUnit],
		[QtyOrder],
		[QtyReceipt],
		[QtyReturn],
		[RecallProduct],
		[ReferenceNo],
		[RequiredDate],
		[SalesOrderDetail],
		[ScheduledShippingDate],
		[ServicePartsAndMaterial],
		[ShippingDate],
		[Site],
		[StockNumber],
		[TitleRegistrationFee],
		[TotalAmount],
		[TotalAmountBeforeDiscount],
		[TotalBaseAmount],
		[TotalConsumptionTaxAmount],
		[TotalVolume],
		[TotalWeight],
		[TransactionAmount],
		[UnitCost],
		[Warehouse],
		[RowStatus],
		[CreatedBy],
		[CreatedTime],
		[LastUpdateBy],
		[LastUpdateTime]		
		FROM	
		[dbo].[POOtherVendorDetail] 

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
-- Date Created	: 20 Maret 2018
-- Created By	: Mitrais Team by using CodeSmith v 2.6
-- Rev History	:
---------------------------------------------------------------------------------------------------------------

CREATE PROCEDURE [dbo].up_UpdatePOOtherVendorDetail
	@ID int OUTPUT,
	@POOtherVendorID int,
	@Owner varchar(100),
	@DealerCode varchar(100),
	@CloseLine bit,
	@CloseReason varchar(100),
	@Completed bit,
	@ConsumptionTax1Amount money,
	@ConsumptionTax1 varchar(100),
	@ConsumptionTax2Amount money,
	@ConsumptionTax2 varchar(100),
	@Department varchar(100),
	@Description varchar(100),
	@DiscountAmount money,
	@DiscountPercentage float,
	@EventData varchar(100),
	@FormSource smallint,
	@BaseQtyOrder float,
	@BaseQtyReceipt float,
	@BaseQtyReturn float,
	@InventoryUnit varchar(100),
	@ProductCrossReference varchar(100),
	@ProductDescription varchar(100),
	@Product varchar(100),
	@ProductSubstitute varchar(100),
	@ProductVariant varchar(100),
	@ProductVolume float,
	@ProductWeight float,
	@PromisedDate datetime,
	@PurchaseFor smallint,
	@PurchaseOrderNo varchar(100),
	@PurchaseRequisitionDetail varchar(100),
	@PurchaseUnit varchar(100),
	@QtyOrder float,
	@QtyReceipt float,
	@QtyReturn float,
	@RecallProduct bit,
	@ReferenceNo varchar(100),
	@RequiredDate datetime,
	@SalesOrderDetail varchar(100),
	@ScheduledShippingDate datetime,
	@ServicePartsAndMaterial varchar(100),
	@ShippingDate datetime,
	@Site varchar(100),
	@StockNumber varchar(100),
	@TitleRegistrationFee money,
	@TotalAmount money,
	@TotalAmountBeforeDiscount money,
	@TotalBaseAmount money,
	@TotalConsumptionTaxAmount money,
	@TotalVolume float,
	@TotalWeight float,
	@TransactionAmount money,
	@UnitCost money,
	@Warehouse varchar(100),
	@RowStatus smallint,
	@CreatedBy varchar(20),
	--@CreatedTime datetime,
	@LastUpdateBy varchar(20)	--@LastUpdateTime datetime	
AS

UPDATE	[dbo].[POOtherVendorDetail]
SET
	[POOtherVendorID] = @POOtherVendorID,
	[Owner] = @Owner,
	[DealerCode] = @DealerCode,
	[CloseLine] = @CloseLine,
	[CloseReason] = @CloseReason,
	[Completed] = @Completed,
	[ConsumptionTax1Amount] = @ConsumptionTax1Amount,
	[ConsumptionTax1] = @ConsumptionTax1,
	[ConsumptionTax2Amount] = @ConsumptionTax2Amount,
	[ConsumptionTax2] = @ConsumptionTax2,
	[Department] = @Department,
	[Description] = @Description,
	[DiscountAmount] = @DiscountAmount,
	[DiscountPercentage] = @DiscountPercentage,
	[EventData] = @EventData,
	[FormSource] = @FormSource,
	[BaseQtyOrder] = @BaseQtyOrder,
	[BaseQtyReceipt] = @BaseQtyReceipt,
	[BaseQtyReturn] = @BaseQtyReturn,
	[InventoryUnit] = @InventoryUnit,
	[ProductCrossReference] = @ProductCrossReference,
	[ProductDescription] = @ProductDescription,
	[Product] = @Product,
	[ProductSubstitute] = @ProductSubstitute,
	[ProductVariant] = @ProductVariant,
	[ProductVolume] = @ProductVolume,
	[ProductWeight] = @ProductWeight,
	[PromisedDate] = @PromisedDate,
	[PurchaseFor] = @PurchaseFor,
	[PurchaseOrderNo] = @PurchaseOrderNo,
	[PurchaseRequisitionDetail] = @PurchaseRequisitionDetail,
	[PurchaseUnit] = @PurchaseUnit,
	[QtyOrder] = @QtyOrder,
	[QtyReceipt] = @QtyReceipt,
	[QtyReturn] = @QtyReturn,
	[RecallProduct] = @RecallProduct,
	[ReferenceNo] = @ReferenceNo,
	[RequiredDate] = @RequiredDate,
	[SalesOrderDetail] = @SalesOrderDetail,
	[ScheduledShippingDate] = @ScheduledShippingDate,
	[ServicePartsAndMaterial] = @ServicePartsAndMaterial,
	[ShippingDate] = @ShippingDate,
	[Site] = @Site,
	[StockNumber] = @StockNumber,
	[TitleRegistrationFee] = @TitleRegistrationFee,
	[TotalAmount] = @TotalAmount,
	[TotalAmountBeforeDiscount] = @TotalAmountBeforeDiscount,
	[TotalBaseAmount] = @TotalBaseAmount,
	[TotalConsumptionTaxAmount] = @TotalConsumptionTaxAmount,
	[TotalVolume] = @TotalVolume,
	[TotalWeight] = @TotalWeight,
	[TransactionAmount] = @TransactionAmount,
	[UnitCost] = @UnitCost,
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
-- Date Created	: 20 Maret 2018
-- Created By	: Mitrais Team by using CodeSmith v 2.6
-- Rev History	:
---------------------------------------------------------------------------------------------------------------

CREATE PROCEDURE [dbo].up_DeletePOOtherVendorDetail
	@ID int OUTPUT	
AS

DELETE
FROM	[dbo].[POOtherVendorDetail]
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
-- Date Created	: 20 Maret 2018
-- Created By	: Mitrais Team by using CodeSmith v 2.6
-- Rev History	:
---------------------------------------------------------------------------------------------------------------

CREATE PROCEDURE [dbo].up_ValidatePOOtherVendorDetail
	@Result	varchar(1000),
	@ID int OUTPUT,
	@POOtherVendorID int,
	@Owner varchar(100),
	@DealerCode varchar(100),
	@CloseLine bit,
	@CloseReason varchar(100),
	@Completed bit,
	@ConsumptionTax1Amount money,
	@ConsumptionTax1 varchar(100),
	@ConsumptionTax2Amount money,
	@ConsumptionTax2 varchar(100),
	@Department varchar(100),
	@Description varchar(100),
	@DiscountAmount money,
	@DiscountPercentage float,
	@EventData varchar(100),
	@FormSource smallint,
	@BaseQtyOrder float,
	@BaseQtyReceipt float,
	@BaseQtyReturn float,
	@InventoryUnit varchar(100),
	@ProductCrossReference varchar(100),
	@ProductDescription varchar(100),
	@Product varchar(100),
	@ProductSubstitute varchar(100),
	@ProductVariant varchar(100),
	@ProductVolume float,
	@ProductWeight float,
	@PromisedDate datetime,
	@PurchaseFor smallint,
	@PurchaseOrderNo varchar(100),
	@PurchaseRequisitionDetail varchar(100),
	@PurchaseUnit varchar(100),
	@QtyOrder float,
	@QtyReceipt float,
	@QtyReturn float,
	@RecallProduct bit,
	@ReferenceNo varchar(100),
	@RequiredDate datetime,
	@SalesOrderDetail varchar(100),
	@ScheduledShippingDate datetime,
	@ServicePartsAndMaterial varchar(100),
	@ShippingDate datetime,
	@Site varchar(100),
	@StockNumber varchar(100),
	@TitleRegistrationFee money,
	@TotalAmount money,
	@TotalAmountBeforeDiscount money,
	@TotalBaseAmount money,
	@TotalConsumptionTaxAmount money,
	@TotalVolume float,
	@TotalWeight float,
	@TransactionAmount money,
	@UnitCost money,
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


SET QUOTED_IDENTIFIER ON 
GO
SET ANSI_NULLS OFF 
GO
---------------------------------------------------------------------------------------------------------------
-- Date Created	: 20 Maret 2018
-- Created By	: Mitrais Team by using CodeSmith v 2.6
-- Rev History	:
---------------------------------------------------------------------------------------------------------------
GRANT  EXECUTE  ON [dbo].up_InsertPOOtherVendorDetail TO [bsi];

GRANT  EXECUTE  ON [dbo].up_UpdatePOOtherVendorDetail TO [bsi];

GRANT  EXECUTE  ON [dbo].up_RetrievePOOtherVendorDetail TO [bsi];

GRANT  EXECUTE  ON [dbo].up_RetrievePOOtherVendorDetailList TO [bsi];

GRANT  EXECUTE  ON [dbo].up_ValidatePOOtherVendorDetail TO [bsi];

GRANT  EXECUTE  ON [dbo].up_DeletePOOtherVendorDetail TO [bsi];

SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO



