  
 

BEGIN TRANSACTION

INSERT  INTO [dbo].[MSPCustomer]
        ( [RefCustomerID] ,
          [Name1] ,
          [Name2] ,
          [Name3] ,
          [Alamat] ,
          [Kelurahan] ,
          [Kecamatan] ,
          [ProvinceID] ,
          [PostalCode] ,
          [PreArea] ,
          [CityID] ,
          [PrintRegion] ,
          [PhoneNo] ,
          [Email] ,
          [Attachment] ,
          [Status] ,
          [DeletionMark] ,
          [CompleteName] ,
          [KTPNo] ,
          [Age] ,
          [RowStatus] ,
          [CreatedBy] ,
          [CreatedTime] ,
          [LastUpdateBy] ,
          [LastUpdateTime]
		)
        SELECT  NULL , -- RefCustomerID - int
                a.[Nama Konsumen] , -- Name1 - varchar(100)
                '' , -- Name2 - varchar(100)
                '' , -- Name3 - varchar(50)
                a.[Alamat] , -- Alamat - varchar(100)
                '' , -- Kelurahan - varchar(50)
                '' , -- Kecamatan - varchar(50)
                C.[ProvinceID] , -- ProvinceID - int
                '' , -- PostalCode - varchar(10)
                '' , -- PreArea - varchar(20)
                CityID = C.[ID] , -- CityID - smallint
                '' , -- PrintRegion - varchar(1)
                '' , -- PhoneNo - varchar(30)
                '' , -- Email - varchar(50)
                '' , -- Attachment - varchar(250)
                0 , -- Status - smallint
                0 , -- DeletionMark - smallint
                '' , -- CompleteName - varchar(100)
                '' , -- KTPNo - varchar(100)
                a.[Usia] , -- Age - int
                0 , -- RowStatus - smallint
                'admin-MIGRATION' , -- CreatedBy - varchar(20)
                GETDATE() , -- CreatedTime - datetime
                a.[No Rangka] , -- LastUpdateBy - varchar(20)
                GETDATE()  -- LastUpdateTime - datetime ,
        FROM    [dbo].[MSPCustomer2] a ( NOLOCK )
                INNER JOIN dbo.[Dealer] b ON [a].[Kode Dealer ] = b.[DealerCode]
                OUTER APPLY ( SELECT TOP 1
                                        C.* ,
                                        d.[ProvinceName]
                              FROM      [dbo].[City] C
                                        INNER JOIN dbo.[Province] d ON [d].[ID] = [C].[ProvinceID]
                              WHERE     C.[CityName] LIKE '%' + a.[Kota] + '%'
                                        OR a.[Kota] LIKE '%' + C.[CityName]
                                        + '%'
                              ORDER BY  [C].[ID] DESC
                            ) C
--INNER JOIN dbo.[ChassisMaster] b (NOLOCK) ON a.[No Rangka] = b.[ChassisNumber]
        WHERE   1 = 1
		--AND b.[RowStatus] = 0



--ROLLBACK
--COMMIT TRANSACTION

DECLARE @LastMSPNUmber VARCHAR(100)
DECLARE @Num AS INTEGER
SET @LastMSPNUmber = ( SELECT TOP 1
                                a.MSPCode
                       FROM     dbo.[MSPRegistration] a
                       ORDER BY a.[ID] DESC
                     )
 

SET @Num = CONVERT(INT, REPLACE(@LastMSPNUmber, 'MSP', ''))

SELECT  @Num ,
        @LastMSPNUmber;
WITH    ISYEMSP
          AS ( SELECT   c.[No Rangka] ,
                        ROW_NUMBER() OVER ( ORDER BY [b].[ChassisNumber] ASC ) AS RowN ,
                        a.[ID] MSPCustomerID , -- MSPCustomerID - int
                        d.[ID] DealerID , -- DealerID - int
                        b.[ID] ChassisMasterID , -- ChassisMasterID - int
                        '' OldMSPCode , -- OldMSPCode - varchar(10)
                        0 RowStatus , -- RowStatus - smallint
                        '' CreatedBy , -- CreatedBy - varchar(20)
                        GETDATE() CreatedTime , -- CreatedTime - datetime
                        '' LastUpdateBy , -- LastUpdateBy - varchar(20)
                        GETDATE() LastUpdateTime-- LastUpdateTime - datetime
               FROM     [dbo].[MSPCustomer] a ( NOLOCK )
                        INNER JOIN dbo.[ChassisMaster] b ON b.[ChassisNumber] = a.[LastUpdateBy]
                                                            AND b.[RowStatus] = 0
                        INNER JOIN dbo.[MSPCustomer2] c ON a.[LastUpdateBy] = c.[No Rangka]
                        INNER JOIN dbo.[Dealer] d ON d.[DealerCode] = c.[Kode Dealer ]
               WHERE    a.[CreatedBy] = 'admin-MIGRATION'
--ORDER BY ID DESC
             ),
        MSPClear
          AS ( SELECT   [a].[No Rangka] ,
                        NEWMSPCODE = 'MSP' + RIGHT('0000000'
                                                   + CONVERT(VARCHAR(7), ( @Num
                                                              + [a].[RowN] )),
                                                   7) ,
                        [a].[MSPCustomerID] ,
                        [a].[DealerID] ,
                        [a].[ChassisMasterID] ,
                        [a].[OldMSPCode] ,
                        [a].[RowStatus] ,
                        [a].[CreatedBy] ,
                        [a].[CreatedTime] ,
                        [a].[LastUpdateBy] ,
                        [a].[LastUpdateTime]
               FROM     [ISYEMSP] a
             )
    INSERT  INTO [dbo].[MSPRegistration]
            ( [MSPCustomerID] ,
              [DealerID] ,
              [ChassisMasterID] ,
              [MSPCode] ,
              [OldMSPCode] ,
              [RowStatus] ,
              [CreatedBy] ,
              [CreatedTime] ,
              [LastUpdateBy] ,
              [LastUpdateTime]
			)
            SELECT  [a].[MSPCustomerID] ,
                    [a].[DealerID] ,
                    [a].[ChassisMasterID] ,
                    [a].[NEWMSPCODE] ,
                    '' ,
                    [a].[RowStatus] ,
                    'ADMIN-MIGRATION' ,
                    [a].[CreatedTime] ,
                    a.[No Rangka] ,
                    [a].[LastUpdateTime]
            FROM    MSPClear a


	 --UPDATE dbo.[MSPRegistration] 
		--SET [CreatedBy]='ADMIN-MIGRATION'
		--WHERE [CreatedTime]>='20180907'



INSERT  INTO [dbo].[MSPRegistrationHistory]
        ( [MSPRegistrationID] ,
          [MSPMasterID] ,
          [BenefitMasterHeaderID] ,
          [RegistrationDate] ,
          [RequestType] ,
          [Status] ,
          [PrintedBy] ,
          [PrintedDate] ,
          [IsDownloadCertificate] ,
          [Soldby] ,
          [IsTransferToSF] ,
          [SFDate] ,
          [DebitChargeNo] ,
          [SelisihAmount] ,
          [Sequence] ,
          [RowStatus] ,
          [CreatedBy] ,
          [CreatedTime] ,
          [LastUpdateBy] ,
          [LastUpdateTime]
		)
 SELECT   a.ID , -- MSPRegistrationID - int
          x.ID , -- MSPMasterID - int
          x.bfid , -- BenefitMasterHeaderID - int
         x.[Tgl Join MSP] , -- RegistrationDate - datetime
          0 , -- RequestType - varchar(50)
          6 , -- Status - smallint
        x.[Dijual Oleh], -- PrintedBy - varchar(20)
          NULL , -- PrintedDate - datetime
          0 , -- IsDownloadCertificate - bit
          N'' , -- Soldby - nvarchar(20)
          NULL , -- IsTransferToSF - bit
        NULL , -- SFDate - datetime
          '' , -- DebitChargeNo - varchar(18)
          0 , -- SelisihAmount - money
          0 , -- Sequence - smallint
          0 , -- RowStatus - smallint
          'ADMIN-MIGRATION' , -- CreatedBy - varchar(20)
          GETDATE() , -- CreatedTime - datetime
          '' , -- LastUpdateBy - varchar(20)
          GETDATE()  -- LastUpdateTime - datetime
		 
FROM    dbo.[MSPRegistration] a
        INNER JOIN dbo.[MSPCustomer] b ON a.[MSPCustomerID] = b.[ID]
        OUTER APPLY ( SELECT TOP 1
                                mm.* ,
                                X.*,
								bf.id bfid
                      FROM      dbo.[MSPCustomer2] X
                                INNER JOIN dbo.[ChassisMaster] c ON c.[ChassisNumber] = X.[No Rangka]
                                INNER JOIN dbo.[VechileColor] vc ON [vc].[ID] = [c].[VechileColorID]
                                INNER JOIN dbo.[VechileType] vt ON [vt].[ID] = [vc].[VechileTypeID]
                                INNER JOIN dbo.[MSPMaster] mm ON vt.[ID] = mm.[VehicleTypeID]
                                INNER JOIN dbo.[MSPType] mt ON mm.[MSPTypeID] = mt.[ID]
                                LEFT JOIN dbo.BenefitMasterHeader bf ON X.[Benefit Reg No] = bf.BenefitRegNo
                                                              AND bf.RowStatus = 0
                      WHERE     1 = 1
                                AND c.[ChassisNumber] = X.[No Rangka]
                                AND mm.[Duration] = X.[Durasi]
                                AND X.[Tipe MSP] = mt.[Description]
                                AND a.ChassisMasterID = c.ID
                    ) X
WHERE   a.[CreatedBy] = 'ADMIN-MIGRATION'
