USE [BSIDNET_MMKSI_DMS]
GO

/****** Object:  View [dbo].[VWI_Province]    Script Date: 16/04/2018 15:24:02 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE view [dbo].[VWI_Province] AS

select ROW_NUMBER() OVER (ORDER BY a.ProvinceCode) AS ID, a.ProvinceCode, a.ProvinceName, a.LastUpdateTime, a.Status
from           
(
SELECT DISTINCT ProvinceCode as ProvinceCode, MAX(ProvinceName) as ProvinceName,
          MAX(LastUpdateTime) AS LastUpdateTime, max(RowStatus) As Status
FROM [dbo].[Province]
WHERE RowStatus = 0 and ProvinceCode <> 'UK'
GROUP BY ProvinceCode
) a




GO


