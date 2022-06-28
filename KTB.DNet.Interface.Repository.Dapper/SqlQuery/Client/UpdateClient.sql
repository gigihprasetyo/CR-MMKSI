UPDATE [APIClient]
   SET [Name] = @Name
      ,[AppId] = @AppId
      ,[UpdatedBy] = @UpdatedBy
      ,[UpdatedTime] = @UpdatedTime
 WHERE ClientId = @ClientId

