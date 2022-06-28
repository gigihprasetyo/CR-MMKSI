USE [BSIDNET_MMKSI_DMS]
GO


/****** Object:  View [dbo].[VWI_EmployeeParts]    Script Date: 24/04/2018 14:39:26 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


CREATE VIEW [dbo].[VWI_EmployeeParts] 
AS
SELECT a.ID, a.SalesmanCode, a.DealerId, b.DealerCode, a.DealerBranchId, c.DealerBranchCode,
       Status = case when a.RowStatus = -1 then a.RowStatus else
	                case when a.Status = 2 then 0 else -1 end
			    end,  
	   a.LastUpdateTime
FROM SalesmanHeader a
join Dealer b on a.DealerId = b.ID and b.RowStatus = 0
join DealerBranch c on a.DealerID = c.DealerID and a.DealerBranchId = c.ID and c.RowStatus = 0
WHERE a.SalesIndicator = 1

GO