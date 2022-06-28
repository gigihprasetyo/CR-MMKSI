USE [BSIDNET_MMKSI_DMS]
GO
/****** Object:  StoredProcedure [dbo].[up_UpdateSPKHeader]    Script Date: 02/03/2018 10:36:47 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER ON
GO

---------------------------------------------------------------------------------------------------------------
-- Date Created	: Monday, March 07, 2011
-- Created By	: DNet Team by using CodeSmith v 2.6
-- Rev History	:
---------------------------------------------------------------------------------------------------------------

CREATE PROCEDURE [dbo].[up_UpdateSPKHeader]
	@ID int OUTPUT,
	@DealerID smallint,
	@Status varchar(2),
	@SPKNumber varchar(15),
	@DealerSPKNumber varchar(15),
	@IndentNumber varchar(10),
	@PlanDeliveryMonth tinyint,
	@PlanDeliveryYear smallint,
	@PlanDeliveryDate datetime,
	@PlanInvoiceMonth tinyint,
	@PlanInvoiceYear smallint,
	@PlanInvoiceDate datetime,
	@CustomerRequestID int,
	@SPKCustomerID int,
	@ValidateTime datetime,
	@ValidateBy nvarchar(20),
	@RejectedReason nvarchar(255),
	@SalesmanHeaderID smallint,
	@EvidenceFile nvarchar(255),
	@ValidationKey nvarchar(20),
	@FlagUpdate smallint,
	@DealerBranchID int,
	@IsSend smallint,
	@DealerSPKDate datetime,
	@BenefitMasterHeaderID int,
	@RowStatus smallint,
	--@CreatedTime datetime,
	@CreatedBy nvarchar(20),
	--@LastUpdateTime datetime,
	@LastUpdateBy nvarchar(20)
	
AS
--IF @Status = 7 
--	BEGIN
--		UPDATE	[dbo].[SPKHeader]
--		SET
--			[DealerID] = @DealerID,
--			[Status] = @Status,
--			[SPKNumber] = @SPKNumber,
--			[DealerSPKNumber] = @DealerSPKNumber,
--			[IndentNumber] = dbo.[ufn_CreateSPKIndentNumber](GETDATE()),
--			[PlanDeliveryMonth] = @PlanDeliveryMonth,
--			[PlanDeliveryYear] = @PlanDeliveryYear,
--			[PlanDeliveryDate] = @PlanDeliveryDate,
--			[PlanInvoiceMonth] = @PlanInvoiceMonth,
--			[PlanInvoiceYear] = @PlanInvoiceYear,
--			[PlanInvoiceDate] = @PlanInvoiceDate,
--			[CustomerRequestID] = @CustomerRequestID,
--			[SPKCustomerID] = @SPKCustomerID,
--			[ValidateTime] = @ValidateTime,
--			[ValidateBy] = @ValidateBy,
--			[RejectedReason] = @RejectedReason,
--			[SalesmanHeaderID] = @SalesmanHeaderID,
--			[EvidenceFile] = @EvidenceFile,
--			[ValidationKey] = @ValidationKey,
--			[FlagUpdate]= @FlagUpdate ,
--			[DealerBranchID] = @DealerBranchID,
--			[IsSend] = @IsSend,
--			[DealerSPKDate] = @DealerSPKDate,
--			[BenefitMasterHeaderID] = @BenefitMasterHeaderID,
--			[RowStatus] = @RowStatus,
--			--[CreatedTime] = @CreatedTime,
--			[CreatedBy] = @CreatedBy,
--			[LastUpdateTime] = GETDATE(),
--			[LastUpdateBy] = @LastUpdateBy	
--		WHERE
--			[ID] = @ID
--	END 
--ELSE
	BEGIN
		UPDATE	[dbo].[SPKHeader]
		SET
			[DealerID] = @DealerID,
			[Status] = @Status,
			[SPKNumber] = @SPKNumber,
			[DealerSPKNumber] = @DealerSPKNumber,
			[IndentNumber] = @IndentNumber,
			[PlanDeliveryMonth] = @PlanDeliveryMonth,
			[PlanDeliveryYear] = @PlanDeliveryYear,
			[PlanDeliveryDate] = @PlanDeliveryDate,
			[PlanInvoiceMonth] = @PlanInvoiceMonth,
			[PlanInvoiceYear] = @PlanInvoiceYear,
			[PlanInvoiceDate] = @PlanInvoiceDate,
			[CustomerRequestID] = @CustomerRequestID,
			[SPKCustomerID] = @SPKCustomerID,
			[ValidateTime] = @ValidateTime,
			[ValidateBy] = @ValidateBy,
			[RejectedReason] = @RejectedReason,
			[SalesmanHeaderID] = @SalesmanHeaderID,
			[EvidenceFile] = @EvidenceFile,
			[ValidationKey] = @ValidationKey,
			[FlagUpdate]= @FlagUpdate ,
			[DealerBranchID] = @DealerBranchID,
			[IsSend] = @IsSend,
			[DealerSPKDate] = @DealerSPKDate,
			[BenefitMasterHeaderID] = @BenefitMasterHeaderID,
			[RowStatus] = @RowStatus,
			--[CreatedTime] = @CreatedTime,
			[CreatedBy] = @CreatedBy,
			[LastUpdateTime] = GETDATE(),
			[LastUpdateBy] = @LastUpdateBy	
		WHERE
			[ID] = @ID
	END
