INSERT INTO [APIRolePermission]
           ([ClientRoleId]
           ,[PermissionId]
           ,[CreatedBy]
           ,[CreatedTime]
           ,[UpdatedBy]
           ,[UpdatedTime])
OUTPUT INSERTED.ID
     VALUES
           (@ClientRoleId
           ,@PermissionId
           ,@CreatedBy
           ,@CreatedTime
           ,@UpdatedBy
           ,@UpdatedTime)
