INSERT	INTO [dbo].[FSCampaign]
		(
		  [Description] ,
		  [ErrMessage] ,
		  [DateFrom] ,
		  [DateTo] ,
		  [DealerChecked] ,
		  [FSTypeChecked] ,
		  [VehicleTypeChecked] ,
		  [FakturDateChecked] ,
		  [Status] ,
		  [ExtendedFleetChecked] ,
		  [RetailChecked] ,
		  [OpenFakturDateFrom] ,
		  [OpenFakturDateTo] ,
		  [RowStatus] ,
		  [CreatedBy] ,
		  [CreatedTime] ,
		  [LastUpdateTime] ,
		  [LastUpdateBy]
		)
SELECT	[a].[Description] ,
		[a].[ErrMessage] ,
		[a].[DateFrom] ,
		[a].[DateTo] ,
		[a].[DealerChecked] ,
		[a].[FSTypeChecked] ,
		[a].[VehicleTypeChecked] ,
		[a].[FakturDateChecked] ,
		[a].[Status] ,
		[a].[ExtendedFleetChecked] ,
		[a].[RetailChecked] ,
		[a].[OpenFakturDateFrom] ,
		[a].[OpenFakturDateTo] ,
		[a].[RowStatus] ,
		'ADMIN-MIGRATION' ,
		GETDATE() ,
		GETDATE() ,
		''
FROM	[BSIDNET_KTB_FS2].[dbo].[FSCampaign] a
WHERE	a.[ID] IN ( 59, 62, 63, 64, 65, 66 )



INSERT	INTO dbo.[FSCampaignDealer]
		(
		  [CampaignID] ,
		  [DealerCode] ,
		  [RowStatus] ,
		  [CreatedTime] ,
		  [CreatedBy] ,
		  [LastUpdatedTime] ,
		  [LastUpdateBy]
		)
SELECT	b.[ID] CAmpaignID ,
		[c].[DealerCode] ,
		[c].[RowStatus] ,
		b.[CreatedTime] ,
		b.[CreatedBy] ,
		b.[LastUpdateTime] ,
		b.[LastUpdateBy]
FROM	[BSIDNET_KTB_FS2].[dbo].[FSCampaign] a
INNER JOIN [BSIDNET_KTB_FS2].dbo.[FSCampaignDealer] c ON [c].[CampaignID] = [a].[ID]
INNER JOIN dbo.[FSCampaign] b ON a.[Description] = b.[Description]
								 AND b.[CreatedBy] = 'ADMIN-MIGRATION'
WHERE	a.[ID] IN ( 59, 62, 63, 64, 65, 66 )



INSERT	INTO [dbo].[FSCampaignKind]
		(
		  [CampaignID] ,
		  [FSKindID] ,
		  [RowStatus] ,
		  [CreatedTime] ,
		  [CreatedBy] ,
		  [LastUpdateTime] ,
		  [LastUpdateBy]
		)
SELECT	b.[ID] CAmpaignID ,
		[c].[FSKindID] ,
		[c].[RowStatus] ,
		b.[CreatedTime] ,
		b.[CreatedBy] ,
		b.[LastUpdateTime] ,
		b.[LastUpdateBy]
FROM	[BSIDNET_KTB_FS2].[dbo].[FSCampaign] a
INNER JOIN [BSIDNET_KTB_FS2].dbo.[FSCampaignKind] c ON [c].[CampaignID] = [a].[ID]
INNER JOIN dbo.[FSCampaign] b ON a.[Description] = b.[Description]
								 AND b.[CreatedBy] = 'ADMIN-MIGRATION'
WHERE	a.[ID] IN ( 59, 62, 63, 64, 65, 66 )



INSERT	INTO [dbo].[FSCampaignVehicle]
		(
		  [CampaignID] ,
		  [VehicleTypeID] ,
		  [RowStatus] ,
		  [CreatedTime] ,
		  [CreatedBy] ,
		  [LastUpdateTime] ,
		  [LastUpdateBy]
		)
SELECT	b.[ID] CAmpaignID ,
		[c].[VehicleTypeID] ,
		[c].[RowStatus] ,
		b.[CreatedTime] ,
		b.[CreatedBy] ,
		b.[LastUpdateTime] ,
		b.[LastUpdateBy]
FROM	[BSIDNET_KTB_FS2].[dbo].[FSCampaign] a
INNER JOIN [BSIDNET_KTB_FS2].dbo.[FSCampaignVehicle] c ON [c].[CampaignID] = [a].[ID]
INNER JOIN dbo.[FSCampaign] b ON a.[Description] = b.[Description]
								 AND b.[CreatedBy] = 'ADMIN-MIGRATION'
WHERE	a.[ID] IN ( 59, 62, 63, 64, 65, 66 )
