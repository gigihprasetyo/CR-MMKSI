/*NEW SCRIPT FOR DATA*/
 
GO
UPDATE  StandardCode
SET     RowStatus = -1
WHERE   Category IN ( 'SPKAdditionalParts', 'EnumAgeSegment', 'EnumGender',
                      'SAPCustomerStatus', 'EnumStatusSPK' )
GO
INSERT  INTO StandardCode
        ( Category ,
		  ValueId ,
		  ValueCode ,
		  ValueDesc ,
		  Sequence ,
		  RowStatus ,
		  CreatedBy ,
		  CreatedTime ,
		  LastUpdateBy ,
		  LastUpdateTime
		)
        SELECT  Category ,
		ValueId ,
		ValueCode ,
		ValueDesc ,
		Sequence ,
		RowStatus ,
		'ADMIN' AS CreatedBy ,
		GETDATE() AS CreatedTIme ,
		'ADMIN' AS LastUpdateBy ,
		GETDATE() AS LastUpdateTime
        FROM    [BSIDNET_MMKSI_DMS_20180924_0100].dbo.StandardCodeForProduction
ORDER BY Category ,
		ValueId ASC
GO

INSERT  INTO StandardCodeChar
        ( Category ,
		  ValueId ,
		  ValueCode ,
		  ValueDesc ,
		  Sequence ,
		  RowStatus ,
		  CreatedBy ,
		  CreatedTime ,
		  LastUpdateBy ,
		  LastUpdateTime
		)
        SELECT  Category ,
		ValueId ,
		ValueCode ,
		ValueDesc ,
		Sequence ,
		0 AS RowStatus ,
		'ADMIN' AS CreatedBy ,
		GETDATE() AS CreatedTIme ,
		'ADMIN' AS LastUpdateBy ,
		GETDATE() AS LastUpdateTime
        FROM    [BSIDNET_MMKSI_DMS_20180924_0100].dbo.StandardCodeCharForProduction
ORDER BY ID

GO

INSERT  INTO [dbo].[AppConfig]
        ( [Name] ,
		  [Value] ,
		  [AppID] ,
		  [Status] ,
		  [RowStatus] ,
		  [CreatedBy] ,
		  [CreatedTime] ,
		  [LastUpdateBy] ,
		  [LastUpdateTime]
		)
VALUES  ( 'DuplicateNameInPartShop' ,
		  'false' ,
		  'KTB.DNet.WebApi' ,
		  0 ,
		  0 ,
		  'ADMIN' ,
		  GETDATE() ,
		  'ADMIN' ,
		  GETDATE()
		)
GO
INSERT  INTO [dbo].[AppConfig]
        ( [Name] ,
		  [Value] ,
		  [AppID] ,
		  [Status] ,
		  [RowStatus] ,
		  [CreatedBy] ,
		  [CreatedTime] ,
		  [LastUpdateBy] ,
		  [LastUpdateTime]
		)
VALUES  ( 'SparePartProductFixedPrice' ,
		  'true' ,
		  'KTB.DNet.WebApi' ,
		  0 ,
		  0 ,
		  'ADMIN' ,
		  GETDATE() ,
		  'ADMIN' ,
		  GETDATE()
		)
GO
INSERT  INTO [dbo].[AppConfig]
        ( [Name] ,
		  [Value] ,
		  [AppID] ,
		  [Status] ,
		  [RowStatus] ,
		  [CreatedBy] ,
		  [CreatedTime] ,
		  [LastUpdateBy] ,
		  [LastUpdateTime]
		)
VALUES  ( 'SP_POAutoSendToSAP' ,
		  '0' ,
		  'KTB.DNet.WebApi' ,
		  0 ,
		  0 ,
		  'ADMIN' ,
		  GETDATE() ,
		  'ADMIN' ,
		  GETDATE()
		)
GO
INSERT  INTO [dbo].[AppConfig]
        ( [Name] ,
		  [Value] ,
		  [AppID] ,
		  [Status] ,
		  [RowStatus] ,
		  [CreatedBy] ,
		  [CreatedTime] ,
		  [LastUpdateBy] ,
		  [LastUpdateTime]
		)
VALUES  ( 'SMTP' ,
          '172.17.2.69' ,
		  'KTB.DNet.WebApi' ,
		  0 ,
		  0 ,
		  'ADMIN' ,
		  GETDATE() ,
		  'ADMIN' ,
		  GETDATE()
		)
GO
INSERT  INTO [dbo].[AppConfig]
        ( [Name] ,
		  [Value] ,
		  [AppID] ,
		  [Status] ,
		  [RowStatus] ,
		  [CreatedBy] ,
		  [CreatedTime] ,
		  [LastUpdateBy] ,
		  [LastUpdateTime]
		)
VALUES  ( 'EmailFrom' ,
		  'admin.d-net@ktb.co.id' ,
		  'KTB.DNet.WebApi' ,
		  0 ,
		  0 ,
		  'ADMIN' ,
		  GETDATE() ,
		  'ADMIN' ,
		  GETDATE()
		)
GO
INSERT  INTO [dbo].[AppConfig]
        ( [Name] ,
		  [Value] ,
		  [AppID] ,
		  [Status] ,
		  [RowStatus] ,
		  [CreatedBy] ,
		  [CreatedTime] ,
		  [LastUpdateBy] ,
		  [LastUpdateTime]
		)
VALUES  ( 'EmailSPAdmin' ,
		  'su.D-net@bsi.co.id' ,
		  'KTB.DNet.WebApi' ,
		  0 ,
		  0 ,
		  'ADMIN' ,
		  GETDATE() ,
		  'ADMIN' ,
		  GETDATE()
		)
GO

INSERT  INTO dbo.PaymentPurpose
        ( PaymentPurposeCode ,
		  Description ,
		  RowStatus ,
		  CreatedBy ,
		  CreatedTime ,
		  LastUpdateBy ,
		  LastUpdateTime
		)
VALUES  ( 'SP' ,
		  'TOP Spare Part' ,
		  0 ,
		  'ADMIN' ,
		  GETDATE() ,
		  'ADMIN' ,
		  GETDATE()
		)
GO
INSERT  INTO [dbo].[AppConfig]
        ( [Name] ,
		  [Value] ,
		  [AppID] ,
		  [Status] ,
		  [RowStatus] ,
		  [CreatedBy] ,
		  [CreatedTime] ,
		  [LastUpdateBy] ,
		  [LastUpdateTime]
		)
VALUES  ( 'TOPSPTransferPaymentListBank' ,
		  0 ,
		  'KTB.Dnet.UI.40' ,
		  0 ,
		  0 ,
		  'ADMIN' ,
		  GETDATE() ,
		  'ADMIN' ,
		  GETDATE()
		)
GO
INSERT  INTO [dbo].[AppConfig]
        ( [Name] ,
		  [Value] ,
		  [AppID] ,
		  [Status] ,
		  [RowStatus] ,
		  [CreatedBy] ,
		  [CreatedTime] ,
		  [LastUpdateBy] ,
		  [LastUpdateTime]
		)
VALUES	(
		  'TOPKelipatanPembayaran' ,
		  1 ,
		  'KTB.DNet.UI.40' ,
		  0 ,
		  0 ,
		  'ADMIN' ,
		  GETDATE() ,
		  'ADMIN' ,
		  GETDATE()
		)
GO
----------------------------------------------------------------------------
INSERT	INTO [dbo].[AppConfig]
		(
		  [Name] ,
		  [Value] ,
		  [AppID] ,
		  [Status] ,
		  [RowStatus] ,
		  [CreatedBy] ,
		  [CreatedTime] ,
		  [LastUpdateBy] ,
		  [LastUpdateTime]
		)
VALUES  ( 'SparePartNonTopID' ,
		  1 ,
		  'KTB.DNet.WebApi' ,
		  0 ,
		  0 ,
		  'ADMIN' ,
		  GETDATE() ,
		  'ADMIN' ,
		  GETDATE()
		)
GO


INSERT  INTO [dbo].[AppConfig]
        ( [Name] ,
          [Value] ,
          [AppID] ,
          [Status] ,
          [RowStatus] ,
          [CreatedBy] ,
          [CreatedTime] ,
          [LastUpdateBy] ,
          [LastUpdateTime]
		)
VALUES  ( 'UpdateTOPKelipatan' ,
          1 ,
          '' ,
          0 ,
          0 ,
          'ADMIN' ,
          GETDATE() ,
          'ADMIN' ,
          GETDATE()
        )
GO

INSERT  INTO SparePartPOTypeTOP
        ( SparePartPOType ,
		  IsTOP ,
		  TermOfPaymentIDNotTOP ,
		  RowStatus ,
		  CreatedBy ,
		  CreatedTime ,
		  LastUpdateBy ,
		  LastUpdateTime
		)
VALUES  ( 'E' ,
		  0 ,
		  1 ,
		  0 ,
		  'ADMIN' ,
		  GETDATE() ,
		  'ADMIN' ,
		  GETDATE()
		)
GO
INSERT  INTO SparePartPOTypeTOP
        ( SparePartPOType ,
		  IsTOP ,
		  TermOfPaymentIDNotTOP ,
		  RowStatus ,
		  CreatedBy ,
		  CreatedTime ,
		  LastUpdateBy ,
		  LastUpdateTime
		)
VALUES  ( 'R' ,
		  1 ,
		  1 ,
		  0 ,
		  'ADMIN' ,
		  GETDATE() ,
		  'ADMIN' ,
		  GETDATE()
		)
GO
INSERT  INTO SparePartPOTypeTOP
        ( SparePartPOType ,
		  IsTOP ,
		  TermOfPaymentIDNotTOP ,
		  RowStatus ,
		  CreatedBy ,
		  CreatedTime ,
		  LastUpdateBy ,
		  LastUpdateTime
		)
VALUES  ( 'K' ,
		  0 ,
		  1 ,
		  0 ,
		  'ADMIN' ,
		  GETDATE() ,
		  'ADMIN' ,
		  GETDATE()
		)
GO
INSERT  INTO SparePartPOTypeTOP
        ( SparePartPOType ,
		  IsTOP ,
		  TermOfPaymentIDNotTOP ,
		  RowStatus ,
		  CreatedBy ,
		  CreatedTime ,
		  LastUpdateBy ,
		  LastUpdateTime
		)
VALUES  ( 'I' ,
		  1 ,
		  1 ,
		  0 ,
		  'ADMIN' ,
		  GETDATE() ,
		  'ADMIN' ,
		  GETDATE()
		)
GO
INSERT  INTO SparePartPOTypeTOP
        ( SparePartPOType ,
		  IsTOP ,
		  TermOfPaymentIDNotTOP ,
		  RowStatus ,
		  CreatedBy ,
		  CreatedTime ,
		  LastUpdateBy ,
		  LastUpdateTime
		)
VALUES  ( 'Z' ,
		  1 ,
		  1 ,
		  0 ,
		  'ADMIN' ,
		  GETDATE() ,
		  'ADMIN' ,
		  GETDATE()
		)
GO
INSERT  INTO SparePartPOTypeTOP
        ( SparePartPOType ,
		  IsTOP ,
		  TermOfPaymentIDNotTOP ,
		  RowStatus ,
		  CreatedBy ,
		  CreatedTime ,
		  LastUpdateBy ,
		  LastUpdateTime
		)
VALUES  ( 'Y' ,
		  0 ,
		  1 ,
		  0 ,
		  'ADMIN' ,
		  GETDATE() ,
		  'ADMIN' ,
		  GETDATE()
		)
GO

INSERT  INTO dbo.SparePartMasterTOP
        ( SparePartPOTypeTOPID ,
		  SparePartMasterID ,
		  Status ,
		  RowStatus ,
		  CreatedBy ,
		  CreatedTime ,
		  LastUpdateBy ,
		  LastUpdateTime
		)
        SELECT  a.ID AS SparePartPOTypeTOPID ,
		b.ID AS SparePartMasterID ,
		a.IsTOP AS Status ,
		0 AS RowStatus ,
		'ADMIN' AS CreatedBy ,
		GETDATE() AS CreatedTIme ,
		'ADMIN' AS LastUpdateBy ,
		GETDATE() AS LastUpdateTime
        FROM    SparePartPOTypeTOP a
                CROSS JOIN ( SELECT *
                             FROM   SparePartMaster
                             WHERE  ProductCategoryID IN ( 1, 3 )
		   ) b

GO

INSERT  INTO DealerSystems
        ( DealerID ,
          SystemID ,
          isSPKMatchFaktur ,
          isOnlyUploadPhotoTenagaPenjual ,
          RowStatus ,
		  CreatedBy ,
		  CreatedTime ,
          LastUpdateBy ,
          LastUpdateTime
		)
        SELECT  ID ,
                SystemId ,
                isSPKMatchFaktur ,
                isOnlyUploadPhotoTenagaPenjual ,
                RowStatus ,
                CreatedBy ,
                CreatedTime ,
                LastUpdateBy ,
                LastUpdateTime
        FROM    ( SELECT    ID ,
                            1 AS SystemId ,
                            0 AS isSPKMatchFaktur ,
                            0 AS isOnlyUploadPhotoTenagaPenjual ,
                            0 AS RowStatus ,
                            'ADMIN' AS CreatedBy ,
                            GETDATE() AS CreatedTime ,
                            'ADMIN' AS LastUpdateBy ,
                            GETDATE() AS LastUpdateTime
                  FROM      Dealer
                  WHERE     RowStatus = 0
                            AND ID NOT IN ( 1, 2 )
                            AND ID NOT IN (
                            SELECT  ID
                            FROM    Dealer
                            WHERE   DealerGroupID IN ( 11, 20 ) )
                  UNION
                  SELECT    ID ,
                            2 AS SystemId ,
                            1 AS isSPKMatchFaktur ,
                            1 AS isOnlyUploadPhotoTenagaPenjual ,
                            0 AS RowStatus ,
                            'ADMIN' AS CreatedBy ,
                            GETDATE() AS CreatedTime ,
                            'ADMIN' AS LastUpdateBy ,
                            GETDATE() AS LastUpdateTime
                  FROM      Dealer
                  WHERE     RowStatus = 0
                            AND ID IN ( SELECT  ID
                                        FROM    Dealer
                                        WHERE   DealerGroupID IN ( 11, 20 ) )
                ) a
        ORDER BY a.ID

GO

SET IDENTITY_INSERT [dbo].[BusinessSectorHeader] ON 

INSERT  [dbo].[BusinessSectorHeader]
        ( [ID] ,
          [BusinessSectorName] ,
          [RowStatus] ,
          [CreatedBy] ,
          [CreatedTime] ,
          [LastUpdateBy] ,
          [LastUpdateTime]
		)
VALUES  ( 1 ,
          N'SERVICES' ,
          0 ,
          N'ADMIN' ,
		  GETDATE() ,
          N'ADMIN' ,
		  GETDATE()
		)
INSERT  [dbo].[BusinessSectorHeader]
        ( [ID] ,
          [BusinessSectorName] ,
          [RowStatus] ,
          [CreatedBy] ,
          [CreatedTime] ,
          [LastUpdateBy] ,
          [LastUpdateTime]
		)
VALUES  ( 2 ,
          N'AIRLINE INDUSTRY' ,
          0 ,
          N'ADMIN' ,
		  GETDATE() ,
          N'ADMIN' ,
		  GETDATE()
		)
INSERT  [dbo].[BusinessSectorHeader]
        ( [ID] ,
          [BusinessSectorName] ,
          [RowStatus] ,
          [CreatedBy] ,
          [CreatedTime] ,
          [LastUpdateBy] ,
          [LastUpdateTime]
		)
VALUES  ( 3 ,
          N'PLANTATION' ,
          0 ,
          N'ADMIN' ,
		  GETDATE() ,
          N'ADMIN' ,
		  GETDATE()
		)
INSERT  [dbo].[BusinessSectorHeader]
        ( [ID] ,
          [BusinessSectorName] ,
          [RowStatus] ,
          [CreatedBy] ,
          [CreatedTime] ,
          [LastUpdateBy] ,
          [LastUpdateTime]
        )
VALUES  ( 4 ,
          N'CONSUMER GOODS' ,
          0 ,
          N'ADMIN' ,
          GETDATE() ,
          N'ADMIN' ,
          GETDATE()
        )
INSERT  [dbo].[BusinessSectorHeader]
        ( [ID] ,
          [BusinessSectorName] ,
          [RowStatus] ,
          [CreatedBy] ,
          [CreatedTime] ,
          [LastUpdateBy] ,
          [LastUpdateTime]
        )
VALUES  ( 5 ,
          N'BANKING / FINANCING & INSURANCE' ,
          0 ,
          N'ADMIN' ,
          GETDATE() ,
          N'ADMIN' ,
          GETDATE()
        )
INSERT  [dbo].[BusinessSectorHeader]
        ( [ID] ,
          [BusinessSectorName] ,
          [RowStatus] ,
          [CreatedBy] ,
          [CreatedTime] ,
          [LastUpdateBy] ,
          [LastUpdateTime]
        )
VALUES  ( 6 ,
          N'MANUFACTURING' ,
          0 ,
          N'ADMIN' ,
          GETDATE() ,
          N'ADMIN' ,
          GETDATE()
        )
INSERT  [dbo].[BusinessSectorHeader]
        ( [ID] ,
          [BusinessSectorName] ,
          [RowStatus] ,
          [CreatedBy] ,
          [CreatedTime] ,
          [LastUpdateBy] ,
          [LastUpdateTime]
        )
VALUES  ( 7 ,
          N'MINING' ,
          0 ,
          N'ADMIN' ,
          GETDATE() ,
          N'ADMIN' ,
          GETDATE()
        )
INSERT  [dbo].[BusinessSectorHeader]
        ( [ID] ,
          [BusinessSectorName] ,
          [RowStatus] ,
          [CreatedBy] ,
          [CreatedTime] ,
          [LastUpdateBy] ,
          [LastUpdateTime]
        )
VALUES  ( 8 ,
          N'PUBLIC SECTOR / GOVERNMENT' ,
          0 ,
          N'ADMIN' ,
          GETDATE() ,
          N'ADMIN' ,
          GETDATE()
        )
INSERT  [dbo].[BusinessSectorHeader]
        ( [ID] ,
          [BusinessSectorName] ,
          [RowStatus] ,
          [CreatedBy] ,
          [CreatedTime] ,
          [LastUpdateBy] ,
          [LastUpdateTime]
        )
VALUES  ( 9 ,
          N'TECHNOLOGY INFORMATION' ,
          0 ,
          N'ADMIN' ,
          GETDATE() ,
          N'ADMIN' ,
          GETDATE()
        )
INSERT  [dbo].[BusinessSectorHeader]
        ( [ID] ,
          [BusinessSectorName] ,
          [RowStatus] ,
          [CreatedBy] ,
          [CreatedTime] ,
          [LastUpdateBy] ,
          [LastUpdateTime]
        )
VALUES  ( 10 ,
          N'TRANSPORT & LOGISTIC' ,
          0 ,
          N'ADMIN' ,
          GETDATE() ,
          N'ADMIN' ,
          GETDATE()
        )
INSERT  [dbo].[BusinessSectorHeader]
        ( [ID] ,
          [BusinessSectorName] ,
          [RowStatus] ,
          [CreatedBy] ,
          [CreatedTime] ,
          [LastUpdateBy] ,
          [LastUpdateTime]
        )
VALUES  ( 11 ,
          N'ENERGY & UTILITIES' ,
          0 ,
          N'ADMIN' ,
          GETDATE() ,
          N'ADMIN' ,
          GETDATE()
        )
INSERT  [dbo].[BusinessSectorHeader]
        ( [ID] ,
          [BusinessSectorName] ,
          [RowStatus] ,
          [CreatedBy] ,
          [CreatedTime] ,
          [LastUpdateBy] ,
          [LastUpdateTime]
        )
VALUES  ( 12 ,
          N'DISTRIBUTOR / SUPPLIER / TRADING' ,
          0 ,
          N'ADMIN' ,
          GETDATE() ,
          N'ADMIN' ,
          GETDATE()
        )
SET IDENTITY_INSERT [dbo].[BusinessSectorHeader] OFF

GO

SET IDENTITY_INSERT [dbo].[BusinessSectorDetail] ON 

INSERT  [dbo].[BusinessSectorDetail]
        ( [ID] ,
          [BusinessSectorHeaderID] ,
          [BusinessDomain] ,
          [RowStatus] ,
          [CreatedBy] ,
          [CreatedTime] ,
          [LastUpdateBy] ,
          [LastUpdateTime]
		)
VALUES  ( 1 ,
		  1 ,
          N'SECURITY OUTSOURCE' ,
          0 ,
          N'ADMIN' ,
          GETDATE() ,
          N'ADMIN' ,
          GETDATE()
        )
INSERT  [dbo].[BusinessSectorDetail]
        ( [ID] ,
          [BusinessSectorHeaderID] ,
          [BusinessDomain] ,
          [RowStatus] ,
          [CreatedBy] ,
          [CreatedTime] ,
          [LastUpdateBy] ,
          [LastUpdateTime]
        )
VALUES  ( 2 ,
		  1 ,
          N'HOSPITAL, CLINIC' ,
          0 ,
          N'ADMIN' ,
          GETDATE() ,
          N'ADMIN' ,
          GETDATE()
        )
INSERT  [dbo].[BusinessSectorDetail]
        ( [ID] ,
          [BusinessSectorHeaderID] ,
          [BusinessDomain] ,
          [RowStatus] ,
          [CreatedBy] ,
          [CreatedTime] ,
          [LastUpdateBy] ,
          [LastUpdateTime]
        )
VALUES  ( 3 ,
          1 ,
          N'TOUR & TRAVEL' ,
          0 ,
          N'ADMIN' ,
          GETDATE() ,
          N'ADMIN' ,
          GETDATE()
        )
INSERT  [dbo].[BusinessSectorDetail]
        ( [ID] ,
          [BusinessSectorHeaderID] ,
          [BusinessDomain] ,
          [RowStatus] ,
          [CreatedBy] ,
          [CreatedTime] ,
          [LastUpdateBy] ,
          [LastUpdateTime]
        )
VALUES  ( 4 ,
          1 ,
          N'OTHERS' ,
          0 ,
          N'ADMIN' ,
          GETDATE() ,
          N'ADMIN' ,
          GETDATE()
        )
INSERT  [dbo].[BusinessSectorDetail]
        ( [ID] ,
          [BusinessSectorHeaderID] ,
          [BusinessDomain] ,
          [RowStatus] ,
          [CreatedBy] ,
          [CreatedTime] ,
          [LastUpdateBy] ,
          [LastUpdateTime]
        )
VALUES  ( 5 ,
		  2 ,
          N'COMMERCIAL AIRLINE' ,
          0 ,
          N'ADMIN' ,
          GETDATE() ,
          N'ADMIN' ,
          GETDATE()
        )
INSERT  [dbo].[BusinessSectorDetail]
        ( [ID] ,
          [BusinessSectorHeaderID] ,
          [BusinessDomain] ,
          [RowStatus] ,
          [CreatedBy] ,
          [CreatedTime] ,
          [LastUpdateBy] ,
          [LastUpdateTime]
        )
VALUES  ( 6 ,
          2 ,
          N'NONCOMMERCIAL AIRLINE' ,
          0 ,
          N'ADMIN' ,
          GETDATE() ,
          N'ADMIN' ,
          GETDATE()
        )
INSERT  [dbo].[BusinessSectorDetail]
        ( [ID] ,
          [BusinessSectorHeaderID] ,
          [BusinessDomain] ,
          [RowStatus] ,
          [CreatedBy] ,
          [CreatedTime] ,
          [LastUpdateBy] ,
          [LastUpdateTime]
        )
VALUES  ( 7 ,
          2 ,
          N'OTHERS' ,
          0 ,
          N'ADMIN' ,
          GETDATE() ,
          N'ADMIN' ,
          GETDATE()
        )
INSERT  [dbo].[BusinessSectorDetail]
        ( [ID] ,
          [BusinessSectorHeaderID] ,
          [BusinessDomain] ,
          [RowStatus] ,
          [CreatedBy] ,
          [CreatedTime] ,
          [LastUpdateBy] ,
          [LastUpdateTime]
        )
VALUES  ( 8 ,
          3 ,
          N'OIL PALM, RUBBER' ,
          0 ,
          N'ADMIN' ,
          GETDATE() ,
          N'ADMIN' ,
          GETDATE()
        )
INSERT  [dbo].[BusinessSectorDetail]
        ( [ID] ,
          [BusinessSectorHeaderID] ,
          [BusinessDomain] ,
          [RowStatus] ,
          [CreatedBy] ,
          [CreatedTime] ,
          [LastUpdateBy] ,
          [LastUpdateTime]
        )
VALUES  ( 9 ,
          3 ,
          N'FRUIT' ,
          0 ,
          N'ADMIN' ,
          GETDATE() ,
          N'ADMIN' ,
          GETDATE()
        )
INSERT  [dbo].[BusinessSectorDetail]
        ( [ID] ,
          [BusinessSectorHeaderID] ,
          [BusinessDomain] ,
          [RowStatus] ,
          [CreatedBy] ,
          [CreatedTime] ,
          [LastUpdateBy] ,
          [LastUpdateTime]
        )
VALUES  ( 10 ,
          3 ,
          N'FORESTRY, ETC' ,
          0 ,
          N'ADMIN' ,
          GETDATE() ,
          N'ADMIN' ,
          GETDATE()
        )
INSERT  [dbo].[BusinessSectorDetail]
        ( [ID] ,
          [BusinessSectorHeaderID] ,
          [BusinessDomain] ,
          [RowStatus] ,
          [CreatedBy] ,
          [CreatedTime] ,
          [LastUpdateBy] ,
          [LastUpdateTime]
        )
VALUES  ( 11 ,
          4 ,
          N'FMCG' ,
          0 ,
          N'ADMIN' ,
          GETDATE() ,
          N'ADMIN' ,
          GETDATE()
        )
INSERT  [dbo].[BusinessSectorDetail]
        ( [ID] ,
          [BusinessSectorHeaderID] ,
          [BusinessDomain] ,
          [RowStatus] ,
          [CreatedBy] ,
          [CreatedTime] ,
          [LastUpdateBy] ,
          [LastUpdateTime]
        )
VALUES  ( 12 ,
          4 ,
          N'COSMETIC' ,
          0 ,
          N'ADMIN' ,
          GETDATE() ,
          N'ADMIN' ,
          GETDATE()
        )
INSERT  [dbo].[BusinessSectorDetail]
        ( [ID] ,
          [BusinessSectorHeaderID] ,
          [BusinessDomain] ,
          [RowStatus] ,
          [CreatedBy] ,
          [CreatedTime] ,
          [LastUpdateBy] ,
          [LastUpdateTime]
        )
VALUES  ( 13 ,
          4 ,
          N'FOOD & BEVERAGES, RESTAURANT' ,
          0 ,
          N'ADMIN' ,
          GETDATE() ,
          N'ADMIN' ,
          GETDATE()
        )
INSERT  [dbo].[BusinessSectorDetail]
        ( [ID] ,
          [BusinessSectorHeaderID] ,
          [BusinessDomain] ,
          [RowStatus] ,
          [CreatedBy] ,
          [CreatedTime] ,
          [LastUpdateBy] ,
          [LastUpdateTime]
        )
VALUES  ( 14 ,
          5 ,
          N'CORPORATE & RETAIL BANKING' ,
          0 ,
          N'ADMIN' ,
          GETDATE() ,
          N'ADMIN' ,
          GETDATE()
        )
INSERT  [dbo].[BusinessSectorDetail]
        ( [ID] ,
          [BusinessSectorHeaderID] ,
          [BusinessDomain] ,
          [RowStatus] ,
          [CreatedBy] ,
          [CreatedTime] ,
          [LastUpdateBy] ,
          [LastUpdateTime]
        )
VALUES  ( 15 ,
          5 ,
          N'LEASING & AUTO INSURANCE' ,
          0 ,
          N'ADMIN' ,
          GETDATE() ,
          N'ADMIN' ,
          GETDATE()
        )
INSERT  [dbo].[BusinessSectorDetail]
        ( [ID] ,
          [BusinessSectorHeaderID] ,
          [BusinessDomain] ,
          [RowStatus] ,
          [CreatedBy] ,
          [CreatedTime] ,
          [LastUpdateBy] ,
          [LastUpdateTime]
        )
VALUES  ( 16 ,
          5 ,
          N'HEALTH & LIFE INSURANCE' ,
          0 ,
          N'ADMIN' ,
          GETDATE() ,
          N'ADMIN' ,
          GETDATE()
        )
INSERT  [dbo].[BusinessSectorDetail]
        ( [ID] ,
          [BusinessSectorHeaderID] ,
          [BusinessDomain] ,
          [RowStatus] ,
          [CreatedBy] ,
          [CreatedTime] ,
          [LastUpdateBy] ,
          [LastUpdateTime]
        )
VALUES  ( 17 ,
          6 ,
          N'METALURGI MANUFACTURE' ,
          0 ,
          N'ADMIN' ,
          GETDATE() ,
          N'ADMIN' ,
          GETDATE()
        )
INSERT  [dbo].[BusinessSectorDetail]
        ( [ID] ,
          [BusinessSectorHeaderID] ,
          [BusinessDomain] ,
          [RowStatus] ,
          [CreatedBy] ,
          [CreatedTime] ,
          [LastUpdateBy] ,
          [LastUpdateTime]
        )
VALUES  ( 18 ,
          6 ,
          N'CHEMICAL MANUFACTURE' ,
          0 ,
          N'ADMIN' ,
          GETDATE() ,
          N'ADMIN' ,
          GETDATE()
        )
INSERT  [dbo].[BusinessSectorDetail]
        ( [ID] ,
          [BusinessSectorHeaderID] ,
          [BusinessDomain] ,
          [RowStatus] ,
          [CreatedBy] ,
          [CreatedTime] ,
          [LastUpdateBy] ,
          [LastUpdateTime]
        )
VALUES  ( 19 ,
          6 ,
          N'AUTOMOTIVE MANUFACTURE' ,
          0 ,
          N'ADMIN' ,
          GETDATE() ,
          N'ADMIN' ,
          GETDATE()
        )
INSERT  [dbo].[BusinessSectorDetail]
        ( [ID] ,
          [BusinessSectorHeaderID] ,
          [BusinessDomain] ,
          [RowStatus] ,
          [CreatedBy] ,
          [CreatedTime] ,
          [LastUpdateBy] ,
          [LastUpdateTime]
        )
VALUES  ( 20 ,
          6 ,
          N'TEXTILE MANUFACTURE' ,
          0 ,
          N'ADMIN' ,
          GETDATE() ,
          N'ADMIN' ,
          GETDATE()
        )
INSERT  [dbo].[BusinessSectorDetail]
        ( [ID] ,
          [BusinessSectorHeaderID] ,
          [BusinessDomain] ,
          [RowStatus] ,
          [CreatedBy] ,
          [CreatedTime] ,
          [LastUpdateBy] ,
          [LastUpdateTime]
        )
VALUES  ( 21 ,
          6 ,
          N'PHARMACY MANUFACTURE' ,
          0 ,
          N'ADMIN' ,
          GETDATE() ,
          N'ADMIN' ,
          GETDATE()
        )
INSERT  [dbo].[BusinessSectorDetail]
        ( [ID] ,
          [BusinessSectorHeaderID] ,
          [BusinessDomain] ,
          [RowStatus] ,
          [CreatedBy] ,
          [CreatedTime] ,
          [LastUpdateBy] ,
          [LastUpdateTime]
        )
VALUES  ( 22 ,
          6 ,
          N'OTHERS' ,
          0 ,
          N'ADMIN' ,
          GETDATE() ,
          N'ADMIN' ,
          GETDATE()
        )
INSERT  [dbo].[BusinessSectorDetail]
        ( [ID] ,
          [BusinessSectorHeaderID] ,
          [BusinessDomain] ,
          [RowStatus] ,
          [CreatedBy] ,
          [CreatedTime] ,
          [LastUpdateBy] ,
          [LastUpdateTime]
        )
VALUES  ( 23 ,
          7 ,
          N'COAL, NICKEL, LIMESTONE' ,
          0 ,
          N'ADMIN' ,
          GETDATE() ,
          N'ADMIN' ,
          GETDATE()
        )
INSERT  [dbo].[BusinessSectorDetail]
        ( [ID] ,
          [BusinessSectorHeaderID] ,
          [BusinessDomain] ,
          [RowStatus] ,
          [CreatedBy] ,
          [CreatedTime] ,
          [LastUpdateBy] ,
          [LastUpdateTime]
        )
VALUES  ( 24 ,
          7 ,
          N'GEMSTONES, DIAMOND, GOLD' ,
          0 ,
          N'ADMIN' ,
          GETDATE() ,
          N'ADMIN' ,
          GETDATE()
        )
INSERT  [dbo].[BusinessSectorDetail]
        ( [ID] ,
          [BusinessSectorHeaderID] ,
          [BusinessDomain] ,
          [RowStatus] ,
          [CreatedBy] ,
          [CreatedTime] ,
          [LastUpdateBy] ,
          [LastUpdateTime]
        )
VALUES  ( 25 ,
          8 ,
          N'MILITARY/TNI' ,
          0 ,
          N'ADMIN' ,
          GETDATE() ,
          N'ADMIN' ,
          GETDATE()
        )
INSERT  [dbo].[BusinessSectorDetail]
        ( [ID] ,
          [BusinessSectorHeaderID] ,
          [BusinessDomain] ,
          [RowStatus] ,
          [CreatedBy] ,
          [CreatedTime] ,
          [LastUpdateBy] ,
          [LastUpdateTime]
        )
VALUES  ( 26 ,
          8 ,
          N'POLICE' ,
          0 ,
          N'ADMIN' ,
          GETDATE() ,
          N'ADMIN' ,
          GETDATE()
        )
INSERT  [dbo].[BusinessSectorDetail]
        ( [ID] ,
          [BusinessSectorHeaderID] ,
          [BusinessDomain] ,
          [RowStatus] ,
          [CreatedBy] ,
          [CreatedTime] ,
          [LastUpdateBy] ,
          [LastUpdateTime]
        )
VALUES  ( 27 ,
          8 ,
          N'MINISTRY, ETC' ,
          0 ,
          N'ADMIN' ,
          GETDATE() ,
          N'ADMIN' ,
          GETDATE()
        )
INSERT  [dbo].[BusinessSectorDetail]
        ( [ID] ,
          [BusinessSectorHeaderID] ,
          [BusinessDomain] ,
          [RowStatus] ,
          [CreatedBy] ,
          [CreatedTime] ,
          [LastUpdateBy] ,
          [LastUpdateTime]
        )
VALUES  ( 28 ,
          9 ,
          N'NETWORK' ,
          0 ,
          N'ADMIN' ,
          GETDATE() ,
          N'ADMIN' ,
          GETDATE()
        )
INSERT  [dbo].[BusinessSectorDetail]
        ( [ID] ,
          [BusinessSectorHeaderID] ,
          [BusinessDomain] ,
          [RowStatus] ,
          [CreatedBy] ,
          [CreatedTime] ,
          [LastUpdateBy] ,
          [LastUpdateTime]
        )
VALUES  ( 29 ,
          9 ,
          N'MEDIA CONFERENCE' ,
          0 ,
          N'ADMIN' ,
          GETDATE() ,
          N'ADMIN' ,
          GETDATE()
        )
INSERT  [dbo].[BusinessSectorDetail]
        ( [ID] ,
          [BusinessSectorHeaderID] ,
          [BusinessDomain] ,
          [RowStatus] ,
          [CreatedBy] ,
          [CreatedTime] ,
          [LastUpdateBy] ,
          [LastUpdateTime]
        )
VALUES  ( 30 ,
          9 ,
          N'TELECOMMUNICATION, ETC' ,
          0 ,
          N'ADMIN' ,
          GETDATE() ,
          N'ADMIN' ,
          GETDATE()
        )
INSERT  [dbo].[BusinessSectorDetail]
        ( [ID] ,
          [BusinessSectorHeaderID] ,
          [BusinessDomain] ,
          [RowStatus] ,
          [CreatedBy] ,
          [CreatedTime] ,
          [LastUpdateBy] ,
          [LastUpdateTime]
        )
VALUES  ( 31 ,
          10 ,
          N'RENTAL' ,
          0 ,
          N'ADMIN' ,
          GETDATE() ,
          N'ADMIN' ,
          GETDATE()
        )
INSERT  [dbo].[BusinessSectorDetail]
        ( [ID] ,
          [BusinessSectorHeaderID] ,
          [BusinessDomain] ,
          [RowStatus] ,
          [CreatedBy] ,
          [CreatedTime] ,
          [LastUpdateBy] ,
          [LastUpdateTime]
        )
VALUES  ( 32 ,
          10 ,
          N'LOGISTIC, CARGO' ,
          0 ,
          N'ADMIN' ,
          GETDATE() ,
          N'ADMIN' ,
          GETDATE()
        )
INSERT  [dbo].[BusinessSectorDetail]
        ( [ID] ,
          [BusinessSectorHeaderID] ,
          [BusinessDomain] ,
          [RowStatus] ,
          [CreatedBy] ,
          [CreatedTime] ,
          [LastUpdateBy] ,
          [LastUpdateTime]
        )
VALUES  ( 33 ,
          10 ,
          N'PUBLIC TRANSPORTATION' ,
          0 ,
          N'ADMIN' ,
          GETDATE() ,
          N'ADMIN' ,
          GETDATE()
        )
INSERT  [dbo].[BusinessSectorDetail]
        ( [ID] ,
          [BusinessSectorHeaderID] ,
          [BusinessDomain] ,
          [RowStatus] ,
          [CreatedBy] ,
          [CreatedTime] ,
          [LastUpdateBy] ,
          [LastUpdateTime]
        )
VALUES  ( 34 ,
          11 ,
          N'OIL & GAS' ,
          0 ,
          N'ADMIN' ,
          GETDATE() ,
          N'ADMIN' ,
          GETDATE()
        )
INSERT  [dbo].[BusinessSectorDetail]
        ( [ID] ,
          [BusinessSectorHeaderID] ,
          [BusinessDomain] ,
          [RowStatus] ,
          [CreatedBy] ,
          [CreatedTime] ,
          [LastUpdateBy] ,
          [LastUpdateTime]
        )
VALUES  ( 35 ,
          11 ,
          N'HEAVY EQUIPMENT' ,
          0 ,
          N'ADMIN' ,
          GETDATE() ,
          N'ADMIN' ,
          GETDATE()
        )
INSERT  [dbo].[BusinessSectorDetail]
        ( [ID] ,
          [BusinessSectorHeaderID] ,
          [BusinessDomain] ,
          [RowStatus] ,
          [CreatedBy] ,
          [CreatedTime] ,
          [LastUpdateBy] ,
          [LastUpdateTime]
        )
VALUES  ( 36 ,
          11 ,
          N'CONSTRUCTION, BUILDING & ENGINEERING' ,
          0 ,
          N'ADMIN' ,
          GETDATE() ,
          N'ADMIN' ,
          GETDATE()
        )
INSERT  [dbo].[BusinessSectorDetail]
        ( [ID] ,
          [BusinessSectorHeaderID] ,
          [BusinessDomain] ,
          [RowStatus] ,
          [CreatedBy] ,
          [CreatedTime] ,
          [LastUpdateBy] ,
          [LastUpdateTime]
        )
VALUES  ( 37 ,
          12 ,
          N'WHOLESALER, RETAILER' ,
		  0 ,
          N'ADMIN' ,
		  GETDATE() ,
          N'ADMIN' ,
		  GETDATE()
		)
INSERT  [dbo].[BusinessSectorDetail]
        ( [ID] ,
          [BusinessSectorHeaderID] ,
          [BusinessDomain] ,
          [RowStatus] ,
          [CreatedBy] ,
          [CreatedTime] ,
          [LastUpdateBy] ,
          [LastUpdateTime]
		)
VALUES  ( 38 ,
          12 ,
          N'TRADING, ETC' ,
          0 ,
          N'ADMIN' ,
		  GETDATE() ,
          N'ADMIN' ,
		  GETDATE()
		)
SET IDENTITY_INSERT [dbo].[BusinessSectorDetail] OFF
