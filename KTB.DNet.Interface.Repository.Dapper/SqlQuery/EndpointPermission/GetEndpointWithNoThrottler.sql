/****** Script for SelectTopNRows command from SSMS  ******/
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
  LEFT JOIN [APIThrottle] AS Throttle ON EndpointPermission.Id = Throttle.EndpointId
  WHERE Throttle.EndpointId IS NULL
  ORDER BY EndpointPermission.PermissionCode