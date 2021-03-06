--========================================================================================================================                  
-- Created By: Mitrais              
-- Create SP to Update API Client              
--========================================================================================================================  
create procedure dbo.up_UpdateAPIClient 
@ClientId uniqueidentifier, 
@Name nvarchar(max),
@AppId uniqueidentifier,
@SecretKey uniqueidentifier,
@UpdatedBy nvarchar(max),
@RowCount int output
as 
	begin		
	
		update [dbo].[APICLient] set 
			Name = @Name,
			AppId = @AppId, 
			SecretKey = @SecretKey,
			UpdatedBy = @UpdatedBy,
			UpdatedTime = GETDATE()
		where ClientId = @ClientId 
		
		set @RowCount = (select @@ROWCOUNT)
		
		if @RowCount = 0 
		print 'Warning: no records are updated'	

	end 
 