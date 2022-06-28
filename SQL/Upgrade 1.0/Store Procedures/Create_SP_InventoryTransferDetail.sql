


SET QUOTED_IDENTIFIER ON 
GO
SET ANSI_NULLS OFF 
GO
---------------------------------------------------------------------------------------------------------------
-- Date Created	: 25 Maret 2018
-- Created By	: Mitrais Team by using CodeSmith v 2.6
-- Rev History	:
---------------------------------------------------------------------------------------------------------------

CREATE PROCEDURE [dbo].up_InsertInventoryTransferDetail
	@ID int OUTPUT,
	@InventoryTransferID int,
	@Owner varchar(100),
	@BaseQuantity float,
	@ConsumptionTaxIn varchar(100),
	@ConsumptionTaxOut varchar(100),
	@FromBatchNo varchar(100),
	@FromDealer varchar(100),
	@FromConfiguration varchar(100),
	@FromExteriorColor varchar(100),
	@FromInteriorColor varchar(100),
	@FromLocation varchar(100),
	@FromSerialNo varchar(100),
	@FromSite varchar(100),
	@FromStyle varchar(100),
	@FromWarehouse varchar(100),
	@InventoryTransferNo varchar(100),
	@InventoryUnit varchar(100),
	@ProductDescription varchar(100),
	@Product varchar(100),
	@Quantity float,
	@Remarks varchar(100),
	@ServicePartsandMaterial varchar(100),
	@SourceData varchar(50),
	@StockNumber varchar(100),
	@StockNumberNV varchar(100),
	@StockNumberLookupName varchar(200),
	@StockNumberLookupType int,
	@ToBatchNo varchar(100),
	@ToDealer varchar(100),
	@ToConfiguration varchar(100),
	@ToExteriorColor varchar(100),
	@ToInteriorColor varchar(100),
	@ToLocation varchar(100),
	@ToSerialNo varchar(100),
	@ToSite varchar(100),
	@ToStyle varchar(100),
	@ToWarehouse varchar(100),
	@TransactionUnit varchar(100),
	@VIN varchar(50),
	@RowStatus smallint,
	@CreatedBy varchar(100),
	--@CreatedTime datetime,
	@LastUpdateBy varchar(100)	--@LastUpdateTime datetime	
AS
INSERT	INTO	[dbo].[InventoryTransferDetail]
VALUES
(
	@InventoryTransferID,
	@Owner,
	@BaseQuantity,
	@ConsumptionTaxIn,
	@ConsumptionTaxOut,
	@FromBatchNo,
	@FromDealer,
	@FromConfiguration,
	@FromExteriorColor,
	@FromInteriorColor,
	@FromLocation,
	@FromSerialNo,
	@FromSite,
	@FromStyle,
	@FromWarehouse,
	@InventoryTransferNo,
	@InventoryUnit,
	@ProductDescription,
	@Product,
	@Quantity,
	@Remarks,
	@ServicePartsandMaterial,
	@SourceData,
	@StockNumber,
	@StockNumberNV,
	@StockNumberLookupName,
	@StockNumberLookupType,
	@ToBatchNo,
	@ToDealer,
	@ToConfiguration,
	@ToExteriorColor,
	@ToInteriorColor,
	@ToLocation,
	@ToSerialNo,
	@ToSite,
	@ToStyle,
	@ToWarehouse,
	@TransactionUnit,
	@VIN,
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
-- Date Created	: 25 Maret 2018
-- Created By	: Mitrais Team by using CodeSmith v 2.6
-- Rev History	:
---------------------------------------------------------------------------------------------------------------

CREATE PROCEDURE [dbo].up_RetrieveInventoryTransferDetail
	@ID int OUTPUT	
AS
SET TRANSACTION ISOLATION LEVEL READ COMMITTED

SET NOCOUNT ON

SELECT
	[ID],
	[InventoryTransferID],
	[Owner],
	[BaseQuantity],
	[ConsumptionTaxIn],
	[ConsumptionTaxOut],
	[FromBatchNo],
	[FromDealer],
	[FromConfiguration],
	[FromExteriorColor],
	[FromInteriorColor],
	[FromLocation],
	[FromSerialNo],
	[FromSite],
	[FromStyle],
	[FromWarehouse],
	[InventoryTransferNo],
	[InventoryUnit],
	[ProductDescription],
	[Product],
	[Quantity],
	[Remarks],
	[ServicePartsandMaterial],
	[SourceData],
	[StockNumber],
	[StockNumberNV],
	[StockNumberLookupName],
	[StockNumberLookupType],
	[ToBatchNo],
	[ToDealer],
	[ToConfiguration],
	[ToExteriorColor],
	[ToInteriorColor],
	[ToLocation],
	[ToSerialNo],
	[ToSite],
	[ToStyle],
	[ToWarehouse],
	[TransactionUnit],
	[VIN],
	[RowStatus],
	[CreatedBy],
	[CreatedTime],
	[LastUpdateBy],
	[LastUpdateTime]	
FROM	[dbo].[InventoryTransferDetail]

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
-- Date Created	: 25 Maret 2018
-- Created By	: Mitrais Team by using CodeSmith v 2.6
-- Rev History	:
---------------------------------------------------------------------------------------------------------------

CREATE PROCEDURE [dbo].up_RetrieveInventoryTransferDetailList

AS

SET TRANSACTION ISOLATION LEVEL READ COMMITTED
SET NOCOUNT ON


SELECT
		[ID],
		[InventoryTransferID],
		[Owner],
		[BaseQuantity],
		[ConsumptionTaxIn],
		[ConsumptionTaxOut],
		[FromBatchNo],
		[FromDealer],
		[FromConfiguration],
		[FromExteriorColor],
		[FromInteriorColor],
		[FromLocation],
		[FromSerialNo],
		[FromSite],
		[FromStyle],
		[FromWarehouse],
		[InventoryTransferNo],
		[InventoryUnit],
		[ProductDescription],
		[Product],
		[Quantity],
		[Remarks],
		[ServicePartsandMaterial],
		[SourceData],
		[StockNumber],
		[StockNumberNV],
		[StockNumberLookupName],
		[StockNumberLookupType],
		[ToBatchNo],
		[ToDealer],
		[ToConfiguration],
		[ToExteriorColor],
		[ToInteriorColor],
		[ToLocation],
		[ToSerialNo],
		[ToSite],
		[ToStyle],
		[ToWarehouse],
		[TransactionUnit],
		[VIN],
		[RowStatus],
		[CreatedBy],
		[CreatedTime],
		[LastUpdateBy],
		[LastUpdateTime]		
		FROM	
		[dbo].[InventoryTransferDetail] 

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
-- Date Created	: 25 Maret 2018
-- Created By	: Mitrais Team by using CodeSmith v 2.6
-- Rev History	:
---------------------------------------------------------------------------------------------------------------

CREATE PROCEDURE [dbo].up_UpdateInventoryTransferDetail
	@ID int OUTPUT,
	@InventoryTransferID int,
	@Owner varchar(100),
	@BaseQuantity float,
	@ConsumptionTaxIn varchar(100),
	@ConsumptionTaxOut varchar(100),
	@FromBatchNo varchar(100),
	@FromDealer varchar(100),
	@FromConfiguration varchar(100),
	@FromExteriorColor varchar(100),
	@FromInteriorColor varchar(100),
	@FromLocation varchar(100),
	@FromSerialNo varchar(100),
	@FromSite varchar(100),
	@FromStyle varchar(100),
	@FromWarehouse varchar(100),
	@InventoryTransferNo varchar(100),
	@InventoryUnit varchar(100),
	@ProductDescription varchar(100),
	@Product varchar(100),
	@Quantity float,
	@Remarks varchar(100),
	@ServicePartsandMaterial varchar(100),
	@SourceData varchar(50),
	@StockNumber varchar(100),
	@StockNumberNV varchar(100),
	@StockNumberLookupName varchar(200),
	@StockNumberLookupType int,
	@ToBatchNo varchar(100),
	@ToDealer varchar(100),
	@ToConfiguration varchar(100),
	@ToExteriorColor varchar(100),
	@ToInteriorColor varchar(100),
	@ToLocation varchar(100),
	@ToSerialNo varchar(100),
	@ToSite varchar(100),
	@ToStyle varchar(100),
	@ToWarehouse varchar(100),
	@TransactionUnit varchar(100),
	@VIN varchar(50),
	@RowStatus smallint,
	@CreatedBy varchar(100),
	--@CreatedTime datetime,
	@LastUpdateBy varchar(100)	--@LastUpdateTime datetime	
AS

UPDATE	[dbo].[InventoryTransferDetail]
SET
	[InventoryTransferID] = @InventoryTransferID,
	[Owner] = @Owner,
	[BaseQuantity] = @BaseQuantity,
	[ConsumptionTaxIn] = @ConsumptionTaxIn,
	[ConsumptionTaxOut] = @ConsumptionTaxOut,
	[FromBatchNo] = @FromBatchNo,
	[FromDealer] = @FromDealer,
	[FromConfiguration] = @FromConfiguration,
	[FromExteriorColor] = @FromExteriorColor,
	[FromInteriorColor] = @FromInteriorColor,
	[FromLocation] = @FromLocation,
	[FromSerialNo] = @FromSerialNo,
	[FromSite] = @FromSite,
	[FromStyle] = @FromStyle,
	[FromWarehouse] = @FromWarehouse,
	[InventoryTransferNo] = @InventoryTransferNo,
	[InventoryUnit] = @InventoryUnit,
	[ProductDescription] = @ProductDescription,
	[Product] = @Product,
	[Quantity] = @Quantity,
	[Remarks] = @Remarks,
	[ServicePartsandMaterial] = @ServicePartsandMaterial,
	[SourceData] = @SourceData,
	[StockNumber] = @StockNumber,
	[StockNumberNV] = @StockNumberNV,
	[StockNumberLookupName] = @StockNumberLookupName,
	[StockNumberLookupType] = @StockNumberLookupType,
	[ToBatchNo] = @ToBatchNo,
	[ToDealer] = @ToDealer,
	[ToConfiguration] = @ToConfiguration,
	[ToExteriorColor] = @ToExteriorColor,
	[ToInteriorColor] = @ToInteriorColor,
	[ToLocation] = @ToLocation,
	[ToSerialNo] = @ToSerialNo,
	[ToSite] = @ToSite,
	[ToStyle] = @ToStyle,
	[ToWarehouse] = @ToWarehouse,
	[TransactionUnit] = @TransactionUnit,
	[VIN] = @VIN,
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
-- Date Created	: 25 Maret 2018
-- Created By	: Mitrais Team by using CodeSmith v 2.6
-- Rev History	:
---------------------------------------------------------------------------------------------------------------

CREATE PROCEDURE [dbo].up_DeleteInventoryTransferDetail
	@ID int OUTPUT	
AS

DELETE
FROM	[dbo].[InventoryTransferDetail]
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
-- Date Created	: 25 Maret 2018
-- Created By	: Mitrais Team by using CodeSmith v 2.6
-- Rev History	:
---------------------------------------------------------------------------------------------------------------

CREATE PROCEDURE [dbo].up_ValidateInventoryTransferDetail
	@Result	varchar(1000),
	@ID int OUTPUT,
	@InventoryTransferID int,
	@Owner varchar(100),
	@BaseQuantity float,
	@ConsumptionTaxIn varchar(100),
	@ConsumptionTaxOut varchar(100),
	@FromBatchNo varchar(100),
	@FromDealer varchar(100),
	@FromConfiguration varchar(100),
	@FromExteriorColor varchar(100),
	@FromInteriorColor varchar(100),
	@FromLocation varchar(100),
	@FromSerialNo varchar(100),
	@FromSite varchar(100),
	@FromStyle varchar(100),
	@FromWarehouse varchar(100),
	@InventoryTransferNo varchar(100),
	@InventoryUnit varchar(100),
	@ProductDescription varchar(100),
	@Product varchar(100),
	@Quantity float,
	@Remarks varchar(100),
	@ServicePartsandMaterial varchar(100),
	@SourceData varchar(50),
	@StockNumber varchar(100),
	@StockNumberNV varchar(100),
	@StockNumberLookupName varchar(200),
	@StockNumberLookupType int,
	@ToBatchNo varchar(100),
	@ToDealer varchar(100),
	@ToConfiguration varchar(100),
	@ToExteriorColor varchar(100),
	@ToInteriorColor varchar(100),
	@ToLocation varchar(100),
	@ToSerialNo varchar(100),
	@ToSite varchar(100),
	@ToStyle varchar(100),
	@ToWarehouse varchar(100),
	@TransactionUnit varchar(100),
	@VIN varchar(50),
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




