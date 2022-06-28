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
           ('SparePartSO.SalesChannel'
           ,0
           ,'N/A'
           ,0
           ,'ADMIN'
           ,'2018-03-21 15:01:16.960'
           ,null
           ,null
           ,null
		   ),
		   ('SparePart.Status'
           ,0
           ,'Active'
           ,0
           ,'ADMIN'
           ,'2018-03-21 15:01:16.960'
           ,null
           ,null
           ,null
		   ),
		   ('SparePart.Status'
           ,1
           ,'Inactive'
           ,0
           ,'ADMIN'
           ,'2018-03-21 15:01:16.960'
           ,null
           ,null
           ,null
		   ),
		   ('SparePartSO.Handling'
           ,1
           ,'No Action'
           ,0
           ,'ADMIN'
           ,'2018-03-21 15:01:16.960'
           ,null
           ,null
           ,null
		   ),
		   ('SparePartSO.Handling'
           ,2
           ,'Release'
           ,0
           ,'ADMIN'
           ,'2018-03-21 15:01:16.960'
           ,null
           ,null
           ,null
		   ),
		   ('SparePartSO.Handling'
           ,3
           ,'Cancel'
           ,0
           ,'ADMIN'
           ,'2018-03-21 15:01:16.960'
           ,null
           ,null
           ,null
		   ),
		   ('SparePartSO.ShipmentType'
           ,1
           ,'Ship Complete'
           ,0
           ,'ADMIN'
           ,'2018-03-21 15:01:16.960'
           ,null
           ,null
           ,null
		   ),
		   ('SparePartSO.ShipmentType'
           ,2
           ,'Back Order Allowed'
           ,0
           ,'ADMIN'
           ,'2018-03-21 15:01:16.960'
           ,null
           ,null
           ,null
		   ),
		   ('SparePartSO.State'
           ,1
           ,'Open'
           ,0
           ,'ADMIN'
           ,'2018-03-21 15:01:16.960'
           ,null
           ,null
           ,null
		   ),
		   ('SparePartSO.State'
           ,2
           ,'Released'
           ,0
           ,'ADMIN'
           ,'2018-03-21 15:01:16.960'
           ,null
           ,null
           ,null
		   ),
		   ('SparePartSO.State'
           ,3
           ,'Canceled'
           ,0
           ,'ADMIN'
           ,'2018-03-21 15:01:16.960'
           ,null
           ,null
           ,null
		   ),
		   ('SparePartSO.State'
           ,4
           ,'Partial Delivered'
           ,0
           ,'ADMIN'
           ,'2018-03-21 15:01:16.960'
           ,null
           ,null
           ,null
		   ),
		   ('SparePartSO.State'
           ,5
           ,'Delivered'
           ,0
           ,'ADMIN'
           ,'2018-03-21 15:01:16.960'
           ,null
           ,null
           ,null
		   ),
		   ('SparePartSO.State'
           ,6
           ,'Completed'
           ,0
           ,'ADMIN'
           ,'2018-03-21 15:01:16.960'
           ,null
           ,null
           ,null
		   )


