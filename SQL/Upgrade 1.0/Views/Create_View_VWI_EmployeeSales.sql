USE [BSIDNET_MMKSI_DMS]
GO

/****** Object:  View [dbo].[VWI_EmployeeSales]    Script Date: 24/04/2018 9:54:17 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


CREATE VIEW [dbo].[VWI_EmployeeSales] 
AS
SELECT a.ID, a.SalesmanCode, a.DealerId, b.DealerCode, 
       Status = case when a.RowStatus = -1 then a.RowStatus else
	                case when a.Status = 2 then 0 else -1 end
			    end,  
	   a.LastUpdateTime
FROM SalesmanHeader a
join Dealer b on a.DealerId = b.ID and b.RowStatus = 0
WHERE a.SalesIndicator = 1


GO