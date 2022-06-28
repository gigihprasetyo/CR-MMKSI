 SELECT a.*
			   FROM		BSIDNET_MMKSI_GO_TEST.dbo.[AppConfig] a
			   WHERE	1 = 1
						AND [a].[RowStatus] = 0
						AND a.[Name] NOT IN ( SELECT	a.[Name]
											  FROM		dbo.[AppConfig] a
											  WHERE		1 = 1
														AND [a].[RowStatus] = 0 )
	
	
 SELECT a.*
			   FROM		BSIDNET_MMKSI_GO_TEST.dbo.[StandardCode] a
			   WHERE	1 = 1
						AND [a].[RowStatus] = 0
						AND a.[Category] NOT IN ( SELECT	a.[Category]
											  FROM		dbo.[StandardCode] a
											  WHERE		1 = 1
														AND [a].[RowStatus] = 0 )
	
	
		  		  