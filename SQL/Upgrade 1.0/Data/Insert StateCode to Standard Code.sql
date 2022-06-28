USE [BSIDNET_MMKSI_DMS]


BEGIN TRAN
IF NOT EXISTS(select * from [dbo].[StandardCode] where Category = 'LeadStateCode')
BEGIN
  insert into [dbo].[StandardCode]  
	([Category]
      ,[ValueId]
      ,[ValueDesc]
      ,[RowStatus]
      ,[CreatedBy]
      ,[CreatedTime])
  values('LeadStateCode', '0', 'Open', 0, 'ADMIN', GETDATE())

  insert into [dbo].[StandardCode]
  	([Category]
      ,[ValueId]
      ,[ValueDesc]
      ,[RowStatus]
      ,[CreatedBy]
      ,[CreatedTime])
  values('LeadStateCode', '1', 'Won', 0, 'ADMIN', GETDATE())

  insert into [dbo].[StandardCode]
  	([Category]
      ,[ValueId]
      ,[ValueDesc]
      ,[RowStatus]
      ,[CreatedBy]
      ,[CreatedTime])
  values('LeadStateCode', '2', 'Lost', 0, 'ADMIN', GETDATE())

END
COMMIT TRAN