UPDATE [APIThrottle]
   SET [EndpointId] = @EndpointId
      ,[RequestLimit] = @RequestLimit
      ,[TimeInSeconds] = @TimeInSeconds
      ,[Enable] = @Enable
      ,[CreatedBy] = @CreatedBy
      ,[CreatedTime] = @CreatedTime
      ,[UpdatedBy] = @UpdatedBy
      ,[UpdatedTime] = @UpdatedTime
 WHERE Id = @Id


