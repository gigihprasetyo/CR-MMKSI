--========================================================================================================================                  
-- Created By: Mitrais              
-- Create SP to Retrieve API Client List 
--========================================================================================================================  
create procedure up_RetrieveAPIClientList
as 
select [ClientId]
      ,[Name]
      ,[SecretKey]
      ,[AppId]
      ,[CreatedBy]
      ,[CreatedTime]
      ,[UpdatedBy]
      ,[UpdatedTime]
 from APIClient