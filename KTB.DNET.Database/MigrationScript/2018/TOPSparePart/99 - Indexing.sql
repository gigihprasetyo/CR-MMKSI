CREATE INDEX [IX_SparePartBilling_BillingNumber_RowStatus] ON [dbo].[SparePartBilling] ([BillingNumber], [RowStatus]) INCLUDE ([BillingDate], [TotalAmount], [Tax])


CREATE INDEX [IX_SparePartBillingDetail_SparePartBillingID_RowStatus] ON [dbo].[SparePartBillingDetail] ([SparePartBillingID], [RowStatus]) INCLUDE ([ID], [BillingItemNo], [SparePartDODetailID], [Quantity], [ItemPrice], [TotalPrice], [Tax], [CreatedBy], [CreatedTime], [LastUpdateBy], [LastUpdateTime])



CREATE INDEX [IX_SparePartPOEstimateDetail_RowStatus] ON [dbo].[SparePartPOEstimateDetail] ([RowStatus]) INCLUDE ([SparePartPOEstimateID], [AllocQty], [RetailPrice], [Discount])



CREATE INDEX [IX_SparePartBillingDetail_SparePartBillingID] ON [dbo].[SparePartBillingDetail] ([SparePartBillingID]) INCLUDE ([ID], [BillingItemNo], [SparePartDODetailID], [Quantity], [ItemPrice], [TotalPrice], [Tax], [RowStatus], [CreatedBy], [CreatedTime], [LastUpdateBy], [LastUpdateTime])