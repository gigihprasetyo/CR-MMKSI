
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
  INNER JOIN [APIRolePermission] AS RolePermission ON EndpointPermission.Id = RolePermission.PermissionId
WHERE (@Keyword = '' 
		OR EndpointPermission.Name LIKE '%' + @Keyword + '%')
	AND (RolePermission.ClientRoleId = @ClientRoleId)

