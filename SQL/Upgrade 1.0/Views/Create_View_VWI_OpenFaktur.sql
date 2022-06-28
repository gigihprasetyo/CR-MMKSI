USE [BSIDNET_MMKSI_DMS]
GO

/****** Object:  View [dbo].[VWI_OpenFaktur]    Script Date: 02/03/2018 16:17:49 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE VIEW [dbo].[VWI_OpenFaktur]
AS
SELECT        dbo.ChassisMaster.ID, dbo.ChassisMaster.SoldDealerID, dbo.Dealer.DealerCode, dbo.ChassisMaster.ChassisNumber, dbo.EndCustomer.OpenFakturDate, dbo.EndCustomer.LastUpdateTime
FROM            dbo.ChassisMaster 
				INNER JOIN dbo.EndCustomer ON dbo.ChassisMaster.EndCustomerID = dbo.EndCustomer.ID
				INNER JOIN dbo.Dealer ON dbo.ChassisMaster.SoldDealerID = dbo.Dealer.ID
WHERE        (dbo.ChassisMaster.RowStatus = 0) AND (dbo.EndCustomer.RowStatus = 0) AND (dbo.Dealer.RowStatus = 0)

GO