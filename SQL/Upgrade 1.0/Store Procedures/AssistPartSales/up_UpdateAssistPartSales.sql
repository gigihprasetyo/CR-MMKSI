USE [BSIDNET_MMKSI_DMS]
GO

/****** Object:  StoredProcedure [dbo].[up_UpdateAssistPartSales]    Script Date: 14/03/2018 8:44:21 ******/
SET ANSI_NULLS OFF
GO

SET QUOTED_IDENTIFIER ON
GO


---------------------------------------------------------------------------------------------------------------
-- Date Created	: Wednesday, January 17, 2018
-- Created By	: DNet Team by using CodeSmith v 2.6
-- Rev History	:
---------------------------------------------------------------------------------------------------------------

CREATE PROCEDURE [dbo].[up_UpdateAssistPartSales]
	@ID int OUTPUT,
	@AssistUploadLogID int,
	@TglTransaksi date,
	@DealerID int,
	@DealerCode varchar(50),
	@KodeCustomer varchar(80),
	@SalesChannelID int,
	@SalesChannelCode varchar(50),
	@TrTraineeSalesSparepartID int,
	@SalesmanHeaderID int,
	@KodeSalesman varchar(50),
	@NoWorkOrder varchar(50),
	@SparepartMasterID int,
	@NoParts varchar(50),
	@Qty float,
	@HargaBeli money,
	@HargaJual money,
	@IsCampaign bit,
	@CampaignNo varchar(20),
	@CampaignDescription varchar(100),
	@DealerBranchID int,
	@DealerBranchCode varchar(50),
	@RemarksSystem varchar(MAX),
	@StatusAktif smallint,
	@ValidateSystemStatus smallint,
	@RowStatus smallint,
	@CreatedBy varchar(50),
	--@CreatedTime datetime,
	@LastUpdateBy varchar(50)	--@LastUpdateTime datetime	
AS

UPDATE	[dbo].[AssistPartSales]
SET
	[AssistUploadLogID] = @AssistUploadLogID,
	[TglTransaksi] = @TglTransaksi,
	[DealerID] = @DealerID,
	[DealerCode] = @DealerCode,
	[KodeCustomer] = @KodeCustomer,
	[SalesChannelID] = @SalesChannelID,
	[SalesChannelCode] = @SalesChannelCode,
	[TrTraineeSalesSparepartID] = @TrTraineeSalesSparepartID,
	[SalesmanHeaderID] = @SalesmanHeaderID,
	[KodeSalesman] = @KodeSalesman,
	[NoWorkOrder] = @NoWorkOrder,
	[SparepartMasterID] = @SparepartMasterID,
	[NoParts] = @NoParts,
	[Qty] = @Qty,
	[HargaBeli] = @HargaBeli,
	[HargaJual] = @HargaJual,
	[IsCampaign] = @IsCampaign,
	[CampaignNo] = @CampaignNo,
	[CampaignDescription] = @CampaignDescription,
	[DealerBranchID] = @DealerBranchID ,
	[DealerBranchCode] = @DealerBranchCode ,
	[RemarksSystem] = @RemarksSystem,
	[StatusAktif] = @StatusAktif,
	[ValidateSystemStatus] = @ValidateSystemStatus,
	[RowStatus] = @RowStatus,
	[CreatedBy] = @CreatedBy,
	--[CreatedTime] = @CreatedTime,
	[LastUpdateBy] = @LastUpdateBy,
	[LastUpdateTime] = GETDATE()	
WHERE
	[ID] = @ID

GO

