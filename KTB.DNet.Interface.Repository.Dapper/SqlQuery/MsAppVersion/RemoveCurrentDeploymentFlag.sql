UPDATE MsAppVersion
   SET IsCurrentDeployment = 0
	  ,[UpdatedBy] = @UpdatedBy
	  ,[UpdatedTime] = @UpdatedTime
 WHERE AppId = @AppId


 


