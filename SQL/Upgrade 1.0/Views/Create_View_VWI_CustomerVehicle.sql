USE [BSIDNET_MMKSI_DMS]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE VIEW VWI_CustomerVehicle 

AS

SELECT a.ID, b.DealerCode, a.CustomerCode, a.LastUpdateTime 

FROM CustomerRequest a

join Dealer b with (nolock) on a.DealerID = b.ID

where a.RowStatus = 0


