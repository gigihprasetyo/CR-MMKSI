


SET QUOTED_IDENTIFIER ON 
GO
SET ANSI_NULLS OFF 
GO
---------------------------------------------------------------------------------------------------------------
-- Date Created	: 24 Maret 2018
-- Created By	: DNet Team by using CodeSmith v 2.6
-- Rev History	:
---------------------------------------------------------------------------------------------------------------

CREATE PROCEDURE [dbo].up_InsertAPPayment
	@ID int OUTPUT,
	@Owner varchar(100),
	@APPaymentNo varchar(50),
	@APReferenceNo varchar(100),
	@APVoucherReferenceNo varchar(100),
	@AppliedToDocument money,
	@BU varchar(100),
	@Cancelled bit,
	@CashAndBank varchar(100),
	@MethodOfPayment varchar(100),
	@AvailableBalance money,
	@State smallint,
	@TotalChangeAmount money,
	@TotalPaymentAmount money,
	@TransactionDate datetime,
	@Type smallint,
	@VendorDescription varchar(100),
	@Vendor varchar(100),
	@RowStatus smallint,
	@CreatedBy varchar(100),
	--@CreatedTime datetime,
	@LastUpdateBy varchar(100)	--@LastUpdateTime datetime	
AS
INSERT	INTO	[dbo].[APPayment]
VALUES
(
	@Owner,
	@APPaymentNo,
	@APReferenceNo,
	@APVoucherReferenceNo,
	@AppliedToDocument,
	@BU,
	@Cancelled,
	@CashAndBank,
	@MethodOfPayment,
	@AvailableBalance,
	@State,
	@TotalChangeAmount,
	@TotalPaymentAmount,
	@TransactionDate,
	@Type,
	@VendorDescription,
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

CREATE PROCEDURE [dbo].up_RetrieveAPPayment
	@ID int OUTPUT	
AS
SET TRANSACTION ISOLATION LEVEL READ COMMITTED

SET NOCOUNT ON

SELECT
	[ID],
	[Owner],
	[APPaymentNo],
	[APReferenceNo],
	[APVoucherReferenceNo],
	[AppliedToDocument],
	[BU],
	[Cancelled],
	[CashAndBank],
	[MethodOfPayment],
	[AvailableBalance],
	[State],
	[TotalChangeAmount],
	[TotalPaymentAmount],
	[TransactionDate],
	[Type],
	[VendorDescription],
	[Vendor],
	[RowStatus],
	[CreatedBy],
	[CreatedTime],
	[LastUpdateBy],
	[LastUpdateTime]	
FROM	[dbo].[APPayment]

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

CREATE PROCEDURE [dbo].up_RetrieveAPPaymentList

AS

SET TRANSACTION ISOLATION LEVEL READ COMMITTED
SET NOCOUNT ON


SELECT
		[ID],
		[Owner],
		[APPaymentNo],
		[APReferenceNo],
		[APVoucherReferenceNo],
		[AppliedToDocument],
		[BU],
		[Cancelled],
		[CashAndBank],
		[MethodOfPayment],
		[AvailableBalance],
		[State],
		[TotalChangeAmount],
		[TotalPaymentAmount],
		[TransactionDate],
		[Type],
		[VendorDescription],
		[Vendor],
		[RowStatus],
		[CreatedBy],
		[CreatedTime],
		[LastUpdateBy],
		[LastUpdateTime]		
		FROM	
		[dbo].[APPayment] 

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

CREATE PROCEDURE [dbo].up_UpdateAPPayment
	@ID int OUTPUT,
	@Owner varchar(100),
	@APPaymentNo varchar(50),
	@APReferenceNo varchar(100),
	@APVoucherReferenceNo varchar(100),
	@AppliedToDocument money,
	@BU varchar(100),
	@Cancelled bit,
	@CashAndBank varchar(100),
	@MethodOfPayment varchar(100),
	@AvailableBalance money,
	@State smallint,
	@TotalChangeAmount money,
	@TotalPaymentAmount money,
	@TransactionDate datetime,
	@Type smallint,
	@VendorDescription varchar(100),
	@Vendor varchar(100),
	@RowStatus smallint,
	@CreatedBy varchar(100),
	--@CreatedTime datetime,
	@LastUpdateBy varchar(100)	--@LastUpdateTime datetime	
AS

UPDATE	[dbo].[APPayment]
SET
	[Owner] = @Owner,
	[APPaymentNo] = @APPaymentNo,
	[APReferenceNo] = @APReferenceNo,
	[APVoucherReferenceNo] = @APVoucherReferenceNo,
	[AppliedToDocument] = @AppliedToDocument,
	[BU] = @BU,
	[Cancelled] = @Cancelled,
	[CashAndBank] = @CashAndBank,
	[MethodOfPayment] = @MethodOfPayment,
	[AvailableBalance] = @AvailableBalance,
	[State] = @State,
	[TotalChangeAmount] = @TotalChangeAmount,
	[TotalPaymentAmount] = @TotalPaymentAmount,
	[TransactionDate] = @TransactionDate,
	[Type] = @Type,
	[VendorDescription] = @VendorDescription,
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

CREATE PROCEDURE [dbo].up_DeleteAPPayment
	@ID int OUTPUT	
AS

DELETE
FROM	[dbo].[APPayment]
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

CREATE PROCEDURE [dbo].up_ValidateAPPayment
	@Result	varchar(1000),
	@ID int OUTPUT,
	@Owner varchar(100),
	@APPaymentNo varchar(50),
	@APReferenceNo varchar(100),
	@APVoucherReferenceNo varchar(100),
	@AppliedToDocument money,
	@BU varchar(100),
	@Cancelled bit,
	@CashAndBank varchar(100),
	@MethodOfPayment varchar(100),
	@AvailableBalance money,
	@State smallint,
	@TotalChangeAmount money,
	@TotalPaymentAmount money,
	@TransactionDate datetime,
	@Type smallint,
	@VendorDescription varchar(100),
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




