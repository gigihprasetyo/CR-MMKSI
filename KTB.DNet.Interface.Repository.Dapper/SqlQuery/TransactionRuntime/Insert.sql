INSERT INTO [TransactionRuntime]
           ([TransactionLogId]
           ,[MethodName]
           ,[StartedTime]
           ,[FinishedTime]
           ,[CreatedBy]
           ,[CreatedTime]
           ,[UpdatedBy]
           ,[UpdatedTime])
OUTPUT INSERTED.ID
     VALUES
           (@TransactionLogId
           ,@MethodName
           ,@StartedTime
           ,@FinishedTime
           ,@CreatedBy
           ,@CreatedTime
           ,@UpdatedBy
           ,@UpdatedTime)


