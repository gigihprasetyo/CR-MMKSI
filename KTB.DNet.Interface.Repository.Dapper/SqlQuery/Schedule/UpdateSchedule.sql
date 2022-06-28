UPDATE [APISchedule]
SET [Name] = @Name ,
    [ScheduleType]= @ScheduleType ,
    [ScheduleDay]= @ScheduleDay ,
    [MonthDay]= @MonthDay ,
    [ScheduleTime]= @ScheduleTime ,
    [Interval]= @Interval ,
    [DealerCode]= @DealerCode,
    [UpdatedBy]= @UpdatedBy ,
    [UpdatedTime]= @UpdatedTime
WHERE Id = @Id