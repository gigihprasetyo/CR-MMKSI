USE [BSIDNET_MMKSI_DMS]
GO

/****** Object:  Stored Procedure [dbo].[up_UpdateSparePartPRDetailFromVendor]    Script Date: 21 Maret 2018 ******/
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'[dbo].[up_InsertSparePartPRDetailFromVendor]') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
DROP PROCEDURE [dbo].[up_InsertSparePartPRDetailFromVendor]
GO


/****** Object:  Stored Procedure [dbo].[up_RetrieveSparePartPRDetailFromVendor]    Script Date: 21 Maret 2018 ******/
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'[dbo].[up_RetrieveSparePartPRDetailFromVendor]') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
DROP PROCEDURE [dbo].[up_RetrieveSparePartPRDetailFromVendor]
GO


/****** Object:  Stored Procedure [dbo].[up_RetrieveSparePartPRDetailFromVendorList]    Script Date: 21 Maret 2018 ******/
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'[dbo].[up_RetrieveSparePartPRDetailFromVendorList]') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
DROP PROCEDURE [dbo].[up_RetrieveSparePartPRDetailFromVendorList]
GO


/****** Object:  Stored Procedure [dbo].[up_UpdateSparePartPRDetailFromVendor]    Script Date: 21 Maret 2018 ******/
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'[dbo].[up_UpdateSparePartPRDetailFromVendor]') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
DROP PROCEDURE [dbo].[up_UpdateSparePartPRDetailFromVendor]
GO


/****** Object:  Stored Procedure [dbo].[up_DeleteSparePartPRDetailFromVendor]    Script Date: 21 Maret 2018 ******/
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'[dbo].[up_DeleteSparePartPRDetailFromVendor]') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
DROP PROCEDURE [dbo].[up_DeleteSparePartPRDetailFromVendor]
GO


/****** Object:  Stored Procedure [dbo].[up_ValidateSparePartPRDetailFromVendor]    Script Date: 21 Maret 2018 ******/
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'[dbo].[up_ValidateSparePartPRDetailFromVendor]') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
DROP PROCEDURE [dbo].[up_ValidateSparePartPRDetailFromVendor]
GO



SET QUOTED_IDENTIFIER ON 
GO
SET ANSI_NULLS OFF 
GO
---------------------------------------------------------------------------------------------------------------
-- Date Created	: 21 Maret 2018
-- Created By	: Mitrais Team by using CodeSmith v 2.6
-- Rev History	:
---------------------------------------------------------------------------------------------------------------

CREATE PROCEDURE [dbo].up_InsertSparePartPRDetailFromVendor
	@ID int OUTPUT,
	@PRDetailNumber varchar(50),
	@SparePartPRID int,
	@PRNumber varchar(100),
	@Owner varchar(100),
	@BaseReceivedQuantity decimal(18, 9),
	@BatchNumber varchar(100),
	@DealerCode varchar(100),
	@ChassisModel varchar(50),
	@ChassisNumberRegister varchar(50),
	@ConsumptionTax1Amount money,
	@ConsumptionTax1 varchar(100),
	@ConsumptionTax2Amount money,
	@ConsumptionTax2 varchar(100),
	@DiscountAmount money,
	@EngineNumber varchar(50),
	@EventData varchar(1000),
	@InventoryUnit varchar(100),
	@KeyNumber varchar(50),
	@LandedCost money,
	@Location varchar(100),
	@ProductDescription varchar(100),
	@Product varchar(100),
	@ProductVolume decimal(18, 9),
	@ProductWeight decimal(18, 9),
	@PurchaseUnit varchar(100),
	@ReceivedQuantity decimal(18, 9),
	@ReferenceNumber varchar(50),
	@ReturnPRDetail varchar(100),
	@ServicePartsAndMaterial varchar(100),
	@Site varchar(100),
	@StockNumber varchar(100),
	@TitleRegistrationFee money,
	@TotalAmount money,
	@TotalBaseAmount money,
	@TotalConsumptionTaxAmount money,
	@TotalVolume decimal(18, 9),
	@TotalWeight decimal(18, 9),
	@TransactionAmount money,
	@UnitCost money,
	@Warehouse varchar(100),
	@RowStatus smallint,
	@CreatedBy varchar(100),
	--@CreatedTime datetime,
	@LastUpdateBy varchar(100)	--@LastUpdateTime datetime	
AS
INSERT	INTO	[dbo].[SparePartPRDetailFromVendor]
VALUES
(
	@PRDetailNumber,
	@SparePartPRID,
	@PRNumber,
	@Owner,
	@BaseReceivedQuantity,
	@BatchNumber,
	@DealerCode,
	@ChassisModel,
	@ChassisNumberRegister,
	@ConsumptionTax1Amount,
	@ConsumptionTax1,
	@ConsumptionTax2Amount,
	@ConsumptionTax2,
	@DiscountAmount,
	@EngineNumber,
	@EventData,
	@InventoryUnit,
	@KeyNumber,
	@LandedCost,
	@Location,
	@ProductDescription,
	@Product,
	@ProductVolume,
	@ProductWeight,
	@PurchaseUnit,
	@ReceivedQuantity,
	@ReferenceNumber,
	@ReturnPRDetail,
	@ServicePartsAndMaterial,
	@Site,
	@StockNumber,
	@TitleRegistrationFee,
	@TotalAmount,
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
-- Date Created	: 21 Maret 2018
-- Created By	: Mitrais Team by using CodeSmith v 2.6
-- Rev History	:
---------------------------------------------------------------------------------------------------------------

CREATE PROCEDURE [dbo].up_RetrieveSparePartPRDetailFromVendor
	@ID int OUTPUT	
AS
SET TRANSACTION ISOLATION LEVEL READ COMMITTED

SET NOCOUNT ON

SELECT
	[ID],
	[PRDetailNumber],
	[SparePartPRID],
	[PRNumber],
	[Owner],
	[BaseReceivedQuantity],
	[BatchNumber],
	[DealerCode],
	[ChassisModel],
	[ChassisNumberRegister],
	[ConsumptionTax1Amount],
	[ConsumptionTax1],
	[ConsumptionTax2Amount],
	[ConsumptionTax2],
	[DiscountAmount],
	[EngineNumber],
	[EventData],
	[InventoryUnit],
	[KeyNumber],
	[LandedCost],
	[Location],
	[ProductDescription],
	[Product],
	[ProductVolume],
	[ProductWeight],
	[PurchaseUnit],
	[ReceivedQuantity],
	[ReferenceNumber],
	[ReturnPRDetail],
	[ServicePartsAndMaterial],
	[Site],
	[StockNumber],
	[TitleRegistrationFee],
	[TotalAmount],
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
FROM	[dbo].[SparePartPRDetailFromVendor]

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
-- Date Created	: 21 Maret 2018
-- Created By	: Mitrais Team by using CodeSmith v 2.6
-- Rev History	:
---------------------------------------------------------------------------------------------------------------

CREATE PROCEDURE [dbo].up_RetrieveSparePartPRDetailFromVendorList

AS

SET TRANSACTION ISOLATION LEVEL READ COMMITTED
SET NOCOUNT ON


SELECT
		[ID],
		[PRDetailNumber],
		[SparePartPRID],
		[PRNumber],
		[Owner],
		[BaseReceivedQuantity],
		[BatchNumber],
		[DealerCode],
		[ChassisModel],
		[ChassisNumberRegister],
		[ConsumptionTax1Amount],
		[ConsumptionTax1],
		[ConsumptionTax2Amount],
		[ConsumptionTax2],
		[DiscountAmount],
		[EngineNumber],
		[EventData],
		[InventoryUnit],
		[KeyNumber],
		[LandedCost],
		[Location],
		[ProductDescription],
		[Product],
		[ProductVolume],
		[ProductWeight],
		[PurchaseUnit],
		[ReceivedQuantity],
		[ReferenceNumber],
		[ReturnPRDetail],
		[ServicePartsAndMaterial],
		[Site],
		[StockNumber],
		[TitleRegistrationFee],
		[TotalAmount],
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
		[dbo].[SparePartPRDetailFromVendor] 

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
-- Date Created	: 21 Maret 2018
-- Created By	: Mitrais Team by using CodeSmith v 2.6
-- Rev History	:
---------------------------------------------------------------------------------------------------------------

CREATE PROCEDURE [dbo].up_UpdateSparePartPRDetailFromVendor
	@ID int OUTPUT,
	@PRDetailNumber varchar(50),
	@SparePartPRID int,
	@PRNumber varchar(100),
	@Owner varchar(100),
	@BaseReceivedQuantity decimal(18, 9),
	@BatchNumber varchar(100),
	@DealerCode varchar(100),
	@ChassisModel varchar(50),
	@ChassisNumberRegister varchar(50),
	@ConsumptionTax1Amount money,
	@ConsumptionTax1 varchar(100),
	@ConsumptionTax2Amount money,
	@ConsumptionTax2 varchar(100),
	@DiscountAmount money,
	@EngineNumber varchar(50),
	@EventData varchar(1000),
	@InventoryUnit varchar(100),
	@KeyNumber varchar(50),
	@LandedCost money,
	@Location varchar(100),
	@ProductDescription varchar(100),
	@Product varchar(100),
	@ProductVolume decimal(18, 9),
	@ProductWeight decimal(18, 9),
	@PurchaseUnit varchar(100),
	@ReceivedQuantity decimal(18, 9),
	@ReferenceNumber varchar(50),
	@ReturnPRDetail varchar(100),
	@ServicePartsAndMaterial varchar(100),
	@Site varchar(100),
	@StockNumber varchar(100),
	@TitleRegistrationFee money,
	@TotalAmount money,
	@TotalBaseAmount money,
	@TotalConsumptionTaxAmount money,
	@TotalVolume decimal(18, 9),
	@TotalWeight decimal(18, 9),
	@TransactionAmount money,
	@UnitCost money,
	@Warehouse varchar(100),
	@RowStatus smallint,
	@CreatedBy varchar(100),
	--@CreatedTime datetime,
	@LastUpdateBy varchar(100)	--@LastUpdateTime datetime	
AS

UPDATE	[dbo].[SparePartPRDetailFromVendor]
SET
	[PRDetailNumber] = @PRDetailNumber,
	[SparePartPRID] = @SparePartPRID,
	[PRNumber] = @PRNumber,
	[Owner] = @Owner,
	[BaseReceivedQuantity] = @BaseReceivedQuantity,
	[BatchNumber] = @BatchNumber,
	[DealerCode] = @DealerCode,
	[ChassisModel] = @ChassisModel,
	[ChassisNumberRegister] = @ChassisNumberRegister,
	[ConsumptionTax1Amount] = @ConsumptionTax1Amount,
	[ConsumptionTax1] = @ConsumptionTax1,
	[ConsumptionTax2Amount] = @ConsumptionTax2Amount,
	[ConsumptionTax2] = @ConsumptionTax2,
	[DiscountAmount] = @DiscountAmount,
	[EngineNumber] = @EngineNumber,
	[EventData] = @EventData,
	[InventoryUnit] = @InventoryUnit,
	[KeyNumber] = @KeyNumber,
	[LandedCost] = @LandedCost,
	[Location] = @Location,
	[ProductDescription] = @ProductDescription,
	[Product] = @Product,
	[ProductVolume] = @ProductVolume,
	[ProductWeight] = @ProductWeight,
	[PurchaseUnit] = @PurchaseUnit,
	[ReceivedQuantity] = @ReceivedQuantity,
	[ReferenceNumber] = @ReferenceNumber,
	[ReturnPRDetail] = @ReturnPRDetail,
	[ServicePartsAndMaterial] = @ServicePartsAndMaterial,
	[Site] = @Site,
	[StockNumber] = @StockNumber,
	[TitleRegistrationFee] = @TitleRegistrationFee,
	[TotalAmount] = @TotalAmount,
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
-- Date Created	: 21 Maret 2018
-- Created By	: Mitrais Team by using CodeSmith v 2.6
-- Rev History	:
---------------------------------------------------------------------------------------------------------------

CREATE PROCEDURE [dbo].up_DeleteSparePartPRDetailFromVendor
	@ID int OUTPUT	
AS

DELETE
FROM	[dbo].[SparePartPRDetailFromVendor]
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
-- Date Created	: 21 Maret 2018
-- Created By	: Mitrais Team by using CodeSmith v 2.6
-- Rev History	:
---------------------------------------------------------------------------------------------------------------

CREATE PROCEDURE [dbo].up_ValidateSparePartPRDetailFromVendor
	@Result	varchar(1000),
	@ID int OUTPUT,
	@PRDetailNumber varchar(50),
	@SparePartPRID int,
	@PRNumber varchar(100),
	@Owner varchar(100),
	@BaseReceivedQuantity decimal(18, 9),
	@BatchNumber varchar(100),
	@DealerCode varchar(100),
	@ChassisModel varchar(50),
	@ChassisNumberRegister varchar(50),
	@ConsumptionTax1Amount money,
	@ConsumptionTax1 varchar(100),
	@ConsumptionTax2Amount money,
	@ConsumptionTax2 varchar(100),
	@DiscountAmount money,
	@EngineNumber varchar(50),
	@EventData varchar(1000),
	@InventoryUnit varchar(100),
	@KeyNumber varchar(50),
	@LandedCost money,
	@Location varchar(100),
	@ProductDescription varchar(100),
	@Product varchar(100),
	@ProductVolume decimal(18, 9),
	@ProductWeight decimal(18, 9),
	@PurchaseUnit varchar(100),
	@ReceivedQuantity decimal(18, 9),
	@ReferenceNumber varchar(50),
	@ReturnPRDetail varchar(100),
	@ServicePartsAndMaterial varchar(100),
	@Site varchar(100),
	@StockNumber varchar(100),
	@TitleRegistrationFee money,
	@TotalAmount money,
	@TotalBaseAmount money,
	@TotalConsumptionTaxAmount money,
	@TotalVolume decimal(18, 9),
	@TotalWeight decimal(18, 9),
	@TransactionAmount money,
	@UnitCost money,
	@Warehouse varchar(100),
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


SET QUOTED_IDENTIFIER ON 
GO
SET ANSI_NULLS OFF 
GO
---------------------------------------------------------------------------------------------------------------
-- Date Created	: 21 Maret 2018
-- Created By	: Mitrais Team by using CodeSmith v 2.6
-- Rev History	:
---------------------------------------------------------------------------------------------------------------
GRANT  EXECUTE  ON [dbo].up_InsertSparePartPRDetailFromVendor TO [bsi];

GRANT  EXECUTE  ON [dbo].up_UpdateSparePartPRDetailFromVendor TO [bsi];

GRANT  EXECUTE  ON [dbo].up_RetrieveSparePartPRDetailFromVendor TO [bsi];

GRANT  EXECUTE  ON [dbo].up_RetrieveSparePartPRDetailFromVendorList TO [bsi];

GRANT  EXECUTE  ON [dbo].up_ValidateSparePartPRDetailFromVendor TO [bsi];

GRANT  EXECUTE  ON [dbo].up_DeleteSparePartPRDetailFromVendor TO [bsi];

SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO