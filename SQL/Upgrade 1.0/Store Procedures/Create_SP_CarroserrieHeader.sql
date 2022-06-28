
/****** Object:  Stored Procedure [dbo].[up_UpdateCarrosserieHeader]    Script Date: 05 Maret 2018 ******/
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'[dbo].[up_InsertCarrosserieHeader]') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
DROP PROCEDURE [dbo].[up_InsertCarrosserieHeader]
GO


/****** Object:  Stored Procedure [dbo].[up_RetrieveCarrosserieHeader]    Script Date: 05 Maret 2018 ******/
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'[dbo].[up_RetrieveCarrosserieHeader]') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
DROP PROCEDURE [dbo].[up_RetrieveCarrosserieHeader]
GO


/****** Object:  Stored Procedure [dbo].[up_RetrieveCarrosserieHeaderList]    Script Date: 05 Maret 2018 ******/
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'[dbo].[up_RetrieveCarrosserieHeaderList]') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
DROP PROCEDURE [dbo].[up_RetrieveCarrosserieHeaderList]
GO


/****** Object:  Stored Procedure [dbo].[up_UpdateCarrosserieHeader]    Script Date: 05 Maret 2018 ******/
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'[dbo].[up_UpdateCarrosserieHeader]') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
DROP PROCEDURE [dbo].[up_UpdateCarrosserieHeader]
GO


/****** Object:  Stored Procedure [dbo].[up_DeleteCarrosserieHeader]    Script Date: 05 Maret 2018 ******/
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'[dbo].[up_DeleteCarrosserieHeader]') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
DROP PROCEDURE [dbo].[up_DeleteCarrosserieHeader]
GO


/****** Object:  Stored Procedure [dbo].[up_ValidateCarrosserieHeader]    Script Date: 05 Maret 2018 ******/
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'[dbo].[up_ValidateCarrosserieHeader]') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
DROP PROCEDURE [dbo].[up_ValidateCarrosserieHeader]
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

CREATE PROCEDURE [dbo].up_InsertCarrosserieHeader
	@ID int OUTPUT,
	@PDIStateCode smallint,
	@PDIStateName varchar(50),
	@PDIStatusCode smallint,
	@PDIStatusName varchar(50),
	@BUID int,
	@BUName varchar(50),
	@PDINO int,
	@PDIName varchar(100),
	@PDIReceiptNO varchar(50),
	@PDIReceiptRef int,
	@PDIReceiptRefName varchar(100),
	@PDIReceiptStatus smallint,
	@PDIReceiptStatusName varchar(50),
	@TransactionDate datetime,
	@TransactionType smallint,
	@VendorID int,
	@VendorName varchar(100),
	@ChassisNumber varchar(20),
	@RowStatus smallint,
	@CreatedBy varchar(20),
	--@CreatedTime datetime,
	@LastUpdateBy varchar(20)	--@LastUpdateTime datetime	
AS
INSERT	INTO	[dbo].[CarrosserieHeader]
VALUES
(
	@PDIStateCode,
	@PDIStateName,
	@PDIStatusCode,
	@PDIStatusName,
	@BUID,
	@BUName,
	@PDINO,
	@PDIName,
	@PDIReceiptNO,
	@PDIReceiptRef,
	@PDIReceiptRefName,
	@PDIReceiptStatus,
	@PDIReceiptStatusName,
	@TransactionDate,
	@TransactionType,
	@VendorID,
	@VendorName,
	@ChassisNumber,
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

CREATE PROCEDURE [dbo].up_RetrieveCarrosserieHeader
	@ID int OUTPUT	
AS
SET TRANSACTION ISOLATION LEVEL READ COMMITTED

SET NOCOUNT ON

SELECT
	[ID],
	[PDIStateCode],
	[PDIStateName],
	[PDIStatusCode],
	[PDIStatusName],
	[BUID],
	[BUName],
	[PDINO],
	[PDIName],
	[PDIReceiptNO],
	[PDIReceiptRef],
	[PDIReceiptRefName],
	[PDIReceiptStatus],
	[PDIReceiptStatusName],
	[TransactionDate],
	[TransactionType],
	[VendorID],
	[VendorName],
	[ChassisNumber],
	[RowStatus],
	[CreatedBy],
	[CreatedTime],
	[LastUpdateBy],
	[LastUpdateTime]	
FROM	[dbo].[CarrosserieHeader]

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

CREATE PROCEDURE [dbo].up_RetrieveCarrosserieHeaderList

AS

SET TRANSACTION ISOLATION LEVEL READ COMMITTED
SET NOCOUNT ON


SELECT
		[ID],
		[PDIStateCode],
		[PDIStateName],
		[PDIStatusCode],
		[PDIStatusName],
		[BUID],
		[BUName],
		[PDINO],
		[PDIName],
		[PDIReceiptNO],
		[PDIReceiptRef],
		[PDIReceiptRefName],
		[PDIReceiptStatus],
		[PDIReceiptStatusName],
		[TransactionDate],
		[TransactionType],
		[VendorID],
		[VendorName],
		[ChassisNumber],
		[RowStatus],
		[CreatedBy],
		[CreatedTime],
		[LastUpdateBy],
		[LastUpdateTime]		
		FROM	
		[dbo].[CarrosserieHeader] 

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

CREATE PROCEDURE [dbo].up_UpdateCarrosserieHeader
	@ID int OUTPUT,
	@PDIStateCode smallint,
	@PDIStateName varchar(50),
	@PDIStatusCode smallint,
	@PDIStatusName varchar(50),
	@BUID int,
	@BUName varchar(50),
	@PDINO int,
	@PDIName varchar(100),
	@PDIReceiptNO varchar(50),
	@PDIReceiptRef int,
	@PDIReceiptRefName varchar(100),
	@PDIReceiptStatus smallint,
	@PDIReceiptStatusName varchar(50),
	@TransactionDate datetime,
	@TransactionType smallint,
	@VendorID int,
	@VendorName varchar(100),
	@ChassisNumber varchar(20),
	@RowStatus smallint,
	@CreatedBy varchar(20),
	--@CreatedTime datetime,
	@LastUpdateBy varchar(20)	--@LastUpdateTime datetime	
AS

UPDATE	[dbo].[CarrosserieHeader]
SET
	[PDIStateCode] = @PDIStateCode,
	[PDIStateName] = @PDIStateName,
	[PDIStatusCode] = @PDIStatusCode,
	[PDIStatusName] = @PDIStatusName,
	[BUID] = @BUID,
	[BUName] = @BUName,
	[PDINO] = @PDINO,
	[PDIName] = @PDIName,
	[PDIReceiptNO] = @PDIReceiptNO,
	[PDIReceiptRef] = @PDIReceiptRef,
	[PDIReceiptRefName] = @PDIReceiptRefName,
	[PDIReceiptStatus] = @PDIReceiptStatus,
	[PDIReceiptStatusName] = @PDIReceiptStatusName,
	[TransactionDate] = @TransactionDate,
	[TransactionType] = @TransactionType,
	[VendorID] = @VendorID,
	[VendorName] = @VendorName,
	[ChassisNumber] = @ChassisNumber,
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

CREATE PROCEDURE [dbo].up_DeleteCarrosserieHeader
	@ID int OUTPUT	
AS

DELETE
FROM	[dbo].[CarrosserieHeader]
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

CREATE PROCEDURE [dbo].up_ValidateCarrosserieHeader
	@Result	varchar(1000),
	@ID int OUTPUT,
	@PDIStateCode smallint,
	@PDIStateName varchar(50),
	@PDIStatusCode smallint,
	@PDIStatusName varchar(50),
	@BUID int,
	@BUName varchar(50),
	@PDINO int,
	@PDIName varchar(100),
	@PDIReceiptNO varchar(50),
	@PDIReceiptRef int,
	@PDIReceiptRefName varchar(100),
	@PDIReceiptStatus smallint,
	@PDIReceiptStatusName varchar(50),
	@TransactionDate datetime,
	@TransactionType smallint,
	@VendorID int,
	@VendorName varchar(100),
	@ChassisNumber varchar(20),
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
-- Date Created	: 05 Maret 2018
-- Created By	: DNet Team by using CodeSmith v 2.6
-- Rev History	:
---------------------------------------------------------------------------------------------------------------
GRANT  EXECUTE  ON [dbo].up_InsertCarrosserieHeader TO [bsi];

GRANT  EXECUTE  ON [dbo].up_UpdateCarrosserieHeader TO [bsi];

GRANT  EXECUTE  ON [dbo].up_RetrieveCarrosserieHeader TO [bsi];

GRANT  EXECUTE  ON [dbo].up_RetrieveCarrosserieHeaderList TO [bsi];

GRANT  EXECUTE  ON [dbo].up_ValidateCarrosserieHeader TO [bsi];

GRANT  EXECUTE  ON [dbo].up_DeleteCarrosserieHeader TO [bsi];

SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO



