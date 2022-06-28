set xact_abort on
go

begin transaction
go

set ANSI_NULLS on
go

---------------------------------------------------------------------------------------------------------------
-- Date Created	: Wednesday, September 05, 2018
-- Created By	: DNet Team by using CodeSmith v 2.6
-- Rev History	:
---------------------------------------------------------------------------------------------------------------
alter PROCEDURE up_InsertRevisionPaymentHeader
	@ID int OUTPUT,
	@DealerID int,
	@PaymentType varchar(3),
	@RegNumber varchar(15),
	@RevisionPaymentDocID int,
	@SlipNumber varchar(20),
	@TotalAmount money,
	@Status smallint,
	@EvidencePath varchar(150),
	@ActualPaymentDate datetime,
	@ActualPaymentAmount money,
	@AccDocNumber varchar(30),
	@GyroDate datetime,
	@RowStatus smallint,
	@CreatedBy varchar(20),
	--@CreatedTime datetime,
	@LastUpdateBy varchar(20)	--@LastUpdateTime datetime	
AS

--CREATE Autonumber Nomor Pembayaran
DECLARE @SeqNum int, @DealerCode as char(5)

SELECT @DealerCode=RIGHT(RTRIM(DealerCode),5) FROM Dealer WHERE ID=@DealerID

SELECT @SeqNum = MAX(RIGHT(RegNumber,4))
FROM RevisionPaymentHeader with (nolock)
WHERE '20'+SUBSTRING(RegNumber,7,2) = Year(getdate())

SET @SeqNum = ISNULL(@SeqNum,0)+1
SELECT @RegNumber='3' + @DealerCode + RIGHT(CONVERT(CHAR(4),Year(getdate())),2) + REPLICATE('0',4-LEN(@SeqNum)) + CONVERT(VARCHAR(4), @SeqNum)

INSERT	INTO	[dbo].[RevisionPaymentHeader]
VALUES
(
	@DealerID,
	@PaymentType,
	@RegNumber,
	@RevisionPaymentDocID,
	@SlipNumber,
	@TotalAmount,
	@Status,
	@EvidencePath,
	@ActualPaymentDate,
	@ActualPaymentAmount,
	@AccDocNumber,
	@GyroDate,
	@RowStatus,
	@CreatedBy,
	GETDATE(),	
	@LastUpdateBy,
	GETDATE())	
SET @ID = @@IDENTITY
go

---------------------------------------------------------------------------------------------------------------
-- Date Created	: Thursday, September 29, 2005
-- Created By	: DNet Team by using CodeSmith v 2.6
-- Rev History	:
---------------------------------------------------------------------------------------------------------------

alter PROCEDURE up_RetrieveArea1
	@ID int OUTPUT
AS

SET TRANSACTION ISOLATION LEVEL READ COMMITTED

SET NOCOUNT ON

SELECT
	[ID],
	[AreaCode],
	[Description],
	[PICSales],
	[PICServices],
	[PICSpareparts],
	[MainAreaID],
	[RowStatus],
	[CreatedBy],
	[CreatedTime],
	[LastUpdateBy],
	[LastUpdateTime]
FROM	[dbo].[Area1]
WHERE
	[ID] = @ID

SET NOCOUNT OFF
go

---------------------------------------------------------------------------------------------------------------
-- Date Created	: Thursday, September 29, 2005
-- Created By	: DNet Team by using CodeSmith v 2.6
-- Rev History	:
---------------------------------------------------------------------------------------------------------------

alter PROCEDURE up_RetrieveArea1List

AS

SET TRANSACTION ISOLATION LEVEL READ COMMITTED
SET NOCOUNT ON

SELECT
		[ID],
		[AreaCode],
		[Description],
		[PICSales],
		[PICServices],
		[PICSpareparts],
		[MainAreaID],
		[RowStatus],
		[CreatedBy],
		[CreatedTime],
		[LastUpdateBy],
		[LastUpdateTime]
		FROM	
		[dbo].[Area1] 

SET NOCOUNT OFF
go

set ANSI_NULLS off
go

--/****** Object:  Stored Procedure dbo.up_RetrieveArea2    Script Date: 14/10/2005 11:06:16 ******/
---------------------------------------------------------------------------------------------------------------
-- Date Created	: Thursday, September 29, 2005
-- Created By	: DNet Team by using CodeSmith v 2.6
-- Rev History	:
---------------------------------------------------------------------------------------------------------------

alter PROCEDURE up_RetrieveArea2
	@ID int OUTPUT
AS

SET TRANSACTION ISOLATION LEVEL READ COMMITTED

SET NOCOUNT ON

SELECT
	[ID],
	[AreaCode],
	[Description],
	[ACFinishUnit],
	[ACSparePart],
	[ACService],
	[Area1ID],
	[RowStatus],
	[CreatedBy],
	[CreatedTime],
	[LastUpdateBy],
	[LastUpdateTime]
FROM	[dbo].[Area2]
WHERE
	[ID] = @ID

SET NOCOUNT OFF
go

--/****** Object:  Stored Procedure dbo.up_RetrieveArea2List    Script Date: 14/10/2005 11:06:16 ******/
---------------------------------------------------------------------------------------------------------------
-- Date Created	: Thursday, September 29, 2005
-- Created By	: DNet Team by using CodeSmith v 2.6
-- Rev History	:
---------------------------------------------------------------------------------------------------------------

alter PROCEDURE up_RetrieveArea2List

AS

SET TRANSACTION ISOLATION LEVEL READ COMMITTED
SET NOCOUNT ON

SELECT
		[ID],
	[AreaCode],
	[Description],
	[ACFinishUnit],
	[ACSparePart],
	[ACService],
	[Area1ID],
	[RowStatus],
	[CreatedBy],
	[CreatedTime],
	[LastUpdateBy],
	[LastUpdateTime]
		FROM	
		[dbo].[Area2] 

SET NOCOUNT OFF
go

set ANSI_NULLS on
go

---------------------------------------------------------------------------------------------------------------
-- Date Created	: Wednesday, January 17, 2018
-- Created By	: DNet Team by using CodeSmith v 2.6
-- Rev History	:
---------------------------------------------------------------------------------------------------------------

alter PROCEDURE up_RetrieveAssistPartSales
	@ID int OUTPUT	
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
FROM	[dbo].[AssistPartSales]

WHERE
	[ID] = @ID

SET NOCOUNT OFF
go

---------------------------------------------------------------------------------------------------------------
-- Date Created	: Wednesday, January 17, 2018
-- Created By	: DNet Team by using CodeSmith v 2.6
-- Rev History	:
---------------------------------------------------------------------------------------------------------------

alter PROCEDURE up_RetrieveAssistPartSalesList

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
go

---------------------------------------------------------------------------------------------------------------
-- Date Created	: Wednesday, January 17, 2018
-- Created By	: DNet Team by using CodeSmith v 2.6
-- Rev History	:
---------------------------------------------------------------------------------------------------------------

alter PROCEDURE up_RetrieveAssistPartStock
	@ID int OUTPUT	
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
	[AssistPartStock].[SparepartMasterID],
	[AssistPartStock].[NoParts],
	[AssistPartStock].[JumlahStokAwal],
	[AssistPartStock].[JumlahDatang],
	[AssistPartStock].[HargaBeli],
	[AssistPartStock].[DealerBranchID] ,
	[AssistPartStock].[DealerBranchCode],
	[AssistPartStock].[RemarksSystem],
	[AssistPartStock].[StatusAktif],
	[AssistPartStock].[ValidateSystemStatus],
	[AssistPartStock].[RowStatus],
	[AssistPartStock].[CreatedBy],
	[AssistPartStock].[CreatedTime],
	[AssistPartStock].[LastUpdateBy],
	[AssistPartStock].[LastUpdateTime]
FROM	[dbo].[AssistPartStock]

WHERE
	[AssistPartStock].[ID] = @ID

SET NOCOUNT OFF
go

---------------------------------------------------------------------------------------------------------------
-- Date Created	: Wednesday, January 17, 2018
-- Created By	: DNet Team by using CodeSmith v 2.6
-- Rev History	:
---------------------------------------------------------------------------------------------------------------

alter PROCEDURE up_RetrieveAssistPartStockList

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
		[AssistPartStock].[SparepartMasterID],
		[AssistPartStock].[NoParts],
		[AssistPartStock].[JumlahStokAwal],
		[AssistPartStock].[JumlahDatang],
		[AssistPartStock].[HargaBeli],
		[AssistPartStock].[DealerBranchID] ,
		[AssistPartStock].[DealerBranchCode],
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
go

---------------------------------------------------------------------------------------------------------------
-- Date Created	: Wednesday, January 17, 2018
-- Created By	: DNet Team by using CodeSmith v 2.6
-- Rev History	:
---------------------------------------------------------------------------------------------------------------

alter PROCEDURE up_RetrieveAssistServiceIncoming
	@ID int OUTPUT	
AS
SET TRANSACTION ISOLATION LEVEL READ COMMITTED

SET NOCOUNT ON

SELECT
	[ID],
	[AssistUploadLogID],
	[TglBukaTransaksi],
	[WaktuMasuk],
	[TglTutupTransaksi],
	[WaktuKeluar],
	[DealerID],
	[DealerCode],
	[TrTraineMekanikID],
	[KodeMekanik],
	[NoWorkOrder],
	[ChassisMasterID],
	[KodeChassis],
	[WorkOrderCategoryID],
	[WorkOrderCategoryCode],
	[KMService],
	[ServicePlaceID],
	[ServicePlaceCode],
	[ServiceTypeID],
	[ServiceTypeCode],
	[TotalLC],
	[MetodePembayaran],
	[Model],
	[Transmition],
	[DriveSystem],
	[DealerBranchID],
    [DealerBranchCode],
	[RemarksSystem],
	[RemarksSpecial],
	[RemarksBM],
	[WOStatus],
	[StatusAktif],
	[ValidateSystemStatus],
	[RowStatus],
	[CreatedBy],
	[CreatedTime],
	[LastUpdateBy],
	[LastUpdateTime]	
FROM	[dbo].[AssistServiceIncoming]

WHERE
	[ID] = @ID

SET NOCOUNT OFF
go

---------------------------------------------------------------------------------------------------------------
-- Date Created	: Wednesday, January 17, 2018
-- Created By	: DNet Team by using CodeSmith v 2.6
-- Rev History	:
---------------------------------------------------------------------------------------------------------------

alter PROCEDURE up_RetrieveAssistServiceIncomingList

AS

SET TRANSACTION ISOLATION LEVEL READ COMMITTED
SET NOCOUNT ON


SELECT
		[ID],
		[AssistUploadLogID],
		[TglBukaTransaksi],
		[WaktuMasuk],
		[TglTutupTransaksi],
		[WaktuKeluar],
		[DealerID],
		[DealerCode],
		[TrTraineMekanikID],
		[KodeMekanik],
		[NoWorkOrder],
		[ChassisMasterID],
		[KodeChassis],
		[WorkOrderCategoryID],
		[WorkOrderCategoryCode],
		[KMService],
		[ServicePlaceID],
		[ServicePlaceCode],
		[ServiceTypeID],
		[ServiceTypeCode],
		[TotalLC],
		[MetodePembayaran],
		[Model],
		[Transmition],
		[DriveSystem],
		[DealerBranchID],
		[DealerBranchCode],
		[RemarksSystem],
		[RemarksSpecial],
		[RemarksBM],
		[WOStatus],
		[StatusAktif],
		[ValidateSystemStatus],
		[RowStatus],
		[CreatedBy],
		[CreatedTime],
		[LastUpdateBy],
		[LastUpdateTime]		
		FROM	
		[dbo].[AssistServiceIncoming] 

SET NOCOUNT OFF
go

commit
go



set xact_abort on
go

begin transaction
go

set ANSI_NULLS on
go

---------------------------------------------------------------------------------------------------------------
-- Date Created	: Friday, June 24, 2011
-- Created By	: DNet Team by using CodeSmith v 2.6
-- Rev History	:
---------------------------------------------------------------------------------------------------------------

alter PROCEDURE up_RetrieveCityPart
	@ID int OUTPUT	
AS
SET TRANSACTION ISOLATION LEVEL READ COMMITTED

SET NOCOUNT ON

SELECT
	[ID],
	[ProvinceID],
	[CityID],
	[CityName],
	[CityCode],
	[RowStatus],
	[CreatedBy],
	[CreatedTime],
	[LastUpdateBy],
	[LastUpdateTime]	
FROM	[dbo].[CityPart]

WHERE
	[ID] = @ID

SET NOCOUNT OFF
go

---------------------------------------------------------------------------------------------------------------
-- Date Created	: Friday, June 24, 2011
-- Created By	: DNet Team by using CodeSmith v 2.6
-- Rev History	:
---------------------------------------------------------------------------------------------------------------

alter PROCEDURE up_RetrieveCityPartList

AS

SET TRANSACTION ISOLATION LEVEL READ COMMITTED
SET NOCOUNT ON


SELECT
		[ID],
		[ProvinceID],
		[CityID],
		[CityName],
		[CityCode],
		[RowStatus],
		[CreatedBy],
		[CreatedTime],
		[LastUpdateBy],
		[LastUpdateTime]		
		FROM	
		[dbo].[CityPart] 

SET NOCOUNT OFF
go

---------------------------------------------------------------------------------------------------------------
-- Date Created	: Thursday, August 03, 2017
-- Created By	: DNet Team by using CodeSmith v 2.6
-- Rev History	:
---------------------------------------------------------------------------------------------------------------

alter PROCEDURE up_RetrieveCustomerCase
	@ID int OUTPUT	
AS
SET TRANSACTION ISOLATION LEVEL READ COMMITTED

SET NOCOUNT ON

SELECT
	[ID],
	[DealerID],
	[SalesforceID],
	[CaseNumber],
	[CustomerName],
	[Phone],
	[Email],
	[Category],
	[SubCategory1],
	[SubCategory2],
	[SubCategory3],
	[SubCategory4],
	[CallerType],
	[CarType],
	[Variant],
	[EngineNumber],
	[ChassisNumber],
	[Odometer],
	[PlateNumber],
	[Priority],
	[CaseNumberReff],
	[CaseDate],
	[Subject],
	[Description],
	[Status],
	[ReservationNumber],
	[RowStatus],
	[CreatedBy],
	[CreatedTime],
	[LastUpdateBy],
	[LastUpdateTIme]	
FROM	[dbo].[CustomerCase]

WHERE
	[ID] = @ID

SET NOCOUNT OFF
go

---------------------------------------------------------------------------------------------------------------
-- Date Created	: Thursday, August 03, 2017
-- Created By	: DNet Team by using CodeSmith v 2.6
-- Rev History	:
---------------------------------------------------------------------------------------------------------------

alter PROCEDURE up_RetrieveCustomerCaseList

AS

SET TRANSACTION ISOLATION LEVEL READ COMMITTED
SET NOCOUNT ON


SELECT
		[ID],
		[DealerID],
		[SalesforceID],
		[CaseNumber],
		[CustomerName],
		[Phone],
		[Email],
		[Category],
		[SubCategory1],
		[SubCategory2],
		[SubCategory3],
		[SubCategory4],
		[CallerType],
		[CarType],
		[Variant],
		[EngineNumber],
		[ChassisNumber],
		[Odometer],
		[PlateNumber],
		[Priority],
		[CaseNumberReff],
		[CaseDate],
		[Subject],
		[Description],
		[Status],
	    [ReservationNumber],
		[RowStatus],
		[CreatedBy],
		[CreatedTime],
		[LastUpdateBy],
		[LastUpdateTIme]		
		FROM	
		[dbo].[CustomerCase] 

SET NOCOUNT OFF
go

set ANSI_NULLS off
go

---------------------------------------------------------------------------------------------------------------
-- Date Created	: Friday, August 25, 2017
-- Created By	: DNet Team by using CodeSmith v 2.6
-- Rev History	:
---------------------------------------------------------------------------------------------------------------

alter PROCEDURE up_RetrieveDealerBranch
	@ID int OUTPUT	
AS
SET TRANSACTION ISOLATION LEVEL READ COMMITTED

SET NOCOUNT ON

SELECT
	[ID],
	[DealerID],
	[Name],
	[Status],
	[Address],
	[CityID],
	[ZipCode],
	[ProvinceID],
	[Phone],
	[Fax],
	[Website],
	[Email],
	[TypeBranch],
	[DealerBranchCode],
	[Term1],
	[Term2],
	[MainAreaID],
	[Area1ID],
	[Area2ID],
	[BranchAssignmentNo],
	[BranchAssignmentDate],
	[SalesUnitFlag],
	[ServiceFlag],
	[SparepartFlag],
	[RowStatus],
	[CreatedBy],
	[CreatedTime],
	[LastUpdateBy],
	[LastUpdateTime]	
FROM	[dbo].[DealerBranch]

WHERE
	[ID] = @ID

SET NOCOUNT OFF
go

---------------------------------------------------------------------------------------------------------------
-- Date Created	: Friday, August 25, 2017
-- Created By	: DNet Team by using CodeSmith v 2.6
-- Rev History	:
---------------------------------------------------------------------------------------------------------------

alter PROCEDURE up_RetrieveDealerBranchList

AS

SET TRANSACTION ISOLATION LEVEL READ COMMITTED
SET NOCOUNT ON


SELECT
		[ID],
		[DealerID],
		[Name],
		[Status],
		[Address],
		[CityID],
		[ZipCode],
		[ProvinceID],
		[Phone],
		[Fax],
		[Website],
		[Email],
		[TypeBranch],
		[DealerBranchCode],
		[Term1],
		[Term2],
		[MainAreaID],
		[Area1ID],
		[Area2ID],
		[BranchAssignmentNo],
		[BranchAssignmentDate],
		[SalesUnitFlag],
		[ServiceFlag],
		[SparepartFlag],
		[RowStatus],
		[CreatedBy],
		[CreatedTime],
		[LastUpdateBy],
		[LastUpdateTime]		
		FROM	
		[dbo].[DealerBranch] 

SET NOCOUNT OFF
go

---------------------------------------------------------------------------------------------------------------
-- Date Created	: 15 Nopember 2005
-- Created By	: DNet Team by using CodeSmith v 2.6
-- Rev History	:
---------------------------------------------------------------------------------------------------------------

alter PROCEDURE up_RetrieveDepositLine
	@ID int OUTPUT
AS

SET TRANSACTION ISOLATION LEVEL READ COMMITTED

SET NOCOUNT ON

SELECT
	[ID],
	[DepositID],
	[DocumentNo],
	[PostingDate],
	[ClearingDate],
	[Debit],
	[Credit],
	[ReferenceNo],
	[InvoiceNo],
	[Remark],
	PaymentType,
	[RowStatus],
	[CreatedBy],
	[CreatedTime],
	[LastUpdateBy],
	[LastUpdateTime]
FROM	[dbo].[DepositLine]
WHERE
	[ID] = @ID

SET NOCOUNT OFF
go

---------------------------------------------------------------------------------------------------------------
-- Date Created	: 15 Nopember 2005
-- Created By	: DNet Team by using CodeSmith v 2.6
-- Rev History	:
---------------------------------------------------------------------------------------------------------------

alter PROCEDURE up_RetrieveDepositLineList

AS

SET TRANSACTION ISOLATION LEVEL READ COMMITTED
SET NOCOUNT ON

SELECT
		[ID],
		[DepositID],
		[DocumentNo],
		[PostingDate],
		[ClearingDate],
		[Debit],
		[Credit],
		[ReferenceNo],
		[InvoiceNo],
		[Remark],
		PaymentType,
		[RowStatus],
		[CreatedBy],
		[CreatedTime],
		[LastUpdateBy],
		[LastUpdateTime]
		FROM	
		[dbo].[DepositLine] 

SET NOCOUNT OFF
go

set ANSI_NULLS on
go

---------------------------------------------------------------------------------------------------------------
-- Date Created	: Thursday, June 23, 2011
-- Created By	: DNet Team by using CodeSmith v 2.6
-- Rev History	:
---------------------------------------------------------------------------------------------------------------
alter PROCEDURE up_RetrieveEndCustomer
	@ID int OUTPUT	
AS
SET TRANSACTION ISOLATION LEVEL READ COMMITTED
SET NOCOUNT ON
SELECT
	[ID],
	[ProjectIndicator],
	[RefChassisNumberID],
	[CustomerID],
	[Name1],
	[FakturDate],
	[OpenFakturDate],
	[FakturNumber],
	[AreaViolationFlag],
	[AreaViolationPaymentMethodID],
	[AreaViolationyAmount],
	[AreaViolationBankName],
	[AreaViolationGyroNumber],
	[PenaltyFlag],
	[PenaltyPaymentMethodID],
	[PenaltyAmount],
	[PenaltyBankName],
	[PenaltyGyroNumber],
	[ReferenceLetterFlag],
	[ReferenceLetter],
	[SaveBy],
	[SaveTime],
	[ValidateBy],
	[ValidateTime],
	[ConfirmBy],
	[ConfirmTime],
	[DownloadBy],
	[DownloadTime],
	[PrintedBy],
	[PrintedTime],
	[CleansingCustomerID],
	[MCPHeaderID],
	[MCPStatus],
	[LKPPHeaderID],
	[LKPPStatus],
	[Remark1],
	[Remark2],
	[HandoverDate],
	[IsTemporary],
	[RowStatus],
	[CreatedBy],
	[CreatedTime],
	[LastUpdateBy],
	[LastUpdateTime]	
FROM	[dbo].[EndCustomer]
WHERE
	[ID] = @ID
SET NOCOUNT OFF


---------------------------------------------------------------
/****** Object:  StoredProcedure [dbo].[up_RetrieveEndCustomerList]    Script Date: 18/09/2018 11:47:48 ******/
SET ANSI_NULLS OFF
go

---------------------------------------------------------------------------------------------------------------
-- Date Created	: Thursday, June 23, 2011
-- Created By	: DNet Team by using CodeSmith v 2.6
-- Rev History	:
---------------------------------------------------------------------------------------------------------------
alter PROCEDURE up_RetrieveEndCustomerList
AS
SET TRANSACTION ISOLATION LEVEL READ COMMITTED
SET NOCOUNT ON
SELECT
		[ID],
		[ProjectIndicator],
		[RefChassisNumberID],
		[CustomerID],
		[Name1],
		[FakturDate],
		[OpenFakturDate],
		[FakturNumber],
		[AreaViolationFlag],
		[AreaViolationPaymentMethodID],
		[AreaViolationyAmount],
		[AreaViolationBankName],
		[AreaViolationGyroNumber],
		[PenaltyFlag],
		[PenaltyPaymentMethodID],
		[PenaltyAmount],
		[PenaltyBankName],
		[PenaltyGyroNumber],
		[ReferenceLetterFlag],
		[ReferenceLetter],
		[SaveBy],
		[SaveTime],
		[ValidateBy],
		[ValidateTime],
		[ConfirmBy],
		[ConfirmTime],
		[DownloadBy],
		[DownloadTime],
		[PrintedBy],
		[PrintedTime],
		[CleansingCustomerID],
		[MCPHeaderID],
		[MCPStatus],
		[LKPPHeaderID],
		[LKPPStatus],
		[Remark1],
		[Remark2],
		[HandoverDate],
		[IsTemporary],
		[RowStatus],
		[CreatedBy],
		[CreatedTime],
		[LastUpdateBy],
		[LastUpdateTime]		
		FROM	
		[dbo].[EndCustomer] 

SET NOCOUNT OFF


---------------------------------------------------------------
/****** Object:  StoredProcedure [dbo].[up_UpdateEndCustomer]    Script Date: 18/09/2018 11:48:02 ******/
SET ANSI_NULLS ON
go

commit
go

set xact_abort on
go

begin transaction
go

set ANSI_NULLS on
go

---------------------------------------------------------------------------------------------------------------
-- Date Created	: Thursday, December 03, 2009
-- Created By	: DNet Team by using CodeSmith v 2.6
-- Rev History	:
---------------------------------------------------------------------------------------------------------------

alter PROCEDURE up_RetrieveEstimationEquipDetail
	@ID int OUTPUT	
AS
SET TRANSACTION ISOLATION LEVEL READ COMMITTED

SET NOCOUNT ON

SELECT
	[ID],
	[EstimationEquipHeaderID],
	[SparePartMasterID],
	[Harga],
	[Discount],
	[TotalForecast],
	[EstimationUnit],
	[Status],
	[ConfirmedDate],
	[Remark],
	[RowStatus],
	[CreatedBy],
	[CreatedTime],
	[LastUpdatedBy],
	[LastUpdatedTime]	
FROM	[dbo].[EstimationEquipDetail]

WHERE
	[ID] = @ID

SET NOCOUNT OFF
go

---------------------------------------------------------------------------------------------------------------
-- Date Created	: Thursday, December 03, 2009
-- Created By	: DNet Team by using CodeSmith v 2.6
-- Rev History	:
---------------------------------------------------------------------------------------------------------------

alter PROCEDURE up_RetrieveEstimationEquipDetailList

AS

SET TRANSACTION ISOLATION LEVEL READ COMMITTED
SET NOCOUNT ON


SELECT
		[ID],
		[EstimationEquipHeaderID],
		[SparePartMasterID],
		[Harga],
		[Discount],
		[TotalForecast],
		[EstimationUnit],
		[Status],
		[ConfirmedDate],
		[Remark],
		[RowStatus],
		[CreatedBy],
		[CreatedTime],
		[LastUpdatedBy],
		[LastUpdatedTime]		
		FROM	
		[dbo].[EstimationEquipDetail] 

SET NOCOUNT OFF
go

---------------------------------------------------------------------------------------------------------------
-- Date Created	: Tuesday, August 18, 2009
-- Created By	: DNet Team by using CodeSmith v 2.6
-- Rev History	:
---------------------------------------------------------------------------------------------------------------

alter PROCEDURE up_RetrieveEstimationEquipHeader
	@ID int OUTPUT
	
AS
SET TRANSACTION ISOLATION LEVEL READ COMMITTED

SET NOCOUNT ON

SELECT
	[ID],
	[EstimationNumber],
	[DealerID],
	[DepositBKewajibanHeaderID],
	[Status],
	[DMSPRNo],
	[RowStatus],
	[CreatedBy],
	[CreatedTime],
	[LastUpdatedBy],
	[LastUpdatedTime]
	
FROM	[dbo].[EstimationEquipHeader]

WHERE
	[ID] = @ID

SET NOCOUNT OFF
go

---------------------------------------------------------------------------------------------------------------
-- Date Created	: Tuesday, August 18, 2009
-- Created By	: DNet Team by using CodeSmith v 2.6
-- Rev History	:
---------------------------------------------------------------------------------------------------------------

alter PROCEDURE up_RetrieveEstimationEquipHeaderList

AS

SET TRANSACTION ISOLATION LEVEL READ COMMITTED
SET NOCOUNT ON

SELECT
		[ID],
		[EstimationNumber],
		[DealerID],
		[DepositBKewajibanHeaderID],
		[Status],
		[DMSPRNo],
		[RowStatus],
		[CreatedBy],
		[CreatedTime],
		[LastUpdatedBy],
		[LastUpdatedTime]
		
		FROM	
		[dbo].[EstimationEquipHeader] 

SET NOCOUNT OFF
go

---------------------------------------------------------------------------------------------------------------
-- Date Created	: Tuesday, October 04, 2016
-- Created By	: DNet Team by using CodeSmith v 2.6
-- Rev History	:
---------------------------------------------------------------------------------------------------------------
  
alter PROCEDURE up_RetrieveFreeService  
 @ID int OUTPUT   
AS  
SET TRANSACTION ISOLATION LEVEL READ COMMITTED  
  
SET NOCOUNT ON  
  
SELECT  
 [ID],  
 [Status],  
 [ChassisMasterID],  
 [FSKindID],  
 [MileAge],  
 [ServiceDate],  
 [ServiceDealerID],  
 [DealerBranchID],  
 [SoldDate],  
 [NotificationNumber],  
 [NotificationType],  
 [TotalAmount],  
 [LabourAmount],  
 [PartAmount],  
 [PPNAmount],  
 [PPHAmount],  
 [Reject],  
 [Reason],  
 [ReleaseBy],  
 [ReleaseDate], 
 [VisitType], 
 [FleetRequestID],  
 [WorkOrderNumber],  
 [RowStatus],  
 [CreatedBy],  
 [CreatedTime],  
 [LastUpdateBy],  
 [LastUpdateTime]   
FROM [dbo].[FreeService]  
  
WHERE  
 [ID] = @ID  
  
SET NOCOUNT OFF
go

---------------------------------------------------------------------------------------------------------------  
-- Date Created : Wednesday, October 19, 2011  
-- Created By : DNet Team by using CodeSmith v 2.6  
-- Rev History :  
---------------------------------------------------------------------------------------------------------------  
  
alter PROCEDURE up_RetrieveFreeServiceBB  
 @ID int OUTPUT   
AS  
SET TRANSACTION ISOLATION LEVEL READ COMMITTED  
  
SET NOCOUNT ON  
  
SELECT  
 [ID],  
 [Status],  
 [ChassisMasterID],  
 [FSKindID],  
 [MileAge],  
 [ServiceDate],  
 [ServiceDealerID],  
 [SoldDate],  
 [NotificationNumber],  
 [NotificationType],  
 [TotalAmount],  
 [LabourAmount],  
 [PartAmount],  
 [PPNAmount],  
 [PPHAmount],  
 [Reject],  
 [Reason],  
 [ReleaseBy],  
 [ReleaseDate],  
 [VisitType],
 [RowStatus],  
 [CreatedBy],  
 [CreatedTime],  
 [LastUpdateBy],  
 [LastUpdateTime]   
FROM [dbo].[FreeServiceBB]  
  
WHERE  
 [ID] = @ID  
  
SET NOCOUNT OFF
go

---------------------------------------------------------------------------------------------------------------  
-- Date Created : Wednesday, October 19, 2011  
-- Created By : DNet Team by using CodeSmith v 2.6  
-- Rev History :  
---------------------------------------------------------------------------------------------------------------  
  
alter PROCEDURE up_RetrieveFreeServiceBBList  
  
AS  
  
SET TRANSACTION ISOLATION LEVEL READ COMMITTED  
SET NOCOUNT ON  
  
  
SELECT  
  [ID],  
  [Status],  
  [ChassisMasterID],  
  [FSKindID],  
  [MileAge],  
  [ServiceDate],  
  [ServiceDealerID],  
  [SoldDate],  
  [NotificationNumber],  
  [NotificationType],  
  [TotalAmount],  
  [LabourAmount],  
  [PartAmount],  
  [PPNAmount],  
  [PPHAmount],  
  [Reject],  
  [Reason],  
  [ReleaseBy],  
  [ReleaseDate],  
  [VisitType],
  [RowStatus],  
  [CreatedBy],  
  [CreatedTime],  
  [LastUpdateBy],  
  [LastUpdateTime]    
  FROM   
  [dbo].[FreeServiceBB]   
  
SET NOCOUNT OFF
go

---------------------------------------------------------------------------------------------------------------  
-- Date Created : Tuesday, October 04, 2016 
-- Created By : DNet Team by using CodeSmith v 2.6  
-- Rev History :  
---------------------------------------------------------------------------------------------------------------  
  
alter PROCEDURE up_RetrieveFreeServiceList  
  
AS  
  
SET TRANSACTION ISOLATION LEVEL READ COMMITTED  
SET NOCOUNT ON  
  
  
SELECT  
  [ID],  
  [Status],  
  [ChassisMasterID],  
  [FSKindID],  
  [MileAge],  
  [ServiceDate],  
  [ServiceDealerID],  
  [DealerBranchID],  
  [SoldDate],  
  [NotificationNumber],  
  [NotificationType],  
  [TotalAmount],  
  [LabourAmount],  
  [PartAmount],  
  [PPNAmount],  
  [PPHAmount],  
  [Reject],  
  [Reason],  
  [ReleaseBy],  
  [ReleaseDate],  
  [VisitType],
  [FleetRequestID],  
  [WorkOrderNumber],  
  [RowStatus],  
  [CreatedBy],  
  [CreatedTime],  
  [LastUpdateBy],  
  [LastUpdateTime]    
  FROM   
  [dbo].[FreeService]   
  
SET NOCOUNT OFF
go

--/****** Object:  Stored Procedure dbo.up_RetrieveFSKind    Script Date: 14/10/2005 11:06:16 ******/  
---------------------------------------------------------------------------------------------------------------  
-- Date Created : Thursday, October 13, 2005  
-- Created By : DNet Team by using CodeSmith v 2.6  
-- Rev History :  
---------------------------------------------------------------------------------------------------------------  
  
alter PROCEDURE up_RetrieveFSKind  
 @ID int OUTPUT  
AS  
  
SET TRANSACTION ISOLATION LEVEL READ COMMITTED  
  
SET NOCOUNT ON  
  
SELECT  
 [ID],  
 [KindCode],  
 [KM],  
 [KindDescription],  
 [FSType],
 [Status],
 [RowStatus],  
 [CreatedBy],  
 [CreatedTime],  
 [LastUpdateBy],  
 [LastUpdateTime]  
FROM [dbo].[FSKind]  
WHERE  
 [ID] = @ID  
  
SET NOCOUNT OFF
go

--/****** Object:  Stored Procedure dbo.up_RetrieveFSKindList    Script Date: 14/10/2005 11:06:16 ******/  
---------------------------------------------------------------------------------------------------------------  
-- Date Created : Thursday, October 13, 2005  
-- Created By : DNet Team by using CodeSmith v 2.6  
-- Rev History :  
---------------------------------------------------------------------------------------------------------------  
  
alter PROCEDURE up_RetrieveFSKindList  
  
AS  
  
SET TRANSACTION ISOLATION LEVEL READ COMMITTED  
SET NOCOUNT ON  
  
SELECT  
  [ID],  
  [KindCode],  
  [KM],  
  [KindDescription],
  [FSType],
  [Status],  
  [RowStatus],  
  [CreatedBy],  
  [CreatedTime],  
  [LastUpdateBy],  
  [LastUpdateTime]  
  FROM   
  [dbo].[FSKind]   
  
SET NOCOUNT OFF
go

commit
GO

set xact_abort on
go

begin transaction
go

set ANSI_NULLS on
go

---------------------------------------------------------------------------------------------------------------
-- Date Created	: 01 Oktober 2007
-- Created By	: DNet Team by using CodeSmith v 2.6
-- Rev History	:
---------------------------------------------------------------------------------------------------------------

alter PROCEDURE up_RetrieveIndentPartDetail
	@ID int OUTPUT
	
AS
SET TRANSACTION ISOLATION LEVEL READ COMMITTED

SET NOCOUNT ON

SELECT
	[ID],
	[IndentPartHeaderID],
	[SparePartMasterID],
	[TotalForecast],
	[Qty],
	[Description],
	[AllocationQty],
	[IsCompletedAllocation],
	[Price],
	[RowStatus],
	[CreatedBy],
	[CreatedTime],
	[LastUpdateBy],
	[LastUpdateTime]
	
FROM	[dbo].[IndentPartDetail]

WHERE
	[ID] = @ID

SET NOCOUNT OFF
go

---------------------------------------------------------------------------------------------------------------
-- Date Created	: 01 Oktober 2007
-- Created By	: DNet Team by using CodeSmith v 2.6
-- Rev History	:
---------------------------------------------------------------------------------------------------------------

alter PROCEDURE up_RetrieveIndentPartDetailList

AS

SET TRANSACTION ISOLATION LEVEL READ COMMITTED
SET NOCOUNT ON

SELECT
		[ID],
		[IndentPartHeaderID],
		[SparePartMasterID],
		[TotalForecast],
		[Qty],
		[Description],
		[AllocationQty],
		[IsCompletedAllocation],
		[Price],
		[RowStatus],
		[CreatedBy],
		[CreatedTime],
		[LastUpdateBy],
		[LastUpdateTime]
		
		FROM	
		[dbo].[IndentPartDetail] 

SET NOCOUNT OFF
go

---------------------------------------------------------------------------------------------------------------
-- Date Created	: Friday, December 12, 2008
-- Created By	: DNet Team by using CodeSmith v 2.6
-- Rev History	:
-- Rev			: 06/03/2018
--				  add Purpose and DMSPRNo columns
---------------------------------------------------------------------------------------------------------------

alter PROCEDURE up_RetrieveIndentPartHeader
	@ID int OUTPUT	
AS
SET TRANSACTION ISOLATION LEVEL READ COMMITTED

SET NOCOUNT ON

SELECT
	[ID],
	[DealerID],
	[RequestNo],
	[RequestDate],
	[MaterialType],
	[TermOfPaymentID],
	[TOPBlockStatusID],
	[Status],
	[StatusKTB],
	[SubmitFile],
	[PaymentType],
	[Price],
	[KTBConfirmedDate],
	[DescID],
	[ChassisNumber],
	[DMSPRNo],
	[RowStatus],
	[CreatedBy],
	[CreatedTime],
	[LastUpdateBy],
	[LastUpdateTime]	
FROM	[dbo].[IndentPartHeader]

WHERE
	[ID] = @ID

SET NOCOUNT OFF
go

---------------------------------------------------------------------------------------------------------------
-- Date Created	: Friday, December 12, 2008
-- Created By	: DNet Team by using CodeSmith v 2.6
-- Rev History	:
-- Rev			: 06/03/2018
--				  add Purpose and DMSPRNo columns
---------------------------------------------------------------------------------------------------------------

alter PROCEDURE up_RetrieveIndentPartHeaderList

AS

SET TRANSACTION ISOLATION LEVEL READ COMMITTED
SET NOCOUNT ON


SELECT
		[ID],
		[DealerID],
		[RequestNo],
		[RequestDate],
		[MaterialType],
		[TermOfPaymentID],
		[TOPBlockStatusID],
		[Status],
		[StatusKTB],
		[SubmitFile],
		[PaymentType],
		[Price],
		[KTBConfirmedDate],
		[DescID],
		[ChassisNumber],
		[DMSPRNo],
		[RowStatus],
		[CreatedBy],
		[CreatedTime],
		[LastUpdateBy],
		[LastUpdateTime]		
		FROM	
		[dbo].[IndentPartHeader] 

SET NOCOUNT OFF
go

---------------------------------------------------------------------------------------------------------------
-- Date Created	: 11 Nopember 2016
-- Created By	: DNet Team by using CodeSmith v 2.6
-- Rev History	:
---------------------------------------------------------------------------------------------------------------

alter PROCEDURE up_RetrieveMainArea
	@ID int OUTPUT	
AS
SET TRANSACTION ISOLATION LEVEL READ COMMITTED

SET NOCOUNT ON

SELECT
	[ID],
	[AreaCode],
	[Description],
	[PICSales],
	[PICServices],
	[PICSpareparts],
	[RowStatus],
	[CreatedBy],
	[CreatedTime],
	[LastUpdateBy],
	[LastUpdateTime]	
FROM	[dbo].[MainArea]

WHERE
	[ID] = @ID

SET NOCOUNT OFF
go

---------------------------------------------------------------------------------------------------------------
-- Date Created	: 11 Nopember 2016
-- Created By	: DNet Team by using CodeSmith v 2.6
-- Rev History	:
---------------------------------------------------------------------------------------------------------------

alter PROCEDURE up_RetrieveMainAreaList

AS

SET TRANSACTION ISOLATION LEVEL READ COMMITTED
SET NOCOUNT ON


SELECT
		[ID],
		[AreaCode],
		[Description],
		[PICSales],
		[PICServices],
		[PICSpareparts],
		[RowStatus],
		[CreatedBy],
		[CreatedTime],
		[LastUpdateBy],
		[LastUpdateTime]		
		FROM	
		[dbo].[MainArea] 

SET NOCOUNT OFF
go

---------------------------------------------------------------------------------------------------------------  
-- Date Created : Wednesday, October 31, 2012  
-- Created By : DNet Team by using CodeSmith v 2.6  
-- Rev History :  
---------------------------------------------------------------------------------------------------------------  
  
alter PROCEDURE up_RetrieveMaxTOPDay  
 @ID int OUTPUT   
AS  
SET TRANSACTION ISOLATION LEVEL READ COMMITTED  
  
SET NOCOUNT ON  
  
SELECT  
 [ID],  
 [DealerID],  
 [VechileTypeID],  
 [Normal],  
 [Factoring],  
 [IsCOD],
 [RowStatus],  
 [CreatedTime],  
 [CreatedBy],  
 [LastUpdateTime],  
 [LastUpdateBy]   
FROM [dbo].[MaxTOPDay]  
  
WHERE  
 [ID] = @ID  
  
SET NOCOUNT OFF
go

---------------------------------------------------------------------------------------------------------------  
-- Date Created : Wednesday, October 31, 2012  
-- Created By : DNet Team by using CodeSmith v 2.6  
-- Rev History :  
---------------------------------------------------------------------------------------------------------------  
  
alter PROCEDURE up_RetrieveMaxTOPDayList  
AS  
SET TRANSACTION ISOLATION LEVEL READ COMMITTED  
SET NOCOUNT ON  
  
SELECT  
  [ID],  
  [DealerID],  
  [VechileTypeID],  
  [Normal],  
  [Factoring],  
  [IsCOD],
  [RowStatus],  
  [CreatedTime],  
  [CreatedBy],  
  [LastUpdateTime],  
  [LastUpdateBy]    
  FROM   
  [dbo].[MaxTOPDay]   
  
SET NOCOUNT OFF
go

commit
go

set xact_abort on
go

begin transaction
go

set ANSI_NULLS on
go

---------------------------------------------------------------------------------------------------------------
-- Date Created	: Friday, July 15, 2011
-- Created By	: DNet Team by using CodeSmith v 2.6
-- Rev History	:
---------------------------------------------------------------------------------------------------------------

alter PROCEDURE up_RetrievePartShop
	@ID int OUTPUT	
AS
SET TRANSACTION ISOLATION LEVEL READ COMMITTED

SET NOCOUNT ON

SELECT
	[ID],
	[DealerID],
	[CityPartID],
	[CityID],
	[PartShopCode],
	[OldPartShopCode],
	[Name],
	[Address],
	[Phone],
	[Fax],
	[Email],
	[Status],
	[RowStatus],
	[CreatedBy],
	[CreatedTime],
	[LastUpdateBy],
	[LastUpdateTime]	
FROM	[dbo].[PartShop]

WHERE
	[ID] = @ID

SET NOCOUNT OFF
go

---------------------------------------------------------------------------------------------------------------
-- Date Created	: Friday, July 15, 2011
-- Created By	: DNet Team by using CodeSmith v 2.6
-- Rev History	:
---------------------------------------------------------------------------------------------------------------

alter PROCEDURE up_RetrievePartShopList

AS

SET TRANSACTION ISOLATION LEVEL READ COMMITTED
SET NOCOUNT ON


SELECT
		[ID],
		[DealerID],
		[CityPartID],
		[CityID],
		[PartShopCode],
		[OldPartShopCode],
		[Name],
		[Address],
		[Phone],
		[Fax],
		[Email],
		[Status],
		[RowStatus],
		[CreatedBy],
		[CreatedTime],
		[LastUpdateBy],
		[LastUpdateTime]		
		FROM	
		[dbo].[PartShop] 

SET NOCOUNT OFF
go

---------------------------------------------------------------------------------------------------------------
-- Date Created	: Wednesday, November 30, 2005
-- Created By	: DNet Team by using CodeSmith v 2.6
-- Rev History	:
---------------------------------------------------------------------------------------------------------------

alter PROCEDURE up_RetrievePDI
	@ID int OUTPUT	
AS
SET TRANSACTION ISOLATION LEVEL READ COMMITTED

SET NOCOUNT ON

SELECT
	[ID],
	[ChassisMasterID],
	[DealerID],
	[DealerBranchID],
	[Kind],
	[PDIStatus],
	[PDIDate],
	[ReleaseBy],
	[ReleaseDate],
	[WorkOrderNumber],
	[RowStatus],
	[CreatedBy],
	[CreatedTime],
	[LastUpdateBy],
	[LastUpdateTime]	
FROM	[dbo].[PDI]

WHERE
	[ID] = @ID

SET NOCOUNT OFF
go

---------------------------------------------------------------------------------------------------------------
-- Date Created	: Wednesday, November 30, 2005
-- Created By	: DNet Team by using CodeSmith v 2.6
-- Rev History	:
---------------------------------------------------------------------------------------------------------------

alter PROCEDURE up_RetrievePDIList

AS

SET TRANSACTION ISOLATION LEVEL READ COMMITTED
SET NOCOUNT ON


SELECT
		[ID],
		[ChassisMasterID],
		[DealerID],
		[DealerBranchID],
		[Kind],
		[PDIStatus],
		[PDIDate],
		[ReleaseBy],
		[ReleaseDate],
		[WorkOrderNumber],
		[RowStatus],
		[CreatedBy],
		[CreatedTime],
		[LastUpdateBy],
		[LastUpdateTime]		
		FROM	
		[dbo].[PDI] 

SET NOCOUNT OFF
go

---------------------------------------------------------------------------------------------------------------
-- Date Created	: Wednesday, August 01, 2007
-- Created By	: DNet Team by using CodeSmith v 2.6
-- Rev History	:
---------------------------------------------------------------------------------------------------------------

alter PROCEDURE up_RetrievePMHeader
	@ID int OUTPUT	
AS
SET TRANSACTION ISOLATION LEVEL READ COMMITTED

SET NOCOUNT ON

SELECT
	[ID],
	[DealerID],
	[DealerBranchID],
	[ChassisNumberID],
	[PMKindID],
	[StandKM],
	[ServiceDate],
	[ReleaseDate],
	[PMStatus],
	[EntryType],
	[WorkOrderNumber],
	[BookingNo],
	[VisitType],
	[Remarks],
	[RowStatus],
	[CreatedBy],
	[CreatedTime],
	[LastUpdateBy],
	[LastUpdateTime]	
FROM	[dbo].[PMHeader]

WHERE
	[ID] = @ID

SET NOCOUNT OFF
go

---------------------------------------------------------------------------------------------------------------
-- Date Created	: Wednesday, August 01, 2007
-- Created By	: DNet Team by using CodeSmith v 2.6
-- Rev History	:
---------------------------------------------------------------------------------------------------------------

alter PROCEDURE up_RetrievePMHeaderList

AS

SET TRANSACTION ISOLATION LEVEL READ COMMITTED
SET NOCOUNT ON


SELECT
		[ID],
		[DealerID],
		[DealerBranchID],
		[ChassisNumberID],
		[PMKindID],
		[StandKM],
		[ServiceDate],
		[ReleaseDate],
		[PMStatus],
		[EntryType],
		[WorkOrderNumber],
		[BookingNo],
		[VisitType],
		[Remarks],
		[RowStatus],
		[CreatedBy],
		[CreatedTime],
		[LastUpdateBy],
		[LastUpdateTime]		
		FROM	
		[dbo].[PMHeader] 

SET NOCOUNT OFF
go

---------------------------------------------------------------------------------------------------------------  
-- Date Created : Monday, July 23, 2007  
-- Created By : DNet Team by using CodeSmith v 2.6  
-- Rev History :  
---------------------------------------------------------------------------------------------------------------  
  
alter PROCEDURE up_RetrievePQRHeader  
 @ID int OUTPUT  
   
AS  
SET TRANSACTION ISOLATION LEVEL READ COMMITTED  
  
SET NOCOUNT ON  
  
SELECT  
 [ID],  
 [PQRNo],  
 [PQRType],
 [RefPQRNo],  
 [DealerID],  
 [DealerBranchID],
 [Year],  
 [SeqNo],  
 [CategoryID],  
 [DocumentDate],  
 [SoldDate],  
 [ChassisMasterID],  
 [PQRDate],  
 [OdoMeter],  
 [Velocity],  
 [CustomerName],  
 [CustomerAddress],  
 [ValidationTime],  
 [ConfirmBy],  
 [ConfirmTime],  
 [RealeseTime],  
 [IntervalProcess],  
 [Complexity],  
 [Subject],  
 [Symptomps],  
 [Causes],  
 [Results],  
 [Notes],  
 [Solutions],  
 [Bobot],  
 [ReleaseBy],  
 [FinishBy],  
 [FinishDate],  
 [CodeA],  
 [CodeB],  
 [CodeC], 
 [WorkOrderNumber], 
 [RowStatus],  
 [CreatedBy],  
 [CreatedTime],  
 [LastUpdateBy],  
 [LastUpdateTime]  
   
FROM [dbo].[PQRHeader]  
  
WHERE  
 [ID] = @ID  
  
SET NOCOUNT OFF
go

---------------------------------------------------------------------------------------------------------------  
-- Date Created : Monday, January 16, 2012  
-- Created By : DNet Team by using CodeSmith v 2.6  
-- Rev History :  
---------------------------------------------------------------------------------------------------------------  
  
alter PROCEDURE up_RetrievePQRHeaderBB  
 @ID int OUTPUT   
AS  
SET TRANSACTION ISOLATION LEVEL READ COMMITTED  
  
SET NOCOUNT ON  
  
SELECT  
 [ID],  
 [PQRNo],  
 [PQRType],
 [RefPQRNo],  
 [DealerID],  
 [DealerBranchID],
 [Year],  
 [SeqNo],  
 [CategoryID],  
 [DocumentDate],  
 [SoldDate],  
 [ChassisMasterBBID],  
 [PQRDate],  
 [OdoMeter],  
 [Velocity],  
 [CustomerName],  
 [CustomerAddress],  
 [ValidationTime],  
 [ConfirmBy],  
 [ConfirmTime],  
 [RealeseTime],  
 [IntervalProcess],  
 [Complexity],  
 [Subject],  
 [Symptomps],  
 [Causes],  
 [Results],  
 [Notes],  
 [Solutions],  
 [Bobot],  
 [ReleaseBy],  
 [FinishBy],  
 [FinishDate],  
 [CodeA],  
 [CodeB],  
 [CodeC],  
 [WorkOrderNumber],
 [RowStatus],  
 [CreatedBy],  
 [CreatedTime],  
 [LastUpdateBy],  
 [LastUpdateTime]   
FROM [dbo].[PQRHeaderBB]  
  
WHERE  
 [ID] = @ID  
  
SET NOCOUNT OFF
go

---------------------------------------------------------------------------------------------------------------    
-- Date Created : Monday, January 16, 2012    
-- Created By : DNet Team by using CodeSmith v 2.6    
-- Rev History :    
---------------------------------------------------------------------------------------------------------------    
    
alter PROCEDURE up_RetrievePQRHeaderBBList    
    
AS    
    
SET TRANSACTION ISOLATION LEVEL READ COMMITTED    
SET NOCOUNT ON    
    
    
SELECT    
  [ID],    
  [PQRNo],    
  [PQRType],
  [RefPQRNo],    
  [DealerID],  
  [DealerBranchID],  
  [Year],    
  [SeqNo],    
  [CategoryID],    
  [DocumentDate],    
  [SoldDate],    
  [ChassisMasterBBID],    
  [PQRDate],    
  [OdoMeter],    
  [Velocity],    
  [CustomerName],    
  [CustomerAddress],    
  [ValidationTime],    
  [ConfirmBy],    
  [ConfirmTime],    
  [RealeseTime],    
  [IntervalProcess],    
  [Complexity],    
  [Subject],    
  [Symptomps],    
  [Causes],    
  [Results],    
  [Notes],    
  [Solutions],    
  [Bobot],    
  [ReleaseBy],    
  [FinishBy],    
  [FinishDate],    
  [CodeA],    
  [CodeB],    
  [CodeC],    
  [WorkOrderNumber],  
  [RowStatus],    
  [CreatedBy],    
  [CreatedTime],    
  [LastUpdateBy],    
  [LastUpdateTime]      
  FROM     
  [dbo].[PQRHeaderBB]     
    
SET NOCOUNT OFF
go

---------------------------------------------------------------------------------------------------------------    
-- Date Created : Monday, July 23, 2007    
-- Created By : DNet Team by using CodeSmith v 2.6    
-- Rev History :    
---------------------------------------------------------------------------------------------------------------    
    
alter PROCEDURE up_RetrievePQRHeaderList    
    
AS    
SET TRANSACTION ISOLATION LEVEL READ COMMITTED    
SET NOCOUNT ON    
    
SELECT    
  [ID],    
  [PQRNo],    
  [PQRType],
  [RefPQRNo],    
  [DealerID],    
  [DealerBranchID],
  [Year],    
  [SeqNo],    
  [CategoryID],    
  [DocumentDate],    
  [SoldDate],    
  [ChassisMasterID],    
  [PQRDate],    
  [OdoMeter],    
  [Velocity],    
  [CustomerName],    
  [CustomerAddress],    
  [ValidationTime],    
  [ConfirmBy],    
  [ConfirmTime],    
  [RealeseTime],    
  [IntervalProcess],    
  [Complexity],    
  [Subject],    
  [Symptomps],    
  [Causes],    
  [Results],    
  [Notes],    
  [Solutions],    
  [Bobot],    
  [ReleaseBy],    
  [FinishBy],    
  [FinishDate],    
  [CodeA],    
  [CodeB],    
  [CodeC],    
  [WorkOrderNumber],  
  [RowStatus],    
  [CreatedBy],    
  [CreatedTime],    
  [LastUpdateBy],    
  [LastUpdateTime]    
      
  FROM     
  [dbo].[PQRHeader]     
    
SET NOCOUNT OFF
go

---------------------------------------------------------------------------------------------------------------
-- Date Created	: 19 April 2016
-- Created By	: DNet Team by using CodeSmith v 2.6
-- Rev History	:
---------------------------------------------------------------------------------------------------------------

alter PROCEDURE up_RetrieveRecallChassisMaster
	@ID int OUTPUT	
AS
SET TRANSACTION ISOLATION LEVEL READ COMMITTED

SET NOCOUNT ON

SELECT
		[ID],
		[ChassisNo],
		[RecallCategoryID],
		[RowStatus],
		[CreatedBy],
		[CreatedTime],
		[LastUpdateBy],
		[LastUpdateTime]	
FROM	[dbo].[RecallChassisMaster] a

WHERE
	[ID] = @ID

SET NOCOUNT OFF
go

---------------------------------------------------------------------------------------------------------------
-- Date Created	: 19 April 2016
-- Created By	: DNet Team by using CodeSmith v 2.6
-- Rev History	:
---------------------------------------------------------------------------------------------------------------

alter PROCEDURE up_RetrieveRecallChassisMasterList

AS

SET TRANSACTION ISOLATION LEVEL READ COMMITTED
SET NOCOUNT ON


SELECT
		[ID],
		[ChassisNo],
		[RecallCategoryID],
		[RowStatus],
		[CreatedBy],
		[CreatedTime],
		[LastUpdateBy],
		[LastUpdateTime]		
		FROM	
		[dbo].[RecallChassisMaster] 

SET NOCOUNT OFF
go

---------------------------------------------------------------------------------------------------------------
-- Date Created	: 19 April 2016
-- Created By	: DNet Team by using CodeSmith v 2.6
-- Rev History	:
---------------------------------------------------------------------------------------------------------------

alter PROCEDURE up_RetrieveRecallService
	@ID int OUTPUT	
AS
SET TRANSACTION ISOLATION LEVEL READ COMMITTED

SET NOCOUNT ON

SELECT
	[ID],
	[ChassisMasterID],
	[MileAge],
	[ServiceDate],
	[ServiceDealerID],
	[DealerBranchID],
	[RecallChassisMasterID],
	[WorkOrderNumber],
	[RowStatus],
	[CreatedBy],
	[CreatedTime],
	[LastUpdateBy],
	[LastUpdateTime]	
FROM	[dbo].[RecallService]

WHERE
	[ID] = @ID

SET NOCOUNT OFF
go

---------------------------------------------------------------------------------------------------------------
-- Date Created	: 19 April 2016
-- Created By	: DNet Team by using CodeSmith v 2.6
-- Rev History	:
---------------------------------------------------------------------------------------------------------------

alter PROCEDURE up_RetrieveRecallServiceList

AS

SET TRANSACTION ISOLATION LEVEL READ COMMITTED
SET NOCOUNT ON


SELECT
		[ID],
		[ChassisMasterID],
		[MileAge],
		[ServiceDate],
		[ServiceDealerID],
		[DealerBranchID],
		[RecallChassisMasterID],
		[WorkOrderNumber],
		[RowStatus],
		[CreatedBy],
		[CreatedTime],
		[LastUpdateBy],
		[LastUpdateTime]		
		FROM	
		[dbo].[RecallService] 

SET NOCOUNT OFF
go

---------------------------------------------------------------------------------------------------------------
-- Date Created	: Thursday, July 13, 2017
-- Created By	: DNet Team by using CodeSmith v 2.6
-- Rev History	:
-- Revision: Friday, 2 March 2018 by Mitrais Team
--			change BenefitMasterHeaderID to CampaignName
--			change IndustrialSectorID to BusinessSectordetailID
---------------------------------------------------------------------------------------------------------------

alter PROCEDURE up_RetrieveSAPCustomer
	@ID int OUTPUT	
AS
SET TRANSACTION ISOLATION LEVEL READ COMMITTED

SET NOCOUNT ON

SELECT
	[ID],
	[SalesforceID],
	[DealerID],
	[SalesmanHeaderID],
	[VechileTypeID],
	[CustomerCode],
	[CustomerName],
	[CustomerType],
	[CustomerAddress],
	[Phone],
	[Email],
	[Sex],
	[AgeSegment],
	[CustomerPurpose],
	[InformationType],
	[InformationSource],
	[Status],
	[Qty],
	[ProspectDate],
	[isSPK],
	[CurrVehicleBrand],
	[CurrVehicleType],
	[Note],
	[WebID],
    [BirthDate],
	[PreferedVehicleModel],
	[Description],
	[EstimatedCloseDate],
	[OriginatingLeadId],
	[StatusCode],
	[LeadStatus],
	[StateCode],
	[CampaignName],
	[BusinessSectorDetailID],	
	[RowStatus],
	[CreatedBy],
	[CreatedTime],
	[LastUpdateBy],
	[LastUpdateTime]
FROM	[dbo].[SAPCustomer]

WHERE
	[ID] = @ID

SET NOCOUNT OFF
go

commit
go

set xact_abort on
go

begin transaction
go

set ANSI_NULLS on
go

---------------------------------------------------------------------------------------------------------------
-- Date Created	: Thursday, July 13, 2017
-- Created By	: DNet Team by using CodeSmith v 2.6
-- Rev History	:
-- Revision: Friday, 2 March 2018 by Mitrais Team
--			change BenefitMasterHeaderID to CampaignName
--			change IndustrialSectorID to BusinessSectordetailID
---------------------------------------------------------------------------------------------------------------

alter PROCEDURE up_RetrieveSAPCustomerList

AS

SET TRANSACTION ISOLATION LEVEL READ COMMITTED
SET NOCOUNT ON


SELECT
		[ID],
		[SalesforceID],
		[DealerID],
		[SalesmanHeaderID],
		[VechileTypeID],
		[CustomerCode],
		[CustomerName],
		[CustomerType],
		[CustomerAddress],
		[Phone],
		[Email],
		[Sex],
		[AgeSegment],
		[CustomerPurpose],
		[InformationType],
		[InformationSource],
		[Status],
		[Qty],
		[ProspectDate],
		[isSPK],
		[CurrVehicleBrand],
		[CurrVehicleType],
		[Note],
		[WebID],
        [BirthDate],
		[PreferedVehicleModel],
		[Description],
		[EstimatedCloseDate],
		[OriginatingLeadId],
		[StatusCode],
		[LeadStatus],
		[StateCode],
		[CampaignName],
		[BusinessSectorDetailID],
		[RowStatus],
		[CreatedBy],
		[CreatedTime],
		[LastUpdateBy],
		[LastUpdateTime]
		FROM	
		[dbo].[SAPCustomer] 

SET NOCOUNT OFF
go

---------------------------------------------------------------------------------------------------------------
-- Date Created	: Tuesday, July 24, 2012
-- Created By	: DNet Team by using CodeSmith v 2.6
-- Rev History	:
---------------------------------------------------------------------------------------------------------------

alter PROCEDURE up_RetrieveSparePartMaster @ID INT OUTPUT
AS
    SET TRANSACTION ISOLATION LEVEL READ COMMITTED

    SET NOCOUNT ON

    SELECT  [ID] ,
            [ProductCategoryID] ,
            [PartNumber] ,
            [PartName] ,
            [uom] ,
            [partnumberreff] ,
            [MaterialCategoryCode] ,
            [AltPartNumber] ,
            [AltPartName] ,
            [PartCode] ,
            [ModelCode] ,
            [SupplierCode] ,
            [TypeCode] ,
            [Stock] ,
            [RetalPrice] ,
            [PartStatus] ,
            [ActiveStatus] ,
            [AccessoriesType] ,
			[ProductType] ,
            [RowStatus] ,
            [CreatedBy] ,
            [CreatedTime] ,
            [LastUpdateBy] ,
            [LastUpdateTime]
    FROM    [dbo].[SparePartMaster]
    WHERE   [ID] = @ID

    SET NOCOUNT OFF
go

---------------------------------------------------------------------------------------------------------------
-- Date Created	: Tuesday, July 24, 2012
-- Created By	: DNet Team by using CodeSmith v 2.6
-- Rev History	:
---------------------------------------------------------------------------------------------------------------

alter PROCEDURE up_RetrieveSparePartMasterList

AS

SET TRANSACTION ISOLATION LEVEL READ COMMITTED
SET NOCOUNT ON


SELECT  [ID] ,
            [ProductCategoryID] ,
            [PartNumber] ,
            [PartName] ,
            [UoM] ,
            [partnumberreff] ,
            [MaterialCategoryCode] ,
            [AltPartNumber] ,
            [AltPartName] ,
            [PartCode] ,
            [ModelCode] ,
            [SupplierCode] ,
            [TypeCode] ,
            [Stock] ,
            [RetalPrice] ,
            [PartStatus] ,
            [ActiveStatus] ,
            [AccessoriesType] ,
			[ProductType] ,
            [RowStatus] ,
            [CreatedBy] ,
            [CreatedTime] ,
            [LastUpdateBy] ,
            [LastUpdateTime]	
		FROM	
		[dbo].[SparePartMaster] 

SET NOCOUNT OFF
go

---------------------------------------------------------------------------------------------------------------
-- Date Created	: Thursday, August 04, 2011
-- Created By	: DNet Team by using CodeSmith v 2.6
-- Rev History	:
---------------------------------------------------------------------------------------------------------------

alter PROCEDURE up_RetrieveSparePartPO
	@ID int OUTPUT	
AS
SET TRANSACTION ISOLATION LEVEL READ COMMITTED

SET NOCOUNT ON

SELECT
	[ID],
	[PONumber],
	[OrderType],
	[DealerID],
	[PODate],
	[TermOfPaymentID],
	[TOPBlockStatusID],
	[DeliveryDate],
	[ProcessCode],
	[CancelRequestBy],
	[IndentTransfer],
	[PickingTicket],
	[SentPODate],
	[IsTransfer],
	[DMSPRNo],
	[RowStatus],
	[CreatedBy],
	[CreatedTime],
	[LastUpdateBy],
	[LastUpdateTime]	
FROM	[dbo].[SparePartPO]

WHERE
	[ID] = @ID

SET NOCOUNT OFF
go

---------------------------------------------------------------------------------------------------------------
-- Date Created	: Thursday, July 15, 2010
-- Created By	: DNet Team by using CodeSmith v 2.6
-- Rev History	:
---------------------------------------------------------------------------------------------------------------

alter PROCEDURE up_RetrieveSparePartPODetail
	@ID int OUTPUT	
AS
SET TRANSACTION ISOLATION LEVEL READ COMMITTED

SET NOCOUNT ON

SELECT
	[ID],
	[SparePartPOID],
	[SparePartMasterID],
	[CheckListStatus],
	[Quantity],
	[RetailPrice],
	[EstimateStatus],
	[StopMark],
    [TotalForecast],
	[RowStatus],
	[CreatedBy],
	[CreatedTime],
	[LastUpdateBy],
	[LastUpdateTime]
FROM	[dbo].[SparePartPODetail]

WHERE
	[ID] = @ID

SET NOCOUNT OFF
go

---------------------------------------------------------------------------------------------------------------
-- Date Created	: Thursday, July 15, 2010
-- Created By	: DNet Team by using CodeSmith v 2.6
-- Rev History	:
---------------------------------------------------------------------------------------------------------------

alter PROCEDURE up_RetrieveSparePartPODetailList

AS

SET TRANSACTION ISOLATION LEVEL READ COMMITTED
SET NOCOUNT ON


SELECT
		[ID],
		[SparePartPOID],
		[SparePartMasterID],
		[CheckListStatus],
		[Quantity],
		[RetailPrice],
		[EstimateStatus],
		[StopMark],
		[TotalForecast],
		[RowStatus],
		[CreatedBy],
		[CreatedTime],
		[LastUpdateBy],
		[LastUpdateTime]		
		FROM	
		[dbo].[SparePartPODetail] 

SET NOCOUNT OFF
go

---------------------------------------------------------------------------------------------------------------
-- Date Created	: Thursday, August 04, 2011
-- Created By	: DNet Team by using CodeSmith v 2.6
-- Rev History	:
---------------------------------------------------------------------------------------------------------------

alter PROCEDURE up_RetrieveSparePartPOList

AS

SET TRANSACTION ISOLATION LEVEL READ COMMITTED
SET NOCOUNT ON


SELECT
		[ID],
		[PONumber],
		[OrderType],
		[DealerID],
		[PODate],
		[TermOfPaymentID],
		[TOPBlockStatusID],
		[DeliveryDate],
		[ProcessCode],
		[CancelRequestBy],
		[IndentTransfer],
		[PickingTicket],
		[SentPODate],
		[IsTransfer],
		[DMSPRNo],
		[RowStatus],
		[CreatedBy],
		[CreatedTime],
		[LastUpdateBy],
		[LastUpdateTime]		
		FROM	
		[dbo].[SparePartPO] 

SET NOCOUNT OFF
go

---------------------------------------------------------------------------------------------------------------
-- Date Created	: Thursday, June 23, 2011
-- Created By	: DNet Team by using CodeSmith v 2.6
-- Rev History	:
---------------------------------------------------------------------------------------------------------------

alter PROCEDURE up_RetrieveSPKCustomer
	@ID int OUTPUT	
AS
SET TRANSACTION ISOLATION LEVEL READ COMMITTED

SET NOCOUNT ON

SELECT
	[ID],
	[Code],
	[ReffCode],
	[TipeCustomer],
	[TipePerusahaan],
	[Name1],
	[Name2],
	[Name3],
	[Alamat],
	[Kelurahan],
	[Kecamatan],
	[PostalCode],
	[PreArea],
	[CityID],
	[PrintRegion],
	[PhoneNo],
	[OfficeNo],
	[HomeNo],
	[HpNo],
	[Email],
	[Status],
	[MCPStatus],
	[LKPPStatus],
	[SAPCustomerID],
	[LKPPReference],
	[BusinessSectorDetailID],
	[ImagePath],
	[RowStatus],
	[CreatedTime],
	[CreatedBy],
	[LastUpdateTime],
	[LastUpdateBy]	
FROM	[dbo].[SPKCustomer]

WHERE
	[ID] = @ID

SET NOCOUNT OFF
go

---------------------------------------------------------------------------------------------------------------
-- Date Created	: Thursday, June 23, 2011
-- Created By	: DNet Team by using CodeSmith v 2.6
-- Rev History	:
---------------------------------------------------------------------------------------------------------------

alter PROCEDURE up_RetrieveSPKCustomerList

AS

SET TRANSACTION ISOLATION LEVEL READ COMMITTED
SET NOCOUNT ON


SELECT
		[ID],
		[Code],
		[ReffCode],
		[TipeCustomer],
		[TipePerusahaan],
		[Name1],
		[Name2],
		[Name3],
		[Alamat],
		[Kelurahan],
		[Kecamatan],
		[PostalCode],
		[PreArea],
		[CityID],
		[PrintRegion],
		[PhoneNo],
		[OfficeNo],
		[HomeNo],
		[HpNo],
		[Email],
		[Status],
		[MCPStatus],
		[LKPPStatus],
		[SAPCustomerID],
		[LKPPReference],
		[BusinessSectorDetailID],
		[ImagePath],
		[RowStatus],
		[CreatedTime],
		[CreatedBy],
		[LastUpdateTime],
		[LastUpdateBy]		
		FROM	
		[dbo].[SPKCustomer] 

SET NOCOUNT OFF
go

---------------------------------------------------------------------------------------------------------------
-- Date Created	: Monday, March 07, 2011
-- Created By	: DNet Team by using CodeSmith v 2.6
-- Rev History	:
---------------------------------------------------------------------------------------------------------------

alter PROCEDURE up_RetrieveSPKHeader
	@ID int OUTPUT	
AS
SET TRANSACTION ISOLATION LEVEL READ COMMITTED

SET NOCOUNT ON

SELECT
	[ID],
	[DealerID],
	[Status],
	[SPKNumber],
	[SPKReferenceNumber],
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
FROM	[dbo].[SPKHeader]

WHERE
	[ID] = @ID

SET NOCOUNT OFF
go

---------------------------------------------------------------------------------------------------------------
-- Date Created	: Monday, March 07, 2011
-- Created By	: DNet Team by using CodeSmith v 2.6
-- Rev History	:
---------------------------------------------------------------------------------------------------------------

alter PROCEDURE up_RetrieveSPKHeaderList

AS

SET TRANSACTION ISOLATION LEVEL READ COMMITTED
SET NOCOUNT ON


SELECT
		[ID],
		[DealerID],
		[Status],
		[SPKNumber],
		[SPKReferenceNumber],
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
go

---------------------------------------------------------------------------------------------------------------
-- Date Created	: 09 Februari 2018
-- Created By	: Mitrais Team by using CodeSmith v 2.6
-- Rev History	:
---------------------------------------------------------------------------------------------------------------

alter PROCEDURE up_RetrieveStandardCode
	@ID int OUTPUT	
AS
SET TRANSACTION ISOLATION LEVEL READ COMMITTED

SET NOCOUNT ON

SELECT
	[ID],
	[Category],
	[ValueId],
	[ValueCode]
	[ValueDesc],
	[Sequence],
	[RowStatus],
	[CreatedBy],
	[CreatedTime],
	[LastUpdateBy],
	[LastUpdateTime]	
FROM	[dbo].[StandardCode]

WHERE
	[ID] = @ID

SET NOCOUNT OFF
go
 
 
---------------------------------------------------------------------------------------------------------------
-- Date Created	: 09 Februari 2018
-- Created By	: Mitrais Team by using CodeSmith v 2.6
-- Rev History	:
---------------------------------------------------------------------------------------------------------------

alter PROCEDURE up_RetrieveStandardCodeList

AS

SET TRANSACTION ISOLATION LEVEL READ COMMITTED
SET NOCOUNT ON


SELECT
		[ID],
		[Category],
		[ValueId],
		[ValueCode],
		[ValueDesc],
		[RowStatus],
		[CreatedBy],
		[CreatedTime],
		[LastUpdateBy],
		[LastUpdateTime]		
		FROM	
		[dbo].[StandardCode] 

SET NOCOUNT OFF
go

 
go

---------------------------------------------------------------------------------------------------------------
-- Date Created	: Friday, November 18, 2005
-- Created By	: DNet Team by using CodeSmith v 2.6
-- Rev History	:
---------------------------------------------------------------------------------------------------------------

alter PROCEDURE up_RetrieveTrTrainee
	@ID int OUTPUT	
AS
SET TRANSACTION ISOLATION LEVEL READ COMMITTED

SET NOCOUNT ON

SELECT
	[ID],
	[SalesmanHeaderID],
	[Name],
	[DealerID],
	[DealerBranchID],
	[BirthDate],
	[Gender],
	[NoKTP],
	[Email],
	[StartWorkingDate],
	[Status],
	[JobPosition],
	[EducationLevel],
	[Photo],
	[ShirtSize],
	[RowStatus],
	[CreatedBy],
	[CreatedTime],
	[LastUpdateBy],
	[LastUpdateTime]	
FROM	[dbo].[TrTrainee]

WHERE
	[ID] = @ID

SET NOCOUNT OFF
go

---------------------------------------------------------------------------------------------------------------
-- Date Created	: Friday, November 18, 2005
-- Created By	: DNet Team by using CodeSmith v 2.6
-- Rev History	:
---------------------------------------------------------------------------------------------------------------

alter PROCEDURE up_RetrieveTrTraineeList

AS

SET TRANSACTION ISOLATION LEVEL READ COMMITTED
SET NOCOUNT ON


SELECT
		[ID],
		[SalesmanHeaderID],
		[Name],
		[DealerID],
		[DealerBranchID],
		[BirthDate],
		[Gender],
		[NoKTP],
		[Email],
		[StartWorkingDate],
		[Status],
		[JobPosition],
		[EducationLevel],
		[Photo],
		[ShirtSize],
		[RowStatus],
		[CreatedBy],
		[CreatedTime],
		[LastUpdateBy],
		[LastUpdateTime]		
		FROM	
		[dbo].[TrTrainee] 

SET NOCOUNT OFF
go

---------------------------------------------------------------------------------------------------------------
-- Date Created	: 17 Nopember 2005
-- Created By	: DNet Team by using CodeSmith v 2.6
-- Rev History	:
---------------------------------------------------------------------------------------------------------------

alter PROCEDURE up_RetrieveVechileModel
	@ID int OUTPUT
AS

SET TRANSACTION ISOLATION LEVEL READ COMMITTED

SET NOCOUNT ON

SELECT
	[ID],
	[VechileModelCode],
	[CategoryID],
	[Description],
	[VechileModelIndCode],
	[IndDescription],
	[SalesFlag],
	[RowStatus],
	[CreatedBy],
	[CreatedTime],
	[LastUpdateBy],
	[LastUpdateTime]
FROM	[dbo].[VechileModel]
WHERE
	[ID] = @ID

SET NOCOUNT OFF
go

---------------------------------------------------------------------------------------------------------------
-- Date Created	: 17 Nopember 2005
-- Created By	: DNet Team by using CodeSmith v 2.6
-- Rev History	:
---------------------------------------------------------------------------------------------------------------

alter PROCEDURE up_RetrieveVechileModelList

AS

SET TRANSACTION ISOLATION LEVEL READ COMMITTED
SET NOCOUNT ON

SELECT
		[ID],
		[VechileModelCode],
		[CategoryID],
		[Description],
		[VechileModelIndCode],
		[IndDescription],
		[SalesFlag],
		[RowStatus],
		[CreatedBy],
		[CreatedTime],
		[LastUpdateBy],
		[LastUpdateTime]
		FROM	
		[dbo].[VechileModel] 

SET NOCOUNT OFF
go

---------------------------------------------------------------------------------------------------------------
-- Date Created	: Tuesday, February 11, 2014
-- Created By	: DNet Team by using CodeSmith v 2.6
-- Rev History	:
---------------------------------------------------------------------------------------------------------------

alter PROCEDURE up_RetrieveVechileType
	@ID smallint OUTPUT	
AS
SET TRANSACTION ISOLATION LEVEL READ COMMITTED

SET NOCOUNT ON

SELECT
	[ID],
	[VechileTypeCode],
	[ModelID],
	[CategoryID],
	[ProductCategoryID],
	[Description],
	[Status],
	[VehicleClassID],
	[IsVehicleKind1],
	[IsVehicleKind2],
	[IsVehicleKind3],
	[IsVehicleKind4],
	[MaxTOPDays],
	[SAPModel],
		[SegmentType],
	    [VariantType],
	    [TransmitType],
	    [DriveSystemType],
	    [SpeedType],
	    [FuelType],
	[RowStatus],
	[CreatedBy],
	[CreatedTime],
	[LastUpdateBy],
	[LastUpdateTime]
FROM	[dbo].[VechileType]

WHERE
	[ID] = @ID

SET NOCOUNT OFF
go

---------------------------------------------------------------------------------------------------------------
-- Date Created	: Tuesday, February 11, 2014
-- Created By	: DNet Team by using CodeSmith v 2.6
-- Rev History	:
---------------------------------------------------------------------------------------------------------------

alter PROCEDURE up_RetrieveVechileTypeList

AS

SET TRANSACTION ISOLATION LEVEL READ COMMITTED
SET NOCOUNT ON


SELECT
		[ID],
		[VechileTypeCode],
		[ModelID],
		[CategoryID],
		[ProductCategoryID],
		[Description],
		[Status],
		[VehicleClassID],
		[IsVehicleKind1],
		[IsVehicleKind2],
		[IsVehicleKind3],
		[IsVehicleKind4],
		[MaxTOPDays],
		[SAPModel],
		[SegmentType],
	    [VariantType],
	    [TransmitType],
	    [DriveSystemType],
	    [SpeedType],
	    [FuelType],
		[RowStatus],
		[CreatedBy],
		[CreatedTime],
		[LastUpdateBy],
		[LastUpdateTime]		
		FROM	
		[dbo].[VechileType] 

SET NOCOUNT OFF
go

---------------------------------------------------------------------------------------------------------------  
-- Date Created : Thursday, September 05, 2013  
-- Created By : DNet Team by using CodeSmith v 2.6  
-- Rev History :  
---------------------------------------------------------------------------------------------------------------  
  
alter PROCEDURE up_RetrieveWSCDetail  
 @ID int OUTPUT   
AS  
SET TRANSACTION ISOLATION LEVEL READ COMMITTED  
  
SET NOCOUNT ON  
  
SELECT  
 [ID],  
 [WSCHeaderID],  
 [WSCType],  
 [LaborMasterID],  
 [PositionCode],  
 [WorkCode],  
 [SparePartMasterID],  
 [Quantity],  
 [PartPrice],  
 [MainPart],  
 [QuantityReceived],  
 [ReceivedBy],  
 [ReceivedDate],  
 [Status],  
 [RowStatus],  
 [CreatedBy],  
 [CreatedTime],  
 [LastUpdateBy],  
 [LastUpdateTime]  
FROM [dbo].[WSCDetail]  
  
WHERE  
 [ID] = @ID  
  
SET NOCOUNT OFF
go

---------------------------------------------------------------------------------------------------------------
-- Date Created	: Friday, November 18, 2011
-- Created By	: DNet Team by using CodeSmith v 2.6
-- Rev History	:
---------------------------------------------------------------------------------------------------------------

alter PROCEDURE up_RetrieveWSCDetailBB
	@ID int OUTPUT	
AS
SET TRANSACTION ISOLATION LEVEL READ COMMITTED

SET NOCOUNT ON

SELECT
	[ID],
	[WSCHeaderBBID],
	[WSCType],
	[LaborMasterID],
	[PositionCode],
	[WorkCode],
	[SparePartMasterID],
	[Quantity],
	[PartPrice],
	[MainPart],
	[QuantityReceived],
	[ReceivedBy],
	[ReceivedDate],
	[Status],
	[RowStatus],
	[CreatedBy],
	[CreatedTime],
	[LastUpdateBy],
	[LastUpdateTime]	
FROM	[dbo].[WSCDetailBB]

WHERE
	[ID] = @ID

SET NOCOUNT OFF
go

---------------------------------------------------------------------------------------------------------------
-- Date Created	: Friday, November 18, 2011
-- Created By	: DNet Team by using CodeSmith v 2.6
-- Rev History	:
---------------------------------------------------------------------------------------------------------------

alter PROCEDURE up_RetrieveWSCDetailBBList

AS

SET TRANSACTION ISOLATION LEVEL READ COMMITTED
SET NOCOUNT ON


SELECT
		[ID],
		[WSCHeaderBBID],
		[WSCType],
		[LaborMasterID],
		[PositionCode],
		[WorkCode],
		[SparePartMasterID],
		[Quantity],
		[PartPrice],
		[MainPart],
		[QuantityReceived],
		[ReceivedBy],
		[ReceivedDate],
		[Status],
		[RowStatus],
		[CreatedBy],
		[CreatedTime],
		[LastUpdateBy],
		[LastUpdateTime]		
		FROM	
		[dbo].[WSCDetailBB] 

SET NOCOUNT OFF
go

---------------------------------------------------------------------------------------------------------------
-- Date Created	: Thursday, September 05, 2013
-- Created By	: DNet Team by using CodeSmith v 2.6
-- Rev History	:
---------------------------------------------------------------------------------------------------------------

alter PROCEDURE up_RetrieveWSCDetailList

AS

SET TRANSACTION ISOLATION LEVEL READ COMMITTED
SET NOCOUNT ON


SELECT
		[ID],
		[WSCHeaderID],
		[WSCType],
		[LaborMasterID],
		[PositionCode],
		[WorkCode],
		[SparePartMasterID],
		[Quantity],
		[PartPrice],
		[MainPart],
		[QuantityReceived],
		[ReceivedBy],
		[ReceivedDate],
		[Status],
		[RowStatus],
		[CreatedBy],
		[CreatedTime],
		[LastUpdateBy]	
		FROM	
		[dbo].[WSCDetail]

SET NOCOUNT OFF
go

---------------------------------------------------------------------------------------------------------------    
-- Date Created : Tuesday, November 29, 2005    
-- Created By : DNet Team by using CodeSmith v 2.6    
-- Rev History :    
---------------------------------------------------------------------------------------------------------------    
    
alter PROCEDURE up_RetrieveWSCHeader    
 @ID int OUTPUT    
     
AS    
SET TRANSACTION ISOLATION LEVEL READ COMMITTED    
    
SET NOCOUNT ON    
    
SELECT    
 [ID],    
 [ClaimType],    
 [DealerID],  
 [DealerBranchID],  
 [ClaimNumber],    
 [RefClaimNumber],    
 [ChassisMasterID],   
 [FailureDate], 
 [ServiceDate],    
 [Miliage],    
 [PQR],    
 [PQRStatus],    
 [CodeA],    
 [CodeB],    
 [CodeC],    
 [Description],    
 [EvidencePhoto],    
 [EvidenceInvoice],    
 [EvidenceDmgPart],  
 [EvidenceRepair],
 [EvidenceWSCLetter],
 [EvidenceWSCTechnical],
 [Causes],
 [Results],
 [Notes],  
 [ReqDmgPart],    
 [ReqDmgPartBy],    
 [ReqDmgPartTime],    
 [NotificationNumber],    
 [DecideDate],    
 [Status],    
 [ClaimStatus],    
 [ReasonID],    
 [LaborAmount],    
 [PartAmount],    
 [PartReceiveBy],    
 [PartReceiveTime],    
 [DownLoadBy],    
 [DownLoadTime],    
 [ResponseTime],    
 [WorkOrderNumber],  
 [RowStatus],    
 [CreatedBy],    
 [CreatedTime],    
 [LastUpdateBy],    
 [LastUpdateTime]    
     
FROM [dbo].[WSCHeader]    
    
WHERE    
 [ID] = @ID    
    
SET NOCOUNT OFF
go

---------------------------------------------------------------------------------------------------------------    
-- Date Created : Friday, November 18, 2011    
-- Created By : DNet Team by using CodeSmith v 2.6    
-- Rev History :    
---------------------------------------------------------------------------------------------------------------    
    
alter PROCEDURE up_RetrieveWSCHeaderBB    
 @ID int OUTPUT     
AS    
SET TRANSACTION ISOLATION LEVEL READ COMMITTED    
    
SET NOCOUNT ON    
    
SELECT    
 [ID],    
 [ClaimType],    
 [DealerID],    
 [DealerBranchID],
 [ClaimNumber],    
 [RefClaimNumber],    
 [ChassisMasterBBID],
 [FailureDate],    
 [ServiceDate],    
 [Miliage],    
 [PQR],    
 [PQRStatus],    
 [CodeA],    
 [CodeB],    
 [CodeC],    
 [Description],    
 [EvidencePhoto],    
 [EvidenceInvoice],    
 [EvidenceDmgPart],   
 [EvidenceRepair],
 [EvidenceWSCLetter],
 [EvidenceWSCTechnical],
 [Causes],
 [Results],
 [Notes], 
 [ReqDmgPart],    
 [ReqDmgPartBy],    
 [ReqDmgPartTime],    
 [NotificationNumber],    
 [DecideDate],    
 [Status],    
 [ClaimStatus],    
 [ReasonID],    
 [LaborAmount],    
 [PartAmount],    
 [PartReceiveBy],    
 [PartReceiveTime],    
 [DownLoadBy],    
 [DownLoadTime],    
 [ResponseTime],    
 [WorkOrderNumber],  
 [RowStatus],    
 [CreatedBy],    
 [CreatedTime],    
 [LastUpdateBy],    
 [LastUpdateTime]     
FROM [dbo].[WSCHeaderBB]    
    
WHERE    
 [ID] = @ID    
    
SET NOCOUNT OFF
go

---------------------------------------------------------------------------------------------------------------    
-- Date Created : Friday, November 18, 2011    
-- Created By : DNet Team by using CodeSmith v 2.6    
-- Rev History :    
---------------------------------------------------------------------------------------------------------------    
    
alter PROCEDURE up_RetrieveWSCHeaderBBList    
    
AS    
    
SET TRANSACTION ISOLATION LEVEL READ COMMITTED    
SET NOCOUNT ON    
    
    
SELECT    
  [ID],    
  [ClaimType],    
  [DealerID],    
  [DealerBranchID],
  [ClaimNumber],    
  [RefClaimNumber],    
  [ChassisMasterBBID], 
  [FailureDate],   
  [ServiceDate],    
  [Miliage],    
  [PQR],    
  [PQRStatus],    
  [CodeA],    
  [CodeB],    
  [CodeC],    
  [Description],    
  [EvidencePhoto],    
  [EvidenceInvoice],    
  [EvidenceDmgPart],  
  [EvidenceRepair],
  [EvidenceWSCLetter],
  [EvidenceWSCTechnical],
  [Causes],
  [Results],
  [Notes],  
  [ReqDmgPart],    
  [ReqDmgPartBy],    
  [ReqDmgPartTime],    
  [NotificationNumber],    
  [DecideDate],    
  [Status],    
  [ClaimStatus],    
  [ReasonID],    
  [LaborAmount],    
  [PartAmount],    
  [PartReceiveBy],    
  [PartReceiveTime],    
  [DownLoadBy],    
  [DownLoadTime],    
  [ResponseTime],    
  [WorkOrderNumber],  
  [RowStatus],    
  [CreatedBy],    
  [CreatedTime],    
  [LastUpdateBy],    
  [LastUpdateTime]      
  FROM     
  [dbo].[WSCHeaderBB]     
    
SET NOCOUNT OFF
go

---------------------------------------------------------------------------------------------------------------    
-- Date Created : Tuesday, November 29, 2005    
-- Created By : DNet Team by using CodeSmith v 2.6    
-- Rev History :    
---------------------------------------------------------------------------------------------------------------    
    
alter PROCEDURE up_RetrieveWSCHeaderList    
    
AS    
    
SET TRANSACTION ISOLATION LEVEL READ COMMITTED    
SET NOCOUNT ON    
    
SELECT    
  [ID],    
  [ClaimType],    
  [DealerID],    
  [DealerBranchID],
  [ClaimNumber],    
  [RefClaimNumber],    
  [ChassisMasterID], 
  [FailureDate],   
  [ServiceDate],    
  [Miliage],    
  [PQR],    
  [PQRStatus],    
  [CodeA],    
  [CodeB],    
  [CodeC],    
  [Description],    
  [EvidencePhoto],    
  [EvidenceInvoice],    
  [EvidenceDmgPart],  
  [EvidenceRepair],  
  [EvidenceWSCLetter],  
  [EvidenceWSCTechnical],  
  [Causes],  
  [Results],  
  [Notes],      
  [ReqDmgPart],    
  [ReqDmgPartBy],    
  [ReqDmgPartTime],    
  [NotificationNumber],    
  [DecideDate],    
  [Status],    
  [ClaimStatus],    
  [ReasonID],    
  [LaborAmount],    
  [PartAmount],    
  [PartReceiveBy],    
  [PartReceiveTime],    
  [DownLoadBy],    
  [DownLoadTime],    
  [ResponseTime],   
  [WorkOrderNumber],   
  [RowStatus],    
  [CreatedBy],    
  [CreatedTime],    
  [LastUpdateBy],    
  [LastUpdateTime]    
      
  FROM     
  [dbo].[WSCHeader]     
    
SET NOCOUNT OFF
go

commit
go









 
GO
/****** Object:  StoredProcedure [dbo].[up_RetrieveFreeServiceBB]    Script Date: 1/10/2019 8:26:02 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

---------------------------------------------------------------------------------------------------------------  
-- Date Created : Wednesday, October 19, 2011  
-- Created By : DNet Team by using CodeSmith v 2.6  
-- Rev History :  
---------------------------------------------------------------------------------------------------------------  
  
ALTER PROCEDURE [dbo].[up_RetrieveFreeServiceBB]  
 @ID int OUTPUT   
AS  
SET TRANSACTION ISOLATION LEVEL READ COMMITTED  
  
SET NOCOUNT ON  
  
SELECT  
 a.ID ,
 a.Status ,
 a.ChassisMasterID ,
 a.FSKindID ,
 a.MileAge ,
 a.ServiceDate ,
 a.ServiceDealerID ,
 a.DealerBranchID ,
 a.SoldDate ,
 a.NotificationNumber ,
 a.NotificationType ,
 a.TotalAmount ,
 a.LabourAmount ,
 a.PartAmount ,
 a.PPNAmount ,
 a.PPHAmount ,
 a.Reject ,
 a.Reason ,
 a.ReleaseBy ,
 a.ReleaseDate ,
 a.VisitType ,
 a.WorkOrderNumber ,
 a.RowStatus ,
 a.CreatedBy ,
 a.CreatedTime ,
 a.LastUpdateBy ,
 a.LastUpdateTime 
FROM [dbo].[FreeServiceBB]   a
  
WHERE  
 [ID] = @ID  
  
SET NOCOUNT OFF

go

ALTER PROCEDURE [dbo].[up_RetrieveVWI_BusinessSectorList]

AS

SET TRANSACTION ISOLATION LEVEL READ COMMITTED
SET NOCOUNT ON


SELECT
		[ID],
		[BusinessSectorName],
		[BusinessDomain],
		[BusinessName],
		[LastUpdateTime]
		--[Code]		
		FROM	
		[dbo].[VWI_BusinessSector] 

SET NOCOUNT OFF

GO

ALTER PROCEDURE [dbo].[up_RetrieveVWI_BusinessSector]
	@ID int OUTPUT	
AS
SET TRANSACTION ISOLATION LEVEL READ COMMITTED

SET NOCOUNT ON

SELECT
	[ID],
	[BusinessSectorName],
	[BusinessDomain],
	[BusinessName],
	[LastUpdateTime]
	--[Code]	
FROM	[dbo].[VWI_BusinessSector]

WHERE
	[ID] = @ID

SET NOCOUNT OFF



GO