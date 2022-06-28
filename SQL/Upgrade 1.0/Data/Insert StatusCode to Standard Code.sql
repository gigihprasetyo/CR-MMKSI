USE [BSIDNET_MMKSI_DMS]


BEGIN TRAN
IF NOT EXISTS(select * from [dbo].[StandardCode] where Category = 'LeadStatusCode')
BEGIN
  insert into [dbo].[StandardCode]  
	([Category]
      ,[ValueId]
      ,[ValueDesc]
      ,[RowStatus]
      ,[CreatedBy]
      ,[CreatedTime])
  values('LeadStatusCode', '1', 'In Progress', 0, 'ADMIN', GETDATE())

  insert into [dbo].[StandardCode]
  	([Category]
      ,[ValueId]
      ,[ValueDesc]
      ,[RowStatus]
      ,[CreatedBy]
      ,[CreatedTime])
  values('LeadStatusCode', '2', 'On Hold', 0, 'ADMIN', GETDATE())

  insert into [dbo].[StandardCode]
  	([Category]
      ,[ValueId]
      ,[ValueDesc]
      ,[RowStatus]
      ,[CreatedBy]
      ,[CreatedTime])
  values('LeadStatusCode', '3', 'Won', 0, 'ADMIN', GETDATE())

  insert into [dbo].[StandardCode]
  	([Category]
      ,[ValueId]
      ,[ValueDesc]
      ,[RowStatus]
      ,[CreatedBy]
      ,[CreatedTime])
  values('LeadStatusCode', '4', 'Canceled', 0, 'ADMIN', GETDATE())

  insert into [dbo].[StandardCode]
  	([Category]
      ,[ValueId]
      ,[ValueDesc]
      ,[RowStatus]
      ,[CreatedBy]
      ,[CreatedTime])
  values('LeadStatusCode', '5', 'Out-Sold', 0, 'ADMIN', GETDATE())

END
COMMIT TRAN