SELECT up.[Id]
      ,up.[ClientUserId]
      ,up.[PermissionId]
      ,up.[IsCustomPermission]
      ,up.[IsDismantledPermission]
      ,up.[CreatedBy]
      ,up.[CreatedTime]
      ,up.[UpdatedBy]
      ,up.[UpdatedTime]
      ,p.[Id]
      ,p.[Name]
      ,p.[PermissionCode]
      ,p.[EndpointType]
      ,p.[OperationType]
FROM [APIUserPermission] up
JOIN APIEndpointPermission p ON p.Id = up.PermissionId
WHERE ClientUserId = @ClientUserId
ORDER BY p.PermissionCode

