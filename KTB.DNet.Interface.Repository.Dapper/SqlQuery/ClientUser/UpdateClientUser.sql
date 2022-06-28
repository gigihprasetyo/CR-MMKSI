UPDATE [APIClientUser]
   SET 
       [Token] = @Token
      ,[TokenExpired] = @TokenExpired
      ,[LastActivity] = @LastActivity
      ,[LastLogin] = @LastLogin
      ,[UpdatedBy] = @UpdatedBy
      ,[UpdatedTime] = @UpdatedTime
 WHERE Id = @Id
