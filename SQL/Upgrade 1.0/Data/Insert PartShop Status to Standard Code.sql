USE [BSIDNET_MMKSI_DMS]

BEGIN TRAN
IF NOT EXISTS(select * from [dbo].[StandardCode] where Category = 'PartShop.Status')
BEGIN
  insert into [dbo].[StandardCode]  
	([Category]
      ,[ValueId]
      ,[ValueDesc]
      ,[RowStatus]
      ,[CreatedBy]
      ,[CreatedTime])
  values('PartShop.Status', '0', 'Baru', 0, 'ADMIN', GETDATE())

  insert into [dbo].[StandardCode]
  	([Category]
      ,[ValueId]
      ,[ValueDesc]
      ,[RowStatus]
      ,[CreatedBy]
      ,[CreatedTime])
  values('PartShop.Status', '1', 'Request ID', 0, 'ADMIN', GETDATE())

  insert into [dbo].[StandardCode]
  	([Category]
      ,[ValueId]
      ,[ValueDesc]
      ,[RowStatus]
      ,[CreatedBy]
      ,[CreatedTime])
  values('PartShop.Status', '2', 'Aktif', 0, 'ADMIN', GETDATE())

  insert into [dbo].[StandardCode]
  	([Category]
      ,[ValueId]
      ,[ValueDesc]
      ,[RowStatus]
      ,[CreatedBy]
      ,[CreatedTime])
  values('PartShop.Status', '3', 'Tidak Aktif', 0, 'ADMIN', GETDATE())

END
COMMIT TRAN