
--========================================================================================================================                  
-- Created By: Mitrais              
-- Create SP to Insert API Client              
--========================================================================================================================  
create procedure [dbo].[up_InsertMsApplication] 
@Name nvarchar(max),
@DeploymentJenkinsJobName nvarchar(max),
@DeploymentBackupFolder nvarchar(max),
@CreatedBy nvarchar(max),
@UpdatedBy nvarchar(max),
@RowCount int output
as 
	begin		
	 
		insert into MsApplication
		(AppId,Name,DeploymentJenkinsJobName,DeploymentBackupFolder,CreatedBy,CreatedTime,UpdatedBy,UpdatedTime)
		values 
		(newid(), @Name , @DeploymentJenkinsJobName, @DeploymentBackupFolder, @CreatedBy, GETDATE(), @UpdatedBy, GETDATE())

		set @RowCount = (select @@ROWCOUNT)
		
		if @RowCount = 0 
		print 'Warning: no records are inserted'	

	end 
 