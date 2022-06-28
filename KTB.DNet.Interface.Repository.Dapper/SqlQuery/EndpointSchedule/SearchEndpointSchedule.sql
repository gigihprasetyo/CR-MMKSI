SELECT 
    /**PagingIndexQuery**/
    es.[Id]
    ,es.[EndpointId]
    ,es.[CreatedBy]
    ,es.[CreatedTime]
    ,es.[UpdatedBy]
    ,es.[UpdatedTime]
    ,es.[ScheduleId]
    ,APISchedule.[Name]
    ,APISchedule.[ScheduleType]
    ,APISchedule.[ScheduleDay]
    ,APISchedule.[MonthDay]
    ,APISchedule.[ScheduleTime]
    ,APISchedule.[Interval]
    ,APISchedule.[DealerCode]
    /**EndPagingIndexQuery**/
FROM [APIEndpointSchedule] es
-- INNER JOIN [APIEndpointPermission] ep on ep.Id = es.EndpointId
INNER JOIN [APISchedule] on APISchedule.Id = es.ScheduleId
WHERE (@EndpointId = '' OR lower(es.EndpointId) = lower(@EndpointId))
AND (@Keyword = '' OR lower(APISchedule.Name) like lower(@Keyword+'%'))
