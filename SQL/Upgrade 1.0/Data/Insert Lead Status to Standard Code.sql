USE [BSIDNET_MMKSI_DMS]


BEGIN TRAN
IF NOT EXISTS(select * from [dbo].[StandardCode] where Category = 'LeadStatus')
BEGIN
  insert into [dbo].[StandardCode]  
	([Category]
      ,[ValueId]
      ,[ValueDesc]
      ,[RowStatus]
      ,[CreatedBy]
      ,[CreatedTime])
  values('LeadStatus', '0', 'New', 0, 'ADMIN', GETDATE())

  insert into [dbo].[StandardCode]
  	([Category]
      ,[ValueId]
      ,[ValueDesc]
      ,[RowStatus]
      ,[CreatedBy]
      ,[CreatedTime])
  values('LeadStatus', '1', 'Contacted', 0, 'ADMIN', GETDATE())

  insert into [dbo].[StandardCode] 
  	([Category]
      ,[ValueId]
      ,[ValueDesc]
      ,[RowStatus]
      ,[CreatedBy]
      ,[CreatedTime])
  values('LeadStatus', '2', 'Qualified', 0, 'ADMIN', GETDATE())

  insert into [dbo].[StandardCode]
    	([Category]
      ,[ValueId]
      ,[ValueDesc]
      ,[RowStatus]
      ,[CreatedBy]
      ,[CreatedTime])
  values('LeadStatus', '3', 'Lost', 0, 'ADMIN', GETDATE())

  insert into [dbo].[StandardCode]
  	([Category]
      ,[ValueId]
      ,[ValueDesc]
      ,[RowStatus]
      ,[CreatedBy]
      ,[CreatedTime])
  values('LeadStatus', '4', 'Cannot Contact', 0, 'ADMIN', GETDATE())

  insert into [dbo].[StandardCode]
    	([Category]
      ,[ValueId]
      ,[ValueDesc]
      ,[RowStatus]
      ,[CreatedBy]
      ,[CreatedTime])
  values('LeadStatus', '5', 'No Longer Interested', 0, 'ADMIN', GETDATE())

  insert into [dbo].[StandardCode]
    	([Category]
      ,[ValueId]
      ,[ValueDesc]
      ,[RowStatus]
      ,[CreatedBy]
      ,[CreatedTime])
  values('LeadStatus', '6', 'Cancelled', 0, 'ADMIN', GETDATE())
END
COMMIT TRAN