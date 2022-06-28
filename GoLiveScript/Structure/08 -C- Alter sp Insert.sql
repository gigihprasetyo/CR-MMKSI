set xact_abort on
go

begin transaction
go

set ANSI_NULLS on
go

alter PROCEDURE SP_InsertPrice
	    @ID INT OUTPUT  ,
	   @VechileColorID SMALLINT ,
	   @DealerCode VARCHAR(10) ,
	   @ValidFrom DATETIME ,
	   @BasePrice MONEY ,
	   @OptionPrice MONEY ,
	   @PPN_BM MONEY ,
	   @PPN MONEY ,
	   @PPh22 MONEY ,
	   @Interest MONEY ,
	   @FactoringInt MONEY ,
	   @PPh23 MONEY ,
	   @Status VARCHAR(1) ,
	   @DiscountReward MONEY ,
	   @RowStatus SMALLINT ,
	   @CreatedBy VARCHAR(20) ,
	--@CreatedTime datetime,
	   @LastUpdateBy VARCHAR(20)	--@LastUpdateTime datetime	
AS
	   BEGIN

	;
			 WITH	CTE_Dealer
					  AS ( SELECT	d.ID
						   FROM		dbo.Dealer d
						   WHERE	( d.[ID] NOT IN ( 425, 1, 2 ) )
									AND ( d.RowStatus = 0 )
									AND ( d.[Status] = 1 )
									AND ( d.DealerCode = @DealerCode
										  OR @DealerCode = ''
										)
						 ),
					CTE_PRICE
					  AS ( SELECT	D.ID AS DealerID ,
									v.ID AS VechileColorID ,
									@ValidFrom ValidFrom ,
									@BasePrice BasePrice ,
									@OptionPrice OptionPrice ,
									@PPN_BM PPN_BM ,
									@PPN PPN ,
									@PPh22 PPh22 ,
									@Interest Interest ,
									@FactoringInt FactoringInt ,
									@PPh23 PPh23 ,
									@Status [Status] ,
									@DiscountReward DiscountReward ,
									@RowStatus RowStatus ,
									@CreatedBy CreatedBy
						   FROM		CTE_Dealer D
						   INNER JOIN dbo.VechileColor v ON 1 = 1
															AND v.ID = @VechileColorID
															AND v.RowStatus = 0
						 )
				  MERGE dbo.Price AS T
				  USING CTE_PRICE AS S
				  ON T.DealerID = S.DealerID
					AND T.VechileColorID = S.VechileColorID
					AND S.ValidFrom = T.ValidFrom
					AND T.RowStatus = 0
				  WHEN MATCHED THEN
					UPDATE SET [BasePrice] = S.BasePrice ,
							  [OptionPrice] = @OptionPrice ,
							  [PPN_BM] = @PPN_BM ,
							  [PPN] = @PPN ,
							  [PPh22] = @PPh22 ,
							  [Interest] = @Interest ,
							  [FactoringInt] = @FactoringInt ,
							  [PPh23] = @PPh23 ,
							  [Status] = @Status ,
							  [DiscountReward] = @DiscountReward ,
							  [RowStatus] = @RowStatus ,
							  [LastUpdateBy] = @CreatedBy ,
							  [LastUpdateTime] = GETDATE()
				  WHEN NOT MATCHED THEN
					INSERT ( VechileColorID ,
							 DealerID ,
							 ValidFrom ,
							 BasePrice ,
							 OptionPrice ,
							 PPN_BM ,
							 PPN ,
							 PPh22 ,
							 Interest ,
							 FactoringInt ,
							 PPh23 ,
							 [Status] ,
							 DiscountReward ,
							 RowStatus ,
							 CreatedBy ,
							 CreatedTime ,
							 LastUpdateBy ,
							 LastUpdateTime
						   )
					VALUES ( S.VechileColorID ,
							 S.DealerID ,
							 @ValidFrom ,
							 @BasePrice ,
							 @OptionPrice ,
							 @PPN_BM ,
							 @PPN ,
							 @PPh22 ,
							 @Interest ,
							 @FactoringInt ,
							 @PPh23 ,
							 @Status ,
							 @DiscountReward ,
							 @RowStatus ,
							 @CreatedBy ,
							 GETDATE() ,
							 @CreatedBy ,
							 GETDATE()
						   ) ; 

						    SET @ID = @@IDENTITY
	   END
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
-- Date Created	: Thursday, September 29, 2005
-- Created By	: DNet Team by using CodeSmith v 2.6
-- Rev History	:
---------------------------------------------------------------------------------------------------------------
alter PROCEDURE up_InsertArea1
	@ID int OUTPUT,
	@AreaCode varchar(10),
	@Description varchar(100),
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


INSERT	INTO	[dbo].[Area1]
VALUES
(
	@AreaCode,
	@Description,
	@PICSales,
	@PICServices,
	@PICSpareparts,
	@MainAreaID,
	@RowStatus,
	@CreatedBy,
	GETDATE(),
	@LastUpdateBy,
	GETDATE()
)

SET @ID = @@IDENTITY
go

set ANSI_NULLS off
go

--/****** Object:  Stored Procedure dbo.up_InsertArea2    Script Date: 14/10/2005 11:06:16 ******/
---------------------------------------------------------------------------------------------------------------
-- Date Created	: Thursday, September 29, 2005
-- Created By	: DNet Team by using CodeSmith v 2.6
-- Rev History	:
---------------------------------------------------------------------------------------------------------------

alter PROCEDURE up_InsertArea2
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


INSERT	INTO	[dbo].[Area2]
VALUES
(
	@AreaCode,
	@Description,
	@ACFinishUnit,
	@ACSparePart,
	@ACService,
	@Area1ID,
	@RowStatus,
	@CreatedBy,
	GETDATE(),
	@LastUpdateBy,
	GETDATE()
)

SET @ID = @@IDENTITY
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
-- Date Created	: Wednesday, January 17, 2018
-- Created By	: DNet Team by using CodeSmith v 2.6
-- Rev History	:
---------------------------------------------------------------------------------------------------------------

alter PROCEDURE up_InsertAssistPartSales
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
go

---------------------------------------------------------------------------------------------------------------
-- Date Created	: Wednesday, January 17, 2018
-- Created By	: DNet Team by using CodeSmith v 2.6
-- Rev History	:
---------------------------------------------------------------------------------------------------------------

alter PROCEDURE up_InsertAssistPartStock
	@ID int OUTPUT,
	@AssistUploadLogID int,
	@Month nchar(10),
	@Year nchar(10),
	@DealerID int,
	@DealerCode varchar(30),
	@DealerBranchID INT,
	@DealerBranchCode varchar(50),
	@SparepartMasterID int,
	@NoParts varchar(50),
	@JumlahStokAwal float,
	@JumlahDatang float,
	@HargaBeli money,	
	@RemarksSystem varchar(MAX),
	@StatusAktif smallint,
	@ValidateSystemStatus smallint,
	@RowStatus smallint,
	@CreatedBy varchar(20),
	--@CreatedTime datetime,
	@LastUpdateBy varchar(20)	--@LastUpdateTime datetime	
AS
INSERT	INTO	[dbo].[AssistPartStock]
([AssistUploadLogID]
,[Month]
,[Year]
,[DealerID]
,[DealerCode]
,[DealerBranchID]
,[DealerBranchCode]
,[SparepartMasterID]
,[NoParts]
,[JumlahStokAwal]
,[JumlahDatang]
,[HargaBeli]
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
	@Month,
	@Year,
	@DealerID,
	@DealerCode,
	@DealerBranchID ,
	@DealerBranchCode ,
	@SparepartMasterID,
	@NoParts,
	@JumlahStokAwal,
	@JumlahDatang,	
	@HargaBeli,
	@RemarksSystem,
	@StatusAktif,
	@ValidateSystemStatus,
	@RowStatus,
	@CreatedBy,
	GETDATE(),	
	@LastUpdateBy,
	GETDATE())

	
SET @ID = @@IDENTITY
go

---------------------------------------------------------------------------------------------------------------
-- Date Created	: Wednesday, January 17, 2018
-- Created By	: DNet Team by using CodeSmith v 2.6
-- Rev History	:
---------------------------------------------------------------------------------------------------------------

alter PROCEDURE up_InsertAssistServiceIncoming
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
	@DealerBranchID INT,
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
INSERT	INTO	[dbo].[AssistServiceIncoming]
([AssistUploadLogID]
           ,[TglBukaTransaksi]
           ,[WaktuMasuk]
           ,[TglTutupTransaksi]
           ,[WaktuKeluar]
           ,[DealerID]
           ,[DealerCode]
           ,[TrTraineMekanikID]
           ,[KodeMekanik]
           ,[NoWorkOrder]
           ,[ChassisMasterID]
           ,[KodeChassis]
           ,[WorkOrderCategoryID]
           ,[WorkOrderCategoryCode]
           ,[KMService]
           ,[ServicePlaceID]
           ,[ServicePlaceCode]
           ,[ServiceTypeID]
           ,[ServiceTypeCode]
           ,[TotalLC]
           ,[MetodePembayaran]
           ,[Model]
           ,[Transmition]
           ,[DriveSystem]
           ,[DealerBranchID]
           ,[DealerBranchCode]
           ,[RemarksSystem]
           ,[RemarksSpecial]
           ,[RemarksBM]
		   ,[WOStatus]
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
	@TglBukaTransaksi,
	Convert(time,@WaktuMasuk),
	@TglTutupTransaksi,
	Convert(time,@WaktuKeluar),
	@DealerID,
	@DealerCode,
	@TrTraineMekanikID,
	@KodeMekanik,
	@NoWorkOrder,
	@ChassisMasterID,
	@KodeChassis,
	@WorkOrderCategoryID,
	@WorkOrderCategoryCode,
	@KMService,
	@ServicePlaceID,
	@ServicePlaceCode,
	@ServiceTypeID,
	@ServiceTypeCode,
	@TotalLC,
	@MetodePembayaran,
	@Model,
	@Transmition,
	@DriveSystem,
	@DealerBranchID,
	@DealerBranchCode,
	@RemarksSystem,
	@RemarksSpecial,
	@RemarksBM,
	@WOStatus,
	@StatusAktif,
	@ValidateSystemStatus,
	@RowStatus,
	@CreatedBy,
	GETDATE(),	
	@LastUpdateBy,
	GETDATE())

	
SET @ID = @@IDENTITY
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

alter PROCEDURE up_InsertCityPart
	@ID int OUTPUT,
	@ProvinceID int,
	@CityId INT,
	@CityName varchar(50),
	@CityCode varchar(10),
	@RowStatus smallint,
	@CreatedBy varchar(20),
	--@CreatedTime datetime,
	@LastUpdateBy varchar(20)	--@LastUpdateTime datetime	
AS
INSERT	INTO	[dbo].[CityPart]
VALUES
(
	@ProvinceID,
	@CityId,
	@CityName,
	@CityCode,
	@RowStatus,
	@CreatedBy,
	GETDATE(),	
	@LastUpdateBy,
	GETDATE())

	
SET @ID = @@IDENTITY
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

alter PROCEDURE up_InsertCityPart
	@ID int OUTPUT,
	@ProvinceID int,
	@CityId INT,
	@CityName varchar(50),
	@CityCode varchar(10),
	@RowStatus smallint,
	@CreatedBy varchar(20),
	--@CreatedTime datetime,
	@LastUpdateBy varchar(20)	--@LastUpdateTime datetime	
AS
INSERT	INTO	[dbo].[CityPart]
VALUES
(
	@ProvinceID,
	@CityId,
	@CityName,
	@CityCode,
	@RowStatus,
	@CreatedBy,
	GETDATE(),	
	@LastUpdateBy,
	GETDATE())

	
SET @ID = @@IDENTITY
go

----===================== EXECUTE STORE PROCEDURE =============================================================================================

---------------------------------------------------------------------------------------------------------------
-- Date Created	: Thursday, August 03, 2017
-- Created By	: DNet Team by using CodeSmith v 2.6
-- Rev History	:
---------------------------------------------------------------------------------------------------------------

alter PROCEDURE up_InsertCustomerCase
      @ID INT OUTPUT ,
      @DealerID SMALLINT ,
      @SalesforceID NVARCHAR(255) ,
      @CaseNumber NVARCHAR(255) ,
      @CustomerName NVARCHAR(50) ,
      @Phone NVARCHAR(50) ,
      @Email NVARCHAR(50) ,
      @Category NVARCHAR(50) ,
      @SubCategory1 NVARCHAR(50) ,
      @SubCategory2 NVARCHAR(50) ,
      @SubCategory3 NVARCHAR(50) ,
      @SubCategory4 NVARCHAR(50) ,
      @CallerType NCHAR(10) ,
      @CarType NVARCHAR(50) ,
      @Variant NVARCHAR(50) ,
      @EngineNumber NVARCHAR(50) ,
      @ChassisNumber NVARCHAR(50) ,
      @Odometer INT ,
      @PlateNumber NVARCHAR(20) ,
      @Priority SMALLINT ,
      @CaseNumberReff NVARCHAR(255) ,
      @CaseDate DATETIME ,
      @Subject VARCHAR(255) ,
      @Description NVARCHAR(MAX) ,
      @Status SMALLINT ,
      @ReservationNumber VARCHAR(50) = '' ,
      @RowStatus SMALLINT ,
      @CreatedBy VARCHAR(20) ,
	--@CreatedTime datetime,
      @LastUpdateBy VARCHAR(20) ,
      @LastUpdateTIme DATETIME
AS
IF @Status IN ( '0' )
   BEGIN
         SET @CreatedBy = 'Salesforce'
   END 

INSERT  INTO [dbo].[CustomerCase]
        (
          [DealerID] ,
          [SalesforceID] ,
          [CaseNumber] ,
          [CustomerName] ,
          [Phone] ,
          [Email] ,
          [Category] ,
          [SubCategory1] ,
          [SubCategory2] ,
          [SubCategory3] ,
          [SubCategory4] ,
          [CallerType] ,
          [CarType] ,
          [Variant] ,
          [EngineNumber] ,
          [ChassisNumber] ,
          [Odometer] ,
          [PlateNumber] ,
          [Priority] ,
          [CaseNumberReff] ,
          [CaseDate] ,
          [Subject] ,
          [Description] ,
          [Status] ,
          [ReservationNumber] ,
          [RowStatus] ,
          [CreatedBy] ,
          [CreatedTime] ,
          [LastUpdateBy] ,
          [LastUpdateTime]
        )
VALUES  (
          @DealerID ,
          @SalesforceID ,
          @CaseNumber ,
          @CustomerName ,
          @Phone ,
          @Email ,
          @Category ,
          @SubCategory1 ,
          @SubCategory2 ,
          @SubCategory3 ,
          @SubCategory4 ,
          @CallerType ,
          @CarType ,
          @Variant ,
          @EngineNumber ,
          @ChassisNumber ,
          @Odometer ,
          @PlateNumber ,
          @Priority ,
          @CaseNumberReff ,
          @CaseDate ,
          @Subject ,
          @Description ,
          @Status ,
          @ReservationNumber ,
          @RowStatus ,
          @CreatedBy ,
          GETDATE() ,
          @LastUpdateBy ,
          @LastUpdateTIme
        )

	
SET @ID = @@IDENTITY


-- Jika status case new 
IF @Status IN ( '0' )
   BEGIN
         INSERT INTO [dbo].[CustomerCaseResponse]
         VALUES ( @ID, @Subject, NULL, @Status, 1, @RowStatus, @CreatedBy, GETDATE(), @LastUpdateBy, @LastUpdateTIme )
   END
go

set ANSI_NULLS off
go

---------------------------------------------------------------------------------------------------------------
-- Date Created	: Friday, August 25, 2017
-- Created By	: DNet Team by using CodeSmith v 2.6
-- Rev History	:
---------------------------------------------------------------------------------------------------------------

alter PROCEDURE up_InsertDealerBranch
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
INSERT	INTO	[dbo].[DealerBranch]
VALUES
(
	@DealerID,
	@Name,
	@Status,
	@Address,
	@CityID,
	@ZipCode,
	@ProvinceID,
	@Phone,
	@Fax,
	@Website,
	@Email,
	@TypeBranch,
	@DealerBranchCode,
	@Term1,
	@Term2,
	@MainAreaID,
	@Area1ID,
	@Area2ID,
	@BranchAssignmentNo,
	@BranchAssignmentDate,
	@SalesUnitFlag,
	@ServiceFlag,
	@SparepartFlag,
	@RowStatus,
	@CreatedBy,
	GETDATE(),	
	@LastUpdateBy,
	GETDATE())

	
SET @ID = @@IDENTITY
go

---------------------------------------------------------------------------------------------------------------
-- Date Created	: 15 Nopember 2005
-- Created By	: DNet Team by using CodeSmith v 2.6
-- Rev History	:
---------------------------------------------------------------------------------------------------------------

alter PROCEDURE up_InsertDepositLine
    @ID INT OUTPUT ,
    @DepositID INT ,
    @DocumentNo VARCHAR(20) ,
    @PostingDate DATETIME ,
    @ClearingDate DATETIME ,
    @Debit MONEY ,
    @Credit MONEY ,
    @ReferenceNo VARCHAR(20) ,
    @InvoiceNo VARCHAR(20) ,
    @Remark VARCHAR(100) ,
    @PaymentType TINYINT ,
    @RowStatus SMALLINT ,
    @CreatedBy VARCHAR(20) ,
	--@CreatedTime datetime,
    @LastUpdateBy VARCHAR(20)
	--@LastUpdateTime datetime
AS
    BEGIN

        INSERT  INTO dbo.DepositLine
                ( DepositID ,
                  DocumentNo ,
                  PostingDate ,
                  ClearingDate ,
                  Debit ,
                  Credit ,
                  ReferenceNo ,
                  InvoiceNo ,
                  Remark ,
                  PaymentType ,
                  RowStatus ,
                  CreatedBy ,
                  CreatedTime ,
                  LastUpdateBy ,
                  LastUpdateTime
                )
        VALUES  ( @DepositID ,
                  @DocumentNo ,
                  @PostingDate ,
                  @ClearingDate ,
                  @Debit ,
                  @Credit ,
                  @ReferenceNo ,
                  @InvoiceNo ,
                  @Remark ,
                  @PaymentType ,
                  @RowStatus ,
                  @CreatedBy ,
                  GETDATE() ,
                  @LastUpdateBy ,
                  GETDATE()
                )

        SET @ID = @@IDENTITY





    END
go

set ANSI_NULLS on
go

---------------------------------------------------------------------------------------------------------------
-- Date Created	: Thursday, June 23, 2011
-- Created By	: DNet Team by using CodeSmith v 2.6
-- Rev History	:
---------------------------------------------------------------------------------------------------------------
alter PROCEDURE up_InsertEndCustomer
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
	--@CreatedTime datetime,
	@LastUpdateBy varchar(20)	--@LastUpdateTime datetime	
AS
INSERT	INTO	[dbo].[EndCustomer]
VALUES
(
	@ProjectIndicator,
	@RefChassisNumberID,
	@CustomerID,
	@Name1,
	@FakturDate,
	@OpenFakturDate,
	@FakturNumber,
	@AreaViolationFlag,
	@AreaViolationPaymentMethodID,
	@AreaViolationyAmount,
	@AreaViolationBankName,
	@AreaViolationGyroNumber,
	@PenaltyFlag,
	@PenaltyPaymentMethodID,
	@PenaltyAmount,
	@PenaltyBankName,
	@PenaltyGyroNumber,
	@ReferenceLetterFlag,
	@ReferenceLetter,
	@SaveBy,
	@SaveTime,
	@ValidateBy,
	@ValidateTime,
	@ConfirmBy,
	@ConfirmTime,
	@DownloadBy,
	@DownloadTime,
	@PrintedBy,
	@PrintedTime,
	@CleansingCustomerID,
	@MCPHeaderID,
	@MCPStatus,
	@LKPPHeaderID,
	@LKPPStatus,
	@Remark1,
	@Remark2,
	@HandoverDate,
	@IsTemporary,
	@RowStatus,
	@CreatedBy,
	GETDATE(),	
	@LastUpdateBy,
	GETDATE())
SET @ID = @@IDENTITY
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

alter PROCEDURE up_InsertEstimationEquipDetail
	@ID int OUTPUT,
	@EstimationEquipHeaderID int,
	@SparePartMasterID int,
	@Harga decimal(19, 9),
	@Discount decimal(7, 5),
	@EstimationUnit int,
	@Status smallint,
	@ConfirmedDate datetime,
	@Remark varchar(500),
	@TotalForecast int, 
	@RowStatus smallint,
	@CreatedBy varchar(20),
	--@CreatedTime datetime,
	@LastUpdatedBy varchar(20)--	@LastUpdatedTime datetime,
	
AS
INSERT	INTO	[dbo].[EstimationEquipDetail]
VALUES
(
	@EstimationEquipHeaderID,
	@SparePartMasterID,
	@Harga,
	@Discount,
	@EstimationUnit,
	@Status,
	@ConfirmedDate,
	@Remark,
    @TotalForecast,
	@RowStatus,
	@CreatedBy,
	GETDATE(),	
	@LastUpdatedBy,
	GETDATE())--@LastUpdatedTime)

	
SET @ID = @@IDENTITY
go

---------------------------------------------------------------------------------------------------------------
-- Date Created	: Tuesday, August 18, 2009
-- Created By	: DNet Team by using CodeSmith v 2.6
-- Rev History	:
---------------------------------------------------------------------------------------------------------------

alter PROCEDURE up_InsertEstimationEquipHeader
	@ID int OUTPUT,
	@EstimationNumber varchar(13),
	@DealerID smallint,
	@DepositBKewajibanHeaderID int,
	@Status smallint,
	@DMSPRNo varchar(50),
	@RowStatus smallint,
	@CreatedBy varchar(20),
	--@CreatedTime datetime,
	@LastUpdatedBy varchar(20)
	--@LastUpdatedTime datetime
	
AS

Declare @EstimationNumberGenerated  varchar(13)
declare @requestdate datetime
set @requestdate = getdate()
set @EstimationNumberGenerated=dbo.ufn_CreateEstimationEquipNumber(@requestdate,@DealerId)

INSERT	INTO	[dbo].[EstimationEquipHeader]
VALUES
(
	@EstimationNumberGenerated,
	@DealerID,
	@DepositBKewajibanHeaderID,
	@Status,
	@DMSPRNo,
	@RowStatus,
	@CreatedBy,
	GETDATE(),	
	@LastUpdatedBy,
	GETDATE()
)

	
SET @ID = @@IDENTITY
go

---------------------------------------------------------------------------------------------------------------
-- Date Created	: Tuesday, October 04, 2016
-- Created By	: DNet Team by using CodeSmith v 2.6
-- Rev History	:
---------------------------------------------------------------------------------------------------------------

alter PROCEDURE up_InsertFreeService
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
	@LastUpdateBy varchar(20)	--@LastUpdateTime datetime	
AS
INSERT	INTO	[dbo].[FreeService]
VALUES
(
	@Status,
	@ChassisMasterID,
	@FSKindID,
	@MileAge,
	@ServiceDate,
	@ServiceDealerID,
	@DealerBranchID,
	@SoldDate,
	@NotificationNumber,
	@NotificationType,
	@TotalAmount,
	@LabourAmount,
	@PartAmount,
	@PPNAmount,
	@PPHAmount,
	@Reject,
	@Reason,
	@ReleaseBy,
	@ReleaseDate,
	@VisitType,
	@FleetRequestID,
	@WorkOrderNumber,
	@RowStatus,
	@CreatedBy,
	GETDATE(),	
	@LastUpdateBy,
	GETDATE())

	
SET @ID = @@IDENTITY
go

---------------------------------------------------------------------------------------------------------------
-- Date Created	: Wednesday, October 19, 2011
-- Created By	: DNet Team by using CodeSmith v 2.6
-- Rev History	:
---------------------------------------------------------------------------------------------------------------

alter PROCEDURE up_InsertFreeServiceBB
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
	@LastUpdateBy varchar(20)	--@LastUpdateTime datetime	
AS
INSERT	INTO	[dbo].[FreeServiceBB]
VALUES
(
	@Status,
	@ChassisMasterID,
	@FSKindID,
	@MileAge,
	@ServiceDate,
	@ServiceDealerID,
	@DealerBranchID,
	@SoldDate,
	@NotificationNumber,
	@NotificationType,
	@TotalAmount,
	@LabourAmount,
	@PartAmount,
	@PPNAmount,
	@PPHAmount,
	@Reject,
	@Reason,
	@ReleaseBy,
	@ReleaseDate,
	@VisitType,
	@WorkOrderNumber,
	@RowStatus,
	@CreatedBy,
	GETDATE(),	
	@LastUpdateBy,
	GETDATE())

	
SET @ID = @@IDENTITY
go

---------------------------------------------------------------------------------------------------------------
-- Date Created	: 01 Oktober 2007
-- Created By	: DNet Team by using CodeSmith v 2.6
-- Rev History	:
---------------------------------------------------------------------------------------------------------------

alter PROCEDURE up_InsertIndentPartDetail
	@ID int OUTPUT,
	@IndentPartHeaderID int,
	@SparePartMasterID int,
	@Qty int,
	@Description varchar(255),
	@AllocationQty int,
	@IsCompletedAllocation tinyint,
	@Price money,
	@TotalForecast int,
	@RowStatus smallint,
	@CreatedBy varchar(20),
	--@CreatedTime datetime,
	@LastUpdateBy varchar(20)
	--@LastUpdateTime datetime
	
AS
INSERT	INTO	[dbo].[IndentPartDetail]
VALUES
(
	@IndentPartHeaderID,
	@SparePartMasterID,
	@Qty,
	@Description,
	@AllocationQty,
	@IsCompletedAllocation,
	@Price,
	@TotalForecast,
	@RowStatus,
	@CreatedBy,
	GETDATE(),	
	@LastUpdateBy,
	GETDATE()
)

	
SET @ID = @@IDENTITY
go

---------------------------------------------------------------------------------------------------------------
-- Date Created	: Friday, December 12, 2008
-- Created By	: DNet Team by using CodeSmith v 2.6
-- Rev History	:
-- Rev			: 06/03/2018
--				  add Purpose and DMSPRNo columns
---------------------------------------------------------------------------------------------------------------

alter PROCEDURE up_InsertIndentPartHeader
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

set @DealerID = ISNULL(@DealerID,0)
if(@DealerID=0)
begin
	set @ID=-1
end 
else
begin
	Declare @IPNoGenerated  varchar(18)
	set @IPNoGenerated=dbo.ufn_CreateIndentPartNumber(@RequestDate,@DealerId)

	INSERT	INTO	[dbo].[IndentPartHeader]
	VALUES
	(
		@DealerID,
		@IPNoGenerated,
		@RequestDate,
		@MaterialType,
		@TermOfPaymentID,
		@TOPBlockStatusID,
		@Status,
		@StatusKTB,
		@SubmitFile,
		@PaymentType,
		@Price,
		@KTBConfirmedDate,
		@DescID,
		@ChassisNumber,
		@DMSPRNo,
		@RowStatus,
		@CreatedBy,
		GETDATE(),	
		@LastUpdateBy,
		GETDATE())

		
	SET @ID = @@IDENTITY

end
go

---------------------------------------------------------------------------------------------------------------
-- Date Created	: 11 Nopember 2016
-- Created By	: DNet Team by using CodeSmith v 2.6
-- Rev History	:
---------------------------------------------------------------------------------------------------------------

alter PROCEDURE up_InsertMainArea
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
INSERT	INTO	[dbo].[MainArea]
VALUES
(
	@AreaCode,
	@Description,
	@PICSales,
	@PICServices,
	@PICSpareparts,
	@RowStatus,
	@CreatedBy,
	GETDATE(),	
	@LastUpdateBy,
	GETDATE())

	
SET @ID = @@IDENTITY
go

---------------------------------------------------------------------------------------------------------------
-- Date Created	: Wednesday, February 14, 2018
-- Created By	: DNet Team by using CodeSmith v 2.6
-- Rev History	:
---------------------------------------------------------------------------------------------------------------

alter PROCEDURE up_InsertOCRIdentity
      @ID INT OUTPUT ,
      @SPKCustomerID INT ,
      @Type SMALLINT ,
      @ImageID VARCHAR(50) ,
      @ImagePath VARCHAR(200) ,
      @IdentityNumber VARCHAR(100) ,
      @Name VARCHAR(100) ,
      @BirthOfDate VARCHAR(50) ,
      @BirthOfPlace VARCHAR(100) ,
      @Gender VARCHAR(20) ,
      @Height VARCHAR(10) ,
      @Address VARCHAR(200) ,
      @RtRw VARCHAR(50) ,
      @District VARCHAR(100) ,
      @Subdistrict VARCHAR(100) ,
      @Regency VARCHAR(100) ,
      @Province VARCHAR(100) ,
      @Religion VARCHAR(100) ,
      @MaritalStatus VARCHAR(50) ,
      @Occupation VARCHAR(100) ,
      @Citizenship VARCHAR(100) ,
      @ValidUntil VARCHAR(100) ,
      @Polda VARCHAR(100) ,
      @TotalChars INT ,
      @ConfidenceChars INT ,
      @ProcessingTime FLOAT ,
      @Errors VARCHAR(200) ,
      @JSon VARCHAR(1000) ,
      @RowStatus SMALLINT ,
      @CreatedBy VARCHAR(20) ,
	--@CreatedTime datetime,
      @LastUpdateBy VARCHAR(20)	--@LastUpdateTime datetime	
AS
BEGIN
      UPDATE    dbo.[OCRIdentity]
      SET       [RowStatus] = -1 ,
                [LastUpdateTime] = GETDATE() ,
                [LastUpdateBy] = @CreatedBy
      WHERE     [OCRIdentity].SPKCustomerID = @SPKCustomerID AND [OCRIdentity].SPKCustomerID IS NOT NULL


      INSERT    INTO [dbo].[OCRIdentity]
      VALUES    ( @SPKCustomerID, @Type, @ImageID, @ImagePath, @IdentityNumber, @Name, @BirthOfDate, @BirthOfPlace,
                  @Gender, @Height, @Address, @RtRw, @District, @Subdistrict, @Regency, @Province, @Religion,
                  @MaritalStatus, @Occupation, @Citizenship, @ValidUntil, @Polda, @TotalChars, @ConfidenceChars,
                  @ProcessingTime, @Errors, @JSon, @RowStatus, @CreatedBy, GETDATE(), @LastUpdateBy, GETDATE() )

	
      SET @ID = @@IDENTITY

END
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

alter PROCEDURE up_InsertPartShop
	@ID int OUTPUT,
	@DealerID smallint,
	@CityPartID int,
	@CityId INT = NULL,
	@PartShopCode varchar(10),
	@OldPartShopCode varchar(10)=NULL,
	@Name varchar(50),
	@Address varchar(100),
	@Phone varchar(40),
	@Fax varchar(40),
	@Email varchar(50),
	@Status tinyint,
	@RowStatus smallint,
	@CreatedBy varchar(20),
	--@CreatedTime datetime,
	@LastUpdateBy varchar(20)	--@LastUpdateTime datetime	
AS

IF @CityPartID = NULL AND @CityID = NULL
BEGIN
	RETURN
END

IF @CityPartID > 0 and @CityID is NULL  
BEGIN
	 set @CityID = (select CityID from CityPart where ID = @CityPartID)  
END

INSERT INTO [dbo].[PartShop]
           ([DealerID]
           ,[CityPartID]
           ,[CityID]
           ,[PartShopCode]
           ,[OldPartShopCode]
           ,[Name]
           ,[Address]
           ,[Phone]
           ,[Fax]
           ,[Email]
           ,[Status]
           ,[RowStatus]
           ,[CreatedBy]
           ,[CreatedTime]
           ,[LastUpdateBy]
           ,[LastUpdateTime])
VALUES
(
	@DealerID,
	@CityPartID,
	@CityId,
	@PartShopCode,
	@OldPartShopCode,
	@Name,
	@Address,
	@Phone,
	@Fax,
	@Email,
	@Status,
	@RowStatus,
	@CreatedBy,
	GETDATE(),	
	@LastUpdateBy,
	GETDATE())

	
SET @ID = @@IDENTITY
go

---------------------------------------------------------------------------------------------------------------  
-- Date Created : Wednesday, November 30, 2005  
-- Created By : DNet Team by using CodeSmith v 2.6  
-- Rev History :  
---------------------------------------------------------------------------------------------------------------  
  
alter PROCEDURE up_InsertPDI  
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
 --@CreatedTime datetime,  
 @LastUpdateBy varchar(20) --@LastUpdateTime datetime   
AS  
INSERT INTO [dbo].[PDI]  
VALUES  
(  
 @ChassisMasterID,  
 @DealerID,  
 @DealerBranchID,  
 @Kind,  
 @PDIStatus,  
 @PDIDate,  
 @ReleaseBy,  
 @ReleaseDate,  
 @WorkOrderNumber,  
 @RowStatus,  
 @CreatedBy,  
 GETDATE(),   
 @LastUpdateBy,  
 GETDATE())  
  
   
SET @ID = @@IDENTITY
go

---------------------------------------------------------------------------------------------------------------  
-- Date Created : Wednesday, August 01, 2007  
-- Created By : DNet Team by using CodeSmith v 2.6  
-- Rev History :  
---------------------------------------------------------------------------------------------------------------  
  
alter PROCEDURE up_InsertPMHeader  
 @ID int OUTPUT,  
 @DealerID smallint,  
 @DealerBranchID int,  
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
 @LastUpdateBy varchar(20) 
 --@LastUpdateTime datetime   
AS  
INSERT INTO [dbo].[PMHeader]  
VALUES  
(  
 @DealerID,  
 @DealerBranchID,  
 @ChassisNumberID,  
 @PMKindID,  
 @StandKM,  
 @ServiceDate,  
 @ReleaseDate,  
 @PMStatus,  
 @EntryType,  
 @WorkOrderNumber,  
 @BookingNo,  
 @VisitType,  
 @Remarks,  
 @RowStatus,  
 @CreatedBy,  
 GETDATE(),   
 @LastUpdateBy,  
 GETDATE())  
  
   
SET @ID = @@IDENTITY
go

---------------------------------------------------------------------------------------------------------------  
-- Date Created : Monday, July 23, 2007  
-- Created By : DNet Team by using CodeSmith v 2.6  
-- Rev History :  
---------------------------------------------------------------------------------------------------------------  
  
alter PROCEDURE up_InsertPQRHeader  
 @ID int OUTPUT,  
 @PQRNo varchar(25),  
 @PQRType int,
 @RefPQRNo varchar(25),  
 @DealerID int,  
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
 @ReleaseBy varchar(50),  
 @FinishBy varchar(50),  
 @FinishDate datetime,  
 @CodeA varchar(4),  
 @CodeB varchar(4),  
 @CodeC varchar(4),  
 @WorkOrderNumber varchar(50)='',
 @RowStatus smallint,  
 @CreatedBy varchar(20),  
 --@CreatedTime datetime,  
 @LastUpdateBy varchar(20)  
 --@LastUpdateTime datetime  
   
AS  
INSERT INTO [dbo].[PQRHeader]  
VALUES  
(    
 dbo.ufn_CreatePQRNumber(GETDATE(),right(@PQRNo,len(@PQRNo)-7), left(@PQRNo,6)),  
 @PQRType,
 @RefPQRNo,  
 @DealerID,  
 @DealerBranchID,
 @Year,  
 @SeqNo,  
 @CategoryID,  
 @DocumentDate,  
 @SoldDate,  
 @ChassisMasterID,  
 @PQRDate,  
 @OdoMeter,  
 @Velocity,  
 @CustomerName,  
 @CustomerAddress,  
 @ValidationTime,  
 @ConfirmBy,  
 @ConfirmTime,  
 @RealeseTime,  
 @IntervalProcess,  
 @Complexity,  
 @Subject,  
 @Symptomps,  
 @Causes,  
 ltrim(rtrim(@Results)),  
 @Notes,  
 @Solutions,  
 @Bobot,  
 @ReleaseBy,  
 @FinishBy,  
 @FinishDate,  
 @CodeA,  
 @CodeB,  
 @CodeC,  
 @WorkOrderNumber,
 @RowStatus,  
 @CreatedBy,  
 GETDATE(),   
 @LastUpdateBy,  
 GETDATE()  
)  
  
   
SET @ID = @@IDENTITY
go

---------------------------------------------------------------------------------------------------------------    
-- Date Created : Monday, January 16, 2012    
-- Created By : DNet Team by using CodeSmith v 2.6    
-- Rev History :    
---------------------------------------------------------------------------------------------------------------    
    
alter PROCEDURE up_InsertPQRHeaderBB    
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
 @WorkOrderNumber varchar(50)=null,  
 @RowStatus smallint,    
 @CreatedBy varchar(20),    
 --@CreatedTime datetime,    
 @LastUpdateBy varchar(20) --@LastUpdateTime datetime     
AS    
INSERT INTO [dbo].[PQRHeaderBB]    
VALUES    
(    
 dbo.ufn_CreatePQRNumberBB(GETDATE(),right(@PQRNo,len(@PQRNo)-7), left(@PQRNo,6)),--@PQRNo,   
 @PQRType, 
 @RefPQRNo,    
 @DealerID,  
 @DealerBranchID,  
 @Year,    
 @SeqNo,    
 @CategoryID,    
 @DocumentDate,    
 @SoldDate,    
 @ChassisMasterBBID,    
 @PQRDate,    
 @OdoMeter,    
 @Velocity,    
 @CustomerName,    
 @CustomerAddress,    
 @ValidationTime,    
 @ConfirmBy,    
 @ConfirmTime,    
 @RealeseTime,    
 @IntervalProcess,    
 @Complexity,    
 @Subject,    
 @Symptomps,    
 @Causes,    
 @Results,    
 @Notes,    
 @Solutions,    
 @Bobot,    
 @ReleaseBy,    
 @FinishBy,    
 @FinishDate,    
 @CodeA,    
 @CodeB,    
 @CodeC,    
 @WorkOrderNumber,  
 @RowStatus,    
 @CreatedBy,    
 GETDATE(),     
 @LastUpdateBy,    
 GETDATE())    
    
     
SET @ID = @@IDENTITY
go

---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
---------------------------------------------------------------------------------------------------------------
-- Date Created	: 19 April 2016
-- Created By	: DNet Team by using CodeSmith v 2.6
-- Rev History	:
---------------------------------------------------------------------------------------------------------------

alter PROCEDURE up_InsertRecallService
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
	  BEGIN

			IF NOT EXISTS ( SELECT TOP 1
									'*'
							FROM	[dbo].[RecallService] a ( NOLOCK )
							WHERE	[a].[ChassisMasterID] = @ChassisMasterID
									AND [a].[RecallChassisMasterID] = @RecallChassisMasterID 
									AND a.[RowStatus]=0
									)
			   INSERT	INTO [dbo].[RecallService]
						( ChassisMasterID ,
						  MileAge ,
						  ServiceDate ,
						  ServiceDealerID ,
                          DealerBranchID ,			
						  RecallChassisMasterID ,
                          WorkOrderNumber ,
						  RowStatus ,
						  CreatedBy ,
						  CreatedTime ,
						  LastUpdateBy ,
						  LastUpdateTime
						)
			   VALUES	( @ChassisMasterID ,
						  @MileAge ,
						  @ServiceDate ,
						  @ServiceDealerID ,
                          @DealerBranchID ,
						  @RecallChassisMasterID ,
                          @WorkOrderNumber ,
						  @RowStatus ,
						  @CreatedBy ,
						  GETDATE() ,
						  @LastUpdateBy ,
						  GETDATE()
						)


			SET @ID = @@IDENTITY


	  END
go

---------------------------------------------------------------------------------------------------------------
-- Date Created	: 09 Agustus 2018
-- Created By	: Mitrais Team by using CodeSmith v 2.6
-- Rev History	:
---------------------------------------------------------------------------------------------------------------
alter PROCEDURE up_InsertRevisionFaktur
	@ID int OUTPUT,
	@ChassisMasterID int,
	@EndCustomerID int,
	@OldEndCustomerID int,
	@RegNumber varchar(15),
	@RevisionStatus smallint,
	@RevisionTypeID smallint,
	@IsPay smallint,
	@NewValidationDate datetime,
	@NewValidationBy varchar(20),
	@NewConfirmationDate datetime,
	@NewConfirmationBy varchar(20),
	@Remark varchar(200),
	@RowStatus smallint,
	@CreatedBy varchar(20),
	--@CreatedTime datetime,
	@LastUpdateBy varchar(20)	--@LastUpdateTime datetime	
AS
INSERT	INTO	[dbo].[RevisionFaktur]
VALUES
(
	@ChassisMasterID,
	@EndCustomerID,
	@OldEndCustomerID,
	@RegNumber,
	@RevisionStatus,
	@RevisionTypeID,
	@IsPay,
	@NewValidationDate,
	@NewValidationBy,
	@NewConfirmationDate,
	@NewConfirmationBy,
	@Remark,
	@RowStatus,
	@CreatedBy,
	GETDATE(),	
	@LastUpdateBy,
	GETDATE())
	
SET @ID = @@IDENTITY

--CREATE Autonumber @RegNumber/Nomor Pengajuan
DECLARE @SeqNum int

SELECT @SeqNum = MAX(RIGHT(RegNumber,6))
FROM RevisionFaktur 
WHERE '20'+SUBSTRING(RegNumber,3,2) = Year(getdate())

SET @SeqNum = ISNULL(@SeqNum,0)+1
SELECT @RegNumber='RF'+RIGHT(CONVERT(CHAR(4),Year(getdate())),2)+REPLICATE('0',6-LEN(@SeqNum))+CONVERT(VARCHAR(6),@SeqNum)

UPDATE RevisionFaktur SET RegNumber=@RegNumber
WHERE ID=@ID
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
-- Date Created	: Thursday, July 13, 2017
-- Created By	: DNet Team by using CodeSmith v 2.6
-- Rev History	:
-- Revision: Friday, 2 March 2018 by Mitrais Team
--			change BenefitMasterHeaderID to CampaignName
--			change IndustrialSectorID to BusinessSectordetailID
---------------------------------------------------------------------------------------------------------------

alter PROCEDURE up_InsertSAPCustomer
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
INSERT	INTO	[dbo].[SAPCustomer]
VALUES
(
	@SalesforceID,
	@DealerID,
	@SalesmanHeaderID,
	@VechileTypeID,
	@CustomerCode,
	@CustomerName,
	@CustomerType,
	@CustomerAddress,
	@Phone,
	@Email,
	@Sex,
	@AgeSegment,
	@CustomerPurpose,
	@InformationType,
	@InformationSource,
	@Status,
	@Qty,
	@ProspectDate,
	@isSPK,
	@CurrVehicleBrand,
	@CurrVehicleType,
	@Note,
	@WebID,
    @BirthDate,
	@PreferedVehicleModel,
	@Description,
	@EstimatedCloseDate,
	@OriginatingLeadId,
	@StatusCode,
	@LeadStatus,
	@StateCode,
	@CampaignName,
	@BusinessSectorDetailID,
	@RowStatus,
	@CreatedBy,
	GETDATE(),	
	@LastUpdateBy,
	GETDATE()
)

	
SET @ID = @@IDENTITY
go

---------------------------------------------------------------------------------------------------------------
-- Date Created	: Tuesday, July 24, 2012
-- Created By	: DNet Team by using CodeSmith v 2.6
-- Rev History	:
---------------------------------------------------------------------------------------------------------------

alter PROCEDURE up_InsertSparePartMaster
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
    INSERT  INTO [dbo].[SparePartMaster]
            ( [ProductCategoryID] ,
              [PartNumber] ,
              [PartName] ,
              [PartNumberReff] ,
              [UoM] ,
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
            )
    VALUES  ( @ProductCategoryID ,
              @PartNumber ,
              @PartName ,
              @PartNumberReff ,
              @UoM ,
              @MaterialCategoryCode ,
              @AltPartNumber ,
              @AltPartName ,
              @PartCode ,
              @ModelCode ,
              @SupplierCode ,
              @TypeCode ,
              @Stock ,
              @RetalPrice ,
              @PartStatus ,
              @ActiveStatus ,
              @AccessoriesType ,
			  @ProductType ,
              @RowStatus ,
              @CreatedBy ,
              GETDATE() ,
              @LastUpdateBy ,
              GETDATE()
            )

	
    SET @ID = @@IDENTITY
go

---------------------------------------------------------------------------------------------------------------
-- Date Created	: Friday, December 28, 2007
-- Created By	: DNet Team by using CodeSmith v 2.6
-- Rev History	:
-- Date Updated : 06 Maret 2018
-- Updated By : Mitrais Team
-- Rev History : Add DMS PRNo
---------------------------------------------------------------------------------------------------------------

alter PROCEDURE up_InsertSparePartPO
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
	@SentPODate DATETIME,
	@IsTransfer BIT	,
    @DMSPRNo varchar(50),
	@RowStatus smallint,
	@CreatedBy varchar(20),
	--@CreatedTime datetime,
	@LastUpdateBy varchar(20)
	--@LastUpdateTime datetime
	
AS
INSERT	INTO	[dbo].[SparePartPO]
(  PONumber, OrderType, DealerID, PODate, TermOfPaymentID, TOPBlockStatusID, DeliveryDate, ProcessCode, CancelRequestBy, IndentTransfer,
PickingTicket, SentPODate, IsTransfer, DMSPRNo, RowStatus, CreatedBy, CreatedTime, LastUpdateBy, LastUpdateTime
)
VALUES
(
	dbo.ufn_CreateSparePartPONumber (@OrderType,@DealerID,@PODate,@PONumber),
	@OrderType,
	@DealerID,
	@PODate,
	@TermOfPaymentID,
	@TOPBlockStatusID,
	@DeliveryDate,
	@ProcessCode,
	@CancelRequestBy,
	@IndentTransfer,
	@PickingTicket,
	@SentPODate,
	@IsTransfer,
    @DMSPRNo,
	@RowStatus,
	@CreatedBy,
	GETDATE(),	
	@LastUpdateBy,
	GETDATE()
)

	
SET @ID = @@IDENTITY
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

alter PROCEDURE up_InsertSAPCustomer
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
INSERT	INTO	[dbo].[SAPCustomer]
VALUES
(
	@SalesforceID,
	@DealerID,
	@SalesmanHeaderID,
	@VechileTypeID,
	@CustomerCode,
	@CustomerName,
	@CustomerType,
	@CustomerAddress,
	@Phone,
	@Email,
	@Sex,
	@AgeSegment,
	@CustomerPurpose,
	@InformationType,
	@InformationSource,
	@Status,
	@Qty,
	@ProspectDate,
	@isSPK,
	@CurrVehicleBrand,
	@CurrVehicleType,
	@Note,
	@WebID,
    @BirthDate,
	@PreferedVehicleModel,
	@Description,
	@EstimatedCloseDate,
	@OriginatingLeadId,
	@StatusCode,
	@LeadStatus,
	@StateCode,
	@CampaignName,
	@BusinessSectorDetailID,
	@RowStatus,
	@CreatedBy,
	GETDATE(),	
	@LastUpdateBy,
	GETDATE()
)

	
SET @ID = @@IDENTITY
go

---------------------------------------------------------------------------------------------------------------
-- Date Created	: Tuesday, July 24, 2012
-- Created By	: DNet Team by using CodeSmith v 2.6
-- Rev History	:
---------------------------------------------------------------------------------------------------------------

alter PROCEDURE up_InsertSparePartMaster
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
    INSERT  INTO [dbo].[SparePartMaster]
            ( [ProductCategoryID] ,
              [PartNumber] ,
              [PartName] ,
              [PartNumberReff] ,
              [UoM] ,
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
            )
    VALUES  ( @ProductCategoryID ,
              @PartNumber ,
              @PartName ,
              @PartNumberReff ,
              @UoM ,
              @MaterialCategoryCode ,
              @AltPartNumber ,
              @AltPartName ,
              @PartCode ,
              @ModelCode ,
              @SupplierCode ,
              @TypeCode ,
              @Stock ,
              @RetalPrice ,
              @PartStatus ,
              @ActiveStatus ,
              @AccessoriesType ,
			  @ProductType ,
              @RowStatus ,
              @CreatedBy ,
              GETDATE() ,
              @LastUpdateBy ,
              GETDATE()
            )

	
    SET @ID = @@IDENTITY
go

---------------------------------------------------------------------------------------------------------------
-- Date Created	: Friday, December 28, 2007
-- Created By	: DNet Team by using CodeSmith v 2.6
-- Rev History	:
-- Date Updated : 06 Maret 2018
-- Updated By : Mitrais Team
-- Rev History : Add DMS PRNo
---------------------------------------------------------------------------------------------------------------

alter PROCEDURE up_InsertSparePartPO
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
	@SentPODate DATETIME,
	@IsTransfer BIT	,
    @DMSPRNo varchar(50),
	@RowStatus smallint,
	@CreatedBy varchar(20),
	--@CreatedTime datetime,
	@LastUpdateBy varchar(20)
	--@LastUpdateTime datetime
	
AS
INSERT	INTO	[dbo].[SparePartPO]
(  PONumber, OrderType, DealerID, PODate, TermOfPaymentID, TOPBlockStatusID, DeliveryDate, ProcessCode, CancelRequestBy, IndentTransfer,
PickingTicket, SentPODate, IsTransfer, DMSPRNo, RowStatus, CreatedBy, CreatedTime, LastUpdateBy, LastUpdateTime
)
VALUES
(
	dbo.ufn_CreateSparePartPONumber (@OrderType,@DealerID,@PODate,@PONumber),
	@OrderType,
	@DealerID,
	@PODate,
	@TermOfPaymentID,
	@TOPBlockStatusID,
	@DeliveryDate,
	@ProcessCode,
	@CancelRequestBy,
	@IndentTransfer,
	@PickingTicket,
	@SentPODate,
	@IsTransfer,
    @DMSPRNo,
	@RowStatus,
	@CreatedBy,
	GETDATE(),	
	@LastUpdateBy,
	GETDATE()
)

	
SET @ID = @@IDENTITY
go

---------------------------------------------------------------------------------------------------------------  
-- Date Created : Thursday, July 15, 2010  
-- Created By : DNet Team by using CodeSmith v 2.6  
-- Rev History :  
-- Date Updated : 06 Maret 2018  
-- Updated By : Mitrais Team  
-- Rev History : Add TotalForecast  
---------------------------------------------------------------------------------------------------------------  
  
alter PROCEDURE up_InsertSparePartPODetail  
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
 --@CreatedTime datetime,  
 @LastUpdateBy varchar(20) 
 --@LastUpdateTime datetime    
AS  
INSERT INTO [dbo].[SparePartPODetail]  
VALUES  
(  
 @SparePartPOID,  
 @SparePartMasterID,  
 @CheckListStatus,  
 @Quantity,  
 @RetailPrice,  
 @EstimateStatus,  
 @StopMark,
 @TotalForecast,  
 @RowStatus,  
 @CreatedBy,  
 GETDATE(),   
 @LastUpdateBy,  
 GETDATE() 
)  
  
   
SET @ID = @@IDENTITY
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
-- Date Created	: Thursday, June 23, 2011
-- Created By	: DNet Team by using CodeSmith v 2.6
-- Rev History	:
---------------------------------------------------------------------------------------------------------------

alter PROCEDURE up_InsertSPKCustomer
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
INSERT	INTO	[dbo].[SPKCustomer]
		(
		  [Code]
		, [ReffCode]
		, [TipeCustomer]
		, [TipePerusahaan]
		, [Name1]
		, [Name2]
		, [Name3]
		, [Alamat]
		, [Kelurahan]
		, [Kecamatan]
		, [PostalCode]
		, [PreArea]
		, [CityID]
		, [PrintRegion]
		, [PhoneNo]
		, [OfficeNo]
		, [HomeNo]
		, [HpNo]
		, [Email]
		, [Status]
		, [MCPStatus]
		, [lkppstatus]
		, [SAPCustomerID]
		, [LKPPReference]
		, [BusinessSectorDetailID]
		, [ImagePath]
		, [RowStatus]
		, [CreatedTime]
		, [CreatedBy]
		, [LastUpdateTime]
		, [LastUpdateBy]
		)
 
VALUES
(
	@Code,
	@ReffCode,
	@TipeCustomer,
	@TipePerusahaan,
	@Name1,
	@Name2,
	@Name3,
	@Alamat,
	@Kelurahan,
	@Kecamatan,
	@PostalCode,
	@PreArea,
	@CityID,
	@PrintRegion,
	@PhoneNo,
	@OfficeNo,
	@HomeNo,
	@HpNo,
	@Email,
	@Status,
	@MCPStatus,
	@LKPPStatus,
	@SAPCustomerID ,
	@LKPPReference,
	@BusinessSectorDetailID,
	@ImagePath,
	@RowStatus,
	GETDATE(),	
	@CreatedBy,
	GETDATE(),	
	@LastUpdateBy)

	
SET @ID = @@IDENTITY
go

---------------------------------------------------------------------------------------------------------------
-- Date Created	: Monday, March 07, 2011
-- Created By	: DNet Team by using CodeSmith v 2.6
-- Rev History	:
---------------------------------------------------------------------------------------------------------------

alter PROCEDURE up_InsertSPKHeader
      @ID INT OUTPUT ,
      @DealerID SMALLINT ,
	  @DealerBranchID INT ,
      @Status VARCHAR(2) ,
      @SPKNumber VARCHAR(15) ,
	  @SPKReferenceNumber VARCHAR(15) = null,
      @IndentNumber VARCHAR(10) ,
      @DealerSPKNumber VARCHAR(15) ,
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

      DECLARE @Tbl AS TABLE ( SPKNumber VARCHAR(15) )
      INSERT    INTO [dbo].[SPKHeader]
                (
                  DealerID ,
                  [Status] ,
                  SPKNumber ,
				  SPKReferenceNumber,
                  DealerSPKNumber ,
                  IndentNumber ,
                  PlanDeliveryMonth ,
                  PlanDeliveryYear ,
                  PlanDeliveryDate ,
                  PlanInvoiceMonth ,
                  PlanInvoiceYear ,
                  PlanInvoiceDate ,
                  CustomerRequestID ,
                  SPKCustomerID ,
                  ValidateTime ,
                  ValidateBy ,
                  RejectedReason ,
                  SalesmanHeaderID ,
                  EvidenceFile ,
                  ValidationKey ,
                  FlagUpdate ,
                  DealerBranchID ,
                  IsSend ,
                  DealerSPKDate ,
                  BenefitMasterHeaderID ,
                  RowStatus ,
                  CreatedTime ,
                  CreatedBy ,
                  LastUpdateTime ,
                  LastUpdateBy
                )
      OUTPUT    [Inserted].[SPKNumber]
                INTO  @Tbl ( SPKNumber )
      VALUES    (
                  @DealerID ,
                  @Status ,
                  dbo.ufn_CreateSPKNumber(GETDATE()) ,--@SPKNumber,
				  @SPKReferenceNumber,
                  @DealerSPKNumber ,
                  NULL ,--dbo.ufn_CreateSPKIndentNumber (GETDATE()),--@IndentNumber,
                  @PlanDeliveryMonth ,
                  @PlanDeliveryYear ,
                  @PlanDeliveryDate ,
                  @PlanInvoiceMonth ,
                  @PlanInvoiceYear ,
                  @PlanInvoiceDate ,
                  @CustomerRequestID ,
                  @SPKCustomerID ,
                  @ValidateTime ,
                  @ValidateBy ,
                  @RejectedReason ,
                  @SalesmanHeaderID ,
                  @EvidenceFile ,
                  @ValidationKey ,
                  @FlagUpdate ,
                  @DealerBranchID ,
                  @IsSend ,
                  @DealerSPKDate ,
                  @BenefitMasterHeaderID ,
                  @RowStatus ,
                  GETDATE() ,
                  @CreatedBy ,
                  GETDATE() ,
                  @LastUpdateBy
	            )

	
      SET @ID = @@IDENTITY


      SELECT    @SPKNumber = SPKNumber
      FROM      @Tbl

      IF NOT EXISTS ( SELECT TOP 1
                                ''
                      FROM      [dbo].[StatusChangeHistory] a
                 WHERE     a.[DocumentType] = 6
                                AND [a].[DocumentRegNumber] = @SPKNumber
                                AND [a].[RowStatus] = 0 )
         BEGIN

               INSERT   INTO [dbo].[StatusChangeHistory]
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
               SELECT   6 , -- DocumentType - tinyint
                        @SPKNumber , -- DocumentRegNumber - varchar(15)
                        0 , -- OldStatus - smallint
                        @Status , -- NewStatus - smallint
                        0 , -- RowStatus - smallint
                        LEFT(@CreatedBy, 20) , -- CreatedBy - varchar(20)
                        GETDATE() , -- CreatedTime - datetime
                        '' , -- LastUpdateBy - varchar(20)
                        GETDATE()  -- LastUpdateTime - datetime
         END
END
go

---------------------------------------------------------------------------------------------------------------
-- Date Created	: 09 Februari 2018
-- Created By	: Mitrais Team by using CodeSmith v 2.6
-- Rev History	:
---------------------------------------------------------------------------------------------------------------

alter PROCEDURE up_InsertStandardCode
	@ID int OUTPUT,
	@Category varchar(100),
	@ValueId int,
	@ValueCode varchar(200),
	@ValueDesc varchar(200),
	@Sequence int,
	@RowStatus smallint,
	@CreatedBy varchar(20),
	--@CreatedTime datetime,
	@LastUpdateBy varchar(20)	--@LastUpdateTime datetime	
AS
INSERT	INTO	[dbo].[StandardCode]
VALUES
(
	@Category,
	@ValueId,
	@ValueCode,
	@ValueDesc,
	@RowStatus,
	@CreatedBy,
	GETDATE(),	
	@LastUpdateBy,
	GETDATE(),
	@Sequence)

	
SET @ID = @@IDENTITY
go

 
go

---------------------------------------------------------------------------------------------------------------
-- Date Created	: Friday, November 18, 2005
-- Created By	: DNet Team by using CodeSmith v 2.6
-- Rev History	: 
-- Updated By   : Mitrais Team
---------------------------------------------------------------------------------------------------------------

alter PROCEDURE up_InsertTrTrainee
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
INSERT	INTO	[dbo].[TrTrainee]
(
    SalesmanHeaderID,
    Name,
    DealerID, 
	DealerBranchID,
	BirthDate, 
	Gender,
	NoKTP,
	Email,
	StartWorkingDate, 
	Status, 
	JobPosition, 
	EducationLevel, 
	Photo, 
	ShirtSize, RowStatus, CreatedBy, CreatedTime, LastUpdateBy, LastUpdateTime
)
VALUES
(
	@SalesmanHeaderId,
	@Name,
	@DealerID,
	@DealerBranchID,
	@BirthDate,
	@Gender,
	@NoKTP,
	@Email,
	@StartWorkingDate,
	@Status,
	@JobPosition,
	@EducationLevel,
	@Photo,
	@ShirtSize,
	@RowStatus,
	@CreatedBy,		
	GETDATE(),	
	@LastUpdateBy,		
	GETDATE())

	
SET @ID = @@IDENTITY
go

commit
go




set xact_abort on
go

begin transaction
go

set ANSI_NULLS off
go

--/****** Object:  Stored Procedure dbo.up_InsertVechileColor    Script Date: 14/10/2005 11:06:14 ******/
---------------------------------------------------------------------------------------------------------------
-- Date Created	: Tuesday, September 27, 2005
-- Created By	: DNet Team by using CodeSmith v 2.6
-- Rev History	:
---------------------------------------------------------------------------------------------------------------

alter PROCEDURE up_InsertVechileColor
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


INSERT	INTO	[dbo].[VechileColor]
VALUES
(
	@VechileTypeID,
	@ColorCode,
	@ColorIndName,
	@ColorEngName,
	@MaterialNumber,
	@MaterialDescription,
	@HeaderBOM,
	@MarketCode,
	@SpecialFlag,
	@Status,
	@RowStatus,
	@CreatedBy,
	GETDATE(),
	@LastUpdateBy,
	GETDATE()
)

SET @ID = @@IDENTITY
go

set ANSI_NULLS on
go

---------------------------------------------------------------------------------------------------------------
-- Date Created	: 17 Nopember 2005
-- Created By	: DNet Team by using CodeSmith v 2.6
-- Rev History	:
---------------------------------------------------------------------------------------------------------------

alter PROCEDURE up_InsertVechileModel
	@ID int OUTPUT,
	@VechileModelCode varchar(4),
	@CategoryID int,
	@Description varchar(40),
	@VechileModelIndCode varchar(30) = null,
	@IndDescription varchar(40) = null,
	@SalesFlag tinyint = 0,
	@RowStatus smallint,
	@CreatedBy varchar(20),
	--@CreatedTime datetime,
	@LastUpdateBy varchar(20)
	--@LastUpdateTime datetime
AS


INSERT	INTO	[dbo].[VechileModel]
VALUES
(
	@VechileModelCode,
	@CategoryID,
	@Description,
	@VechileModelIndCode,
	@IndDescription,
	@SalesFlag, 
	@RowStatus,
	@CreatedBy,
	GETDATE(),
	@LastUpdateBy,
	GETDATE()
)

SET @ID = @@IDENTITY
go

---------------------------------------------------------------------------------------------------------------
-- Date Created	: Tuesday, February 11, 2014
-- Created By	: DNet Team by using CodeSmith v 2.6
-- Rev History	:
---------------------------------------------------------------------------------------------------------------

alter PROCEDURE up_InsertVechileType
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
	@SAPModel nvarchar(20) = NULL,
	@MaxTOPDays int,
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

INSERT	INTO	[dbo].[VechileType]
VALUES
(
	@VechileTypeCode,
	@ModelID,
	@CategoryID,
	/*set productcategoryid by donas on 20141001153*/
	(select c.ProductCategoryID from Category c where c.ID=@CategoryID),--@ProductCategoryID,
	@Description,
	@Status,
	@VehicleClassID,
	@IsVehicleKind1,
	@IsVehicleKind2,
	@IsVehicleKind3,
	@IsVehicleKind4,
	@MaxTOPDays,
	@SAPModel,
	@SegmentType,
    @VariantType,
    @TransmitType,
    @DriveSystemType,
    @SpeedType,
    @FuelType,
	@RowStatus,
	@CreatedBy,
	GETDATE(),	
	@LastUpdateBy,
	GETDATE())

	
SET @ID = @@IDENTITY
go

---------------------------------------------------------------------------------------------------------------  
-- Date Created : Thursday, September 05, 2013  
-- Created By : DNet Team by using CodeSmith v 2.6  
-- Rev History :  
---------------------------------------------------------------------------------------------------------------  
  
alter PROCEDURE up_InsertWSCDetail  
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
 @LastUpdateBy varchar(20) 
 --@LastUpdateTime datetime   
AS  
INSERT INTO [dbo].[WSCDetail]  
VALUES  
(  
 @WSCHeaderID,  
 @WSCType,  
 @LaborMasterID,  
 @PositionCode,
 @WorkCode,
 @SparePartMasterID,  
 @Quantity,  
 @PartPrice,  
 @MainPart,  
 @QuantityReceived,  
 @ReceivedBy,  
 @ReceivedDate,  
 @Status,
 @RowStatus,  
 @CreatedBy,  
 GETDATE(),   
 @LastUpdateBy,  
 GETDATE())  
  
   
SET @ID = @@IDENTITY
go

---------------------------------------------------------------------------------------------------------------  
-- Date Created : Friday, November 18, 2011  
-- Created By : DNet Team by using CodeSmith v 2.6  
-- Rev History :  
---------------------------------------------------------------------------------------------------------------  
  
alter PROCEDURE up_InsertWSCDetailBB  
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
INSERT INTO [dbo].[WSCDetailBB]  
VALUES  
(  
 @WSCHeaderBBID,  
 @WSCType,  
 @LaborMasterID,  
 @PositionCode,
 @WorkCode,
 @SparePartMasterID,  
 @Quantity,  
 @PartPrice,  
 @MainPart,  
 @QuantityReceived,
 @ReceivedBy,
 @ReceivedDate,
 @Status,
 @RowStatus,  
 @CreatedBy,  
 GETDATE(),   
 @LastUpdateBy,  
 GETDATE())  
  
   
SET @ID = @@IDENTITY
go

---------------------------------------------------------------------------------------------------------------    
-- Date Created : Tuesday, November 29, 2005    
-- Created By : DNet Team by using CodeSmith v 2.6    
-- Rev History :    
---------------------------------------------------------------------------------------------------------------    
    
alter PROCEDURE up_InsertWSCHeader    
 @ID int OUTPUT,    
 @ClaimType varchar(2),    
 @DealerID int,    
 @DealerBranchID int,
 @ClaimNumber varchar(6),    
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
 @WorkOrderNumber varchar(50) = '',  
 @RowStatus smallint,    
 @CreatedBy varchar(50),    
 --@CreatedTime datetime,    
 @LastUpdateBy varchar(50)    
 --@LastUpdateTime datetime    
     
AS    
INSERT INTO [dbo].[WSCHeader]    
VALUES    
(    
 @ClaimType,    
 @DealerID,    
 @DealerBranchID,
 @ClaimNumber,    
 @RefClaimNumber,    
 @ChassisMasterID,  
 @FailureDate,  
 @ServiceDate,    
 @Miliage,    
 @PQR,    
 @PQRStatus,    
 @CodeA,    
 @CodeB,    
 @CodeC,    
 @Description,    
 @EvidencePhoto,    
 @EvidenceInvoice,    
 @EvidenceDmgPart, 
 @EvidenceRepair,
 @EvidenceWSCLetter,
 @EvidenceWSCTechnical,
 @Causes,
 @Results,
 @Notes,   
 @ReqDmgPart,    
 @ReqDmgPartBy,    
 @ReqDmgPartTime,    
 @NotificationNumber,    
 @DecideDate,    
 @Status,    
 @ClaimStatus,    
 @ReasonID,    
 @LaborAmount,    
 @PartAmount,    
 @PartReceiveBy,    
 @PartReceiveTime,    
 @DownLoadBy,    
 @DownLoadTime,    
 @ResponseTime,    
 @WorkOrderNumber,  
 @RowStatus,    
 @CreatedBy,      
 GETDATE(),     
 @LastUpdateBy,    
 GETDATE()    
)    
    
     
SET @ID = @@IDENTITY
go

---------------------------------------------------------------------------------------------------------------    
-- Date Created : Friday, November 18, 2011    
-- Created By : DNet Team by using CodeSmith v 2.6    
-- Rev History :    
---------------------------------------------------------------------------------------------------------------    
    
alter PROCEDURE up_InsertWSCHeaderBB    
 @ID int OUTPUT,    
 @ClaimType varchar(2),    
 @DealerID smallint,    
 @DealerBranchID int = null,
 @ClaimNumber varchar(13),    
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
 @LastUpdateBy varchar(50) 
 --@LastUpdateTime datetime     
AS    
INSERT INTO [dbo].[WSCHeaderBB]    
VALUES    
(    
 @ClaimType,    
 @DealerID,    
 @DealerBranchID,
 @ClaimNumber,    
 @RefClaimNumber,    
 @ChassisMasterBBID,
 @FailureDate,    
 @ServiceDate,    
 @Miliage,    
 @PQR,    
 @PQRStatus,    
 @CodeA,    
 @CodeB,    
 @CodeC,    
 @Description,    
 @EvidencePhoto,    
 @EvidenceInvoice,    
 @EvidenceDmgPart,  
 @EvidenceRepair,
 @EvidenceWSCLetter,
 @EvidenceWSCTechnical,
 @Causes,
 @Results,
 @Notes,  
 @ReqDmgPart,    
 @ReqDmgPartBy,    
 @ReqDmgPartTime,    
 @NotificationNumber,    
 @DecideDate,    
 @Status,    
 @ClaimStatus,    
 @ReasonID,    
 @LaborAmount,    
 @PartAmount,    
 @PartReceiveBy,    
 @PartReceiveTime,    
 @DownLoadBy,    
 @DownLoadTime,    
 @ResponseTime,   
 @WorkOrderNumber,   
 @RowStatus,    
 @CreatedBy,    
 GETDATE(),     
 @LastUpdateBy,    
 GETDATE())    
    
     
SET @ID = @@IDENTITY
go

commit
go


