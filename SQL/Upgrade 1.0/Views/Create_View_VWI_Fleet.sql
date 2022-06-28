USE [BSIDNET_MMKSI_DMS]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO





create VIEW dbo.VWI_Fleet 

AS



select ROW_NUMBER() OVER (ORDER BY a.DealerCode, a.FleetCode) AS ID,              

       a.DealerCode, a.DealerName, a.CustomerCode, a.FleetCode, a.FleetCustomerName, a.FleetCustomerAddress,

       a.LastUpdateTime

from

(

SELECT 

f.DealerCode AS DealerCode, f.DealerName AS DealerName, 

c.Code AS CustomerCode,

a.Code AS FleetCode, a.Name AS FleetCustomerName, a.Alamat AS FleetCustomerAddress,

c.LastUpdateTime AS LastUpdateTime

FROM FleetCustomer a

INNER JOIN FleetCustomerToCustomer b ON a.ID = b.FleetCustomerID

INNER JOIN Customer c ON b.CustomerID = c.ID

INNER JOIN EndCustomer d ON c.ID = d.CustomerID

INNER JOIN ChassisMaster e ON d.ID = e.EndCustomerID

INNER JOIN Dealer f ON e.SoldDealerID = f.ID

) a


GO