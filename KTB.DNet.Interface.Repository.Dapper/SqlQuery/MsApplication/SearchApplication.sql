SELECT 
    /**PagingIndexQuery**/
      [AppId]
      ,[Name]
      ,[DeploymentJenkinsJobName]
      ,[DeploymentBackupFolder]
      ,[CreatedBy]
      ,[CreatedTime]
      ,[UpdatedBy]
      ,[UpdatedTime]
    /**EndPagingIndexQuery**/
FROM [MsApplication]
WHERE @Keyword = '' OR Name LIKE '%' + @Keyword + '%'
