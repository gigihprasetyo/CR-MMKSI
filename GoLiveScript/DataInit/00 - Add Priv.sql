DECLARE	@Query AS VARCHAR(MAX)= '';
WITH	DATAPriv
		  AS (
			   SELECT	'EXEC [dbo].[up_InitPriv] @Name = ''' + a.Name + ''', @Description =''' + a.Description
						+ ''',@Title=''' + a.Title + ''' ' Nama
			   FROM		BSIDNET_MMKSI_DMS_20180924_0100.dbo.[Privilege] a
			   WHERE	1 = 1
						AND [a].[RowStatus] = 0
						AND a.[Name] NOT IN ( SELECT	a.[Name]
											  FROM		dbo.[Privilege] a
											  WHERE		1 = 1
														AND [a].[RowStatus] = 0 )
			 )
	 SELECT	@Query = STUFF((SELECT	Nama + CHAR(13) + CHAR(10) + ' GO ' + CHAR(13) + CHAR(10)
							FROM	DATAPriv
			FOR			   XML PATH('') ,
							   TYPE)
                .value('.', 'NVARCHAR(MAX)'), 1, 0, '')
 

PRINT @Query

IF ISNULL(@Query, '') <> ''
   BEGIN

		 EXEC(@Query)

   END