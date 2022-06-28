UPDATE [APIEndpointSchedule]
   SET [EndpointId] = @EndpointId
      ,[ScheduleId] = @ScheduleId
      ,[CreatedBy] = @CreatedBy
      ,[CreatedTime] = @CreatedTime
      ,[UpdatedBy] = @UpdatedBy
      ,[UpdatedTime] = @UpdatedTime
 WHERE Id = @Id

