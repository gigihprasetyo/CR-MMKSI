USE [BSIDNET_MMKSI_DMS]
GO

/****** Object:  View [dbo].[VWI_PRHistorySO]    Script Date: 18/04/2018 8:34:55 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

--========================================================================================================================                
-- Created By: Mitrais (Prins Carl S)                
-- PR History Part 1                
--========================================================================================================================   
CREATE view [dbo].[VWI_PRHistorySO]
as  
select     ROW_NUMBER() OVER (ORDER BY PONumber, NomorPenjualan) AS ID,
           a.PONumber, a.DealerID, a.DealerCode, a.PODate, a.DMSPRNo, a.OrderType, a.SODate, a.NomorPenjualan, a.DocumentType,
		   a.KodeBarang, a.NamaBarang, a.JumlahPesanan, a.JumlahPemenuhan, a.HargaEceran, a.TotalBaseAmountDetail, a.NomorPengganti,
		   a.Diskon, a.TotalAmountDetail, a.TotalBaseAmountHeader, a.TotalConsumptionTaxAmount, a.TotalAmountHeader,
		   a.Status, a.StatusDesc
from
(           
	select distinct a.ID, 
		   PONumber = coalesce(coalesce(i.EstimationNumber, f.RequestNo), a.PONumber), 
		   a.DealerID, t.DealerCode, a.PODate, 
		   ISNULL(coalesce(coalesce(i.DMSPRNo, f.DMSPRNo), a.DMSPRNo),'') AS DMSPRNo, 
		   a.OrderType,
		   ISNULL(j.SODate,'') AS SODate, ISNULL(j.SONumber,'') AS NomorPenjualan, DocumentType = ISNULL(j.DocumentType,''),
		   KodeBarang = ISNULL(k.PartNumber,''), NamaBarang = ISNULL(k.PartName,''), 
		   JumlahPesanan = coalesce(coalesce(h.EstimationUnit, e.Qty), b.Quantity),
		   JumlahPemenuhan = IsNull(k.AllocQty,0),
		   HargaEceran = coalesce(coalesce(h.Harga, e.Price), b.RetailPrice),
		   TotalBaseAmountDetail = 0,
		   NomorPengganti = ISNULL(k.AltPartNumber,''),
		   Diskon = ISNULL(k.Discount,0),
		   TotalAmountDetail = Coalesce((k.RetailPrice * k.AllocQty) - k.Discount,0),
		   TotalBaseAmountHeader = 0,
		   TotalConsumptionTaxAmount = 0,
		   TotalAmountHeader = 0,
		   Status = coalesce(coalesce(cast(i.Status as char), cast(f.Status as char)),a.ProcessCode),
		   StatusDesc = coalesce(coalesce(n.ValueDesc, m.ValueDesc), l.ValueDesc)
	from SparePartPO a with (nolock)
	left join SparePartPODetail b with (nolock) on a.ID = b.SparePartPOID and b.RowStatus = 0
	join SparePartMaster o with (nolock) on b.SparePartMasterID = o.ID and o.RowStatus = 0
	left join IndentPartPO c with (nolock) on b.ID = c.SparePartPODetailID and c.RowStatus = 0
	left join IndentPartDetail e with (nolock) on e.ID = c.IndentPartDetailID and e.RowStatus = 0
	left join IndentPartHeader f with (nolock) on f.ID = e.IndentPartHeaderID and f.RowStatus = 0
	left join EstimationEquipPO g with (nolock) on g.IndentPartDetailID = e.ID and g.RowStatus = 0
	left join EstimationEquipDetail h with (nolock) on h.ID = g.EstimationEquipDetailID and h.RowStatus = 0
	left join EstimationEquipHeader i with (nolock) on i.ID = h.EstimationEquipHeaderID and i.RowStatus = 0
	left join SparePartPOEstimate j with (nolock) on j.SparePartPOID = a.ID and j.RowStatus = 0
	left join SparePartPOEstimateDetail k with (nolock) on j.ID = k.SparePartPOEstimateID and k.RowStatus = 0 and o.PartNumber = k.PartNumber
	left join StandardCodeChar l with (nolock) on l.Category = 'SparePartPOStatus' and  l.ValueID = a.ProcessCode and l.RowStatus = 0
	left join StandardCode m with (nolock) on m.Category = 'EnumIndentPartStatus.IndentPartStatusDealer' and m.ValueId = f.Status and m.RowStatus = 0
	left join StandardCode n with (nolock) on n.Category = 'EnumIndertPartEquipStatus.EnumStatusDealer' and n.ValueId = i.Status and n.RowStatus = 0
	left join Dealer t with (nolock) on t.ID = a.DealerID and t.RowStatus = 0
	where a.RowStatus = 0 and a.PODate > '2017-12-31' 
	--and a.PODate = '2018-01-02'
	--and a.OrderType in ('R') 
	--order by ID
	--and a.PONumber = 'EAKZ01621800010'
) a

GO


