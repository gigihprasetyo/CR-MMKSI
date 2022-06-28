SELECT cu.[Id]
      ,cu.[ClientId]
      ,cu.[UserId]
      ,cu.[Token]
      ,cu.[TokenExpired]
      ,cu.[LastActivity]
      ,cu.[LastLogin]
      ,cu.[CreatedBy]
      ,cu.[CreatedTime]
      ,cu.[UpdatedBy]
      ,cu.[UpdatedTime]
      ,u.[Id]
      ,u.[FirstName]
      ,u.[LastName]
      ,u.[Status]
      ,u.[IsActive]
      ,u.[DealerId]
      ,u.[UserName]
      ,c.[ClientId]
      ,c.[Name]
      ,c.[AppId]
	  ,c.[SecretKey]
FROM [APIClientUser] cu 
JOIN APIUser u ON u.Id = cu.UserId
JOIN APIClient c ON c.ClientId = cu.ClientId
JOIN MsApplication app ON app.AppId = c.AppId
WHERE cu.UserId = @UserId AND app.Name LIKE '%'+@AppName+'%'

