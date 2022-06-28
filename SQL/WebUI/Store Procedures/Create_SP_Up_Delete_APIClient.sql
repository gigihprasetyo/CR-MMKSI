--========================================================================================================================                  
-- Created By: Mitrais              
-- Create SP to Delete API Client              
--========================================================================================================================  
create procedure up_DeleteAPIClient
				 @ClientId uniqueidentifier, 
				 @RowCount int output
as 
begin 
		delete [dbo].[APIClient] where ClientId =   @ClientId 
		
		set @RowCount = (select @@ROWCOUNT)
		
		if @RowCount = 0 
		print 'Warning: no records are deleted'	
end 