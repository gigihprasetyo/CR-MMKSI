USE [BSIDNET_MMKSI_DMS]
GO

/****** Object:  StoredProcedure [dbo].[up_InsertEstimationEquipHeader]    Script Date: 18/03/2018 21:05:05 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


---------------------------------------------------------------------------------------------------------------
-- Date Created	: Tuesday, August 18, 2009
-- Created By	: DNet Team by using CodeSmith v 2.6
-- Rev History	:
---------------------------------------------------------------------------------------------------------------

CREATE PROCEDURE [dbo].[up_InsertEstimationEquipHeader]
	@ID int OUTPUT,
	@EstimationNumber varchar(13),
	@DealerID smallint,
	@DepositBKewajibanHeaderID int,
	@Status smallint,
	@DMSPRNo varchar(50),
	@RowStatus smallint,
	@CreatedBy varchar(20),
	--@CreatedTime datetime,
	@LastUpdatedBy varchar(20)
	--@LastUpdatedTime datetime
	
AS

Declare @EstimationNumberGenerated  varchar(13)
declare @requestdate datetime
set @requestdate = getdate()
set @EstimationNumberGenerated=dbo.ufn_CreateEstimationEquipNumber(@requestdate,@DealerId)

INSERT	INTO	[dbo].[EstimationEquipHeader]
VALUES
(
	@EstimationNumberGenerated,
	@DealerID,
	@DepositBKewajibanHeaderID,
	@Status,
	@DMSPRNo,
	@RowStatus,
	@CreatedBy,
	GETDATE(),	
	@LastUpdatedBy,
	GETDATE()
)

	
SET @ID = @@IDENTITY

GO

