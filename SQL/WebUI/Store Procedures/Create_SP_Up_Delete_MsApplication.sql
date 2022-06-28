--========================================================================================================================                  
-- Created By: Mitrais              
-- Create SP to Delete API Client              
--========================================================================================================================  
alter procedure up_DeleteMsApplication
				 @AppId uniqueidentifier 
				 
as 
begin 
		delete [dbo].[MsApplication] where AppId =   @AppId
		
end 