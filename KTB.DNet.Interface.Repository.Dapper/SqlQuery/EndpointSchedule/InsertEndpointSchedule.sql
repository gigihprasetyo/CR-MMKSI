INSERT INTO [APIEndpointSchedule]
           ([EndpointId]
           ,[ScheduleId]
           ,[CreatedBy]
           ,[CreatedTime]
           ,[UpdatedBy]
           ,[UpdatedTime])
	OUTPUT Inserted.Id
    VALUES
           (@EndpointId
           ,@ScheduleId
           ,@CreatedBy
           ,@CreatedTime
           ,@UpdatedBy
           ,@UpdatedTime)

