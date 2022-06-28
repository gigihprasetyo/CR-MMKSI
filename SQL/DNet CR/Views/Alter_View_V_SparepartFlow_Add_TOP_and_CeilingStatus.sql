USE [BSIDNET_MMKSI_TOP]
GO

/****** Object:  View [dbo].[V_SparePartFlow]    Script Date: 05/09/2018 9:14:52 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

ALTER VIEW [dbo].[V_SparePartFlow]
AS
SELECT
  ROW_NUMBER() OVER (ORDER BY po.ID) AS Row,
  po.ID POID,
  po.PONumber,
  po.PODate,
  po.SentPODate POSendDate,
  po.TermOfPaymentID,
  teop.Description TOPDescription,
  so.ID SOID,
  so.SONumber,
  so.SODate,
  do.ID DOID,
  do.DONumber,
  do.DoDate,
  bill.ID BillingID,
  bill.BillingNumber,
  bill.BillingDate,
  po.DealerID,
  dl.DealerCode,
  po.OrderType,
  so.DocumentType,
  tbs.TOPCeilingStatus,
  [dbo].[fn_GetDOStatus](po.PONumber, so.SONumber, do.DONumber) [STATUS]
FROM SparePartPO po (NOLOCK)
LEFT JOIN SparePartPOEstimate so (NOLOCK)
  ON so.SparePartPOID = po.ID
  AND so.RowStatus = 0
LEFT JOIN SparePartDODetail dodet (NOLOCK)
  ON dodet.SparePartPOEstimateID = so.ID
  AND dodet.RowStatus = 0
LEFT JOIN SparePartDO do (NOLOCK)
  ON do.ID = dodet.SparePartDOID
  AND do.RowStatus = 0
LEFT JOIN SparePartBillingDetail billdet (NOLOCK)
  ON billdet.SparePartDODetailID = dodet.ID
  AND billdet.RowStatus = 0
LEFT JOIN SparePartBilling bill (NOLOCK)
  ON bill.ID = billdet.SparePartBillingID
  AND bill.RowStatus = 0
LEFT JOIN Dealer dl (NOLOCK)
  ON dl.ID = po.DealerID
  AND dl.RowStatus = 0
LEFT JOIN TermOfPayment teop (NOLOCK)
  ON teop.ID = po.TermOfPaymentID
  AND teop.RowStatus = 0
LEFT JOIN (SELECT
  tbs.ID TOPBlockStatusID,
  ValueDesc TOPCeilingStatus
FROM TOPBlockStatus tbs
LEFT JOIN StandardCode sc
  ON sc.valueid = tbs.status
  AND sc.Category = 'TOPCeilingStatus') tbs
  ON tbs.TOPBlockStatusID = po.TOPBlockStatusID
WHERE 1 = 1
AND po.RowStatus = 0
--and so.SONumber is not null
--and do.DONumber is not null
--and bill.BillingNumber is not null
GROUP BY po.ID,
         po.PONumber,
         po.PODate,
         po.SentPODate,
         po.TermOfPaymentID,
         so.ID,
         so.SONumber,
         so.SODate,
         do.ID,
         do.DONumber,
         do.DoDate,
         bill.ID,
         bill.BillingNumber,
         bill.BillingDate,
         po.DealerID,
         dl.DealerCode,
         po.OrderType,
         so.DocumentType,
         tbs.TOPCeilingStatus,
         teop.Description
GO