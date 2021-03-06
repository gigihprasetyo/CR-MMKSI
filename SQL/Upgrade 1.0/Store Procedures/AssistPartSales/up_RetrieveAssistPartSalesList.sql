USE [BSIDNET_MMKSI_DMS]
GO

/****** Object:  StoredProcedure [dbo].[up_RetrieveAssistPartSalesList]    Script Date: 14/03/2018 8:24:28 ******/
SET ANSI_NULLS OFF
GO

SET QUOTED_IDENTIFIER ON
GO


---------------------------------------------------------------------------------------------------------------
-- Date Created	: Wednesday, January 17, 2018
-- Created By	: DNet Team by using CodeSmith v 2.6
-- Rev History	:
---------------------------------------------------------------------------------------------------------------

CREATE PROCEDURE [dbo].[up_RetrieveAssistPartSalesList]

AS

SET TRANSACTION ISOLATION LEVEL READ COMMITTED
SET NOCOUNT ON


SELECT
		[ID],
		[AssistUploadLogID],
		[TglTransaksi],
		[DealerID],
		[DealerCode],
		[KodeCustomer],
		[SalesChannelID],
		[SalesChannelCode],
		[TrTraineeSalesSparepartID],
		[SalesmanHeaderID],
		[KodeSalesman],
		[NoWorkOrder],
		[SparepartMasterID],
		[NoParts],
		[Qty],
		[HargaBeli],
		[HargaJual],
		[IsCampaign],
		[CampaignNo],
		[CampaignDescription],
		[DealerBranchID],
		[DealerBranchCode],
		[RemarksSystem],
		[StatusAktif],
		[ValidateSystemStatus],
		[RowStatus],
		[CreatedBy],
		[CreatedTime],
		[LastUpdateBy],
		[LastUpdateTime]		
		FROM	
		[dbo].[AssistPartSales] 

SET NOCOUNT OFF

GO

