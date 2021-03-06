USE [BSIDNET_MMKSI_DMS]
GO

/****** Object:  StoredProcedure [dbo].[up_InsertAssistPartSales]    Script Date: 13/03/2018 17:23:36 ******/
SET ANSI_NULLS OFF
GO

SET QUOTED_IDENTIFIER ON
GO


---------------------------------------------------------------------------------------------------------------
-- Date Created	: Wednesday, January 17, 2018
-- Created By	: DNet Team by using CodeSmith v 2.6
-- Rev History	:
---------------------------------------------------------------------------------------------------------------

CREATE PROCEDURE [dbo].[up_InsertAssistPartSales]
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
INSERT	INTO	[dbo].[AssistPartSales]
			([AssistUploadLogID]
           ,[TglTransaksi]
           ,[DealerID]
           ,[DealerCode]
           ,[KodeCustomer]
           ,[SalesChannelID]
           ,[SalesChannelCode]
           ,[TrTraineeSalesSparepartID]
           ,[SalesmanHeaderID]
           ,[KodeSalesman]
           ,[NoWorkOrder]
           ,[SparepartMasterID]
           ,[NoParts]
           ,[Qty]
           ,[HargaBeli]
           ,[HargaJual]
           ,[IsCampaign]
           ,[CampaignNo]
           ,[CampaignDescription]
           ,[DealerBranchID]
           ,[DealerBranchCode]
           ,[RemarksSystem]
           ,[StatusAktif]
           ,[ValidateSystemStatus]
           ,[RowStatus]
           ,[CreatedBy]
           ,[CreatedTime]
           ,[LastUpdateBy]
           ,[LastUpdateTime])
VALUES
(
	@AssistUploadLogID,
	@TglTransaksi,
	@DealerID,
	@DealerCode,
	@KodeCustomer,
	@SalesChannelID,
	@SalesChannelCode,
	@TrTraineeSalesSparepartID,
	@SalesmanHeaderID,
	@KodeSalesman,
	@NoWorkOrder,
	@SparepartMasterID,
	@NoParts,
	@Qty,
	@HargaBeli,
	@HargaJual,
	@IsCampaign,
	@CampaignNo,
	@CampaignDescription,
	@DealerBranchID,
	@DealerBranchCode,
	@RemarksSystem,
	@StatusAktif,
	@ValidateSystemStatus,
	@RowStatus,
	@CreatedBy,
	GETDATE(),	
	@LastUpdateBy,
	GETDATE())

	
SET @ID = @@IDENTITY



GO

