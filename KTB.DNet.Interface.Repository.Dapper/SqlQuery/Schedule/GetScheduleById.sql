﻿SELECT [Id]
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
  FROM [APISchedule]
  WHERE Id = @Id
