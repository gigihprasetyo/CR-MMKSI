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
		   ('APPaymentDetail.ExternalDocumentType'
           ,0
           ,'N/A'
           ,0
           ,'ADMIN'
           ,'2018-03-21 15:01:16.960'
           ,null
           ,null
           ,null
		   ),
           ('APPaymentDetail.ExternalDocumentType'
           ,1
           ,'Invoice'
           ,0
           ,'ADMIN'
           ,'2018-03-21 15:01:16.960'
           ,null
           ,null
           ,null
		   ),
		   ('APPaymentDetail.ExternalDocumentType'
           ,2
           ,'Delivery Order'
           ,0
           ,'ADMIN'
           ,'2018-03-21 15:01:16.960'
           ,null
           ,null
           ,null
		   ),
		   
		   ('APPaymentDetail.SourceType'
           ,0
           ,'N/A'
           ,0
           ,'ADMIN'
           ,'2018-03-21 15:01:16.960'
           ,null
           ,null
           ,null
		   ),
		   ('APPaymentDetail.SourceType'
           ,1
           ,'Purchase Order'
           ,0
           ,'ADMIN'
           ,'2018-03-21 15:01:16.960'
           ,null
           ,null
           ,null
		   ),
		   ('APPaymentDetail.SourceType'
           ,4
           ,'AP Voucher'
           ,0
           ,'ADMIN'
           ,'2018-03-21 15:01:16.960'
           ,null
           ,null
           ,null
		   ),
		   ('APPaymentDetail.SourceType'
           ,5
           ,'Outsource Work Order'
           ,0
           ,'ADMIN'
           ,'2018-03-21 15:01:16.960'
           ,null
           ,null
           ,null
		   )


