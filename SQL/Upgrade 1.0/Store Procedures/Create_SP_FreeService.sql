
/****** Object:  Stored Procedure [dbo].[up_UpdateFreeService]    Script Date: 14 Maret 2018 ******/
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'[dbo].[up_InsertFreeService]') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
DROP PROCEDURE [dbo].[up_InsertFreeService]
GO


/****** Object:  Stored Procedure [dbo].[up_RetrieveFreeService]    Script Date: 14 Maret 2018 ******/
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'[dbo].[up_RetrieveFreeService]') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
DROP PROCEDURE [dbo].[up_RetrieveFreeService]
GO


/****** Object:  Stored Procedure [dbo].[up_RetrieveFreeServiceList]    Script Date: 14 Maret 2018 ******/
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'[dbo].[up_RetrieveFreeServiceList]') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
DROP PROCEDURE [dbo].[up_RetrieveFreeServiceList]
GO


/****** Object:  Stored Procedure [dbo].[up_UpdateFreeService]    Script Date: 14 Maret 2018 ******/
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'[dbo].[up_UpdateFreeService]') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
DROP PROCEDURE [dbo].[up_UpdateFreeService]
GO


/****** Object:  Stored Procedure [dbo].[up_DeleteFreeService]    Script Date: 14 Maret 2018 ******/
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'[dbo].[up_DeleteFreeService]') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
DROP PROCEDURE [dbo].[up_DeleteFreeService]
GO


/****** Object:  Stored Procedure [dbo].[up_ValidateFreeService]    Script Date: 14 Maret 2018 ******/
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'[dbo].[up_ValidateFreeService]') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
DROP PROCEDURE [dbo].[up_ValidateFreeService]
GO



SET QUOTED_IDENTIFIER ON 
GO
SET ANSI_NULLS OFF 
GO
---------------------------------------------------------------------------------------------------------------
-- Date Created	: 14 Maret 2018
-- Created By	: DNet Team by using CodeSmith v 2.6
-- Rev History	:
---------------------------------------------------------------------------------------------------------------

CREATE PROCEDURE [dbo].up_InsertFreeService
	@ID int OUTPUT,
	@Status varchar(1),
	@ChassisMasterID int,
	@FSKindID tinyint,
	@MileAge int,
	@ServiceDate smalldatetime,
	@ServiceDealerID smallint,
	@DealerBranchID int,
	@SoldDate smalldatetime,
	@NotificationNumber varchar(20),
	@NotificationType varchar(2),
	@TotalAmount money,
	@LabourAmount money,
	@PartAmount money,
	@PPNAmount money,
	@PPHAmount money,
	@Reject varchar(4),
	@Reason smallint,
	@ReleaseBy varchar(20),
	@ReleaseDate datetime,
	@FleetRequestID int,
	@WorkOrderNumber varchar(50),
	@RowStatus smallint,
	@CreatedBy varchar(20),
	--@CreatedTime datetime,
	@LastUpdateBy varchar(20)	--@LastUpdateTime datetime	
AS
INSERT	INTO	[dbo].[FreeService]
VALUES
(
	@Status,
	@ChassisMasterID,
	@FSKindID,
	@MileAge,
	@ServiceDate,
	@ServiceDealerID,
	@DealerBranchID,
	@SoldDate,
	@NotificationNumber,
	@NotificationType,
	@TotalAmount,
	@LabourAmount,
	@PartAmount,
	@PPNAmount,
	@PPHAmount,
	@Reject,
	@Reason,
	@ReleaseBy,
	@ReleaseDate,
	@FleetRequestID,
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
-- Date Created	: 14 Maret 2018
-- Created By	: DNet Team by using CodeSmith v 2.6
-- Rev History	:
---------------------------------------------------------------------------------------------------------------

CREATE PROCEDURE [dbo].up_RetrieveFreeService
	@ID int OUTPUT	
AS
SET TRANSACTION ISOLATION LEVEL READ COMMITTED

SET NOCOUNT ON

SELECT
	[ID],
	[Status],
	[ChassisMasterID],
	[FSKindID],
	[MileAge],
	[ServiceDate],
	[ServiceDealerID],
	[DealerBranchID],
	[SoldDate],
	[NotificationNumber],
	[NotificationType],
	[TotalAmount],
	[LabourAmount],
	[PartAmount],
	[PPNAmount],
	[PPHAmount],
	[Reject],
	[Reason],
	[ReleaseBy],
	[ReleaseDate],
	[FleetRequestID],
	[WorkOrderNumber],
	[RowStatus],
	[CreatedBy],
	[CreatedTime],
	[LastUpdateBy],
	[LastUpdateTime]	
FROM	[dbo].[FreeService]

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
-- Date Created	: 14 Maret 2018
-- Created By	: DNet Team by using CodeSmith v 2.6
-- Rev History	:
---------------------------------------------------------------------------------------------------------------

CREATE PROCEDURE [dbo].up_RetrieveFreeServiceList

AS

SET TRANSACTION ISOLATION LEVEL READ COMMITTED
SET NOCOUNT ON


SELECT
		[ID],
		[Status],
		[ChassisMasterID],
		[FSKindID],
		[MileAge],
		[ServiceDate],
		[ServiceDealerID],
		[DealerBranchID],
		[SoldDate],
		[NotificationNumber],
		[NotificationType],
		[TotalAmount],
		[LabourAmount],
		[PartAmount],
		[PPNAmount],
		[PPHAmount],
		[Reject],
		[Reason],
		[ReleaseBy],
		[ReleaseDate],
		[FleetRequestID],
		[WorkOrderNumber],
		[RowStatus],
		[CreatedBy],
		[CreatedTime],
		[LastUpdateBy],
		[LastUpdateTime]		
		FROM	
		[dbo].[FreeService] 

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
-- Date Created	: 14 Maret 2018
-- Created By	: DNet Team by using CodeSmith v 2.6
-- Rev History	:
---------------------------------------------------------------------------------------------------------------

CREATE PROCEDURE [dbo].up_UpdateFreeService
	@ID int OUTPUT,
	@Status varchar(1),
	@ChassisMasterID int,
	@FSKindID tinyint,
	@MileAge int,
	@ServiceDate smalldatetime,
	@ServiceDealerID smallint,
	@DealerBranchID int,
	@SoldDate smalldatetime,
	@NotificationNumber varchar(20),
	@NotificationType varchar(2),
	@TotalAmount money,
	@LabourAmount money,
	@PartAmount money,
	@PPNAmount money,
	@PPHAmount money,
	@Reject varchar(4),
	@Reason smallint,
	@ReleaseBy varchar(20),
	@ReleaseDate datetime,
	@FleetRequestID int,
	@WorkOrderNumber varchar(50),
	@RowStatus smallint,
	@CreatedBy varchar(20),
	--@CreatedTime datetime,
	@LastUpdateBy varchar(20)	--@LastUpdateTime datetime	
AS

UPDATE	[dbo].[FreeService]
SET
	[Status] = @Status,
	[ChassisMasterID] = @ChassisMasterID,
	[FSKindID] = @FSKindID,
	[MileAge] = @MileAge,
	[ServiceDate] = @ServiceDate,
	[ServiceDealerID] = @ServiceDealerID,
	[DealerBranchID] = @DealerBranchID,
	[SoldDate] = @SoldDate,
	[NotificationNumber] = @NotificationNumber,
	[NotificationType] = @NotificationType,
	[TotalAmount] = @TotalAmount,
	[LabourAmount] = @LabourAmount,
	[PartAmount] = @PartAmount,
	[PPNAmount] = @PPNAmount,
	[PPHAmount] = @PPHAmount,
	[Reject] = @Reject,
	[Reason] = @Reason,
	[ReleaseBy] = @ReleaseBy,
	[ReleaseDate] = @ReleaseDate,
	[FleetRequestID] = @FleetRequestID,
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
-- Date Created	: 14 Maret 2018
-- Created By	: DNet Team by using CodeSmith v 2.6
-- Rev History	:
---------------------------------------------------------------------------------------------------------------

CREATE PROCEDURE [dbo].up_DeleteFreeService
	@ID int OUTPUT	
AS

DELETE
FROM	[dbo].[FreeService]
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
-- Date Created	: 14 Maret 2018
-- Created By	: DNet Team by using CodeSmith v 2.6
-- Rev History	:
---------------------------------------------------------------------------------------------------------------

CREATE PROCEDURE [dbo].up_ValidateFreeService
	@Result	varchar(1000),
	@ID int OUTPUT,
	@Status varchar(1),
	@ChassisMasterID int,
	@FSKindID tinyint,
	@MileAge int,
	@ServiceDate smalldatetime,
	@ServiceDealerID smallint,
	@DealerBranchID smallint,
	@SoldDate smalldatetime,
	@NotificationNumber varchar(20),
	@NotificationType varchar(2),
	@TotalAmount money,
	@LabourAmount money,
	@PartAmount money,
	@PPNAmount money,
	@PPHAmount money,
	@Reject varchar(4),
	@Reason smallint,
	@ReleaseBy varchar(20),
	@ReleaseDate datetime,
	@FleetRequestID int,
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
-- Date Created	: 14 Maret 2018
-- Created By	: DNet Team by using CodeSmith v 2.6
-- Rev History	:
---------------------------------------------------------------------------------------------------------------
GRANT  EXECUTE  ON [dbo].up_InsertFreeService TO [bsi];

GRANT  EXECUTE  ON [dbo].up_UpdateFreeService TO [bsi];

GRANT  EXECUTE  ON [dbo].up_RetrieveFreeService TO [bsi];

GRANT  EXECUTE  ON [dbo].up_RetrieveFreeServiceList TO [bsi];

GRANT  EXECUTE  ON [dbo].up_ValidateFreeService TO [bsi];

GRANT  EXECUTE  ON [dbo].up_DeleteFreeService TO [bsi];

SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO



