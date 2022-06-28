SELECT [Id]
      ,[ClientId]
      ,[RoleId]
      ,[CreatedBy]
      ,[CreatedTime]
      ,[UpdatedBy]
      ,[UpdatedTime]
  FROM [APIClientRole]
WHERE 
    ClientId = @ClientId AND
    RoleId = @RoleId

