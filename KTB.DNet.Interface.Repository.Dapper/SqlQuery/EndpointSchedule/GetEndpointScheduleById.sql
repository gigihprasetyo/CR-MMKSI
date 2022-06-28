SELECT 
    [APIEndpointSchedule].[Id]
    ,[APIEndpointSchedule].[EndpointId]
    ,[APIEndpointSchedule].[CreatedBy]
    ,[APIEndpointSchedule].[CreatedTime]
    ,[APIEndpointSchedule].[UpdatedBy]
    ,[APIEndpointSchedule].[UpdatedTime]
    ,[APIEndpointSchedule].[ScheduleId]
FROM [APIEndpointSchedule] 
WHERE [APIEndpointSchedule].Id = @Id
