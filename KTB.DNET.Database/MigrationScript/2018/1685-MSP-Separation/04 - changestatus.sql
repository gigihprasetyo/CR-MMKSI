set xact_abort on
go

begin transaction
go

set ANSI_NULLS on
go

-- =============================================
-- Author:		Ako
-- Create date: 15Jan2018
-- Description:	digunakan id Periodical Maintenance untuk mendapatkan status MSP
-- exec sp_MSP_GetMSPStatus 'MK2NCWPARJJ004442',7
-- select getdate() 
-- =============================================
alter PROCEDURE sp_MSP_GetMSPStatus
	-- Add the parameters for the stored procedure here
	  @ChassisNumber VARCHAR(20) ,
	  @PMKindID INT
AS
	  BEGIN
	-- Insert statements for procedure here
			DECLARE	@CreatedDate AS DATETIME = GETDATE()
			DECLARE	@PMKindCode AS VARCHAR(2)
			SELECT	@PMKindCode = KindCode
			FROM	PMKind
			WHERE	ID = @PMKindID

			SELECT	LastMSPRegHistory.ID AS MSPRegHistoryID ,
					CASE WHEN IsTrfPayment.ID IS NULL
							  AND LastMSPRegHistory.BenefitMasterHeaderID = 0 THEN 'Need Payment'
						 ELSE CASE WHEN IsCover.ID IS NULL THEN 'PM'
								   ELSE CASE WHEN DATEADD(YEAR, g.Duration, ISNULL(b.OpenFakturDate, '1753/01/01')) < GETDATE()
											 THEN 'MSP EXPIRED'
											 ELSE 'Smart Package; Type ' + h.Description + '; Tahun ke '
												  + CONVERT(VARCHAR(5), (DATEDIFF(YEAR,
																				 ISNULL(b.OpenFakturDate, '1753/01/01'),
																				 @CreatedDate)/365) +1     ) + ' (Valid Until '
												  + CONVERT(VARCHAR(20), DATEADD(YEAR, g.Duration,
																				 ISNULL(b.OpenFakturDate, '1753/01/01')), 103)
												  + ')'
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
							  WHERE		f.RowStatus = 0
										AND (
											  (
												f.Status >= 1
												AND f.Status <> 2
												AND f.IsDownloadCertificate = 1
											  )
											  OR f.Status = 6
											)
										AND f.MSPRegistrationID = c.ID
							  ORDER BY	ID DESC
						) LastMSPRegHistory
			INNER JOIN MSPMaster g ON g.ID = LastMSPRegHistory.MSPMasterID
			INNER JOIN MSPType h ON h.ID = g.MSPTypeID
			OUTER APPLY (
						  SELECT TOP 1
									e.ID
						  FROM		MSPDurationPMKind e
						  WHERE		e.RowStatus = 0
									AND e.Duration = g.Duration
									AND e.PMKindCode = @PMKindCode
						) IsCover
			OUTER APPLY (
						  -- untuk mencari pembayaran dengan status selesai
                              SELECT	i.ID
							  FROM		MSPTransferPaymentDetail i
							  INNER JOIN MSPTransferPayment j ON j.ID = i.MSPTransferPaymentID
							  WHERE		i.RowStatus = 0
										AND j.RowStatus = 0
										AND i.MSPRegistrationHistoryID = LastMSPRegHistory.ID
										AND j.Status = 6
						) IsTrfPayment

						-- OUTER APPLY (
						--  SELECT TOP 1
						--			PK.[PKTDate]
						--  FROM		dbo.[ChassisMasterPKT] PK
						--  WHERE		1 = 1 AND PK.[RowStatus] = 0 AND PK.[ChassisMasterID] = a.[ID]
						--  ORDER BY	PK.Id DESC
						--) PK
			WHERE	a.RowStatus = 0
					AND b.RowStatus = 0
					AND c.RowStatus = 0
					AND g.RowStatus = 0
					AND h.RowStatus = 0
					AND a.ChassisNumber = @ChassisNumber
	  END
go

commit
go


