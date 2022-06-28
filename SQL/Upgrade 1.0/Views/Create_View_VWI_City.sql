USE [BSIDNET_MMKSI_DMS]
GO

/****** Object:  View [dbo].[VWI_City]    Script Date: 16/04/2018 15:19:33 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE VIEW [dbo].[VWI_City] AS

select ROW_NUMBER() OVER (ORDER BY a.ProvinceCode, a.CityCode) AS ID, 
       a.ProvinceCode, a.ProvinceName, a.CityCode, a.CityName,
	   a.LastUpdateTime, a.Status
from 
( 
SELECT b.ProvinceCode, b.ProvinceName, a.CityCode, a.CityName, 
       a.LastUpdateTime, Status = case when a.RowStatus = -1 then a.RowStatus else case when a.Status = 'X' then -1 else a.RowStatus end end
FROM City AS a 
INNER JOIN Province AS b ON a.ProvinceID = b.ID
Where CityCode <> 'UNKNOWN'
) a
GO


