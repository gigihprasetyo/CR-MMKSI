USE [BSIDNET_MMKSI_DMS]
GO
/****** Object:  StoredProcedure [dbo].[up_RetrievePMHeader]    Script Date: 22/03/2018 10:14:37 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER ON
GO

---------------------------------------------------------------------------------------------------------------
-- Date Created	: Wednesday, August 01, 2007
-- Created By	: DNet Team by using CodeSmith v 2.6
-- Rev History	:
---------------------------------------------------------------------------------------------------------------

ALTER PROCEDURE [dbo].[up_RetrievePMHeader]
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
