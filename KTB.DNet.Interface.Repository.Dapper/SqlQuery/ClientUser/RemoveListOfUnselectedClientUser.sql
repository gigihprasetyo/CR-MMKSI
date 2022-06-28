DELETE UserPermission
FROM APIUserPermission UserPermission
INNER JOIN APIClientUser ClientUser ON UserPermission.ClientUserId = ClientUser.Id
WHERE ClientUser.ClientId = @ClientId
AND ClientUser.UserId NOT IN @ListOfSelectedUserIds
	AND ClientUser.UserId != @DMSUserId

DELETE APIClientUser 
WHERE ClientId = @ClientId 
	AND UserId NOT IN @ListOfSelectedUserIds
	AND UserId != @DMSUserId
