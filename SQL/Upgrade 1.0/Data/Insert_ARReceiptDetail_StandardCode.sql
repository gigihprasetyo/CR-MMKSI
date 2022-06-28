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
           ('ARReceiptDetail.SourceType'
           ,1
           ,'New Vehicle'
           ,0
           ,'ADMIN'
           ,'2018-03-21 15:01:16.960'
           ,null
           ,null
           ,null
		   ),
		   ('ARReceiptDetail.SourceType'
           ,2
           ,'Used Vehicle'
           ,0
           ,'ADMIN'
           ,'2018-03-21 15:01:16.960'
           ,null
           ,null
           ,null
		   ),
		   ('ARReceiptDetail.SourceType'
           ,3
           ,'Service'
           ,0
           ,'ADMIN'
           ,'2018-03-21 15:01:16.960'
           ,null
           ,null
           ,null
		   ),
		   ('ARReceiptDetail.SourceType'
           ,4
           ,'Sales Order'
           ,0
           ,'ADMIN'
           ,'2018-03-21 15:01:16.960'
           ,null
           ,null
           ,null
		   )