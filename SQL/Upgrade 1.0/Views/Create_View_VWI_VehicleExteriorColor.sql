USE [BSIDNET_MMKSI_DMS]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE VIEW [dbo].[VWI_VehicleExteriorColor]
AS

	SELECT ROW_NUMBER() OVER (ORDER BY ColorCode ) as ID, 
	T0.ColorCode AS ColorCode,
	MAX(T0.ColorIndName) AS ColorIndName,
		   MAX(T0.LastUpdateBy) AS LastUpdateBy,
			  MAX(T0.LastUpdateTime) AS LastUpdateTime,
	T0.RowStatus
	FROM VechileColor as T0 inner join VechileType as T1 on T0.vechiletypeid = T1.id
	WHERE T1.categoryid <> 3
	AND  T0.RowStatus = 0
	GROUP BY T0.ColorCode, T0.RowStatus;

GO