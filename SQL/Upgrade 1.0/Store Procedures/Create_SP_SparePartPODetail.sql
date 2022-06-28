USE [BSIDNET_MMKSI_DMS]
GO

/****** Object:  Stored Procedure [dbo].[up_UpdateSparePartPODetail]    Script Date: 06 Maret 2018 ******/
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'[dbo].[up_InsertSparePartPODetail]') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
DROP PROCEDURE [dbo].[up_InsertSparePartPODetail]
GO


/****** Object:  Stored Procedure [dbo].[up_RetrieveSparePartPODetail]    Script Date: 06 Maret 2018 ******/
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'[dbo].[up_RetrieveSparePartPODetail]') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
DROP PROCEDURE [dbo].[up_RetrieveSparePartPODetail]
GO


/****** Object:  Stored Procedure [dbo].[up_RetrieveSparePartPODetailList]    Script Date: 06 Maret 2018 ******/
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'[dbo].[up_RetrieveSparePartPODetailList]') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
DROP PROCEDURE [dbo].[up_RetrieveSparePartPODetailList]
GO


/****** Object:  Stored Procedure [dbo].[up_UpdateSparePartPODetail]    Script Date: 06 Maret 2018 ******/
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'[dbo].[up_UpdateSparePartPODetail]') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
DROP PROCEDURE [dbo].[up_UpdateSparePartPODetail]
GO


/****** Object:  Stored Procedure [dbo].[up_DeleteSparePartPODetail]    Script Date: 06 Maret 2018 ******/
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'[dbo].[up_DeleteSparePartPODetail]') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
DROP PROCEDURE [dbo].[up_DeleteSparePartPODetail]
GO


/****** Object:  Stored Procedure [dbo].[up_ValidateSparePartPODetail]    Script Date: 06 Maret 2018 ******/
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'[dbo].[up_ValidateSparePartPODetail]') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
DROP PROCEDURE [dbo].[up_ValidateSparePartPODetail]
GO



SET QUOTED_IDENTIFIER ON 
GO
SET ANSI_NULLS OFF 
GO
---------------------------------------------------------------------------------------------------------------
-- Date Created	: 06 Maret 2018
-- Created By	: Mitrais Team by using CodeSmith v 2.6
-- Rev History	:
---------------------------------------------------------------------------------------------------------------

CREATE PROCEDURE [dbo].up_InsertSparePartPODetail
	@ID int OUTPUT,
	@SparePartPOID int,
	@SparePartMasterID int,
	@CheckListStatus varchar(2),
	@Quantity int,
	@RetailPrice money,
	@EstimateStatus varchar(1),
	@StopMark smallint,
	@RowStatus smallint,
	@CreatedBy varchar(20),
	--@CreatedTime datetime,
	@LastUpdateBy varchar(20),
	--@LastUpdateTime datetime,
	@TotalForecast int
	
AS
INSERT	INTO	[dbo].[SparePartPODetail]
VALUES
(
	@SparePartPOID,
	@SparePartMasterID,
	@CheckListStatus,
	@Quantity,
	@RetailPrice,
	@EstimateStatus,
	@StopMark,
	@RowStatus,
	@CreatedBy,
	GETDATE(),	
	@LastUpdateBy,
	GETDATE(),	
	@TotalForecast)

	
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
-- Date Created	: 06 Maret 2018
-- Created By	: Mitrais Team by using CodeSmith v 2.6
-- Rev History	:
---------------------------------------------------------------------------------------------------------------

CREATE PROCEDURE [dbo].up_RetrieveSparePartPODetail
	@ID int OUTPUT	
AS
SET TRANSACTION ISOLATION LEVEL READ COMMITTED

SET NOCOUNT ON

SELECT
	[ID],
	[SparePartPOID],
	[SparePartMasterID],
	[CheckListStatus],
	[Quantity],
	[RetailPrice],
	[EstimateStatus],
	[StopMark],
	[RowStatus],
	[CreatedBy],
	[CreatedTime],
	[LastUpdateBy],
	[LastUpdateTime],
	[TotalForecast]	
FROM	[dbo].[SparePartPODetail]

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
-- Date Created	: 06 Maret 2018
-- Created By	: Mitrais Team by using CodeSmith v 2.6
-- Rev History	:
---------------------------------------------------------------------------------------------------------------

CREATE PROCEDURE [dbo].up_RetrieveSparePartPODetailList

AS

SET TRANSACTION ISOLATION LEVEL READ COMMITTED
SET NOCOUNT ON


SELECT
		[ID],
		[SparePartPOID],
		[SparePartMasterID],
		[CheckListStatus],
		[Quantity],
		[RetailPrice],
		[EstimateStatus],
		[StopMark],
		[RowStatus],
		[CreatedBy],
		[CreatedTime],
		[LastUpdateBy],
		[LastUpdateTime],
		[TotalForecast]		
		FROM	
		[dbo].[SparePartPODetail] 

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
-- Date Created	: 06 Maret 2018
-- Created By	: Mitrais Team by using CodeSmith v 2.6
-- Rev History	:
---------------------------------------------------------------------------------------------------------------

CREATE PROCEDURE [dbo].up_UpdateSparePartPODetail
	@ID int OUTPUT,
	@SparePartPOID int,
	@SparePartMasterID int,
	@CheckListStatus varchar(2),
	@Quantity int,
	@RetailPrice money,
	@EstimateStatus varchar(1),
	@StopMark smallint,
	@RowStatus smallint,
	@CreatedBy varchar(20),
	--@CreatedTime datetime,
	@LastUpdateBy varchar(20),
	--@LastUpdateTime datetime,
	@TotalForecast int
	
AS

UPDATE	[dbo].[SparePartPODetail]
SET
	[SparePartPOID] = @SparePartPOID,
	[SparePartMasterID] = @SparePartMasterID,
	[CheckListStatus] = @CheckListStatus,
	[Quantity] = @Quantity,
	[RetailPrice] = @RetailPrice,
	[EstimateStatus] = @EstimateStatus,
	[StopMark] = @StopMark,
	[RowStatus] = @RowStatus,
	[CreatedBy] = @CreatedBy,
	--[CreatedTime] = @CreatedTime,
	[LastUpdateBy] = @LastUpdateBy,
	[LastUpdateTime] = GETDATE(),
	[TotalForecast] = @TotalForecast	
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
-- Date Created	: 06 Maret 2018
-- Created By	: Mitrais Team by using CodeSmith v 2.6
-- Rev History	:
---------------------------------------------------------------------------------------------------------------

CREATE PROCEDURE [dbo].up_DeleteSparePartPODetail
	@ID int OUTPUT	
AS

DELETE
FROM	[dbo].[SparePartPODetail]
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
-- Date Created	: 06 Maret 2018
-- Created By	: Mitrais Team by using CodeSmith v 2.6
-- Rev History	:
---------------------------------------------------------------------------------------------------------------

CREATE PROCEDURE [dbo].up_ValidateSparePartPODetail
	@Result	varchar(1000),
	@ID int OUTPUT,
	@SparePartPOID int,
	@SparePartMasterID int,
	@CheckListStatus varchar(2),
	@Quantity int,
	@RetailPrice money,
	@EstimateStatus varchar(1),
	@StopMark smallint,
	@RowStatus smallint,
	@CreatedBy varchar(20),
	@CreatedTime datetime,
	@LastUpdateBy varchar(20),
	@LastUpdateTime datetime,
	@TotalForecast int	
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
-- Date Created	: 06 Maret 2018
-- Created By	: Mitrais Team by using CodeSmith v 2.6
-- Rev History	:
---------------------------------------------------------------------------------------------------------------
GRANT  EXECUTE  ON [dbo].up_InsertSparePartPODetail TO [bsi];

GRANT  EXECUTE  ON [dbo].up_UpdateSparePartPODetail TO [bsi];

GRANT  EXECUTE  ON [dbo].up_RetrieveSparePartPODetail TO [bsi];

GRANT  EXECUTE  ON [dbo].up_RetrieveSparePartPODetailList TO [bsi];

GRANT  EXECUTE  ON [dbo].up_ValidateSparePartPODetail TO [bsi];

GRANT  EXECUTE  ON [dbo].up_DeleteSparePartPODetail TO [bsi];

SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO



