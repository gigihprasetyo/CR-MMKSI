USE [BSIDNET_MMKSI_DMS]
GO

/****** Object:  StoredProcedure [dbo].[up_RetrieveAssistPartStockList]    Script Date: 13/03/2018 17:29:08 ******/
SET ANSI_NULLS OFF
GO

SET QUOTED_IDENTIFIER ON
GO


---------------------------------------------------------------------------------------------------------------
-- Date Created	: Wednesday, January 17, 2018
-- Created By	: DNet Team by using CodeSmith v 2.6
-- Rev History	:
---------------------------------------------------------------------------------------------------------------

CREATE PROCEDURE [dbo].[up_RetrieveAssistPartStockList]

AS

SET TRANSACTION ISOLATION LEVEL READ COMMITTED
SET NOCOUNT ON


SELECT
		[AssistPartStock].[ID],
		[AssistPartStock].[AssistUploadLogID],
		[AssistPartStock].[Month],
		[AssistPartStock].[Year],
		[AssistPartStock].[DealerID],
		[AssistPartStock].[DealerCode],
		[AssistPartStock].[DealerBranchID],
		[AssistPartStock].[DealerBranchCode],
		[AssistPartStock].[SparepartMasterID],
		[AssistPartStock].[NoParts],
		[AssistPartStock].[JumlahStokAwal],
		[AssistPartStock].[JumlahDatang],
		[AssistPartStock].[HargaBeli],
		[AssistPartStock].[RemarksSystem],
		[AssistPartStock].[StatusAktif],
		[AssistPartStock].[ValidateSystemStatus],
		[AssistPartStock].[RowStatus],
		[AssistPartStock].[CreatedBy],
		[AssistPartStock].[CreatedTime],
		[AssistPartStock].[LastUpdateBy],
		[AssistPartStock].[LastUpdateTime]
		FROM	
		[dbo].[AssistPartStock] 
		
SET NOCOUNT OFF

GO


