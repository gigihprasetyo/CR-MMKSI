


SET QUOTED_IDENTIFIER ON 
GO
SET ANSI_NULLS OFF 
GO
---------------------------------------------------------------------------------------------------------------
-- Date Created	: 25 Maret 2018
-- Created By	: Mitrais Team by using CodeSmith v 2.6
-- Rev History	:
---------------------------------------------------------------------------------------------------------------

CREATE PROCEDURE [dbo].up_InsertInventoryTransfer
	@ID int OUTPUT,
	@Owner varchar(100),
	@FromDealer varchar(100),
	@FromSite varchar(100),
	@InventoryTransferNo varchar(50),
	@ItemTypeForTransfer smallint,
	@PersonInCharge varchar(100),
	@ReceiptDate datetime,
	@ReceiptNo varchar(100),
	@ReferenceNo varchar(100),
	@SearchVehicle varchar(50),
	@SourceData varchar(50),
	@State smallint,
	@ToDealer varchar(100),
	@ToSite varchar(100),
	@TransactionDate datetime,
	@TransactionType smallint,
	@TransferStatus smallint,
	@TransferStep bit,
	@WONo varchar(100),
	@RowStatus smallint,
	@CreatedBy varchar(100),
	--@CreatedTime datetime,
	@LastUpdateBy varchar(100)	--@LastUpdateTime datetime	
AS
INSERT	INTO	[dbo].[InventoryTransfer]
VALUES
(
	@Owner,
	@FromDealer,
	@FromSite,
	@InventoryTransferNo,
	@ItemTypeForTransfer,
	@PersonInCharge,
	@ReceiptDate,
	@ReceiptNo,
	@ReferenceNo,
	@SearchVehicle,
	@SourceData,
	@State,
	@ToDealer,
	@ToSite,
	@TransactionDate,
	@TransactionType,
	@TransferStatus,
	@TransferStep,
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
-- Date Created	: 25 Maret 2018
-- Created By	: Mitrais Team by using CodeSmith v 2.6
-- Rev History	:
---------------------------------------------------------------------------------------------------------------

CREATE PROCEDURE [dbo].up_RetrieveInventoryTransfer
	@ID int OUTPUT	
AS
SET TRANSACTION ISOLATION LEVEL READ COMMITTED

SET NOCOUNT ON

SELECT
	[ID],
	[Owner],
	[FromDealer],
	[FromSite],
	[InventoryTransferNo],
	[ItemTypeForTransfer],
	[PersonInCharge],
	[ReceiptDate],
	[ReceiptNo],
	[ReferenceNo],
	[SearchVehicle],
	[SourceData],
	[State],
	[ToDealer],
	[ToSite],
	[TransactionDate],
	[TransactionType],
	[TransferStatus],
	[TransferStep],
	[WONo],
	[RowStatus],
	[CreatedBy],
	[CreatedTime],
	[LastUpdateBy],
	[LastUpdateTime]	
FROM	[dbo].[InventoryTransfer]

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

CREATE PROCEDURE [dbo].up_RetrieveInventoryTransferList

AS

SET TRANSACTION ISOLATION LEVEL READ COMMITTED
SET NOCOUNT ON


SELECT
		[ID],
		[Owner],
		[FromDealer],
		[FromSite],
		[InventoryTransferNo],
		[ItemTypeForTransfer],
		[PersonInCharge],
		[ReceiptDate],
		[ReceiptNo],
		[ReferenceNo],
		[SearchVehicle],
		[SourceData],
		[State],
		[ToDealer],
		[ToSite],
		[TransactionDate],
		[TransactionType],
		[TransferStatus],
		[TransferStep],
		[WONo],
		[RowStatus],
		[CreatedBy],
		[CreatedTime],
		[LastUpdateBy],
		[LastUpdateTime]		
		FROM	
		[dbo].[InventoryTransfer] 

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

CREATE PROCEDURE [dbo].up_UpdateInventoryTransfer
	@ID int OUTPUT,
	@Owner varchar(100),
	@FromDealer varchar(100),
	@FromSite varchar(100),
	@InventoryTransferNo varchar(50),
	@ItemTypeForTransfer smallint,
	@PersonInCharge varchar(100),
	@ReceiptDate datetime,
	@ReceiptNo varchar(100),
	@ReferenceNo varchar(100),
	@SearchVehicle varchar(50),
	@SourceData varchar(50),
	@State smallint,
	@ToDealer varchar(100),
	@ToSite varchar(100),
	@TransactionDate datetime,
	@TransactionType smallint,
	@TransferStatus smallint,
	@TransferStep bit,
	@WONo varchar(100),
	@RowStatus smallint,
	@CreatedBy varchar(100),
	--@CreatedTime datetime,
	@LastUpdateBy varchar(100)	--@LastUpdateTime datetime	
AS

UPDATE	[dbo].[InventoryTransfer]
SET
	[Owner] = @Owner,
	[FromDealer] = @FromDealer,
	[FromSite] = @FromSite,
	[InventoryTransferNo] = @InventoryTransferNo,
	[ItemTypeForTransfer] = @ItemTypeForTransfer,
	[PersonInCharge] = @PersonInCharge,
	[ReceiptDate] = @ReceiptDate,
	[ReceiptNo] = @ReceiptNo,
	[ReferenceNo] = @ReferenceNo,
	[SearchVehicle] = @SearchVehicle,
	[SourceData] = @SourceData,
	[State] = @State,
	[ToDealer] = @ToDealer,
	[ToSite] = @ToSite,
	[TransactionDate] = @TransactionDate,
	[TransactionType] = @TransactionType,
	[TransferStatus] = @TransferStatus,
	[TransferStep] = @TransferStep,
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
-- Date Created	: 25 Maret 2018
-- Created By	: Mitrais Team by using CodeSmith v 2.6
-- Rev History	:
---------------------------------------------------------------------------------------------------------------

CREATE PROCEDURE [dbo].up_DeleteInventoryTransfer
	@ID int OUTPUT	
AS

DELETE
FROM	[dbo].[InventoryTransfer]
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

CREATE PROCEDURE [dbo].up_ValidateInventoryTransfer
	@Result	varchar(1000),
	@ID int OUTPUT,
	@Owner varchar(100),
	@FromDealer varchar(100),
	@FromSite varchar(100),
	@InventoryTransferNo varchar(50),
	@ItemTypeForTransfer smallint,
	@PersonInCharge varchar(100),
	@ReceiptDate datetime,
	@ReceiptNo varchar(100),
	@ReferenceNo varchar(100),
	@SearchVehicle varchar(50),
	@SourceData varchar(50),
	@State smallint,
	@ToDealer varchar(100),
	@ToSite varchar(100),
	@TransactionDate datetime,
	@TransactionType smallint,
	@TransferStatus smallint,
	@TransferStep bit,
	@WONo varchar(100),
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




