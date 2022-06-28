USE [BSIDNET_MMKSI_DMS]
GO

/****** Object:  View [dbo].[VWI_PODealer]    Script Date: 22/03/2018 9:23:46 ******/
DROP VIEW [dbo].[VWI_PODealer]
GO

/****** Object:  View [dbo].[VWI_PODealer]    Script Date: 22/03/2018 9:23:46 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


--========================================================================================================================          
-- Created By: Mitrais (Prins Carl S)          
-- PO DEaler Result          
--========================================================================================================================          
CREATE view [dbo].[VWI_PODealer]          
as          
select     
       a.POHeaderId, a.DealerId, a.DealerCode, a.DealerName,       
       a.PONumber, a.AllocQty, a.Price, a.Discount, a.Interest, a.ContractNumber, a.PKNumber, a.DealerPKNumber, a.ProjectName,        
       a.SalesOrderId, a.SONumber, a.SODate, a.PaymentRef, a.SOType, a.TermOfPaymentCode, a.TOPDescription, a.DueDate, a.LastUpdateTime,     
       a.VehicleColorCode, a.MaterialNumber, a.MaterialDescription,    
       a.BasePrice, a.OptionPrice, a.DiscountBeforeTax, a.NetPrice, a.TotalHarga,        
       a.PPN, a.TotalHargaPPN, a.TotalHargaPP, a.TotalHargaLC    
from    
(    
 select distinct z.POHeaderId, z.DealerId, z.DealerCode, z.DealerName,       
     z.PONumber, z.AllocQty, z.Price, z.Discount, z.Interest, z.ContractNumber, z.PKNumber, z.DealerPKNumber, z.ProjectName,        
     z.SalesOrderId, z.SONumber, z.SODate, z.PaymentRef, z.SOType, z.TermOfPaymentCode, z.TOPDescription, z.DueDate, z.LastUpdateTime,     
     --z.VehicleColorID,         
     z.VehicleColorCode, z.MaterialNumber, z.MaterialDescription,    
     z.BasePrice, z.OptionPrice, z.DiscountBeforeTax, z.NetPrice, z.TotalHarga,        
     PPN = FLOOR(z.PPN * 0.01 * z.TotalHarga), TotalHargaPPN = FLOOR(z.TotalHarga + FLOOR(z.PPN * 0.01 * z.TotalHarga)),        
     TotalHargaPP = FLOOR(z.PPh22V_Price * 0.01 * z.TotalHarga), TotalHargaLC=coalesce(z.TotalHargaLC,0)
 from        
 (        
  select POHeaderId=a.ID, a.PONumber, a.DealerID, g.DealerCode, g.DealerName,        
     a.TermOfPaymentID, h.TermOfPaymentCode, TOPDescription=h.Description,      
  i.DueDate,      
  a.status, a.FreePPh22Indicator, b.ReqQty,          
     PPh22 = CASE WHEN a.IsFactoring=1 THEN b.PPh22 ELSE  e.PPh22 END,         
  b.AllocQty, b.Price, b.Discount, b.Interest,        
     c.ContractNumber, c.PKNumber, c.DealerPKNumber, c.ProjectName,        
     SalesOrderId=d.ID, d.SONumber, d.SODate,        
     d.PaymentRef, d.SOType,         
     d.LastUpdateTime,        
     --e.VehicleColorID,        
     VehicleColorCode=j.ColorCode, j.MaterialNumber, j.MaterialDescription,    
     f.BasePrice, f.OptionPrice,         
     PPN = f.PPN,        
     PPh22V_Price = f.PPh22,        
     DiscountBeforeTax = b.Discount / 1.1,        
     NetPrice = f.BasePrice - (b.Discount / 1.1),        
     TotalHarga = ( case when a.status in(0,1,2,3)           
        then CASE WHEN a.IsFactoring=1 THEN   b.ReqQty * (f.BasePrice - (b.Discount / 1.1))  ELSE b.ReqQty*(f.BasePrice - (b.Discount / 1.1)) END          
        when a.status=5 then 0           
        else CASE WHEN a.IsFactoring=1 THEN   b.AllocQty* (f.BasePrice - (b.Discount / 1.1)) ELSE b.AllocQty * (f.BasePrice - (b.Discount / 1.1)) END           
       end),        
      TotalDeposit = ( case when a.status in(0,1,2,3)           
        then CASE WHEN a.IsFactoring=1 THEN   b.ReqQty * f.OptionPrice  ELSE b.ReqQty*f.OptionPrice END          
        when a.status=5 then 0           
        else CASE WHEN a.IsFactoring=1 THEN   b.AllocQty* f.OptionPrice ELSE b.AllocQty * f.OptionPrice END           
       end),
	   TotalHargaLC = ( case when a.status in(0,1,2,3)           
        then CASE WHEN a.IsFactoring=1 THEN   b.ReqQty * b.LogisticCost  ELSE b.ReqQty*b.LogisticCost END          
        when a.status=5 then 0           
        else CASE WHEN a.IsFactoring=1 THEN   b.AllocQty* b.LogisticCost ELSE b.AllocQty * b.LogisticCost END           
       end)        
 from POHeader a with (nolock)       
 left join PODetail b with (nolock) on a.ID = b.POHeaderID and b.RowStatus = 0        
 left join ContractHeader c with (nolock) on a.ContractHeaderID = c.ID and c.RowStatus = 0        
 left join SalesOrder d with (nolock) on a.ID = d.POHeaderID and d.RowStatus = 0        
 left join ContractDetail e with (nolock) on c.ID = e.ContractHeaderID and b.ContractDetailID = e.ID and e.RowStatus = 0        
 left join Dealer g with (nolock) on a.DealerID = g.ID and g.RowStatus = 0        
 left join TermOfPayment h with (nolock) on a.TermOfPaymentID = h.ID and h.RowStatus = 0      
 left join SalesOrderDueDate i with (nolock) on d.ID = i.SalesOrderID and i.RowStatus = 0      
 left join VechileColor j with (nolock) on j.ID = e.VehicleColorID and j.RowStatus = 0    
 CROSS APPLY fni_RetrievePriceListActive(cast(c.ContractPeriodYear as varchar)+'-'+RIGHT('00'+cast(c.ContractPeriodMonth as varchar),2)+'-'+RIGHT('00'+cast(c.ContractPeriodDay as varchar),2), e.VehicleColorID, a.DealerID) f       
 where a.RowStatus = 0         
 and d.ID is not null        
 ) z        
) a 
GO


