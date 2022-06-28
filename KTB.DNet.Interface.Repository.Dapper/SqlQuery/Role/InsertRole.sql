INSERT INTO [APIRole]
           ([Name]
           ,[Level]
           ,[CreatedBy]
           ,[CreatedTime]
           ,[UpdatedBy]
           ,[UpdatedTime])
		   OUTPUT INSERTED.ID
     VALUES
           (@Name
           ,@Level
           ,@CreatedBy
           ,@CreatedTime
           ,@UpdatedBy
           ,@UpdatedTime)

