
SELECT 
    /**PagingIndexQuery**/
		[APIThrottle].[Id]
      ,[APIThrottle].[EndpointId] -- Endpoint Permission Id
      ,[APIThrottle].[RequestLimit]
      ,[APIThrottle].[TimeInSeconds]
      ,[APIThrottle].[Enable]
      ,[APIThrottle].[CreatedBy]
      ,[APIThrottle].[CreatedTime]
      ,[APIThrottle].[UpdatedBy]
      ,[APIThrottle].[UpdatedTime]
      ,[APIEndpointPermission].[Name]
      ,[APIEndpointPermission].[PermissionCode]
      ,[APIEndpointPermission].[URI]
      ,[APIEndpointPermission].[EndpointType]
      ,[APIEndpointPermission].[OperationType]
      ,[APIEndpointPermission].[Description]
      ,[APIEndpointPermission].[IsScheduled]
	/**EndPagingIndexQuery**/
  FROM [APIThrottle]
  INNER JOIN [APIEndpointPermission] ON [APIThrottle].EndpointId = [APIEndpointPermission].Id 
WHERE (@Keyword = '' 
		OR [APIEndpointPermission].Name LIKE '%' + @Keyword + '%')
	

