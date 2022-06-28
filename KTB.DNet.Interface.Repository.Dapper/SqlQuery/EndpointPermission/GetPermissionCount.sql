IF @IsAdmin = 1
BEGIN
	SELECT Count (1)
	FROM [APIEndpointPermission]
END
ELSE
BEGIN
	SELECT Count (Distinct PermissionId)
	FROM [APIClientPermission] AS ClientPermission 
	INNER JOIN [APIClientUser] AS ClientUser ON ClientPermission.ClientId = ClientUser.ClientId
	WHERE ClientUser.UserId = @UserId
END