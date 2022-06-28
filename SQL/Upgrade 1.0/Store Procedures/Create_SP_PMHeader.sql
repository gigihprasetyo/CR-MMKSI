USE [BSIDNET_MMKSI_DMS]
GO

/****** Object:  StoredProcedure [dbo].[up_InsertPMHeader]    Script Date: 14/03/2018 10:05:30 ******/
SET ANSI_NULLS OFF
GO

SET QUOTED_IDENTIFIER ON
GO


---------------------------------------------------------------------------------------------------------------
-- Date Created	: Wednesday, August 01, 2007
-- Created By	: DNet Team by using CodeSmith v 2.6
-- Rev History	:
---------------------------------------------------------------------------------------------------------------

CREATE PROCEDURE [dbo].[up_InsertPMHeader]
	@ID int OUTPUT,
	@DealerID int,
	@DealerBranchID int,
	@ChassisNumberID int,
	@StandKM int,
	@ServiceDate datetime,
	@ReleaseDate datetime,
	@PMStatus varchar(4),
	@EntryType varchar(20),
	@WorkOrderNumber varchar(50),
	@BookingNo varchar(5),
	@VisitType varchar(5),
	@Remarks varchar(250),
	@PMKindID int,
	@RowStatus smallint,
	@CreatedBy varchar(20),
	--@CreatedTime datetime,
	@LastUpdateBy varchar(20)
	--@LastUpdateTime datetime
	
AS
INSERT	INTO	[dbo].[PMHeader]
VALUES
(
	@DealerID,
	@DealerBranchID,
	@ChassisNumberID,
	@StandKM,
	@ServiceDate,
	@ReleaseDate,
	@PMStatus,
	@EntryType,
	@WorkOrderNumber,
	@BookingNo,
	@VisitType,
	@Remarks,
	@PMKindID,
	@RowStatus,
	@CreatedBy,
	GETDATE(),	
	@LastUpdateBy,
	GETDATE()
)

	
SET @ID = @@IDENTITY

GO


---------------------------------------------------------------------------------------------------------------


USE [BSIDNET_MMKSI_DMS]
GO

/****** Object:  StoredProcedure [dbo].[up_RetrievePMHeader]    Script Date: 14/03/2018 10:07:41 ******/
SET ANSI_NULLS OFF
GO

SET QUOTED_IDENTIFIER ON
GO


---------------------------------------------------------------------------------------------------------------
-- Date Created	: Wednesday, August 01, 2007
-- Created By	: DNet Team by using CodeSmith v 2.6
-- Rev History	:
---------------------------------------------------------------------------------------------------------------

CREATE PROCEDURE [dbo].[up_RetrievePMHeader]
	@ID int OUTPUT
	
AS
SET TRANSACTION ISOLATION LEVEL READ COMMITTED

SET NOCOUNT ON

SELECT
	[ID],
	[DealerID],
	[DealerBranchID],
	[ChassisNumberID],
	[StandKM],
	[ServiceDate],
	[ReleaseDate],
	[PMStatus],
	[EntryType],
	[WorkOrderNumber],
	[BookingNo],
	[VisitType],
	[Remarks],
	[PMKindID],
	[RowStatus],
	[CreatedBy],
	[CreatedTime],
	[LastUpdateBy],
	[LastUpdateTime]
	
FROM	[dbo].[PMHeader]

WHERE
	[ID] = @ID

SET NOCOUNT OFF

GO


---------------------------------------------------------------------------------------------------------------


USE [BSIDNET_MMKSI_DMS]
GO

/****** Object:  StoredProcedure [dbo].[up_RetrievePMHeaderList]    Script Date: 14/03/2018 10:10:30 ******/
SET ANSI_NULLS OFF
GO

SET QUOTED_IDENTIFIER ON
GO


---------------------------------------------------------------------------------------------------------------
-- Date Created	: Wednesday, August 01, 2007
-- Created By	: DNet Team by using CodeSmith v 2.6
-- Rev History	:
---------------------------------------------------------------------------------------------------------------

CREATE PROCEDURE [dbo].[up_RetrievePMHeaderList]

AS

SET TRANSACTION ISOLATION LEVEL READ COMMITTED
SET NOCOUNT ON

SELECT
		[ID],
		[DealerID],
		[DealerBranchID],
		[ChassisNumberID],
		[StandKM],
		[ServiceDate],
		[ReleaseDate],
		[PMStatus],
		[EntryType],
		[WorkOrderNumber],
		[BookingNo],
		[VisitType],
		[Remarks],
		[PMKindID],
		[RowStatus],
		[CreatedBy],
		[CreatedTime],
		[LastUpdateBy],
		[LastUpdateTime]
		
		FROM	
		[dbo].[PMHeader] 

SET NOCOUNT OFF

GO


---------------------------------------------------------------------------------------------------------------


USE [BSIDNET_MMKSI_DMS]
GO

/****** Object:  StoredProcedure [dbo].[up_UpdatePMHeader]    Script Date: 14/03/2018 10:14:26 ******/
SET ANSI_NULLS OFF
GO

SET QUOTED_IDENTIFIER ON
GO


---------------------------------------------------------------------------------------------------------------
-- Date Created	: Wednesday, August 01, 2007
-- Created By	: DNet Team by using CodeSmith v 2.6
-- Rev History	:
---------------------------------------------------------------------------------------------------------------

CREATE PROCEDURE [dbo].[up_UpdatePMHeader]
	@ID int OUTPUT,
	@DealerID int,
	@DealerBranchID int,
	@ChassisNumberID int,
	@StandKM int,
	@ServiceDate datetime,
	@ReleaseDate datetime,
	@PMStatus varchar(4),
	@EntryType varchar(20),
	@WorkOrderNumber varchar(50),
	@BookingNo varchar(5),
	@VisitType varchar(5),
	@Remarks varchar(250),
	@PMKindID int,
	@RowStatus smallint,
	@CreatedBy varchar(20),
	--@CreatedTime datetime,
	@LastUpdateBy varchar(20)
	--@LastUpdateTime datetime
	
AS

UPDATE	[dbo].[PMHeader]
SET
	[DealerID] = @DealerID,
	[DealerBranchID] = @DealerBranchID,
	[ChassisNumberID] = @ChassisNumberID,
	[StandKM] = @StandKM,
	[ServiceDate] = @ServiceDate,
	[ReleaseDate] = @ReleaseDate,
	[PMStatus] = @PMStatus,
	[EntryType] = @EntryType,
	[WorkOrderNumber] = @WorkOrderNumber,
	[BookingNo] = @BookingNo,
	[VisitType] = @VisitType,
	[Remarks] = @Remarks,
	[PMKindID] = @PMKindID,
	[RowStatus] = @RowStatus,
	[CreatedBy] = @CreatedBy,
	--[CreatedTime] = @CreatedTime,
	[LastUpdateBy] = @LastUpdateBy,
	[LastUpdateTime] = GETDATE()
	
WHERE
	[ID] = @ID

GO


---------------------------------------------------------------------------------------------------------------


USE [BSIDNET_MMKSI_DMS]
GO

/****** Object:  StoredProcedure [dbo].[up_ValidatePMHeader]    Script Date: 14/03/2018 10:15:35 ******/
SET ANSI_NULLS OFF
GO

SET QUOTED_IDENTIFIER ON
GO


---------------------------------------------------------------------------------------------------------------
-- Date Created	: Wednesday, August 01, 2007
-- Created By	: DNet Team by using CodeSmith v 2.6
-- Rev History	:
---------------------------------------------------------------------------------------------------------------

CREATE PROCEDURE [dbo].[up_ValidatePMHeader]
	@Result	varchar(1000),
	@ID int OUTPUT,
	@DealerID int,
	@DealerBranchID int,
	@ChassisNumberID int,
	@StandKM int,
	@ServiceDate datetime,
	@ReleaseDate datetime,
	@PMStatus varchar(4),
	@EntryType varchar(20),
	@WorkOrderNumber varchar(50),
	@BookingNo varchar(5),
	@VisitType varchar(5),
	@Remarks varchar(250),
	@PMKindID int,
	@RowStatus smallint,
	@CreatedBy varchar(20),
	@CreatedTime datetime,
	@LastUpdateBy varchar(20),
	@LastUpdateTime datetime
	
AS

SET	@Result = ''

GO


