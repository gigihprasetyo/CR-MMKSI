USE [BSIDNET_MMKSI_DMS]
GO
/****** Object:  StoredProcedure [dbo].[up_InsertPMHeader]    Script Date: 22/03/2018 10:11:39 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER ON
GO

---------------------------------------------------------------------------------------------------------------
-- Date Created	: Wednesday, August 01, 2007
-- Created By	: DNet Team by using CodeSmith v 2.6
-- Rev History	:
---------------------------------------------------------------------------------------------------------------

ALTER PROCEDURE [dbo].[up_InsertPMHeader]
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
