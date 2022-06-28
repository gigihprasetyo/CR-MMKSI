USE [BSIDNET_MMKSI_CR_IR]
GO

INSERT INTO [dbo].[Leasing]
           ([LeasingGroupName]
           ,[LeasingCode]
           ,[LeasingName]
           ,[City]
           ,[Alamat]
           ,[Kelurahan]
           ,[Kecamatan]
           ,[Province]
           ,[PostalCode]
           ,[PhoneNo]
           ,[Fax]
           ,[WebSite]
           ,[Email]
           ,[ContactPerson]
           ,[HP]
           ,[Status]
           ,[RowStatus]
           ,[CreatedBy]
           ,[CreatedTime]
           ,[LastUpdateBy]
           ,[LastUpdateTime])
     VALUES
           ('DIPO STAR FINANCE'
           ,'L00001'
           ,'PT DIPO STAR FINANCE'
           ,'JAKARTA PUSAT'
           ,'SENTRAL SENAYAN 2, 3RD FLOOR JL. ASIA AFRIKA NO.8, SENAYAN'
           ,''
           ,''
           ,'DKI JAKARTA'
           ,'10270'
           ,'021-57954100'
           ,''
           ,''
           ,''
           ,''
           ,''
           ,1
           ,0
           ,'WSM'
           ,'2018-05-11 16:36:47.397'
           ,'WS'
           ,'2018-05-11 16:36:47.397')
GO


select * from Leasing