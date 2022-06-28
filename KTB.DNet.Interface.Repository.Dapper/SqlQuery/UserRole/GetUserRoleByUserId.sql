SELECT [UserId]
      ,[RoleId]
      ,[CreatedBy]
      ,[CreatedTime]
      ,[UpdatedBy]
      ,[UpdatedTime]
  FROM [APIUserRole]
WHERE UserId = @UserId

