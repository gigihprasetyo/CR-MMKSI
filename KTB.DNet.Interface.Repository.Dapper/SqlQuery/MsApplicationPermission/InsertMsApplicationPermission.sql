INSERT INTO [MsApplicationPermission]
           ([AppId]
           ,[PermissionId]
           ,[CreatedBy]
           ,[CreatedTime]
           ,[UpdatedBy]
           ,[UpdatedTime])
OUTPUT INSERTED.ID
     VALUES
           (@AppId
           ,@PermissionId
           ,@CreatedBy
           ,@CreatedTime
           ,@UpdatedBy
           ,@UpdatedTime)
