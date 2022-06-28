USE [BSIDNET_MMKSI_DMS]
GO

/****** Object:  View [dbo].[VWI_SparePartUoM]    Script Date: 27/03/2018 15:27:27 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE VIEW [dbo].[VWI_SparePartUoM]
AS
SELECT ROW_NUMBER() OVER (ORDER BY UoM ) as ID, UoM
FROM dbo.SparePartMaster group by UoM;

GO