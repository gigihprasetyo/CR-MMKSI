INSERT INTO [APIUserPermission]
           ([ClientUserId]
           ,[PermissionId]
           ,[IsCustomPermission]
           ,[IsDismantledPermission]
           ,[CreatedBy]
           ,[CreatedTime]
           ,[UpdatedBy]
           ,[UpdatedTime])
OUTPUT INSERTED.ID
     VALUES
           (@ClientUserId
           ,@PermissionId
           ,@IsCustomPermission
           ,0
           ,@CreatedBy
           ,@CreatedTime
           ,@UpdatedBy
           ,@UpdatedTime)
