USE [BSIDNET_MMKSI_TOP]
GO

/****** Object:  View [dbo].[V_SpPO_Indent]    Script Date: 05/09/2018 16:07:18 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER OFF
GO

ALTER VIEW [dbo].[V_SpPO_Indent]
AS

SELECT     dbo.SparePartPODetail.SparePartPOID AS ID, dbo.IndentPartHeader.RequestNo, dbo.SparePartPO.PONumber, dbo.SparePartPO.OrderType, 
                      dbo.SparePartPO.DealerID, dbo.SparePartPO.PODate, dbo.SparePartPO.IndentTransfer, dbo.Dealer.DealerCode, dbo.Dealer.DealerName, 
                      dbo.IndentPartHeader.ID AS IndentPartHeaderID
					  , dbo.TermOfPayment.Description TOPDescription, dbo.TermOfPayment.ID TermOfPaymentID
FROM         dbo.SparePartPODetail INNER JOIN
                      dbo.IndentPartPO ON dbo.SparePartPODetail.ID = dbo.IndentPartPO.SparePartPODetailID INNER JOIN
                      dbo.IndentPartDetail ON dbo.IndentPartPO.IndentPartDetailID = dbo.IndentPartDetail.ID INNER JOIN
                      dbo.IndentPartHeader ON dbo.IndentPartDetail.IndentPartHeaderID = dbo.IndentPartHeader.ID INNER JOIN
                      dbo.SparePartPO ON dbo.SparePartPODetail.SparePartPOID = dbo.SparePartPO.ID INNER JOIN
                      dbo.Dealer ON dbo.SparePartPO.DealerID = dbo.Dealer.ID LEFT JOIN
                      dbo.TermOfPayment ON dbo.SparePartPO.TermOfPaymentID = dbo.TermOfPayment.ID 
WHERE dbo.IndentPartHeader.MaterialType<>4 --Exclude Equipment
GROUP BY dbo.SparePartPODetail.SparePartPOID, dbo.IndentPartHeader.RequestNo, dbo.SparePartPO.PONumber, dbo.SparePartPO.OrderType, 
                      dbo.SparePartPO.DealerID, dbo.SparePartPO.PODate, dbo.SparePartPO.IndentTransfer, dbo.Dealer.DealerCode, dbo.Dealer.DealerName, 
                      dbo.IndentPartHeader.ID
					  , dbo.TermOfPayment.Description ,dbo.TermOfPayment.ID 


GO


