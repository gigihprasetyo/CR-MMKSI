USE [BSIDNET_MMKSI_DMS]
GO

/****** Object:  View [dbo].[VWI_PRHistoryDO]    Script Date: 18/04/2018 8:36:31 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

--========================================================================================================================                
-- Created By: Mitrais (Prins Carl S)                
-- PR History Part 2            
--========================================================================================================================   
CREATE view [dbo].[VWI_PRHistoryDO]
as  
select ROW_NUMBER() OVER (ORDER BY SONumber, NomorDO) AS ID,              
       a.DealerID, a.DealerCode, a.DealerName, a.PONumber, a.PODate, a.DMSPRNo, 
	   a.OrderType, a.SONumber, a.NomorDO, a.TanggalDO, a.SparePartMasterID, a.PartNumber, a.PartName, a.Qty,
	   a.EstimasiTanggalPengiriman, a.PickingDate, a.PackingDate, a.GoodIssueDate, a.PaymentDate, a.ReadyForDeliveryDate,
	   a.ExpeditionNo, a.ExpeditionName, a.ATD, a.ETA 
from
(
select distinct a.DealerID, e.DealerCode, e.DealerName,
       ISNULL(coalesce(coalesce(p.EstimationNumber, m.RequestNo), h.PONumber),'') AS PONumber, 
	   h.PODate, ISNULL(coalesce(coalesce(p.DMSPRNo, m.DMSPRNo), h.DMSPRNo),'') AS DMSPRNo, 
	   h.OrderType, ISNULL(g.SONumber,'') AS SONumber, 
       ISNULL(a.DONumber,'') AS NomorDO, TanggalDO = a.DoDate, 
	   f.SparePartMasterID, ISNULL(q.PartNumber,'') AS PartNumber, ISNULL(q.PartName,'') AS PartName, ISNULL(f.Qty,0) AS Qty,
	   EstimasiTanggalPengiriman = a.EstmationDeliveryDate,
	   a.PickingDate, a.PackingDate, a.GoodIssueDate, a.PaymentDate, a.ReadyForDeliveryDate,
	   ISNULL(d.ExpeditionNo,'') AS ExpeditionNo, ISNULL(d.ExpeditionName,'') AS ExpeditionName, d.ATD, d.ETA 
from SparePartDO a with (nolock)
left join SparePartPackingDetail b with (nolock) on b.SparePartDOID = a.ID and b.RowStatus = 0
left join SparePartPacking c with (nolock) on c.ID = b.SparePartPackingID and c.RowStatus = 0
left join SparePartDOExpedition d with (nolock) on c.SparePartDOExpeditionID = d.ID and d.RowStatus = 0
left join Dealer e with (nolock) on e.ID = a.DealerID and e.RowStatus = 0
left join SparePartDODetail f with (nolock) on f.SparePartDOID = a.ID and f.RowStatus = 0
join SparePartPOEstimate g with (nolock) on f.SparePartPOEstimateID = g.ID and g.RowStatus = 0
--join VWI_PRHistorySO h with (nolock) on g.SONumber = h.NomorPenjualan
left join SparePartPO h with (nolock) on g.SparePartPOID = h.ID
left join SparePartPODetail i with (nolock) on g.ID = i.SparePartPOID and i.RowStatus = 0
left join IndentPartPO j with (nolock) on i.ID = j.SparePartPODetailID and j.RowStatus = 0
left join IndentPartDetail l with (nolock) on l.ID = j.IndentPartDetailID and l.RowStatus = 0
left join IndentPartHeader m with (nolock) on m.ID = l.IndentPartHeaderID and m.RowStatus = 0
left join EstimationEquipPO n with (nolock) on n.IndentPartDetailID = l.ID and n.RowStatus = 0
left join EstimationEquipDetail o with (nolock) on o.ID = n.EstimationEquipDetailID and o.RowStatus = 0
left join EstimationEquipHeader p with (nolock) on p.ID = o.EstimationEquipHeaderID and p.RowStatus = 0
left join SparePartMaster q with (nolock) on f.SparePartMasterID = q.ID and q.RowStatus = 0
where a.RowStatus = 0 and cast(a.DoDate as date) > '2017-12-31'
) a

GO


