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

alter PROCEDURE up_UpdateArea1
	@ID int OUTPUT,
	@AreaCode varchar(10),
	@Description varchar(40),
	@PICSales varchar(50)=null,
	@PICServices varchar(50)=null,
	@PICSpareparts varchar(50)=null,
	@MainAreaID INT,
	@RowStatus smallint,
	@CreatedBy varchar(20),
	--@CreatedTime datetime,
	@LastUpdateBy varchar(20)
	--@LastUpdateTime datetime
AS

UPDATE	[dbo].[Area1]
SET
	[AreaCode] = @AreaCode,
	[Description] = @Description,
	[PICSales] = @PICSales,
	[PICServices] = @PICServices,
	[PICSpareparts] = @PICSpareparts,
	[MainAreaID] = @MainAreaID,
	[RowStatus] = @RowStatus,
	[CreatedBy] = @CreatedBy,
	--[CreatedTime] = @CreatedTime,
	[LastUpdateBy] = @LastUpdateBy,
	[LastUpdateTime] = GETDATE()
WHERE
	[ID] = @ID
go

set ANSI_NULLS off
go

--/****** Object:  Stored Procedure dbo.up_UpdateArea2    Script Date: 14/10/2005 11:06:16 ******/
---------------------------------------------------------------------------------------------------------------
-- Date Created	: Thursday, September 29, 2005
-- Created By	: DNet Team by using CodeSmith v 2.6
-- Rev History	:
---------------------------------------------------------------------------------------------------------------

alter PROCEDURE up_UpdateArea2
	@ID int OUTPUT,
	@AreaCode varchar(10),
	@Description varchar(100),
	@ACFinishUnit varchar(50),
	@ACSparePart varchar(50),
	@ACService varchar(50),
	@Area1ID Int,
	@RowStatus smallint,
	@CreatedBy varchar(20),
	--@CreatedTime datetime,
	@LastUpdateBy varchar(20)
	--@LastUpdateTime datetime
AS

UPDATE	[dbo].[Area2]
SET
	[AreaCode] = @AreaCode,
	[Description] = @Description,
	[ACFinishUnit] = @ACFinishUnit,
	[ACSparePart] = @ACSparePart,
	[ACService] = @ACService,
	[Area1ID] = @Area1ID,
	[RowStatus] = @RowStatus,
	[CreatedBy] = @CreatedBy,
	--[CreatedTime] = @CreatedTime,
	[LastUpdateBy] = @LastUpdateBy,
	[LastUpdateTime] = GETDATE()
WHERE
	[ID] = @ID
go

set ANSI_NULLS on
go

---------------------------------------------------------------------------------------------------------------
-- Date Created	: Wednesday, January 17, 2018
-- Created By	: DNet Team by using CodeSmith v 2.6
-- Rev History	:
---------------------------------------------------------------------------------------------------------------

alter PROCEDURE up_UpdateAssistPartSales
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
	[DealerBranchID] = @DealerBranchID,
	[DealerBranchCode] = @DealerBranchCode,
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
go

---------------------------------------------------------------------------------------------------------------
-- Date Created	: Wednesday, January 17, 2018
-- Created By	: DNet Team by using CodeSmith v 2.6
-- Rev History	:
---------------------------------------------------------------------------------------------------------------

alter PROCEDURE up_UpdateAssistPartStock
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
	--@CreatedTime datetime,
	@LastUpdateBy varchar(20)	--@LastUpdateTime datetime	
AS

UPDATE	[dbo].[AssistPartStock]
SET
	[AssistUploadLogID] = @AssistUploadLogID,
	[Month] = @Month,
	[Year] = @Year,
	[DealerID] = @DealerID,
	[DealerCode] = @DealerCode,
	[SparepartMasterID] = @SparepartMasterID,
	[NoParts] = @NoParts,
	[JumlahStokAwal] = @JumlahStokAwal,
	[JumlahDatang] = @JumlahDatang,
	[HargaBeli] = @HargaBeli,
	[DealerBranchID] = @DealerBranchID,
	[DealerBranchCode] = @DealerBranchCode,
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
go

---------------------------------------------------------------------------------------------------------------
-- Date Created	: Wednesday, January 17, 2018
-- Created By	: DNet Team by using CodeSmith v 2.6
-- Rev History	:
---------------------------------------------------------------------------------------------------------------

alter PROCEDURE up_UpdateAssistServiceIncoming
	@ID int OUTPUT,
	@AssistUploadLogID int,
	@TglBukaTransaksi date,
	@WaktuMasuk varchar(20),
	@TglTutupTransaksi date,
	@WaktuKeluar varchar(20),
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
	@WOStatus smallint,
	@StatusAktif smallint,
	@ValidateSystemStatus smallint,
	@RowStatus smallint,
	@CreatedBy varchar(20),
	--@CreatedTime datetime,
	@LastUpdateBy varchar(20)	--@LastUpdateTime datetime	
AS

UPDATE	[dbo].[AssistServiceIncoming]
SET
	[AssistUploadLogID] = @AssistUploadLogID,
	[TglBukaTransaksi] = @TglBukaTransaksi,
	[WaktuMasuk] = Convert(time,@WaktuMasuk),
	[TglTutupTransaksi] = @TglTutupTransaksi,
	[WaktuKeluar] = Convert(time,@WaktuKeluar),
	[DealerID] = @DealerID,
	[DealerCode] = @DealerCode,
	[TrTraineMekanikID] = @TrTraineMekanikID,
	[KodeMekanik] = @KodeMekanik,
	[NoWorkOrder] = @NoWorkOrder,
	[ChassisMasterID] = @ChassisMasterID,
	[KodeChassis] = @KodeChassis,
	[WorkOrderCategoryID] = @WorkOrderCategoryID,
	[WorkOrderCategoryCode] = @WorkOrderCategoryCode,
	[KMService] = @KMService,
	[ServicePlaceID] = @ServicePlaceID,
	[ServicePlaceCode] = @ServicePlaceCode,
	[ServiceTypeID] = @ServiceTypeID,
	[ServiceTypeCode] = @ServiceTypeCode,
	[TotalLC] = @TotalLC,
	[MetodePembayaran] = @MetodePembayaran,
	[Model] = @Model,
	[Transmition] = @Transmition,
	[DriveSystem] = @DriveSystem,
	[DealerBranchID] = @DealerBranchID,
    [DealerBranchCode] = @DealerBranchCode,
	[RemarksSystem] = @RemarksSystem,
	[RemarksSpecial] = @RemarksSpecial,
	[RemarksBM] = @RemarksBM,
	[WOStatus] = @WOStatus,
	[StatusAktif] = @StatusAktif,
	[ValidateSystemStatus] = @ValidateSystemStatus,
	[RowStatus] = @RowStatus,
	[CreatedBy] = @CreatedBy,
	--[CreatedTime] = @CreatedTime,
	[LastUpdateBy] = @LastUpdateBy,
	[LastUpdateTime] = GETDATE()	
WHERE
	[ID] = @ID
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

alter PROCEDURE up_UpdateCityPart
	@ID int OUTPUT,
	@ProvinceID int,
	@CityID INT,
	@CityName varchar(50),
	@CityCode varchar(10),
	@RowStatus smallint,
	@CreatedBy varchar(20),
	--@CreatedTime datetime,
	@LastUpdateBy varchar(20)	--@LastUpdateTime datetime	
AS

UPDATE	[dbo].[CityPart]
SET
	[ProvinceID] = @ProvinceID,
	[CityID] = @CityID,
	[CityName] = @CityName,
	[CityCode] = @CityCode,
	[RowStatus] = @RowStatus,
	[CreatedBy] = @CreatedBy,
	--[CreatedTime] = @CreatedTime,
	[LastUpdateBy] = @LastUpdateBy,
	[LastUpdateTime] = GETDATE()	
WHERE
	[ID] = @ID
go

---------------------------------------------------------------------------------------------------------------
-- Date Created	: Thursday, August 03, 2017
-- Created By	: DNet Team by using CodeSmith v 2.6
-- Rev History	:
---------------------------------------------------------------------------------------------------------------

alter PROCEDURE up_UpdateCustomerCase
	@ID int OUTPUT,
	@DealerID smallint,
	@SalesforceID nvarchar(255),
	@CaseNumber nvarchar(255),
	@CustomerName nvarchar(50),
	@Phone nvarchar(50),
	@Email nvarchar(50),
	@Category nvarchar(50),
	@SubCategory1 nvarchar(50),
	@SubCategory2 nvarchar(50),
	@SubCategory3 nvarchar(50),
	@SubCategory4 nvarchar(50),
	@CallerType nchar(10),
	@CarType nvarchar(50),
	@Variant nvarchar(50),
	@EngineNumber nvarchar(50),
	@ChassisNumber nvarchar(50),
	@Odometer int,
	@PlateNumber nvarchar(20),
	@Priority smallint,
	@CaseNumberReff nvarchar(255),
	@CaseDate datetime,
	@Subject varchar(255),
	@Description nvarchar(max),
	@ReservationNumber nvarchar(50)=NULL,
	@Status smallint,
	@RowStatus smallint,
	@CreatedBy varchar(20),
	--@CreatedTime datetime,
	@LastUpdateBy varchar(20),
	@LastUpdateTIme datetime
	
AS

IF @Status IN ('0', '1')
BEGIN
	SET @CreatedBy = 'Salesforce'
END 

UPDATE	[dbo].[CustomerCase]
SET
	[DealerID] = @DealerID,
	[SalesforceID] = @SalesforceID,
	[CaseNumber] = @CaseNumber,
	[CustomerName] = @CustomerName,
	[Phone] = @Phone,
	[Email] = @Email,
	[Category] = @Category,
	[SubCategory1] = @SubCategory1,
	[SubCategory2] = @SubCategory2,
	[SubCategory3] = @SubCategory3,
	[SubCategory4] = @SubCategory4,
	[CallerType] = @CallerType,
	[CarType] = @CarType,
	[Variant] = @Variant,
	[EngineNumber] = @EngineNumber,
	[ChassisNumber] = @ChassisNumber,
	[Odometer] = @Odometer,
	[PlateNumber] = @PlateNumber,
	[Priority] = @Priority,
	[CaseNumberReff] = @CaseNumberReff,
	[CaseDate] = @CaseDate,
	[Subject] = @Subject,
	[Description] = @Description,
	[ReservationNumber] = ISNULL(@ReservationNumber, [ReservationNumber]),
	[Status] = @Status,
	[RowStatus] = @RowStatus,
	[CreatedBy] = @CreatedBy,
	--[CreatedTime] = @CreatedTime,
	[LastUpdateBy] = @LastUpdateBy,
	[LastUpdateTIme] = getdate()	
WHERE
	[ID] = @ID


-- Jika status case re-open
IF @Status IN ('1')
BEGIN
	INSERT	INTO	[dbo].[CustomerCaseResponse]
	VALUES
	(
		@ID,
		@Subject,
		Null,
		@Status,
		1,
		@RowStatus,
		@CreatedBy,
		GETDATE(),	
		@LastUpdateBy,
		@LastUpdateTIme)
END
go

set ANSI_NULLS off
go

---------------------------------------------------------------------------------------------------------------
-- Date Created	: Friday, August 25, 2017
-- Created By	: DNet Team by using CodeSmith v 2.6
-- Rev History	:
---------------------------------------------------------------------------------------------------------------

alter PROCEDURE up_UpdateDealerBranch
	@ID int OUTPUT,
	@DealerID smallint,
	@Name varchar(50),
	@Status varchar(1),
	@Address varchar(100),
	@CityID smallint,
	@ZipCode varchar(5),
	@ProvinceID int,
	@Phone varchar(50),
	@Fax varchar(20),
	@Website varchar(20),
	@Email varchar(40),
	@TypeBranch varchar(5),
	@DealerBranchCode varchar(50),
	@Term1 varchar(100),
	@Term2 varchar(100),
	@MainAreaID int,
	@Area1ID int,
	@Area2ID int,
	@BranchAssignmentNo varchar(50),
	@BranchAssignmentDate datetime,
	@SalesUnitFlag varchar(1),
	@ServiceFlag varchar(1),
	@SparepartFlag varchar(1),
	@RowStatus smallint,
	@CreatedBy varchar(20),
	--@CreatedTime datetime,
	@LastUpdateBy varchar(20)	--@LastUpdateTime datetime	
AS

UPDATE	[dbo].[DealerBranch]
SET
	[DealerID] = @DealerID,
	[Name] = @Name,
	[Status] = @Status,
	[Address] = @Address,
	[CityID] = @CityID,
	[ZipCode] = @ZipCode,
	[ProvinceID] = @ProvinceID,
	[Phone] = @Phone,
	[Fax] = @Fax,
	[Website] = @Website,
	[Email] = @Email,
	[TypeBranch] = @TypeBranch,
	[DealerBranchCode] = @DealerBranchCode,
	[Term1] = @Term1,
	[Term2] = @Term2,
	[MainAreaID] = @MainAreaID,
	[Area1ID] = @Area1ID,
	[Area2ID] = @Area2ID,
	[BranchAssignmentNo] = @BranchAssignmentNo,
	[BranchAssignmentDate] = @BranchAssignmentDate,
	[SalesUnitFlag] = @SalesUnitFlag,
	[ServiceFlag] = @ServiceFlag,
	[SparepartFlag] = @SparepartFlag,
	[RowStatus] = @RowStatus,
	[CreatedBy] = @CreatedBy,
	--[CreatedTime] = @CreatedTime,
	[LastUpdateBy] = @LastUpdateBy,
	[LastUpdateTime] = GETDATE()	
WHERE
	[ID] = @ID
go

---------------------------------------------------------------------------------------------------------------
-- Date Created	: 15 Nopember 2005
-- Created By	: DNet Team by using CodeSmith v 2.6
-- Rev History	:
---------------------------------------------------------------------------------------------------------------

alter PROCEDURE up_UpdateDepositLine
	@ID int OUTPUT,
	@DepositID int,
	@DocumentNo varchar(20),
	@PostingDate datetime,
	@ClearingDate datetime,
	@Debit money,
	@Credit money,
	@ReferenceNo varchar(20),
	@InvoiceNo varchar(20),
	@Remark varchar(100),
	@PaymentType TINYINT,
	@RowStatus smallint,
	@CreatedBy varchar(20),
	--@CreatedTime datetime,
	@LastUpdateBy varchar(20)
	--@LastUpdateTime datetime
AS

UPDATE	[dbo].[DepositLine]
SET
	[DepositID] = @DepositID,
	[DocumentNo] = @DocumentNo,
	[PostingDate] = @PostingDate,
	[ClearingDate] = @ClearingDate,
	[Debit] = @Debit,
	[Credit] = @Credit,
	[ReferenceNo] = @ReferenceNo,
	[InvoiceNo] = @InvoiceNo,
	[Remark] = @Remark,
	PaymentType = @PaymentType,
	[RowStatus] = @RowStatus,
	[CreatedBy] = @CreatedBy,
	--[CreatedTime] = @CreatedTime,
	[LastUpdateBy] = @LastUpdateBy,
	[LastUpdateTime] = GETDATE()
WHERE
	[ID] = @ID
go

set ANSI_NULLS on
go

---------------------------------------------------------------------------------------------------------------
-- Date Created	: Thursday, June 23, 2011
-- Created By	: DNet Team by using CodeSmith v 2.6
-- Rev History	:
/*
2015/11/25 - add recalculation MCPDetail.UnitRemain
*/
---------------------------------------------------------------------------------------------------------------
alter PROCEDURE up_UpdateEndCustomer
    @ID INT OUTPUT ,
    @ProjectIndicator VARCHAR(1) ,
    @RefChassisNumberID INT ,
    @CustomerID INT ,
    @Name1 VARCHAR(50) ,
    @FakturDate DATETIME ,
    @OpenFakturDate DATETIME ,
    @FakturNumber VARCHAR(18) ,
    @AreaViolationFlag VARCHAR(50) ,
    @AreaViolationPaymentMethodID TINYINT ,
    @AreaViolationyAmount MONEY ,
    @AreaViolationBankName VARCHAR(30) ,
    @AreaViolationGyroNumber VARCHAR(30) ,
    @PenaltyFlag VARCHAR(50) ,
    @PenaltyPaymentMethodID TINYINT ,
    @PenaltyAmount MONEY ,
    @PenaltyBankName VARCHAR(30) ,
    @PenaltyGyroNumber VARCHAR(30) ,
    @ReferenceLetterFlag VARCHAR(1) ,
    @ReferenceLetter VARCHAR(40) ,
    @SaveBy VARCHAR(20) ,
    @SaveTime DATETIME ,
    @ValidateBy VARCHAR(20) ,
    @ValidateTime DATETIME ,
    @ConfirmBy VARCHAR(20) ,
    @ConfirmTime DATETIME ,
    @DownloadBy VARCHAR(20) ,
    @DownloadTime DATETIME ,
    @PrintedBy VARCHAR(20) ,
    @PrintedTime DATETIME ,
    @CleansingCustomerID INT ,
    @MCPHeaderID INT ,
    @MCPStatus SMALLINT ,
	@LKPPHeaderID int, 
	@LKPPStatus SMALLINT,
    @Remark1 VARCHAR(255) ,
    @Remark2 VARCHAR(255) ,
	@HandoverDate DATETIME,
	@IsTemporary SMALLINT,
    @RowStatus SMALLINT ,
    @CreatedBy VARCHAR(20) ,
	--@CreatedTime datetime,
    @LastUpdateBy VARCHAR(20)	--@LastUpdateTime datetime	
AS
    BEGIN

	--Temp MCP before changed
       
        DECLARE @Temp_Table AS TABLE
            (
              MCPHeaderIDBefore INT,
		 	  LKPPHeaderIDBefore INT
            )
		 
        UPDATE  [dbo].[EndCustomer]
        SET     [ProjectIndicator] = @ProjectIndicator ,
                [RefChassisNumberID] = @RefChassisNumberID ,
                [CustomerID] = @CustomerID ,
                [Name1] = @Name1 ,
                [FakturDate] = @FakturDate ,
                [OpenFakturDate] = @OpenFakturDate ,
                [FakturNumber] = @FakturNumber ,
                [AreaViolationFlag] = @AreaViolationFlag ,
                [AreaViolationPaymentMethodID] = @AreaViolationPaymentMethodID ,
                [AreaViolationyAmount] = @AreaViolationyAmount ,
                [AreaViolationBankName] = @AreaViolationBankName ,
                [AreaViolationGyroNumber] = @AreaViolationGyroNumber ,
                [PenaltyFlag] = @PenaltyFlag ,
                [PenaltyPaymentMethodID] = @PenaltyPaymentMethodID ,
                [PenaltyAmount] = @PenaltyAmount ,
                [PenaltyBankName] = @PenaltyBankName ,
                [PenaltyGyroNumber] = @PenaltyGyroNumber ,
                [ReferenceLetterFlag] = @ReferenceLetterFlag ,
                [ReferenceLetter] = @ReferenceLetter ,
                [SaveBy] = @SaveBy ,
                [SaveTime] = @SaveTime ,
                [ValidateBy] = @ValidateBy ,
                [ValidateTime] = @ValidateTime ,
                [ConfirmBy] = @ConfirmBy ,
                [ConfirmTime] = @ConfirmTime ,
                [DownloadBy] = @DownloadBy ,
                [DownloadTime] = @DownloadTime ,
                [PrintedBy] = @PrintedBy ,
                [PrintedTime] = @PrintedTime ,
                [CleansingCustomerID] = @CleansingCustomerID ,
                [MCPHeaderID] = @MCPHeaderID ,
                [MCPStatus] = @MCPStatus ,
				[LKPPHeaderID] = @LKPPHeaderID ,
                [LKPPStatus] = @LKPPStatus ,
				[Remark1] = @Remark1 ,
                [Remark2] = @Remark2 ,
				[HandoverDate]=@HandoverDate,
				[IsTemporary] = @IsTemporary,
                [RowStatus] = @RowStatus ,
                [CreatedBy] = @CreatedBy ,
	--[CreatedTime] = @CreatedTime,
                [LastUpdateBy] = @LastUpdateBy ,
                [LastUpdateTime] = GETDATE()
        OUTPUT  Deleted.MCPHeaderID, deleted.LKPPHeaderID
                INTO @Temp_Table
        WHERE   [ID] = @ID

/*Validasi update rows*/
     
        DECLARE @MCPHeaderIDBefore INT = NULL
		DECLARE @LKPPHeaderIDBefore INT = NULL
	
        SELECT  @MCPHeaderIDBefore = MCPHeaderIDBefore,
				@LKPPHeaderIDBefore = LKPPHeaderIDBefore
        FROM    @Temp_Table
      
        BEGIN
            SET NOCOUNT ON
		  
			 -- Update Previous MCP
            IF @MCPHeaderIDBefore IS NOT NULL
                BEGIN
                    EXEC up_RecalculateMCP @MCPHeaderIDBefore

                END
           
				--update current MCP
            IF @MCPHeaderID IS NOT NULL AND @MCPHeaderIDBefore<> @MCPHeaderID
                BEGIN 
                    EXEC up_RecalculateMCP @MCPHeaderID
                END
		 
		     -- Update Previous LKPP
            IF @LKPPHeaderIDBefore IS NOT NULL
                BEGIN
                    EXEC up_RecalculateLKPP @LKPPHeaderIDBefore
                END

					--update current LKPP
            IF @LKPPHeaderID IS NOT NULL AND @LKPPHeaderIDBefore<> @LKPPHeaderID
                BEGIN 
                    EXEC up_RecalculateLKPP @LKPPHeaderID
                END

            SET NOCOUNT OFF 
        END
        
    END
go

---------------------------------------------------------------------------------------------------------------
-- Date Created	: Thursday, December 03, 2009
-- Created By	: DNet Team by using CodeSmith v 2.6
-- Rev History	:
---------------------------------------------------------------------------------------------------------------

alter PROCEDURE up_UpdateEstimationEquipDetail
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
UPDATE	[dbo].[EstimationEquipDetail]
SET
	[EstimationEquipHeaderID] = @EstimationEquipHeaderID,
	[SparePartMasterID] = @SparePartMasterID,
	[Harga] = @Harga,
	[Discount] = @Discount,
	[TotalForecast] = @TotalForecast,
	[EstimationUnit] = @EstimationUnit,
	[Status] = @Status,
	[ConfirmedDate] = @ConfirmedDate,
	[Remark] = @Remark,
	[RowStatus] = @RowStatus,
	[CreatedBy] = @CreatedBy,
	--[CreatedTime] = @CreatedTime,
	[LastUpdatedBy] = @LastUpdatedBy,
	[LastUpdatedTime] =GETDATE()-- @LastUpdatedTime	
WHERE
	[ID] = @ID
go

---------------------------------------------------------------------------------------------------------------
-- Date Created	: Tuesday, August 18, 2009
-- Created By	: DNet Team by using CodeSmith v 2.6
-- Rev History	:
---------------------------------------------------------------------------------------------------------------

alter PROCEDURE up_UpdateEstimationEquipHeader
	@ID int OUTPUT,
	@EstimationNumber varchar(3),
	@DealerID smallint,
	@DepositBKewajibanHeaderID int,
	@Status smallint,
	@DMSPRNo varchar(50),
	@RowStatus smallint,
	--@CreatedBy varchar(20),
	--@CreatedTime datetime,
	@LastUpdatedBy varchar(20)
	--@LastUpdatedTime datetime
	
AS

UPDATE	[dbo].[EstimationEquipHeader]
SET
	--[EstimationNumber] = @EstimationNumber,
	[DealerID] = @DealerID,
	[DepositBKewajibanHeaderID] = @DepositBKewajibanHeaderID,
	[Status] = @Status,
	[DMSPRNo] = @DMSPRNo,
	[RowStatus] = @RowStatus,
	--[CreatedBy] = @CreatedBy,
	--[CreatedTime] = @CreatedTime,
	[LastUpdatedBy] = @LastUpdatedBy,
	[LastUpdatedTime] = GETDATE()
	
WHERE
	[ID] = @ID
go

---------------------------------------------------------------------------------------------------------------  
-- Date Created : Tuesday, October 04, 2016  
-- Created By : DNet Team by using CodeSmith v 2.6  
-- Rev History :  
---------------------------------------------------------------------------------------------------------------  
  
alter PROCEDURE up_UpdateFreeService  
 @ID int OUTPUT,  
 @Status varchar(1),  
 @ChassisMasterID int,  
 @FSKindID tinyint,  
 @MileAge int,  
 @ServiceDate smalldatetime,  
 @ServiceDealerID smallint,  
 @DealerBranchID int,  
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
 @VisitType varchar(20),
 @FleetRequestID int,  
 @WorkOrderNumber varchar(50),  
 @RowStatus smallint,  
 @CreatedBy varchar(20),  
 --@CreatedTime datetime,  
 @LastUpdateBy varchar(20) --@LastUpdateTime datetime   
AS  
  
UPDATE [dbo].[FreeService]  
SET  
 [Status] = @Status,  
 [ChassisMasterID] = @ChassisMasterID,  
 [FSKindID] = @FSKindID,  
 [MileAge] = @MileAge,  
 [ServiceDate] = @ServiceDate,  
 [ServiceDealerID] = @ServiceDealerID,  
 [DealerBranchID] = @DealerBranchID,  
 [SoldDate] = @SoldDate,  
 [NotificationNumber] = @NotificationNumber,  
 [NotificationType] = @NotificationType,  
 [TotalAmount] = @TotalAmount,  
 [LabourAmount] = @LabourAmount,  
 [PartAmount] = @PartAmount,  
 [PPNAmount] = @PPNAmount,  
 [PPHAmount] = @PPHAmount,  
 [Reject] = @Reject,  
 [Reason] = @Reason,  
 [ReleaseBy] = @ReleaseBy,  
 [ReleaseDate] = @ReleaseDate,  
 [VisitType] = @VisitType,
 [FleetRequestID] = @FleetRequestID,  
 [WorkOrderNumber] = @WorkOrderNumber,  
 [RowStatus] = @RowStatus,  
 [CreatedBy] = @CreatedBy,  
 --[CreatedTime] = @CreatedTime,  
 [LastUpdateBy] = @LastUpdateBy,  
 [LastUpdateTime] = GETDATE()   
WHERE  
 [ID] = @ID
go

---------------------------------------------------------------------------------------------------------------  
-- Date Created : Wednesday, October 19, 2011  
-- Created By : DNet Team by using CodeSmith v 2.6  
-- Rev History :  
---------------------------------------------------------------------------------------------------------------  
  
alter PROCEDURE up_UpdateFreeServiceBB  
 @ID int OUTPUT,  
 @Status varchar(1),  
 @ChassisMasterID int,  
 @FSKindID tinyint,  
 @MileAge int,  
 @ServiceDate datetime,  
 @ServiceDealerID smallint,  
 @DealerBranchID int,  
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
 @VisitType varchar(20),
 @WorkOrderNumber varchar(50),  
 @RowStatus smallint,  
 @CreatedBy varchar(20),  
 --@CreatedTime datetime,  
 @LastUpdateBy varchar(20) --@LastUpdateTime datetime   
AS  
  
UPDATE [dbo].[FreeServiceBB]  
SET  
 [Status] = @Status,  
 [ChassisMasterID] = @ChassisMasterID,  
 [FSKindID] = @FSKindID,  
 [MileAge] = @MileAge,  
 [ServiceDate] = @ServiceDate,  
 [ServiceDealerID] = @ServiceDealerID,  
 [DealerBranchID] = @DealerBranchID,  
 [SoldDate] = @SoldDate,  
 [NotificationNumber] = @NotificationNumber,  
 [NotificationType] = @NotificationType,  
 [TotalAmount] = @TotalAmount,  
 [LabourAmount] = @LabourAmount,  
 [PartAmount] = @PartAmount,  
 [PPNAmount] = @PPNAmount,  
 [PPHAmount] = @PPHAmount,  
 [Reject] = @Reject,  
 [Reason] = @Reason,  
 [ReleaseBy] = @ReleaseBy,  
 [ReleaseDate] = @ReleaseDate,  
 [VisitType] = @VisitType,
 [WorkOrderNumber] = @WorkOrderNumber,  
 [RowStatus] = @RowStatus,  
 [CreatedBy] = @CreatedBy,  
 --[CreatedTime] = @CreatedTime,  
 [LastUpdateBy] = @LastUpdateBy,  
 [LastUpdateTime] = GETDATE()   
WHERE  
 [ID] = @ID
go

---------------------------------------------------------------------------------------------------------------
-- Date Created	: 01 Oktober 2007
-- Created By	: DNet Team by using CodeSmith v 2.6
-- Rev History	:
---------------------------------------------------------------------------------------------------------------

alter PROCEDURE up_UpdateIndentPartDetail
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
	--@CreatedTime datetime,
	@LastUpdateBy varchar(20)
	--@LastUpdateTime datetime
	
AS

UPDATE	[dbo].[IndentPartDetail]
SET
	[IndentPartHeaderID] = @IndentPartHeaderID,
	[SparePartMasterID] = @SparePartMasterID,
	[TotalForecast] = @TotalForecast,
	[Qty] = @Qty,
	[Description] = @Description,
	[AllocationQty] = @AllocationQty,
	[IsCompletedAllocation] = @IsCompletedAllocation,
	[Price] = @Price,
	[RowStatus] = @RowStatus,
	[CreatedBy] = @CreatedBy,
	--[CreatedTime] = @CreatedTime,
	[LastUpdateBy] = @LastUpdateBy,
	[LastUpdateTime] = GETDATE()
	
WHERE
	[ID] = @ID
go

---------------------------------------------------------------------------------------------------------------
-- Date Created	: Friday, December 12, 2008
-- Created By	: DNet Team by using CodeSmith v 2.6
-- Rev History	:
-- Rev			: 06/03/2018
--				  add Purpose and DMSPRNo columns
---------------------------------------------------------------------------------------------------------------

alter PROCEDURE up_UpdateIndentPartHeader
	@ID int OUTPUT,
	@DealerID int,
	@RequestNo varchar(13),
	@RequestDate datetime,
	@MaterialType int,
	@TermOfPaymentID int = null,
	@TOPBlockStatusID int = null,
	@Status tinyint,
	@StatusKTB tinyint,
	@SubmitFile varchar(50),
	@PaymentType tinyint,
	@Price money,
	@KTBConfirmedDate datetime,
	@DescID tinyint,
	@ChassisNumber varchar(20),
	@DMSPRNo varchar(50),
	@RowStatus smallint,
	@CreatedBy varchar(20),
	--@CreatedTime datetime,
	@LastUpdateBy varchar(20)	--@LastUpdateTime datetime	
AS

UPDATE	[dbo].[IndentPartHeader]
SET
	[DealerID] = @DealerID,
	[RequestNo] = @RequestNo,
	[RequestDate] = @RequestDate,
	[MaterialType] = @MaterialType,
	[TermOfPaymentID] = @TermOfPaymentID,
	[TOPBlockStatusID] = @TOPBlockStatusID, 
	[Status] = @Status,
	[StatusKTB] = @StatusKTB,
	[SubmitFile] = @SubmitFile,
	[PaymentType] = @PaymentType,
	[Price] = @Price,
	[KTBConfirmedDate] = @KTBConfirmedDate,
	[DescID] = @DescID,
	[ChassisNumber] = @ChassisNumber,
	[DMSPRNo] = @DMSPRNo,
	[RowStatus] = @RowStatus,
	[CreatedBy] = @CreatedBy,
	--[CreatedTime] = @CreatedTime,
	[LastUpdateBy] = @LastUpdateBy,
	[LastUpdateTime] = GETDATE()	
WHERE
	[ID] = @ID
go

---------------------------------------------------------------------------------------------------------------
-- Date Created	: 11 Nopember 2016
-- Created By	: DNet Team by using CodeSmith v 2.6
-- Rev History	:
---------------------------------------------------------------------------------------------------------------

alter PROCEDURE up_UpdateMainArea
	@ID int OUTPUT,
	@AreaCode varchar(20),
	@Description varchar(50),
	@PICSales varchar(50)=null,
	@PICServices varchar(50)=null,
	@PICSpareparts varchar(50)=null,
	@RowStatus smallint,
	@CreatedBy varchar(20),
	--@CreatedTime datetime,
	@LastUpdateBy varchar(20)	--@LastUpdateTime datetime	
AS

UPDATE	[dbo].[MainArea]
SET
	[AreaCode] = @AreaCode,
	[Description] = @Description,
	[PICSales] = @PICSales,
	[PICServices] = @PICServices,
	[PICSpareparts] = @PICSpareparts,
	[RowStatus] = @RowStatus,
	[CreatedBy] = @CreatedBy,
	--[CreatedTime] = @CreatedTime,
	[LastUpdateBy] = @LastUpdateBy,
	[LastUpdateTime] = GETDATE()	
WHERE
	[ID] = @ID
go

commit
go



SET XACT_ABORT ON
GO

BEGIN TRANSACTION
GO

SET ANSI_NULLS ON
GO

---------------------------------------------------------------------------------------------------------------
-- Date Created	: Monday, March 07, 2011
-- Created By	: DNet Team by using CodeSmith v 2.6
-- Rev History	:
---------------------------------------------------------------------------------------------------------------

ALTER PROCEDURE up_UpdateSPKHeader
	  @ID INT OUTPUT ,
	  @DealerID SMALLINT ,
	  @Status VARCHAR(2) ,
	  @SPKNumber VARCHAR(15) ,
	  @SPKReferenceNumber VARCHAR(15) = NULL ,
	  @DealerSPKNumber VARCHAR(15) ,
	  @IndentNumber VARCHAR(10) ,
	  @PlanDeliveryMonth TINYINT ,
	  @PlanDeliveryYear SMALLINT ,
	  @PlanDeliveryDate DATETIME ,
	  @PlanInvoiceMonth TINYINT ,
	  @PlanInvoiceYear SMALLINT ,
	  @PlanInvoiceDate DATETIME ,
	  @CustomerRequestID INT ,
	  @SPKCustomerID INT ,
	  @ValidateTime DATETIME ,
	  @ValidateBy NVARCHAR(20) ,
	  @RejectedReason NVARCHAR(255) ,
	  @SalesmanHeaderID SMALLINT ,
	  @EvidenceFile NVARCHAR(255) ,
	  @ValidationKey NVARCHAR(20) ,
	  @FlagUpdate SMALLINT ,
	  @DealerBranchID INT ,
	  @IsSend SMALLINT ,
	  @DealerSPKDate DATETIME ,
	  @BenefitMasterHeaderID INT ,
	  @RowStatus SMALLINT ,
	--@CreatedTime datetime,
	  @CreatedBy NVARCHAR(20) ,
	--@LastUpdateTime datetime,
	  @LastUpdateBy NVARCHAR(20)
AS
BEGIN

SET NOCOUNT ON;
 DECLARE @Tbl AS TABLE
					   (
						 oldStatus INT ,
						 NewStatus INT
					   )


UPDATE	[dbo].[SPKHeader]
			   SET		[DealerID] = @DealerID ,
						[Status] = @Status ,
						[SPKNumber] = @SPKNumber ,
						[SPKReferenceNumber] = @SPKReferenceNumber ,
						[DealerSPKNumber] = @DealerSPKNumber ,
						[IndentNumber] = CASE WHEN @Status=7 AND IndentNumber ='' THEN  dbo.[ufn_CreateSPKIndentNumber](GETDATE())  ELSE   @IndentNumber end,
						[PlanDeliveryMonth] = @PlanDeliveryMonth ,
						[PlanDeliveryYear] = @PlanDeliveryYear ,
						[PlanDeliveryDate] = @PlanDeliveryDate ,
						[PlanInvoiceMonth] = @PlanInvoiceMonth ,
						[PlanInvoiceYear] = @PlanInvoiceYear ,
						[PlanInvoiceDate] = @PlanInvoiceDate ,
						[CustomerRequestID] = @CustomerRequestID ,
						[SPKCustomerID] = @SPKCustomerID ,
						[ValidateTime] = @ValidateTime ,
						[ValidateBy] = @ValidateBy ,
						[RejectedReason] = @RejectedReason ,
						[SalesmanHeaderID] = @SalesmanHeaderID ,
						[EvidenceFile] = @EvidenceFile ,
						[ValidationKey] = @ValidationKey ,
						[FlagUpdate] = @FlagUpdate ,
						[DealerBranchID] = @DealerBranchID ,
						[IsSend] = @IsSend ,
						[DealerSPKDate] = @DealerSPKDate ,
						[BenefitMasterHeaderID] = @BenefitMasterHeaderID ,
						[RowStatus] = @RowStatus ,
			--[CreatedTime] = @CreatedTime,
						[CreatedBy] = @CreatedBy ,
						[LastUpdateTime] = GETDATE() ,
						[LastUpdateBy] = @LastUpdateBy
			   OUTPUT	[Deleted].[Status] ,
						[Inserted].[Status]
						INTO  @Tbl ( [oldStatus], [NewStatus] )
			   WHERE	[ID] = @ID


			   IF EXISTS ( SELECT	'*'
						   FROM		@Tbl a
						   WHERE	a.[oldStatus] <> a.[NewStatus] )
				  BEGIN
						INSERT	INTO [dbo].[StatusChangeHistory]
								(
								  [DocumentType] ,
								  [DocumentRegNumber] ,
								  [OldStatus] ,
								  [NewStatus] ,
								  [RowStatus] ,
								  [CreatedBy] ,
								  [CreatedTime] ,
								  [LastUpdateBy] ,
								  [LastUpdateTime]
								)
						SELECT	6 , -- DocumentType - tinyint
								@SPKNumber , -- DocumentRegNumber - varchar(15)
								[@Tbl].[oldStatus] , -- OldStatus - smallint
								[@Tbl].[NewStatus] , -- NewStatus - smallint
								0 , -- RowStatus - smallint
								LEFT(@LastUpdateBy, 20) , -- CreatedBy - varchar(20)
								GETDATE() , -- CreatedTime - datetime
								'' , -- LastUpdateBy - varchar(20)
								GETDATE()  -- LastUpdateTime - datetime
						FROM	@Tbl
 
				  END
 

			   
		 END
GO

COMMIT
GO


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

alter PROCEDURE up_UpdatePartShop
    @ID INT OUTPUT ,
    @DealerID SMALLINT ,
    @CityPartID INT ,
    @CityID INT = NULL ,
    @PartShopCode VARCHAR(10) ,
    @Name VARCHAR(50) ,
    @Address VARCHAR(100) ,
    @Phone VARCHAR(40) ,
    @Fax VARCHAR(40) ,
    @Email VARCHAR(50) ,
    @Status TINYINT ,
    @RowStatus SMALLINT ,
    @CreatedBy VARCHAR(20) ,
	--@CreatedTime datetime,
    @LastUpdateBy VARCHAR(20)	--@LastUpdateTime datetime	
AS
    IF @CityPartID = NULL
        AND @CityID = NULL
        BEGIN
            RETURN
        END


    IF @CityID = NULL
        BEGIN

            SELECT  @CityID = CityId
            FROM    dbo.CityPart
            WHERE   ID = @CityPartID
            IF @CityID = NULL
                BEGIN
                    SELECT  @CityID = ID
                    FROM    dbo.City
                    WHERE   CityName LIKE ( SELECT  CityName
                                            FROM    CityPart
                                            WHERE   ID = @CityPartID
                                          )

                    UPDATE  dbo.CityPart
                    SET     cityid = @CityID
                    WHERE   ID = @CityPartID
                END 
        END



    IF @Status = 2
        AND @PartShopCode = ''
        BEGIN
            SET @PartShopCode = dbo.ufn_CreatePartShopCode(@CityPartID)
        END
  
    UPDATE  [dbo].[PartShop]
    SET     [DealerID] = @DealerID ,
            [CityPartID] = @CityPartID ,
            [CityID] = @CityID ,
            [PartShopCode] = @PartShopCode ,
            [Name] = @Name ,
            [Address] = @Address ,
            [Phone] = @Phone ,
            [Fax] = @Fax ,
            [Email] = @Email ,
            [Status] = @Status ,
            [RowStatus] = @RowStatus ,
            [CreatedBy] = @CreatedBy ,
	--[CreatedTime] = @CreatedTime,
            [LastUpdateBy] = @LastUpdateBy ,
            [LastUpdateTime] = GETDATE()
    WHERE   [ID] = @ID
go

---------------------------------------------------------------------------------------------------------------
-- Date Created	: Wednesday, November 30, 2005
-- Created By	: DNet Team by using CodeSmith v 2.6
-- Rev History	:
---------------------------------------------------------------------------------------------------------------

alter PROCEDURE up_UpdatePDI
	@ID int OUTPUT,
	@ChassisMasterID int,
	@DealerID smallint,
	@DealerBranchID smallint,
	@Kind char(1),
	@PDIStatus char(1),
	@PDIDate datetime,
	@ReleaseBy varchar(20),
	@ReleaseDate datetime,
	@WorkOrderNumber varchar(50),
	@RowStatus smallint,
	@CreatedBy varchar(20),
	--@CreatedTime datetime,
	@LastUpdateBy varchar(20)	--@LastUpdateTime datetime	
AS

UPDATE	[dbo].[PDI]
SET
	[ChassisMasterID] = @ChassisMasterID,
	[DealerID] = @DealerID,
	[DealerBranchID] = @DealerBranchID,
	[Kind] = @Kind,
	[PDIStatus] = @PDIStatus,
	[PDIDate] = @PDIDate,
	[ReleaseBy] = @ReleaseBy,
	[ReleaseDate] = @ReleaseDate,
	[WorkOrderNumber] = @WorkOrderNumber,
	[RowStatus] = @RowStatus,
	[CreatedBy] = @CreatedBy,
	--[CreatedTime] = @CreatedTime,
	[LastUpdateBy] = @LastUpdateBy,
	[LastUpdateTime] = GETDATE()	
WHERE
	[ID] = @ID
go

---------------------------------------------------------------------------------------------------------------
-- Date Created	: Wednesday, August 01, 2007
-- Created By	: DNet Team by using CodeSmith v 2.6
-- Rev History	:
---------------------------------------------------------------------------------------------------------------

alter PROCEDURE up_UpdatePMHeader
	@ID int OUTPUT,
	@DealerID smallint,
	@DealerBranchID smallint,
	@ChassisNumberID int,
	@PMKindID int,
	@StandKM int,
	@ServiceDate datetime,
	@ReleaseDate datetime,
	@PMStatus varchar(4),
	@EntryType varchar(20),
	@WorkOrderNumber varchar(50),
	@BookingNo varchar(50),
	@VisitType varchar(5),
	@Remarks varchar(250),
	@RowStatus smallint,
	@CreatedBy varchar(20),
	--@CreatedTime datetime,
	@LastUpdateBy varchar(20)	--@LastUpdateTime datetime	
AS

UPDATE	[dbo].[PMHeader]
SET
	[DealerID] = @DealerID,
	[DealerBranchID] = @DealerBranchID,
	[ChassisNumberID] = @ChassisNumberID,
	[StandKM] = @StandKM,
	[ServiceDate] = @ServiceDate,
	[ReleaseDate] = @ReleaseDate,
	[PMStatus] = @PMStatus,
	[EntryType] = @EntryType,
	[WorkOrderNumber] = @WorkOrderNumber,
	[BookingNo] = @BookingNo,
	[VisitType] = @VisitType,
	[Remarks] = @Remarks,
	[PMKindID] = @PMKindID,
	[RowStatus] = @RowStatus,
	[CreatedBy] = @CreatedBy,
	--[CreatedTime] = @CreatedTime,
	[LastUpdateBy] = @LastUpdateBy,
	[LastUpdateTime] = GETDATE()	
WHERE
	[ID] = @ID
go

---------------------------------------------------------------------------------------------------------------  
-- Date Created : 10 Agustus 2016  
-- Created By : DNet Team by using CodeSmith v 2.6  
-- Rev History :  
---------------------------------------------------------------------------------------------------------------  
  
alter PROCEDURE up_UpdatePOHeader  
      @ID INT OUTPUT ,  
      @DealerID SMALLINT ,  
      @PONumber VARCHAR(10) ,  
      @Status VARCHAR(2) ,  
      @ContractHeaderID INT ,  
      @ReqAllocationDate TINYINT ,  
      @ReqAllocationMonth TINYINT ,  
      @ReqAllocationYear SMALLINT ,  
      @ReqAllocationDateTime SMALLDATETIME ,  
      @EffectiveDate DATETIME ,  
      @DealerPONumber VARCHAR(20) ,  
      @TermOfPaymentID INT ,  
      @POType CHAR(1) ,  
      @ReleaseDate TINYINT ,  
      @ReleaseMonth TINYINT ,  
      @ReleaseYear SMALLINT ,  
      @SONumber VARCHAR(10) ,  
      @FreePPh22Indicator TINYINT ,  
      @PassTOP TINYINT ,  
      @LastReqAllocationDateTime DATETIME ,  
      @RemarkStatus SMALLINT ,  
      @DOBlockHistory SMALLINT ,  
      @Remark VARCHAR(500) ,  
      @ChangedTime DATETIME ,  
      @ChangedBy VARCHAR(20) ,  
      @BlockedStatus SMALLINT ,  
      @IsFactoring SMALLINT ,  
      @SPLID INT ,  
      @IsTransfer SMALLINT ,  
      @PODestinationID INT ,  
      @RowStatus SMALLINT ,  
      @CreatedBy VARCHAR(20) ,  
 --@CreatedTime datetime,  
      @LastUpdateBy VARCHAR(20) --@LastUpdateTime datetime   
AS  
BEGIN   
      DECLARE @DD AS TABLE  
              (  
                [ReqAllocationDateTime] SMALLDATETIME  
              )  
      UPDATE    [dbo].[POHeader]  
      SET       [DealerID] = @DealerID ,  
                [PONumber] = @PONumber ,  
                [Status] = @Status ,  
                [ContractHeaderID] = @ContractHeaderID ,  
                [ReqAllocationDate] = @ReqAllocationDate ,  
                [ReqAllocationMonth] = @ReqAllocationMonth ,  
                [ReqAllocationYear] = @ReqAllocationYear ,  
                [ReqAllocationDateTime] = @ReqAllocationDateTime ,  
                [EffectiveDate] = @EffectiveDate ,  
                [DealerPONumber] = @DealerPONumber ,  
                [TermOfPaymentID] = @TermOfPaymentID ,  
                [POType] = @POType ,  
                [ReleaseDate] = @ReleaseDate ,  
                [ReleaseMonth] = @ReleaseMonth ,  
                [ReleaseYear] = @ReleaseYear ,  
                [SONumber] = @SONumber ,  
                [FreePPh22Indicator] = @FreePPh22Indicator ,  
                [PassTOP] = @PassTOP ,  
                [LastReqAllocationDateTime] = @LastReqAllocationDateTime ,  
                [RemarkStatus] = @RemarkStatus ,  
                [DOBlockHistory] = @DOBlockHistory ,  
                [Remark] = @Remark ,  
                [ChangedTime] = @ChangedTime ,  
                [ChangedBy] = @ChangedBy ,  
                [BlockedStatus] = @BlockedStatus ,  
                [IsFactoring] = @IsFactoring ,  
                [SPLID] = @SPLID ,  
                [IsTransfer] = @IsTransfer ,  
                [PODestinationID] = @PODestinationID ,  
                [RowStatus] = @RowStatus ,  
                [CreatedBy] = @CreatedBy ,  
 --[CreatedTime] = @CreatedTime,  
                [LastUpdateBy] = @LastUpdateBy ,  
                [LastUpdateTime] = GETDATE()  
      OUTPUT    [Deleted].[ReqAllocationDateTime]  
                INTO @DD ( [ReqAllocationDateTime] )  
      WHERE     [ID] = @ID  
  
  
      DECLARE @OldReqAllocationDateTime DATETIME  
      SELECT    @OldReqAllocationDateTime = a.[ReqAllocationDateTime]  
      FROM      @DD a  
  
      IF @IsTransfer = 1  
         AND @Status = 8
   --    AND @OldReqAllocationDateTime <> @ReqAllocationDateTime  
         BEGIN  
                
               DECLARE @SOID INT  
               SELECT   @SOID = ID  
               FROM     SalesOrder  
               WHERE    SONumber = @SONumber;  
  
               EXEC dbo.sp_SetSalesOrderDueDate @SOID;  
         END  
  
  
END
go

---------------------------------------------------------------------------------------------------------------    
-- Date Created : Monday, July 23, 2007    
-- Created By : DNet Team by using CodeSmith v 2.6    
-- Rev History :    
---------------------------------------------------------------------------------------------------------------    
    
alter  PROCEDURE up_UpdatePQRHeader    
 @ID int OUTPUT,    
 @PQRNo varchar(25),   
 @PQRType int, 
 @RefPQRNo varchar(25),    
 @DealerID smallint,    
 @DealerBranchID int = null,   
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
 @ReleaseBy VARCHAR(50),    
 @FinishBy VARChar(50),    
 @FinishDate  datetime,    
 @CodeA varchar(4),    
 @CodeB varchar(4),    
 @CodeC varchar(4),    
 @WorkOrderNumber varchar(50),  
 @RowStatus smallint,    
 @CreatedBy varchar(20),    
 --@CreatedTime datetime,    
 @LastUpdateBy varchar(20)    
 --@LastUpdateTime datetime    
     
AS    
UPDATE [dbo].[PQRHeader]    
SET    
 [PQRNo] = @PQRNo,    
 [PQRType] = @PQRType,
 [RefPQRNo] = @RefPQRNo,    
 [DealerID] = @DealerID,    
 [DealerBranchID] = @DealerBranchID,
 [Year] = @Year,    
 [SeqNo] = @SeqNo,    
 [CategoryID] = @CategoryID,    
 [DocumentDate] = @DocumentDate,    
 [SoldDate] = @SoldDate,    
 [ChassisMasterID] = @ChassisMasterID,    
 [PQRDate] = @PQRDate,    
 [OdoMeter] = @OdoMeter,    
 [Velocity] = @Velocity,    
 [CustomerName] = @CustomerName,    
 [CustomerAddress] = @CustomerAddress,    
 [ValidationTime] = @ValidationTime,    
 [ConfirmBy] = @ConfirmBy,    
 [ConfirmTime] = @ConfirmTime,    
 [RealeseTime] = @RealeseTime,    
 [IntervalProcess] = @IntervalProcess,    
 [Complexity] = @Complexity,    
 [Subject] = @Subject,    
 [Symptomps] = @Symptomps,    
 [Causes] = @Causes,    
 [Results] = ltrim(rtrim(@Results)),    
 [Notes] = @Notes,    
 [Solutions] = @Solutions,    
 [Bobot] = @Bobot,    
 [ReleaseBy]=@ReleaseBy,    
 [FinishBy] = @FinishBy,    
 [FinishDate]=@FinishDate,    
 [CodeA] = @CodeA,    
 [CodeB] = @CodeB,    
 [CodeC] = @CodeC,    
 [WorkOrderNumber] = @WorkOrderNumber,  
 [RowStatus] = @RowStatus,    
 [CreatedBy] = @CreatedBy,    
 --[CreatedTime] = @CreatedTime,    
 [LastUpdateBy] = @LastUpdateBy,    
 [LastUpdateTime] = GETDATE()    
     
WHERE    
 [ID] = @ID
go

---------------------------------------------------------------------------------------------------------------    
-- Date Created : Monday, January 16, 2012    
-- Created By : DNet Team by using CodeSmith v 2.6    
-- Rev History :    
---------------------------------------------------------------------------------------------------------------    
    
alter PROCEDURE up_UpdatePQRHeaderBB    
 @ID int OUTPUT,    
 @PQRNo varchar(25),   
 @PQRType int, 
 @RefPQRNo varchar(25),    
 @DealerID smallint,    
 @DealerBranchID int = null,
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
 @WorkOrderNumber varchar(50) = null,  
 @RowStatus smallint,    
 @CreatedBy varchar(20),    
 --@CreatedTime datetime,    
 @LastUpdateBy varchar(20) --@LastUpdateTime datetime     
AS    
    
UPDATE [dbo].[PQRHeaderBB]    
SET    
 [PQRNo] = @PQRNo,    
 [PQRType] = @PQRType,
 [RefPQRNo] = @RefPQRNo,    
 [DealerID] = @DealerID,    
 [DealerBranchID] = @DealerBranchID,
 [Year] = @Year,    
 [SeqNo] = @SeqNo,    
 [CategoryID] = @CategoryID,    
 [DocumentDate] = @DocumentDate,    
 [SoldDate] = @SoldDate,    
 [ChassisMasterBBID] = @ChassisMasterBBID,    
 [PQRDate] = @PQRDate,    
 [OdoMeter] = @OdoMeter,    
 [Velocity] = @Velocity,    
 [CustomerName] = @CustomerName,    
 [CustomerAddress] = @CustomerAddress,    
 [ValidationTime] = @ValidationTime,    
 [ConfirmBy] = @ConfirmBy,    
 [ConfirmTime] = @ConfirmTime,    
 [RealeseTime] = @RealeseTime,    
 [IntervalProcess] = @IntervalProcess,    
 [Complexity] = @Complexity,    
 [Subject] = @Subject,    
 [Symptomps] = @Symptomps,    
 [Causes] = @Causes,    
 [Results] = @Results,    
 [Notes] = @Notes,    
 [Solutions] = @Solutions,    
 [Bobot] = @Bobot,    
 [ReleaseBy] = @ReleaseBy,    
 [FinishBy] = @FinishBy,    
 [FinishDate] = @FinishDate,    
 [CodeA] = @CodeA,    
 [CodeB] = @CodeB,    
 [CodeC] = @CodeC,    
 [WorkOrderNumber] = @WorkOrderNumber,  
 [RowStatus] = @RowStatus,    
 [CreatedBy] = @CreatedBy,    
 --[CreatedTime] = @CreatedTime,    
 [LastUpdateBy] = @LastUpdateBy,    
 [LastUpdateTime] = GETDATE()     
WHERE    
 [ID] = @ID
go

---------------------------------------------------------------------------------------------------------------
-- Date Created	: 19 April 2016
-- Created By	: DNet Team by using CodeSmith v 2.6
-- Rev History	:
---------------------------------------------------------------------------------------------------------------

alter PROCEDURE up_UpdateRecallService
	@ID int OUTPUT,
	@ChassisMasterID int,
	@MileAge int,
	@ServiceDate datetime,
	@ServiceDealerID smallint,
	@DealerBranchID int,
	@RecallChassisMasterID int,
	@WorkOrderNumber varchar(50),
	@RowStatus smallint,
	@CreatedBy varchar(20),
	--@CreatedTime datetime,
	@LastUpdateBy varchar(20)	--@LastUpdateTime datetime	
AS

UPDATE	[dbo].[RecallService]
SET
	[ChassisMasterID] = @ChassisMasterID,
	[MileAge] = @MileAge,
	[ServiceDate] = @ServiceDate,
	[ServiceDealerID] = @ServiceDealerID,
	[DealerBranchID] = @DealerBranchID,
	[RecallChassisMasterID] = @RecallChassisMasterID,
	[WorkOrderNumber] = @WorkOrderNumber,
	[RowStatus] = @RowStatus,
	[CreatedBy] = @CreatedBy,
	--[CreatedTime] = @CreatedTime,
	[LastUpdateBy] = @LastUpdateBy,
	[LastUpdateTime] = GETDATE()	
WHERE
	[ID] = @ID
go

---------------------------------------------------------------------------------------------------------------
-- Date Created	: Thursday, July 13, 2017
-- Created By	: DNet Team by using CodeSmith v 2.6
-- Rev History	:
-- Revision: Friday, 2 March 2018 by Mitrais Team
--			change BenefitMasterHeaderID to CampaignName
--			change IndustrialSectorID to BusinessSectordetailID
---------------------------------------------------------------------------------------------------------------

alter PROCEDURE up_UpdateSAPCustomer
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
	--@CreatedTime datetime,
	@LastUpdateBy varchar(20)
	--@LastUpdateTime datetime,
AS

UPDATE	[dbo].[SAPCustomer]
SET
	[SalesforceID] = @SalesforceID,
	[DealerID] = @DealerID,
	[SalesmanHeaderID] = @SalesmanHeaderID,
	[VechileTypeID] = @VechileTypeID,
	[CustomerCode] = @CustomerCode,
	[CustomerName] = @CustomerName,
	[CustomerType] = @CustomerType,
	[CustomerAddress] = @CustomerAddress,
	[Phone] = @Phone,
	[Email] = @Email,
	[Sex] = @Sex,
	[AgeSegment] = @AgeSegment,
	[CustomerPurpose] = @CustomerPurpose,
	[InformationType] = @InformationType,
	[InformationSource] = @InformationSource,
	[Status] = @Status,
	[Qty] = @Qty,
	[ProspectDate] = @ProspectDate,
	[isSPK] = @isSPK,
	[CurrVehicleBrand] = @CurrVehicleBrand,
	[CurrVehicleType] = @CurrVehicleType,
	[Note] = @Note,
	[WebID] = @WebID,
    [BirthDate] = @BirthDate,
	[PreferedVehicleModel] = @PreferedVehicleModel,
	[Description] = @Description,
	[EstimatedCloseDate] = @EstimatedCloseDate,
	[OriginatingLeadId] = @OriginatingLeadId,
	[StatusCode] = @StatusCode,
	[LeadStatus] = @LeadStatus,
	[StateCode] = @StateCode,
	[CampaignName] = @CampaignName,
	[BusinessSectorDetailID] = @BusinessSectorDetailID,
	[RowStatus] = @RowStatus,
	[CreatedBy] = @CreatedBy,
	--[CreatedTime] = @CreatedTime,
	[LastUpdateBy] = @LastUpdateBy,
	[LastUpdateTime] = GETDATE()
WHERE
	[ID] = @ID
go

---------------------------------------------------------------------------------------------------------------
-- Date Created	: Tuesday, July 24, 2012
-- Created By	: DNet Team by using CodeSmith v 2.6
-- Rev History	:
---------------------------------------------------------------------------------------------------------------

alter PROCEDURE up_UpdateSparePartMaster
    @ID INT OUTPUT ,
    @ProductCategoryID SMALLINT ,
    @PartNumber VARCHAR(18) ,
    @UoM VARCHAR(18) = NULL ,
    @PartNumberReff VARCHAR(18) = NULL ,
    @MaterialCategoryCode VARCHAR(18) = NULL ,
    @PartName VARCHAR(50) ,
    @AltPartNumber VARCHAR(18) ,
    @AltPartName VARCHAR(50) ,
    @PartCode VARCHAR(1) ,
    @ModelCode VARCHAR(9) ,
    @SupplierCode VARCHAR(10) ,
    @TypeCode VARCHAR(5) ,
    @Stock INT ,
    @RetalPrice MONEY ,
    @PartStatus VARCHAR(1) ,
    @ActiveStatus SMALLINT ,
    @AccessoriesType SMALLINT ,
	@ProductType VARCHAR(100) ,
    @RowStatus SMALLINT ,
    @CreatedBy VARCHAR(20) ,
	--@CreatedTime datetime,
    @LastUpdateBy VARCHAR(20)	--@LastUpdateTime datetime	
AS
    UPDATE  [dbo].[SparePartMaster]
    SET     [ProductCategoryID] = @ProductCategoryID ,
            [UoM] = @UoM ,
            [MaterialCategoryCode] = @MaterialCategoryCode ,
            [PartNumberReff] = @PartNumberReff ,
            [PartNumber] = @PartNumber ,
            [PartName] = @PartName ,
            [AltPartNumber] = @AltPartNumber ,
            [AltPartName] = @AltPartName ,
            [PartCode] = @PartCode ,
            [ModelCode] = @ModelCode ,
            [SupplierCode] = @SupplierCode ,
            [TypeCode] = @TypeCode ,
            [Stock] = @Stock ,
            [RetalPrice] = @RetalPrice ,
            [PartStatus] = @PartStatus ,
            [ActiveStatus] = @ActiveStatus ,
            [AccessoriesType] = @AccessoriesType ,
			[ProductType] = @ProductType ,
            [RowStatus] = @RowStatus ,
            [CreatedBy] = @CreatedBy ,
	--[CreatedTime] = @CreatedTime,
            [LastUpdateBy] = @LastUpdateBy ,
            [LastUpdateTime] = GETDATE()
    WHERE   [ID] = @ID
go

---------------------------------------------------------------------------------------------------------------
-- Date Created	: Thursday, August 04, 2011
-- Created By	: DNet Team by using CodeSmith v 2.6
-- Rev History	:
-- Date Created	: 06 Maret 2018
-- Created By	: Mitrais Team by using CodeSmith v 2.6
-- Rev History	: Add DMSPRNo
---------------------------------------------------------------------------------------------------------------

alter PROCEDURE up_UpdateSparePartPO
	@ID int OUTPUT,
	@PONumber varchar(15),
	@OrderType varchar(1),
	@DealerID smallint,
	@PODate smalldatetime,
	@TermOfPaymentID int = null,
	@TOPBlockStatusID int = null,
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
	--@CreatedTime datetime,
	@LastUpdateBy varchar(20)	--@LastUpdateTime datetime	
AS
	declare @tmpPONumber as varchar(15)
	
	set @tmpPONumber = @PONumber 
	set @PONumber= dbo.ufn_UpdateSparePartPONumber(@ID)
	if @PONumber='' begin set @PONumber=@tmpPONumber end 

UPDATE	[dbo].[SparePartPO]
SET
	[PONumber] = @PONumber,
	[OrderType] = @OrderType,
	[DealerID] = @DealerID,
	[PODate] = @PODate,
	[TermOfPaymentID] = @TermOfPaymentID,
	[TOPBlockStatusID] = @TOPBlockStatusID,
	[DeliveryDate] = @DeliveryDate,
	[ProcessCode] = @ProcessCode,
	[CancelRequestBy] = @CancelRequestBy,
	[IndentTransfer] = @IndentTransfer,
	[PickingTicket] = @PickingTicket,
	[SentPODate] = @SentPODate,
	[IsTransfer] = @IsTransfer,
    [DMSPRNo] = @DMSPRNo,
	[RowStatus] = @RowStatus,
	[CreatedBy] = @CreatedBy,
	--[CreatedTime] = @CreatedTime,
	[LastUpdateBy] = @LastUpdateBy,
	[LastUpdateTime] = GETDATE()	
WHERE
	[ID] = @ID
go

---------------------------------------------------------------------------------------------------------------  
-- Date Created : Thursday, July 15, 2010  
-- Created By : DNet Team by using CodeSmith v 2.6  
-- Rev History :  
-- Date Created : 06 Maret 2018  
-- Created By : Mitrais Team  
-- Rev History : Add Total Forecast  
---------------------------------------------------------------------------------------------------------------  
  
alter PROCEDURE up_UpdateSparePartPODetail  
 @ID int OUTPUT,  
 @SparePartPOID int,  
 @SparePartMasterID int,  
 @CheckListStatus varchar(2),  
 @Quantity int,  
 @RetailPrice money,  
 @EstimateStatus varchar(1),  
 @StopMark smallint,  
 @RowStatus smallint,  
 @CreatedBy varchar(20),  
 --@CreatedTime datetime,  
 @LastUpdateBy varchar(20),  
 --@LastUpdateTime datetime,  
 @TotalForecast int  
   
AS  
  
UPDATE [dbo].[SparePartPODetail]  
SET  
 [SparePartPOID] = @SparePartPOID,  
 [SparePartMasterID] = @SparePartMasterID,  
 [CheckListStatus] = @CheckListStatus,  
 [Quantity] = @Quantity,  
 [RetailPrice] = @RetailPrice,  
 [EstimateStatus] = @EstimateStatus,  
 [StopMark] = @StopMark,  
 [TotalForecast] = @TotalForecast,
 [RowStatus] = @RowStatus,  
 [CreatedBy] = @CreatedBy,  
 --[CreatedTime] = @CreatedTime,  
 [LastUpdateBy] = @LastUpdateBy,  
 [LastUpdateTime] = GETDATE()  
WHERE  
 [ID] = @ID
go

---------------------------------------------------------------------------------------------------------------
-- Date Created	: Thursday, June 23, 2011
-- Created By	: DNet Team by using CodeSmith v 2.6
-- Rev History	:
---------------------------------------------------------------------------------------------------------------

alter PROCEDURE up_UpdateSPKCustomer
	@ID int OUTPUT,
	@Code varchar(50),
	@ReffCode varchar(50),
	@TipeCustomer smallint,
	@TipePerusahaan smallint,
	@Name1 nvarchar(50),
	@Name2 nvarchar(50),
	@Name3 nvarchar(50),
	@Alamat nvarchar(100),
	@Kelurahan nvarchar(50),
	@Kecamatan nvarchar(50),
	@PostalCode nvarchar(10),
	@PreArea varchar(20),
	@CityID smallint,
	@PrintRegion varchar(1),
	@PhoneNo nvarchar(30),
	@OfficeNo nvarchar(30),
	@HomeNo nvarchar(30),
	@HpNo nvarchar(30),
	@Email nvarchar(50),
	@Status int,
	@MCPStatus smallint,
	@LKPPStatus SMALLINT,
	@SAPCustomerID int,
	@LKPPReference VARCHAR(50),
	@BusinessSectorDetailID INT,
	@ImagePath NVARCHAR(200),
	@RowStatus smallint,
	--@CreatedTime datetime,
	@CreatedBy nvarchar(20),
	--@LastUpdateTime datetime,
	@LastUpdateBy nvarchar(20)
	
AS

UPDATE	[dbo].[SPKCustomer]
SET
	[Code] = @Code,
	[ReffCode] = @ReffCode,
	[TipeCustomer] = @TipeCustomer,
	[TipePerusahaan] = @TipePerusahaan,
	[Name1] = @Name1,
	[Name2] = @Name2,
	[Name3] = @Name3,
	[Alamat] = @Alamat,
	[Kelurahan] = @Kelurahan,
	[Kecamatan] = @Kecamatan,
	[PostalCode] = @PostalCode,
	[PreArea] = @PreArea,
	[CityID] = @CityID,
	[PrintRegion] = @PrintRegion,
	[PhoneNo] = @PhoneNo,
	[OfficeNo] = @OfficeNo,
	[HomeNo] = @HomeNo,
	[HpNo] = @HpNo,
	[Email] = @Email,
	[Status] = @Status,
	[MCPStatus] = @MCPStatus,
	[LKPPStatus] = @LKPPStatus,
	[SAPCustomerID] = @SAPCustomerID,
	[LKPPReference] = @LKPPReference,
	[BusinessSectorDetailID] = @BusinessSectorDetailID,
	[ImagePath] = @ImagePath,
	[RowStatus] = @RowStatus,
	--[CreatedTime] = @CreatedTime,
	[CreatedBy] = @CreatedBy,
	[LastUpdateTime] = GETDATE(),
	[LastUpdateBy] = @LastUpdateBy	
WHERE
	[ID] = @ID
go

---------------------------------------------------------------------------------------------------------------
-- Date Created	: 09 Februari 2018
-- Created By	: Mitrais Team by using CodeSmith v 2.6
-- Rev History	:
---------------------------------------------------------------------------------------------------------------

alter PROCEDURE up_UpdateStandardCode
	@ID int OUTPUT,
	@Category varchar(100),
	@ValueId int,
	@ValueCode varchar(200)='',
	@ValueDesc varchar(200),
	@Sequence int,
	@RowStatus smallint,
	@CreatedBy varchar(20),
	--@CreatedTime datetime,
	@LastUpdateBy varchar(20)	--@LastUpdateTime datetime	
AS

UPDATE	[dbo].[StandardCode]
SET
	[Category] = @Category,
	[ValueId] = @ValueId,
	[ValueCode] = @ValueCode,
	[ValueDesc] = @ValueDesc,
	[Sequence ] = @Sequence ,
	[RowStatus] = @RowStatus,
	[CreatedBy] = @CreatedBy,
	--[CreatedTime] = @CreatedTime,
	[LastUpdateBy] = @LastUpdateBy,
	[LastUpdateTime] = GETDATE()	
WHERE
	[ID] = @ID
go

 

---------------------------------------------------------------------------------------------------------------
-- Date Created	: Friday, November 18, 2005
-- Created By	: DNet Team by using CodeSmith v 2.6
-- Rev History	: Mitrais Team
---------------------------------------------------------------------------------------------------------------

alter PROCEDURE up_UpdateTrTrainee
	@ID int OUTPUT,
	@SalesmanHeaderId INT,
	@Name varchar(50),
	@DealerID int,
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
	--@CreatedTime datetime,
	@LastUpdateBy varchar(20)	--@LastUpdateTime datetime	
AS

UPDATE	[dbo].[TrTrainee]
SET
	[SalesmanHeaderID] = @SalesmanHeaderId,
	[Name] = @Name,
	[DealerID] = @DealerID,
	[DealerBranchID] = @DealerBranchID,
	[BirthDate] = @BirthDate,
	[Gender] = @Gender,
	[NoKTP] = @NoKTP,
	[Email] = @Email,
	[StartWorkingDate] = @StartWorkingDate,
	[Status] = @Status,
	[JobPosition] = @JobPosition,
	[EducationLevel] = @EducationLevel,
	[Photo] = @Photo,
	[ShirtSize] = @ShirtSize,
	[RowStatus] = @RowStatus,
	[CreatedBy] = @CreatedBy,	
	--[CreatedTime] = @CreatedTime,
	[LastUpdateBy] = @LastUpdateBy,	
	[LastUpdateTime] = GETDATE()	
WHERE
	[ID] = @ID
go

set ANSI_NULLS off
go

--/****** Object:  Stored Procedure dbo.up_UpdateVechileColor    Script Date: 14/10/2005 11:06:14 ******/
---------------------------------------------------------------------------------------------------------------
-- Date Created	: Tuesday, September 27, 2005
-- Created By	: DNet Team by using CodeSmith v 2.6
-- Rev History	:
---------------------------------------------------------------------------------------------------------------

alter PROCEDURE up_UpdateVechileColor
	@ID int OUTPUT,
	@VechileTypeID int,
	@ColorCode varchar(4),
	@ColorIndName varchar(50),
	@ColorEngName varchar(50),
	@MaterialNumber varchar(20),
	@MaterialDescription varchar(100),
	@HeaderBOM varchar(20),
	@MarketCode varchar(30),
	@SpecialFlag varchar(1),
	@Status varchar(1),
	@RowStatus smallint,
	@CreatedBy varchar(20),
	--@CreatedTime datetime,
	@LastUpdateBy varchar(20)
	--@LastUpdateTime datetime
AS

UPDATE	[dbo].[VechileColor]
SET
	[VechileTypeID] = @VechileTypeID,
	[ColorCode] = @ColorCode,
	[ColorIndName] = @ColorIndName,
	[ColorEngName] = @ColorEngName,
	[MaterialNumber] = @MaterialNumber,
	[MaterialDescription] = @MaterialDescription,
	[HeaderBOM] = @HeaderBOM,
	[MarketCode] = @MarketCode,
	[SpecialFlag] = @SpecialFlag,
	[Status] = @Status,
	[RowStatus] = @RowStatus,
	[CreatedBy] = @CreatedBy,
	--[CreatedTime] = @CreatedTime,
	[LastUpdateBy] = @LastUpdateBy,
	[LastUpdateTime] = GETDATE()
WHERE
	[ID] = @ID
go

set ANSI_NULLS on
go

---------------------------------------------------------------------------------------------------------------
-- Date Created	: 17 Nopember 2005
-- Created By	: DNet Team by using CodeSmith v 2.6
-- Rev History	:
---------------------------------------------------------------------------------------------------------------

alter PROCEDURE up_UpdateVechileModel
	@ID int OUTPUT,
	@VechileModelCode varchar(4),
	@CategoryID int,
	@Description varchar(40),
	@VechileModelIndCode varchar(30) = null,
	@IndDescription varchar(40) =null,
	@SalesFlag tinyint = 0,
	@RowStatus smallint,
	@CreatedBy varchar(20),
	--@CreatedTime datetime,
	@LastUpdateBy varchar(20)
	--@LastUpdateTime datetime
AS

UPDATE	[dbo].[VechileModel]
SET
	[VechileModelCode] = @VechileModelCode,
	[CategoryID] = @CategoryID,
	[Description] = @Description,
	[VechileModelIndCode] = ISNULL(@VechileModelIndCode,VechileModelIndCode),
	[IndDescription] = ISNULL(@IndDescription, IndDescription),
	[SalesFlag] = @SalesFlag,
	[RowStatus] = @RowStatus,
	[CreatedBy] = @CreatedBy,
	--[CreatedTime] = @CreatedTime,
	[LastUpdateBy] = @LastUpdateBy,
	[LastUpdateTime] = GETDATE()
WHERE
	[ID] = @ID
go

---------------------------------------------------------------------------------------------------------------
-- Date Created	: Tuesday, February 11, 2014
-- Created By	: DNet Team by using CodeSmith v 2.6
-- Rev History	:
---------------------------------------------------------------------------------------------------------------

alter PROCEDURE up_UpdateVechileType
	@ID smallint OUTPUT,
	@VechileTypeCode varchar(4),
	@ModelID smallint,
	@CategoryID tinyint,
	@ProductCategoryID smallint,
	@Description varchar(100),
	@Status varchar(1),
	@VehicleClassID int,
	@IsVehicleKind1 tinyint,
	@IsVehicleKind2 tinyint,
	@IsVehicleKind3 tinyint,
	@IsVehicleKind4 tinyint,
	@MaxTOPDays int,
	@SAPModel nvarchar(20) = NULL,
	@SegmentType varchar(40) = NULL,
    @VariantType varchar(30) = NULL,
    @TransmitType varchar(25) = NULL,
    @DriveSystemType varchar(25) = NULL,
    @SpeedType varchar(2) = NULL,
    @FuelType varchar(10) = NULL,
	@RowStatus smallint,
	@CreatedBy varchar(20),
	--@CreatedTime datetime,
	@LastUpdateBy varchar(20)	--@LastUpdateTime datetime	
AS

UPDATE	[dbo].[VechileType]
SET
	[VechileTypeCode] = @VechileTypeCode,
	[ModelID] = @ModelID,
	[CategoryID] = @CategoryID,
	/*set productcategoryid by donas on 20141001153*/
	[ProductCategoryID] = (select c.ProductCategoryID from Category c where c.ID=@CategoryID),--@ProductCategoryID,
	[Description] = @Description,
	[Status] = @Status,
	[VehicleClassID] = @VehicleClassID,
	[IsVehicleKind1] = @IsVehicleKind1,
	[IsVehicleKind2] = @IsVehicleKind2,
	[IsVehicleKind3] = @IsVehicleKind3,
	[IsVehicleKind4] = @IsVehicleKind4,
	[SAPModel] = ISNULL(@SAPModel, SAPModel),
	[MaxTOPDays] = @MaxTOPDays,
	[SegmentType] = ISNULL(@SegmentType, SegmentType),
	[VariantType] = ISNULL(@VariantType, VariantType),
	[TransmitType] = ISNULL(@TransmitType, TransmitType),
	[DriveSystemType] = ISNULL(@DriveSystemType, DriveSystemType),
	[SpeedType] = ISNULL(@SpeedType, SpeedType),
	[FuelType] = ISNULL(@FuelType, FuelType),
	[RowStatus] = @RowStatus,
	[CreatedBy] = @CreatedBy,
	--[CreatedTime] = @CreatedTime,
	[LastUpdateBy] = @LastUpdateBy,
	[LastUpdateTime] = GETDATE()
WHERE
	[ID] = @ID
go

---------------------------------------------------------------------------------------------------------------  
-- Date Created : Thursday, September 05, 2013  
-- Created By : DNet Team by using CodeSmith v 2.6  
-- Rev History :  
---------------------------------------------------------------------------------------------------------------  
  
alter PROCEDURE up_UpdateWSCDetail  
 @ID int OUTPUT,  
 @WSCHeaderID int,  
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
 --@CreatedTime datetime,  
 @LastUpdateBy varchar(20) --@LastUpdateTime datetime   
AS  
  
UPDATE [dbo].[WSCDetail]  
SET  
 [WSCHeaderID] = @WSCHeaderID,  
 [WSCType] = @WSCType,  
 [LaborMasterID] = @LaborMasterID,  
 [PositionCode] = @PositionCode,
 [WorkCode] = @WorkCode,
 [SparePartMasterID] = @SparePartMasterID,  
 [Quantity] = @Quantity,  
 [PartPrice] = @PartPrice,  
 [MainPart] = @MainPart,  
 [QuantityReceived] = @QuantityReceived,  
 [ReceivedBy] = @ReceivedBy,  
 [ReceivedDate] = @ReceivedDate,  
 [Status] = @Status,
 [RowStatus] = @RowStatus,  
 [CreatedBy] = @CreatedBy,  
 --[CreatedTime] = @CreatedTime,  
 [LastUpdateBy] = @LastUpdateBy,  
 [LastUpdateTime] = GETDATE()   
WHERE  
 [ID] = @ID
go

---------------------------------------------------------------------------------------------------------------  
-- Date Created : Friday, November 18, 2011  
-- Created By : DNet Team by using CodeSmith v 2.6  
-- Rev History :  
---------------------------------------------------------------------------------------------------------------  
  
alter PROCEDURE up_UpdateWSCDetailBB  
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
 --@CreatedTime datetime,  
 @LastUpdateBy varchar(20) 
 --@LastUpdateTime datetime   
AS  
  
UPDATE [dbo].[WSCDetailBB]  
SET  
 [WSCHeaderBBID] = @WSCHeaderBBID,  
 [WSCType] = @WSCType,  
 [LaborMasterID] = @LaborMasterID,  
 [PositionCode] = @PositionCode,
 [WorkCode] = @WorkCode,
 [SparePartMasterID] = @SparePartMasterID,  
 [Quantity] = @Quantity,  
 [PartPrice] = @PartPrice,  
 [MainPart] = @MainPart, 
 [QuantityReceived] = @QuantityReceived,
 [ReceivedBy] = @ReceivedBy,
 [ReceivedDate] = @ReceivedDate, 
 [Status] = @Status,	
 [RowStatus] = @RowStatus,  
 [CreatedBy] = @CreatedBy,  
 --[CreatedTime] = @CreatedTime,  
 [LastUpdateBy] = @LastUpdateBy,  
 [LastUpdateTime] = GETDATE()   
WHERE  
 [ID] = @ID
go

---------------------------------------------------------------------------------------------------------------      
-- Date Created : Tuesday, November 29, 2005      
-- Created By : DNet Team by using CodeSmith v 2.6      
-- Rev History :      
---------------------------------------------------------------------------------------------------------------      
      
alter PROCEDURE up_UpdateWSCHeader      
 @ID int OUTPUT,      
 @ClaimType varchar(2),      
 @DealerID int,      
 @DealerBranchID int,
 @ClaimNumber varchar(13),      
 @RefClaimNumber varchar(6),      
 @ChassisMasterID int,      
 @FailureDate datetime,  
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
 @EvidenceRepair varchar(1),  
 @EvidenceWSCLetter varchar(1),  
 @EvidenceWSCTechnical varchar(1),  
 @Causes varchar(1000),  
 @Results varchar(1000),  
 @Notes varchar(1000),     
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
 @CreatedBy varchar(50),      
 --@CreatedTime datetime,      
 @LastUpdateBy varchar(50)      
 --@LastUpdateTime datetime      
       
AS      
      
UPDATE [dbo].[WSCHeader]      
SET      
 [ClaimType] = @ClaimType,      
 [DealerID] = @DealerID,      
 [DealerBranchID] = @DealerBranchID,
 [ClaimNumber] = @ClaimNumber,      
 [RefClaimNumber] = @RefClaimNumber,      
 [ChassisMasterID] = @ChassisMasterID,   
 [FailureDate] = @FailureDate,     
 [ServiceDate] = @ServiceDate,      
 [Miliage] = @Miliage,      
 [PQR] = @PQR,      
 [PQRStatus] = @PQRStatus,      
 [CodeA] = @CodeA,      
 [CodeB] = @CodeB,      
 [CodeC] = @CodeC,      
 [Description] = @Description,      
 [EvidencePhoto] = @EvidencePhoto,      
 [EvidenceInvoice] = @EvidenceInvoice,      
 [EvidenceDmgPart] = @EvidenceDmgPart,      
 [EvidenceRepair] = @EvidenceRepair,  
 [EvidenceWSCLetter] = @EvidenceWSCLetter,  
 [EvidenceWSCTechnical] = @EvidenceWSCTechnical,  
 [Causes] = @Causes,  
 [Results] = @Results,  
 [Notes] = @Notes,  
 [ReqDmgPart] = @ReqDmgPart,      
 [ReqDmgPartBy] = @ReqDmgPartBy,      
 [ReqDmgPartTime] = @ReqDmgPartTime,      
 [NotificationNumber] = @NotificationNumber,      
 [DecideDate] = @DecideDate,      
 [Status] = @Status,      
 [ClaimStatus] = @ClaimStatus,      
 [ReasonID] = @ReasonID,      
 [LaborAmount] = @LaborAmount,      
 [PartAmount] = @PartAmount,      
 [PartReceiveBy] = @PartReceiveBy,      
 [PartReceiveTime] = @PartReceiveTime,      
 [DownLoadBy] = @DownLoadBy,      
 [DownLoadTime] = @DownLoadTime,      
 [ResponseTime] = @ResponseTime,      
 [WorkOrderNumber] = @WorkOrderNumber,    
 [RowStatus] = @RowStatus,      
 [CreatedBy] = @CreatedBy,      
 --[CreatedTime] = @CreatedTime,      
 [LastUpdateBy] = @LastUpdateBy,       
 [LastUpdateTime] = GETDATE()      
       
WHERE      
 [ID] = @ID
go

---------------------------------------------------------------------------------------------------------------    
-- Date Created : Friday, November 18, 2011    
-- Created By : DNet Team by using CodeSmith v 2.6    
-- Rev History :    
---------------------------------------------------------------------------------------------------------------    
    
alter PROCEDURE up_UpdateWSCHeaderBB    
 @ID int OUTPUT,    
 @ClaimType varchar(2),    
 @DealerID smallint,  
 @DealerBranchID int = null,  
 @ClaimNumber varchar(6),    
 @RefClaimNumber varchar(6),    
 @ChassisMasterBBID int,  
 @FailureDate datetime,  
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
 @EvidenceRepair varchar(1),
 @EvidenceWSCLetter varchar(1),
 @EvidenceWSCTechnical varchar(1),
 @Causes varchar(1000),
 @Results varchar(1000),
 @Notes varchar(1000),
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
 @CreatedBy varchar(50),    
 --@CreatedTime datetime,    
 @LastUpdateBy varchar(50) --@LastUpdateTime datetime     
AS    
    
UPDATE [dbo].[WSCHeaderBB]    
SET    
 [ClaimType] = @ClaimType,    
 [DealerID] = @DealerID,    
 [DealerBranchID] = @DealerBranchID,
 [ClaimNumber] = @ClaimNumber,    
 [RefClaimNumber] = @RefClaimNumber,    
 [ChassisMasterBBID] = @ChassisMasterBBID, 
 [FailureDate] = @FailureDate,   
 [ServiceDate] = @ServiceDate,    
 [Miliage] = @Miliage,    
 [PQR] = @PQR,    
 [PQRStatus] = @PQRStatus,    
 [CodeA] = @CodeA,    
 [CodeB] = @CodeB,    
 [CodeC] = @CodeC,    
 [Description] = @Description,    
 [EvidencePhoto] = @EvidencePhoto,    
 [EvidenceInvoice] = @EvidenceInvoice,    
 [EvidenceDmgPart] = @EvidenceDmgPart,   
 [EvidenceRepair] = @EvidenceRepair,
 [EvidenceWSCLetter] = @EvidenceWSCLetter,
 [EvidenceWSCTechnical] = @EvidenceWSCTechnical,
 [Causes] = @Causes,
 [Results] = @Results,
 [Notes] = @Notes, 
 [ReqDmgPart] = @ReqDmgPart,    
 [ReqDmgPartBy] = @ReqDmgPartBy,    
 [ReqDmgPartTime] = @ReqDmgPartTime,    
 [NotificationNumber] = @NotificationNumber,    
 [DecideDate] = @DecideDate,    
 [Status] = @Status,    
 [ClaimStatus] = @ClaimStatus,    
 [ReasonID] = @ReasonID,    
 [LaborAmount] = @LaborAmount,    
 [PartAmount] = @PartAmount,    
 [PartReceiveBy] = @PartReceiveBy,    
 [PartReceiveTime] = @PartReceiveTime,    
 [DownLoadBy] = @DownLoadBy,    
 [DownLoadTime] = @DownLoadTime,    
 [ResponseTime] = @ResponseTime,   
 [WorkOrderNumber] = @WorkOrderNumber,   
 [RowStatus] = @RowStatus,    
 [CreatedBy] = @CreatedBy,    
 --[CreatedTime] = @CreatedTime,    
 [LastUpdateBy] = @LastUpdateBy,    
 [LastUpdateTime] = GETDATE()     
WHERE    
 [ID] = @ID
go

commit
go




