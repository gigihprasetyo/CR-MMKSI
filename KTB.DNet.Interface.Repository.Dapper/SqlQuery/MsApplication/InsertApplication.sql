INSERT INTO [MsApplication]
           ([AppId]
           ,[Name]
           ,[DeploymentJenkinsJobName]
           ,[DeploymentBackupFolder]
           ,[CreatedBy]
           ,[CreatedTime]
           ,[UpdatedBy]
           ,[UpdatedTime])
     VALUES
           (@AppId
           ,@Name
           ,@DeploymentJenkinsJobName
           ,@DeploymentBackupFolder
           ,@CreatedBy
           ,@CreatedTime
           ,@UpdatedBy
           ,@UpdatedTime)
