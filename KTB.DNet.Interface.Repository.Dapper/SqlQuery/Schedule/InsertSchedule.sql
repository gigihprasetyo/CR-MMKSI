

INSERT INTO [APISchedule] (
	[Name], 
	[ScheduleType], 
	[ScheduleDay], 
	[MonthDay], 
	[ScheduleTime], 
	[Interval], 
	[DealerCode], 
	[CreatedBy], 
	[CreatedTime])
OUTPUT Inserted.Id
VALUES (@Name ,
        @ScheduleType ,
        @ScheduleDay ,
        @MonthDay ,
        @ScheduleTime ,
        @Interval ,
        @DealerCode ,
        @CreatedBy ,
        @CreatedTime);