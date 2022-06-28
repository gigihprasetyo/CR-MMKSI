INSERT INTO [UserActivity]
           ([Username]
           ,[Endpoint]
           ,[Activity]
           ,[ActivityTime]
           ,[ActivityDesc]
           ,[DealerCode]
           ,[AppId]
           ,[CreatedBy]
           ,[CreatedTime]
           ,[UpdatedBy]
           ,[UpdatedTime])
      OUTPUT INSERTED.ID
     VALUES
           (@Username
           ,@Endpoint
           ,@Activity
           ,@ActivityTime
           ,@ActivityDesc
           ,@DealerCode
           ,@AppId
           ,@CreatedBy
           ,@CreatedTime
           ,@UpdatedBy
           ,@UpdatedTime);
 