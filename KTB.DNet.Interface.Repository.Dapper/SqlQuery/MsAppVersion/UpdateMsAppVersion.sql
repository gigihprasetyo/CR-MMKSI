UPDATE MsAppVersion
   SET Version = @Version
	  ,Description = @Description
	  ,IsCurrentDeployment = @IsCurrentDeployment
	  ,AppId = @AppId
	  ,[UpdatedBy] = @UpdatedBy
	  ,[UpdatedTime] = @UpdatedTime
 WHERE VersionId = @VersionId


 


