
 SELECT 
    /**PagingIndexQuery**/
    [Id]
      ,[Name]
      ,[ScheduleType]
      ,[ScheduleDay]
      ,[MonthDay]
      ,[ScheduleTime]
      ,[Interval]
      ,[DealerCode]
      ,[CreatedBy]
      ,[CreatedTime]
      ,[UpdatedBy]
      ,[UpdatedTime]
      /**EndPagingIndexQuery**/
FROM [APISchedule]
WHERE (@Keyword = '' OR lower(Name) like lower(@Keyword+'%'))
    OR (@Keyword = '' OR lower(DealerCode) like lower(@Keyword+'%'))
