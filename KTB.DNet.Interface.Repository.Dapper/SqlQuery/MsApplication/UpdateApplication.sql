UPDATE [MsApplication]
   SET [AppId] = @AppId
      ,[Name] = @Name
      ,[DeploymentJenkinsJobName] = @DeploymentJenkinsJobName
      ,[DeploymentBackupFolder] = @DeploymentBackupFolder
      ,[UpdatedBy] = @UpdatedBy
      ,[UpdatedTime] = @UpdatedTime
 WHERE AppId = @AppId


