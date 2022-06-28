USE [BSIDNET_MMKSI_DMS]
GO

/****** Object:  View [dbo].[VWI_QuickProduct]    Script Date: 18/04/2018 9:17:10 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

create view [dbo].[VWI_QuickProduct]
as
select ROW_NUMBER() OVER (ORDER BY a.VehicleType, a.ColorCode, a.Status) AS ID, 
       a.VehicleType, a.VehicleDesc, a.ProductCategory, a.VehicleCatDesc, a.ColorCode, a.ColorDescription, a.VehicleBrand,
	   a.VehicleModel_S1, a.VehicleCategory_S2, a.ProductSegment_S3, a.DriveSystem_S4, a.LastUpdateTime, a.Status
from
(
SELECT a.VechileTypeCode AS VehicleType, a.Description AS VehicleDesc,
       c.Code AS ProductCategory, d.Description AS VehicleCatDesc,
       b.ColorCode AS ColorCode, b.ColorIndName AS ColorDescription,
	   'MITSUBISHI' AS VehicleBrand,
	   '' AS VehicleModel_S1,
	   d.CategoryCode AS VehicleCategory_S2, 
	   '' AS ProductSegment_S3,
	   '' AS DriveSystem_S4, a.LastUpdateTime, 
	   Status=case when a.RowStatus = -1 then a.RowStatus else case when b.RowStatus = -1 then b.RowStatus else case when a.Status = 'X' then -1 else case when b.Status = 'X' then -1 else 0 end end end end
FROM VechileType AS a 
INNER JOIN VechileColor AS b ON a.ID = b.VechileTypeID
INNER JOIN ProductCategory AS c ON a.ProductCategoryID = c.ID
INNER JOIN Category AS d ON a.CategoryID = d.ID
where c.ID = 1
) a
GO


