SELECT cu.[Id]
      ,cu.[ClientId]
      ,cu.[UserId]
FROM [APIClientUser] cu 
WHERE cu.ClientId = @ClientId

