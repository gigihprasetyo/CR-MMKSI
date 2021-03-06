USE [BSIDNET_MMKSI_DMS]
GO
/****** Object:  StoredProcedure [dbo].[up_ValidatePMHeader]    Script Date: 22/03/2018 10:17:55 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER ON
GO

---------------------------------------------------------------------------------------------------------------
-- Date Created	: Wednesday, August 01, 2007
-- Created By	: DNet Team by using CodeSmith v 2.6
-- Rev History	:
---------------------------------------------------------------------------------------------------------------

ALTER PROCEDURE [dbo].[up_ValidatePMHeader]
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
