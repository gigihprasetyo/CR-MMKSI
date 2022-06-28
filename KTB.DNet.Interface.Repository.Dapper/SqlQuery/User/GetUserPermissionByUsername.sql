SELECT up.[Id]
      ,up.[ClientUserId]
      ,up.[PermissionId]
      ,up.[IsCustomPermission]
      ,up.[IsDismantledPermission]
      ,up.[CreatedBy]
      ,up.[CreatedTime]
      ,up.[UpdatedBy]
      ,up.[UpdatedTime]
      ,p.Id
      ,p.PermissionCode
	  ,p.EndpointGroup
      ,p.EndpointType
      ,p.OperationType
      ,p.URI
      ,p.IsLogEnabled
      ,p.IsRuntimeLogEnabled
FROM APIUserPermission up 
JOIN APIClientUser cu ON cu.Id = up.ClientUserId
JOIN APIUser u ON u.Id = cu.UserId
JOIN APIEndpointPermission p ON p.Id = up.PermissionId 
WHERE 
  u.Username = @Username 
  AND (@ClientId = '' OR cu.ClientId = @ClientId)



