SELECT 
    es.[Id]
    ,es.[EndpointId]
    ,es.[CreatedBy]
    ,es.[CreatedTime]
    ,es.[UpdatedBy]
    ,es.[UpdatedTime]
    ,es.[ScheduleId]
    ,s.[Id]
    ,s.[Name]
    ,s.[ScheduleType]
    ,s.[ScheduleDay]
    ,s.[MonthDay]
    ,s.[ScheduleTime]
    ,s.[Interval]
    ,s.[DealerCode]
FROM [APIEndpointSchedule] es
INNER JOIN [APISchedule] s on s.Id = es.ScheduleId
WHERE (@EndpointId = '' OR lower(es.EndpointId) = lower(@EndpointId))
