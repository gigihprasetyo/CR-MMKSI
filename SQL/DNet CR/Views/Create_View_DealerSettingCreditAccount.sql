CREATE view [dbo].[VWI_DealerSettingCreditAccount]
as	
select t.id, DealerId = d.id, d.RowStatus, d.DealerCode, d.DealerName, c.CityName, ProvinceId= p.id, p.ProvinceName,DealerGroupId = dg.Id, dg.GroupName, d.SearchTerm1, t.Status, d.SalesUnitFlag, d.ServiceFlag, d.SparepartFlag, t.LastUpdateTime, t.TermOfPaymentID, d.CreditAccount, t.KelipatanPembayaran  from Dealer d
left join City c on c.id = d.CityID
left join Province p on p.id = d.ProvinceID
left join DealerGroup dg on dg.ID = d.DealerGroupID
left join TOPCreditAccount t on d.id = t.DealerID where d.RowStatus = 0 
GO
