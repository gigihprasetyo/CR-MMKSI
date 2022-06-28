SELECT
/**PagingIndexQuery**/
	APIClient.ClientId, APIClient.Name, MsApplication.AppId, MsApplication.Name AS [MsApplication.Name]
/**EndPagingIndexQuery**/
FROM APIClient
LEFT JOIN MsApplication ON APIClient.AppId = MsApplication.AppId
WHERE @Keyword = '' OR 
	(APIClient.Name LIKE '%' + @Keyword + '%' OR
	 MsApplication.Name LIKE '%' + @Keyword + '%')