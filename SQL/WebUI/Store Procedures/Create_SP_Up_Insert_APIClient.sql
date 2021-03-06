
--========================================================================================================================                  
-- Created By: Mitrais              
-- Create SP to Insert API Client              
--========================================================================================================================  
create procedure [dbo].[up_InsertAPIClient] 
@Name nvarchar(max),
@AppId uniqueidentifier,
@CreatedBy nvarchar(max),
@UpdatedBy nvarchar(max),
@RowCount int output
as 
	begin		
	 
		insert into APICLient (ClientId, Name, AppId, SecretKey, CreatedBy, CreatedTime, UpdatedBy, UpdatedTime)
		values 
		(newid(), @Name , @AppId, newid(), @CreatedBy, GETDATE(), @UpdatedBy, GETDATE() )

		set @RowCount = (select @@ROWCOUNT)
		
		if @RowCount = 0 
		print 'Warning: no records are inserted'	

	end 
 