USE [BSIDNET_MMKSI_DMS]
GO
/****** Object:  StoredProcedure [dbo].[up_RetrieveSPKHeaderList]    Script Date: 02/03/2018 10:34:00 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER ON
GO

---------------------------------------------------------------------------------------------------------------
-- Date Created	: Monday, March 07, 2011
-- Created By	: DNet Team by using CodeSmith v 2.6
-- Rev History	:
---------------------------------------------------------------------------------------------------------------

CREATE PROCEDURE [dbo].[up_RetrieveSPKHeaderList]

AS

SET TRANSACTION ISOLATION LEVEL READ COMMITTED
SET NOCOUNT ON


SELECT
		[ID],
		[DealerID],
		[Status],
		[SPKNumber],
		[DealerSPKNumber],
		[IndentNumber],
		[PlanDeliveryMonth],
		[PlanDeliveryYear],
		[PlanDeliveryDate],
		[PlanInvoiceMonth],
		[PlanInvoiceYear],
		[PlanInvoiceDate],
		[CustomerRequestID],
		[SPKCustomerID],
		[ValidateTime],
		[ValidateBy],
		[RejectedReason],
		[SalesmanHeaderID],
		[EvidenceFile],
		[ValidationKey],
		[FlagUpdate],
		[DealerBranchID],
		[IsSend],
		[DealerSPKDate],
		[BenefitMasterHeaderID],
		[RowStatus],
		[CreatedTime],
		[CreatedBy],
		[LastUpdateTime],
		[LastUpdateBy]		
		FROM	
		[dbo].[SPKHeader] 

SET NOCOUNT OFF
