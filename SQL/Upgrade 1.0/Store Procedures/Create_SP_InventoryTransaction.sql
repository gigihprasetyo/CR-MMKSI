
/****** Object:  Stored Procedure [dbo].[up_UpdateInventoryTransaction]    Script Date: 16 Maret 2018 ******/
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'[dbo].[up_InsertInventoryTransaction]') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
DROP PROCEDURE [dbo].[up_InsertInventoryTransaction]
GO


/****** Object:  Stored Procedure [dbo].[up_RetrieveInventoryTransaction]    Script Date: 16 Maret 2018 ******/
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'[dbo].[up_RetrieveInventoryTransaction]') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
DROP PROCEDURE [dbo].[up_RetrieveInventoryTransaction]
GO


/****** Object:  Stored Procedure [dbo].[up_RetrieveInventoryTransactionList]    Script Date: 16 Maret 2018 ******/
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'[dbo].[up_RetrieveInventoryTransactionList]') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
DROP PROCEDURE [dbo].[up_RetrieveInventoryTransactionList]
GO


/****** Object:  Stored Procedure [dbo].[up_UpdateInventoryTransaction]    Script Date: 16 Maret 2018 ******/
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'[dbo].[up_UpdateInventoryTransaction]') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
DROP PROCEDURE [dbo].[up_UpdateInventoryTransaction]
GO


/****** Object:  Stored Procedure [dbo].[up_DeleteInventoryTransaction]    Script Date: 16 Maret 2018 ******/
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'[dbo].[up_DeleteInventoryTransaction]') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
DROP PROCEDURE [dbo].[up_DeleteInventoryTransaction]
GO


/****** Object:  Stored Procedure [dbo].[up_ValidateInventoryTransaction]    Script Date: 16 Maret 2018 ******/
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'[dbo].[up_ValidateInventoryTransaction]') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
DROP PROCEDURE [dbo].[up_ValidateInventoryTransaction]
GO



SET QUOTED_IDENTIFIER ON 
GO
SET ANSI_NULLS OFF 
GO
---------------------------------------------------------------------------------------------------------------
-- Date Created	: 16 Maret 2018
-- Created By	: Mitrais Team by using CodeSmith v 2.6
-- Rev History	:
---------------------------------------------------------------------------------------------------------------

CREATE PROCEDURE [dbo].up_InsertInventoryTransaction
	@ID int OUTPUT,
	@Owner varchar(100),
	@DealerCode varchar(100),
	@InventoryTransactionNo varchar(100),
	@InventoryTransferNo varchar(100),
	@PersonInCharge varchar(100),
	@ProcessCode varchar(10),
	@SourceData varchar(50),
	@State smallint,
	@TransactionDate datetime,
	@TransactionType smallint,
	@WONo varchar(100),
	@RowStatus smallint,
	@CreatedBy varchar(20),
	--@CreatedTime datetime,
	@LastUpdateBy varchar(20)	--@LastUpdateTime datetime	
AS
INSERT	INTO	[dbo].[InventoryTransaction]
VALUES
(
	@Owner,
	@DealerCode,
	@InventoryTransactionNo,
	@InventoryTransferNo,
	@PersonInCharge,
	@ProcessCode,
	@SourceData,
	@State,
	@TransactionDate,
	@TransactionType,
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
-- Date Created	: 16 Maret 2018
-- Created By	: Mitrais Team by using CodeSmith v 2.6
-- Rev History	:
---------------------------------------------------------------------------------------------------------------

CREATE PROCEDURE [dbo].up_RetrieveInventoryTransaction
	@ID int OUTPUT	
AS
SET TRANSACTION ISOLATION LEVEL READ COMMITTED

SET NOCOUNT ON

SELECT
	[ID],
	[Owner],
	[DealerCode],
	[InventoryTransactionNo],
	[InventoryTransferNo],
	[PersonInCharge],
	[ProcessCode],
	[SourceData],
	[State],
	[TransactionDate],
	[TransactionType],
	[WONo],
	[RowStatus],
	[CreatedBy],
	[CreatedTime],
	[LastUpdateBy],
	[LastUpdateTime]	
FROM	[dbo].[InventoryTransaction]

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
-- Date Created	: 16 Maret 2018
-- Created By	: Mitrais Team by using CodeSmith v 2.6
-- Rev History	:
---------------------------------------------------------------------------------------------------------------

CREATE PROCEDURE [dbo].up_RetrieveInventoryTransactionList

AS

SET TRANSACTION ISOLATION LEVEL READ COMMITTED
SET NOCOUNT ON


SELECT
		[ID],
		[Owner],
		[DealerCode],
		[InventoryTransactionNo],
		[InventoryTransferNo],
		[PersonInCharge],
		[ProcessCode],
		[SourceData],
		[State],
		[TransactionDate],
		[TransactionType],
		[WONo],
		[RowStatus],
		[CreatedBy],
		[CreatedTime],
		[LastUpdateBy],
		[LastUpdateTime]		
		FROM	
		[dbo].[InventoryTransaction] 

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
-- Date Created	: 16 Maret 2018
-- Created By	: Mitrais Team by using CodeSmith v 2.6
-- Rev History	:
---------------------------------------------------------------------------------------------------------------

CREATE PROCEDURE [dbo].up_UpdateInventoryTransaction
	@ID int OUTPUT,
	@Owner varchar(100),
	@DealerCode varchar(100),
	@InventoryTransactionNo varchar(100),
	@InventoryTransferNo varchar(100),
	@PersonInCharge varchar(100),
	@ProcessCode varchar(10),
	@SourceData varchar(50),
	@State smallint,
	@TransactionDate datetime,
	@TransactionType smallint,
	@WONo varchar(100),
	@RowStatus smallint,
	@CreatedBy varchar(20),
	--@CreatedTime datetime,
	@LastUpdateBy varchar(20)	--@LastUpdateTime datetime	
AS

UPDATE	[dbo].[InventoryTransaction]
SET
	[Owner] = @Owner,
	[DealerCode] = @DealerCode,
	[InventoryTransactionNo] = @InventoryTransactionNo,
	[InventoryTransferNo] = @InventoryTransferNo,
	[PersonInCharge] = @PersonInCharge,
	[ProcessCode] = @ProcessCode,
	[SourceData] = @SourceData,
	[State] = @State,
	[TransactionDate] = @TransactionDate,
	[TransactionType] = @TransactionType,
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
-- Date Created	: 16 Maret 2018
-- Created By	: Mitrais Team by using CodeSmith v 2.6
-- Rev History	:
---------------------------------------------------------------------------------------------------------------

CREATE PROCEDURE [dbo].up_DeleteInventoryTransaction
	@ID int OUTPUT	
AS

DELETE
FROM	[dbo].[InventoryTransaction]
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
-- Date Created	: 16 Maret 2018
-- Created By	: Mitrais Team by using CodeSmith v 2.6
-- Rev History	:
---------------------------------------------------------------------------------------------------------------

CREATE PROCEDURE [dbo].up_ValidateInventoryTransaction
	@Result	varchar(1000),
	@ID int OUTPUT,
	@Owner varchar(100),
	@DealerCode varchar(100),
	@InventoryTransactionNo varchar(100),
	@InventoryTransferNo varchar(100),
	@PersonInCharge varchar(100),
	@ProcessCode varchar(10),
	@SourceData varchar(50),
	@State smallint,
	@TransactionDate datetime,
	@TransactionType smallint,
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
-- Date Created	: 16 Maret 2018
-- Created By	: Mitrais Team by using CodeSmith v 2.6
-- Rev History	:
---------------------------------------------------------------------------------------------------------------
GRANT  EXECUTE  ON [dbo].up_InsertInventoryTransaction TO [bsi];

GRANT  EXECUTE  ON [dbo].up_UpdateInventoryTransaction TO [bsi];

GRANT  EXECUTE  ON [dbo].up_RetrieveInventoryTransaction TO [bsi];

GRANT  EXECUTE  ON [dbo].up_RetrieveInventoryTransactionList TO [bsi];

GRANT  EXECUTE  ON [dbo].up_ValidateInventoryTransaction TO [bsi];

GRANT  EXECUTE  ON [dbo].up_DeleteInventoryTransaction TO [bsi];

SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO



