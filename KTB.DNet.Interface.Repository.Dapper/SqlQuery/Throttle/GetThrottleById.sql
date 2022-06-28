SELECT Throttle.[Id]
      ,Throttle.[EndpointId]
      ,Throttle.[RequestLimit]
      ,Throttle.[TimeInSeconds]
      ,Throttle.[Enable]
      ,Throttle.[CreatedBy]
      ,Throttle.[CreatedTime]
      ,Throttle.[UpdatedBy]
      ,Throttle.[UpdatedTime]
      
      ,EndpointPermission.[Name]
      ,EndpointPermission.[PermissionCode]
      ,EndpointPermission.[URI]
      ,EndpointPermission.[EndpointType]
      ,EndpointPermission.[OperationType]
      ,EndpointPermission.[Description]
      ,EndpointPermission.[IsScheduled]
     
  FROM [APIThrottle] AS Throttle
  INNER JOIN [APIEndpointPermission] AS EndpointPermission ON Throttle.EndpointId = EndpointPermission.Id 
  WHERE Throttle.Id = @Id