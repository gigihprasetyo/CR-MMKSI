
SELECT 
	appVer.VersionId,
	appVer.Version,
	appVer.Description,
	appVer.IsCurrentDeployment,
	appVer.CreatedBy,
	appVer.CreatedTime,
	appVer.UpdatedBy,
	appVer.UpdatedTime,
	appVer.AppId
FROM MsAppVersion appVer


