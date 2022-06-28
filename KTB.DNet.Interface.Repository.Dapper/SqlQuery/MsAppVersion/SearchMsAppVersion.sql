SELECT 
	/**PagingIndexQuery**/
	MsAppVersion.VersionId,
	MsAppVersion.Version,
	MsAppVersion.Description,
	MsAppVersion.IsCurrentDeployment,
	MsAppVersion.CreatedBy,
	MsAppVersion.CreatedTime,
	MsAppVersion.UpdatedBy,
	MsAppVersion.UpdatedTime,
	MsAppVersion.AppId,
	MsApplication.Name,
	MsApplication.DeploymentJenkinsJobName,
	MsApplication.DeploymentBackupFolder
	/**EndPagingIndexQuery**/
FROM MsAppVersion
JOIN MsApplication ON MsApplication.AppId = MsAppVersion.appId
WHERE 
	@Keyword = '' OR 
	MsAppVersion.Version LIKE '%'+@Keyword+'%' OR
	MsApplication.Name LIKE '%'+@Keyword+'%'

