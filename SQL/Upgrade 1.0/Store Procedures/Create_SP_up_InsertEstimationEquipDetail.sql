USE [BSIDNET_MMKSI_DMS]
GO

/****** Object:  StoredProcedure [dbo].[up_InsertEstimationEquipDetail]    Script Date: 05/03/2018 13:30:58 ******/
SET ANSI_NULLS OFF
GO

SET QUOTED_IDENTIFIER ON
GO



---------------------------------------------------------------------------------------------------------------
-- Date Created	: Thursday, December 03, 2009
-- Created By	: DNet Team by using CodeSmith v 2.6
-- Rev History	:
---------------------------------------------------------------------------------------------------------------

CREATE PROCEDURE [dbo].[up_InsertEstimationEquipDetail]
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
INSERT	INTO	[dbo].[EstimationEquipDetail]
VALUES
(
	@EstimationEquipHeaderID,
	@SparePartMasterID,
	@Harga,
	@Discount,
	@TotalForecast,
	@EstimationUnit,
	@Status,
	@ConfirmedDate,
	@Remark,
	@RowStatus,
	@CreatedBy,
	GETDATE(),	
	@LastUpdatedBy,
	GETDATE())--@LastUpdatedTime)

	
SET @ID = @@IDENTITY


GO

