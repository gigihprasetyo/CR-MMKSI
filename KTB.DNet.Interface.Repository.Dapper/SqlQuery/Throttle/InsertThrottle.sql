INSERT INTO [APIThrottle]
           ([EndpointId]
           ,[RequestLimit]
           ,[TimeInSeconds]
           ,[Enable]
           ,[CreatedBy]
           ,[CreatedTime]
           ,[UpdatedBy]
           ,[UpdatedTime])
      OUTPUT INSERTED.ID
     VALUES
           (@EndpointId
           ,@RequestLimit
           ,@TimeInSeconds
           ,@Enable
           ,@CreatedBy
           ,@CreatedTime
           ,@UpdatedBy
           ,@UpdatedTime)


