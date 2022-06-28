
/****** Object:  Stored Procedure [dbo].[up_UpdatePOOtherVendor]    Script Date: 22 Maret 2018 ******/
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'[dbo].[up_InsertPOOtherVendor]') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
DROP PROCEDURE [dbo].[up_InsertPOOtherVendor]
GO


/****** Object:  Stored Procedure [dbo].[up_RetrievePOOtherVendor]    Script Date: 22 Maret 2018 ******/
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'[dbo].[up_RetrievePOOtherVendor]') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
DROP PROCEDURE [dbo].[up_RetrievePOOtherVendor]
GO


/****** Object:  Stored Procedure [dbo].[up_RetrievePOOtherVendorList]    Script Date: 22 Maret 2018 ******/
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'[dbo].[up_RetrievePOOtherVendorList]') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
DROP PROCEDURE [dbo].[up_RetrievePOOtherVendorList]
GO


/****** Object:  Stored Procedure [dbo].[up_UpdatePOOtherVendor]    Script Date: 22 Maret 2018 ******/
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'[dbo].[up_UpdatePOOtherVendor]') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
DROP PROCEDURE [dbo].[up_UpdatePOOtherVendor]
GO


/****** Object:  Stored Procedure [dbo].[up_DeletePOOtherVendor]    Script Date: 22 Maret 2018 ******/
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'[dbo].[up_DeletePOOtherVendor]') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
DROP PROCEDURE [dbo].[up_DeletePOOtherVendor]
GO


/****** Object:  Stored Procedure [dbo].[up_ValidatePOOtherVendor]    Script Date: 22 Maret 2018 ******/
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'[dbo].[up_ValidatePOOtherVendor]') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
DROP PROCEDURE [dbo].[up_ValidatePOOtherVendor]
GO



SET QUOTED_IDENTIFIER ON 
GO
SET ANSI_NULLS OFF 
GO
---------------------------------------------------------------------------------------------------------------
-- Date Created	: 22 Maret 2018
-- Created By	: Mitrais Team by using CodeSmith v 2.6
-- Rev History	:
---------------------------------------------------------------------------------------------------------------

CREATE PROCEDURE [dbo].up_InsertPOOtherVendor
	@ID int OUTPUT,
	@Owner varchar(100),
	@Address1 varchar(100),
	@Address2 varchar(100),
	@Address3 varchar(100),
	@AllocationPeriod varchar(100),
	@Balance money,
	@DealerCode varchar(100),
	@City varchar(100),
	@CloseRespon varchar(100),
	@Country varchar(100),
	@DeliveryMethod smallint,
	@Description varchar(100),
	@DownPayment money,
	@DownPaymentAmountPaid money,
	@DownPaymentIsPaid bit,
	@EventDate varchar(100),
	@ExternalDocNo varchar(100),
	@FormSource smallint,
	@GrandTotal money,
	@PaymentGroup smallint,
	@PersonInCharge varchar(100),
	@PostalCode varchar(100),
	@Priority smallint,
	@Province varchar(100),
	@PRPOType varchar(100),
	@PurchaseOrderNo varchar(100),
	@SONo varchar(100),
	@Site varchar(100),
	@State smallint,
	@StockReferenceNo varchar(100),
	@Taxable smallint,
	@TermsOfPayment varchar(100),
	@TotalAmountBeforeDiscount money,
	@TotalBaseAmount money,
	@TotalConsumptionTaxAmount money,
	@TotalDiscountAmount money,
	@TotalTitleRegistrationFee money,
	@PurchaseOrderDate datetime,
	@VendorDescription varchar(100),
	@Vendor varchar(100),
	@Warehouse varchar(100),
	@WONo varchar(100),
	@RowStatus smallint,
	@CreatedBy varchar(20),
	--@CreatedTime datetime,
	@LastUpdateBy varchar(20)	--@LastUpdateTime datetime	
AS
INSERT	INTO	[dbo].[POOtherVendor]
VALUES
(
	@Owner,
	@Address1,
	@Address2,
	@Address3,
	@AllocationPeriod,
	@Balance,
	@DealerCode,
	@City,
	@CloseRespon,
	@Country,
	@DeliveryMethod,
	@Description,
	@DownPayment,
	@DownPaymentAmountPaid,
	@DownPaymentIsPaid,
	@EventDate,
	@ExternalDocNo,
	@FormSource,
	@GrandTotal,
	@PaymentGroup,
	@PersonInCharge,
	@PostalCode,
	@Priority,
	@Province,
	@PRPOType,
	@PurchaseOrderNo,
	@SONo,
	@Site,
	@State,
	@StockReferenceNo,
	@Taxable,
	@TermsOfPayment,
	@TotalAmountBeforeDiscount,
	@TotalBaseAmount,
	@TotalConsumptionTaxAmount,
	@TotalDiscountAmount,
	@TotalTitleRegistrationFee,
	@PurchaseOrderDate,
	@VendorDescription,
	@Vendor,
	@Warehouse,
	@WONo,
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
-- Created By	: Mitrais Team by using CodeSmith v 2.6
-- Rev History	:
---------------------------------------------------------------------------------------------------------------

CREATE PROCEDURE [dbo].up_RetrievePOOtherVendor
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
	[AllocationPeriod],
	[Balance],
	[DealerCode],
	[City],
	[CloseRespon],
	[Country],
	[DeliveryMethod],
	[Description],
	[DownPayment],
	[DownPaymentAmountPaid],
	[DownPaymentIsPaid],
	[EventDate],
	[ExternalDocNo],
	[FormSource],
	[GrandTotal],
	[PaymentGroup],
	[PersonInCharge],
	[PostalCode],
	[Priority],
	[Province],
	[PRPOType],
	[PurchaseOrderNo],
	[SONo],
	[Site],
	[State],
	[StockReferenceNo],
	[Taxable],
	[TermsOfPayment],
	[TotalAmountBeforeDiscount],
	[TotalBaseAmount],
	[TotalConsumptionTaxAmount],
	[TotalDiscountAmount],
	[TotalTitleRegistrationFee],
	[PurchaseOrderDate],
	[VendorDescription],
	[Vendor],
	[Warehouse],
	[WONo],
	[RowStatus],
	[CreatedBy],
	[CreatedTime],
	[LastUpdateBy],
	[LastUpdateTime]	
FROM	[dbo].[POOtherVendor]

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
-- Created By	: Mitrais Team by using CodeSmith v 2.6
-- Rev History	:
---------------------------------------------------------------------------------------------------------------

CREATE PROCEDURE [dbo].up_RetrievePOOtherVendorList

AS

SET TRANSACTION ISOLATION LEVEL READ COMMITTED
SET NOCOUNT ON


SELECT
		[ID],
		[Owner],
		[Address1],
		[Address2],
		[Address3],
		[AllocationPeriod],
		[Balance],
		[DealerCode],
		[City],
		[CloseRespon],
		[Country],
		[DeliveryMethod],
		[Description],
		[DownPayment],
		[DownPaymentAmountPaid],
		[DownPaymentIsPaid],
		[EventDate],
		[ExternalDocNo],
		[FormSource],
		[GrandTotal],
		[PaymentGroup],
		[PersonInCharge],
		[PostalCode],
		[Priority],
		[Province],
		[PRPOType],
		[PurchaseOrderNo],
		[SONo],
		[Site],
		[State],
		[StockReferenceNo],
		[Taxable],
		[TermsOfPayment],
		[TotalAmountBeforeDiscount],
		[TotalBaseAmount],
		[TotalConsumptionTaxAmount],
		[TotalDiscountAmount],
		[TotalTitleRegistrationFee],
		[PurchaseOrderDate],
		[VendorDescription],
		[Vendor],
		[Warehouse],
		[WONo],
		[RowStatus],
		[CreatedBy],
		[CreatedTime],
		[LastUpdateBy],
		[LastUpdateTime]		
		FROM	
		[dbo].[POOtherVendor] 

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
-- Created By	: Mitrais Team by using CodeSmith v 2.6
-- Rev History	:
---------------------------------------------------------------------------------------------------------------

CREATE PROCEDURE [dbo].up_UpdatePOOtherVendor
	@ID int OUTPUT,
	@Owner varchar(100),
	@Address1 varchar(100),
	@Address2 varchar(100),
	@Address3 varchar(100),
	@AllocationPeriod varchar(100),
	@Balance money,
	@DealerCode varchar(100),
	@City varchar(100),
	@CloseRespon varchar(100),
	@Country varchar(100),
	@DeliveryMethod smallint,
	@Description varchar(100),
	@DownPayment money,
	@DownPaymentAmountPaid money,
	@DownPaymentIsPaid bit,
	@EventDate varchar(100),
	@ExternalDocNo varchar(100),
	@FormSource smallint,
	@GrandTotal money,
	@PaymentGroup smallint,
	@PersonInCharge varchar(100),
	@PostalCode varchar(100),
	@Priority smallint,
	@Province varchar(100),
	@PRPOType varchar(100),
	@PurchaseOrderNo varchar(100),
	@SONo varchar(100),
	@Site varchar(100),
	@State smallint,
	@StockReferenceNo varchar(100),
	@Taxable smallint,
	@TermsOfPayment varchar(100),
	@TotalAmountBeforeDiscount money,
	@TotalBaseAmount money,
	@TotalConsumptionTaxAmount money,
	@TotalDiscountAmount money,
	@TotalTitleRegistrationFee money,
	@PurchaseOrderDate datetime,
	@VendorDescription varchar(100),
	@Vendor varchar(100),
	@Warehouse varchar(100),
	@WONo varchar(100),
	@RowStatus smallint,
	@CreatedBy varchar(20),
	--@CreatedTime datetime,
	@LastUpdateBy varchar(20)	--@LastUpdateTime datetime	
AS

UPDATE	[dbo].[POOtherVendor]
SET
	[Owner] = @Owner,
	[Address1] = @Address1,
	[Address2] = @Address2,
	[Address3] = @Address3,
	[AllocationPeriod] = @AllocationPeriod,
	[Balance] = @Balance,
	[DealerCode] = @DealerCode,
	[City] = @City,
	[CloseRespon] = @CloseRespon,
	[Country] = @Country,
	[DeliveryMethod] = @DeliveryMethod,
	[Description] = @Description,
	[DownPayment] = @DownPayment,
	[DownPaymentAmountPaid] = @DownPaymentAmountPaid,
	[DownPaymentIsPaid] = @DownPaymentIsPaid,
	[EventDate] = @EventDate,
	[ExternalDocNo] = @ExternalDocNo,
	[FormSource] = @FormSource,
	[GrandTotal] = @GrandTotal,
	[PaymentGroup] = @PaymentGroup,
	[PersonInCharge] = @PersonInCharge,
	[PostalCode] = @PostalCode,
	[Priority] = @Priority,
	[Province] = @Province,
	[PRPOType] = @PRPOType,
	[PurchaseOrderNo] = @PurchaseOrderNo,
	[SONo] = @SONo,
	[Site] = @Site,
	[State] = @State,
	[StockReferenceNo] = @StockReferenceNo,
	[Taxable] = @Taxable,
	[TermsOfPayment] = @TermsOfPayment,
	[TotalAmountBeforeDiscount] = @TotalAmountBeforeDiscount,
	[TotalBaseAmount] = @TotalBaseAmount,
	[TotalConsumptionTaxAmount] = @TotalConsumptionTaxAmount,
	[TotalDiscountAmount] = @TotalDiscountAmount,
	[TotalTitleRegistrationFee] = @TotalTitleRegistrationFee,
	[PurchaseOrderDate] = @PurchaseOrderDate,
	[VendorDescription] = @VendorDescription,
	[Vendor] = @Vendor,
	[Warehouse] = @Warehouse,
	[WONo] = @WONo,
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
-- Created By	: Mitrais Team by using CodeSmith v 2.6
-- Rev History	:
---------------------------------------------------------------------------------------------------------------

CREATE PROCEDURE [dbo].up_DeletePOOtherVendor
	@ID int OUTPUT	
AS

DELETE
FROM	[dbo].[POOtherVendor]
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
-- Created By	: Mitrais Team by using CodeSmith v 2.6
-- Rev History	:
---------------------------------------------------------------------------------------------------------------

CREATE PROCEDURE [dbo].up_ValidatePOOtherVendor
	@Result	varchar(1000),
	@ID int OUTPUT,
	@Owner varchar(100),
	@Address1 varchar(100),
	@Address2 varchar(100),
	@Address3 varchar(100),
	@AllocationPeriod varchar(100),
	@Balance money,
	@DealerCode varchar(100),
	@City varchar(100),
	@CloseRespon varchar(100),
	@Country varchar(100),
	@DeliveryMethod smallint,
	@Description varchar(100),
	@DownPayment money,
	@DownPaymentAmountPaid money,
	@DownPaymentIsPaid bit,
	@EventDate varchar(100),
	@ExternalDocNo varchar(100),
	@FormSource smallint,
	@GrandTotal money,
	@PaymentGroup smallint,
	@PersonInCharge varchar(100),
	@PostalCode varchar(100),
	@Priority smallint,
	@Province varchar(100),
	@PRPOType varchar(100),
	@PurchaseOrderNo varchar(100),
	@SONo varchar(100),
	@Site varchar(100),
	@State smallint,
	@StockReferenceNo varchar(100),
	@Taxable smallint,
	@TermsOfPayment varchar(100),
	@TotalAmountBeforeDiscount money,
	@TotalBaseAmount money,
	@TotalConsumptionTaxAmount money,
	@TotalDiscountAmount money,
	@TotalTitleRegistrationFee money,
	@PurchaseOrderDate datetime,
	@VendorDescription varchar(100),
	@Vendor varchar(100),
	@Warehouse varchar(100),
	@WONo varchar(100),
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
-- Date Created	: 22 Maret 2018
-- Created By	: Mitrais Team by using CodeSmith v 2.6
-- Rev History	:
---------------------------------------------------------------------------------------------------------------
GRANT  EXECUTE  ON [dbo].up_InsertPOOtherVendor TO [bsi];

GRANT  EXECUTE  ON [dbo].up_UpdatePOOtherVendor TO [bsi];

GRANT  EXECUTE  ON [dbo].up_RetrievePOOtherVendor TO [bsi];

GRANT  EXECUTE  ON [dbo].up_RetrievePOOtherVendorList TO [bsi];

GRANT  EXECUTE  ON [dbo].up_ValidatePOOtherVendor TO [bsi];

GRANT  EXECUTE  ON [dbo].up_DeletePOOtherVendor TO [bsi];

SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO



