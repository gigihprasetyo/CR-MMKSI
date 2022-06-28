set xact_abort on
go

begin transaction
go

set ANSI_NULLS on
go

---------------------------------------------------------------------------------------------------------------
-- Date Created	: Thursday, September 29, 2005
-- Created By	: DNet Team by using CodeSmith v 2.6
-- Rev History	:
---------------------------------------------------------------------------------------------------------------

alter PROCEDURE up_ValidateArea1
	@Result	varchar(1000),
	@ID int OUTPUT,
	@AreaCode varchar(10),
	@Description varchar(40),
	@PICSales varchar(50),
	@PICServices varchar(50),
	@PICSpareparts varchar(50),
	@MainAreaID INT,
	@RowStatus smallint,
	@CreatedBy varchar(20),
	@CreatedTime datetime,
	@LastUpdateBy varchar(20),
	@LastUpdateTime datetime
AS

SET	@Result = ''
go

set ANSI_NULLS off
go

--/****** Object:  Stored Procedure dbo.up_ValidateArea2    Script Date: 14/10/2005 11:06:14 ******/
---------------------------------------------------------------------------------------------------------------
-- Date Created	: Thursday, September 29, 2005
-- Created By	: DNet Team by using CodeSmith v 2.6
-- Rev History	:
---------------------------------------------------------------------------------------------------------------

alter PROCEDURE up_ValidateArea2
	@Result	varchar(1000),
	@ID int OUTPUT,
	@AreaCode varchar(10),
	@Description varchar(40),
	@ACFinishUnit varchar(50),
	@ACSparePart varchar(50),
	@ACService varchar(50),
	@Area1ID Int,
	@RowStatus smallint,
	@CreatedBy varchar(20),
	@CreatedTime datetime,
	@LastUpdateBy varchar(20),
	@LastUpdateTime datetime
AS

SET	@Result = ''
go

set ANSI_NULLS on
go

---------------------------------------------------------------------------------------------------------------
-- Date Created	: Wednesday, January 17, 2018
-- Created By	: DNet Team by using CodeSmith v 2.6
-- Rev History	:
---------------------------------------------------------------------------------------------------------------

alter PROCEDURE up_ValidateAssistPartSales
	@Result	varchar(1000),
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
	@DealerBranchID INT,
	@DealerBranchCode varchar(50),
	@RemarksSystem varchar(MAX),
	@StatusAktif smallint,
	@ValidateSystemStatus smallint,
	@RowStatus smallint,
	@CreatedBy varchar(50),
	@CreatedTime datetime,
	@LastUpdateBy varchar(50),
	@LastUpdateTime datetime	
AS

SET	@Result = ''
go

---------------------------------------------------------------------------------------------------------------
-- Date Created	: Wednesday, January 17, 2018
-- Created By	: DNet Team by using CodeSmith v 2.6
-- Rev History	:
---------------------------------------------------------------------------------------------------------------

alter PROCEDURE up_ValidateAssistPartStock
	@Result	varchar(1000),
	@ID int OUTPUT,
	@AssistUploadLogID int,
	@Month nchar(10),
	@Year nchar(10),
	@DealerID int,
	@DealerCode varchar(30),
	@SparepartMasterID int,
	@NoParts varchar(50),
	@JumlahStokAwal float,
	@JumlahDatang float,
	@HargaBeli money,
	@DealerBranchID INT,
	@DealerBranchCode varchar(50),
	@RemarksSystem varchar(MAX),
	@StatusAktif smallint,
	@ValidateSystemStatus smallint,
	@RowStatus smallint,
	@CreatedBy varchar(20),
	@CreatedTime datetime,
	@LastUpdateBy varchar(20),
	@LastUpdateTime datetime	
AS

SET	@Result = ''
go

---------------------------------------------------------------------------------------------------------------
-- Date Created	: Wednesday, January 17, 2018
-- Created By	: DNet Team by using CodeSmith v 2.6
-- Rev History	:
---------------------------------------------------------------------------------------------------------------

alter PROCEDURE up_ValidateAssistServiceIncoming
	@Result	varchar(1000),
	@ID int OUTPUT,
	@AssistUploadLogID int,
	@TglBukaTransaksi date,
	@WaktuMasuk time,
	@TglTutupTransaksi date,
	@WaktuKeluar time,
	@DealerID int,
	@DealerCode varchar(50),
	@TrTraineMekanikID int,
	@KodeMekanik varchar(50),
	@NoWorkOrder varchar(30),
	@ChassisMasterID int,
	@KodeChassis varchar(50),
	@WorkOrderCategoryID int,
	@WorkOrderCategoryCode varchar(50),
	@KMService int,
	@ServicePlaceID int,
	@ServicePlaceCode varchar(50),
	@ServiceTypeID int,
	@ServiceTypeCode varchar(50),
	@TotalLC money,
	@MetodePembayaran varchar(50),
	@Model varchar(100),
	@Transmition varchar(30),
	@DriveSystem varchar(20),
	@DealerBranchID int,
	@DealerBranchCode varchar(50),
	@RemarksSystem varchar(MAX),
	@RemarksSpecial varchar(MAX),
	@RemarksBM varchar(MAX),
	@StatusAktif smallint,
	@ValidateSystemStatus smallint,
	@RowStatus smallint,
	@CreatedBy varchar(20),
	@CreatedTime datetime,
	@LastUpdateBy varchar(20),
	@LastUpdateTime datetime	
AS

SET	@Result = ''
go

---------------------------------------------------------------------------------------------------------------
-- Date Created	: Thursday, June 23, 2011
-- Created By	: DNet Team by using CodeSmith v 2.6
-- Rev History	:
---------------------------------------------------------------------------------------------------------------
alter PROCEDURE up_ValidateEndCustomer
	@Result	varchar(1000),
	@ID int OUTPUT,
	@ProjectIndicator varchar(1),
	@RefChassisNumberID int,
	@CustomerID int,
	@Name1 varchar(50),
	@FakturDate datetime,
	@OpenFakturDate datetime,
	@FakturNumber varchar(18),
	@AreaViolationFlag varchar(50),
	@AreaViolationPaymentMethodID tinyint,
	@AreaViolationyAmount money,
	@AreaViolationBankName varchar(30),
	@AreaViolationGyroNumber varchar(30),
	@PenaltyFlag varchar(50),
	@PenaltyPaymentMethodID tinyint,
	@PenaltyAmount money,
	@PenaltyBankName varchar(30),
	@PenaltyGyroNumber varchar(30),
	@ReferenceLetterFlag varchar(1),
	@ReferenceLetter varchar(40),
	@SaveBy varchar(20),
	@SaveTime datetime,
	@ValidateBy varchar(20),
	@ValidateTime datetime,
	@ConfirmBy varchar(20),
	@ConfirmTime datetime,
	@DownloadBy varchar(20),
	@DownloadTime datetime,
	@PrintedBy varchar(20),
	@PrintedTime datetime,
	@CleansingCustomerID int,
	@MCPHeaderID int,
	@MCPStatus SMALLINT,
	@LKPPHeaderID int,
	@LKPPStatus SMALLINT,
	@Remark1 varchar(255),
	@Remark2 varchar(255),
	@HandoverDate datetime,
	@IsTemporary smallint,
	@RowStatus smallint,
	@CreatedBy varchar(20),
	@CreatedTime datetime,
	@LastUpdateBy varchar(20),
	@LastUpdateTime datetime	
AS
SET	@Result = ''

SET ANSI_NULLS OFF
go

---------------------------------------------------------------------------------------------------------------
-- Date Created	: Tuesday, October 04, 2016
-- Created By	: DNet Team by using CodeSmith v 2.6
-- Rev History	:
-- Date Created	: 14 Maret 2018
-- Created By	: Mitrais Team
-- Rev History	:
---------------------------------------------------------------------------------------------------------------

alter PROCEDURE up_ValidateFreeService
	@Result	varchar(1000),
	@ID int OUTPUT,
	@Status varchar(1),
	@ChassisMasterID int,
	@FSKindID tinyint,
	@MileAge int,
	@ServiceDate smalldatetime,
	@ServiceDealerID smallint,
	@DealerBranchID smallint,
	@SoldDate smalldatetime,
	@NotificationNumber varchar(20),
	@NotificationType varchar(2),
	@TotalAmount money,
	@LabourAmount money,
	@PartAmount money,
	@PPNAmount money,
	@PPHAmount money,
	@Reject varchar(4),
	@Reason smallint,
	@ReleaseBy varchar(20),
	@ReleaseDate datetime,
	@FleetRequestID int,
	@WorkOrderNumber varchar(50),
	@RowStatus smallint,
	@CreatedBy varchar(20),
	@CreatedTime datetime,
	@LastUpdateBy varchar(20),
	@LastUpdateTime datetime	
AS

SET	@Result = ''
go

---------------------------------------------------------------------------------------------------------------  
-- Date Created : Wednesday, October 19, 2011  
-- Created By : DNet Team by using CodeSmith v 2.6  
-- Rev History :  
---------------------------------------------------------------------------------------------------------------  
  
alter PROCEDURE up_ValidateFreeServiceBB  
 @Result varchar(1000),  
 @ID int OUTPUT,  
 @Status varchar(1),  
 @ChassisMasterID int,  
 @FSKindID tinyint,  
 @MileAge int,  
 @ServiceDate datetime,  
 @ServiceDealerID smallint,  
 @SoldDate datetime,  
 @NotificationNumber varchar(20),  
 @NotificationType varchar(2),  
 @TotalAmount money,  
 @LabourAmount money,  
 @PartAmount money,  
 @PPNAmount money,  
 @PPHAmount money,  
 @Reject varchar(4),  
 @Reason smallint,  
 @ReleaseBy varchar(20),  
 @ReleaseDate datetime,  
 @VisitType VARCHAR(20),
 @RowStatus smallint,  
 @CreatedBy varchar(20),  
 @CreatedTime datetime,  
 @LastUpdateBy varchar(20),  
 @LastUpdateTime datetime   
AS  
  
SET @Result = ''
go

--/****** Object:  Stored Procedure dbo.up_ValidateFSKind    Script Date: 14/10/2005 11:06:15 ******/  
---------------------------------------------------------------------------------------------------------------  
-- Date Created : Thursday, October 13, 2005  
-- Created By : DNet Team by using CodeSmith v 2.6  
-- Rev History :  
---------------------------------------------------------------------------------------------------------------  
  
alter PROCEDURE up_ValidateFSKind  
 @Result varchar(1000),  
 @ID int OUTPUT,  
 @KindCode varchar(3),  
 @KM int,  
 @KindDescription varchar(30),  
 @FSType varchar(30),
 @Status smallint,
 @RowStatus smallint,  
 @CreatedBy varchar(20),  
 @CreatedTime datetime,  
 @LastUpdateBy varchar(20),  
 @LastUpdateTime datetime  
AS  
  
SET @Result = ''
go

---------------------------------------------------------------------------------------------------------------
-- Date Created	: 01 Oktober 2007
-- Created By	: DNet Team by using CodeSmith v 2.6
-- Rev History	:
---------------------------------------------------------------------------------------------------------------

alter PROCEDURE up_ValidateIndentPartDetail
	@Result	varchar(1000),
	@ID int OUTPUT,
	@IndentPartHeaderID int,
	@SparePartMasterID int,
	@TotalForecast int,
	@Qty int,
	@Description varchar(255),
	@AllocationQty int,
	@IsCompletedAllocation tinyint,
	@Price money,
	@RowStatus smallint,
	@CreatedBy varchar(20),
	@CreatedTime datetime,
	@LastUpdateBy varchar(20),
	@LastUpdateTime datetime
	
AS

SET	@Result = ''
go

---------------------------------------------------------------------------------------------------------------
-- Date Created	: Friday, December 12, 2008
-- Created By	: DNet Team by using CodeSmith v 2.6
-- Rev History	:
---------------------------------------------------------------------------------------------------------------

alter PROCEDURE up_ValidateIndentPartHeader
	@Result	varchar(1000),
	@ID int OUTPUT,
	@DealerID int,
	@RequestNo varchar(13),
	@RequestDate datetime,
	@MaterialType int,
	@TermOfPaymentID int,
	@TOPBlockStatusID int,
	@Status tinyint,
	@StatusKTB tinyint,
	@SubmitFile varchar(50),
	@PaymentType tinyint,
	@Price money,
	@KTBConfirmedDate datetime,
	@DescID tinyint,
	@ChassisNumber varchar(20),
	@DMSPRNo varchar(20),
	@RowStatus smallint,
	@CreatedBy varchar(20),
	@CreatedTime datetime,
	@LastUpdateBy varchar(20),
	@LastUpdateTime datetime	
AS

SET	@Result = ''
go

---------------------------------------------------------------------------------------------------------------
-- Date Created	: 11 Nopember 2016
-- Created By	: DNet Team by using CodeSmith v 2.6
-- Rev History	:
---------------------------------------------------------------------------------------------------------------

alter PROCEDURE up_ValidateMainArea
	@Result	varchar(1000),
	@ID int OUTPUT,
	@AreaCode varchar(20),
	@Description varchar(50),
	@PICSales varchar(50),
	@PICServices varchar(50),
	@PICSpareparts varchar(50),
	@RowStatus smallint,
	@CreatedBy varchar(20),
	@CreatedTime datetime,
	@LastUpdateBy varchar(20),
	@LastUpdateTime datetime	
AS

SET	@Result = ''
go

---------------------------------------------------------------------------------------------------------------
-- Date Created	: Wednesday, November 30, 2005
-- Created By	: DNet Team by using CodeSmith v 2.6
-- Rev History	:
-- Date Created	: 12 Maret 2018
-- Created By	: Mitrais Team
-- Rev History	:
---------------------------------------------------------------------------------------------------------------

alter PROCEDURE up_ValidatePDI
	@Result	varchar(1000),
	@ID int OUTPUT,
	@ChassisMasterID int,
	@DealerID smallint,
	@DealerBranchID int,
	@Kind char(1),
	@PDIStatus char(1),
	@PDIDate datetime,
	@ReleaseBy varchar(20),
	@ReleaseDate datetime,
	@WorkOrderNumber varchar(50),
	@RowStatus smallint,
	@CreatedBy varchar(20),
	@CreatedTime datetime,
	@LastUpdateBy varchar(20),
	@LastUpdateTime datetime	
AS

SET	@Result = ''
go

---------------------------------------------------------------------------------------------------------------
-- Date Created	: Wednesday, August 01, 2007
-- Created By	: DNet Team by using CodeSmith v 2.6
-- Rev History	:
---------------------------------------------------------------------------------------------------------------

alter PROCEDURE up_ValidatePMHeader
	@Result	varchar(1000),
	@ID int OUTPUT,
	@DealerID int,
	@DealerBranchID int,
	@ChassisNumberID int,
	@PMKindID int,
	@StandKM int,
	@ServiceDate datetime,
	@ReleaseDate datetime,
	@PMStatus varchar(4),
	@EntryType varchar(20),
	@WorkOrderNumber varchar(20),
	@BookingNo varchar(5),
	@VisitType varchar(5),
	@Remarks varchar(250),
	@RowStatus smallint,
	@CreatedBy varchar(20),
	@CreatedTime datetime,
	@LastUpdateBy varchar(20),
	@LastUpdateTime datetime
	
AS

SET	@Result = ''
go

---------------------------------------------------------------------------------------------------------------    
-- Date Created : Monday, July 23, 2007    
-- Created By : DNet Team by using CodeSmith v 2.6    
-- Rev History :    
---------------------------------------------------------------------------------------------------------------    
    
alter PROCEDURE up_ValidatePQRHeader    
 @Result varchar(1000),    
 @ID int OUTPUT,    
 @PQRNo varchar(25),    
 @PQRType int,
 @RefPQRNo varchar(25),    
 @DealerID int,    
 @DealerBranchID int,
 @Year int,    
 @SeqNo int,    
 @CategoryID varchar(5),    
 @DocumentDate datetime,    
 @SoldDate datetime,    
 @ChassisMasterID int,    
 @PQRDate datetime,    
 @OdoMeter int,    
 @Velocity int,    
 @CustomerName varchar(40),    
 @CustomerAddress varchar(100),    
 @ValidationTime datetime,    
 @ConfirmBy varchar(20),    
 @ConfirmTime datetime,    
 @RealeseTime datetime,    
 @IntervalProcess datetime,    
 @Complexity smallint,    
 @Subject varchar(50),    
 @Symptomps varchar(1000),    
 @Causes varchar(1000),    
 @Results varchar(1000),    
 @Notes varchar(1000),    
 @Solutions varchar(1000),    
 @Bobot int,    
 @ReleaseBy varchar(50),    
 @FinishBy varchar(50),    
 @FinishDate datetime,    
 @CodeA varchar(4),    
 @CodeB varchar(4),    
 @CodeC varchar(4),    
 @WorkOrderNumber varchar(50),  
 @RowStatus smallint,    
 @CreatedBy varchar(20),    
 @CreatedTime datetime,    
 @LastUpdateBy varchar(20),    
 @LastUpdateTime datetime    
     
AS    
SET @Result = ''
go

---------------------------------------------------------------------------------------------------------------    
-- Date Created : Monday, January 16, 2012    
-- Created By : DNet Team by using CodeSmith v 2.6    
-- Rev History :    
---------------------------------------------------------------------------------------------------------------    
    
alter PROCEDURE up_ValidatePQRHeaderBB    
 @Result varchar(1000),    
 @ID int OUTPUT,    
 @PQRNo varchar(25),    
 @PQRType int,
 @RefPQRNo varchar(25),    
 @DealerID smallint,  
 @DealerBranchID int,  
 @Year int,    
 @SeqNo int,    
 @CategoryID tinyint,    
 @DocumentDate datetime,    
 @SoldDate datetime,    
 @ChassisMasterBBID int,    
 @PQRDate datetime,    
 @OdoMeter int,    
 @Velocity int,    
 @CustomerName varchar(40),    
 @CustomerAddress varchar(100),    
 @ValidationTime datetime,    
 @ConfirmBy varchar(20),    
 @ConfirmTime datetime,    
 @RealeseTime datetime,    
 @IntervalProcess datetime,    
 @Complexity smallint,    
 @Subject varchar(50),    
 @Symptomps varchar(1000),    
 @Causes varchar(1000),    
 @Results varchar(1000),    
 @Notes varchar(1000),    
 @Solutions varchar(1000),    
 @Bobot int,    
 @ReleaseBy varchar(50),    
 @FinishBy varchar(50),    
 @FinishDate datetime,    
 @CodeA varchar(4),    
 @CodeB varchar(4),    
 @CodeC varchar(4),    
 @WorkOrderNumber varchar(50),  
 @RowStatus smallint,    
 @CreatedBy varchar(20),    
 @CreatedTime datetime,    
 @LastUpdateBy varchar(20),    
 @LastUpdateTime datetime     
AS    
    
SET @Result = ''
go

---------------------------------------------------------------------------------------------------------------  
-- Date Created : 19 April 2016  
-- Created By : DNet Team by using CodeSmith v 2.6  
-- Rev History :  
---------------------------------------------------------------------------------------------------------------  
  
alter PROCEDURE up_ValidateRecallService  
 @Result varchar(1000),  
 @ID int OUTPUT,  
 @ChassisMasterID int, 
 @RecallChassisMasterID int,  
 @MileAge int,  
 @ServiceDate datetime,  
 @ServiceDealerID smallint,  
 @DealerBranchID int,   
 @WorkOrderNumber varchar(50),  
 @RowStatus smallint,  
 @CreatedBy varchar(20),  
 @CreatedTime datetime,  
 @LastUpdateBy varchar(20),  
 @LastUpdateTime datetime   
AS  
  
SET @Result = ''
go

---------------------------------------------------------------------------------------------------------------
-- Date Created	: Thursday, July 13, 2017
-- Created By	: DNet Team by using CodeSmith v 2.6
-- Rev History	:
---------------------------------------------------------------------------------------------------------------

alter PROCEDURE up_ValidateSAPCustomer
	@Result	varchar(1000),
	@ID int OUTPUT,
	@SalesforceID varchar(50),
	@DealerID smallint,
	@SalesmanHeaderID smallint,
	@VechileTypeID smallint,
	@CustomerCode varchar(8),
	@CustomerName varchar(50),
	@CustomerType smallint,
	@CustomerAddress varchar(100),
	@Phone varchar(30),
	@Email varchar(50),
	@Sex tinyint,
	@AgeSegment tinyint,
	@CustomerPurpose smallint,
	@InformationType smallint,
	@InformationSource smallint,
	@Status tinyint,
	@Qty int,
	@ProspectDate datetime,
	@isSPK bit,
	@CurrVehicleBrand varchar(50),
	@CurrVehicleType varchar(50),
	@Note varchar(100),
	@WebID nvarchar(20),
    @BirthDate date,
	@PreferedVehicleModel varchar(100),
	@Description varchar(2000),
	@EstimatedCloseDate date,
	@OriginatingLeadId uniqueidentifier,
	@StatusCode smallint,
	@LeadStatus tinyint,
	@StateCode tinyint,
	@CampaignName varchar(50),
	@BusinessSectorDetailID int,
	@RowStatus smallint,
	@CreatedBy varchar(20),
	@CreatedTime datetime,
	@LastUpdateBy varchar(20),
	@LastUpdateTime datetime		
AS

SET	@Result = ''
go

---------------------------------------------------------------------------------------------------------------
-- Date Created	: Tuesday, July 24, 2012
-- Created By	: DNet Team by using CodeSmith v 2.6
-- Rev History	:
---------------------------------------------------------------------------------------------------------------

alter PROCEDURE up_ValidateSparePartMaster
	@Result	varchar(1000),
	@ID int OUTPUT,
	@ProductCategoryID smallint,
	@PartNumber varchar(18),
	@PartName varchar(30),
	@AltPartNumber varchar(18),
	@AltPartName varchar(30),
	@PartCode varchar(1),
	@ModelCode varchar(9),
	@SupplierCode varchar(10),
	@TypeCode varchar(5),
	@Stock int,
	@RetalPrice money,
	@PartStatus varchar(1),
	@ActiveStatus smallint,
	@AccessoriesType smallint,
	@ProductType varchar(100),
	@RowStatus smallint,
	@CreatedBy varchar(20),
	@CreatedTime datetime,
	@LastUpdateBy varchar(20),
	@LastUpdateTime datetime	
AS

SET	@Result = ''
go

---------------------------------------------------------------------------------------------------------------  
-- Date Created : Thursday, August 04, 2011  
-- Created By : DNet Team by using CodeSmith v 2.6  
-- Rev History :  Mitrais -- Add DMSPRNO
---------------------------------------------------------------------------------------------------------------  

alter PROCEDURE up_ValidateSparePartPO
	@Result	varchar(1000),
	@ID int OUTPUT,
	@PONumber varchar(15),
	@OrderType varchar(1),
	@DealerID smallint,
	@PODate smalldatetime,
	@TermOfPaymentID int,
	@TOPBlockStatus int,
	@DeliveryDate datetime,
	@ProcessCode varchar(1),
	@CancelRequestBy varchar(20),
	@IndentTransfer tinyint,
	@PickingTicket varchar(100),
	@SentPODate datetime,
	@IsTransfer bit,
	@DMSPRNo varchar(50),
	@RowStatus smallint,
	@CreatedBy varchar(20),
	@CreatedTime datetime,
	@LastUpdateBy varchar(20),
	@LastUpdateTime datetime	
AS

SET	@Result = ''
go

---------------------------------------------------------------------------------------------------------------
-- Date Created	: Thursday, July 15, 2010
-- Created By	: DNet Team by using CodeSmith v 2.6
-- Rev History	:
---------------------------------------------------------------------------------------------------------------

alter PROCEDURE up_ValidateSparePartPODetail
	@Result	varchar(1000),
	@ID int OUTPUT,
	@SparePartPOID int,
	@SparePartMasterID int,
	@CheckListStatus varchar(2),
	@Quantity int,
	@RetailPrice money,
	@EstimateStatus varchar(1),
	@StopMark smallint,
	@TotalForecast int,
    @RowStatus smallint,
	@CreatedBy varchar(20),
	@CreatedTime datetime,
	@LastUpdateBy varchar(20),
	@LastUpdateTime datetime
	
AS

SET	@Result = ''
go

---------------------------------------------------------------------------------------------------------------
-- Date Created	: 09 Februari 2018
-- Created By	: DNet Team by using CodeSmith v 2.6
-- Rev History	:
---------------------------------------------------------------------------------------------------------------

alter PROCEDURE up_ValidateStandardCode
	@Result	varchar(1000),
	@ID int OUTPUT,
	@Category varchar(100),
	@ValueId int,
	@ValueCode varchar(200),
	@ValueDesc varchar(200),
	@Sequence int,
	@RowStatus smallint,
	@CreatedBy varchar(20),
	@CreatedTime datetime,
	@LastUpdateBy varchar(20),
	@LastUpdateTime datetime	
AS

SET	@Result = ''
go


go

---------------------------------------------------------------------------------------------------------------
-- Date Created	: Friday, November 18, 2005
-- Created By	: DNet Team by using CodeSmith v 2.6
-- Rev History	: Mitrais Team
---------------------------------------------------------------------------------------------------------------

alter PROCEDURE up_ValidateTrTrainee
	@Result	varchar(1000),
	@ID int OUTPUT,
	@SalesmanHeaderID smallint,
	@Name varchar(50),
	@DealerID smallint,
	@DealerBranchID int,
	@BirthDate datetime,
	@Gender tinyint,
	@NoKTP varchar(20),
	@Email varchar(50),
	@StartWorkingDate datetime,
	@Status char(1),
	@JobPosition varchar(50),
	@EducationLevel varchar(50),
	@Photo image,
	@ShirtSize varchar(10),
	@RowStatus smallint,
	@CreatedBy varchar(20),
	@CreatedTime datetime,
	@LastUpdateBy varchar(20),
	@LastUpdateTime datetime
AS

SET	@Result = ''
go

---------------------------------------------------------------------------------------------------------------
-- Date Created	: 17 Nopember 2005
-- Created By	: DNet Team by using CodeSmith v 2.6
-- Rev History	:
---------------------------------------------------------------------------------------------------------------

alter PROCEDURE up_ValidateVechileModel
	@Result	varchar(1000),
	@ID int OUTPUT,
	@VechileModelCode varchar(4),
	@CategoryID int,
	@Description varchar(40),
	@VechileModelIndCode varchar(30),
	@IndDescription varchar(40),
	@SalesFlag tinyint,
	@RowStatus smallint,
	@CreatedBy varchar(20),
	@CreatedTime datetime,
	@LastUpdateBy varchar(20),
	@LastUpdateTime datetime
AS

SET	@Result = ''
go

---------------------------------------------------------------------------------------------------------------
-- Date Created	: Tuesday, February 11, 2014
-- Created By	: DNet Team by using CodeSmith v 2.6
-- Rev History	:
---------------------------------------------------------------------------------------------------------------

alter PROCEDURE up_ValidateVechileType
	@Result	varchar(1000),
	@ID smallint OUTPUT,
	@VechileTypeCode varchar(4),
	@ModelID smallint,
	@CategoryID tinyint,
	@ProductCategoryID smallint,
	@Description varchar(80),
	@Status varchar(1),
	@VehicleClassID int,
	@IsVehicleKind1 tinyint,
	@IsVehicleKind2 tinyint,
	@IsVehicleKind3 tinyint,
	@IsVehicleKind4 tinyint,
	@MaxTOPDays int,
	@SAPModel nvarchar(40),
	@SegmentType varchar(40),
    @VariantType varchar(30),
    @TransmitType varchar(25),
    @DriveSystemType varchar(25),
    @SpeedType varchar(2),
    @FuelType varchar(10),
	@RowStatus smallint,
	@CreatedBy varchar(20),
	@CreatedTime datetime,
	@LastUpdateBy varchar(20),
	@LastUpdateTime datetime	
AS

SET	@Result = ''
go

---------------------------------------------------------------------------------------------------------------  
-- Date Created : Friday, November 18, 2011  
-- Created By : DNet Team by using CodeSmith v 2.6  
-- Rev History :  
---------------------------------------------------------------------------------------------------------------  
  
alter PROCEDURE up_ValidateWSCDetailBB  
    @Result	varchar(1000),
	@ID int OUTPUT,
	@WSCHeaderBBID int,
	@WSCType varchar(1),
	@LaborMasterID int,
	@PositionCode varchar(10),
	@WorkCode varchar(6),
	@SparePartMasterID int,
	@Quantity real,
	@PartPrice money,
	@MainPart smallint,
	@QuantityReceived real,
	@ReceivedBy varchar(20),
	@ReceivedDate datetime,
	@Status smallint,
	@RowStatus smallint,
	@CreatedBy varchar(20),
	@CreatedTime datetime,
	@LastUpdateBy varchar(20),
	@LastUpdateTime datetime
AS  
  
SET @Result = ''
go

---------------------------------------------------------------------------------------------------------------    
-- Date Created : Tuesday, November 29, 2005    
-- Created By : DNet Team by using CodeSmith v 2.6    
-- Rev History :    
---------------------------------------------------------------------------------------------------------------    
    
alter PROCEDURE up_ValidateWSCHeader    
 @Result varchar(1000),    
 @ID int OUTPUT,    
 @ClaimType varchar(2),    
 @DealerID int,
 @DealerBranchID int,    
 @ClaimNumber varchar(13),    
 @RefClaimNumber varchar(6),    
 @ChassisMasterID int,    
 @ServiceDate datetime,    
 @Miliage int,    
 @PQR varchar(20),    
 @PQRStatus varchar(1),    
 @CodeA varchar(4),    
 @CodeB varchar(4),    
 @CodeC varchar(4),    
 @Description varchar(100),    
 @EvidencePhoto varchar(1),    
 @EvidenceInvoice varchar(1),    
 @EvidenceDmgPart varchar(1),    
 @ReqDmgPart varchar(1),    
 @ReqDmgPartBy varchar(20),    
 @ReqDmgPartTime datetime,    
 @NotificationNumber varchar(10),    
 @DecideDate datetime,    
 @Status varchar(1),    
 @ClaimStatus varchar(4),    
 @ReasonID int,    
 @LaborAmount money,    
 @PartAmount money,    
 @PartReceiveBy varchar(20),    
 @PartReceiveTime datetime,    
 @DownLoadBy varchar(20),    
 @DownLoadTime datetime,    
 @ResponseTime datetime,    
 @WorkOrderNumber varchar(50)='',  
 @RowStatus smallint,    
 @CreatedBy varchar(20),    
 @CreatedTime datetime,    
 @LastUpdateBy varchar(20),    
 @LastUpdateTime datetime    
     
AS    
    
SET @Result = ''
go

---------------------------------------------------------------------------------------------------------------    
-- Date Created : Friday, November 18, 2011    
-- Created By : DNet Team by using CodeSmith v 2.6    
-- Rev History :    
---------------------------------------------------------------------------------------------------------------    
    
alter PROCEDURE up_ValidateWSCHeaderBB    
 @Result varchar(1000),    
 @ID int OUTPUT,    
 @ClaimType varchar(2),    
 @DealerID smallint,    
 @DealerBranchID int,
 @ClaimNumber varchar(13),    
 @RefClaimNumber varchar(6),    
 @ChassisMasterBBID int,    
 @ServiceDate datetime,    
 @Miliage int,    
 @PQR varchar(20),    
 @PQRStatus varchar(1),    
 @CodeA varchar(4),    
 @CodeB varchar(4),    
 @CodeC varchar(4),    
 @Description varchar(100),    
 @EvidencePhoto varchar(1),    
 @EvidenceInvoice varchar(1),    
 @EvidenceDmgPart varchar(1),    
 @ReqDmgPart varchar(1),    
 @ReqDmgPartBy varchar(20),    
 @ReqDmgPartTime datetime,    
 @NotificationNumber varchar(10),    
 @DecideDate datetime,    
 @Status varchar(1),    
 @ClaimStatus varchar(4),    
 @ReasonID smallint,    
 @LaborAmount money,    
 @PartAmount money,    
 @PartReceiveBy varchar(20),    
 @PartReceiveTime datetime,    
 @DownLoadBy varchar(20),    
 @DownLoadTime datetime,    
 @ResponseTime datetime,   
 @WorkOrderNumber varchar(50) = '',   
 @RowStatus smallint,    
 @CreatedBy varchar(20),    
 @CreatedTime datetime,    
 @LastUpdateBy varchar(20),    
 @LastUpdateTime datetime     
AS    
    
SET @Result = ''
go

commit
go


