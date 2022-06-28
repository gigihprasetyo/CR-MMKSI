SET XACT_ABORT ON
GO

BEGIN TRANSACTION
GO

SET ANSI_NULLS ON
GO

--========================================================================================================================                  
-- Created By: Mitrais (Prins Carl S)                  
-- Business Sector MMKSI                  
--========================================================================================================================                  

CREATE VIEW VWI_BusinessSector
AS
	   --MMKSI
SELECT	a.ID ,
		b.BusinessSectorName ,
		a.BusinessDomain ,
		BusinessName = b.BusinessSectorName + ' - ' + a.BusinessDomain ,
		a.LastUpdateTime --, a.Code
FROM	BusinessSectorDetail a WITH ( NOLOCK )
JOIN	BusinessSectorHeader b WITH ( NOLOCK ) ON a.BusinessSectorHeaderID = b.ID
												  AND b.RowStatus = 0
WHERE	a.RowStatus = 0
--KTB
--select ID, Description from ProfileDetail where ProfileHeaderID = 47
GO

--========================================================================================================================                  
-- Created By: Mitrais (Prins Carl S)                  
-- View For Campaign Report            
--========================================================================================================================   
CREATE VIEW VWI_CampaignReport
AS
	   SELECT	ROW_NUMBER() OVER ( ORDER BY HeaderID, BenefitRegNo, DealerCode, VehicleTypeID ) AS ID ,
				a.HeaderID ,
				a.NomorSurat ,
				a.Status ,
				a.BenefitRegNo ,
				a.Remarks ,
				a.RowStatus ,
				a.LastUpdateTime ,
				a.DealerID ,
				a.DealerCode ,
				a.FakturValidationStart ,
				a.FakturValidationEnd ,
				a.FakturOpenStart ,
				a.FakturOpenEnd ,
				a.VehicleTypeID ,
				a.VehicleTypeCode ,
				a.VehicleTypeDesc--, a.FormulaID    
	   FROM		(
				  SELECT DISTINCT
							bmh.ID AS HeaderID ,
							bmh.NomorSurat AS NomorSurat ,
							bmh.Status AS Status , -- status 0 (active) status 1 (not active)    
							bmh.BenefitRegNo AS BenefitRegNo ,
							bmh.Remarks AS Remarks ,
							bmh.RowStatus AS RowStatus ,
							bmh.LastUpdateTime AS LastUpdateTime ,
							bmd.RowStatus AS DetailRowStatus ,
							bml.DealerID AS DealerID ,
							dlr.DealerCode AS DealerCode ,
							bmd.FakturValidationStart ,
							bmd.FakturValidationEnd ,
							bmd.FakturOpenStart ,
							bmd.FakturOpenEnd ,
							bmt.VechileTypeID AS VehicleTypeID ,
							vtp.VechileTypeCode AS VehicleTypeCode ,
							vtp.Description AS VehicleTypeDesc--,    
       --bmd.FormulaID    
				  FROM		BenefitMasterHeader bmh WITH ( NOLOCK )
				  INNER JOIN BenefitMasterDetail bmd WITH ( NOLOCK ) ON bmh.ID = bmd.BenefitMasterHeaderID
																		AND bmd.RowStatus = 0
				  INNER JOIN BenefitMasterDealer bml WITH ( NOLOCK ) ON bmh.ID = bml.BenefitMasterHeaderID
																		AND bml.RowStatus = 0
				  INNER JOIN Dealer dlr WITH ( NOLOCK ) ON bml.DealerID = dlr.ID
														   AND dlr.RowStatus = 0
				  INNER JOIN BenefitMasterVehicleType bmt WITH ( NOLOCK ) ON bmd.ID = bmt.BenefitMasterDetailID
																			 AND bmt.RowStatus = 0
				  INNER JOIN VechileType vtp WITH ( NOLOCK ) ON bmt.VechileTypeID = vtp.ID
																AND vtp.RowStatus = 0
																AND vtp.Status <> 'X'    
--WHERE bmh.RowStatus =0    
				 ) a
GO

--========================================================================================================================                  
-- Created By: Mitrais (Prins Carl S)                  
-- View For City            
--========================================================================================================================   
CREATE VIEW VWI_City
AS
	   SELECT	ROW_NUMBER() OVER ( ORDER BY a.ProvinceCode, a.CityCode ) AS ID ,
				a.ProvinceCode ,
				a.ProvinceName ,
				a.CityCode ,
				a.CityName ,
				a.LastUpdateTime ,
				a.Status
	   FROM		(
				  SELECT	b.ProvinceCode ,
							b.ProvinceName ,
							a.CityCode ,
							a.CityName ,
							a.LastUpdateTime ,
							Status = CASE WHEN a.RowStatus <> 0 THEN a.RowStatus
										  ELSE CASE	WHEN a.Status = 'X' THEN -1
													ELSE a.RowStatus
											   END
									 END
				  FROM		City AS a WITH ( NOLOCK )
				  INNER JOIN Province AS b WITH ( NOLOCK ) ON a.ProvinceID = b.ID
				  WHERE		CityCode <> 'UNKNOWN'
				) a
GO

--========================================================================================================================                  
-- Created By: Mitrais (Prins Carl S)                  
-- View For Customer             
--========================================================================================================================   
CREATE VIEW VWI_Customer
AS
	   SELECT	a.CustomerCode ,
				a.CustomerRequestId ,
				a.DealerCode ,
				a.SPKNumber ,
				a.LastUpdateTime
	   FROM		(
				  SELECT	CustomerCode = a.Code ,
							CustomerRequestId = b.ID ,
							d.DealerCode ,
							c.SPKNumber ,
							a.LastUpdateTime
				  FROM		Customer a WITH ( NOLOCK )
				  LEFT JOIN CustomerRequest b WITH ( NOLOCK ) ON a.Code = b.CustomerCode
																 AND b.RowStatus = 0
				  LEFT JOIN SPKHeader c WITH ( NOLOCK ) ON b.ID = c.CustomerRequestID
														   AND c.RowStatus = 0
				  JOIN		Dealer d WITH ( NOLOCK ) ON b.DealerID = d.ID
														AND d.RowStatus = 0
				) a
--order by a.Code
GO

--========================================================================================================================                  
-- Created By: Mitrais (Prins Carl S)                  
-- View For Customer Vehicle               
--========================================================================================================================   
CREATE VIEW VWI_CustomerVehicle
AS
	   SELECT	a.ID ,
				b.DealerCode ,
				a.CustomerCode ,
				a.LastUpdateTime
	   FROM		CustomerRequest a WITH ( NOLOCK )
	   JOIN		Dealer b WITH ( NOLOCK ) ON a.DealerID = b.ID
	   WHERE	a.RowStatus = 0
GO

CREATE VIEW VWI_DealerSettingCreditAccount
AS
	   SELECT	t.ID ,
				DealerId = d.ID ,
				d.RowStatus ,
				d.DealerCode ,
				d.DealerName ,
				c.CityName ,
				ProvinceId = p.ID ,
				p.ProvinceName ,
				DealerGroupId = dg.ID ,
				dg.GroupName ,
				d.SearchTerm1 ,
				t.Status ,
				d.SalesUnitFlag ,
				d.ServiceFlag ,
				d.SparepartFlag ,
				t.LastUpdateTime ,
				t.TermOfPaymentID ,
				d.CreditAccount ,
				t.KelipatanPembayaran
	   FROM		Dealer d WITH ( NOLOCK )
	   LEFT JOIN City c WITH ( NOLOCK ) ON c.ID = d.CityID
	   LEFT JOIN Province p WITH ( NOLOCK ) ON p.ID = d.ProvinceID
	   LEFT JOIN DealerGroup dg WITH ( NOLOCK ) ON dg.ID = d.DealerGroupID
	   LEFT JOIN TOPCreditAccount t WITH ( NOLOCK ) ON d.ID = t.DealerID
	   WHERE	d.RowStatus = 0
GO

--========================================================================================================================                  
-- Created By: Mitrais (Prins Carl S)                  
-- WSCWorkOrder       
--========================================================================================================================     
CREATE VIEW VWI_DMSWorkOrderWSCStatus
AS
	   SELECT DISTINCT
				a.ID ,--a.DealerID,   
				b.DealerCode ,
				a.ChassisNumber ,   
    --c.ID AS ChassisMasterID,  
				a.WorkOrderNumber ,
				d.PQRNo ,
				d.PQRType ,
				g.ValueDesc AS PQRTypeText ,
				d.PQRDate ,
				d.RowStatus AS PQRStatus ,
				f.ValueDesc AS PQRStatusText ,
				e.ClaimType ,
				e.ClaimNumber ,
				e.Description ,
				e.ClaimStatus ,
				e.Status AS WSCStatus ,
				h.ValueDesc AS WSCStatusText ,
				LastUpdateTime = COALESCE(COALESCE(e.LastUpdateTime, d.LastUpdateTime), a.LastUpdateTime)
	   FROM		DMSWOWarrantyClaim a WITH ( NOLOCK )
	   LEFT JOIN Dealer b WITH ( NOLOCK ) ON a.DealerID = b.ID
											 AND b.RowStatus = 0
	   LEFT JOIN ChassisMaster c WITH ( NOLOCK ) ON a.ChassisNumber = c.ChassisNumber
													AND c.RowStatus = 0
	   LEFT JOIN PQRHeader d WITH ( NOLOCK ) ON a.DealerID = d.DealerID
												AND c.ID = d.ChassisMasterID
												AND d.RowStatus <> -1
	   LEFT JOIN WSCHeader e WITH ( NOLOCK ) ON d.DealerID = e.DealerID
												AND d.ChassisMasterID = e.ChassisMasterID
												AND d.PQRNo = e.PQR
												AND e.RowStatus = 0
	   LEFT JOIN StandardCode f WITH ( NOLOCK ) ON d.RowStatus = f.ValueId
												   AND f.Category = 'PQRStatus'
												   AND f.RowStatus = 0
	   LEFT JOIN StandardCode g WITH ( NOLOCK ) ON d.PQRType = g.ValueId
												   AND g.Category = 'PQRType'
												   AND f.RowStatus = 0
	   LEFT JOIN StandardCode h WITH ( NOLOCK ) ON e.Status = h.ValueId
												   AND h.Category = 'WSCStatus'
												   AND f.RowStatus = 0
	   WHERE	a.RowStatus = 0
GO

--========================================================================================================================                  
-- Created By: Mitrais (Prins Carl S)                  
-- View For Employee Parts               
--========================================================================================================================        
CREATE VIEW VWI_EmployeeParts
AS
	   SELECT	a.ID ,
				a.SalesmanCode ,
				a.DealerId ,
				b.DealerCode ,
				a.DealerBranchID ,
				c.DealerBranchCode ,
				Status = CASE WHEN a.RowStatus = -1 THEN a.RowStatus
							  ELSE CASE	WHEN a.Status = 2 THEN 0
										ELSE -1
								   END
						 END ,
				a.LastUpdateTime
	   FROM		SalesmanHeader a WITH ( NOLOCK )
	   LEFT JOIN Dealer b WITH ( NOLOCK ) ON a.DealerId = b.ID
											 AND b.RowStatus = 0
	   LEFT JOIN DealerBranch c WITH ( NOLOCK ) ON a.DealerId = c.DealerID
												   AND a.DealerBranchID = c.ID
												   AND c.RowStatus = 0
	   WHERE	a.SalesIndicator = 0
GO

--========================================================================================================================                  
-- Created By: Mitrais (Prins Carl S)                  
-- View For Employee Sales               
--========================================================================================================================
CREATE VIEW VWI_EmployeeSales
AS
	   SELECT	a.ID ,
				a.SalesmanCode ,
				a.DealerId ,
				b.DealerCode ,
				a.DealerBranchID ,
				c.DealerBranchCode ,
				Status = CASE WHEN a.RowStatus = -1 THEN a.RowStatus
							  ELSE CASE	WHEN a.Status = 2 THEN 0
										ELSE -1
								   END
						 END ,
				a.LastUpdateTime
	   FROM		SalesmanHeader a WITH ( NOLOCK )
	   LEFT JOIN Dealer b WITH ( NOLOCK ) ON a.DealerId = b.ID
											 AND b.RowStatus = 0
	   LEFT JOIN DealerBranch c WITH ( NOLOCK ) ON a.DealerId = c.DealerID
												   AND a.DealerBranchID = c.ID
												   AND c.RowStatus = 0
	   WHERE	a.SalesIndicator = 1
GO

--========================================================================================================================                  
-- Created By: Mitrais (Prins Carl S)                  
-- View For Employee Services              
--========================================================================================================================   
CREATE VIEW VWI_EmployeeService
AS
	   SELECT	a.[ID] ,
				a.[Name] ,
				D1.DealerCode ,
				D2.DealerBranchCode ,
				a.[BirthDate] ,
				a.[Gender] ,
				a.[NoKTP] ,
				a.[Email] ,
				a.[StartWorkingDate] ,
				a.[JobPosition] ,
				a.[EducationLevel] ,
				a.[Photo] ,
				a.[ShirtSize] ,
				Status = CASE WHEN a.RowStatus = -1 THEN a.RowStatus
							  ELSE CASE	WHEN a.Status <> 1 THEN -1
										ELSE 0
								   END
						 END ,
				a.[LastUpdateTime]
	   FROM		[TrTrainee] a WITH ( NOLOCK )
	   LEFT JOIN Dealer D1 WITH ( NOLOCK ) ON a.DealerID = D1.ID
											  AND D1.RowStatus = 0
	   LEFT JOIN DealerBranch D2 WITH ( NOLOCK ) ON a.DealerBranchID = D2.ID
													AND D2.RowStatus = 0
	   JOIN		JobPosition b WITH ( NOLOCK ) ON a.JobPosition = b.Code
												 AND b.RowStatus = 0
												 AND b.Category = 2
	   WHERE	b.Category = 2
GO

--========================================================================================================================                
-- Created By: Mitrais (Prins Carl S)                
-- View for Field Fix List     
--======================================================================================================================== 
CREATE VIEW VWI_FieldFixList
AS
	   SELECT	a.ID ,
				a.ChassisNo ,
				b.RecallRegNo ,
				b.Description ,
				b.BuletinDescription ,
				b.ValidStartDate , --c.ID, c.SoldDealerID,     
				e.DealerCode ,
				e.DealerName ,
				a.LastUpdateTime
	   FROM		RecallChassisMaster a WITH ( NOLOCK )
	   INNER JOIN RecallCategory b WITH ( NOLOCK ) ON a.RecallCategoryID = b.ID
													  AND b.RowStatus = 0
	   INNER JOIN ChassisMaster c WITH ( NOLOCK ) ON a.ChassisNo = c.ChassisNumber
													 AND c.RowStatus = 0
	   LEFT JOIN RecallService d WITH ( NOLOCK ) ON a.ID = d.RecallChassisMasterID
													AND d.RowStatus = 0
	   INNER JOIN Dealer e WITH ( NOLOCK ) ON c.SoldDealerID = e.ID
											  AND e.RowStatus = 0
	   WHERE	a.RowStatus = 0
				AND d.ID IS NULL     
--order by a.ID
GO

--========================================================================================================================                
-- Created By: Mitrais (Prins Carl S)                
-- View for Fleet        
--======================================================================================================================== 
CREATE VIEW VWI_Fleet
AS
	   SELECT	ID ,
				Code AS FleetCode ,
				Name AS FleetCustomerName ,
				LastUpdatedTime AS LastUpdateTime ,
				Status = RowStatus
	   FROM		FleetCustomer WITH ( NOLOCK )
	   WHERE	RowStatus = 0
	    
GO

--========================================================================================================================                
-- Created By: Mitrais (Prins Carl S)                
-- Get Distinct All Spare Part Master UOM               
--========================================================================================================================                
CREATE VIEW VWI_GetDistinctAllSparePartMasterUOM
AS
	   SELECT DISTINCT
				UoM
	   FROM		SparePartMaster WITH ( NOLOCK )
GO

--========================================================================================================================                  
-- Created By: Mitrais (Prins Carl S)                  
-- View for Invoice Revision              
--========================================================================================================================  
CREATE VIEW VWI_InvoiceRevision
AS
	   SELECT	a.ID ,
				a.RegNumber ,
				COALESCE(COALESCE(a.NewConfirmationDate, a.NewValidationDate), a.CreatedTime) AS RevisionDate ,
				a.RevisionStatus AS RevisionStatusID ,
				g.ValueDesc AS RevisionStatus ,
				f.Description AS RevisionType ,
				d.ChassisNumber ,
				b.SPKHeaderID ,
				c.SPKNumber ,
				e.DealerCode ,
				c.DealerSPKNumber ,
				a.LastUpdateTime
	   FROM		RevisionFaktur a WITH ( NOLOCK )
	   JOIN		RevisionSPKFaktur b WITH ( NOLOCK ) ON a.EndCustomerID = b.EndCustomerID
													   AND b.RowStatus = 0
	   JOIN		SPKHeader c WITH ( NOLOCK ) ON b.SPKHeaderID = c.ID
											   AND c.RowStatus = 0
	   JOIN		ChassisMaster d WITH ( NOLOCK ) ON a.ChassisMasterID = d.ID
												   AND d.RowStatus = 0
	   JOIN		Dealer e WITH ( NOLOCK ) ON c.DealerID = e.ID
											AND e.RowStatus = 0
	   LEFT JOIN RevisionType f WITH ( NOLOCK ) ON a.RevisionTypeID = f.ID
												   AND f.RowStatus = 0
	   LEFT JOIN StandardCode g WITH ( NOLOCK ) ON g.Category = 'IRRevisionStatus'
												   AND a.RevisionStatus = g.ValueId
												   AND g.RowStatus = 0
	   WHERE	a.RowStatus = 0
GO

--========================================================================================================================                    
-- Created By: Mitrais (Prins Carl S)                    
-- View for Karoseri                  
--========================================================================================================================                    
CREATE VIEW VWI_Karoseri
AS
	   SELECT	Code ,
				Name ,
				Province ,
				City ,
				LastUpdateTime ,
				CASE WHEN RowStatus = 0
						  AND Status = 1 THEN 0
					 ELSE -1
				END AS Status
	   FROM		Karoseri WITH ( NOLOCK )
GO

--========================================================================================================================                
-- Created By: Mitrais (Prins Carl S)                
-- View For Lead Customer from Sales Force             
--======================================================================================================================== 
CREATE VIEW VWI_LeadCustomerSalesForce
AS
	   SELECT	ROW_NUMBER() OVER ( ORDER BY a.DNetID ) AS ID ,
				a.DNetID ,
				a.SumberData ,
				a.CreateDate ,
				a.CreateDate_YYYYMMDD ,
				a.DealerCode ,
				a.DealerName ,
				a.CustomerTypeID ,
				a.CustomerType ,
				a.SalesmanCode ,
				a.Name ,
				a.CustomerCode ,
				a.CustomerName ,
				a.CustomerAddress ,
				a.Phone ,
				a.Email ,
				a.SexID ,
				a.Sex ,
				a.AgeSegmentID ,
				a.AgeSegment ,
				a.CustomerStatusID ,
				a.CustomerStatus ,
				InformationTypeID ,
				InformationType ,
				a.InformationSourceID ,
				a.InformationSource ,
				a.CustomerPurposeID ,
				a.CustomerPurpose ,
				a.ProspectDate ,
				a.ProspectDate_YYYYMMDD ,
				a.VechileTypeCode ,
				a.Description ,
				a.CurrVehicleType ,
				a.CurrVehicleBrand ,
				a.Note ,
				a.Keterangan ,
				a.StatusResponseID ,
				a.StatusResponse ,
				a.WebID ,
				a.LastUpdateTime
	   FROM		(
				  SELECT	c.ID AS DNetID ,
							CASE WHEN c.InformationSource <= 10 THEN 'Dealer'
								 WHEN c.InformationSource > 10 THEN 'Rekomendasi MMKSI'
								 ELSE ''
							END AS 'SumberData' ,
							CONVERT(VARCHAR(10), c.CreatedTime, 111) [CreateDate] ,
							CONVERT(VARCHAR(10), c.CreatedTime, 112) [CreateDate_YYYYMMDD] ,
							d.DealerCode ,
							d.DealerName ,
							COALESCE(p.ValueDesc, '-') AS CustomerType ,
							c.CustomerType AS CustomerTypeID ,
							s.SalesmanCode ,
							s.Name ,
							c.CustomerCode ,
							c.CustomerName ,
							c.CustomerAddress ,
							c.Phone ,
							c.Email ,
							COALESCE(q.ValueDesc, '-') AS Sex ,
							c.Sex AS SexID ,
							COALESCE(t.ValueDesc, '-') AS AgeSegment ,
							c.AgeSegment AS AgeSegmentID ,
							COALESCE(u.ValueDesc, '-') AS CustomerStatus ,
							c.Status AS CustomerStatusID ,
							COALESCE(w.ValueDesc, '-') AS InformationType ,
							c.InformationType AS InformationTypeID ,
							COALESCE(x.ValueDesc, '-') AS InformationSource ,
							c.InformationSource AS InformationSourceID ,
							COALESCE(y.ValueDesc, '-') AS CustomerPurpose ,
							c.CustomerPurpose AS CustomerPurposeID ,
							CONVERT(VARCHAR(10), c.ProspectDate, 111) [ProspectDate] ,
							CONVERT(VARCHAR(10), c.ProspectDate, 112) [ProspectDate_YYYYMMDD] ,
							v.VechileTypeCode ,
							v.Description ,
							c.CurrVehicleBrand ,
							c.CurrVehicleType ,
							c.Note ,
							ISNULL(r.Description, '') Keterangan ,
							COALESCE(z.ValueDesc, '-') AS StatusResponse ,
							r.Status AS StatusResponseID ,
							c.WebID AS WebID ,
							c.LastUpdateTime
				  FROM		SAPCustomer c WITH ( NOLOCK )
				  LEFT JOIN Dealer d WITH ( NOLOCK ) ON c.DealerID = d.ID
				  LEFT JOIN SalesmanHeader s WITH ( NOLOCK ) ON s.ID = c.SalesmanHeaderID
				  LEFT JOIN VechileType v WITH ( NOLOCK ) ON v.ID = c.VechileTypeID
				  LEFT JOIN V_LeadCustomerResponse r WITH ( NOLOCK ) ON r.SAPCustomerID = c.ID
																		AND r.DealerID = c.DealerID
				  LEFT JOIN StandardCode z WITH ( NOLOCK ) ON z.Category = 'EnumSAPCustomerResponse.SAPCustomerResponse'
															  AND z.RowStatus = 0
															  AND r.Status = z.ValueId
				  LEFT JOIN StandardCode y WITH ( NOLOCK ) ON y.Category = 'EnumCustomerPurpose.CustomerPurpose'
															  AND y.RowStatus = 0
															  AND c.CustomerPurpose = y.ValueId
				  LEFT JOIN StandardCode x WITH ( NOLOCK ) ON x.Category = 'EnumInformationSource.InformationSource'
															  AND x.RowStatus = 0
															  AND c.InformationSource = x.ValueId
				  LEFT JOIN StandardCode w WITH ( NOLOCK ) ON w.Category = 'EnumInformationSource.InformationType'
															  AND w.RowStatus = 0
															  AND c.InformationType = w.ValueId
				  LEFT JOIN StandardCode u WITH ( NOLOCK ) ON u.Category = 'EnumSAPCustomerStatus.SAPCustomerStatus'
															  AND u.RowStatus = 0
															  AND c.Status = u.ValueId
				  LEFT JOIN StandardCode t WITH ( NOLOCK ) ON t.Category = 'EnumAgeSegment.AgeSegment'
															  AND t.RowStatus = 0
															  AND c.AgeSegment = t.ValueId
				  LEFT JOIN StandardCode q WITH ( NOLOCK ) ON q.Category = 'EnumGender.Gender'
															  AND q.RowStatus = 0
															  AND c.Sex = q.ValueId
				  LEFT JOIN StandardCode p WITH ( NOLOCK ) ON p.Category = 'EnumTipePelanggan'
															  AND p.RowStatus = 0
															  AND c.CustomerType = p.ValueId
				  WHERE		c.SalesforceID <> ''
							AND c.SalesmanHeaderID IS NULL
				) a
 
GO

--========================================================================================================================                    
-- Created By: Mitrais (Prins Carl S)                    
-- View for Karoseri                  
--========================================================================================================================                    
CREATE VIEW VWI_Leasing
AS
	   SELECT	T0.LeasingCode ,
				T0.LeasingName ,
				CASE WHEN T0.RowStatus = -1 THEN T0.RowStatus
					 ELSE CASE WHEN T0.Status = 1 THEN 0
							   ELSE -1
						  END
				END AS Status ,
				T0.LastUpdateTime
	   FROM		Leasing AS T0 WITH ( NOLOCK )
GO

--========================================================================================================================                    
-- Created By: Mitrais (Prins Carl S)                    
-- Vehicle Information                    
--========================================================================================================================                    

CREATE VIEW VWI_MSPMembership
AS
	   SELECT	a.MSPCustomerID ,
				a.DealerId ,
				f.DealerCode ,
				f.DealerName ,
				a.ChassisMasterID ,
				a.MSPCode ,
				c.ChassisNumber ,
				d.ColorCode ,
				VehicleTypeCode = g.VechileTypeCode ,
				VehicleTypeDesc = g.Description ,
				b.MSPKm ,
				b.Duration ,
				b.MSPDesc AS Description ,     
       --ValidUntil = DATEADD(year, b.Duration, e.TglPKT),    
				ValidUntil = DATEADD(YEAR, b.Duration, h.OpenFakturDate) ,
				b.RegistrationDate ,
				a.LastUpdateTime
	   FROM		MSPRegistration a WITH ( NOLOCK )
	   LEFT JOIN (
				   SELECT	a.MSPRegistrationID ,
							a.VehicleTypeID ,
							a.MSPTypeID ,
							b.MSPMasterID ,
							c.MSPKm ,
							c.Duration ,
							b.RegistrationDate ,
							a.MSPDesc
				   FROM		(
							  SELECT	a.MSPRegistrationID ,
										a.VehicleTypeID ,
										d.ID AS MSPTypeID ,
										d.Description AS MSPDesc
							  FROM		(
										  SELECT	a.MSPRegistrationID ,
													b.VehicleTypeID ,
													MAX(c.Sequence) AS MSTypeSequence
										  FROM		MSPRegistrationHistory a WITH ( NOLOCK )
										  LEFT JOIN MSPMaster b WITH ( NOLOCK ) ON a.MSPMasterID = b.ID
																				   AND b.RowStatus = 0
										  LEFT JOIN MSPType c WITH ( NOLOCK ) ON b.MSPTypeID = c.ID
																				 AND c.RowStatus = 0
										  WHERE		a.RowStatus = 0
													AND a.Status = 6
										  GROUP BY	a.MSPRegistrationID ,
													b.VehicleTypeID
										) a
							  LEFT JOIN MSPRegistrationHistory b WITH ( NOLOCK ) ON a.MSPRegistrationID = b.MSPRegistrationID
																					AND b.RowStatus = 0
							  LEFT JOIN MSPMaster c WITH ( NOLOCK ) ON b.MSPMasterID = c.ID
																	   AND c.RowStatus = 0
							  JOIN		MSPType d WITH ( NOLOCK ) ON c.MSPTypeID = d.ID
																	 AND c.RowStatus = 0
																	 AND d.Sequence = a.MSTypeSequence
							) a
				   LEFT JOIN MSPRegistrationHistory b WITH ( NOLOCK ) ON a.MSPRegistrationID = b.MSPRegistrationID
																		 AND b.RowStatus = 0
				   JOIN		MSPMaster c WITH ( NOLOCK ) ON b.MSPMasterID = c.ID
														   AND c.VehicleTypeID = a.VehicleTypeID
														   AND c.MSPTypeID = a.MSPTypeID
														   AND c.RowStatus = 0
				 ) b ON a.ID = b.MSPRegistrationID
	   JOIN		ChassisMaster c WITH ( NOLOCK ) ON a.ChassisMasterID = c.ID
												   AND c.RowStatus = 0
												   AND c.FakturStatus = 4
	   LEFT JOIN VechileColor d WITH ( NOLOCK ) ON c.VechileColorID = d.ID
												   AND d.RowStatus = 0
	   LEFT JOIN ChassisMasterPKT e WITH ( NOLOCK ) ON e.ChassisMasterID = a.ChassisMasterID
													   AND e.RowStatus = 0
	   LEFT JOIN Dealer f WITH ( NOLOCK ) ON a.DealerID = f.ID
											 AND f.RowStatus = 0
	   LEFT JOIN VechileType g WITH ( NOLOCK ) ON d.VechileTypeID = g.ID
												  AND g.RowStatus = 0
	   JOIN		EndCustomer h WITH ( NOLOCK ) ON c.EndCustomerID = h.ID
												 AND h.RowStatus = 0

 
GO

--========================================================================================================================                  
-- Created By: Mitrais (Prins Carl S)                  
-- View For Open Facture              
--========================================================================================================================   
CREATE VIEW VWI_OpenFaktur
AS
	   SELECT	T0.SPKHeaderID AS ID ,
				CASE WHEN T3.FakturStatus = 4 THEN T1.FakturDate
					 ELSE NULL
				END AS OpenFakturDate ,
				T1.LastUpdateTime ,
				T2.DealerID AS SoldDealerID ,
				T4.DealerCode ,
				T3.ChassisNumber
	   FROM		SPKFaktur AS T0 WITH ( NOLOCK )
	   LEFT JOIN EndCustomer AS T1 WITH ( NOLOCK ) ON T0.EndCustomerID = T1.ID
													  AND T1.RowStatus = 0
	   LEFT JOIN ChassisMaster AS T3 WITH ( NOLOCK ) ON T3.EndCustomerID = T1.ID
														AND T3.RowStatus = 0
	   LEFT JOIN SPKHeader AS T2 WITH ( NOLOCK ) ON T0.SPKHeaderID = T2.ID
													AND T2.RowStatus = 0
	   LEFT JOIN Dealer AS T4 WITH ( NOLOCK ) ON T4.ID = T2.DealerID
												 AND T4.RowStatus = 0  --T4.id = t3.SoldDealerID   
	   WHERE	T0.RowStatus = 0
	   UNION ALL
	   SELECT	T1.SPKHeaderID ,
				CASE WHEN T2.FakturStatus = 4 THEN T3.FakturDate
					 ELSE NULL
				END AS OpenFakturDate ,
				T3.LastUpdateTime ,
				T1.DealerID AS SoldDealerID ,
				T4.DealerCode ,
				T2.ChassisNumber
	   FROM		(
				  SELECT DISTINCT
							ChassisMasterID ,
							MAX(EndCustomerID) AS EndCustomerId
				  FROM		RevisionFaktur WITH ( NOLOCK )
				  WHERE		RowStatus = 0
							AND RevisionStatus = 4
				  GROUP BY	ChassisMasterID
				) T0
	   CROSS APPLY FNI_GetLatestSPKForChassis(T0.ChassisMasterID) T1
	   LEFT JOIN ChassisMaster T2 WITH ( NOLOCK ) ON T1.ChassisMasterID = T2.ID
													 AND T2.RowStatus = 0
	   LEFT JOIN EndCustomer T3 WITH ( NOLOCK ) ON T0.EndCustomerId = T3.ID
												   AND T3.RowStatus = 0
	   LEFT JOIN Dealer T4 WITH ( NOLOCK ) ON T1.DealerId = T4.ID
											  AND T4.RowStatus = 0

 
GO

--========================================================================================================================                
-- Created By: Mitrais (Prins Carl S)                
-- Get View For PartShop            
--========================================================================================================================   
CREATE VIEW VWI_PartShop
AS
	   SELECT	a.ID , --a.DealerID,   
				b.DealerCode ,
				c.CityCode ,
				a.PartShopCode ,
				a.OldPartShopCode ,
				a.Name ,
				a.Address ,
				a.Phone ,
				a.Fax ,
				a.Email ,
				Status = CASE WHEN a.RowStatus = -1 THEN a.RowStatus
							  ELSE CASE	WHEN a.Status <> 2 THEN -1
										ELSE 0
								   END
						 END ,
				a.LastUpdateTime
	   FROM		PartShop a WITH ( NOLOCK )
	   JOIN		Dealer b WITH ( NOLOCK ) ON a.DealerID = b.ID
											AND b.RowStatus = 0
	   JOIN		City c WITH ( NOLOCK ) ON a.CityID = c.ID
										  AND c.RowStatus = 0
GO

--========================================================================================================================                
-- Created By: Mitrais (Prins Carl S)                
-- PO DEaler Result                
--========================================================================================================================                

CREATE VIEW VWI_PODealer
AS
	   SELECT --ROW_NUMBER() OVER (ORDER BY POHeaderId, SONumber, VehicleColorCode, VehicleTypeCode) AS ID,            
				a.POHeaderId ,
				a.DealerID ,
				a.DealerCode ,
				a.DealerName ,
				a.PONumber ,
				a.POType ,
				a.NumOfInstallment ,
				a.AllocQty ,
				a.Price ,
				a.Discount ,
				a.Interest ,
				a.ContractNumber ,
				a.PKNumber ,
				a.DealerPKNumber ,
				a.ProjectName ,
				a.SalesOrderId ,
				a.SONumber ,
				a.SODate ,
				a.PaymentRef ,
				a.SOType ,
				a.TermOfPaymentCode ,
				a.TOPDescription ,
				a.DueDate ,
				a.LastUpdateTime ,
				a.VehicleColorCode ,
				a.VehicleTypeCode ,
				a.MaterialNumber ,
				a.MaterialDescription ,
				a.BasePrice ,
				a.OptionPrice ,
				a.DiscountBeforeTax ,
				a.NetPrice ,
				a.TotalHarga ,
				a.PPN ,
				a.TotalHargaPPN ,
				a.TotalHargaPP ,
				a.TotalHargaLC ,
				a.TotalDeposit ,
				a.TotalInterest ,
				a.SPLNumber
	   FROM		(
				  SELECT DISTINCT
							z.POHeaderId ,
							z.DealerID ,
							z.DealerCode ,
							z.DealerName ,
							z.PONumber ,
							z.POType ,
							z.NumOfInstallment ,
							z.AllocQty ,
							z.Price ,
							z.Discount ,
							z.Interest ,
							z.ContractNumber ,
							z.PKNumber ,
							z.DealerPKNumber ,
							z.ProjectName ,
							z.SalesOrderId ,
							z.SONumber ,
							z.SODate ,
							z.PaymentRef ,
							z.SOType ,
							z.TermOfPaymentCode ,
							z.TOPDescription ,
							z.DueDate ,
							z.LastUpdateTime ,           
     --z.VehicleColorID,               
							z.VehicleColorCode ,
							z.VehicleTypeCode ,
							z.MaterialNumber ,
							z.MaterialDescription ,
							z.BasePrice ,
							z.OptionPrice ,
							z.DiscountBeforeTax ,
							z.NetPrice ,
							z.TotalHarga ,
							PPN = FLOOR(z.PPN * 0.01 * z.TotalHarga) ,
							TotalHargaPPN = FLOOR(z.TotalHarga + FLOOR(z.PPN * 0.01 * z.TotalHarga)) ,
							TotalHargaPP = FLOOR(z.PPh22V_Price * 0.01 * z.TotalHarga) ,
							TotalHargaLC = COALESCE(FLOOR(z.TotalHargaLC), 0) ,
							z.TotalDeposit ,
							z.TotalInterest ,
							z.SPLNumber
				  FROM		(
							  SELECT	POHeaderId = a.ID ,
										a.PONumber ,
										a.POType ,
										NumOfInstallment = COALESCE(k.NumOfInstallment, 0) ,
										a.DealerID ,
										g.DealerCode ,
										g.DealerName ,
										k.SPLNumber ,
										a.TermOfPaymentID ,
										h.TermOfPaymentCode ,
										TOPDescription = h.Description ,
										i.DueDate ,
										a.Status ,
										a.FreePPh22Indicator ,
										b.ReqQty ,
										PPh22 = CASE WHEN a.IsFactoring = 1 THEN b.PPh22
													 ELSE e.PPh22
												END ,
										b.AllocQty ,
										b.Price ,
										b.Discount ,
										b.Interest ,
										c.ContractNumber ,
										c.PKNumber ,
										c.DealerPKNumber ,
										c.ProjectName ,
										SalesOrderId = d.ID ,
										d.SONumber ,
										d.SODate ,
										d.PaymentRef ,
										d.SOType ,
										d.LastUpdateTime ,              
         --e.VehicleColorID,              
										VehicleColorCode = j.ColorCode ,
										VehicleTypeCode = LEFT(RTRIM(LTRIM(j.MaterialNumber)), 4) ,
										j.MaterialNumber ,
										j.MaterialDescription ,
										f.BasePrice ,
										f.OptionPrice ,
										PPN = f.PPN ,
										PPh22V_Price = f.PPh22 ,
										DiscountBeforeTax = b.Discount / 1.1 ,
										NetPrice = f.BasePrice - ( b.Discount / 1.1 ) ,
										TotalHarga = ( CASE	WHEN a.Status IN ( 0, 1, 2, 3 )
															THEN CASE WHEN a.IsFactoring = 1
																	  THEN b.ReqQty * ( f.BasePrice - ( b.Discount / 1.1 ) )
																	  ELSE b.ReqQty * ( f.BasePrice - ( b.Discount / 1.1 ) )
																 END
															WHEN a.Status = 5 THEN 0
															ELSE CASE WHEN a.IsFactoring = 1
																	  THEN b.AllocQty * ( f.BasePrice - ( b.Discount
																										/ 1.1 ) )
																	  ELSE b.AllocQty * ( f.BasePrice - ( b.Discount
																										/ 1.1 ) )
																 END
													   END ) ,
										TotalDeposit = ( CASE WHEN a.Status IN ( 0, 1, 2, 3 )
															  THEN CASE	WHEN a.IsFactoring = 1
																		THEN b.ReqQty * f.OptionPrice
																		ELSE b.ReqQty * f.OptionPrice
																   END
															  WHEN a.Status = 5 THEN 0
															  ELSE CASE	WHEN a.IsFactoring = 1
																		THEN b.AllocQty * f.OptionPrice
																		ELSE b.AllocQty * f.OptionPrice
																   END
														 END ) ,
										TotalHargaLC = ( CASE WHEN a.Status IN ( 0, 1, 2, 3 )
															  THEN CASE	WHEN a.IsFactoring = 1
																		THEN b.ReqQty * b.LogisticCost
																		ELSE b.ReqQty * b.LogisticCost
																   END
															  WHEN a.Status = 5 THEN 0
															  ELSE CASE	WHEN a.IsFactoring = 1
																		THEN b.AllocQty * b.LogisticCost
																		ELSE b.AllocQty * b.LogisticCost
																   END
														 END ) ,
										TotalInterest = ( CASE WHEN a.Status IN ( 0, 1, 2, 3 )
															   THEN CASE WHEN a.IsFactoring = 1
																		 THEN b.ReqQty * b.Interest
																		 ELSE b.ReqQty * b.Interest
																	END
															   WHEN a.Status = 5 THEN 0
															   ELSE CASE WHEN a.IsFactoring = 1
																		 THEN b.AllocQty * b.Interest
																		 ELSE b.AllocQty * b.Interest
																	END
														  END )
							  FROM		POHeader a WITH ( NOLOCK )
							  LEFT JOIN PODetail b WITH ( NOLOCK ) ON a.ID = b.POHeaderID
																	  AND b.RowStatus = 0
							  LEFT JOIN ContractHeader c WITH ( NOLOCK ) ON a.ContractHeaderID = c.ID
																			AND c.RowStatus = 0
							  LEFT JOIN SalesOrder d WITH ( NOLOCK ) ON a.ID = d.POHeaderID
																		AND d.RowStatus = 0
							  LEFT JOIN ContractDetail e WITH ( NOLOCK ) ON c.ID = e.ContractHeaderID
																			AND b.ContractDetailID = e.ID
																			AND e.RowStatus = 0
							  LEFT JOIN Dealer g WITH ( NOLOCK ) ON a.DealerID = g.ID
																	AND g.RowStatus = 0
							  LEFT JOIN TermOfPayment h WITH ( NOLOCK ) ON a.TermOfPaymentID = h.ID
																		   AND h.RowStatus = 0
							  LEFT JOIN (
										  SELECT	SalesOrderID ,
													PaymentPurposeID ,
													MAX(DueDate) AS DueDate
										  FROM		SalesOrderDueDate WITH ( NOLOCK )
										  WHERE		PaymentPurposeID = 3
													AND RowStatus = 0
										  GROUP BY	SalesOrderID ,
													PaymentPurposeID
										) i ON d.ID = i.SalesOrderID
							  LEFT JOIN SPL k WITH ( NOLOCK ) ON k.ID = a.SPLID
																 AND k.RowStatus = 0
							  LEFT JOIN VechileColor j WITH ( NOLOCK ) ON j.ID = e.VehicleColorID
																		  AND j.RowStatus = 0
							  CROSS APPLY fni_RetrievePriceListActive(CAST(c.ContractPeriodYear AS VARCHAR) + '-'
																	  + RIGHT('00'
																			  + CAST(c.ContractPeriodMonth AS VARCHAR),
																			  2) + '-' + RIGHT('00'
																							   + CAST(c.ContractPeriodDay AS VARCHAR),
																							   2), e.VehicleColorID,
																	  a.DealerID) f
							  WHERE		a.RowStatus = 0
										AND d.ID IS NOT NULL
										AND b.AllocQty > 0
							) z
				) a 

 
GO
 

--========================================================================================================================                  
-- Created By: Mitrais (Prins Carl S)                  
-- View PODOExpedition              
--========================================================================================================================  
CREATE VIEW VWI_PODOExpedition
AS
	   SELECT	DONumber ,
				ExpeditionNo
	   FROM		(
				  SELECT	a.DONumber ,
							STUFF((
									SELECT	'; ' + b.ExpeditionNo
									FROM	(
											  SELECT DISTINCT
														a.DONumber ,
														a.DoDate ,
														d.ExpeditionName ,
														d.ExpeditionNo
											  FROM		SparePartDO a WITH ( NOLOCK )
											  JOIN		SparePartPackingDetail b WITH ( NOLOCK ) ON a.ID = b.SparePartDOID
																									AND b.RowStatus = 0
											  JOIN		SparePartPacking c WITH ( NOLOCK ) ON b.SparePartPackingID = c.ID
																							  AND c.RowStatus = 0
											  JOIN		SparePartDOExpedition d WITH ( NOLOCK ) ON c.SparePartDOExpeditionID = d.ID
																								   AND d.RowStatus = 0
											  WHERE		YEAR(a.DoDate) >= YEAR(GETDATE()) - 1
											) b
									WHERE	a.DONumber = b.DONumber
								  FOR
									XML	PATH('')
								  ), 1, 1, '') [ExpeditionNo]
				  FROM		SparePartDO a WITH ( NOLOCK )
				  WHERE		YEAR(a.DoDate) >= YEAR(GETDATE()) - 1
							AND a.RowStatus = 0
				  GROUP BY	a.DONumber
				) a
GO

--========================================================================================================================                  
-- Created By: Mitrais (Prins Carl S)                  
-- View For PO DP Have Billing               
--========================================================================================================================   
CREATE VIEW VWI_PODOHaveBilling
AS
	   SELECT	ROW_NUMBER() OVER ( ORDER BY a.SparePartDOID ) AS ID ,
--SELECT ROW_NUMBER() OVER (ORDER BY (SELECT 1)) AS ID, 
				a.SparePartDOID ,
				a.DONumber ,
				a.DealerID ,
				a.DealerCode ,
				a.DoDate ,
				a.BillingDate ,
				a.BillingNumber ,
				a.ExpeditionNo ,
				a.TermOfPaymentCode ,
				a.TermOfPaymentValue ,
				a.TermOfPaymentDesc ,
				a.DueDate ,
				a.LastUpdateTime
	   FROM		(
				  SELECT DISTINCT
							SparePartDOID = d.ID ,
							d.DONumber ,
							a.DealerID ,
							h.DealerCode ,
							d.DoDate ,
							a.BillingDate ,
							a.BillingNumber ,
							n.ExpeditionNo ,
							m.TermOfPaymentCode ,
							m.TermOfPaymentValue ,
							TermOfPaymentDesc = m.Description ,
							j.DueDate ,
							a.LastUpdateTime
				  FROM		SparePartBilling a WITH ( NOLOCK )
				  JOIN		SparePartBillingDetail b WITH ( NOLOCK ) ON a.ID = b.SparePartBillingID
																		AND b.RowStatus = 0
				  JOIN		SparePartDODetail c WITH ( NOLOCK ) ON b.SparePartDODetailID = c.ID
																   AND c.RowStatus = 0
				  JOIN		SparePartDO d WITH ( NOLOCK ) ON c.SparePartDOID = d.ID
															 AND d.RowStatus = 0
															 AND a.DealerID = d.DealerID
				  JOIN		Dealer h WITH ( NOLOCK ) ON a.DealerID = h.ID
														AND h.RowStatus = 0
	--left join VWI_POEstimateHaveBilling i WITH (NOLOCK) on c.SparePartPOEstimateID = i.SparePartPOEstimateID-- and i.RowStatus = 0
				  LEFT JOIN TOPSPDueDate j WITH ( NOLOCK ) ON a.ID = j.SparePartBillingID
															  AND j.RowStatus = 0
				  LEFT JOIN SparePartPOEstimate k WITH ( NOLOCK ) ON c.SparePartPOEstimateID = k.ID
																	 AND k.RowStatus = 0
				  LEFT JOIN SparePartPO l WITH ( NOLOCK ) ON k.SparePartPOID = l.ID
															 AND l.RowStatus = 0
				  LEFT JOIN TermOfPayment m WITH ( NOLOCK ) ON l.TermOfPaymentID = m.ID
															   AND m.RowStatus = 0
				  LEFT JOIN VWI_PODOExpedition n WITH ( NOLOCK ) ON d.DONumber = n.DONumber
				  WHERE		a.RowStatus = 0
							AND YEAR(a.BillingDate) >= YEAR(GETDATE()) - 1
				) a
GO

--========================================================================================================================                  
-- Created By: Mitrais (Prins Carl S)                  
-- View POEstimateHaveBilling                  
--========================================================================================================================    
CREATE VIEW VWI_POEstimateHaveBilling
AS
	   SELECT	a.SparePartPOEstimateID ,
				a.SparePartPOID ,
				a.SONumber ,
				a.SODate ,
				a.PONumber ,
				a.DMSPRNo ,
				a.DealerID ,
				a.DealerCode ,
	   --a.DeliveryDate, 
				a.DocumentType ,
				a.LastUpdateTime ,
				a.TermOfPaymentCode ,
				a.TermOfPaymentValue ,
				a.TermOfPaymentDesc
	   FROM		(
				  SELECT DISTINCT
							a.SparePartPOEstimateID ,
							a.SparePartPOID ,
							a.SONumber ,
							a.SODate ,
							PONumber = COALESCE(COALESCE(l.EstimationNumber, i.RequestNo), e.PONumber) ,
							DMSPRNo = COALESCE(COALESCE(l.DMSPRNo, i.DMSPRNo), e.DMSPRNo) ,
							e.DealerID ,
							m.DealerCode ,
       --a.DeliveryDate, 
							a.DocumentType ,
							a.LastUpdateTime ,
							n.TermOfPaymentCode ,
							n.TermOfPaymentValue ,
							n.Description AS TermOfPaymentDesc
				  FROM		(
							  SELECT	SparePartPOEstimateID = d.ID ,
										d.SparePartPOID ,
										d.SONumber ,
										d.SODate ,
										d.DocumentType ,
										MAX(a.LastUpdateTime) AS LastUpdateTime
							  FROM		SparePartBilling a WITH ( NOLOCK )
							  JOIN		SparePartBillingDetail b WITH ( NOLOCK ) ON a.ID = b.SparePartBillingID
																					AND b.RowStatus = 0
							  JOIN		SparePartDODetail c WITH ( NOLOCK ) ON b.SparePartDODetailID = c.ID
																			   AND c.RowStatus = 0
							  JOIN		SparePartPOEstimate d WITH ( NOLOCK ) ON c.SparePartPOEstimateID = d.ID
																				 AND d.RowStatus = 0
							  JOIN		SparePartPOEstimateDetail z WITH ( NOLOCK ) ON d.ID = z.SparePartPOEstimateID
																					   AND z.RowStatus = 0
							  WHERE		a.RowStatus = 0
										AND YEAR(a.BillingDate) >= YEAR(GETDATE()) - 1
							  GROUP BY	d.ID ,
										d.SparePartPOID ,
										d.SONumber ,
										d.SODate ,
										d.DocumentType
							) a
				  LEFT JOIN SparePartPO e WITH ( NOLOCK ) ON e.ID = a.SparePartPOID
															 AND e.RowStatus = 0
				  LEFT JOIN SparePartPODetail f WITH ( NOLOCK ) ON e.ID = f.SparePartPOID
																   AND f.RowStatus = 0
				  LEFT JOIN IndentPartPO g WITH ( NOLOCK ) ON f.ID = g.SparePartPODetailID
															  AND g.RowStatus = 0
				  LEFT JOIN IndentPartDetail h WITH ( NOLOCK ) ON h.ID = g.IndentPartDetailID
																  AND h.RowStatus = 0
				  LEFT JOIN IndentPartHeader i WITH ( NOLOCK ) ON i.ID = h.IndentPartHeaderID
																  AND i.RowStatus = 0
				  LEFT JOIN EstimationEquipPO j WITH ( NOLOCK ) ON j.IndentPartDetailID = h.ID
																   AND j.RowStatus = 0
				  LEFT JOIN EstimationEquipDetail k WITH ( NOLOCK ) ON k.ID = j.EstimationEquipDetailID
																	   AND k.RowStatus = 0
				  LEFT JOIN EstimationEquipHeader l WITH ( NOLOCK ) ON l.ID = k.EstimationEquipHeaderID
																	   AND l.RowStatus = 0
				  LEFT JOIN Dealer m WITH ( NOLOCK ) ON e.DealerID = m.ID
														AND m.RowStatus = 0
				  LEFT JOIN TermOfPayment n WITH ( NOLOCK ) ON e.TermOfPaymentID = n.ID
															   AND n.RowStatus = 0
				) a


--For Production
/*
select distinct SparePartPOEstimateID = d.ID, d.SparePartPOID, d.SONumber, d.SODate, 
       PONumber=coalesce(coalesce(l.EstimationNumber,i.RequestNo),e.PONumber), 
	   DMSPRNo = coalesce(coalesce(l.DMSPRNo, i.DMSPRNo), e.DMSPRNo),
	   e.DealerID, m.DealerCode,
       a.DeliveryDate, 
	   d.DocumentType, a.LastUpdateTime, n.TermOfPaymentCode, n.TermOfPaymentValue, n.Description as TermOfPaymentDesc
from SparePartPOStatus a with (nolock)
join SparePartPOEstimate d with (nolock) on a.SONumber = d.SONumber and d.RowStatus = 0
join SparePartPOEstimateDetail z with (nolock) on d.ID = z.SparePartPOEstimateID and z.RowStatus = 0
left join SparePartPO e with (nolock) on e.ID = d.SparePartPOID and e.RowStatus = 0
left join SparePartPODetail f with (nolock) on e.ID = f.SparePartPOID and f.RowStatus = 0
left join IndentPartPO g with (nolock) on f.ID = g.SparePartPODetailID and g.RowStatus = 0
left join IndentPartDetail h with (nolock) on h.ID = g.IndentPartDetailID and h.RowStatus = 0
left join IndentPartHeader i with (nolock) on i.ID = h.IndentPartHeaderID and i.RowStatus = 0
left join EstimationEquipPO j with (nolock) on j.IndentPartDetailID = h.ID and j.RowStatus = 0
left join EstimationEquipDetail k with (nolock) on k.ID = j.EstimationEquipDetailID and k.RowStatus = 0
left join EstimationEquipHeader l with (nolock) on l.ID = k.EstimationEquipHeaderID and l.RowStatus = 0
left join Dealer m with (nolock) on e.DealerID = m.ID and m.RowStatus = 0
left join TermOfPayment n with (nolock) on e.TermOfPaymentID = n.ID and n.RowStatus = 0
where a.RowStatus = 0 and year(a.BillingDate) >= year(GETDATE()) -1 and a.BillingNumber is not null and a.BillingNumber <> ''
*/
 
GO

--========================================================================================================================                
-- Created By: Mitrais (Prins Carl S)                
-- PR History Part 2            
--========================================================================================================================   
CREATE VIEW VWI_PRHistoryDO
AS
	   SELECT	a.DealerID ,
				a.DealerCode ,
				a.DealerName ,
				a.PONumber ,
				a.PODate ,
				a.DMSPRNo ,
				a.OrderType ,
				a.SONumber ,
				a.NomorDO ,
				a.TanggalDO ,
				a.SparePartMasterID ,
				a.PartNumber ,
				a.PartName ,
				a.Qty ,
				a.EstimasiTanggalPengiriman ,
				a.PickingDate ,
				a.PackingDate ,
				a.GoodIssueDate ,
				a.PaymentDate ,
				a.ReadyForDeliveryDate ,
				a.ExpeditionNo ,
				a.ExpeditionName ,
				a.ATD ,
				a.ETA
	   FROM		(
				  SELECT DISTINCT
							a.DealerID ,
							e.DealerCode ,
							e.DealerName ,
							COALESCE(p.EstimationNumber, m.RequestNo, h.PONumber, '') AS PONumber ,
							h.PODate ,
							COALESCE(p.DMSPRNo, m.DMSPRNo, h.DMSPRNo, '') AS DMSPRNo ,
							h.OrderType ,
							ISNULL(g.SONumber, '') AS SONumber ,
							ISNULL(a.DONumber, '') AS NomorDO ,
							TanggalDO = a.DoDate ,
							f.SparePartMasterID ,
							ISNULL(q.PartNumber, '') AS PartNumber ,
							ISNULL(q.PartName, '') AS PartName ,
							ISNULL(f.Qty, 0) AS Qty ,
							EstimasiTanggalPengiriman = a.EstmationDeliveryDate ,
							a.PickingDate ,
							a.PackingDate ,
							a.GoodIssueDate ,
							a.PaymentDate ,
							a.ReadyForDeliveryDate ,
							ISNULL(d.ExpeditionNo, '') AS ExpeditionNo ,
							ISNULL(d.ExpeditionName, '') AS ExpeditionName ,
							d.ATD ,
							d.ETA
				  FROM		SparePartDO a WITH ( NOLOCK )
				  LEFT JOIN SparePartPackingDetail b WITH ( NOLOCK ) ON b.SparePartDOID = a.ID
																		AND b.RowStatus = 0
				  LEFT JOIN SparePartPacking c WITH ( NOLOCK ) ON c.ID = b.SparePartPackingID
																  AND c.RowStatus = 0
				  LEFT JOIN SparePartDOExpedition d WITH ( NOLOCK ) ON c.SparePartDOExpeditionID = d.ID
																	   AND d.RowStatus = 0
				  LEFT JOIN Dealer e WITH ( NOLOCK ) ON e.ID = a.DealerID
														AND e.RowStatus = 0
				  LEFT JOIN SparePartDODetail f WITH ( NOLOCK ) ON f.SparePartDOID = a.ID
																   AND f.RowStatus = 0
				  JOIN		SparePartPOEstimate g WITH ( NOLOCK ) ON f.SparePartPOEstimateID = g.ID
																	 AND g.RowStatus = 0
				  LEFT JOIN SparePartPO h WITH ( NOLOCK ) ON g.SparePartPOID = h.ID
				  LEFT JOIN SparePartPODetail i WITH ( NOLOCK ) ON g.ID = i.SparePartPOID
																   AND i.RowStatus = 0
				  LEFT JOIN IndentPartPO j WITH ( NOLOCK ) ON i.ID = j.SparePartPODetailID
															  AND j.RowStatus = 0
				  LEFT JOIN IndentPartDetail l WITH ( NOLOCK ) ON l.ID = j.IndentPartDetailID
																  AND l.RowStatus = 0
				  LEFT JOIN IndentPartHeader m WITH ( NOLOCK ) ON m.ID = l.IndentPartHeaderID
																  AND m.RowStatus = 0
				  LEFT JOIN EstimationEquipPO n WITH ( NOLOCK ) ON n.IndentPartDetailID = l.ID
																   AND n.RowStatus = 0
				  LEFT JOIN EstimationEquipDetail o WITH ( NOLOCK ) ON o.ID = n.EstimationEquipDetailID
																	   AND o.RowStatus = 0
				  LEFT JOIN EstimationEquipHeader p WITH ( NOLOCK ) ON p.ID = o.EstimationEquipHeaderID
																	   AND p.RowStatus = 0
				  LEFT JOIN SparePartMaster q WITH ( NOLOCK ) ON f.SparePartMasterID = q.ID
																 AND q.RowStatus = 0
				  WHERE		a.RowStatus = 0
				            AND YEAR(a.DoDate) >= YEAR(GETDATE()) - 1
				) a
GO

--========================================================================================================================                  
-- Created By: Mitrais (Prins Carl S)                  
-- PR History Indent Status Cancel               
--========================================================================================================================  
CREATE VIEW VWI_PRHistoryIndentStatusCancel
AS
	  

SELECT	ROW_NUMBER() OVER ( ORDER BY a.DealerID ) AS ID ,
		a.DealerID ,
		a.DealerCode ,
		a.PONumber ,
		a.DMSPRNo ,
		a.LastUpdateTime
FROM	(
		  SELECT DISTINCT
					a.DealerID ,
					a.DealerCode ,
					a.PONumber ,
					a.DMSPRNo ,
					a.LastUpdateTime
		  FROM		(
					  SELECT	a.DealerID ,
								b.DealerCode ,
								PONumber = COALESCE(f.EstimationNumber, a.RequestNo) ,
								DMSPRNo = COALESCE(f.DMSPRNo, a.DMSPRNo) ,
								a.LastUpdateTime
					  FROM		IndentPartHeader a WITH ( NOLOCK )
					  JOIN		Dealer b WITH ( NOLOCK ) ON a.DealerID = b.ID
															AND b.RowStatus = 0
					  JOIN		IndentPartDetail c WITH ( NOLOCK ) ON a.ID = c.IndentPartHeaderID
																	  AND c.RowStatus = 0
					  LEFT JOIN EstimationEquipPO d WITH ( NOLOCK ) ON c.ID = d.IndentPartDetailID
																	   AND d.RowStatus = 0
					  LEFT JOIN EstimationEquipDetail e WITH ( NOLOCK ) ON d.EstimationEquipDetailID = e.ID
																		   AND e.RowStatus = 0
					  LEFT JOIN EstimationEquipHeader f WITH ( NOLOCK ) ON e.EstimationEquipHeaderID = f.ID
																		   AND f.RowStatus = 0
					  WHERE		(
								  a.StatusKTB = 5
								  OR a.StatusKTB = 8
								  OR a.Status = 1
								)
								AND a.RowStatus = 0
					  UNION
					  SELECT	a.DealerID ,
								b.DealerCode ,
								PONumber = a.EstimationNumber ,
								DMSPRNo = a.DMSPRNo ,
								LastUpdateTime = a.LastUpdatedTime
					  FROM		EstimationEquipHeader a WITH ( NOLOCK )
					  JOIN		Dealer b WITH ( NOLOCK ) ON a.DealerID = b.ID
															AND b.RowStatus = 0
					  WHERE		a.Status = 2
								AND a.RowStatus = 0
					) a
		) a
GO

--========================================================================================================================                
-- Created By: Mitrais (Prins Carl S)                
-- PR History Part 1                
--========================================================================================================================   
CREATE VIEW VWI_PRHistorySO
AS
	   SELECT	a.PONumber ,
				a.DealerID ,
				a.DealerCode ,
				a.PODate ,
				a.DMSPRNo ,
				a.OrderType ,
				a.SODate ,
				a.NomorPenjualan ,
				a.DocumentType ,
				a.KodeBarang ,
				a.NamaBarang ,
				a.JumlahPesanan ,
				a.JumlahPemenuhan ,
				a.HargaEceran ,
				a.TotalBaseAmountDetail ,
				a.NomorPengganti ,
				a.Diskon ,
				a.TotalAmountDetail ,
				a.TotalBaseAmountHeader ,
				a.TotalConsumptionTaxAmount ,
				a.TotalAmountHeader ,
				a.Status ,
				a.StatusDesc ,
				a.LastUpdateTime
	   FROM		(
				  SELECT DISTINCT
							a.ID ,
							PONumber = COALESCE(i.EstimationNumber, f.RequestNo, a.PONumber) ,
							a.DealerID ,
							t.DealerCode ,
							a.PODate ,
							COALESCE(i.DMSPRNo, f.DMSPRNo, a.DMSPRNo, '') AS DMSPRNo ,
							a.OrderType ,
							ISNULL(j.SODate, '') AS SODate ,
							ISNULL(j.SONumber, '') AS NomorPenjualan ,
							DocumentType = ISNULL(j.DocumentType, '') ,
							KodeBarang = ISNULL(k.PartNumber, '') ,
							NamaBarang = ISNULL(k.PartName, '') ,
							JumlahPesanan = COALESCE(h.EstimationUnit, e.Qty, b.Quantity) ,
							JumlahPemenuhan = ISNULL(k.AllocQty, 0) ,
							HargaEceran = COALESCE(h.Harga, e.Price, b.RetailPrice) ,
							TotalBaseAmountDetail = 0 ,
							NomorPengganti = ISNULL(k.AltPartNumber, '') ,
							Diskon = ISNULL(k.Discount, 0) ,
							TotalAmountDetail = COALESCE(( k.RetailPrice * k.AllocQty ) - k.Discount, 0) ,
							TotalBaseAmountHeader = 0 ,
							TotalConsumptionTaxAmount = 0 ,
							TotalAmountHeader = 0 ,
							Status = COALESCE(CAST(i.Status AS CHAR), CAST(f.Status AS CHAR), a.ProcessCode) ,
							StatusDesc = COALESCE(n.ValueDesc, m.ValueDesc, l.ValueDesc) ,
							LastUpdateTime = COALESCE(i.LastUpdatedTime, f.LastUpdateTime, a.LastUpdateTime)
				  FROM		SparePartPO a WITH ( NOLOCK )
				  LEFT JOIN SparePartPODetail b WITH ( NOLOCK ) ON a.ID = b.SparePartPOID
																   AND b.RowStatus = 0
				  JOIN		SparePartMaster o WITH ( NOLOCK ) ON b.SparePartMasterID = o.ID
																 AND o.RowStatus = 0
				  LEFT JOIN IndentPartPO c WITH ( NOLOCK ) ON b.ID = c.SparePartPODetailID
															  AND c.RowStatus = 0
				  LEFT JOIN IndentPartDetail e WITH ( NOLOCK ) ON e.ID = c.IndentPartDetailID
																  AND e.RowStatus = 0
				  LEFT JOIN IndentPartHeader f WITH ( NOLOCK ) ON f.ID = e.IndentPartHeaderID
																  AND f.RowStatus = 0
				  LEFT JOIN EstimationEquipPO g WITH ( NOLOCK ) ON g.IndentPartDetailID = e.ID
																   AND g.RowStatus = 0
				  LEFT JOIN EstimationEquipDetail h WITH ( NOLOCK ) ON h.ID = g.EstimationEquipDetailID
																	   AND h.RowStatus = 0
				  LEFT JOIN EstimationEquipHeader i WITH ( NOLOCK ) ON i.ID = h.EstimationEquipHeaderID
																	   AND i.RowStatus = 0
				  LEFT JOIN SparePartPOEstimate j WITH ( NOLOCK ) ON j.SparePartPOID = a.ID
																	 AND j.RowStatus = 0
				  LEFT JOIN SparePartPOEstimateDetail k WITH ( NOLOCK ) ON j.ID = k.SparePartPOEstimateID
																		   AND k.RowStatus = 0
																		   AND o.PartNumber = k.PartNumber
				  LEFT JOIN StandardCodeChar l WITH ( NOLOCK ) ON l.Category = 'SparePartPOStatus'
																  AND l.ValueId = a.ProcessCode
																  AND l.RowStatus = 0
				  LEFT JOIN StandardCode m WITH ( NOLOCK ) ON m.Category = 'EnumIndentPartStatus.IndentPartStatusDealer'
															  AND m.ValueId = f.Status
															  AND m.RowStatus = 0
				  LEFT JOIN StandardCode n WITH ( NOLOCK ) ON n.Category = 'EnumIndertPartEquipStatus.EnumStatusDealer'
															  AND n.ValueId = i.Status
															  AND n.RowStatus = 0
				  LEFT JOIN Dealer t WITH ( NOLOCK ) ON t.ID = a.DealerID
														AND t.RowStatus = 0
				  WHERE		a.RowStatus = 0
							AND YEAR(a.PODate) >= YEAR(GETDATE()) - 1
				 ) a
GO

--========================================================================================================================                    
-- Created By: Mitrais (Prins Carl S)                    
-- Get Profile Detail from Header Code       
--========================================================================================================================                    
CREATE VIEW VWI_ProfileDetailFromHeaderCode
AS
	   SELECT	a.ID ,
				a.Code ,
				a.Description ,
				Status = a.RowStatus ,
				a.LastUpdateTime
	   FROM		ProfileDetail a WITH ( NOLOCK ) 
	   LEFT JOIN ProfileHeader b WITH ( NOLOCK ) ON a.ProfileHeaderID = b.ID
													AND b.RowStatus = 0
GO

CREATE VIEW VWI_Province
AS
	   SELECT	ROW_NUMBER() OVER ( ORDER BY a.ProvinceCode ) AS ID ,
				a.ProvinceCode ,
				a.ProvinceName ,
				a.LastUpdateTime ,
				a.Status
	   FROM		(
				  SELECT DISTINCT
							ProvinceCode AS ProvinceCode ,
							MAX(ProvinceName) AS ProvinceName ,
							MAX(LastUpdateTime) AS LastUpdateTime ,
							MAX(RowStatus) AS Status
				  FROM		[dbo].[Province] WITH ( NOLOCK )
				  WHERE		RowStatus = 0
							AND ProvinceCode <> 'UK'
				  GROUP BY	ProvinceCode
				) a
GO

CREATE VIEW VWI_QuickProduct
AS
	   SELECT --ROW_NUMBER() OVER (ORDER BY a.VehicleType, a.ColorCode, a.Status) AS ID,     
				ROW_NUMBER() OVER ( ORDER BY (
											   SELECT	1
											 ) ) AS ID ,
				a.VehicleType ,
				a.VehicleDesc ,
				a.ProductCategory ,
				a.VehicleCatDesc ,
				a.ColorCode ,
				a.ColorDescription ,
				a.VehicleBrand ,
				a.VehicleModel_S1 ,
				a.VehicleCategory_S2 ,
				a.ProductSegment_S3 ,
				a.DriveSystem_S4 ,
				a.LastUpdateTime ,
				a.Status
	   FROM		(
				  SELECT	a.VechileTypeCode AS VehicleType ,
							a.Description AS VehicleDesc ,
							c.Code AS ProductCategory ,
							d.Description AS VehicleCatDesc ,
							b.ColorCode AS ColorCode ,
							b.ColorIndName AS ColorDescription ,
							'MITSUBISHI' AS VehicleBrand ,
							a.VechileTypeCode AS VehicleModel_S1 ,
							d.CategoryCode AS VehicleCategory_S2 ,     
    --e.VechileModelIndCode AS ProductSegment_S3,    
							e.Description AS ProductSegment_S3 ,    
  --  a.DriveSystemType AS DriveSystem_S4, -- Update Yusak 22 Nov 2018  
							a.SegmentType AS DriveSystem_S4 ,
							a.LastUpdateTime ,
							Status = CASE WHEN a.RowStatus = -1 THEN a.RowStatus
										  ELSE CASE	WHEN b.RowStatus = -1 THEN b.RowStatus
													ELSE CASE WHEN a.Status = 'X' THEN -1
															  ELSE CASE	WHEN b.Status = 'X' THEN -1
																		ELSE CASE WHEN e.RowStatus = -1 THEN e.RowStatus
																				  ELSE 0
																			 END
																   END
														 END
											   END
									 END
				  FROM		VechileType AS a  WITH ( NOLOCK ) 
				  INNER JOIN VechileColor AS b WITH ( NOLOCK )  ON a.ID = b.VechileTypeID
				  INNER JOIN ProductCategory AS c  WITH ( NOLOCK ) ON a.ProductCategoryID = c.ID
				  INNER JOIN Category AS d  WITH ( NOLOCK ) ON a.CategoryID = d.ID
				  INNER JOIN VechileModel AS e WITH ( NOLOCK )  ON a.ModelID = e.ID
				  WHERE		c.ID = 1  --MMKSI Only
 
				 ) a
GO

--========================================================================================================================                    
-- Created By: Mitrais (Prins Carl S)                    
-- View for Chassis recall Service                  
--========================================================================================================================                    
CREATE VIEW VWI_RecallChassisServiced
AS
	   SELECT	a.ID ,
				b.ChassisNumber ,
				a.ServiceDate ,
				c.DealerCode ,
				d.DealerBranchCode ,
				a.WorkOrderNumber ,
				f.RecallRegNo ,
				f.Description ,
				a.LastUpdateTime
	   FROM		RecallService a WITH ( NOLOCK )
	   LEFT JOIN ChassisMaster b WITH ( NOLOCK ) ON a.ChassisMasterID = b.ID
													AND b.RowStatus = 0
	   LEFT JOIN Dealer c WITH ( NOLOCK ) ON a.ServiceDealerID = c.ID
											 AND c.RowStatus = 0
	   LEFT JOIN DealerBranch d WITH ( NOLOCK ) ON c.ID = d.DealerID
												   AND a.DealerBranchId = d.ID
												   AND d.RowStatus = 0
	   LEFT JOIN RecallChassisMaster e WITH ( NOLOCK ) ON a.RecallChassisMasterID = e.ID
														  AND e.RowStatus = 0
	   LEFT JOIN RecallCategory f WITH ( NOLOCK ) ON e.RecallCategoryID = f.ID
													 AND f.RowStatus = 0
	   WHERE	a.RowStatus = 0
GO

--========================================================================================================================                  
-- Created By: Mitrais (Prins Carl S)                  
-- Service History              
--========================================================================================================================  
CREATE VIEW VWI_ServiceHistory
AS
	   SELECT	a.ID ,
				a.ChassisMasterID ,
				a.KodeChassis ,
				d.PKTDate ,
				a.TglBukaTransaksi ,
				a.TglTutupTransaksi ,
				a.WaktuMasuk ,
				a.WaktuKeluar , 
       --a.DealerID, 
				a.DealerCode , 
	   --a.DealerBranchID, 
				a.DealerBranchCode ,
	   --a.WorkOrderCategoryID, 
				e.WorkOrderType ,
				a.WorkOrderCategoryCode ,
				a.KMService ,
				a.NoWorkOrder ,
				a.ServicePlaceCode ,
				a.ServiceTypeCode ,
				a.LastUpdateTime
	   FROM		V_AssistServiceIncomingConfirmation a WITH ( NOLOCK )
	   LEFT JOIN ChassisMasterPKT d WITH ( NOLOCK ) ON a.ChassisMasterID = d.ChassisMasterID
													   AND d.RowStatus = 0
	   LEFT JOIN AssistWorkOrderType e WITH ( NOLOCK ) ON a.WorkOrderCategoryID = e.ID
														  AND e.RowStatus = 0
GO

--========================================================================================================================                    
-- Created By: Mitrais (Prins Carl S)                    
-- View for Service Place                  
--========================================================================================================================                    
CREATE VIEW VWI_ServicePlace
AS
	   SELECT	a.ID ,
				a.ServicePlaceCode ,
				a.Description AS ServicePlaceDescription ,
				CASE WHEN a.RowStatus = -1 THEN a.RowStatus
					 ELSE CASE WHEN a.Status = 1 THEN 0
							   ELSE -1
						  END
				END AS Status ,
				a.LastUpdateTime
	   FROM		AssistServicePlace AS a WITH ( NOLOCK )
GO

--========================================================================================================================                  
-- Created By: Mitrais (Prins Carl S)                  
-- View For Service Template               
--========================================================================================================================   
CREATE VIEW VWI_ServiceTemplate
AS
	   SELECT DISTINCT
				SVCTMPTParentGroup = c.WorkOrderType ,
				SVCTMPTParent = b.WorkOrderCategory ,
				SVCTMPTSubGroup = COALESCE(d.VechileTypeCode, a.VechileTypeCode) ,
				a.SvcTemplateCode ,
				Description = a.SvcTemplateDesc ,
				DNETKind = COALESCE(e.KindCode, f.KindCode) ,
				IntervalKM = COALESCE(e.KM, f.KM) ,
				g.ServiceTemplateActivityDesc ,
				g.Duration ,
				Item = COALESCE(i.PartNumber, h.Description) ,
				ItemDesc = COALESCE(i.PartName, h.Description) ,
				h.Qty ,
				h.Price
	   FROM		ServiceTemplate a WITH ( NOLOCK )
	   JOIN		AssistWorkOrderCategory b WITH ( NOLOCK ) ON a.WorkOrderCategoryID = b.ID
															 AND b.RowStatus = 0
	   JOIN		AssistWorkOrderType c WITH ( NOLOCK ) ON b.WorkOrderTypeID = c.ID
														 AND c.RowStatus = 0
	   JOIN		VechileType d WITH ( NOLOCK ) ON a.VechileTypeID = d.ID
												 AND d.RowStatus = 0
	   LEFT JOIN PMKind e WITH ( NOLOCK ) ON a.PMKindID = e.ID
											 AND e.RowStatus = 0
	   LEFT JOIN FSKind f WITH ( NOLOCK ) ON a.FSKindID = f.ID
											 AND f.RowStatus = 0
	   LEFT JOIN ServiceTemplateActivityHeader g WITH ( NOLOCK ) ON a.ID = g.ServiceTemplateID
																	AND g.RowStatus = 0
	   LEFT JOIN ServiceTemplateActivityDetail h WITH ( NOLOCK ) ON g.ID = h.ServiceTemplateActivityHeaderId
																	AND h.RowStatus = 0
	   LEFT JOIN SparePartMaster i WITH ( NOLOCK ) ON i.ID = h.SparePartMasterID
													  AND i.RowStatus = 0
	   WHERE	a.RowStatus = 0
GO

--========================================================================================================================                    
-- Created By: Mitrais (Prins Carl S)                    
-- View for Service Type                  
--========================================================================================================================                    
CREATE VIEW VWI_ServiceType
AS
	   SELECT	a.ID ,
				a.ServiceTypeCode ,
				a.Description AS ServiceTypeDescription ,
				CASE WHEN a.RowStatus = -1 THEN a.RowStatus
					 ELSE CASE WHEN a.Status = 1 THEN 0
							   ELSE -1
						  END
				END AS Status ,
				a.LastUpdateTime
	   FROM		AssistServiceType AS a WITH ( NOLOCK )
GO

--========================================================================================================================                  
-- Created By: Mitrais (Prins Carl S)                  
-- Get Spare Part Master UOM Konversion               
--========================================================================================================================   
CREATE VIEW VWI_SparePartConversion
AS
	   SELECT	a.ID ,
				a.PartNumber ,
				a.PartName ,
				a.TypeCode ,
				UOMFrom = a.UOM ,
				b.UoMto ,
				b.Qty ,
				a.PartNumberReff ,
				a.ProductType ,
				a.ModelCode  
--select ROW_NUMBER() OVER (ORDER BY a.ID) AS ID, a.PartNumber, a.PartName, a.TypeCode, UOMFrom=a.UOM, b.UoMto, b.Qty, a.PartNumberReff, a.ProductType, a.ModelCode  
				,
				Status = b.RowStatus ,
				a.LastUpdateTime
	   FROM		SparePartMaster a WITH ( NOLOCK )
	   JOIN		SparePartConversion b WITH ( NOLOCK ) ON a.ID = b.SparePartMasterID  
--where a.RowStatus = 0 and a.ActiveStatus = 0 and a.ProductCategoryID in (2, 3)  
	   WHERE	a.RowStatus = 0
				AND a.ActiveStatus = 0
				AND a.ProductCategoryID IN ( 1, 3 )
GO

--========================================================================================================================                  
-- Created By: Mitrais (Prins Carl S)                  
-- View SparePartPayment              
--========================================================================================================================  
CREATE VIEW VWI_SparePartPayment
AS
	   SELECT	a.ReferenceNo ,
				a.InvoiceNo ,
				a.PostingDate ,
				a.DealerID ,
				a.DealerCode ,
				a.SONumber ,
				a.Amount ,
				BillingAmount = a.TotalPrice + a.TotalTax ,
				a.LastUpdateTime ,
				0 AS isTOP
	   FROM		(
				  SELECT	a.ReferenceNo ,
							a.InvoiceNo ,
							a.PostingDate ,
							a.DealerID ,
							a.DealerCode ,
							SUM(a.TotalPrice) AS TotalPrice ,
							SUM(a.Tax) AS TotalTax ,
							a.SONumber ,
							a.Amount ,
							a.LastUpdateTime
				  FROM		(
							  SELECT DISTINCT
										a.ReferenceNo ,
										a.InvoiceNo ,
										a.PostingDate ,
										b.DealerID ,
										f.DealerCode ,
										c.TotalPrice ,
										c.Tax ,
										e.SONumber ,
										a.Debit AS Amount ,
										a.LastUpdateTime
							  FROM		DepositLine a WITH ( NOLOCK )
							  JOIN		SparePartBilling b WITH ( NOLOCK ) ON a.InvoiceNo = b.BillingNumber
																			  AND b.RowStatus = 0
							  JOIN		SparePartBillingDetail c WITH ( NOLOCK ) ON b.ID = c.SparePartBillingID
																					AND c.RowStatus = 0
							  JOIN		SparePartDODetail d WITH ( NOLOCK ) ON c.SparePartDODetailID = d.ID
																			   AND d.RowStatus = 0
							  JOIN		SparePartPOEstimate e WITH ( NOLOCK ) ON d.SparePartPOEstimateID = e.ID
																				 AND e.RowStatus = 0
							  JOIN		Dealer f WITH ( NOLOCK ) ON b.DealerID = f.ID
																	AND f.RowStatus = 0
							  WHERE		a.RowStatus = 0
										AND a.PaymentType = 0
										AND YEAR(b.BillingDate) >= YEAR(GETDATE()) - 1
							) a
				  GROUP BY	a.ReferenceNo ,
							a.InvoiceNo ,
							a.PostingDate ,
							a.DealerID ,
							a.DealerCode ,
							a.SONumber ,
							a.Amount ,
							a.LastUpdateTime
				) a
	   UNION
	   SELECT	a.ReferenceNo ,
				a.InvoiceNo ,
				a.PostingDate ,
				a.DealerID ,
				a.DealerCode ,
				a.SONumber ,
				a.Amount ,
				BillingAmount = a.TotalPrice + a.TotalTax ,
				a.LastUpdateTime ,
				1 AS isTOP
	   FROM		(
				  SELECT	a.ReferenceNo ,
							a.InvoiceNo ,
							a.PostingDate ,
							a.DealerID ,
							a.DealerCode ,
							a.SONumber ,
							a.Amount ,
							SUM(a.TotalPrice) AS TotalPrice ,
							SUM(a.Tax) AS TotalTax ,
							a.LastUpdateTime
				  FROM		(
							  SELECT 	h.DONumber AS ReferenceNo ,
										c.BillingNumber AS InvoiceNo ,
										a.TransferActualDate AS PostingDate ,
										c.DealerID ,
										g.DealerCode ,
										f.SONumber ,
										b.Amount ,
										a.LastUpdateTime ,
										d.TotalPrice ,
										d.Tax
							  FROM		TOPSPTransferPayment a WITH ( NOLOCK )
							  JOIN		TOPSPTransferPaymentDetail b WITH ( NOLOCK ) ON b.TOPSPTransferPaymentID = a.ID
																						AND b.RowStatus = 0
							  JOIN		SparePartBilling c WITH ( NOLOCK ) ON b.SparePartBillingID = c.ID
																			  AND c.RowStatus = 0
							  JOIN		SparePartBillingDetail d WITH ( NOLOCK ) ON c.ID = d.SparePartBillingID
																					AND d.RowStatus = 0
							  JOIN		SparePartDODetail e WITH ( NOLOCK ) ON d.SparePartDODetailID = e.ID
																			   AND e.RowStatus = 0
							  JOIN		SparePartPOEstimate f WITH ( NOLOCK ) ON e.SparePartPOEstimateID = f.ID
																				 AND f.RowStatus = 0
							  JOIN		SparePartDO h WITH ( NOLOCK ) ON e.SparePartDOID = h.ID
																		 AND h.RowStatus = 0
							  JOIN		Dealer g WITH ( NOLOCK ) ON c.DealerID = g.ID
																	AND g.RowStatus = 0
							  WHERE		a.Status = 5
										AND a.RowStatus = 0
							) a
				  GROUP BY	a.ReferenceNo ,
							a.InvoiceNo ,
							a.PostingDate ,
							a.DealerID ,
							a.DealerCode ,
							a.SONumber ,
							a.Amount ,
							a.LastUpdateTime
				) a
GO

--========================================================================================================================                
-- Created By: Mitrais (Prins Carl S)                
-- Get Spare Part Master Price List             
--======================================================================================================================== 

CREATE VIEW VWI_SparePartPriceList
AS
	   SELECT	ID ,
				PartNumber ,
				PartName ,
				UOM ,
				RetalPrice AS RetailPrice , 
       --ActiveStatus = case when RowStatus = 0 then ActiveStatus  else 1 end,
				ActiveStatus = CASE	WHEN RowStatus = 0 THEN CASE WHEN ActiveStatus = 0 THEN ActiveStatus
																 ELSE -1
															END
									ELSE -1
							   END ,
				LastUpdateTime
	   FROM		SparePartMaster WITH ( NOLOCK )
	   WHERE	ProductCategoryID IN ( 1, 3 )
GO

--========================================================================================================================                
-- Created By: Mitrais (Prins Carl S)                
-- Get Spare Part UOM             
--======================================================================================================================== 
CREATE VIEW VWI_SparePartUoM
AS
	   SELECT	ROW_NUMBER() OVER ( ORDER BY UoM ) AS ID ,
				UoM
	   FROM		(
				  SELECT	UoM AS UoM
				  FROM		dbo.SparePartMaster WITH ( NOLOCK )
				  WHERE		UoM IS NOT NULL
							AND LEN(UoM) > 0
				  GROUP BY	UoM
				  UNION
				  SELECT	UoMto AS UoM
				  FROM		dbo.SparePartConversion WITH ( NOLOCK )
				  WHERE		UoMto IS NOT NULL
							AND LEN(UoMto) > 0
				  GROUP BY	UoMto
				) Sparepart
GO

CREATE VIEW VWI_SPKChassisMatching
AS
	   SELECT	dbo.SPKChassis.ID ,
				dbo.Dealer.DealerCode ,
				dbo.SPKChassis.MatchingDate ,
				dbo.CustomerRequest.CustomerCode ,
				dbo.CustomerRequest.Name1 AS Name ,
				dbo.SPKHeader.SPKNumber ,
				dbo.ChassisMaster.ChassisNumber ,
				dbo.ChassisMaster.EngineNumber ,
				dbo.SPKChassis.KeyNumber ,
				dbo.VechileType.VechileTypeCode AS VehicleTypeCode ,
				dbo.VechileColor.ColorCode ,
				dbo.VechileColor.MaterialDescription AS Description ,
				dbo.SPKChassis.MatchingNumber ,
				dbo.SPKChassis.ReferenceNumber
	   FROM		dbo.SPKChassis WITH ( NOLOCK )
	   INNER JOIN dbo.SPKDetail WITH ( NOLOCK ) ON dbo.SPKChassis.SPKDetailID = dbo.SPKDetail.ID
	   INNER JOIN dbo.SPKHeader WITH ( NOLOCK ) ON dbo.SPKDetail.SPKHeaderID = dbo.SPKHeader.ID
	   INNER JOIN dbo.Dealer WITH ( NOLOCK ) ON dbo.SPKHeader.DealerID = dbo.Dealer.ID
	   INNER JOIN dbo.ChassisMaster WITH ( NOLOCK ) ON dbo.SPKChassis.ChassisMasterID = dbo.ChassisMaster.ID
	   INNER JOIN dbo.VechileColor WITH ( NOLOCK ) ON dbo.SPKDetail.VehicleColorID = dbo.VechileColor.ID
	   INNER JOIN dbo.VechileType WITH ( NOLOCK ) ON dbo.VechileColor.VechileTypeID = dbo.VechileType.ID
	   INNER JOIN dbo.VechileModel WITH ( NOLOCK ) ON dbo.VechileType.ModelID = dbo.VechileModel.ID
	   INNER JOIN dbo.CustomerRequest WITH ( NOLOCK ) ON dbo.SPKHeader.CustomerRequestID = dbo.CustomerRequest.ID
GO

--========================================================================================================================                    
-- Created By: Mitrais (Prins Carl S)                    
-- View for Unmatch SPK Chassis                   
--========================================================================================================================      
CREATE VIEW VWI_UnmatchSPKChassis
AS
	   SELECT	a.ID ,
				a.RegNumber ,
				a.RevisionDate ,
				a.RevisionStatusID ,
				a.RevisionStatus ,
				a.RevisionType ,
				a.ChassisNumber ,
				a.SPKHeaderID ,
				a.SPKNumber ,
				a.DealerCode ,
				a.DealerSPKNumber ,
				a.LastUpdateTime
	   FROM		VWI_InvoiceRevision a WITH ( NOLOCK )
	   WHERE	a.RevisionStatusID IN ( 2, 3, 4 )  
GO

--========================================================================================================================                  
-- Created By: Mitrais (Prins Carl S)                  
-- View for Vehicle Exterior Color                
--========================================================================================================================  
CREATE VIEW VWI_VehicleExteriorColor
AS
	   SELECT	ROW_NUMBER() OVER ( ORDER BY ColorCode ) AS ID ,
				T0.ColorCode AS ColorCode ,
				MAX(T0.ColorIndName) AS ColorIndName ,
				MAX(T0.LastUpdateBy) AS LastUpdateBy ,
				MAX(T0.LastUpdateTime) AS LastUpdateTime ,
				T0.RowStatus
	   FROM		VechileColor AS T0 WITH ( NOLOCK )
	   INNER JOIN VechileType AS T1 WITH ( NOLOCK ) ON T0.VechileTypeID = T1.ID
	   WHERE	T1.CategoryID <> 3
				AND T0.RowStatus = 0
	   GROUP BY	T0.ColorCode ,
				T0.RowStatus;
GO

--========================================================================================================================                    
-- Created By: Mitrais (Prins Carl S)                    
-- Vehicle Information                    
--========================================================================================================================                    
CREATE VIEW VWI_VehicleInformation
AS
	   SELECT	a.ID ,
				a.ChassisNumber ,
				a.isBB AS IsBB ,
				f.CategoryCode ,
				CategoryDesc = f.Description ,
				b.ColorCode ,
				b.ColorIndName ,
				b.ColorEngName ,
				b.MaterialDescription ,
				VehicleTypeCode = c.VechileTypeCode ,
				VehicleTypeDesc = c.Description ,
				ModelSearchTerm1 = j.VechileModelIndCode ,
				ModelSearchTerm2 = j.IndDescription ,
				c.SegmentType ,
				c.FuelType ,
				c.TransmitType ,
				c.DriveSystemType ,
				c.VariantType ,
				VehicleBrand = 'Mitsubishi' ,
				c.SpeedType ,
				a.VehicleKindID ,
				d.Code ,
				VehicleKindDesc = c.Description ,
				a.SoldDealerID ,
				e.DealerCode ,
				e.DealerName ,
				a.EngineNumber ,
				a.SerialNumber ,
				a.ProductionYear ,
				FleetCode = i.Code ,
				g.OpenFakturDate ,
				g.FakturDate ,
				FSExtended = '' ,
				k.PKTDate ,
				a.LastUpdateTime
	   FROM		(
				  SELECT	ID ,
							EndCustomerID ,
							ChassisNumber ,
							isBB ,
							CategoryID ,
							VechileColorID ,
							VehicleKindID ,
							SoldDealerID ,
							EngineNumber ,
							SerialNumber ,
							ProductionYear ,
							LastUpdateTime
				  FROM		(
							  SELECT	ID ,
										EndCustomerID ,
										ChassisNumber ,
										isBB = 0 ,
										CategoryID ,
										VechileColorID ,
										VehicleKindID ,
										SoldDealerID ,
										EngineNumber ,
										SerialNumber ,
										ProductionYear ,
										LastUpdateTime
							  FROM		ChassisMaster WITH ( NOLOCK )
							  WHERE		RowStatus = 0
										AND FakturStatus = 4
							  UNION
							  SELECT	ID ,
										EndCustomerID ,
										ChassisNumber ,
										isBB = 1 ,
										CategoryID ,
										VechileColorID ,
										VehicleKindID ,
										SoldDealerID ,
										EngineNumber ,
										SerialNumber ,
										ProductionYear ,
										LastUpdateTime
							  FROM		ChassisMasterBB WITH ( NOLOCK )
							  WHERE		RowStatus = 0
							) a
				  WHERE		--a.CategoryID in (3)  
							a.CategoryID IN ( 1, 2 )
				) a
	   LEFT JOIN VechileColor b WITH ( NOLOCK ) ON a.VechileColorID = b.ID
												   AND b.RowStatus = 0
	   LEFT JOIN VechileType c WITH ( NOLOCK ) ON c.ID = b.VechileTypeID
												  AND c.RowStatus = 0
	   LEFT JOIN VehicleKind d WITH ( NOLOCK ) ON a.VehicleKindID = d.ID
												  AND d.RowStatus = 0
	   LEFT JOIN Dealer e WITH ( NOLOCK ) ON a.SoldDealerID = e.ID
											 AND e.RowStatus = 0
	   LEFT JOIN Category f WITH ( NOLOCK ) ON a.CategoryID = f.ID
											   AND f.RowStatus = 0
	   LEFT JOIN EndCustomer g WITH ( NOLOCK ) ON a.EndCustomerID = g.ID
												  AND g.RowStatus = 0
	   LEFT JOIN FleetCustomerToCustomer h WITH ( NOLOCK ) ON g.CustomerID = h.CustomerID
															  AND h.RowStatus = 0
	   LEFT JOIN FleetCustomer i WITH ( NOLOCK ) ON h.FleetCustomerID = i.ID
													AND i.RowStatus = 0
	   LEFT JOIN VechileModel j WITH ( NOLOCK ) ON c.ModelID = j.ID
												   AND j.RowStatus = 0
	   LEFT JOIN ChassisMasterPKT k WITH ( NOLOCK ) ON a.ID = k.ChassisMasterID
													   AND k.RowStatus = 0  
--where c.Status <> 'X'   Comment out by SLA untuk tes tarik data ALL ke SIT QA, nanti pas prod dibalikin lagi 20181115  
  
GO

--========================================================================================================================                    
-- Created By: Mitrais (Prins Carl S)                    
-- ProductSpecification                  
--========================================================================================================================                    
CREATE VIEW VWI_VehicleSpecification
AS
	   SELECT	a.VariantType AS VehicleCategory_S1 ,
				a.VechileTypeCode AS ClassificationNumber ,
				a.Description AS VehicleDesc ,
				b.Code AS ProductCategory ,
				c.Description AS VehicleCatDesc ,
				'MITSUBISHI' AS VehicleBrand ,
				a.SpeedType AS SpeedType ,
				a.FuelType AS FuelType ,
				a.TransmitType AS Transmition ,
				a.DriveSystemType AS Drivesystem ,
				a.SegmentType AS SegmentType ,
				a.LastUpdateTime ,
				CASE WHEN a.RowStatus <> 0 THEN -1
					 ELSE CASE WHEN a.Status = 'X' THEN -1
							   ELSE 0
						  END
				END AS Status
	   FROM		VechileType AS a WITH ( NOLOCK )
	   INNER JOIN ProductCategory AS b WITH ( NOLOCK ) ON a.ProductCategoryID = b.ID
	   INNER JOIN Category AS c WITH ( NOLOCK ) ON a.CategoryID = c.ID
	   WHERE	b.ID = 1
GO

--========================================================================================================================                  
-- Created By: Mitrais (Prins Carl S)                  
-- Whosesale Price List                  
--========================================================================================================================  

CREATE VIEW VWI_WholesalesPrice
AS
	   SELECT	a.DealerID ,
				a.DealerCode ,
				a.VehicleColorID ,
				a.MaterialNumber ,
				a.MaterialDescription ,
				a.VehicleColorCode ,
				a.VehicleColorName ,
				a.VehicleTypeCode ,
				a.VehicleTypeDesc AS VehicleTypeDesc ,
				b.ValidFrom ,
				b.BasePrice ,
				b.OptionPrice ,
				b.PPN_BM ,
				b.PPN ,
				b.PPh22 ,
				b.PPh23 ,
				b.FactoringInt ,
				b.DiscountReward ,
				b.LastUpdateTime
	   FROM		(
				  SELECT	a.DealerID ,
							a.DealerCode ,
							b.VehicleColorID ,
							b.MaterialNumber ,
							b.MaterialDescription ,
							b.VehicleColorCode ,
							b.VehicleColorName ,
							b.VehicleTypeCode ,
							b.VehicleTypeDesc
				  FROM		(
							  SELECT	a.ID AS DealerID ,
										a.DealerCode
							  FROM		Dealer a WITH ( NOLOCK )
							  WHERE		a.RowStatus = 0
							) a
				  CROSS JOIN (
							   SELECT	a.ID AS VehicleColorID ,
										a.MaterialNumber ,
										a.MaterialDescription ,
										a.ColorCode AS VehicleColorCode ,
										a.ColorIndName AS VehicleColorName ,
										b.VechileTypeCode AS VehicleTypeCode ,
										b.Description AS VehicleTypeDesc
							   FROM		VechileColor a WITH ( NOLOCK )
							   LEFT JOIN VechileType b WITH ( NOLOCK ) ON a.VechileTypeID = b.ID
																		  AND b.RowStatus = 0
							   LEFT JOIN VechileModel c ON b.ModelID = c.ID
														   AND c.RowStatus = 0
							   WHERE	a.RowStatus = 0
										AND c.CategoryID IN ( 1, 2 )
							 ) b
				) a
	   OUTER APPLY fni_RetrievePriceListActive(GETDATE(), a.VehicleColorID, a.DealerID) b 

GO

--========================================================================================================================                    
-- Created By: Mitrais (Prins Carl S)                    
-- View Open Faktur For PDI                   
--========================================================================================================================   
CREATE VIEW [dbo].[VWI_OpenFakturForPDI]      
AS      
      
SELECT T0.SPKHeaderID as ID, T1.FakturDate  as OpenFakturDate, 
          T1.LastUpdateTime,  
          T2.DealerId AS SoldDealerID, T4.DealerCode,T3.ChassisNumber 
FROM SPKFAKTUR AS T0 with (nolock)   
LEFT JOIN EndCustomer AS T1 with (nolock)  ON T0.EndCustomerID = T1.ID  and T1.RowStatus = 0 
LEFT JOIN ChassisMaster AS T3  with (nolock) ON T3.EndCustomerID = T1.ID and T3.RowStatus = 0  
LEFT JOIN SPKHeader AS T2 with (nolock) on T0.SPKHeaderId = T2.ID and T2.RowStatus = 0
LEFT JOIN Dealer AS T4 with (nolock) ON T4.id = t2.DealerId AND T4.RowStatus = 0  
WHERE T0.RowStatus = 0 and T3.FakturStatus > 0
GO

--========================================================================================================================                    
-- Created By: Mitrais (Prins Carl S)                    
-- Job Position Sales                  
--========================================================================================================================   
CREATE VIEW [dbo].[VWI_JobPositionSales]      
AS      
SELECT ID, Code, Description FROM JobPosition WITH (NOLOCK)
where RowStatus = 0 and Category = 1
GO

--========================================================================================================================                    
-- Created By: Mitrais (Prins Carl S)                    
-- Job Position Services                  
--========================================================================================================================   
CREATE VIEW [dbo].[VWI_JobPositionServices]      
AS      
SELECT ID, Code, Description FROM JobPosition WITH (NOLOCK)
where RowStatus = 0 and Category = 2
GO

--========================================================================================================================                    
-- Created By: Mitrais (Prins Carl S)                    
-- Job Position Spare Parts                  
--========================================================================================================================   
CREATE VIEW [dbo].[VWI_JobPositionParts]      
AS      
SELECT a.ID, a.Kode, a.PositionName, b.ID as ParentID, b.Kode as ParentKode, b.PositionName as ParentPositionName 
FROM SalesmanCategoryLevel a WITH (NOLOCK)
LEFT JOIN SalesmanCategoryLevel b WITH (NOLOCK) on a.ParentID = b.ID and b.RowStatus = 0
where a.RowStatus = 0
GO

CREATE VIEW V_BusinessSector
AS
	   SELECT	T0.BusinessSectorName ,
				T1.BusinessDomain ,
				CASE WHEN T0.RowStatus <> 0 THEN 'Not Active'
					 WHEN (
							( T0.RowStatus = 0 )
							AND ( T1.RowStatus <> 0 )
						  ) THEN 'Not Active'
					 ELSE 'Active'
				END AS VStatus ,
				T1.CreatedTime
	   FROM		BusinessSectorDetail AS T1
	   INNER JOIN BusinessSectorHeader AS T0 ON T1.BusinessSectorHeaderID = T0.ID
GO

CREATE VIEW V_City
AS
	   SELECT DISTINCT
				T0.CityCode AS CityCode ,
				MAX(T0.CityName) AS CityName ,
				MAX(T0.LastUpdateTime) AS LastUpdateTime
	   FROM		[dbo].[City] AS T0 ( NOLOCK )
	   INNER JOIN [dbo].[Province] AS T1 ( NOLOCK ) ON T0.ProvinceID = T1.ID
	   WHERE	T0.RowStatus = 0
				AND T1.[RowStatus] = 0
	   GROUP BY	T0.CityCode
GO

CREATE VIEW V_LeasingCompany
AS
	   SELECT	ID AS LeasingCompanyID ,
				LeasingName ,
				RowStatus ,
				LastUpdateTime
	   FROM		LeasingCompany
	   WHERE	RowStatus = 0
GO

CREATE VIEW V_SparePartFlow_v2
AS
	   SELECT	po.ID POID ,
				po.PONumber ,
				po.PODate ,
				po.SentPODate POSendDate ,
				po.TermOfPaymentID ,
				teop.Description TOPDescription ,
				so.ID SOID ,
				so.SONumber ,
				so.SODate ,
				do.ID DOID ,
				do.DONumber ,
				do.DoDate ,
				bill.ID BillingID ,
				bill.BillingNumber ,
				bill.BillingDate ,
				po.DealerID ,
				dl.DealerCode ,
				po.OrderType ,
				so.DocumentType ,
				tbs.TOPCeilingStatus ,
				[dbo].[fn_GetDOStatus](po.PONumber, so.SONumber, do.DONumber) --	1
				[STATUS]
	   FROM		SparePartPO po ( NOLOCK )
	   LEFT JOIN SparePartPOEstimate so ( NOLOCK ) ON so.SparePartPOID = po.ID
													  AND so.RowStatus = 0
	   LEFT JOIN SparePartDODetail dodet ( NOLOCK ) ON dodet.SparePartPOEstimateID = so.ID
													   AND dodet.RowStatus = 0
	   LEFT JOIN SparePartDO do ( NOLOCK ) ON do.ID = dodet.SparePartDOID
											  AND do.RowStatus = 0
	   LEFT JOIN SparePartBillingDetail billdet ( NOLOCK ) ON billdet.SparePartDODetailID = dodet.ID
															  AND billdet.RowStatus = 0
	   LEFT JOIN SparePartBilling bill ( NOLOCK ) ON bill.ID = billdet.SparePartBillingID
													 AND bill.RowStatus = 0
	   LEFT JOIN Dealer dl ( NOLOCK ) ON dl.ID = po.DealerID
										 AND dl.RowStatus = 0
	   LEFT JOIN TermOfPayment teop ( NOLOCK ) ON teop.ID = po.TermOfPaymentID
												  AND teop.RowStatus = 0
	   OUTER APPLY (
					 SELECT TOP 1
							tbs.ID TOPBlockStatusID ,
							sc.ValueDesc TOPCeilingStatus
					 FROM	dbo.TOPBlockStatus tbs
					 LEFT JOIN StandardCode sc ON sc.ValueId = tbs.Status
												  AND sc.Category = 'TOPCeilingStatus'
					 WHERE	tbs.RowStatus = 0
							AND tbs.ID = po.TOPBlockStatusID
				   ) tbs
	   WHERE	1 = 1
				AND po.RowStatus = 0
	   GROUP BY	po.ID ,
				po.PONumber ,
				po.PODate ,
				po.SentPODate ,
				po.TermOfPaymentID ,
				so.ID ,
				so.SONumber ,
				so.SODate ,
				do.ID ,
				do.DONumber ,
				do.DoDate ,
				bill.ID ,
				bill.BillingNumber ,
				bill.BillingDate ,
				po.DealerID ,
				dl.DealerCode ,
				po.OrderType ,
				so.DocumentType ,
				tbs.TOPCeilingStatus ,
				teop.Description
GO

COMMIT
GO




set xact_abort on
go

begin transaction
go

set ANSI_NULLS on
go

--========================================================================================================================                    
-- Created By: Mitrais (Prins Carl S)                    
-- Job Position Spare Parts                  
--========================================================================================================================   
alter VIEW VWI_JobPositionParts      
AS      
SELECT a.ID, a.Kode as Code, a.PositionName, b.ID as ParentID, b.Kode as ParentCode, b.PositionName as ParentPositionName 
FROM SalesmanCategoryLevel a WITH (NOLOCK)
LEFT JOIN SalesmanCategoryLevel b WITH (NOLOCK) on a.ParentID = b.ID and b.RowStatus = 0
where a.RowStatus = 0
go

commit
go


