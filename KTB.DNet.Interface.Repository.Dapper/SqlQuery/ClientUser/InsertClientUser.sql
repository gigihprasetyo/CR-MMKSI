
INSERT INTO [APIClientUser]
           ([ClientId]
           ,[UserId]
           ,[CreatedBy]
           ,[CreatedTime]
           ,[UpdatedBy]
           ,[UpdatedTime])
OUTPUT INSERTED.ID
     VALUES
           (@ClientId
           ,@UserId
           ,@CreatedBy
           ,@CreatedTime
           ,@UpdatedBy
           ,@UpdatedTime);