
/****** Object:  Stored Procedure [dbo].[up_UpdateVehiclePurchaseHeader]    Script Date: 05 Maret 2018 ******/
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'[dbo].[up_InsertVehiclePurchaseHeader]') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
DROP PROCEDURE [dbo].[up_InsertVehiclePurchaseHeader]
GO


/****** Object:  Stored Procedure [dbo].[up_RetrieveVehiclePurchaseHeader]    Script Date: 05 Maret 2018 ******/
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'[dbo].[up_RetrieveVehiclePurchaseHeader]') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
DROP PROCEDURE [dbo].[up_RetrieveVehiclePurchaseHeader]
GO


/****** Object:  Stored Procedure [dbo].[up_RetrieveVehiclePurchaseHeaderList]    Script Date: 05 Maret 2018 ******/
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'[dbo].[up_RetrieveVehiclePurchaseHeaderList]') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
DROP PROCEDURE [dbo].[up_RetrieveVehiclePurchaseHeaderList]
GO


/****** Object:  Stored Procedure [dbo].[up_UpdateVehiclePurchaseHeader]    Script Date: 05 Maret 2018 ******/
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'[dbo].[up_UpdateVehiclePurchaseHeader]') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
DROP PROCEDURE [dbo].[up_UpdateVehiclePurchaseHeader]
GO


/****** Object:  Stored Procedure [dbo].[up_DeleteVehiclePurchaseHeader]    Script Date: 05 Maret 2018 ******/
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'[dbo].[up_DeleteVehiclePurchaseHeader]') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
DROP PROCEDURE [dbo].[up_DeleteVehiclePurchaseHeader]
GO


/****** Object:  Stored Procedure [dbo].[up_ValidateVehiclePurchaseHeader]    Script Date: 05 Maret 2018 ******/
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'[dbo].[up_ValidateVehiclePurchaseHeader]') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
DROP PROCEDURE [dbo].[up_ValidateVehiclePurchaseHeader]
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

CREATE PROCEDURE [dbo].up_InsertVehiclePurchaseHeader
	@ID int OUTPUT,
	@BUID int,
	@BUName varchar(100),
	@DeliveryMethod varchar(10),
	@Description varchar(100),
	@PRPOTypeID int,
	@PRPOName varchar(100),
	@DMSPOID int,
	@DMSPONo varchar(50),
	@DMSPOStatus int,
	@DMSPODate datetime,
	@VendorDescription varchar(100),
	@Vendor varchar(100),
	@PurchaseOrderNo varchar(50),
	@PurchaseReceiptID int,
	@PurchaseReceiptNo varchar(50),
	@PurchaseReceiptDetailNo varchar(50),
	@PurchaseReceiptDetailID int,
	@ChassisModel varchar(50),
	@ChassisNumberRegister varchar(50),
	@RowStatus smallint,
	@CreatedBy varchar(50),
	--@CreatedTime datetime,
	@LastUpdateBy varchar(50)	--@LastUpdateTime datetime	
AS
INSERT	INTO	[dbo].[VehiclePurchaseHeader]
VALUES
(
	@BUID,
	@BUName,
	@DeliveryMethod,
	@Description,
	@PRPOTypeID,
	@PRPOName,
	@DMSPOID,
	@DMSPONo,
	@DMSPOStatus,
	@DMSPODate,
	@VendorDescription,
	@Vendor,
	@PurchaseOrderNo,
	@PurchaseReceiptID,
	@PurchaseReceiptNo,
	@PurchaseReceiptDetailNo,
	@PurchaseReceiptDetailID,
	@ChassisModel,
	@ChassisNumberRegister,
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

CREATE PROCEDURE [dbo].up_RetrieveVehiclePurchaseHeader
	@ID int OUTPUT	
AS
SET TRANSACTION ISOLATION LEVEL READ COMMITTED

SET NOCOUNT ON

SELECT
	[ID],
	[BUID],
	[BUName],
	[DeliveryMethod],
	[Description],
	[PRPOTypeID],
	[PRPOName],
	[DMSPOID],
	[DMSPONo],
	[DMSPOStatus],
	[DMSPODate],
	[VendorDescription],
	[Vendor],
	[PurchaseOrderNo],
	[PurchaseReceiptID],
	[PurchaseReceiptNo],
	[PurchaseReceiptDetailNo],
	[PurchaseReceiptDetailID],
	[ChassisModel],
	[ChassisNumberRegister],
	[RowStatus],
	[CreatedBy],
	[CreatedTime],
	[LastUpdateBy],
	[LastUpdateTime]	
FROM	[dbo].[VehiclePurchaseHeader]

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

CREATE PROCEDURE [dbo].up_RetrieveVehiclePurchaseHeaderList

AS

SET TRANSACTION ISOLATION LEVEL READ COMMITTED
SET NOCOUNT ON


SELECT
		[ID],
		[BUID],
		[BUName],
		[DeliveryMethod],
		[Description],
		[PRPOTypeID],
		[PRPOName],
		[DMSPOID],
		[DMSPONo],
		[DMSPOStatus],
		[DMSPODate],
		[VendorDescription],
		[Vendor],
		[PurchaseOrderNo],
		[PurchaseReceiptID],
		[PurchaseReceiptNo],
		[PurchaseReceiptDetailNo],
		[PurchaseReceiptDetailID],
		[ChassisModel],
		[ChassisNumberRegister],
		[RowStatus],
		[CreatedBy],
		[CreatedTime],
		[LastUpdateBy],
		[LastUpdateTime]		
		FROM	
		[dbo].[VehiclePurchaseHeader] 

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

CREATE PROCEDURE [dbo].up_UpdateVehiclePurchaseHeader
	@ID int OUTPUT,
	@BUID int,
	@BUName varchar(100),
	@DeliveryMethod varchar(10),
	@Description varchar(100),
	@PRPOTypeID int,
	@PRPOName varchar(100),
	@DMSPOID int,
	@DMSPONo varchar(50),
	@DMSPOStatus int,
	@DMSPODate datetime,
	@VendorDescription varchar(100),
	@Vendor varchar(100),
	@PurchaseOrderNo varchar(50),
	@PurchaseReceiptID int,
	@PurchaseReceiptNo varchar(50),
	@PurchaseReceiptDetailNo varchar(50),
	@PurchaseReceiptDetailID int,
	@ChassisModel varchar(50),
	@ChassisNumberRegister varchar(50),
	@RowStatus smallint,
	@CreatedBy varchar(50),
	--@CreatedTime datetime,
	@LastUpdateBy varchar(50)	--@LastUpdateTime datetime	
AS

UPDATE	[dbo].[VehiclePurchaseHeader]
SET
	[BUID] = @BUID,
	[BUName] = @BUName,
	[DeliveryMethod] = @DeliveryMethod,
	[Description] = @Description,
	[PRPOTypeID] = @PRPOTypeID,
	[PRPOName] = @PRPOName,
	[DMSPOID] = @DMSPOID,
	[DMSPONo] = @DMSPONo,
	[DMSPOStatus] = @DMSPOStatus,
	[DMSPODate] = @DMSPODate,
	[VendorDescription] = @VendorDescription,
	[Vendor] = @Vendor,
	[PurchaseOrderNo] = @PurchaseOrderNo,
	[PurchaseReceiptID] = @PurchaseReceiptID,
	[PurchaseReceiptNo] = @PurchaseReceiptNo,
	[PurchaseReceiptDetailNo] = @PurchaseReceiptDetailNo,
	[PurchaseReceiptDetailID] = @PurchaseReceiptDetailID,
	[ChassisModel] = @ChassisModel,
	[ChassisNumberRegister] = @ChassisNumberRegister,
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

CREATE PROCEDURE [dbo].up_DeleteVehiclePurchaseHeader
	@ID int OUTPUT	
AS

DELETE
FROM	[dbo].[VehiclePurchaseHeader]
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

CREATE PROCEDURE [dbo].up_ValidateVehiclePurchaseHeader
	@Result	varchar(1000),
	@ID int OUTPUT,
	@BUID int,
	@BUName varchar(100),
	@DeliveryMethod varchar(10),
	@Description varchar(100),
	@PRPOTypeID int,
	@PRPOName varchar(100),
	@DMSPOID int,
	@DMSPONo varchar(50),
	@DMSPOStatus int,
	@DMSPODate datetime,
	@VendorDescription varchar(100),
	@Vendor varchar(100),
	@PurchaseOrderNo varchar(50),
	@PurchaseReceiptID int,
	@PurchaseReceiptNo varchar(50),
	@PurchaseReceiptDetailNo varchar(50),
	@PurchaseReceiptDetailID int,
	@ChassisModel varchar(50),
	@ChassisNumberRegister varchar(50),
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
GRANT  EXECUTE  ON [dbo].up_InsertVehiclePurchaseHeader TO [bsi];

GRANT  EXECUTE  ON [dbo].up_UpdateVehiclePurchaseHeader TO [bsi];

GRANT  EXECUTE  ON [dbo].up_RetrieveVehiclePurchaseHeader TO [bsi];

GRANT  EXECUTE  ON [dbo].up_RetrieveVehiclePurchaseHeaderList TO [bsi];

GRANT  EXECUTE  ON [dbo].up_ValidateVehiclePurchaseHeader TO [bsi];

GRANT  EXECUTE  ON [dbo].up_DeleteVehiclePurchaseHeader TO [bsi];

SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO



