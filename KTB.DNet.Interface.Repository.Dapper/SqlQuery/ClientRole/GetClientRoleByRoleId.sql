SELECT [Id]
      ,[ClientId]
      ,[RoleId]
      ,[CreatedBy]
      ,[CreatedTime]
      ,[UpdatedBy]
      ,[UpdatedTime]
  FROM [APIClientRole]
WHERE 
    RoleId = @RoleId

