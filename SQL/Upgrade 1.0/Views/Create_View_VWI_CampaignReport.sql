/****** Object:  View [dbo].[VWI_CampaignReport]    Script Date: 07/03/2018 11:00:49 ******/
USE [BSIDNET_MMKSI_DMS]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE VIEW [dbo].[VWI_CampaignReport]       
AS    

select ROW_NUMBER() OVER (ORDER BY HeaderID, BenefitRegNo, DealerCode, VehicleTypeID) AS ID,    
       a.HeaderID, a.NomorSurat, a.Status, a.BenefitRegNo, a.Remarks, a.RowStatus, a.LastUpdateTime,    
       a.DealerID, a.DealerCode, a.FakturValidationStart, a.FakturValidationEnd, a.FakturOpenStart, a.FakturOpenEnd,    
	   a.VehicleTypeID, a.VehicleTypeCode, a.VehicleTypeDesc--, a.FormulaID    

FROM    
(    
select distinct bmh.ID As HeaderID, bmh.NomorSurat AS NomorSurat,    
       bmh.status AS Status, -- status 0 (active) status 1 (not active)    
       bmh.BenefitRegNo AS BenefitRegNo,    
       bmh.Remarks AS Remarks,    
       bmh.RowStatus AS RowStatus,    
       bmh.lastUpdateTime AS LastUpdateTime,    
       bmd.Rowstatus AS DetailRowStatus,    
       bml.DealerID AS DealerID,    
       dlr.DealerCode AS DealerCode,    
       bmd.FakturValidationStart,    
       bmd.FakturValidationEnd,    
       bmd.FakturOpenStart,    
       bmd.FakturOpenEnd,    
       bmt.VechileTypeID AS VehicleTypeID,    
       vtp.VechileTypeCode AS VehicleTypeCode,    
       vtp.Description AS VehicleTypeDesc--,    
       --bmd.FormulaID    
from BenefitMasterHeader bmh with (nolock)    
INNER JOIN BenefitMasterDetail bmd with (nolock) on bmh.ID = bmd.BenefitMasterHeaderID and bmd.RowStatus = 0    
INNER JOIN BenefitMasterDealer bml with (nolock) on bmh.ID = bml.BenefitMasterHeaderID and bml.RowStatus = 0    
INNER JOIN Dealer dlr with (nolock) on bml.DealerID = dlr.ID and dlr.RowStatus = 0    
INNER JOIN BenefitMasterVehicleType bmt with (nolock) on bmd.ID = bmt.BenefitMasterDetailID and bmt.RowStatus = 0    
INNER JOIN VechileType vtp with (nolock) on bmt.VechileTypeID = vtp.ID and vtp.RowStatus = 0 and vtp.Status <> 'X'    
--WHERE bmh.RowStatus =0    
) a 

GO