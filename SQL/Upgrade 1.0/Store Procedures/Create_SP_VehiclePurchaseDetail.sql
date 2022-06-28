
/****** Object:  Stored Procedure [dbo].[up_UpdateVehiclePurchaseDetail]    Script Date: 05 Maret 2018 ******/
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'[dbo].[up_InsertVehiclePurchaseDetail]') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
DROP PROCEDURE [dbo].[up_InsertVehiclePurchaseDetail]
GO


/****** Object:  Stored Procedure [dbo].[up_RetrieveVehiclePurchaseDetail]    Script Date: 05 Maret 2018 ******/
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'[dbo].[up_RetrieveVehiclePurchaseDetail]') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
DROP PROCEDURE [dbo].[up_RetrieveVehiclePurchaseDetail]
GO


/****** Object:  Stored Procedure [dbo].[up_RetrieveVehiclePurchaseDetailList]    Script Date: 05 Maret 2018 ******/
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'[dbo].[up_RetrieveVehiclePurchaseDetailList]') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
DROP PROCEDURE [dbo].[up_RetrieveVehiclePurchaseDetailList]
GO


/****** Object:  Stored Procedure [dbo].[up_UpdateVehiclePurchaseDetail]    Script Date: 05 Maret 2018 ******/
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'[dbo].[up_UpdateVehiclePurchaseDetail]') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
DROP PROCEDURE [dbo].[up_UpdateVehiclePurchaseDetail]
GO


/****** Object:  Stored Procedure [dbo].[up_DeleteVehiclePurchaseDetail]    Script Date: 05 Maret 2018 ******/
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'[dbo].[up_DeleteVehiclePurchaseDetail]') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
DROP PROCEDURE [dbo].[up_DeleteVehiclePurchaseDetail]
GO


/****** Object:  Stored Procedure [dbo].[up_ValidateVehiclePurchaseDetail]    Script Date: 05 Maret 2018 ******/
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'[dbo].[up_ValidateVehiclePurchaseDetail]') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
DROP PROCEDURE [dbo].[up_ValidateVehiclePurchaseDetail]
GO



SET QUOTED_IDENTIFIER ON 
GO
SET ANSI_NULLS OFF 
GO
---------------------------------------------------------------------------------------------------------------
-- Date Created	: 05 Maret 2018
-- Created By	: DNet Team by using CodeSmith v 2.6
-- Rev History	:
---------------------------------------------------------------------------------------------------------------

CREATE PROCEDURE [dbo].up_InsertVehiclePurchaseDetail
	@ID int OUTPUT,
	@BUID int,
	@BUName varchar(100),
	@CloseLine bit,
	@CloseLineName varchar(50),
	@CloseReason varchar(100),
	@Completed bit,
	@CompletedName varchar(50),
	@ProductDescription varchar(100),
	@ProductID int,
	@ProductName varchar(100),
	@ProductVariantID int,
	@ProductVariantName varchar(100),
	@PODetail varchar(50),
	@PODetailID varchar(50),
	@PONO int,
	@POName varchar(100),
	@PRDetailID int,
	@PRDetailName varchar(100),
	@PurchaseUnitID int,
	@PurchaseUnitName varchar(100),
	@QtyOrder float,
	@QtyReceipt float,
	@QtyReturn float,
	@RecallProduct bit,
	@RecallProductName varchar(50),
	@SODetail int,
	@SODetailName varchar(100),
	@ScheduledShippingDate datetime,
	@ServicePartsAndMaterial varchar(100),
	@ShippingDate datetime,
	@Site int,
	@StockNumberID int,
	@StockNumberName varchar(100),
	@RowStatus smallint,
	@CreatedBy varchar(50),
	--@CreatedTime datetime,
	@LastUpdateBy varchar(50)	--@LastUpdateTime datetime	
AS
INSERT	INTO	[dbo].[VehiclePurchaseDetail]
VALUES
(
	@BUID,
	@BUName,
	@CloseLine,
	@CloseLineName,
	@CloseReason,
	@Completed,
	@CompletedName,
	@ProductDescription,
	@ProductID,
	@ProductName,
	@ProductVariantID,
	@ProductVariantName,
	@PODetail,
	@PODetailID,
	@PONO,
	@POName,
	@PRDetailID,
	@PRDetailName,
	@PurchaseUnitID,
	@PurchaseUnitName,
	@QtyOrder,
	@QtyReceipt,
	@QtyReturn,
	@RecallProduct,
	@RecallProductName,
	@SODetail,
	@SODetailName,
	@ScheduledShippingDate,
	@ServicePartsAndMaterial,
	@ShippingDate,
	@Site,
	@StockNumberID,
	@StockNumberName,
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
-- Date Created	: 05 Maret 2018
-- Created By	: DNet Team by using CodeSmith v 2.6
-- Rev History	:
---------------------------------------------------------------------------------------------------------------

CREATE PROCEDURE [dbo].up_RetrieveVehiclePurchaseDetail
	@ID int OUTPUT	
AS
SET TRANSACTION ISOLATION LEVEL READ COMMITTED

SET NOCOUNT ON

SELECT
	[ID],
	[BUID],
	[BUName],
	[CloseLine],
	[CloseLineName],
	[CloseReason],
	[Completed],
	[CompletedName],
	[ProductDescription],
	[ProductID],
	[ProductName],
	[ProductVariantID],
	[ProductVariantName],
	[PODetail],
	[PODetailID],
	[PONO],
	[POName],
	[PRDetailID],
	[PRDetailName],
	[PurchaseUnitID],
	[PurchaseUnitName],
	[QtyOrder],
	[QtyReceipt],
	[QtyReturn],
	[RecallProduct],
	[RecallProductName],
	[SODetail],
	[SODetailName],
	[ScheduledShippingDate],
	[ServicePartsAndMaterial],
	[ShippingDate],
	[Site],
	[StockNumberID],
	[StockNumberName],
	[RowStatus],
	[CreatedBy],
	[CreatedTime],
	[LastUpdateBy],
	[LastUpdateTime]	
FROM	[dbo].[VehiclePurchaseDetail]

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
-- Date Created	: 05 Maret 2018
-- Created By	: DNet Team by using CodeSmith v 2.6
-- Rev History	:
---------------------------------------------------------------------------------------------------------------

CREATE PROCEDURE [dbo].up_RetrieveVehiclePurchaseDetailList

AS

SET TRANSACTION ISOLATION LEVEL READ COMMITTED
SET NOCOUNT ON


SELECT
		[ID],
		[BUID],
		[BUName],
		[CloseLine],
		[CloseLineName],
		[CloseReason],
		[Completed],
		[CompletedName],
		[ProductDescription],
		[ProductID],
		[ProductName],
		[ProductVariantID],
		[ProductVariantName],
		[PODetail],
		[PODetailID],
		[PONO],
		[POName],
		[PRDetailID],
		[PRDetailName],
		[PurchaseUnitID],
		[PurchaseUnitName],
		[QtyOrder],
		[QtyReceipt],
		[QtyReturn],
		[RecallProduct],
		[RecallProductName],
		[SODetail],
		[SODetailName],
		[ScheduledShippingDate],
		[ServicePartsAndMaterial],
		[ShippingDate],
		[Site],
		[StockNumberID],
		[StockNumberName],
		[RowStatus],
		[CreatedBy],
		[CreatedTime],
		[LastUpdateBy],
		[LastUpdateTime]		
		FROM	
		[dbo].[VehiclePurchaseDetail] 

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
-- Date Created	: 05 Maret 2018
-- Created By	: DNet Team by using CodeSmith v 2.6
-- Rev History	:
---------------------------------------------------------------------------------------------------------------

CREATE PROCEDURE [dbo].up_UpdateVehiclePurchaseDetail
	@ID int OUTPUT,
	@BUID int,
	@BUName varchar(100),
	@CloseLine bit,
	@CloseLineName varchar(50),
	@CloseReason varchar(100),
	@Completed bit,
	@CompletedName varchar(50),
	@ProductDescription varchar(100),
	@ProductID int,
	@ProductName varchar(100),
	@ProductVariantID int,
	@ProductVariantName varchar(100),
	@PODetail varchar(50),
	@PODetailID varchar(50),
	@PONO int,
	@POName varchar(100),
	@PRDetailID int,
	@PRDetailName varchar(100),
	@PurchaseUnitID int,
	@PurchaseUnitName varchar(100),
	@QtyOrder float,
	@QtyReceipt float,
	@QtyReturn float,
	@RecallProduct bit,
	@RecallProductName varchar(50),
	@SODetail int,
	@SODetailName varchar(100),
	@ScheduledShippingDate datetime,
	@ServicePartsAndMaterial varchar(100),
	@ShippingDate datetime,
	@Site int,
	@StockNumberID int,
	@StockNumberName varchar(100),
	@RowStatus smallint,
	@CreatedBy varchar(50),
	--@CreatedTime datetime,
	@LastUpdateBy varchar(50)	--@LastUpdateTime datetime	
AS

UPDATE	[dbo].[VehiclePurchaseDetail]
SET
	[BUID] = @BUID,
	[BUName] = @BUName,
	[CloseLine] = @CloseLine,
	[CloseLineName] = @CloseLineName,
	[CloseReason] = @CloseReason,
	[Completed] = @Completed,
	[CompletedName] = @CompletedName,
	[ProductDescription] = @ProductDescription,
	[ProductID] = @ProductID,
	[ProductName] = @ProductName,
	[ProductVariantID] = @ProductVariantID,
	[ProductVariantName] = @ProductVariantName,
	[PODetail] = @PODetail,
	[PODetailID] = @PODetailID,
	[PONO] = @PONO,
	[POName] = @POName,
	[PRDetailID] = @PRDetailID,
	[PRDetailName] = @PRDetailName,
	[PurchaseUnitID] = @PurchaseUnitID,
	[PurchaseUnitName] = @PurchaseUnitName,
	[QtyOrder] = @QtyOrder,
	[QtyReceipt] = @QtyReceipt,
	[QtyReturn] = @QtyReturn,
	[RecallProduct] = @RecallProduct,
	[RecallProductName] = @RecallProductName,
	[SODetail] = @SODetail,
	[SODetailName] = @SODetailName,
	[ScheduledShippingDate] = @ScheduledShippingDate,
	[ServicePartsAndMaterial] = @ServicePartsAndMaterial,
	[ShippingDate] = @ShippingDate,
	[Site] = @Site,
	[StockNumberID] = @StockNumberID,
	[StockNumberName] = @StockNumberName,
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
-- Date Created	: 05 Maret 2018
-- Created By	: DNet Team by using CodeSmith v 2.6
-- Rev History	:
---------------------------------------------------------------------------------------------------------------

CREATE PROCEDURE [dbo].up_DeleteVehiclePurchaseDetail
	@ID int OUTPUT	
AS

DELETE
FROM	[dbo].[VehiclePurchaseDetail]
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
-- Date Created	: 05 Maret 2018
-- Created By	: DNet Team by using CodeSmith v 2.6
-- Rev History	:
---------------------------------------------------------------------------------------------------------------

CREATE PROCEDURE [dbo].up_ValidateVehiclePurchaseDetail
	@Result	varchar(1000),
	@ID int OUTPUT,
	@BUID int,
	@BUName varchar(100),
	@CloseLine bit,
	@CloseLineName varchar(50),
	@CloseReason varchar(100),
	@Completed bit,
	@CompletedName varchar(50),
	@ProductDescription varchar(100),
	@ProductID int,
	@ProductName varchar(100),
	@ProductVariantID int,
	@ProductVariantName varchar(100),
	@PODetail varchar(50),
	@PODetailID varchar(50),
	@PONO int,
	@POName varchar(100),
	@PRDetailID int,
	@PRDetailName varchar(100),
	@PurchaseUnitID int,
	@PurchaseUnitName varchar(100),
	@QtyOrder float,
	@QtyReceipt float,
	@QtyReturn float,
	@RecallProduct bit,
	@RecallProductName varchar(50),
	@SODetail int,
	@SODetailName varchar(100),
	@ScheduledShippingDate datetime,
	@ServicePartsAndMaterial varchar(100),
	@ShippingDate datetime,
	@Site int,
	@StockNumberID int,
	@StockNumberName varchar(100),
	@RowStatus smallint,
	@CreatedBy varchar(50),
	@CreatedTime datetime,
	@LastUpdateBy varchar(50),
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
-- Date Created	: 05 Maret 2018
-- Created By	: DNet Team by using CodeSmith v 2.6
-- Rev History	:
---------------------------------------------------------------------------------------------------------------
GRANT  EXECUTE  ON [dbo].up_InsertVehiclePurchaseDetail TO [bsi];

GRANT  EXECUTE  ON [dbo].up_UpdateVehiclePurchaseDetail TO [bsi];

GRANT  EXECUTE  ON [dbo].up_RetrieveVehiclePurchaseDetail TO [bsi];

GRANT  EXECUTE  ON [dbo].up_RetrieveVehiclePurchaseDetailList TO [bsi];

GRANT  EXECUTE  ON [dbo].up_ValidateVehiclePurchaseDetail TO [bsi];

GRANT  EXECUTE  ON [dbo].up_DeleteVehiclePurchaseDetail TO [bsi];

SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO



