SELECT EndpointPermission.[Id]
      ,EndpointPermission.[Name]
      ,EndpointPermission.[PermissionCode]
      ,EndpointPermission.[URI]
      ,EndpointPermission.[EndpointType]
      ,EndpointPermission.[OperationType]
      ,EndpointPermission.[Description]
      ,EndpointPermission.[IsScheduled]
      ,EndpointPermission.[CreatedBy]
      ,EndpointPermission.[CreatedTime]
      ,EndpointPermission.[UpdatedBy]
      ,EndpointPermission.[UpdatedTime]
  FROM [APIEndpointPermission] As EndpointPermission
  INNER JOIN [MsApplicationPermission] As ApplicationPermission ON EndpointPermission.Id = ApplicationPermission.PermissionId
  WHERE ApplicationPermission.AppId = @AppId