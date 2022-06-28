


SET QUOTED_IDENTIFIER ON 
GO
SET ANSI_NULLS OFF 
GO
---------------------------------------------------------------------------------------------------------------
-- Date Created	: 24 Maret 2018
-- Created By	: DNet Team by using CodeSmith v 2.6
-- Rev History	:
---------------------------------------------------------------------------------------------------------------

CREATE PROCEDURE [dbo].up_InsertAPPaymentDetail
	@ID int OUTPUT,
	@APPaymentID int,
	@Owner varchar(100),
	@APPaymentDetailNo varchar(100),
	@APPaymentNo varchar(100),
	@BU varchar(100),
	@ChangeAmount money,
	@Description varchar(100),
	@DifferenceValue float,
	@ExternalDocumentNo varchar(50),
	@ExternalDocumentType smallint,
	@APVoucherNo varchar(100),
	@OrderDate datetime,
	@OrderNoNVSOReferral varchar(100),
	@OrderNoOutsourceWorkOrder varchar(100),
	@OrderNo varchar(100),
	@OrderNoUVSOReferral varchar(100),
	@OutstandingBalance money,
	@PaymentAmount money,
	@PaymentSlipNo varchar(50),
	@ReceiptFromVendor bit,
	@RemainingBalance money,
	@SourceType smallint,
	@TransactionDocument varchar(100),
	@Vendor varchar(100),
	@RowStatus smallint,
	@CreatedBy varchar(100),
	--@CreatedTime datetime,
	@LastUpdateBy varchar(100)	--@LastUpdateTime datetime	
AS
INSERT	INTO	[dbo].[APPaymentDetail]
VALUES
(
	@APPaymentID,
	@Owner,
	@APPaymentDetailNo,
	@APPaymentNo,
	@BU,
	@ChangeAmount,
	@Description,
	@DifferenceValue,
	@ExternalDocumentNo,
	@ExternalDocumentType,
	@APVoucherNo,
	@OrderDate,
	@OrderNoNVSOReferral,
	@OrderNoOutsourceWorkOrder,
	@OrderNo,
	@OrderNoUVSOReferral,
	@OutstandingBalance,
	@PaymentAmount,
	@PaymentSlipNo,
	@ReceiptFromVendor,
	@RemainingBalance,
	@SourceType,
	@TransactionDocument,
	@Vendor,
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
-- Created By	: DNet Team by using CodeSmith v 2.6
-- Rev History	:
---------------------------------------------------------------------------------------------------------------

CREATE PROCEDURE [dbo].up_RetrieveAPPaymentDetail
	@ID int OUTPUT	
AS
SET TRANSACTION ISOLATION LEVEL READ COMMITTED

SET NOCOUNT ON

SELECT
	[ID],
	[APPaymentID],
	[Owner],
	[APPaymentDetailNo],
	[APPaymentNo],
	[BU],
	[ChangeAmount],
	[Description],
	[DifferenceValue],
	[ExternalDocumentNo],
	[ExternalDocumentType],
	[APVoucherNo],
	[OrderDate],
	[OrderNoNVSOReferral],
	[OrderNoOutsourceWorkOrder],
	[OrderNo],
	[OrderNoUVSOReferral],
	[OutstandingBalance],
	[PaymentAmount],
	[PaymentSlipNo],
	[ReceiptFromVendor],
	[RemainingBalance],
	[SourceType],
	[TransactionDocument],
	[Vendor],
	[RowStatus],
	[CreatedBy],
	[CreatedTime],
	[LastUpdateBy],
	[LastUpdateTime]	
FROM	[dbo].[APPaymentDetail]

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
-- Created By	: DNet Team by using CodeSmith v 2.6
-- Rev History	:
---------------------------------------------------------------------------------------------------------------

CREATE PROCEDURE [dbo].up_RetrieveAPPaymentDetailList

AS

SET TRANSACTION ISOLATION LEVEL READ COMMITTED
SET NOCOUNT ON


SELECT
		[ID],
		[APPaymentID],
		[Owner],
		[APPaymentDetailNo],
		[APPaymentNo],
		[BU],
		[ChangeAmount],
		[Description],
		[DifferenceValue],
		[ExternalDocumentNo],
		[ExternalDocumentType],
		[APVoucherNo],
		[OrderDate],
		[OrderNoNVSOReferral],
		[OrderNoOutsourceWorkOrder],
		[OrderNo],
		[OrderNoUVSOReferral],
		[OutstandingBalance],
		[PaymentAmount],
		[PaymentSlipNo],
		[ReceiptFromVendor],
		[RemainingBalance],
		[SourceType],
		[TransactionDocument],
		[Vendor],
		[RowStatus],
		[CreatedBy],
		[CreatedTime],
		[LastUpdateBy],
		[LastUpdateTime]		
		FROM	
		[dbo].[APPaymentDetail] 

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
-- Created By	: DNet Team by using CodeSmith v 2.6
-- Rev History	:
---------------------------------------------------------------------------------------------------------------

CREATE PROCEDURE [dbo].up_UpdateAPPaymentDetail
	@ID int OUTPUT,
	@APPaymentID int,
	@Owner varchar(100),
	@APPaymentDetailNo varchar(100),
	@APPaymentNo varchar(100),
	@BU varchar(100),
	@ChangeAmount money,
	@Description varchar(100),
	@DifferenceValue float,
	@ExternalDocumentNo varchar(50),
	@ExternalDocumentType smallint,
	@APVoucherNo varchar(100),
	@OrderDate datetime,
	@OrderNoNVSOReferral varchar(100),
	@OrderNoOutsourceWorkOrder varchar(100),
	@OrderNo varchar(100),
	@OrderNoUVSOReferral varchar(100),
	@OutstandingBalance money,
	@PaymentAmount money,
	@PaymentSlipNo varchar(50),
	@ReceiptFromVendor bit,
	@RemainingBalance money,
	@SourceType smallint,
	@TransactionDocument varchar(100),
	@Vendor varchar(100),
	@RowStatus smallint,
	@CreatedBy varchar(100),
	--@CreatedTime datetime,
	@LastUpdateBy varchar(100)	--@LastUpdateTime datetime	
AS

UPDATE	[dbo].[APPaymentDetail]
SET
	[APPaymentID] = @APPaymentID,
	[Owner] = @Owner,
	[APPaymentDetailNo] = @APPaymentDetailNo,
	[APPaymentNo] = @APPaymentNo,
	[BU] = @BU,
	[ChangeAmount] = @ChangeAmount,
	[Description] = @Description,
	[DifferenceValue] = @DifferenceValue,
	[ExternalDocumentNo] = @ExternalDocumentNo,
	[ExternalDocumentType] = @ExternalDocumentType,
	[APVoucherNo] = @APVoucherNo,
	[OrderDate] = @OrderDate,
	[OrderNoNVSOReferral] = @OrderNoNVSOReferral,
	[OrderNoOutsourceWorkOrder] = @OrderNoOutsourceWorkOrder,
	[OrderNo] = @OrderNo,
	[OrderNoUVSOReferral] = @OrderNoUVSOReferral,
	[OutstandingBalance] = @OutstandingBalance,
	[PaymentAmount] = @PaymentAmount,
	[PaymentSlipNo] = @PaymentSlipNo,
	[ReceiptFromVendor] = @ReceiptFromVendor,
	[RemainingBalance] = @RemainingBalance,
	[SourceType] = @SourceType,
	[TransactionDocument] = @TransactionDocument,
	[Vendor] = @Vendor,
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
-- Created By	: DNet Team by using CodeSmith v 2.6
-- Rev History	:
---------------------------------------------------------------------------------------------------------------

CREATE PROCEDURE [dbo].up_DeleteAPPaymentDetail
	@ID int OUTPUT	
AS

DELETE
FROM	[dbo].[APPaymentDetail]
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
-- Created By	: DNet Team by using CodeSmith v 2.6
-- Rev History	:
---------------------------------------------------------------------------------------------------------------

CREATE PROCEDURE [dbo].up_ValidateAPPaymentDetail
	@Result	varchar(1000),
	@ID int OUTPUT,
	@APPaymentID int,
	@Owner varchar(100),
	@APPaymentDetailNo varchar(100),
	@APPaymentNo varchar(100),
	@BU varchar(100),
	@ChangeAmount money,
	@Description varchar(100),
	@DifferenceValue float,
	@ExternalDocumentNo varchar(50),
	@ExternalDocumentType smallint,
	@APVoucherNo varchar(100),
	@OrderDate datetime,
	@OrderNoNVSOReferral varchar(100),
	@OrderNoOutsourceWorkOrder varchar(100),
	@OrderNo varchar(100),
	@OrderNoUVSOReferral varchar(100),
	@OutstandingBalance money,
	@PaymentAmount money,
	@PaymentSlipNo varchar(50),
	@ReceiptFromVendor bit,
	@RemainingBalance money,
	@SourceType smallint,
	@TransactionDocument varchar(100),
	@Vendor varchar(100),
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




