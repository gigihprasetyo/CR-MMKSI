USE [BSIDNET_MMKSI_DMS]
GO

INSERT INTO [dbo].[StandardCode]
           ([Category]
           ,[ValueId]
           ,[ValueDesc]
           ,[RowStatus]
           ,[CreatedBy]
           ,[CreatedTime]
           ,[LastUpdateBy]
           ,[LastUpdateTime]
           ,[Sequence])
     VALUES
		   ('InventoryTransfer.ItemTypeforTransfer'
           ,0
           ,'N/A'
           ,0
           ,'ADMIN'
           ,'2018-03-21 15:01:16.960'
           ,null
           ,null
           ,null
		   ),
		   ('InventoryTransfer.ItemTypeforTransfer'
           ,1
           ,'Vehicle'
           ,0
           ,'ADMIN'
           ,'2018-03-21 15:01:16.960'
           ,null
           ,null
           ,null
		   ),
		   ('InventoryTransfer.ItemTypeforTransfer'
           ,2
           ,'Parts and Material'
           ,0
           ,'ADMIN'
           ,'2018-03-21 15:01:16.960'
           ,null
           ,null
           ,null
		   ),
		   ('InventoryTransfer.State'
           ,1
           ,'Open'
           ,0
           ,'ADMIN'
           ,'2018-03-21 15:01:16.960'
           ,null
           ,null
           ,null
		   ),
		   ('InventoryTransfer.State'
           ,2
           ,'Released'
           ,0
           ,'ADMIN'
           ,'2018-03-21 15:01:16.960'
           ,null
           ,null
           ,null
		   ),
		   ('InventoryTransfer.State'
           ,3
           ,'Canceled'
           ,0
           ,'ADMIN'
           ,'2018-03-21 15:01:16.960'
           ,null
           ,null
           ,null
		   ),
		   ('InventoryTransfer.TransactionType'
           ,1
           ,'Normal Transfer'
           ,0
           ,'ADMIN'
           ,'2018-03-21 15:01:16.960'
           ,null
           ,null
           ,null
		   ),
		   ('InventoryTransfer.TransactionType'
           ,2
           ,'WO Transfer'
           ,0
           ,'ADMIN'
           ,'2018-03-21 15:01:16.960'
           ,null
           ,null
           ,null
		   ),
		   ('InventoryTransfer.TransactionType'
           ,3
           ,'WO Return'
           ,0
           ,'ADMIN'
           ,'2018-03-21 15:01:16.960'
           ,null
           ,null
           ,null
		   ),
		   ('InventoryTransfer.TransferStatus'
           ,0
           ,'N/A'
           ,0
           ,'ADMIN'
           ,'2018-03-21 15:01:16.960'
           ,null
           ,null
           ,null
		   ),
		   ('InventoryTransfer.TransferStatus'
           ,1
           ,'In Transit'
           ,0
           ,'ADMIN'
           ,'2018-03-21 15:01:16.960'
           ,null
           ,null
           ,null
		   ),
		   ('InventoryTransfer.TransferStatus'
           ,2
           ,'Received'
           ,0
           ,'ADMIN'
           ,'2018-03-21 15:01:16.960'
           ,null
           ,null
           ,null
		   )
		   
