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
           ('APPayment.Type'
           ,1
           ,'Payment'
           ,0
           ,'ADMIN'
           ,'2018-03-21 15:01:16.960'
           ,null
           ,null
           ,null
		   ),
		   ('APPayment.Type'
           ,2
           ,'Down Payment'
           ,0
           ,'ADMIN'
           ,'2018-03-21 15:01:16.960'
           ,null
           ,null
           ,null
		   ),
		   ('APPayment.Type'
           ,3
           ,'Settlement'
           ,0
           ,'ADMIN'
           ,'2018-03-21 15:01:16.960'
           ,null
           ,null
           ,null
		   ),
		   ('APPayment.Type'
           ,4
           ,'Cancellation'
           ,0
           ,'ADMIN'
           ,'2018-03-21 15:01:16.960'
           ,null
           ,null
           ,null
		   ),
		   ('APPayment.State'
           ,1
           ,'Open'
           ,0
           ,'ADMIN'
           ,'2018-03-21 15:01:16.960'
           ,null
           ,null
           ,null
		   ),
		   ('APPayment.State'
           ,2
           ,'Released'
           ,0
           ,'ADMIN'
           ,'2018-03-21 15:01:16.960'
           ,null
           ,null
           ,null
		   )


