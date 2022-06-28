

INSERT INTO MsAppVersion
		   (Version
		   ,Description
		   ,IsCurrentDeployment
		   ,AppId
		   ,CreatedBy
		   ,CreatedTime
		   ,UpdatedBy
		   ,UpdatedTime)
OUTPUT INSERTED.VersionId
VALUES
	(@Version
	,@Description
	,@IsCurrentDeployment
	,@AppId
	,@CreatedBy
	,@CreatedTime
	,@UpdatedBy
	,@UpdatedTime);  


