USE [BSIDNET_MMKSI_DMS]
GO

/****** Object:  StoredProcedure [dbo].[up_UpdateEstimationEquipHeader]    Script Date: 18/03/2018 21:08:40 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


---------------------------------------------------------------------------------------------------------------
-- Date Created	: Tuesday, August 18, 2009
-- Created By	: DNet Team by using CodeSmith v 2.6
-- Rev History	:
---------------------------------------------------------------------------------------------------------------

CREATE PROCEDURE [dbo].[up_UpdateEstimationEquipHeader]
	@ID int OUTPUT,
	@EstimationNumber varchar(3),
	@DealerID smallint,
	@DepositBKewajibanHeaderID int,
	@Status smallint,
	@DMSPRNo varchar(50),
	@RowStatus smallint,
	--@CreatedBy varchar(20),
	--@CreatedTime datetime,
	@LastUpdatedBy varchar(20)
	--@LastUpdatedTime datetime
	
AS

UPDATE	[dbo].[EstimationEquipHeader]
SET
	--[EstimationNumber] = @EstimationNumber,
	[DealerID] = @DealerID,
	[DepositBKewajibanHeaderID] = @DepositBKewajibanHeaderID,
	[Status] = @Status,
	[DMSPRNo] = @DMSPRNo,
	[RowStatus] = @RowStatus,
	--[CreatedBy] = @CreatedBy,
	--[CreatedTime] = @CreatedTime,
	[LastUpdatedBy] = @LastUpdatedBy,
	[LastUpdatedTime] = GETDATE()
	
WHERE
	[ID] = @ID

GO

