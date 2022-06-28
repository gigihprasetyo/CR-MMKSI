USE [BSIDNET_MMKSI_DMS]
GO
/****** Object:  StoredProcedure [dbo].[up_InsertSPKHeader]    Script Date: 02/03/2018 9:59:04 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER ON
GO

---------------------------------------------------------------------------------------------------------------
-- Date Created	: Monday, March 07, 2011
-- Created By	: DNet Team by using CodeSmith v 2.6
-- Rev History	:
---------------------------------------------------------------------------------------------------------------

CREATE PROCEDURE [dbo].[up_InsertSPKHeader]
	@ID int OUTPUT,
	@DealerID smallint,
	@Status varchar(2),
	@SPKNumber varchar(15),
	@IndentNumber varchar(10),
	@DealerSPKNumber varchar(15),
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
INSERT	INTO	[dbo].[SPKHeader]
(
	DealerID,
	[Status],
	SPKNumber,
	DealerSPKNumber,
	IndentNumber,
	PlanDeliveryMonth,
	PlanDeliveryYear,
	PlanDeliveryDate,
	PlanInvoiceMonth,
	PlanInvoiceYear,
	PlanInvoiceDate,
	CustomerRequestID,
	SPKCustomerID,
	ValidateTime,
	ValidateBy,
	RejectedReason,
	SalesmanHeaderID,
	EvidenceFile,
	ValidationKey,
	FlagUpdate,
	DealerBranchID,
	IsSend,
	DealerSPKDate,
	BenefitMasterHeaderID,
	RowStatus,
	CreatedTime,	
	CreatedBy,
	LastUpdateTime,	
	LastUpdateBy
)
VALUES
(
	@DealerID,
	@Status,
	dbo.ufn_CreateSPKNumber (GETDATE()),--@SPKNumber,
	@DealerSPKNumber,
	NULL,--dbo.ufn_CreateSPKIndentNumber (GETDATE()),--@IndentNumber,
	@PlanDeliveryMonth,
	@PlanDeliveryYear,
	@PlanDeliveryDate,
	@PlanInvoiceMonth,
	@PlanInvoiceYear,
	@PlanInvoiceDate,
	@CustomerRequestID,
	@SPKCustomerID,
	@ValidateTime,
	@ValidateBy,
	@RejectedReason,
	@SalesmanHeaderID,
	@EvidenceFile,
	@ValidationKey,
	@FlagUpdate,
	@DealerBranchID,
	@IsSend,
	@DealerSPKDate,
	@BenefitMasterHeaderID,
	@RowStatus,
	GETDATE(),	
	@CreatedBy,
	GETDATE(),	
	@LastUpdateBy
	)

	
SET @ID = @@IDENTITY

