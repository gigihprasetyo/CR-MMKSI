USE [BSIDNET_MMKSI_CR_IR]
GO

INSERT INTO [dbo].[Karoseri]
           ([Code]
           ,[Name]
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
           ('K00001'
           ,'CV DELIMAJAYA'
           ,'BOGOR'
           ,'JL. KH SOLEH ISKANDAR, NO. 5'
           ,''
           ,''
           ,'JAWA BARAT'
           ,00000
           ,'0251-8654300'
           ,''
           ,''
           ,'INFO@DELIMAJAYACARROSSERIE.COM'
           ,'WINSTON WIYA'
           ,'081511289899, 081318'
           ,1
           ,0
           ,'WSM'
           ,'2018-05-11 16:36:47.577'
           ,'WS'
           ,'2018-05-11 16:36:47.577')
GO
select * from Karoseri

