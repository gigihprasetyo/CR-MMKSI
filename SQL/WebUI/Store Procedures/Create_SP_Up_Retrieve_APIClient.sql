--========================================================================================================================                  
-- Created By: Mitrais              
-- Create SP to Retrieve API Client     
--========================================================================================================================  
create procedure up_RetrieveAPIClient
@clientId uniqueidentifier 
as 
select [ClientId]
      ,[Name]
      ,[SecretKey]
      ,[AppId]
      ,[CreatedBy]
      ,[CreatedTime]
      ,[UpdatedBy]
      ,[UpdatedTime]
 from APIClient where ClientId = @clientId