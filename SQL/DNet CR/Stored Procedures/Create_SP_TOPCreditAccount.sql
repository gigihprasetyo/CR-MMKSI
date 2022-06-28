
/****** Object:  Stored Procedure [dbo].[up_UpdateTOPCreditAccount]    Script Date: 16 Agustus 2018 ******/
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'[dbo].[up_InsertTOPCreditAccount]') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
DROP PROCEDURE [dbo].[up_InsertTOPCreditAccount]
GO


/****** Object:  Stored Procedure [dbo].[up_RetrieveTOPCreditAccount]    Script Date: 16 Agustus 2018 ******/
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'[dbo].[up_RetrieveTOPCreditAccount]') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
DROP PROCEDURE [dbo].[up_RetrieveTOPCreditAccount]
GO


/****** Object:  Stored Procedure [dbo].[up_RetrieveTOPCreditAccountList]    Script Date: 16 Agustus 2018 ******/
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'[dbo].[up_RetrieveTOPCreditAccountList]') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
DROP PROCEDURE [dbo].[up_RetrieveTOPCreditAccountList]
GO


/****** Object:  Stored Procedure [dbo].[up_UpdateTOPCreditAccount]    Script Date: 16 Agustus 2018 ******/
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'[dbo].[up_UpdateTOPCreditAccount]') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
DROP PROCEDURE [dbo].[up_UpdateTOPCreditAccount]
GO


/****** Object:  Stored Procedure [dbo].[up_DeleteTOPCreditAccount]    Script Date: 16 Agustus 2018 ******/
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'[dbo].[up_DeleteTOPCreditAccount]') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
DROP PROCEDURE [dbo].[up_DeleteTOPCreditAccount]
GO


/****** Object:  Stored Procedure [dbo].[up_ValidateTOPCreditAccount]    Script Date: 16 Agustus 2018 ******/
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'[dbo].[up_ValidateTOPCreditAccount]') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
DROP PROCEDURE [dbo].[up_ValidateTOPCreditAccount]
GO



SET QUOTED_IDENTIFIER ON 
GO
SET ANSI_NULLS OFF 
GO
---------------------------------------------------------------------------------------------------------------
-- Date Created	: 16 Agustus 2018
-- Created By	: DNet Team by using CodeSmith v 2.6
-- Rev History	:
---------------------------------------------------------------------------------------------------------------

CREATE PROCEDURE [dbo].up_InsertTOPCreditAccount
	@ID int OUTPUT,
	@DealerID smallint,
	@TermOfPaymentID int,
	@KelipatanPembayaran int,
	@Status smallint,
	@RowStatus smallint,
	@CreatedBy varchar(20),
	--@CreatedTime datetime,
	@LastUpdateBy varchar(20)	--@LastUpdateTime datetime	
AS
INSERT	INTO	[dbo].[TOPCreditAccount]
VALUES
(
	@DealerID,
	@TermOfPaymentID,
	@KelipatanPembayaran,
	@Status,
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
-- Date Created	: 16 Agustus 2018
-- Created By	: DNet Team by using CodeSmith v 2.6
-- Rev History	:
---------------------------------------------------------------------------------------------------------------

CREATE PROCEDURE [dbo].up_RetrieveTOPCreditAccount
	@ID int OUTPUT	
AS
SET TRANSACTION ISOLATION LEVEL READ COMMITTED

SET NOCOUNT ON

SELECT
	[ID],
	[DealerID],
	[TermOfPaymentID],
	[KelipatanPembayaran],
	[Status],
	[RowStatus],
	[CreatedBy],
	[CreatedTime],
	[LastUpdateBy],
	[LastUpdateTime]	
FROM	[dbo].[TOPCreditAccount]

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
-- Date Created	: 16 Agustus 2018
-- Created By	: DNet Team by using CodeSmith v 2.6
-- Rev History	:
---------------------------------------------------------------------------------------------------------------

CREATE PROCEDURE [dbo].up_RetrieveTOPCreditAccountList

AS

SET TRANSACTION ISOLATION LEVEL READ COMMITTED
SET NOCOUNT ON


SELECT
		[ID],
		[DealerID],
		[TermOfPaymentID],
		[KelipatanPembayaran],
		[Status],
		[RowStatus],
		[CreatedBy],
		[CreatedTime],
		[LastUpdateBy],
		[LastUpdateTime]		
		FROM	
		[dbo].[TOPCreditAccount] 

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
-- Date Created	: 16 Agustus 2018
-- Created By	: DNet Team by using CodeSmith v 2.6
-- Rev History	:
---------------------------------------------------------------------------------------------------------------

CREATE PROCEDURE [dbo].up_UpdateTOPCreditAccount
	@ID int OUTPUT,
	@DealerID smallint,
	@TermOfPaymentID int,
	@KelipatanPembayaran int,
	@Status smallint,
	@RowStatus smallint,
	@CreatedBy varchar(20),
	--@CreatedTime datetime,
	@LastUpdateBy varchar(20)	--@LastUpdateTime datetime	
AS

UPDATE	[dbo].[TOPCreditAccount]
SET
	[DealerID] = @DealerID,
	[TermOfPaymentID] = @TermOfPaymentID,
	[KelipatanPembayaran] = @KelipatanPembayaran,
	[Status] = @Status,
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
-- Date Created	: 16 Agustus 2018
-- Created By	: DNet Team by using CodeSmith v 2.6
-- Rev History	:
---------------------------------------------------------------------------------------------------------------

CREATE PROCEDURE [dbo].up_DeleteTOPCreditAccount
	@ID int OUTPUT	
AS

DELETE
FROM	[dbo].[TOPCreditAccount]
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
-- Date Created	: 16 Agustus 2018
-- Created By	: DNet Team by using CodeSmith v 2.6
-- Rev History	:
---------------------------------------------------------------------------------------------------------------

CREATE PROCEDURE [dbo].up_ValidateTOPCreditAccount
	@Result	varchar(1000),
	@ID int OUTPUT,
	@DealerID smallint,
	@TermOfPaymentID int,
	@KelipatanPembayaran int,
	@Status smallint,
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
-- Date Created	: 16 Agustus 2018
-- Created By	: DNet Team by using CodeSmith v 2.6
-- Rev History	:
---------------------------------------------------------------------------------------------------------------
GRANT  EXECUTE  ON [dbo].up_InsertTOPCreditAccount TO [bsi];

GRANT  EXECUTE  ON [dbo].up_UpdateTOPCreditAccount TO [bsi];

GRANT  EXECUTE  ON [dbo].up_RetrieveTOPCreditAccount TO [bsi];

GRANT  EXECUTE  ON [dbo].up_RetrieveTOPCreditAccountList TO [bsi];

GRANT  EXECUTE  ON [dbo].up_ValidateTOPCreditAccount TO [bsi];

GRANT  EXECUTE  ON [dbo].up_DeleteTOPCreditAccount TO [bsi];

SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO



