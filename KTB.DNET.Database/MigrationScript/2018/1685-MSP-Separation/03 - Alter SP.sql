set xact_abort on
go

begin transaction
go

set ANSI_NULLS on
go

-- =============================================
-- Author:		AKO
-- Create date: 15DES2017
-- Description:	Untuk get all MSPMaster yang terkait dengan VehicleType
-- exec sp_MSP_GetMSPMasterList 'MK2NCWPARHJ000234'
-- =============================================
alter PROCEDURE sp_MSP_GetMSPMasterList 
	-- Add the parameters for the stored procedure here
	  @ChassisNumber VARCHAR(50) ,
	  @MSPJoinDate DATETIME = NULL
AS
	  BEGIN
			IF @MSPJoinDate IS NULL
			   BEGIN
					 SET @MSPJoinDate = GETDATE()
			   END
			SELECT	VT.VechileTypeCode VehicleTypeCode ,
					CM.EngineNumber ,
					--EC.HandoverDate PKTDate ,
					--ISNULL(PK.[PKTDate], '1753/01/01') PKTDate ,
					ISNULL(ec.OpenFakturDate, '1753/01/01') PKTDate ,
					MM.ID MSPMasterID
			FROM	ChassisMaster CM
			INNER JOIN VechileColor VC ON VC.ID = CM.VechileColorID
			INNER JOIN VechileType VT ON VT.ID = VC.VechileTypeID
			INNER JOIN MSPMaster MM ON MM.VehicleTypeID = VT.ID
			INNER JOIN EndCustomer EC ON EC.ID = CM.EndCustomerID
			--OUTER APPLY (
			--			  SELECT TOP 1
			--						PK.[PKTDate]
			--			  FROM		dbo.[ChassisMasterPKT] PK
			--			  WHERE		1 = 1 AND PK.[RowStatus] = 0 AND PK.[ChassisMasterID] = CM.[ID]
			--			  ORDER BY	PK.Id DESC
			--			) PK
			WHERE	CM.ChassisNumber = @ChassisNumber AND CM.RowStatus = 0 AND VC.RowStatus = 0 AND VT.RowStatus = 0 AND MM.RowStatus = 0 AND MM.Status = 1 AND EC.RowStatus = 0 AND @MSPJoinDate BETWEEN MM.StartDate AND MM.EndDate
	  END
go

-- =============================================
-- Author:		Ako
-- Create date: 15Jan2018
-- Description:	digunakan id Periodical Maintenance untuk mendapatkan status MSP
-- exec sp_MSP_GetMSPStatus 'MK2NCWPARHJ000259',5
-- select getdate() 
-- =============================================
alter PROCEDURE sp_MSP_GetMSPStatus
	-- Add the parameters for the stored procedure here
	   @ChassisNumber VARCHAR(20) ,
	   @PMKindID INT
AS
	   BEGIN
	-- Insert statements for procedure here
			 DECLARE @CreatedDate AS DATETIME = GETDATE()
			 DECLARE @PMKindCode AS VARCHAR(2)
			 SELECT	@PMKindCode = KindCode
			 FROM	PMKind
			 WHERE	ID = @PMKindID

			 SELECT	LastMSPRegHistory.ID AS MSPRegHistoryID ,
					CASE WHEN IsTrfPayment.ID IS NULL AND LastMSPRegHistory.BenefitMasterHeaderID = 0
						 THEN 'Need Payment'
						 ELSE CASE WHEN IsCover.ID IS NULL THEN 'PM'
								   ELSE CASE WHEN DATEADD(YEAR, g.Duration,ISNULL(b.OpenFakturDate,'1753/01/01')) < GETDATE()
											 THEN 'MSP EXPIRED'
											 ELSE 'Smart Package; Type ' + h.Description + '; Tahun ke ' + CONVERT(VARCHAR(5), DATEDIFF(YEAR,
																										ISNULL(b.OpenFakturDate,'1753/01/01'),
																										@CreatedDate)) + ' (Valid Until ' + CONVERT(VARCHAR(20), DATEADD(YEAR,
																										g.Duration,
																										ISNULL(b.OpenFakturDate,'1753/01/01')), 103) + ')'
										END
							  END
					END MSPStatus ,
					LastMSPRegHistory.RegistrationDate ,
					IsTrfPayment.ID AS IsTrfPaymentID
			 FROM	ChassisMaster a
			 INNER JOIN EndCustomer b ON a.EndCustomerID = b.ID
			 INNER JOIN MSPRegistration c ON a.ID = c.ChassisMasterID
			 OUTER APPLY (
						   -- untuk mencari registrasi pendaftaran terakhir dengan status selesai
                              SELECT TOP 1
										f.*
							  FROM		MSPRegistrationHistory f
							  WHERE		f.RowStatus = 0 AND (
															  (
																f.Status >= 1 AND f.Status <> 2 AND f.IsDownloadCertificate = 1
															  ) OR f.Status = 6
															) AND f.MSPRegistrationID = c.ID
							  ORDER BY	ID DESC
						 ) LastMSPRegHistory
			 INNER JOIN MSPMaster g ON g.ID = LastMSPRegHistory.MSPMasterID
			 INNER JOIN MSPType h ON h.ID = g.MSPTypeID
			 OUTER APPLY (
						   SELECT TOP 1
									e.ID
						   FROM		MSPDurationPMKind e
						   WHERE	e.RowStatus = 0 AND e.Duration = g.Duration AND e.PMKindCode = @PMKindCode
						 ) IsCover
			 OUTER APPLY (
						   -- untuk mencari pembayaran dengan status selesai
                              SELECT	i.ID
							  FROM		MSPTransferPaymentDetail i
							  INNER JOIN MSPTransferPayment j ON j.ID = i.MSPTransferPaymentID
							  WHERE		i.RowStatus = 0 AND j.RowStatus = 0 AND i.MSPRegistrationHistoryID = LastMSPRegHistory.ID AND j.Status = 6
						 ) IsTrfPayment

						-- OUTER APPLY (
						--  SELECT TOP 1
						--			PK.[PKTDate]
						--  FROM		dbo.[ChassisMasterPKT] PK
						--  WHERE		1 = 1 AND PK.[RowStatus] = 0 AND PK.[ChassisMasterID] = a.[ID]
						--  ORDER BY	PK.Id DESC
						--) PK
			 WHERE	a.RowStatus = 0 AND b.RowStatus = 0 AND c.RowStatus = 0 AND g.RowStatus = 0 AND h.RowStatus = 0 AND a.ChassisNumber = @ChassisNumber
	   END
go

set ANSI_NULLS off
go

---------------------------------------------------------------------------------------------------------------
-- Date Created	: Monday, January 22, 2018
-- Created By	: DNet Team by using CodeSmith v 2.6
-- Rev History	:
---------------------------------------------------------------------------------------------------------------

alter PROCEDURE up_InsertMSPClaim
       @ID INT OUTPUT ,
       @DealerID INT ,
       @PMHeaderID INT ,
       @MSPRegistrationHistoryID INT ,
       @ClaimNumber VARCHAR(20) ,
       @ClaimDate DATETIME ,
       @Status SMALLINT ,
	   @ChassisNumberID INT,
	   @StandKM INT,
	   @PMKindID INT,
	   @VisitType VARCHAR(5),
	   @ServiceDate DATETIME,
	   @ReleaseDate DATETIME,
	   @Remarks VARCHAR(250),
       @RowStatus SMALLINT ,
       @CreatedBy VARCHAR(20) ,
	--@CreatedTime datetime,
       @LastUpdateBy VARCHAR(20)	--@LastUpdateTime datetime	
AS
INSERT  INTO [dbo].[MSPClaim]   ( DealerID ,
          PMHeaderID ,
          MSPRegistrationHistoryID ,
          ClaimNumber ,
          ClaimDate ,
          Status ,
          ChassisNumberID ,
          StandKM ,
          PMKindID ,
          VisitType ,
          ServiceDate ,
          ReleaseDate ,
          Remarks ,
          RowStatus ,
          CreatedBy ,
          CreatedTime ,
          LastUpdateBy ,
          LastUpdateTime
        )
VALUES  ( @DealerID, @PMHeaderID, @MSPRegistrationHistoryID, dbo.ufn_CreateMSPClaimNumber(GETDATE(), @DealerID),
          @ClaimDate, @Status, 
		  @ChassisNumberID, @StandKM, @PMKindID, @VisitType, @ServiceDate, @ReleaseDate, @Remarks,
		  @RowStatus, @CreatedBy, GETDATE(), @LastUpdateBy, GETDATE() )
 
SET @ID = @@IDENTITY
go

---------------------------------------------------------------------------------------------------------------
-- Date Created	: Monday, January 22, 2018
-- Created By	: DNet Team by using CodeSmith v 2.6
-- Rev History	:
---------------------------------------------------------------------------------------------------------------

alter PROCEDURE up_RetrieveMSPClaim @ID INT OUTPUT
AS
SET TRANSACTION ISOLATION LEVEL READ COMMITTED

SET NOCOUNT ON

SELECT  ID ,
        DealerID ,
        PMHeaderID ,
        MSPRegistrationHistoryID ,
        ClaimNumber ,
        ClaimDate ,
        [Status] ,
        ChassisNumberID ,
        StandKM ,
        PMKindID ,
        VisitType ,
        ServiceDate ,
        ReleaseDate ,
        Remarks ,
        RowStatus ,
        CreatedBy ,
        CreatedTime ,
        LastUpdateBy ,
        LastUpdateTime
FROM    [dbo].[MSPClaim]
WHERE   [ID] = @ID

SET NOCOUNT OFF
go

---------------------------------------------------------------------------------------------------------------
-- Date Created	: Monday, January 22, 2018
-- Created By	: DNet Team by using CodeSmith v 2.6
-- Rev History	:
---------------------------------------------------------------------------------------------------------------

alter PROCEDURE up_RetrieveMSPClaimList
AS
SET TRANSACTION ISOLATION LEVEL READ COMMITTED
SET NOCOUNT ON


SELECT  ID ,
        DealerID ,
        PMHeaderID ,
        MSPRegistrationHistoryID ,
        ClaimNumber ,
        ClaimDate ,
        Status ,
        ChassisNumberID ,
        StandKM ,
        PMKindID ,
        VisitType ,
        ServiceDate ,
        ReleaseDate ,
        Remarks ,
        RowStatus ,
        CreatedBy ,
        CreatedTime ,
        LastUpdateBy ,
        LastUpdateTime
FROM    [dbo].[MSPClaim] 

SET NOCOUNT OFF
go

---------------------------------------------------------------------------------------------------------------
-- Date Created	: Monday, January 22, 2018
-- Created By	: DNet Team by using CodeSmith v 2.6
-- Rev History	:
---------------------------------------------------------------------------------------------------------------

alter PROCEDURE up_UpdateMSPClaim
    @ID INT OUTPUT ,
    @DealerID INT ,
    @PMHeaderID INT ,
    @MSPRegistrationHistoryID INT ,
    @ClaimNumber VARCHAR(20) ,
    @ClaimDate DATETIME ,
    @Status SMALLINT ,
    @ChassisNumberID INT ,
    @StandKM INT ,
    @PMKindID INT ,
    @VisitType VARCHAR(5) ,
    @ServiceDate DATETIME ,
    @ReleaseDate DATETIME ,
    @Remarks VARCHAR(250) ,
    @RowStatus SMALLINT ,
    @CreatedBy VARCHAR(20) ,
	--@CreatedTime datetime,
    @LastUpdateBy VARCHAR(20)	--@LastUpdateTime datetime	
AS
    UPDATE  [dbo].[MSPClaim]
    SET     [DealerID] = @DealerID ,
            [PMHeaderID] = @PMHeaderID ,
            [MSPRegistrationHistoryID] = @MSPRegistrationHistoryID ,
            [ClaimNumber] = @ClaimNumber ,
            [ClaimDate] = @ClaimDate ,
            [Status] = @Status ,
            [ChassisNumberID] = @ChassisNumberID ,
            ReleaseDate = @ReleaseDate ,
            Remarks = @Remarks ,
            ServiceDate = @ServiceDate ,
            PMKindID = @PMKindID ,
            VisitType = @VisitType ,
            StandKM = @StandKM ,
            [RowStatus] = @RowStatus ,
            [CreatedBy] = @CreatedBy ,
	--[CreatedTime] = @CreatedTime,
            [LastUpdateBy] = @LastUpdateBy ,
            [LastUpdateTime] = GETDATE()
    WHERE   [ID] = @ID
go

commit
go


