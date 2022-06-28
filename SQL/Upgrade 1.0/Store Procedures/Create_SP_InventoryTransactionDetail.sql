
/****** Object:  Stored Procedure [dbo].[up_UpdateInventoryTransactionDetail]    Script Date: 26 Maret 2018 ******/
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'[dbo].[up_InsertInventoryTransactionDetail]') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
DROP PROCEDURE [dbo].[up_InsertInventoryTransactionDetail]
GO


/****** Object:  Stored Procedure [dbo].[up_RetrieveInventoryTransactionDetail]    Script Date: 26 Maret 2018 ******/
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'[dbo].[up_RetrieveInventoryTransactionDetail]') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
DROP PROCEDURE [dbo].[up_RetrieveInventoryTransactionDetail]
GO


/****** Object:  Stored Procedure [dbo].[up_RetrieveInventoryTransactionDetailList]    Script Date: 26 Maret 2018 ******/
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'[dbo].[up_RetrieveInventoryTransactionDetailList]') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
DROP PROCEDURE [dbo].[up_RetrieveInventoryTransactionDetailList]
GO


/****** Object:  Stored Procedure [dbo].[up_UpdateInventoryTransactionDetail]    Script Date: 26 Maret 2018 ******/
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'[dbo].[up_UpdateInventoryTransactionDetail]') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
DROP PROCEDURE [dbo].[up_UpdateInventoryTransactionDetail]
GO


/****** Object:  Stored Procedure [dbo].[up_DeleteInventoryTransactionDetail]    Script Date: 26 Maret 2018 ******/
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'[dbo].[up_DeleteInventoryTransactionDetail]') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
DROP PROCEDURE [dbo].[up_DeleteInventoryTransactionDetail]
GO


/****** Object:  Stored Procedure [dbo].[up_ValidateInventoryTransactionDetail]    Script Date: 26 Maret 2018 ******/
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'[dbo].[up_ValidateInventoryTransactionDetail]') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
DROP PROCEDURE [dbo].[up_ValidateInventoryTransactionDetail]
GO



SET QUOTED_IDENTIFIER ON 
GO
SET ANSI_NULLS OFF 
GO
---------------------------------------------------------------------------------------------------------------
-- Date Created	: 26 Maret 2018
-- Created By	: Mitrais Team by using CodeSmith v 2.6
-- Rev History	:
---------------------------------------------------------------------------------------------------------------

CREATE PROCEDURE [dbo].up_InsertInventoryTransactionDetail
	@ID int OUTPUT,
	@Owner varchar(100),
	@BaseQuantity float,
	@BatchNo varchar(100),
	@BU varchar(100),
	@Department varchar(100),
	@Description varchar(100),
	@FromBU varchar(100),
	@InventoryTransactionID int,
	@InventoryTransactionNo varchar(100),
	@InventoryTransferDetail varchar(100),
	@InventoryUnit varchar(100),
	@Location varchar(100),
	@ProductCrossReference varchar(100),
	@ProductDescription varchar(100),
	@Product varchar(100),
	@Quantity float,
	@ReasonCode varchar(100),
	@ReferenceNo varchar(100),
	@RegisterSerialNumber varchar(100),
	@RunningNumber int,
	@SerialNo varchar(100),
	@ServicePartsAndMaterial varchar(100),
	@Site varchar(100),
	@SourceData varchar(100),
	@StockNumber varchar(100),
	@StockNumberNV varchar(100),
	@TotalCost money,
	@TransactionType varchar(100),
	@TransactionUnit varchar(100),
	@UnitCost money,
	@VIN varchar(100),
	@Warehouse varchar(100),
	@RowStatus smallint,
	@CreatedBy varchar(20),
	--@CreatedTime datetime,
	@LastUpdateBy varchar(20)	--@LastUpdateTime datetime	
AS
INSERT	INTO	[dbo].[InventoryTransactionDetail]
VALUES
(
	@Owner,
	@BaseQuantity,
	@BatchNo,
	@BU,
	@Department,
	@Description,
	@FromBU,
	@InventoryTransactionID,
	@InventoryTransactionNo,
	@InventoryTransferDetail,
	@InventoryUnit,
	@Location,
	@ProductCrossReference,
	@ProductDescription,
	@Product,
	@Quantity,
	@ReasonCode,
	@ReferenceNo,
	@RegisterSerialNumber,
	@RunningNumber,
	@SerialNo,
	@ServicePartsAndMaterial,
	@Site,
	@SourceData,
	@StockNumber,
	@StockNumberNV,
	@TotalCost,
	@TransactionType,
	@TransactionUnit,
	@UnitCost,
	@VIN,
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
-- Date Created	: 26 Maret 2018
-- Created By	: Mitrais Team by using CodeSmith v 2.6
-- Rev History	:
---------------------------------------------------------------------------------------------------------------

CREATE PROCEDURE [dbo].up_RetrieveInventoryTransactionDetail
	@ID int OUTPUT	
AS
SET TRANSACTION ISOLATION LEVEL READ COMMITTED

SET NOCOUNT ON

SELECT
	[ID],
	[Owner],
	[BaseQuantity],
	[BatchNo],
	[BU],
	[Department],
	[Description],
	[FromBU],
	[InventoryTransactionID],
	[InventoryTransactionNo],
	[InventoryTransferDetail],
	[InventoryUnit],
	[Location],
	[ProductCrossReference],
	[ProductDescription],
	[Product],
	[Quantity],
	[ReasonCode],
	[ReferenceNo],
	[RegisterSerialNumber],
	[RunningNumber],
	[SerialNo],
	[ServicePartsAndMaterial],
	[Site],
	[SourceData],
	[StockNumber],
	[StockNumberNV],
	[TotalCost],
	[TransactionType],
	[TransactionUnit],
	[UnitCost],
	[VIN],
	[Warehouse],
	[RowStatus],
	[CreatedBy],
	[CreatedTime],
	[LastUpdateBy],
	[LastUpdateTime]	
FROM	[dbo].[InventoryTransactionDetail]

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
-- Date Created	: 26 Maret 2018
-- Created By	: Mitrais Team by using CodeSmith v 2.6
-- Rev History	:
---------------------------------------------------------------------------------------------------------------

CREATE PROCEDURE [dbo].up_RetrieveInventoryTransactionDetailList

AS

SET TRANSACTION ISOLATION LEVEL READ COMMITTED
SET NOCOUNT ON


SELECT
		[ID],
		[Owner],
		[BaseQuantity],
		[BatchNo],
		[BU],
		[Department],
		[Description],
		[FromBU],
		[InventoryTransactionID],
		[InventoryTransactionNo],
		[InventoryTransferDetail],
		[InventoryUnit],
		[Location],
		[ProductCrossReference],
		[ProductDescription],
		[Product],
		[Quantity],
		[ReasonCode],
		[ReferenceNo],
		[RegisterSerialNumber],
		[RunningNumber],
		[SerialNo],
		[ServicePartsAndMaterial],
		[Site],
		[SourceData],
		[StockNumber],
		[StockNumberNV],
		[TotalCost],
		[TransactionType],
		[TransactionUnit],
		[UnitCost],
		[VIN],
		[Warehouse],
		[RowStatus],
		[CreatedBy],
		[CreatedTime],
		[LastUpdateBy],
		[LastUpdateTime]		
		FROM	
		[dbo].[InventoryTransactionDetail] 

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
-- Date Created	: 26 Maret 2018
-- Created By	: Mitrais Team by using CodeSmith v 2.6
-- Rev History	:
---------------------------------------------------------------------------------------------------------------

CREATE PROCEDURE [dbo].up_UpdateInventoryTransactionDetail
	@ID int OUTPUT,
	@Owner varchar(100),
	@BaseQuantity float,
	@BatchNo varchar(100),
	@BU varchar(100),
	@Department varchar(100),
	@Description varchar(100),
	@FromBU varchar(100),
	@InventoryTransactionID int,
	@InventoryTransactionNo varchar(100),
	@InventoryTransferDetail varchar(100),
	@InventoryUnit varchar(100),
	@Location varchar(100),
	@ProductCrossReference varchar(100),
	@ProductDescription varchar(100),
	@Product varchar(100),
	@Quantity float,
	@ReasonCode varchar(100),
	@ReferenceNo varchar(100),
	@RegisterSerialNumber varchar(100),
	@RunningNumber int,
	@SerialNo varchar(100),
	@ServicePartsAndMaterial varchar(100),
	@Site varchar(100),
	@SourceData varchar(100),
	@StockNumber varchar(100),
	@StockNumberNV varchar(100),
	@TotalCost money,
	@TransactionType varchar(100),
	@TransactionUnit varchar(100),
	@UnitCost money,
	@VIN varchar(100),
	@Warehouse varchar(100),
	@RowStatus smallint,
	@CreatedBy varchar(20),
	--@CreatedTime datetime,
	@LastUpdateBy varchar(20)	--@LastUpdateTime datetime	
AS

UPDATE	[dbo].[InventoryTransactionDetail]
SET
	[Owner] = @Owner,
	[BaseQuantity] = @BaseQuantity,
	[BatchNo] = @BatchNo,
	[BU] = @BU,
	[Department] = @Department,
	[Description] = @Description,
	[FromBU] = @FromBU,
	[InventoryTransactionID] = @InventoryTransactionID,
	[InventoryTransactionNo] = @InventoryTransactionNo,
	[InventoryTransferDetail] = @InventoryTransferDetail,
	[InventoryUnit] = @InventoryUnit,
	[Location] = @Location,
	[ProductCrossReference] = @ProductCrossReference,
	[ProductDescription] = @ProductDescription,
	[Product] = @Product,
	[Quantity] = @Quantity,
	[ReasonCode] = @ReasonCode,
	[ReferenceNo] = @ReferenceNo,
	[RegisterSerialNumber] = @RegisterSerialNumber,
	[RunningNumber] = @RunningNumber,
	[SerialNo] = @SerialNo,
	[ServicePartsAndMaterial] = @ServicePartsAndMaterial,
	[Site] = @Site,
	[SourceData] = @SourceData,
	[StockNumber] = @StockNumber,
	[StockNumberNV] = @StockNumberNV,
	[TotalCost] = @TotalCost,
	[TransactionType] = @TransactionType,
	[TransactionUnit] = @TransactionUnit,
	[UnitCost] = @UnitCost,
	[VIN] = @VIN,
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
-- Date Created	: 26 Maret 2018
-- Created By	: Mitrais Team by using CodeSmith v 2.6
-- Rev History	:
---------------------------------------------------------------------------------------------------------------

CREATE PROCEDURE [dbo].up_DeleteInventoryTransactionDetail
	@ID int OUTPUT	
AS

DELETE
FROM	[dbo].[InventoryTransactionDetail]
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
-- Date Created	: 26 Maret 2018
-- Created By	: Mitrais Team by using CodeSmith v 2.6
-- Rev History	:
---------------------------------------------------------------------------------------------------------------

CREATE PROCEDURE [dbo].up_ValidateInventoryTransactionDetail
	@Result	varchar(1000),
	@ID int OUTPUT,
	@Owner varchar(100),
	@BaseQuantity float,
	@BatchNo varchar(100),
	@BU varchar(100),
	@Department varchar(100),
	@Description varchar(100),
	@FromBU varchar(100),
	@InventoryTransactionID int,
	@InventoryTransactionNo varchar(100),
	@InventoryTransferDetail varchar(100),
	@InventoryUnit varchar(100),
	@Location varchar(100),
	@ProductCrossReference varchar(100),
	@ProductDescription varchar(100),
	@Product varchar(100),
	@Quantity float,
	@ReasonCode varchar(100),
	@ReferenceNo varchar(100),
	@RegisterSerialNumber varchar(100),
	@RunningNumber int,
	@SerialNo varchar(100),
	@ServicePartsAndMaterial varchar(100),
	@Site varchar(100),
	@SourceData varchar(100),
	@StockNumber varchar(100),
	@StockNumberNV varchar(100),
	@TotalCost money,
	@TransactionType varchar(100),
	@TransactionUnit varchar(100),
	@UnitCost money,
	@VIN varchar(100),
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
-- Date Created	: 26 Maret 2018
-- Created By	: Mitrais Team by using CodeSmith v 2.6
-- Rev History	:
---------------------------------------------------------------------------------------------------------------
GRANT  EXECUTE  ON [dbo].up_InsertInventoryTransactionDetail TO [bsi];

GRANT  EXECUTE  ON [dbo].up_UpdateInventoryTransactionDetail TO [bsi];

GRANT  EXECUTE  ON [dbo].up_RetrieveInventoryTransactionDetail TO [bsi];

GRANT  EXECUTE  ON [dbo].up_RetrieveInventoryTransactionDetailList TO [bsi];

GRANT  EXECUTE  ON [dbo].up_ValidateInventoryTransactionDetail TO [bsi];

GRANT  EXECUTE  ON [dbo].up_DeleteInventoryTransactionDetail TO [bsi];

SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO



