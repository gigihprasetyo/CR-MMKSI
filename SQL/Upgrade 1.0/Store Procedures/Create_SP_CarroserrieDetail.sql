
/****** Object:  Stored Procedure [dbo].[up_UpdateCarrosserieDetail]    Script Date: 05 Maret 2018 ******/
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'[dbo].[up_InsertCarrosserieDetail]') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
DROP PROCEDURE [dbo].[up_InsertCarrosserieDetail]
GO


/****** Object:  Stored Procedure [dbo].[up_RetrieveCarrosserieDetail]    Script Date: 05 Maret 2018 ******/
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'[dbo].[up_RetrieveCarrosserieDetail]') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
DROP PROCEDURE [dbo].[up_RetrieveCarrosserieDetail]
GO


/****** Object:  Stored Procedure [dbo].[up_RetrieveCarrosserieDetailList]    Script Date: 05 Maret 2018 ******/
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'[dbo].[up_RetrieveCarrosserieDetailList]') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
DROP PROCEDURE [dbo].[up_RetrieveCarrosserieDetailList]
GO


/****** Object:  Stored Procedure [dbo].[up_UpdateCarrosserieDetail]    Script Date: 05 Maret 2018 ******/
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'[dbo].[up_UpdateCarrosserieDetail]') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
DROP PROCEDURE [dbo].[up_UpdateCarrosserieDetail]
GO


/****** Object:  Stored Procedure [dbo].[up_DeleteCarrosserieDetail]    Script Date: 05 Maret 2018 ******/
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'[dbo].[up_DeleteCarrosserieDetail]') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
DROP PROCEDURE [dbo].[up_DeleteCarrosserieDetail]
GO


/****** Object:  Stored Procedure [dbo].[up_ValidateCarrosserieDetail]    Script Date: 05 Maret 2018 ******/
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'[dbo].[up_ValidateCarrosserieDetail]') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
DROP PROCEDURE [dbo].[up_ValidateCarrosserieDetail]
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

CREATE PROCEDURE [dbo].up_InsertCarrosserieDetail
	@ID int OUTPUT,
	@PDIStateCode smallint,
	@PDIStateName varchar(50),
	@PDIStatusCode smallint,
	@PDIStatusName varchar(50),
	@AccessorriesDescription varchar(100),
	@AccessorriesID int,
	@AccessorriesName varchar(100),
	@BUID int,
	@BUName varchar(100),
	@KITID int,
	@KITName varchar(100),
	@PBUID int,
	@PBUName varchar(100),
	@PDIDetailID int,
	@PDIDetailName varchar(100),
	@PDIReceiptDetailID int,
	@PDIReceiptDetailNO varchar(50),
	@PDIReceiptNO varchar(50),
	@PDIReceiptName varchar(100),
	@ReceiveQuantity float,
	@RowStatus smallint,
	@CreatedBy varchar(20),
	--@CreatedTime datetime,
	@LastUpdateBy varchar(20)	--@LastUpdateTime datetime	
AS
INSERT	INTO	[dbo].[CarrosserieDetail]
VALUES
(
	@PDIStateCode,
	@PDIStateName,
	@PDIStatusCode,
	@PDIStatusName,
	@AccessorriesDescription,
	@AccessorriesID,
	@AccessorriesName,
	@BUID,
	@BUName,
	@KITID,
	@KITName,
	@PBUID,
	@PBUName,
	@PDIDetailID,
	@PDIDetailName,
	@PDIReceiptDetailID,
	@PDIReceiptDetailNO,
	@PDIReceiptNO,
	@PDIReceiptName,
	@ReceiveQuantity,
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

CREATE PROCEDURE [dbo].up_RetrieveCarrosserieDetail
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
	[AccessorriesDescription],
	[AccessorriesID],
	[AccessorriesName],
	[BUID],
	[BUName],
	[KITID],
	[KITName],
	[PBUID],
	[PBUName],
	[PDIDetailID],
	[PDIDetailName],
	[PDIReceiptDetailID],
	[PDIReceiptDetailNO],
	[PDIReceiptNO],
	[PDIReceiptName],
	[ReceiveQuantity],
	[RowStatus],
	[CreatedBy],
	[CreatedTime],
	[LastUpdateBy],
	[LastUpdateTime]	
FROM	[dbo].[CarrosserieDetail]

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

CREATE PROCEDURE [dbo].up_RetrieveCarrosserieDetailList

AS

SET TRANSACTION ISOLATION LEVEL READ COMMITTED
SET NOCOUNT ON


SELECT
		[ID],
		[PDIStateCode],
		[PDIStateName],
		[PDIStatusCode],
		[PDIStatusName],
		[AccessorriesDescription],
		[AccessorriesID],
		[AccessorriesName],
		[BUID],
		[BUName],
		[KITID],
		[KITName],
		[PBUID],
		[PBUName],
		[PDIDetailID],
		[PDIDetailName],
		[PDIReceiptDetailID],
		[PDIReceiptDetailNO],
		[PDIReceiptNO],
		[PDIReceiptName],
		[ReceiveQuantity],
		[RowStatus],
		[CreatedBy],
		[CreatedTime],
		[LastUpdateBy],
		[LastUpdateTime]		
		FROM	
		[dbo].[CarrosserieDetail] 

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

CREATE PROCEDURE [dbo].up_UpdateCarrosserieDetail
	@ID int OUTPUT,
	@PDIStateCode smallint,
	@PDIStateName varchar(50),
	@PDIStatusCode smallint,
	@PDIStatusName varchar(50),
	@AccessorriesDescription varchar(100),
	@AccessorriesID int,
	@AccessorriesName varchar(100),
	@BUID int,
	@BUName varchar(100),
	@KITID int,
	@KITName varchar(100),
	@PBUID int,
	@PBUName varchar(100),
	@PDIDetailID int,
	@PDIDetailName varchar(100),
	@PDIReceiptDetailID int,
	@PDIReceiptDetailNO varchar(50),
	@PDIReceiptNO varchar(50),
	@PDIReceiptName varchar(100),
	@ReceiveQuantity float,
	@RowStatus smallint,
	@CreatedBy varchar(20),
	--@CreatedTime datetime,
	@LastUpdateBy varchar(20)	--@LastUpdateTime datetime	
AS

UPDATE	[dbo].[CarrosserieDetail]
SET
	[PDIStateCode] = @PDIStateCode,
	[PDIStateName] = @PDIStateName,
	[PDIStatusCode] = @PDIStatusCode,
	[PDIStatusName] = @PDIStatusName,
	[AccessorriesDescription] = @AccessorriesDescription,
	[AccessorriesID] = @AccessorriesID,
	[AccessorriesName] = @AccessorriesName,
	[BUID] = @BUID,
	[BUName] = @BUName,
	[KITID] = @KITID,
	[KITName] = @KITName,
	[PBUID] = @PBUID,
	[PBUName] = @PBUName,
	[PDIDetailID] = @PDIDetailID,
	[PDIDetailName] = @PDIDetailName,
	[PDIReceiptDetailID] = @PDIReceiptDetailID,
	[PDIReceiptDetailNO] = @PDIReceiptDetailNO,
	[PDIReceiptNO] = @PDIReceiptNO,
	[PDIReceiptName] = @PDIReceiptName,
	[ReceiveQuantity] = @ReceiveQuantity,
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

CREATE PROCEDURE [dbo].up_DeleteCarrosserieDetail
	@ID int OUTPUT	
AS

DELETE
FROM	[dbo].[CarrosserieDetail]
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

CREATE PROCEDURE [dbo].up_ValidateCarrosserieDetail
	@Result	varchar(1000),
	@ID int OUTPUT,
	@PDIStateCode smallint,
	@PDIStateName varchar(50),
	@PDIStatusCode smallint,
	@PDIStatusName varchar(50),
	@AccessorriesDescription varchar(100),
	@AccessorriesID int,
	@AccessorriesName varchar(100),
	@BUID int,
	@BUName varchar(100),
	@KITID int,
	@KITName varchar(100),
	@PBUID int,
	@PBUName varchar(100),
	@PDIDetailID int,
	@PDIDetailName varchar(100),
	@PDIReceiptDetailID int,
	@PDIReceiptDetailNO varchar(50),
	@PDIReceiptNO varchar(50),
	@PDIReceiptName varchar(100),
	@ReceiveQuantity float,
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
GRANT  EXECUTE  ON [dbo].up_InsertCarrosserieDetail TO [bsi];

GRANT  EXECUTE  ON [dbo].up_UpdateCarrosserieDetail TO [bsi];

GRANT  EXECUTE  ON [dbo].up_RetrieveCarrosserieDetail TO [bsi];

GRANT  EXECUTE  ON [dbo].up_RetrieveCarrosserieDetailList TO [bsi];

GRANT  EXECUTE  ON [dbo].up_ValidateCarrosserieDetail TO [bsi];

GRANT  EXECUTE  ON [dbo].up_DeleteCarrosserieDetail TO [bsi];

SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO



