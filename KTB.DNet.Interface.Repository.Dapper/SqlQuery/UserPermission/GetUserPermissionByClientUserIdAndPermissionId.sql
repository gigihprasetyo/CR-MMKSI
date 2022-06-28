SELECT 
	[Id]
	,[ClientUserId]
	,[PermissionId]
	,[IsCustomPermission]
	,[IsDismantledPermission]
	,[CreatedBy]
	,[CreatedTime]
	,[UpdatedBy]
	,[UpdatedTime]
FROM 
	APIUserPermission 
WHERE 
	ClientUserId = @ClientUserId AND 
	PermissionId = @PermissionId