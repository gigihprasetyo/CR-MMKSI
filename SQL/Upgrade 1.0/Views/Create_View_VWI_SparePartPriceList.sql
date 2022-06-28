USE [BSIDNET_MMKSI_DMS]
GO

/****** Object:  View [dbo].[VWI_SparePartPriceList]    Script Date: 28/03/2018 14:56:36 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO



--========================================================================================================================                
-- Created By: Mitrais (Prins Carl S)                
-- Get Spare Part Master Price List             
--======================================================================================================================== 

CREATE view [dbo].[VWI_SparePartPriceList]
as
select ID, PartNumber, PartName, UoM, RetalPrice as RetailPrice, ActiveStatus from SparePartMaster
where RowStatus = 0 and ProductCategoryID in (1, 3)





GO


