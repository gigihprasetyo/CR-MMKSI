--========================================================================================================================                  
-- Created By: Mitrais              
-- Create SP to Retrieve MsApplication 
--========================================================================================================================  
create procedure up_RetrieveMsApplication
@appId uniqueidentifier 
as 
select [AppId]
      ,[Name]
      ,[DeploymentJenkinsJobName]
      ,[DeploymentBackupFolder]
      ,[CreatedBy]
      ,[CreatedTime]
      ,[UpdatedBy]
      ,[UpdatedTime]
from [dbo].[MsApplication]
where [AppId] = @appId
