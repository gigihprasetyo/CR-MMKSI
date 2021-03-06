--========================================================================================================================                  
-- Created By: Mitrais              
-- Create SP to Update API Client              
--========================================================================================================================  
create procedure dbo.up_UpdateMsApplication
@AppId uniqueidentifier,
@Name nvarchar(max),
@DeploymentJenkinsJobName nvarchar(max),
@DeploymentBackupFolder nvarchar(max),
@UpdatedBy nvarchar(max),
@RowCount int output
as 
	begin		
	
		update [dbo].[MsApplication] set 
			Name = @Name,
			AppId = @AppId, 
			DeploymentJenkinsJobName = @DeploymentJenkinsJobName,
			DeploymentBackupFolder = @DeploymentBackupFolder,
			UpdatedBy = @UpdatedBy,
			UpdatedTime = GETDATE()
		where AppId = @AppId
		
		set @RowCount = (select @@ROWCOUNT)
		
		if @RowCount = 0 
		print 'Warning: no records are updated'	

	end 
 