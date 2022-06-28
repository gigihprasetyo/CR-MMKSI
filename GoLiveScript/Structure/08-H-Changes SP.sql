
/****** Object:  StoredProcedure [dbo].[sp_ProcessAssistSVCIncoming]    Script Date: 11/12/2018 10:11:26 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		SLA
-- Create date: 19-12-2017
-- Description:	
-- =============================================
ALTER PROCEDURE [dbo].[sp_ProcessAssistSVCIncoming]
@TVP dbo.AssistSVCIncomingType READONLY,
@AssistUploadLogID  INT,
@DealerCode VARCHAR(10)
AS
BEGIN


DECLARE @PeriodeMonth INT
DECLARE @PeriodeYear INT
DECLARE @DealerGroupIDHeader INT = NULL
SELECT @DealerGroupIDHeader = DealerGroupID FROM Dealer WHERE DealerCode = @DealerCode
SELECT @PeriodeMonth = [Month], @PeriodeYear = [Year] FROM AssistUploadLog WHERE ID = @AssistUploadLogID

SELECT *, CAST(NULL AS VARCHAR(MAX)) as RemarksSystem,
CAST(NULL AS VARCHAR(300)) as RemarksSpecial,
CAST(NULL AS INT) as DealerID,
CAST(NULL AS INT) as TrTraineMekanikID,
CAST(NULL AS INT) as ChassisMasterID,
CAST(NULL AS INT) as WorkOrderCategoryID,
CAST(NULL AS INT) as ServicePlaceID,
CAST(NULL AS INT) as ServiceTypeID,
CAST(NULL AS VARCHAR(100)) as Model,
CAST(NULL AS VARCHAR(30)) as Transmition,
CAST(NULL AS VARCHAR(20)) as DriveSystem,
CAST(1 AS INT) as StatusAktif
INTO #TempTable
FROM @TVP

UPDATE #TempTable SET NoWorkOrder = (dbo.fn_RemoveSpecialChars(ISNULL(NoWorkOrder,'')))

DECLARE @TempChassisModelBI TABLE (VechileTypeCode VARCHAR(100), MMCComModel3 VARCHAR(200),Transmit VARCHAR(100),DriveSystem VARCHAR(100))
INSERT INTO @TempChassisModelBI
SELECT
distinct(VechileTypeCode),
MMCComModel3,
Transmit,
DriveSystem
FROM AssistChassisMaster 
WHERE VechiletypeCode is not null

--Declare Variable result
DECLARE @TglBukaTransaksiNew DATE = NULL
DECLARE @WaktuMasukNew TIME = NULL
DECLARE @TglTutupTransaksiNew DATE = NULL
DECLARE @WaktuKeluarNew TIME = NULL
DECLARE @DealerID INT = NULL
DECLARE @TrTraineMekanikID INT = NULL
DECLARE @WorkOrderCategoryID INT = NULL
DECLARE @ServicePlaceID INT = NULL
DECLARE @ServiceTypeID INT  = NULL
DECLARE @KMServiceNew INT = NULL
DECLARE @TotalLCNew MONEY = NULL
DECLARE @ChassisMasterID INT = NULL
DECLARE @RemarksSystem VARCHAR(MAX) = ''
DECLARE @RemarksSpecial VARCHAR(300) = ''
DECLARE @StatusAktif INT = NULL
DECLARE @Model VARCHAR(100) = NULL
DECLARE @Transmition VARCHAR(30) = NULL
DECLARE @DriveSystem VARCHAR(20) = NULL
DECLARE @VechileTypeCode VARCHAR(20) = NULL
DECLARE @AssistLogValidateStatus INT = 1
DECLARE @DealerGroupID INT = NULL
 
--Get Closing
DECLARE @IsClosed BIT = 0
IF EXISTS(SELECT TOP 1 1 FROM [AssistCutOffPeriod] A (NOLOCK)
INNER JOIN Dealer B (NOLOCK) ON B.ID = A.DealerID
WHERE [Month] = @PeriodeMonth AND [Year] = @PeriodeYear AND B.DealerCode = @DealerCode
AND A.RowStatus = 0 AND A.Status = 1 AND B.RowStatus = 1 AND B.[Status] = 1)
BEGIN
	SET @IsClosed = 1
END

--Declare Variable for Cursor
DECLARE @TglBukaTransaksi NVARCHAR(200)
DECLARE @WaktuMasuk NVARCHAR(200)
DECLARE @TglTutupTransaksi NVARCHAR(200)
DECLARE @WaktuKeluar NVARCHAR(200)
DECLARE @KodeDealer NVARCHAR(200)
DECLARE @KodeMekanik NVARCHAR(200)
DECLARE @CategoryWorkOrder NVARCHAR(200)
DECLARE @TempatPengerjaan NVARCHAR(200)
DECLARE @Layanan NVARCHAR(200)
DECLARE @MetodePembayaran NVARCHAR(200)
DECLARE @KMService NVARCHAR(200)
DECLARE @TotalLC NVARCHAR(200)
DECLARE @NoWorkOrder NVARCHAR(200) 
DECLARE @NoChassis NVARCHAR(200)
DECLARE @CursorValidation CURSOR

SET @CursorValidation = CURSOR FOR
SELECT ISNULL(TglBukaTransaksi,''), ISNULL(TglTutupTransaksi,''), ISNULL(KodeDealer,''), ISNULL(KodeMekanik,''), ISNULL(CategoryWorkOrder,''), ISNULL(TempatPengerjaan,''), ISNULL(Layanan,''), 
ISNULL(MetodePembayaran,''), ISNULL(KMService,''), ISNULL(TotalLC,''), ISNULL(WaktuMasuk,''), ISNULL([WaktuKeluar],''), ISNULL(NoWorkOrder,''), ISNULL(NoChassis,'')
FROM #TempTable (NOLOCK)
OPEN @CursorValidation
FETCH NEXT
FROM @CursorValidation INTO @TglBukaTransaksi, @TglTutupTransaksi, @KodeDealer, @KodeMekanik, @CategoryWorkOrder,
@TempatPengerjaan, @Layanan, @MetodePembayaran, @KMService, @TotalLC, @WaktuMasuk, @WaktuKeluar, @NoWorkOrder, @NoChassis
WHILE @@FETCH_STATUS = 0
BEGIN
	--Declare Variable
	SET @TglBukaTransaksiNew  = NULL
	SET @WaktuMasukNew = NULL
	SET @TglTutupTransaksiNew = NULL
	SET @WaktuKeluarNew = NULL
	SET @DealerID = NULL
	SET @TrTraineMekanikID = NULL
	SET @WorkOrderCategoryID = NULL
	SET @ServicePlaceID = NULL
	SET @ServiceTypeID = NULL
	SET @KMServiceNew = NULL
	SET @TotalLCNew = NULL
	SET @ChassisMasterID = NULL
	SET @RemarksSystem = ''
	SET @StatusAktif = 0
	SET @Model  = NULL
	SET @Transmition = NULL
	SET @DriveSystem = NULL
	SET @VechileTypeCode = NULL
	SET @RemarksSpecial = ''
	SET @DealerGroupID = NULL

	--Transaction Open Date Validation
	BEGIN TRY
			BEGIN TRY
				SET @TglBukaTransaksiNew = convert(datetime, @TglBukaTransaksi, 3)
			END TRY
			BEGIN CATCH
				SET @TglBukaTransaksiNew = convert(datetime, @TglBukaTransaksi)
			END CATCH
		IF CONVERT(DATE, @TglBukaTransaksiNew) = '1900-01-01'
		BEGIN
			SET @TglBukaTransaksiNew = NULL
			SET @RemarksSystem += ' | Invalid format tanggal buka transaksi ' + @TglBukaTransaksi
		END
	END TRY
	BEGIN CATCH
		SET @RemarksSystem += ' | Invalid format tanggal buka transaksi ' + @TglBukaTransaksi
	END CATCH

	--Time In Validation
	IF (ISNULL(@WaktuMasuk,'') != '')
	BEGIN
			BEGIN TRY
				SET @WaktuMasukNew = convert(time, (REPLACE(REPLACE(REPLACE(@WaktuMasuk,':00.0000000',''),'.',':'),',',':')))
			END TRY
			BEGIN CATCH
				BEGIN TRY
					SET @WaktuMasukNew = convert(time, convert(datetime, @WaktuMasuk, 3))
				END TRY
				BEGIN CATCH
					BEGIN TRY
						SET @WaktuMasukNew = convert(time, convert(datetime, @WaktuMasuk, 103))
					END TRY
					BEGIN CATCH
						SET @RemarksSystem +=  ' | Invalid format waktu masuk ' + @WaktuMasuk
					END CATCH
				END CATCH
			END CATCH
	END
	ELSE
	BEGIN
		SET @RemarksSystem += ' | Invalid format waktu masuk'
	END

	--Transaction Close Date Validation
	BEGIN TRY
			BEGIN TRY
				SET @TglTutupTransaksiNew = convert(datetime, @TglTutupTransaksi, 3)
			END TRY
			BEGIN CATCH
				SET @TglTutupTransaksiNew = convert(datetime, @TglTutupTransaksi)
			END CATCH
		IF CONVERT(DATE, @TglTutupTransaksiNew) = '1900-01-01'
		BEGIN
			SET @TglTutupTransaksiNew = NULL
			SET @RemarksSystem += ' | Invalid format tanggal tutup transaksi ' + @TglTutupTransaksi
		END

		--Period Validation
		ELSE
		BEGIN
			IF (Month(@TglTutupTransaksiNew) = @PeriodeMonth AND YEAR(@TglTutupTransaksiNew) = @PeriodeYear)
			BEGIN
				IF @IsClosed = 1
				BEGIN
					SET @RemarksSystem += ' | Periode upload sudah ditutup.'
				END
			END
			ELSE
			BEGIN
				SET @RemarksSystem += ' | Tanggal tutup transaksi ' + @TglTutupTransaksi + ' diluar periode '+  CONVERT(VARCHAR(10),@PeriodeMonth) + '-' + CONVERT(VARCHAR(10),@PeriodeYear)
			END
		END
	END TRY
	BEGIN CATCH
		SET @RemarksSystem += ' | Invalid format tanggal tutup transaksi ' + @TglTutupTransaksi
	END CATCH

	--Time Out Validation
	IF (ISNULL(@WaktuKeluar,'') != '')
	BEGIN
			BEGIN TRY
				SET @WaktuKeluarNew = convert(time, (REPLACE(REPLACE(REPLACE(@WaktuKeluar,':00.0000000',''),'.',':'),',',':')))
			END TRY
			BEGIN CATCH
				BEGIN TRY
					SET @WaktuKeluarNew = convert(time, convert(datetime, @WaktuKeluar, 3))
				END TRY
				BEGIN CATCH
					BEGIN TRY
						SET @WaktuKeluarNew = convert(time, convert(datetime, @WaktuKeluar, 103))
					END TRY
					BEGIN CATCH
						SET @RemarksSystem +=  ' | Invalid format waktu keluar ' + @WaktuKeluar
					END CATCH
				END CATCH
			END CATCH
	END
	ELSE
	BEGIN
		SET @RemarksSystem += ' | Invalid format waktu keluar'
	END

	--Dealer Validation
	SELECT TOP 1 @DealerID = ID, @DealerGroupID = DealerGroupID FROM Dealer (NOLOCK) WHERE RowStatus = 0 AND [Status] = 1 AND  DealerCode = @KodeDealer
	IF @DealerID IS NULL
	BEGIN 
		SET @RemarksSystem += ' | Invalid kode dealer ' + @KodeDealer
	END
	ELSE
	BEGIN
		IF (@DealerGroupID != @DealerGroupIDHeader)
		BEGIN
			SET @DealerID = NULL
			SET @RemarksSystem += ' | Kode dealer ' + @KodeDealer + ' bukan merupakan group dari ' + @DealerCode
		END
	END
	
	--Mekanik Validation
	SELECT TOP 1 @TrTraineMekanikID = ID FROM V_DataSiswa (NOLOCK) WHERE Status = 'Aktif' AND CONVERT(VARCHAR(50),ID) = @KodeMekanik AND DealerCode = @KodeDealer-- AND JobPosition = 'MKN'
	IF @TrTraineMekanikID IS NULL
	BEGIN 
		SET @RemarksSystem += ' | Invalid kode mekanik ' + @KodeMekanik
	END

	--No Chassis Validation, tidak boleh kosong tapi jika tidak ada dimaster data tidak apa apa
	IF (ISNULL(@NoChassis,'') = '')
	BEGIN
		SET @RemarksSystem += ' | Invalid no chassis '
	END
	ELSE IF (ISNULL(@NoChassis,'') = 'OTHERS')
	BEGIN
		SET @RemarksSystem = @RemarksSystem --it's true
	END
	ELSE
	BEGIN
		SELECT TOP 1 @ChassisMasterID = ID FROM CHassisMaster (NOLOCK) WHERE RowStatus = 0 AND  ChassisNumber  = @NoChassis
		IF @ChassisMasterID IS NOT NULL
		BEGIN
			SELECT TOP 1
                       @VechileTypeCode = C.VechileTypeCode,
					   @Model = D.IndDescription,
                       @Transmition = C.TransmitType,
                       @DriveSystem = C.DriveSystemType
                FROM ChassisMaster A
                    INNER JOIN VechileColor B ON A.VechileColorID = B.ID
                    INNER JOIN VechileType C ON C.ID = B.VechileTypeID
					INNER JOIN VechileModel D ON D.ID = C.ModelID
                WHERE A.ID = @ChassisMasterID;

                IF @Model IS NOT NULL
                BEGIN
                    SELECT TOP 1
                           @Model = MMCComModel3,
                           @Transmition = Transmit,
                           @DriveSystem = DriveSystem
                    FROM @TempChassisModelBI
                    WHERE VechileTypeCode = @VechileTypeCode;
                END;
		END
		ELSE
		BEGIN
			SET @RemarksSpecial = 'No Chassis tidak ditemukan ' + @NoChassis
		END
	END


	--Work Order Category Validation
	SELECT TOP 1 @WorkOrderCategoryID = ID FROM AssistWorkOrderCategory (NOLOCK) WHERE RowStatus = 0 AND [Status] = 1 AND WorkOrderCategory = @CategoryWorkOrder
	IF @WorkOrderCategoryID IS NULL
	BEGIN 
		SET @RemarksSystem += ' | Invalid data work order category ' + @CategoryWorkOrder
	END

	--Service Place Validation
	SELECT TOP 1 @ServicePlaceID = ID FROM AssistServicePlace (NOLOCK) WHERE RowStatus = 0 AND [Status] = 1 AND ServicePlaceCode = @TempatPengerjaan
	IF @ServicePlaceID IS NULL
	BEGIN 
		SET @RemarksSystem += ' | Invalid data tempat pengerjaan ' + @TempatPengerjaan
	END
		
	--Service Type Validation
	IF ISNULL(@Layanan,'') != ''
	BEGIN
		SELECT TOP 1 @ServiceTypeID = ID FROM AssistServiceType (NOLOCK) WHERE RowStatus = 0 AND [Status] = 1 AND ServiceTypeCode = @Layanan
		IF @ServiceTypeID IS NULL
		BEGIN 
			SET @RemarksSystem += ' | Invalid data layanan ' + @Layanan
		END
	END
	ELSE
	BEGIN
		SELECT TOP 1 @ServiceTypeID = ID from AssistServiceType (NOLOCK) WHERE ServiceTypeCode = 'NONE'
	END

	--Metode Pembayaran Validation
	IF @MetodePembayaran NOT IN ('Kredit', 'Tunai')
	BEGIN
		SET @RemarksSystem += ' | Invalid data metode pembayaran ' + @MetodePembayaran
	END

	--No Work Order Validation
	IF ISNULL(@NoWorkOrder,'') = ''
	BEGIN
		SET @RemarksSystem += ' | Invalid data no work order'
	END 

	--KM Service Validation
	IF @KMService LIKE '%,%'
	BEGIN
		SET @RemarksSystem += ' | KM Service tidak boleh ada koma/titik : ' + @KMService
	END
	ELSE IF @KMService LIKE '%.%'
	BEGIN
		SET @RemarksSystem += ' | KM Service tidak boleh ada koma/titik : ' + @KMService
	END
	ELSE IF (ISNUMERIC(@KMService) = 1)
	BEGIN
		IF (@CategoryWorkOrder = 'FS01')
		BEGIN
			IF CONVERT(INT,@KMService) <= 2000
			BEGIN
				SET @KMServiceNew = @KMService
			END
			ELSE
			BEGIN
				SET @RemarksSystem += ' | Invalid KM service ' + @KMService + '. KM service work order FS01 hanya boleh KM service diantara 0 - 2000'
			END
		END

		IF (@CategoryWorkOrder = 'FS02')
		BEGIN
			IF (CONVERT(INT,@KMService) BETWEEN 0 AND 11000)
			BEGIN
				SET @KMServiceNew = @KMService
			END
			ELSE
			BEGIN
				SET @RemarksSystem += ' | Invalid KM service ' + @KMService + '. KM service work order FS02 hanya boleh KM service diantara 0 - 11000'
			END
		END

		IF (@CategoryWorkOrder NOT IN ('FS01','FS02'))
		BEGIN
			SET @KMServiceNew = @KMService
		END

	END
	ELSE
	BEGIN
		SET @RemarksSystem += ' | Invalid format KM service ' + @KMService
	END

	--Total LC Validation
	IF RIGHT(@TotalLC,3) = '.00' --supaya tidak terkena validasi titik dua, terkadang value terbaca ada titik dua dibelakang angka
	BEGIN
		SET @TotalLC = REPLACE( @TotalLC, RIGHT(@TotalLC, 3), '' )
	END

	IF @TotalLC LIKE '%,%'
	BEGIN
		SET @RemarksSystem += ' | Format LC tidak boleh ada koma/titik : ' + @TotalLC
	END
	ELSE IF @TotalLC LIKE '%.%'
	BEGIN
		SET @RemarksSystem += ' | Format LC tidak boleh ada koma/titik : ' + @TotalLC
	END
	ELSE IF (ISNUMERIC(@TotalLC) = 1)
	BEGIN
		SET @TotalLCNew = @TotalLC
	END
	ELSE
	BEGIN
		SET @RemarksSystem += ' | Invalid format total LC ' + @TotalLC
	END

	--Validate duplicate
	IF ((SELECT COUNT(1) FROM #TempTable (NOLOCK) A WHERE 
	ISNULL(KodeDealer,'') = ISNULL(@KodeDealer,'') AND
	ISNULL(TglBukaTransaksi,'') = ISNULL(@TglBukaTransaksi,'') AND
	ISNULL(TglTutupTransaksi,'') = ISNULL(@TglTutupTransaksi,'') AND
	ISNULL(NoWorkOrder,'') = ISNULL(@NoWorkOrder,'') AND
	ISNULL(NoChassis,'') = ISNULL(@NoChassis,'') AND
	ISNULL(CategoryWorkOrder,'') = ISNULL(@CategoryWorkOrder,'')) > 1)
	BEGIN
		SET @RemarksSystem += ' | Duplicate data'
	END

	IF LEFT(@RemarksSystem ,2) = ' |'
	BEGIN
		SET @RemarksSystem = SUBSTRING(@RemarksSystem, 3, LEN(@RemarksSystem))
	END

	--Set value to temp table
	UPDATE #TempTable
	SET TglBukaTransaksi = @TglBukaTransaksiNew
    ,WaktuMasuk = @WaktuMasukNew
    ,TglTutupTransaksi = @TglTutupTransaksiNew
    ,WaktuKeluar = @WaktuKeluarNew
    ,DealerID = @DealerID
    ,TrTraineMekanikID = @TrTraineMekanikID
    ,ChassisMasterID = @ChassisMasterID
    ,WorkOrderCategoryID = @WorkOrderCategoryID
    ,ServicePlaceID = @ServicePlaceID
    ,ServiceTypeID = @ServiceTypeID
    ,TotalLC = @TotalLCNew
	,KMService = @KMServiceNew
    ,MetodePembayaran = UPPER(@MetodePembayaran)
	,Model = @Model
	,Transmition = @Transmition
	,DriveSystem = @DriveSystem
    ,RemarksSystem = @RemarksSystem
	,RemarksSpecial = @RemarksSpecial
	,StatusAktif = @StatusAktif
	WHERE 
	ISNULL(KodeDealer,'') = ISNULL(@KodeDealer,'') AND
	ISNULL(TglBukaTransaksi,'') = ISNULL(@TglBukaTransaksi,'') AND
	ISNULL(TglTutupTransaksi,'') = ISNULL(@TglTutupTransaksi,'') AND
	ISNULL(NoWorkOrder,'') = ISNULL(@NoWorkOrder,'') AND
	ISNULL(NoChassis,'') = ISNULL(@NoChassis,'') AND
	ISNULL(CategoryWorkOrder,'') = ISNULL(@CategoryWorkOrder,'')

	
	--update status di assistlog
	IF ISNULL(@RemarksSystem,'') != ''
	BEGIN
		SET @AssistLogValidateStatus = 0
	END

FETCH NEXT
FROM @CursorValidation INTO @TglBukaTransaksi, @TglTutupTransaksi, @KodeDealer, @KodeMekanik, @CategoryWorkOrder, 
@TempatPengerjaan, @Layanan, @MetodePembayaran, @KMService, @TotalLC, @WaktuMasuk, @WaktuKeluar, @NoWorkOrder, @NoChassis
END
CLOSE @CursorValidation
DEALLOCATE @CursorValidation

BEGIN TRY
BEGIN TRANSACTION

INSERT INTO [dbo].[AssistServiceIncoming]
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
           ,[RemarksSystem]
		   ,[RemarksSpecial]
           ,[RemarksBM]
           ,[StatusAktif]
           ,[ValidateSystemStatus]
           ,[RowStatus]
           ,[CreatedBy]
           ,[CreatedTime]
           ,[LastUpdateBy]
           ,[LastUpdateTime])
SELECT 
			@AssistUploadLogID ,
		   TglBukaTransaksi,
		   WaktuMasuk ,
		   TglTutupTransaksi ,
		   WaktuKeluar,
           DealerID,
           KodeDealer,
           TrTraineMekanikID,
           KodeMekanik,
           NoWorkOrder,
           ChassisMasterID,
           NoChassis ,
           WorkOrderCategoryID,
           CategoryWorkOrder,
           KMService,
           ServicePlaceID,
           TempatPengerjaan ,
           ServiceTypeID,
           Layanan,
           TotalLC,
		   MetodePembayaran,
		   Model,
		   Transmition,
		   DriveSystem,
           RemarksSystem,
		   RemarksSpecial,
           NULL AS RemarksBM,
           StatusAktif,
           (CASE WHEN ISNULL(RemarksSystem,'') != '' THEN 0
		   ELSE 1 END) AS ValidateSystemStatus,
           0 AS RowStatus,
           'System',
		   GETDATE(),
		   '',
		   GETDATE()
		   FROM #TempTable

--update table Assistuploadlogstatus in here

DECLARE @RowCount DECIMAL(18,2)= (SELECT COUNT(1) FROM AssistServiceIncoming (NOLOCK) WHERE AssistUploadLogID = @AssistUploadLogID )
DECLARE @SuccessCount DECIMAL(18,2) = (SELECT COUNT(1) FROM AssistServiceIncoming (NOLOCK) WHERE AssistUploadLogID = @AssistUploadLogID  AND ValidateSystemStatus = 1)
DECLARE @FailedCount DECIMAL(18,2) = (SELECT COUNT(1) FROM AssistServiceIncoming (NOLOCK) WHERE AssistUploadLogID = @AssistUploadLogID  AND ValidateSystemStatus = 0)
DECLARE @Performance DECIMAL(18,2) = 0
DECLARE @ErrorRatio DECIMAL(18,2) = 0
IF (@RowCount > 0)
BEGIN
	SET @Performance = (@SuccessCount / @RowCount) * 100
	SET @ErrorRatio = (@FailedCount / @RowCount) * 100
END

IF @Performance = 100
BEGIN
	SET @AssistLogValidateStatus = 1
END

UPDATE AssistUploadLog SET Performance = @Performance, ErrorRatio = @ErrorRatio, [Status] = 2, ValidateStatus = @AssistLogValidateStatus WHERE ID = @AssistUploadLogID

COMMIT TRANSACTION
END TRY
BEGIN CATCH
IF @@TRANCOUNT > 0
BEGIN
ROLLBACK TRANSACTION

DECLARE @ErrorMessage NVARCHAR(4000);
DECLARE @ErrorSeverity INT;
DECLARE @ErrorState INT;

SELECT 
@ErrorMessage = ERROR_MESSAGE(),
@ErrorSeverity = ERROR_SEVERITY(),
@ErrorState = ERROR_STATE();

RAISERROR (@ErrorMessage, @ErrorSeverity, @ErrorState );

END
END CATCH

END

/****** Object:  StoredProcedure [dbo].[sp_ProcessAssistPartSales]    Script Date: 1/8/2019 11:36:34 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:		SLA
-- Create date: 21-12-2017
-- Description:	
-- =============================================
ALTER PROCEDURE [dbo].[sp_ProcessAssistPartSales]
@TVP dbo.AssistPartSalesType READONLY,
@AssistUploadLogID  INT,
@DealerCode VARCHAR(10)
AS
BEGIN

DECLARE @PeriodeMonth INT
DECLARE @PeriodeYear INT
SELECT @PeriodeMonth = [Month], @PeriodeYear = [Year] FROM AssistUploadLog WHERE ID = @AssistUploadLogID
DECLARE @DealerGroupIDHeader INT = NULL
SELECT @DealerGroupIDHeader = DealerGroupID FROM Dealer WHERE DealerCode = @DealerCode
SELECT @PeriodeMonth = [Month], @PeriodeYear = [Year] FROM AssistUploadLog WHERE ID = @AssistUploadLogID

SELECT *, CAST(NULL AS VARCHAR(MAX)) as RemarksSystem,
CAST(NULL AS INT) as DealerID,
CAST(NULL AS INT) as SalesChannelID,
CAST(NULL AS INT) as TrTraineeSalesSparepartID,
CAST(NULL AS INT) as SalesmanHeaderID,
CAST(NULL AS INT) as SparepartMasterID,
CAST(1 AS INT) as StatusAktif
INTO #TempTable
FROM @TVP

--Declare variable result
DECLARE @TglTransaksiNew DATE = NULL
DECLARE @DealerID INT = NULL
DECLARE @SalesChannelID INT = NULL
DECLARE @TrTraineeSalesSparepartID INT = NULL
DECLARE @SalesmanHeaderID INT = NULL
DECLARE @SparepartMasterID INT = NULL
DECLARE @RetailPrice MONEY = NULL
DECLARE @QtyNew FLOAT = NULL
DECLARE @HargaBeliNew MONEY = NULL
DECLARE @HargaJualNew MONEY = NULL
DECLARE @RemarksSystem VARCHAR(MAX) = ''
DECLARE @StatusAktif INT = NULL
DECLARE @AssistLogValidateStatus INT = 1
DECLARE @DealerGroupID INT = NULL


--Get Closing
DECLARE @IsClosed BIT = 0
IF EXISTS(SELECT TOP 1 1 FROM [AssistCutOffPeriod] A (NOLOCK)
INNER JOIN Dealer B (NOLOCK) ON B.ID = A.DealerID
WHERE [Month] = @PeriodeMonth AND [Year] = @PeriodeYear AND B.DealerCode = @DealerCode
AND A.RowStatus = 0 AND A.Status = 1 AND B.RowStatus = 1 AND B.[Status] = 1)
BEGIN
	SET @IsClosed = 1
END

--Declare variable for cursor
DECLARE @TglTransaksi NVARCHAR(200)
DECLARE @KodeDealer NVARCHAR(200)
DECLARE @KodeCustomer NVARCHAR(200)
DECLARE @KodeSalesChannel NVARCHAR(200)
DECLARE @KodeSalesman NVARCHAR(200)
DECLARE @NoWorkOrder NVARCHAR(200)
DECLARE @NoParts NVARCHAR(200)
DECLARE @Qty NVARCHAR(200)
DECLARE @HargaBeli NVARCHAR(200)
DECLARE @HargaJual NVARCHAR(200)

DECLARE @CursorValidation CURSOR
SET @CursorValidation = CURSOR FOR
SELECT ISNULL(TglTransaksi,''), ISNULL(KodeDealer,''), ISNULL(KodeCustomer,''), ISNULL(KodeSalesChannel,''),ISNULL(KodeSalesman,''), ISNULL(NoWorkOrder,''), ISNULL(NoParts,''), ISNULL(Qty,''), ISNULL(HargaBeli,''), ISNULL(HargaJual,'')
FROM #TempTable (NOLOCK)
OPEN @CursorValidation
FETCH NEXT
FROM @CursorValidation INTO @TglTransaksi, @KodeDealer, @KodeCustomer, @KodeSalesChannel, 
@KodeSalesman, @NoWorkOrder, @NoParts, @Qty, @HargaBeli, @HargaJual
WHILE @@FETCH_STATUS = 0
BEGIN
	
SET @TglTransaksiNew = NULL
SET @DealerID = NULL
SET @SalesChannelID = NULL
SET @TrTraineeSalesSparepartID = NULL
SET @SalesmanHeaderID = NULL
SET @SparepartMasterID = NULL
SET @QtyNew = NULL
SET @HargaBeliNew = NULL
SET @HargaJualNew = NULL
SET @RemarksSystem = ''
SET @StatusAktif = 0	
SET @DealerGroupID = NULL
SET @RetailPrice = NULL

	--Tanggal Transaksi Validation
	BEGIN TRY		
			BEGIN TRY
				SET @TglTransaksiNew = convert(datetime, @TglTransaksi, 3)
			END TRY
			BEGIN CATCH
				SET @TglTransaksiNew = convert(datetime, @TglTransaksi)
			END CATCH

		IF CONVERT(DATE, @TglTransaksiNew) = '1900-01-01'
		BEGIN
			SET @TglTransaksiNew = NULL
			SET @RemarksSystem += ' | Invalid format tanggal transaksi ' + @TglTransaksi
		END
		--Period Validation
		ELSE
		BEGIN
			IF (Month(@TglTransaksiNew) = @PeriodeMonth AND YEAR(@TglTransaksiNew) = @PeriodeYear)
			BEGIN
				IF @IsClosed = 1
				BEGIN
					SET @RemarksSystem += ' | Periode upload sudah ditutup.'
				END
			END
			ELSE
			BEGIN
				SET @RemarksSystem += ' | Tanggal transaksi ' + @TglTransaksi + ' diluar periode '+  CONVERT(VARCHAR(10),@PeriodeMonth) + '-' + CONVERT(VARCHAR(10),@PeriodeYear)
			END
		END
	END TRY
	BEGIN CATCH
		SET @RemarksSystem += ' | Invalid format tanggal transaksi ' + @TglTransaksi
	END CATCH

	--Dealer Validation
	SELECT TOP 1 @DealerID = ID,  @DealerGroupID = DealerGroupID FROM Dealer (NOLOCK) WHERE RowStatus = 0 AND Status = 1 AND  DealerCode = @KodeDealer
	IF @DealerID IS NULL
	BEGIN 
		SET @RemarksSystem += ' | Invalid data dealer ' + @KodeDealer
	END
	ELSE
	BEGIN
		IF (@DealerGroupID != @DealerGroupIDHeader)
		BEGIN
			SET @DealerID = NULL
			SET @RemarksSystem += ' | Kode dealer ' + @KodeDealer + ' bukan merupakan group dari ' + @DealerCode
		END
	END
	
	--Kode Customer Validation
	IF ISNULL(@KodeCustomer,'') = ''
	BEGIN 
		SET @RemarksSystem += ' | Invalid kode customer ' + @KodeCustomer
	END
	ELSE IF (REPLACE(REPLACE(@KodeCustomer , CHAR(13), ''), CHAR(10), '')= 'RS0001')
	BEGIN
		SET @RemarksSystem  = @RemarksSystem --it's true
	END
	ELSE IF (LEFT(@KodeCustomer,2) = 'FF' )
	BEGIN
		SET @RemarksSystem  = @RemarksSystem --it's true because don't have table fleet customer in MMKSI
	END
	ELSE IF (LEFT(@KodeCustomer,1) = '1'  OR LEFT(@KodeCustomer,1) = '2')
	BEGIN
		--cek ke table dealer
		IF NOT EXISTS(SELECT 1 FROM Dealer WHERE DealerCode = @KodeCustomer)
		BEGIN
			SET @RemarksSystem += ' | Invalid kode customer ' + @KodeCustomer
		END
	END
	ELSE
	BEGIN
		IF NOT EXISTS(SELECT 1 FROM PartShop WHERE PartShopCode = @KodeCustomer OR OldPartShopCode = @KodeCustomer)
		BEGIN
			SET @RemarksSystem += ' | Invalid kode customer ' + @KodeCustomer
		END
	END
	
	--Sales Channel Validation
	SELECT TOP 1 @SalesChannelID = ID FROM AssistSalesChannel (NOLOCK) WHERE RowStatus = 0 AND [Status] = 1 AND SalesChannelCode = @KodeSalesChannel
	IF @SalesChannelID IS NULL
	BEGIN 
		SET @RemarksSystem += ' | Invalid data sales channel ' + @KodeSalesChannel
	END

	--Salesman Code Validation
	IF @SalesChannelID IS NOT NULL
	BEGIN
		IF @KodeSalesChannel = 'C'
		BEGIN
			IF ISNULL(@KodeSalesman,'') != ''
			BEGIN
			
				SELECT TOP 1 @SalesmanHeaderID = ID FROM SalesmanHeader (NOLOCK) WHERE [Status] = 2 AND RowStatus = 0 AND (SalesmanCode = @KodeSalesman OR CONVERT(VARCHAR(50),ID) = @KodeSalesman ) AND DealerID = @DealerID --status aktif
				IF @SalesmanHeaderID IS NULL
				BEGIN
					SET @RemarksSystem += ' | Invalid data kode salesman atau kode part employee ' + @KodeSalesman + ' jika sales channel yang dipilih adalah C'
				END
				
			END
			ELSE
			BEGIN
				SET @RemarksSystem += ' | Invalid data kode salesman atau kode part employee'
			END
		END

		IF @KodeSalesChannel = 'S'
		BEGIN
			IF ISNULL(@KodeSalesman,'') != ''
			BEGIN
				SELECT TOP 1 @SalesmanHeaderID = ID FROM SalesmanHeader (NOLOCK) WHERE [Status] = 2 AND RowStatus = 0 AND (SalesmanCode = @KodeSalesman OR CONVERT(VARCHAR(50),ID) = @KodeSalesman ) AND DealerID = @DealerID --status aktif
				IF @SalesmanHeaderID IS NULL
				BEGIN
					SET @RemarksSystem += ' | Invalid data kode salesman ' + @KodeSalesman + ' jika sales channel yang dipilih adalah S'
				END
			END
			ELSE
			BEGIN
				SET @RemarksSystem += ' | Invalid data kode salesman'
			END
		END

		IF @KodeSalesChannel = 'W'
		BEGIN
			IF ISNULL(@KodeSalesman,'') != ''
			BEGIN
				IF ISNUMERIC(@KodeSalesman) = 1
				BEGIN
					SELECT TOP 1 @TrTraineeSalesSparepartID = ID FROM V_DataSiswa (NOLOCK) WHERE [Status] = 'Aktif' AND ID = @KodeSalesman AND DealerCode = @KodeDealer--AND JobPosition = 'SPR_SLM'
				END
				
				IF @TrTraineeSalesSparepartID IS NULL
				BEGIN 
					SET @RemarksSystem += ' | Invalid data kode service advisor ' + @KodeSalesman + ' jika sales channel yang dipilih adalah W'
				END
			END
			ELSE
			BEGIN
				SET @RemarksSystem += ' | Invalid data kode service advisor'
			END
		END

		IF @KodeSalesChannel = 'I'
		BEGIN
			IF (ISNULL(@KodeSalesman,'') = '')
			BEGIN 
				SET @RemarksSystem += ' | Invalid data kode registrasi service harus diisi'
			END
		END
	END
	ELSE
	BEGIN
		SET @RemarksSystem += ' | Invalid data kode salesman'
	END 

	--Work Order Number Validation
	IF ISNULL(@NoWorkOrder,'') = ''
	BEGIN
		SET @RemarksSystem += ' | Invalid data no work order'
	END

	--No Parts Validation
	SELECT TOP 1 @SparepartMasterID = ID, @RetailPrice = RetalPrice FROM SparepartMaster (NOLOCK) WHERE RowStatus = 0 AND PartNumber = @NoParts AND ProductCategoryID != 2 --AND ActiveStatus = 0 
	IF @SparepartMasterID IS NULL
	BEGIN
		IF LEFT(@NoParts,3) != 'NPN' --Parts no genuine diijinkan walau tidak ada di master data
		BEGIN
			SET @RemarksSystem += ' | Invalid data no part ' + @NoParts
		END
	END
	

	--Qty Validation
	IF @Qty = '-' --terkadang user input '-' dan ini dianggap numeric oleh sql
	BEGIN
		SET @RemarksSystem += ' | Invalid format qty ' + @Qty
	END
	ELSE IF @Qty LIKE '%.%' AND @Qty LIKE '%,%'
	BEGIN
		SET @RemarksSystem += ' | Invalid format qty ' + @Qty
	END
	ELSE IF (ISNUMERIC(REPLACE(@Qty,',','.')) = 1)
	BEGIN
		SET @QtyNew = REPLACE(@Qty,',','.')
	END
	ELSE
	BEGIN
		SET @RemarksSystem += ' | Invalid format qty ' + @Qty
	END

	--Harga Beli Validation
	IF RIGHT(@HargaBeli,3) = '.00' --supaya tidak terkena validasi titik dua, terkadang value terbaca ada titik dua dibelakang angka
	BEGIN
		SET @HargaBeli = REPLACE( @HargaBeli, RIGHT(@HargaBeli, 3), '' )
	END

	IF @HargaBeli LIKE '%,%'
	BEGIN
		SET @RemarksSystem += ' | Harga Beli tidak boleh ada koma/titik : ' + @HargaBeli
	END
	ELSE IF @HargaBeli LIKE '%.%'
	BEGIN
		SET @RemarksSystem += ' | Harga Beli tidak boleh ada koma/titik : ' + @HargaBeli
	END
	ELSE IF (ISNUMERIC(@HargaBeli) = 1)
	BEGIN
		SET @HargaBeliNew = @HargaBeli
	END
	ELSE
	BEGIN
		SET @RemarksSystem += ' | Invalid format harga beli ' + @HargaBeli
	END

	--Harga Jual Validation
	IF RIGHT(@HargaJual,3) = '.00' --supaya tidak terkena validasi titik dua, terkadang value terbaca ada titik dua dibelakang angka
	BEGIN
		SET @HargaJual = REPLACE( @HargaJual, RIGHT(@HargaJual, 3), '' )
	END

	IF @HargaJual LIKE '%,%'
	BEGIN
		SET @RemarksSystem += ' | Harga Jual tidak boleh ada koma/titik : ' + @HargaJual
	END
	ELSE IF @HargaJual LIKE '%.%'
	BEGIN
		SET @RemarksSystem += ' | Harga Jual tidak boleh ada koma/titik : ' + @HargaJual
	END
	ELSE IF (ISNUMERIC(@HargaJual) = 1)
	BEGIN
		IF ISNULL(@RetailPrice,0) > 0
		BEGIN
			IF isnull(cast(@HargaJual as float),0) > (@RetailPrice + (@RetailPrice * 0.2))
			BEGIN
				SET @RemarksSystem += ' | Harga Jual ' + @HargaJual + ' terlalu besar'
			END
			ELSE
			BEGIN
				SET @HargaJualNew = @HargaJual
			END
		END 
		
	END
	ELSE
	BEGIN
		SET @RemarksSystem += ' | Invalid format harga jual ' + @HargaJual
	END

	--Validate duplicate
	IF ((SELECT COUNT(1) FROM #TempTable (NOLOCK) A WHERE 
	ISNULL(KodeDealer,'') = ISNULL(@KodeDealer,'') AND
	ISNULL(TglTransaksi,'') = ISNULL(@TglTransaksi,'') AND
	ISNULL(NoParts,'') = ISNULL(@NoParts,'') AND
	ISNULL(NoWorkOrder,'') = ISNULL(@NoWorkOrder,'') AND
	ISNULL(KodeCustomer,'') = ISNULL(@KodeCustomer,'')) > 1)
	BEGIN
		SET @RemarksSystem += ' | Duplicate data'
	END

	--Validate duplicate
	IF ((SELECT COUNT(1) FROM #TempTable (NOLOCK) A WHERE 
	ISNULL(KodeDealer,'') = ISNULL(@KodeDealer,'') AND
	ISNULL(TglTransaksi,'') = ISNULL(@TglTransaksi,'') AND
	ISNULL(NoParts,'') = ISNULL(@NoParts,'') AND
	ISNULL(NoWorkOrder,'') = ISNULL(@NoWorkOrder,'') AND
	ISNULL(KodeCustomer,'') = ISNULL(@KodeCustomer,'')) > 1)
	BEGIN
		SET @RemarksSystem += ' | Duplicate data'
	END

	IF LEFT(@RemarksSystem ,2) = ' |'
	BEGIN
		SET @RemarksSystem = SUBSTRING(@RemarksSystem, 3, LEN(@RemarksSystem))
	END


	UPDATE #TempTable
	SET 
	TglTransaksi =  @TglTransaksiNew,
	DealerID = @DealerID,
	SalesChannelID =  @SalesChannelID,
	TrTraineeSalesSparepartID =  @TrTraineeSalesSparepartID,
	SalesmanHeaderID = @SalesmanHeaderID,
	SparepartMasterID =  @SparepartMasterID,
	Qty = @QtyNew,
	HargaBeli = @HargaBeliNew,
	HargaJual = @HargaJualNew,
	RemarksSystem = @RemarksSystem,
	StatusAktif = @StatusAktif
	WHERE 
	ISNULL(KodeDealer,'') = ISNULL(@KodeDealer,'') AND
	ISNULL(TglTransaksi,'') = ISNULL(@TglTransaksi,'') AND
	ISNULL(NoParts,'') = ISNULL(@NoParts,'') AND
	ISNULL(NoWorkOrder,'') = ISNULL(@NoWorkOrder,'') AND
	ISNULL(KodeCustomer,'') = ISNULL(@KodeCustomer,'')
	
	--update status di assistlog
	IF ISNULL(@RemarksSystem,'') != ''
	BEGIN
		SET @AssistLogValidateStatus = 0
	END

FETCH NEXT
FROM @CursorValidation INTO @TglTransaksi, @KodeDealer, @KodeCustomer, @KodeSalesChannel, 
@KodeSalesman, @NoWorkOrder, @NoParts, @Qty, @HargaBeli, @HargaJual
END
CLOSE @CursorValidation
DEALLOCATE @CursorValidation

BEGIN TRY
BEGIN TRANSACTION

INSERT INTO [dbo].[AssistPartSales]
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
           ,[RemarksSystem]
           ,[StatusAktif]
           ,[ValidateSystemStatus]
           ,[RowStatus]
           ,[CreatedBy]
           ,[CreatedTime]
           ,[LastUpdateBy]
           ,[LastUpdateTime])
     SELECT 
           @AssistUploadLogID,
           TglTransaksi,
           DealerID,
           KodeDealer,
           KodeCustomer,
           SalesChannelID,
           KodeSalesChannel,
           TrTraineeSalesSparepartID,
		   SalesmanHeaderID,
           KodeSalesman,
           NoWorkOrder,
           SparepartMasterID,
           NoParts,
           Qty,
           HargaBeli,
           HargaJual,
           RemarksSystem,
           StatusAktif,
           (CASE WHEN ISNULL(RemarksSystem,'') != '' THEN 0
		   ELSE 1 END) AS ValidateSystemStatus,
           0 AS RowStatus,
           'System',
		   GETDATE(),
		   '',
		   GETDATE()
		   FROM #TempTable
--update table Assistuploadlogstatus in here

DECLARE @RowCount DECIMAL(18,2)= (SELECT COUNT(1) FROM AssistPartSales (NOLOCK) WHERE AssistUploadLogID = @AssistUploadLogID )
DECLARE @SuccessCount DECIMAL(18,2) = (SELECT COUNT(1) FROM AssistPartSales (NOLOCK) WHERE AssistUploadLogID = @AssistUploadLogID  AND ValidateSystemStatus = 1)
DECLARE @FailedCount DECIMAL(18,2) = (SELECT COUNT(1) FROM AssistPartSales (NOLOCK) WHERE AssistUploadLogID = @AssistUploadLogID  AND ValidateSystemStatus = 0)
DECLARE @Performance DECIMAL(18,2) = 0
DECLARE @ErrorRatio DECIMAL(18,2) = 0
IF (@RowCount > 0)
BEGIN
	SET @Performance = (@SuccessCount / @RowCount) * 100
	SET @ErrorRatio = (@FailedCount / @RowCount) * 100
END

IF @Performance = 100
BEGIN
	SET @AssistLogValidateStatus = 1
END

UPDATE AssistUploadLog SET Performance = @Performance, ErrorRatio = @ErrorRatio,  [Status] = 2, ValidateStatus = @AssistLogValidateStatus  WHERE ID = @AssistUploadLogID 

COMMIT TRANSACTION
END TRY
BEGIN CATCH
IF @@TRANCOUNT > 0
BEGIN
ROLLBACK TRANSACTION

DECLARE @ErrorMessage NVARCHAR(4000);
DECLARE @ErrorSeverity INT;
DECLARE @ErrorState INT;

SELECT 
@ErrorMessage = ERROR_MESSAGE(),
@ErrorSeverity = ERROR_SEVERITY(),
@ErrorState = ERROR_STATE();

RAISERROR (@ErrorMessage, @ErrorSeverity, @ErrorState );

END
END CATCH

END