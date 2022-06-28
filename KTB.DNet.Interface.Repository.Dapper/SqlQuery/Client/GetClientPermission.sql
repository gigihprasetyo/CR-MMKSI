SELECT
	endpoint.Id,
	endpoint.Name,
	endpoint.PermissionCode
FROM 
	APIClientPermission cp
	JOIN APIEndpointPermission endpoint ON cp.PermissionId = endpoint.Id
WHERE cp.ClientId = @clientId
