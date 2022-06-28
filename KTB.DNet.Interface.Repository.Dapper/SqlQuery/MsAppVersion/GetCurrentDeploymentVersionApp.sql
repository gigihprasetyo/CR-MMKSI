SELECT 
	appVer.VersionId,
	appVer.Version,
	appVer.Description,
	appVer.IsCurrentDeployment,
	appVer.CreatedBy,
	appVer.CreatedTime,
	appVer.UpdatedBy,
	appVer.UpdatedTime,
	appVer.AppId,
	app.Name,
	app.DeploymentJenkinsJobName,
	app.DeploymentBackupFolder
FROM MsAppVersion appVer
JOIN MsApplication app ON app.AppId = appVer.appId
WHERE appVer.AppId = @AppId AND appVer.IsCurrentDeployment = 1