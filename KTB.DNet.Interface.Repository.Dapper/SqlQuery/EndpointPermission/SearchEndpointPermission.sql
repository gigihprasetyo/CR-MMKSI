
SELECT 
    /**PagingIndexQuery**/
        EndpointPermission.[Id]
      ,EndpointPermission.[Name]
      ,EndpointPermission.[PermissionCode]
      ,EndpointPermission.[URI]
	  ,EndpointPermission.[EndpointGroup]
      ,EndpointPermission.[EndpointType]
      ,EndpointPermission.[OperationType]
      ,EndpointPermission.[Description]
      ,EndpointPermission.[IsScheduled]
      ,EndpointPermission.[IsLogEnabled]
      ,EndpointPermission.[IsRuntimeLogEnabled]
      ,EndpointPermission.[CreatedBy]
      ,EndpointPermission.[CreatedTime]
      ,EndpointPermission.[UpdatedBy]
      ,EndpointPermission.[UpdatedTime]
    /**EndPagingIndexQuery**/
  FROM [APIEndpointPermission] AS EndpointPermission
WHERE (@Keyword = '' 
        OR Name LIKE '%' + @Keyword + '%')
    AND (@IsScheduled = 0 OR IsScheduled = @IsScheduled )

