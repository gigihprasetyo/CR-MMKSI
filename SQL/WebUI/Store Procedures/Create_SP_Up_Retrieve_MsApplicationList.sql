--========================================================================================================================                  
-- Created By: Mitrais              
-- Create SP to Retrieve MsApplication List 
--========================================================================================================================  
create procedure up_RetrieveMsApplicationList
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