USE [BSIDNET_MMKSI_DMS]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


CREATE VIEW dbo.VWI_PRHistoryIndentStatusCancel

as

select ROW_NUMBER() OVER (ORDER BY a.DealerId) AS ID,

       a.DealerID, b.DealerCode, 

       PONumber = coalesce(f.EstimationNumber ,a.RequestNo), DMSPRNo = coalesce(f.DMSPRNo, a.DMSPRNo), a.LastUpdateTime 

from IndentPartHeader a with(nolock)

join Dealer b with(nolock) on a.DealerID = b.ID and b.RowStatus = 0

join IndentPartDetail c with(nolock) on a.ID = c.IndentPartHeaderId and c.RowStatus = 0

join EstimationEquipPO d with(nolock) on c.ID = d.IndentPartDetailID and d.RowStatus = 0

join EstimationEquipDetail e with(nolock) on d.EstimationEquipDetailID = e.ID and e.RowStatus = 0

join EstimationEquipHeader f with(nolock) on e.EstimationEquipHeaderID = f.ID and f.RowStatus = 0

where a.StatusKTB = 5 and a.RowStatus = 0

GO