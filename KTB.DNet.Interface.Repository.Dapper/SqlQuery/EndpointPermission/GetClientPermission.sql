SELECT EndpointPermission.[Id]
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
FROM [APIEndpointPermission] AS EndpointPermission
INNER JOIN [APIClientPermission] AS ClientPermission ON EndpointPermission.Id = ClientPermission.PermissionId
WHERE ClientPermission.ClientId = @ClientId