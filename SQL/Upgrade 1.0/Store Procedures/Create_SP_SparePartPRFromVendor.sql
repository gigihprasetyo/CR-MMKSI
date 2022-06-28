USE [BSIDNET_MMKSI_DMS]
GO

/****** Object:  Stored Procedure [dbo].[up_UpdateSparePartPRFromVendor]    Script Date: 21 Maret 2018 ******/
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'[dbo].[up_InsertSparePartPRFromVendor]') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
DROP PROCEDURE [dbo].[up_InsertSparePartPRFromVendor]
GO


/****** Object:  Stored Procedure [dbo].[up_RetrieveSparePartPRFromVendor]    Script Date: 21 Maret 2018 ******/
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'[dbo].[up_RetrieveSparePartPRFromVendor]') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
DROP PROCEDURE [dbo].[up_RetrieveSparePartPRFromVendor]
GO


/****** Object:  Stored Procedure [dbo].[up_RetrieveSparePartPRFromVendorList]    Script Date: 21 Maret 2018 ******/
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'[dbo].[up_RetrieveSparePartPRFromVendorList]') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
DROP PROCEDURE [dbo].[up_RetrieveSparePartPRFromVendorList]
GO


/****** Object:  Stored Procedure [dbo].[up_UpdateSparePartPRFromVendor]    Script Date: 21 Maret 2018 ******/
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'[dbo].[up_UpdateSparePartPRFromVendor]') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
DROP PROCEDURE [dbo].[up_UpdateSparePartPRFromVendor]
GO


/****** Object:  Stored Procedure [dbo].[up_DeleteSparePartPRFromVendor]    Script Date: 21 Maret 2018 ******/
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'[dbo].[up_DeleteSparePartPRFromVendor]') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
DROP PROCEDURE [dbo].[up_DeleteSparePartPRFromVendor]
GO


/****** Object:  Stored Procedure [dbo].[up_ValidateSparePartPRFromVendor]    Script Date: 21 Maret 2018 ******/
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'[dbo].[up_ValidateSparePartPRFromVendor]') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
DROP PROCEDURE [dbo].[up_ValidateSparePartPRFromVendor]
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

CREATE PROCEDURE [dbo].up_InsertSparePartPRFromVendor
	@ID int OUTPUT,
	@PRNumber nvarchar(50),
	@PONumber varchar(100),
	@Owner varchar(100),
	@APVoucherNumber varchar(100),
	@AssignLandedCost bit,
	@AutoInvoiced bit,
	@DealerCode varchar(100),
	@DeliveryOrderDate datetime,
	@DeliveryOrderNumber varchar(50),
	@EventData varchar(4000),
	@EventData2 text,
	@GrandTotal money,
	@Handling varchar(100),
	@LoadData bit,
	@PackingSlipDate datetime,
	@PackingSlipNumber varchar(50),
	@PRReferenceRequired bit,
	@ReturnPRNumber varchar(100),
	@State varchar(100),
	@TotalBaseAmount money,
	@TotalConsumptionTax1Amount money,
	@TotalConsumptionTax2Amount money,
	@TotalConsumptionTaxAmount money,
	@TotalTitleRegistrationFree money,
	@TransactionDate datetime,
	@TransferOrderRequestingNumber varchar(100),
	@Type varchar(100),
	@VendorDescription varchar(100),
	@Vendor varchar(100),
	@VendorInvoiceNumber varchar(50),
	@WONumber varchar(100),
	@RowStatus smallint,
	@CreatedBy varchar(100),
	--@CreatedTime datetime,
	@LastUpdateBy varchar(100)	--@LastUpdateTime datetime	
AS
INSERT	INTO	[dbo].[SparePartPRFromVendor]
VALUES
(
	@PRNumber,
	@PONumber,
	@Owner,
	@APVoucherNumber,
	@AssignLandedCost,
	@AutoInvoiced,
	@DealerCode,
	@DeliveryOrderDate,
	@DeliveryOrderNumber,
	@EventData,
	@EventData2,
	@GrandTotal,
	@Handling,
	@LoadData,
	@PackingSlipDate,
	@PackingSlipNumber,
	@PRReferenceRequired,
	@ReturnPRNumber,
	@State,
	@TotalBaseAmount,
	@TotalConsumptionTax1Amount,
	@TotalConsumptionTax2Amount,
	@TotalConsumptionTaxAmount,
	@TotalTitleRegistrationFree,
	@TransactionDate,
	@TransferOrderRequestingNumber,
	@Type,
	@VendorDescription,
	@Vendor,
	@VendorInvoiceNumber,
	@WONumber,
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

CREATE PROCEDURE [dbo].up_RetrieveSparePartPRFromVendor
	@ID int OUTPUT	
AS
SET TRANSACTION ISOLATION LEVEL READ COMMITTED

SET NOCOUNT ON

SELECT
	[ID],
	[PRNumber],
	[PONumber],
	[Owner],
	[APVoucherNumber],
	[AssignLandedCost],
	[AutoInvoiced],
	[DealerCode],
	[DeliveryOrderDate],
	[DeliveryOrderNumber],
	[EventData],
	[EventData2],
	[GrandTotal],
	[Handling],
	[LoadData],
	[PackingSlipDate],
	[PackingSlipNumber],
	[PRReferenceRequired],
	[ReturnPRNumber],
	[State],
	[TotalBaseAmount],
	[TotalConsumptionTax1Amount],
	[TotalConsumptionTax2Amount],
	[TotalConsumptionTaxAmount],
	[TotalTitleRegistrationFree],
	[TransactionDate],
	[TransferOrderRequestingNumber],
	[Type],
	[VendorDescription],
	[Vendor],
	[VendorInvoiceNumber],
	[WONumber],
	[RowStatus],
	[CreatedBy],
	[CreatedTime],
	[LastUpdateBy],
	[LastUpdateTime]	
FROM	[dbo].[SparePartPRFromVendor]

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

CREATE PROCEDURE [dbo].up_RetrieveSparePartPRFromVendorList

AS

SET TRANSACTION ISOLATION LEVEL READ COMMITTED
SET NOCOUNT ON


SELECT
		[ID],
		[PRNumber],
		[PONumber],
		[Owner],
		[APVoucherNumber],
		[AssignLandedCost],
		[AutoInvoiced],
		[DealerCode],
		[DeliveryOrderDate],
		[DeliveryOrderNumber],
		[EventData],
		[EventData2],
		[GrandTotal],
		[Handling],
		[LoadData],
		[PackingSlipDate],
		[PackingSlipNumber],
		[PRReferenceRequired],
		[ReturnPRNumber],
		[State],
		[TotalBaseAmount],
		[TotalConsumptionTax1Amount],
		[TotalConsumptionTax2Amount],
		[TotalConsumptionTaxAmount],
		[TotalTitleRegistrationFree],
		[TransactionDate],
		[TransferOrderRequestingNumber],
		[Type],
		[VendorDescription],
		[Vendor],
		[VendorInvoiceNumber],
		[WONumber],
		[RowStatus],
		[CreatedBy],
		[CreatedTime],
		[LastUpdateBy],
		[LastUpdateTime]		
		FROM	
		[dbo].[SparePartPRFromVendor] 

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

CREATE PROCEDURE [dbo].up_UpdateSparePartPRFromVendor
	@ID int OUTPUT,
	@PRNumber nvarchar(50),
	@PONumber varchar(100),
	@Owner varchar(100),
	@APVoucherNumber varchar(100),
	@AssignLandedCost bit,
	@AutoInvoiced bit,
	@DealerCode varchar(100),
	@DeliveryOrderDate datetime,
	@DeliveryOrderNumber varchar(50),
	@EventData varchar(4000),
	@EventData2 text,
	@GrandTotal money,
	@Handling varchar(100),
	@LoadData bit,
	@PackingSlipDate datetime,
	@PackingSlipNumber varchar(50),
	@PRReferenceRequired bit,
	@ReturnPRNumber varchar(100),
	@State varchar(100),
	@TotalBaseAmount money,
	@TotalConsumptionTax1Amount money,
	@TotalConsumptionTax2Amount money,
	@TotalConsumptionTaxAmount money,
	@TotalTitleRegistrationFree money,
	@TransactionDate datetime,
	@TransferOrderRequestingNumber varchar(100),
	@Type varchar(100),
	@VendorDescription varchar(100),
	@Vendor varchar(100),
	@VendorInvoiceNumber varchar(50),
	@WONumber varchar(100),
	@RowStatus smallint,
	@CreatedBy varchar(100),
	--@CreatedTime datetime,
	@LastUpdateBy varchar(100)	--@LastUpdateTime datetime	
AS

UPDATE	[dbo].[SparePartPRFromVendor]
SET
	[PRNumber] = @PRNumber,
	[PONumber] = @PONumber,
	[Owner] = @Owner,
	[APVoucherNumber] = @APVoucherNumber,
	[AssignLandedCost] = @AssignLandedCost,
	[AutoInvoiced] = @AutoInvoiced,
	[DealerCode] = @DealerCode,
	[DeliveryOrderDate] = @DeliveryOrderDate,
	[DeliveryOrderNumber] = @DeliveryOrderNumber,
	[EventData] = @EventData,
	[EventData2] = @EventData2,
	[GrandTotal] = @GrandTotal,
	[Handling] = @Handling,
	[LoadData] = @LoadData,
	[PackingSlipDate] = @PackingSlipDate,
	[PackingSlipNumber] = @PackingSlipNumber,
	[PRReferenceRequired] = @PRReferenceRequired,
	[ReturnPRNumber] = @ReturnPRNumber,
	[State] = @State,
	[TotalBaseAmount] = @TotalBaseAmount,
	[TotalConsumptionTax1Amount] = @TotalConsumptionTax1Amount,
	[TotalConsumptionTax2Amount] = @TotalConsumptionTax2Amount,
	[TotalConsumptionTaxAmount] = @TotalConsumptionTaxAmount,
	[TotalTitleRegistrationFree] = @TotalTitleRegistrationFree,
	[TransactionDate] = @TransactionDate,
	[TransferOrderRequestingNumber] = @TransferOrderRequestingNumber,
	[Type] = @Type,
	[VendorDescription] = @VendorDescription,
	[Vendor] = @Vendor,
	[VendorInvoiceNumber] = @VendorInvoiceNumber,
	[WONumber] = @WONumber,
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

CREATE PROCEDURE [dbo].up_DeleteSparePartPRFromVendor
	@ID int OUTPUT	
AS

DELETE
FROM	[dbo].[SparePartPRFromVendor]
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

CREATE PROCEDURE [dbo].up_ValidateSparePartPRFromVendor
	@Result	varchar(1000),
	@ID int OUTPUT,
	@PRNumber nvarchar(50),
	@PONumber varchar(100),
	@Owner varchar(100),
	@APVoucherNumber varchar(100),
	@AssignLandedCost bit,
	@AutoInvoiced bit,
	@DealerCode varchar(100),
	@DeliveryOrderDate datetime,
	@DeliveryOrderNumber varchar(50),
	@EventData varchar(4000),
	@EventData2 text,
	@GrandTotal money,
	@Handling varchar(100),
	@LoadData bit,
	@PackingSlipDate datetime,
	@PackingSlipNumber varchar(50),
	@PRReferenceRequired bit,
	@ReturnPRNumber varchar(100),
	@State varchar(100),
	@TotalBaseAmount money,
	@TotalConsumptionTax1Amount money,
	@TotalConsumptionTax2Amount money,
	@TotalConsumptionTaxAmount money,
	@TotalTitleRegistrationFree money,
	@TransactionDate datetime,
	@TransferOrderRequestingNumber varchar(100),
	@Type varchar(100),
	@VendorDescription varchar(100),
	@Vendor varchar(100),
	@VendorInvoiceNumber varchar(50),
	@WONumber varchar(100),
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
GRANT  EXECUTE  ON [dbo].up_InsertSparePartPRFromVendor TO [bsi];

GRANT  EXECUTE  ON [dbo].up_UpdateSparePartPRFromVendor TO [bsi];

GRANT  EXECUTE  ON [dbo].up_RetrieveSparePartPRFromVendor TO [bsi];

GRANT  EXECUTE  ON [dbo].up_RetrieveSparePartPRFromVendorList TO [bsi];

GRANT  EXECUTE  ON [dbo].up_ValidateSparePartPRFromVendor TO [bsi];

GRANT  EXECUTE  ON [dbo].up_DeleteSparePartPRFromVendor TO [bsi];

SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO