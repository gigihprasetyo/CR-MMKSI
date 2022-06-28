SELECT Application.[AppId]
      ,Application.[Name]
      ,Application.[DeploymentJenkinsJobName]
      ,Application.[DeploymentBackupFolder]
  FROM [MsApplication] AS Application
  INNER JOIN [APIClient] AS Client ON Application.AppId = Client.AppId

WHERE Client.ClientId = @clientId