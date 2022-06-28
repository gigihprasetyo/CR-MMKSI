USE [BSIDNET_MMKSI_DMS]
GO

/****** Object:  StoredProcedure [dbo].[up_UpdateEstimationEquipDetail]    Script Date: 05/03/2018 13:49:56 ******/
SET ANSI_NULLS OFF
GO

SET QUOTED_IDENTIFIER ON
GO


---------------------------------------------------------------------------------------------------------------
-- Date Created	: Thursday, December 03, 2009
-- Created By	: DNet Team by using CodeSmith v 2.6
-- Rev History	:
---------------------------------------------------------------------------------------------------------------

CREATE PROCEDURE [dbo].[up_UpdateEstimationEquipDetail]
	@ID int OUTPUT,
	@EstimationEquipHeaderID int,
	@SparePartMasterID int,
	@Harga decimal(19, 9),
	@Discount decimal(7, 5),
	@TotalForecast int,
	@EstimationUnit int,
	@Status smallint,
	@ConfirmedDate datetime,
	@Remark varchar(500),
	@RowStatus smallint,
	@CreatedBy varchar(20),
	--@CreatedTime datetime,
	@LastUpdatedBy varchar(20)--	@LastUpdatedTime datetime,
	
AS
UPDATE	[dbo].[EstimationEquipDetail]
SET
	[EstimationEquipHeaderID] = @EstimationEquipHeaderID,
	[SparePartMasterID] = @SparePartMasterID,
	[Harga] = @Harga,
	[Discount] = @Discount,
	[TotalForecast] = @TotalForecast,
	[EstimationUnit] = @EstimationUnit,
	[Status] = @Status,
	[ConfirmedDate] = @ConfirmedDate,
	[Remark] = @Remark,
	[RowStatus] = @RowStatus,
	[CreatedBy] = @CreatedBy,
	--[CreatedTime] = @CreatedTime,
	[LastUpdatedBy] = @LastUpdatedBy,
	[LastUpdatedTime] =GETDATE()-- @LastUpdatedTime	
WHERE
	[ID] = @ID

GO

