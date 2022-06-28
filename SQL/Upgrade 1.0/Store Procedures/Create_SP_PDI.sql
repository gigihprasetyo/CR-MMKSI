
/****** Object:  Stored Procedure [dbo].[up_UpdatePDI]    Script Date: 13 Maret 2018 ******/
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'[dbo].[up_InsertPDI]') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
DROP PROCEDURE [dbo].[up_InsertPDI]
GO


/****** Object:  Stored Procedure [dbo].[up_RetrievePDI]    Script Date: 13 Maret 2018 ******/
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'[dbo].[up_RetrievePDI]') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
DROP PROCEDURE [dbo].[up_RetrievePDI]
GO


/****** Object:  Stored Procedure [dbo].[up_RetrievePDIList]    Script Date: 13 Maret 2018 ******/
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'[dbo].[up_RetrievePDIList]') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
DROP PROCEDURE [dbo].[up_RetrievePDIList]
GO


/****** Object:  Stored Procedure [dbo].[up_UpdatePDI]    Script Date: 13 Maret 2018 ******/
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'[dbo].[up_UpdatePDI]') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
DROP PROCEDURE [dbo].[up_UpdatePDI]
GO


/****** Object:  Stored Procedure [dbo].[up_DeletePDI]    Script Date: 13 Maret 2018 ******/
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'[dbo].[up_DeletePDI]') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
DROP PROCEDURE [dbo].[up_DeletePDI]
GO


/****** Object:  Stored Procedure [dbo].[up_ValidatePDI]    Script Date: 13 Maret 2018 ******/
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'[dbo].[up_ValidatePDI]') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
DROP PROCEDURE [dbo].[up_ValidatePDI]
GO



SET QUOTED_IDENTIFIER ON 
GO
SET ANSI_NULLS OFF 
GO
---------------------------------------------------------------------------------------------------------------
-- Date Created	: 13 Maret 2018
-- Created By	: DNet Team by using CodeSmith v 2.6
-- Rev History	:
---------------------------------------------------------------------------------------------------------------

CREATE PROCEDURE [dbo].up_InsertPDI
	@ID int OUTPUT,
	@ChassisMasterID int,
	@DealerID smallint,
	@DealerBranchID smallint,
	@Kind char(1),
	@PDIStatus char(1),
	@PDIDate datetime,
	@ReleaseBy varchar(20),
	@ReleaseDate datetime,
	@WorkOrderNumber varchar(50),
	@RowStatus smallint,
	@CreatedBy varchar(20),
	--@CreatedTime datetime,
	@LastUpdateBy varchar(20)	--@LastUpdateTime datetime	
AS
INSERT	INTO	[dbo].[PDI]
VALUES
(
	@ChassisMasterID,
	@DealerID,
	@DealerBranchID,
	@Kind,
	@PDIStatus,
	@PDIDate,
	@ReleaseBy,
	@ReleaseDate,
	@WorkOrderNumber,
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
-- Date Created	: 13 Maret 2018
-- Created By	: DNet Team by using CodeSmith v 2.6
-- Rev History	:
---------------------------------------------------------------------------------------------------------------

CREATE PROCEDURE [dbo].up_RetrievePDI
	@ID int OUTPUT	
AS
SET TRANSACTION ISOLATION LEVEL READ COMMITTED

SET NOCOUNT ON

SELECT
	[ID],
	[ChassisMasterID],
	[DealerID],
	[DealerBranchID],
	[Kind],
	[PDIStatus],
	[PDIDate],
	[ReleaseBy],
	[ReleaseDate],
	[WorkOrderNumber],
	[RowStatus],
	[CreatedBy],
	[CreatedTime],
	[LastUpdateBy],
	[LastUpdateTime]	
FROM	[dbo].[PDI]

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
-- Date Created	: 13 Maret 2018
-- Created By	: DNet Team by using CodeSmith v 2.6
-- Rev History	:
---------------------------------------------------------------------------------------------------------------

CREATE PROCEDURE [dbo].up_RetrievePDIList

AS

SET TRANSACTION ISOLATION LEVEL READ COMMITTED
SET NOCOUNT ON


SELECT
		[ID],
		[ChassisMasterID],
		[DealerID],
		[DealerBranchID],
		[Kind],
		[PDIStatus],
		[PDIDate],
		[ReleaseBy],
		[ReleaseDate],
		[WorkOrderNumber],
		[RowStatus],
		[CreatedBy],
		[CreatedTime],
		[LastUpdateBy],
		[LastUpdateTime]		
		FROM	
		[dbo].[PDI] 

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
-- Date Created	: 13 Maret 2018
-- Created By	: DNet Team by using CodeSmith v 2.6
-- Rev History	:
---------------------------------------------------------------------------------------------------------------

CREATE PROCEDURE [dbo].up_UpdatePDI
	@ID int OUTPUT,
	@ChassisMasterID int,
	@DealerID smallint,
	@DealerBranchID smallint,
	@Kind char(1),
	@PDIStatus char(1),
	@PDIDate datetime,
	@ReleaseBy varchar(20),
	@ReleaseDate datetime,
	@WorkOrderNumber varchar(50),
	@RowStatus smallint,
	@CreatedBy varchar(20),
	--@CreatedTime datetime,
	@LastUpdateBy varchar(20)	--@LastUpdateTime datetime	
AS

UPDATE	[dbo].[PDI]
SET
	[ChassisMasterID] = @ChassisMasterID,
	[DealerID] = @DealerID,
	[DealerBranchID] = @DealerBranchID,
	[Kind] = @Kind,
	[PDIStatus] = @PDIStatus,
	[PDIDate] = @PDIDate,
	[ReleaseBy] = @ReleaseBy,
	[ReleaseDate] = @ReleaseDate,
	[WorkOrderNumber] = @WorkOrderNumber,
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
-- Date Created	: 13 Maret 2018
-- Created By	: DNet Team by using CodeSmith v 2.6
-- Rev History	:
---------------------------------------------------------------------------------------------------------------

CREATE PROCEDURE [dbo].up_DeletePDI
	@ID int OUTPUT	
AS

DELETE
FROM	[dbo].[PDI]
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
-- Date Created	: 13 Maret 2018
-- Created By	: DNet Team by using CodeSmith v 2.6
-- Rev History	:
---------------------------------------------------------------------------------------------------------------

CREATE PROCEDURE [dbo].up_ValidatePDI
	@Result	varchar(1000),
	@ID int OUTPUT,
	@ChassisMasterID int,
	@DealerID smallint,
	@DealerBranchID smallint,
	@Kind char(1),
	@PDIStatus char(1),
	@PDIDate datetime,
	@ReleaseBy varchar(20),
	@ReleaseDate datetime,
	@WorkOrderNumber varchar(50),
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
-- Date Created	: 13 Maret 2018
-- Created By	: DNet Team by using CodeSmith v 2.6
-- Rev History	:
---------------------------------------------------------------------------------------------------------------
GRANT  EXECUTE  ON [dbo].up_InsertPDI TO [bsi];

GRANT  EXECUTE  ON [dbo].up_UpdatePDI TO [bsi];

GRANT  EXECUTE  ON [dbo].up_RetrievePDI TO [bsi];

GRANT  EXECUTE  ON [dbo].up_RetrievePDIList TO [bsi];

GRANT  EXECUTE  ON [dbo].up_ValidatePDI TO [bsi];

GRANT  EXECUTE  ON [dbo].up_DeletePDI TO [bsi];

SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO



