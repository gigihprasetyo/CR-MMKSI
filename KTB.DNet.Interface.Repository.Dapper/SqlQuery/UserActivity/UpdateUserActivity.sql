UPDATE [UserActivity]
   SET [Username] = @Username
      ,[Endpoint] = @Endpoint
      ,[Activity] = @Activity
      ,[ActivityTime] = @ActivityTime
      ,[ActivityDesc] = @ActivityDesc
      ,[DealerCode] = @DealerCode
      ,[AppId] = @AppId
      ,[CreatedBy] = @CreatedBy
      ,[CreatedTime] = @CreatedTime
      ,[UpdatedBy] = @UpdatedBy
      ,[UpdatedTime] = @UpdatedTime
 WHERE Id = @Id