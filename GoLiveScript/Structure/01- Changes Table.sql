 

 
PRINT '/*Main Area*/'
GO
SET XACT_ABORT ON
GO

BEGIN TRANSACTION
GO

ALTER TABLE Area1 DROP
CONSTRAINT [FK_MainArea[one]]_Area1[many]]] 
GO

ALTER TABLE Dealer DROP
CONSTRAINT [FK_Dealer_MainArea[one]]] 
GO

ALTER TABLE MainArea DROP
CONSTRAINT DF_MainArea_RowStatus 
GO

EXEC sp_rename
	'dbo.PK_MainArea' ,
	'tmp__PK_MainArea' ,
	'OBJECT'
GO

EXEC sp_rename
	'dbo.MainArea' ,
	'tmp__MainArea_2' ,
	'OBJECT'
GO

CREATE TABLE MainArea
	   (
		 ID INT NOT NULL
				IDENTITY
				CONSTRAINT PK_MainArea PRIMARY KEY ,
		 AreaCode VARCHAR(20) ,
		 Description VARCHAR(50) ,
		 PICSales VARCHAR(50) ,
		 PICServices VARCHAR(50) ,
		 PICSpareparts VARCHAR(50) ,
		 RowStatus SMALLINT CONSTRAINT DF_MainArea_RowStatus DEFAULT ( 0 ) ,
		 CreatedBy VARCHAR(20) ,
		 CreatedTime DATETIME ,
		 LastUpdateBy VARCHAR(20) ,
		 LastUpdateTime DATETIME
	   )
GO

SET IDENTITY_INSERT MainArea ON
GO

INSERT	INTO MainArea
		(
		  ID ,
		  AreaCode ,
		  Description ,
		  RowStatus ,
		  CreatedBy ,
		  CreatedTime ,
		  LastUpdateBy ,
		  LastUpdateTime
		)
SELECT	ID ,
		AreaCode ,
		Description ,
		RowStatus ,
		CreatedBy ,
		CreatedTime ,
		LastUpdateBy ,
		LastUpdateTime
FROM	tmp__MainArea_2
GO

SET IDENTITY_INSERT MainArea OFF
GO

DROP TABLE tmp__MainArea_2
GO

ALTER TABLE Dealer ADD
CONSTRAINT [FK_Dealer_MainArea[one]]] FOREIGN KEY(MainAreaID) REFERENCES MainArea(ID)
GO

ALTER TABLE Area1 ADD
CONSTRAINT [FK_MainArea[one]]_Area1[many]]] FOREIGN KEY(MainAreaID) REFERENCES MainArea(ID)
GO

COMMIT
GO

PRINT '/*area 1 & duo*/'
GO


SET XACT_ABORT ON
GO

BEGIN TRANSACTION
GO

ALTER TABLE Area2 DROP
CONSTRAINT [FK_Area1[one]]_Area2[many]]] 
GO

ALTER TABLE Dealer DROP
CONSTRAINT [FK_Dealer[many]]_Area1[one]]] 
GO

ALTER TABLE Area1 DROP
CONSTRAINT [FK_MainArea[one]]_Area1[many]]] 
GO

EXEC sp_rename
	'dbo.PK_Area1' ,
	'tmp__PK_Area1' ,
	'OBJECT'
GO

EXEC sp_rename
	'dbo.Area1' ,
	'tmp__Area1_3' ,
	'OBJECT'
GO

CREATE TABLE Area1
	   (
		 ID INT NOT NULL
				IDENTITY
				CONSTRAINT PK_Area1 PRIMARY KEY ,
		 AreaCode VARCHAR(10) ,
		 Description VARCHAR(100) ,
		 PICSales VARCHAR(50) ,
		 PICServices VARCHAR(50) ,
		 PICSpareparts VARCHAR(50) ,
		 MainAreaID INT ,
		 RowStatus SMALLINT ,
		 CreatedBy VARCHAR(20) ,
		 CreatedTime DATETIME ,
		 LastUpdateBy VARCHAR(20) ,
		 LastUpdateTime DATETIME
	   )
GO

ALTER TABLE Area1 ADD
CONSTRAINT [FK_MainArea[one]]_Area1[many]]] FOREIGN KEY(MainAreaID) REFERENCES MainArea(ID)
GO

SET IDENTITY_INSERT Area1 ON
GO

INSERT	INTO Area1
		(
		  ID ,
		  AreaCode ,
		  Description ,
		  MainAreaID ,
		  RowStatus ,
		  CreatedBy ,
		  CreatedTime ,
		  LastUpdateBy ,
		  LastUpdateTime
		)
SELECT	ID ,
		AreaCode ,
		CONVERT(VARCHAR(100), Description) ,
		MainAreaID ,
		RowStatus ,
		CreatedBy ,
		CreatedTime ,
		LastUpdateBy ,
		LastUpdateTime
FROM	tmp__Area1_3
GO

SET IDENTITY_INSERT Area1 OFF
GO

DROP TABLE tmp__Area1_3
GO

ALTER TABLE Area2 ALTER COLUMN
Description VARCHAR(100)
GO

ALTER TABLE Area2 ADD
CONSTRAINT [FK_Area1[one]]_Area2[many]]] FOREIGN KEY(Area1ID) REFERENCES Area1(ID)
GO

ALTER TABLE Dealer ADD
CONSTRAINT [FK_Dealer[many]]_Area1[one]]] FOREIGN KEY(Area1ID) REFERENCES Area1(ID)
GO

COMMIT
GO




PRINT '/*DealerBranch*/'
GO

 

SET XACT_ABORT ON
GO

BEGIN TRANSACTION
GO

ALTER TABLE DMSWOWarrantyClaim DROP
CONSTRAINT [FK_DMSWOWarrantyClaim[many]]_DealerBranch[one]]] 
GO

ALTER TABLE SPKHeader DROP
CONSTRAINT [FK_SPKHeader_DealerBranch[one]]] 
GO

ALTER TABLE DealerBranch DROP
CONSTRAINT [FK_DealerBranch_City[one]]] ,
CONSTRAINT [FK_DealerBranch_Province[one]]] 
GO

EXEC sp_rename
	'dbo.PK_DealerBranch' ,
	'tmp__PK_DealerBranch' ,
	'OBJECT'
GO

EXEC sp_rename
	'dbo.DealerBranch' ,
	'tmp__DealerBranch_5' ,
	'OBJECT'
GO

CREATE TABLE DealerBranch
	   (
		 ID INT NOT NULL
				IDENTITY
				CONSTRAINT PK_DealerBranch PRIMARY KEY ,
		 DealerID SMALLINT ,
		 [Name] VARCHAR(50) ,
		 Status VARCHAR(1) ,
		 Address VARCHAR(100) ,
		 CityID SMALLINT ,
		 ZipCode VARCHAR(5) ,
		 ProvinceID INT ,
		 Phone VARCHAR(50) ,
		 Fax VARCHAR(20) ,
		 Website VARCHAR(20) ,
		 Email VARCHAR(40) ,
		 TypeBranch VARCHAR(5) ,
		 DealerBranchCode VARCHAR(50) ,
		 Term1 VARCHAR(100) ,
		 Term2 VARCHAR(100) ,
		 MainAreaID INT ,
		 Area1ID INT ,
		 Area2ID INT ,
		 BranchAssignmentNo VARCHAR(50) ,
		 BranchAssignmentDate DATETIME ,
		 SalesUnitFlag VARCHAR(1) ,
		 ServiceFlag VARCHAR(1) ,
		 SparepartFlag VARCHAR(1) ,
		 RowStatus SMALLINT ,
		 CreatedBy VARCHAR(20) ,
		 CreatedTime DATETIME ,
		 LastUpdateBy VARCHAR(20) ,
		 LastUpdateTime DATETIME
	   )
GO

ALTER TABLE DealerBranch ADD
CONSTRAINT [FK_DealerBranch_City[one]]] FOREIGN KEY(CityID) REFERENCES City(ID),
CONSTRAINT [FK_DealerBranch_Province[one]]] FOREIGN KEY(ProvinceID) REFERENCES Province(ID)
GO

SET IDENTITY_INSERT DealerBranch ON
GO

INSERT	INTO DealerBranch
		(
		  ID ,
		  DealerID ,
		  [Name] ,
		  Status ,
		  Address ,
		  CityID ,
		  ZipCode ,
		  ProvinceID ,
		  Phone ,
		  Fax ,
		  Website ,
		  Email ,
		  TypeBranch ,
		  DealerBranchCode ,
		  Term1 ,
		  Term2 ,
		  MainAreaID ,
		  Area1ID ,
		  Area2ID ,
		  BranchAssignmentNo ,
		  SalesUnitFlag ,
		  ServiceFlag ,
		  SparepartFlag ,
		  RowStatus ,
		  CreatedBy ,
		  CreatedTime ,
		  LastUpdateBy ,
		  LastUpdateTime
		)
SELECT	ID ,
		DealerID ,
		[Name] ,
		Status ,
		Address ,
		CityID ,
		ZipCode ,
		ProvinceID ,
		Phone ,
		Fax ,
		Website ,
		Email ,
		TypeBranch ,
		DealerBranchCode ,
		Term1 ,
		Term2 ,
		MainAreaID ,
		Area1ID ,
		Area2ID ,
		BranchAssignmentNo ,
		SalesUnitFlag ,
		ServiceFlag ,
		SparepartFlag ,
		RowStatus ,
		CreatedBy ,
		CreatedTime ,
		LastUpdateBy ,
		LastUpdateTime
FROM	tmp__DealerBranch_5
GO

SET IDENTITY_INSERT DealerBranch OFF
GO

DROP TABLE tmp__DealerBranch_5
GO

ALTER TABLE SPKHeader ADD
CONSTRAINT [FK_SPKHeader_DealerBranch[one]]] FOREIGN KEY(DealerBranchID) REFERENCES DealerBranch(ID)
GO

ALTER TABLE DMSWOWarrantyClaim ADD
CONSTRAINT [FK_DMSWOWarrantyClaim[many]]_DealerBranch[one]]] FOREIGN KEY(DealerBranchID) REFERENCES DealerBranch(ID)
GO

COMMIT
GO





PRINT '/*Vehicle Model*/'
GO
SET XACT_ABORT ON
GO

BEGIN TRANSACTION
GO

ALTER TABLE VechileType DROP
CONSTRAINT [FK_VechileType_VechileModel[one]]] 
GO

ALTER TABLE StockTarget DROP
CONSTRAINT [FK_StockTarget_VechileModel[one]]] 
GO

ALTER TABLE TransactionControlPK DROP
CONSTRAINT [FK_TransactionControlPK_VechileModel[one]]] 
GO

ALTER TABLE VechileModel DROP
CONSTRAINT [FK_VechileModel_Category[one]]] 
GO

EXEC sp_rename
	'dbo.PK_Model' ,
	'tmp__PK_Model' ,
	'OBJECT'
GO

EXEC sp_rename
	'dbo.VechileModel' ,
	'tmp__VechileModel_6' ,
	'OBJECT'
GO

CREATE TABLE VechileModel
	   (
		 ID SMALLINT NOT NULL
					 IDENTITY
					 CONSTRAINT PK_Model PRIMARY KEY ,
		 VechileModelCode VARCHAR(4) ,
		 CategoryID TINYINT ,
		 Description VARCHAR(40) ,
		 VechileModelIndCode VARCHAR(30) ,
		 IndDescription VARCHAR(40) ,
		 SalesFlag TINYINT ,
		 RowStatus SMALLINT ,
		 CreatedBy VARCHAR(20) ,
		 CreatedTime DATETIME ,
		 LastUpdateBy VARCHAR(20) ,
		 LastUpdateTime DATETIME
	   )
GO

ALTER TABLE VechileModel ADD
CONSTRAINT [FK_VechileModel[many]]_Category[one]]] FOREIGN KEY(CategoryID) REFERENCES Category(ID)
GO

SET IDENTITY_INSERT VechileModel ON
GO

INSERT	INTO VechileModel
		(
		  ID ,
		  VechileModelCode ,
		  CategoryID ,
		  Description ,
		  RowStatus ,
		  CreatedBy ,
		  CreatedTime ,
		  LastUpdateBy ,
		  LastUpdateTime
		)
SELECT	ID ,
		VechileModelCode ,
		CategoryID ,
		Description ,
		RowStatus ,
		CreatedBy ,
		CreatedTime ,
		LastUpdateBy ,
		LastUpdateTime
FROM	tmp__VechileModel_6
GO

SET IDENTITY_INSERT VechileModel OFF
GO

DROP TABLE tmp__VechileModel_6
GO

ALTER TABLE TransactionControlPK ADD
CONSTRAINT [FK_TransactionControlPK_VechileModel[one]]] FOREIGN KEY(ModelID) REFERENCES VechileModel(ID)
GO

ALTER TABLE StockTarget ADD
CONSTRAINT [FK_StockTarget_VechileModel[one]]] FOREIGN KEY(ModelID) REFERENCES VechileModel(ID)
GO

ALTER TABLE VechileType ADD
CONSTRAINT [FK_VechileType_VechileModel[one]]] FOREIGN KEY(ModelID) REFERENCES VechileModel(ID)
GO

COMMIT
GO




PRINT '/*Vehile Type*/'
GO


SET XACT_ABORT ON
GO

BEGIN TRANSACTION
GO

ALTER TABLE VechileColor DROP
CONSTRAINT [FK_VechileColor_VechileType[one]]] 
GO

ALTER TABLE FSCampaignVehicle DROP
CONSTRAINT [FK_FSCampaignVehicle[many]]_VechileType[one]]] 
GO

ALTER TABLE LaborMaster DROP
CONSTRAINT [FK_LaborMaster_VechileType[one]]] 
GO

ALTER TABLE EventSales DROP
CONSTRAINT [FK_EventSales[many]]_VechileType[one]]] 
GO

ALTER TABLE BPIklan DROP
CONSTRAINT [FK_BPIklan[many]]_VechileType[one]]] 
GO

ALTER TABLE PameranDisplay DROP
CONSTRAINT [FK_PameranDisplay[many]]_VechileType[one]]] 
GO

ALTER TABLE MCPDetail DROP
CONSTRAINT [FK_MCPDetail[many]]_VechileType[one]]] 
GO

ALTER TABLE DealerStockReportHeader DROP
CONSTRAINT [FK_DealerStockReportHeader[many]]_VechileType[one]]] 
GO

ALTER TABLE EventParameter DROP
CONSTRAINT [FK_EventParameter[many]]_VechileType[one]]] 
GO

ALTER TABLE LeasingFee DROP
CONSTRAINT [FK_LeasingFee[many]]_VechileType[one]]] 
GO

ALTER TABLE EventLaporanPenjualan DROP
CONSTRAINT [FK_EventLaporanPenjualan[many]]_VechileType[one]]] 
GO

ALTER TABLE BenefitMasterVehicleType DROP
CONSTRAINT [FK_BenefitMasterVehicleType[many]]_VechileType[one]]] 
GO

ALTER TABLE EventReport DROP
CONSTRAINT [FK_EventReport[many]]_VechileType[one]]] 
GO

ALTER TABLE EventProposalDetail DROP
CONSTRAINT [FK_EventProposalDetail[many]]_VechileType[one]]] 
GO

ALTER TABLE SAPCustomer DROP
CONSTRAINT [FK_SAPCustomer[many]]_VechileType[one]]] 
GO

ALTER TABLE SPLDetail DROP
CONSTRAINT [FK_SPLDetail[one]]_VechileType[one]]] 
GO

ALTER TABLE ConditionMaster DROP
CONSTRAINT [FK_ConditionMaster[many]]_VechileType[one]]] 
GO

ALTER TABLE LKPPDetail DROP
CONSTRAINT [FK_LKPPDetail[many]]_VechileType[one]]] 
GO

ALTER TABLE WSCParameterVehicle DROP
CONSTRAINT FK_WSCParameterVehicle_VechileType 
GO

ALTER TABLE VechileType DROP
CONSTRAINT [FK_VechileType[many]]_VehicleClass[one]]] ,
CONSTRAINT [FK_VechileType_Category[one]]] ,
CONSTRAINT [FK_VechileType_VechileModel[one]]] 
GO

EXEC sp_rename
	'dbo.PK_Type' ,
	'tmp__PK_Type' ,
	'OBJECT'
GO

EXEC sp_rename
	'dbo.VechileType' ,
	'tmp__VechileType_7' ,
	'OBJECT'
GO

CREATE TABLE VechileType
	   (
		 ID SMALLINT NOT NULL
					 IDENTITY
					 CONSTRAINT PK_Type PRIMARY KEY ,
		 VechileTypeCode VARCHAR(4) ,
		 ModelID SMALLINT ,
		 CategoryID TINYINT ,
		 ProductCategoryID SMALLINT ,
		 Description VARCHAR(100) ,
		 Status VARCHAR(1) ,
		 VehicleClassID INT ,
		 IsVehicleKind1 TINYINT ,
		 IsVehicleKind2 TINYINT ,
		 IsVehicleKind3 TINYINT ,
		 IsVehicleKind4 TINYINT ,
		 MaxTOPDays INT ,
		 SAPModel NVARCHAR(20) ,
		 SegmentType VARCHAR(40) ,
		 VariantType VARCHAR(30) ,
		 TransmitType VARCHAR(25) ,
		 DriveSystemType VARCHAR(25) ,
		 SpeedType VARCHAR(2) ,
		 FuelType VARCHAR(10) ,
		 RowStatus SMALLINT ,
		 CreatedBy VARCHAR(20) ,
		 CreatedTime DATETIME ,
		 LastUpdateBy VARCHAR(20) ,
		 LastUpdateTime DATETIME
	   )
GO

ALTER TABLE VechileType ADD
CONSTRAINT [FK_VechileType[many]]_Category[one]]] FOREIGN KEY(CategoryID) REFERENCES Category(ID),
CONSTRAINT [FK_VechileType[many]]_VechileModel[one]]] FOREIGN KEY(ModelID) REFERENCES VechileModel(ID),
CONSTRAINT [FK_VechileType[many]]_VehicleClass[one]]] FOREIGN KEY(VehicleClassID) REFERENCES VehicleClass(ID)
GO

CREATE INDEX IX_VechileType_VehicleTypeCode ON VechileType(VechileTypeCode)
GO

SET IDENTITY_INSERT VechileType ON
GO

INSERT	INTO VechileType
		(
		  ID ,
		  VechileTypeCode ,
		  ModelID ,
		  CategoryID ,
		  ProductCategoryID ,
		  Description ,
		  Status ,
		  VehicleClassID ,
		  IsVehicleKind1 ,
		  IsVehicleKind2 ,
		  IsVehicleKind3 ,
		  IsVehicleKind4 ,
		  MaxTOPDays ,
		  SAPModel ,
		  RowStatus ,
		  CreatedBy ,
		  CreatedTime ,
		  LastUpdateBy ,
		  LastUpdateTime
		)
SELECT	ID ,
		VechileTypeCode ,
		ModelID ,
		CategoryID ,
		ProductCategoryID ,
		CONVERT(VARCHAR(100), Description) ,
		Status ,
		VehicleClassID ,
		IsVehicleKind1 ,
		IsVehicleKind2 ,
		IsVehicleKind3 ,
		IsVehicleKind4 ,
		MaxTOPDays ,
		SAPModel ,
		RowStatus ,
		CreatedBy ,
		CreatedTime ,
		LastUpdateBy ,
		LastUpdateTime
FROM	tmp__VechileType_7
GO

SET IDENTITY_INSERT VechileType OFF
GO

DROP TABLE tmp__VechileType_7
GO

ALTER TABLE WSCParameterVehicle ADD
CONSTRAINT FK_WSCParameterVehicle_VechileType FOREIGN KEY(VechileTypeID) REFERENCES VechileType(ID)
GO

ALTER TABLE LKPPDetail ADD
CONSTRAINT [FK_LKPPDetail[many]]_VechileType[one]]] FOREIGN KEY(VehicleTypeID) REFERENCES VechileType(ID)
GO

ALTER TABLE ConditionMaster ADD
CONSTRAINT [FK_ConditionMaster[many]]_VechileType[one]]] FOREIGN KEY(VechileTypeID) REFERENCES VechileType(ID)
GO

ALTER TABLE SPLDetail ADD
CONSTRAINT [FK_SPLDetail[one]]_VechileType[one]]] FOREIGN KEY(VehicleTypeID) REFERENCES VechileType(ID)
GO

ALTER TABLE SAPCustomer ADD
CONSTRAINT [FK_SAPCustomer[many]]_VechileType[one]]] FOREIGN KEY(VechileTypeID) REFERENCES VechileType(ID)
GO

ALTER TABLE EventProposalDetail ADD
CONSTRAINT [FK_EventProposalDetail[many]]_VechileType[one]]] FOREIGN KEY(VechileTypeID) REFERENCES VechileType(ID)
GO

ALTER TABLE EventReport ADD
CONSTRAINT [FK_EventReport[many]]_VechileType[one]]] FOREIGN KEY(VechileTypeID) REFERENCES VechileType(ID)
GO

ALTER TABLE BenefitMasterVehicleType ADD
CONSTRAINT [FK_BenefitMasterVehicleType[many]]_VechileType[one]]] FOREIGN KEY(VechileTypeID) REFERENCES VechileType(ID)
GO

ALTER TABLE EventLaporanPenjualan ADD
CONSTRAINT [FK_EventLaporanPenjualan[many]]_VechileType[one]]] FOREIGN KEY(VechileTypeID) REFERENCES VechileType(ID)
GO

ALTER TABLE LeasingFee ADD
CONSTRAINT [FK_LeasingFee[many]]_VechileType[one]]] FOREIGN KEY(VehicleTypeID) REFERENCES VechileType(ID)
GO

ALTER TABLE EventParameter ADD
CONSTRAINT [FK_EventParameter[many]]_VechileType[one]]] FOREIGN KEY(VehicleTypeID) REFERENCES VechileType(ID)
GO

ALTER TABLE DealerStockReportHeader ADD
CONSTRAINT [FK_DealerStockReportHeader[many]]_VechileType[one]]] FOREIGN KEY(VechileTypeID) REFERENCES VechileType(ID)
GO

ALTER TABLE MCPDetail ADD
CONSTRAINT [FK_MCPDetail[many]]_VechileType[one]]] FOREIGN KEY(VehicleTypeID) REFERENCES VechileType(ID)
GO

ALTER TABLE PameranDisplay ADD
CONSTRAINT [FK_PameranDisplay[many]]_VechileType[one]]] FOREIGN KEY(VechileTypeID) REFERENCES VechileType(ID)
GO

ALTER TABLE BPIklan ADD
CONSTRAINT [FK_BPIklan[many]]_VechileType[one]]] FOREIGN KEY(VechileTypeID) REFERENCES VechileType(ID)
GO

ALTER TABLE EventSales ADD
CONSTRAINT [FK_EventSales[many]]_VechileType[one]]] FOREIGN KEY(VechileTypeID) REFERENCES VechileType(ID)
GO

ALTER TABLE LaborMaster ADD
CONSTRAINT [FK_LaborMaster_VechileType[one]]] FOREIGN KEY(VechileTypeID) REFERENCES VechileType(ID)
GO

ALTER TABLE FSCampaignVehicle ADD
CONSTRAINT [FK_FSCampaignVehicle[many]]_VechileType[one]]] FOREIGN KEY(VehicleTypeID) REFERENCES VechileType(ID)
GO

ALTER TABLE VechileColor ADD
CONSTRAINT [FK_VechileColor_VechileType[one]]] FOREIGN KEY(VechileTypeID) REFERENCES VechileType(ID)
GO

COMMIT
GO



/*vehicle Color*/
ALTER TABLE VechileColor ALTER COLUMN
MaterialDescription VARCHAR(100)
GO


PRINT '/*CityPart*/'
GO
SET XACT_ABORT ON
GO

BEGIN TRANSACTION
GO

ALTER TABLE PartShop DROP
CONSTRAINT [FK_PartShop[many]]_CityPart[one]]] 
GO

ALTER TABLE CityPart DROP
CONSTRAINT [FK_CityPart[many]]_Province[one]]] 
GO

EXEC sp_rename
	'dbo.PK_CityPart' ,
	'tmp__PK_CityPart' ,
	'OBJECT'
GO

EXEC sp_rename
	'dbo.CityPart' ,
	'tmp__CityPart_8' ,
	'OBJECT'
GO

CREATE TABLE CityPart
	   (
		 ID INT NOT NULL
				IDENTITY
				CONSTRAINT PK_CityPart PRIMARY KEY ,
		 ProvinceID INT ,
		 CityID INT ,
		 CityName VARCHAR(50) ,
		 CityCode VARCHAR(10) ,
		 RowStatus SMALLINT ,
		 CreatedBy VARCHAR(20) ,
		 CreatedTime DATETIME ,
		 LastUpdateBy VARCHAR(20) ,
		 LastUpdateTime DATETIME
	   )
GO

ALTER TABLE CityPart ADD
CONSTRAINT [FK_CityPart[many]]_Province[one]]] FOREIGN KEY(ProvinceID) REFERENCES Province(ID)
GO

SET IDENTITY_INSERT CityPart ON
GO

INSERT	INTO CityPart
		(
		  ID ,
		  ProvinceID ,
		  CityName ,
		  CityCode ,
		  RowStatus ,
		  CreatedBy ,
		  CreatedTime ,
		  LastUpdateBy ,
		  LastUpdateTime
		)
SELECT	ID ,
		ProvinceID ,
		CityName ,
		CityCode ,
		RowStatus ,
		CreatedBy ,
		CreatedTime ,
		LastUpdateBy ,
		LastUpdateTime
FROM	tmp__CityPart_8
GO

SET IDENTITY_INSERT CityPart OFF
GO

DROP TABLE tmp__CityPart_8
GO

ALTER TABLE PartShop ADD
CONSTRAINT [FK_PartShop[many]]_CityPart[one]]] FOREIGN KEY(CityPartID) REFERENCES CityPart(ID)
GO

COMMIT
GO




PRINT '/*StandardCOde*/'
SET XACT_ABORT ON
GO

BEGIN TRANSACTION
GO

EXEC sp_rename
	'dbo.PK_StandardCode' ,
	'tmp__PK_StandardCode' ,
	'OBJECT'
GO

EXEC sp_rename
	'dbo.StandardCode' ,
	'tmp__StandardCode_9' ,
	'OBJECT'
GO

CREATE TABLE StandardCode
	   (
		 ID INT NOT NULL
				IDENTITY
				CONSTRAINT PK_StandardCode PRIMARY KEY ,
		 Category VARCHAR(100) ,
		 ValueId INT NOT NULL ,
		 ValueCode VARCHAR(200) ,
		 ValueDesc VARCHAR(200) ,
		 Sequence INT ,
		 RowStatus SMALLINT ,
		 CreatedBy VARCHAR(20) ,
		 CreatedTime DATETIME ,
		 LastUpdateBy VARCHAR(20) ,
		 LastUpdateTime DATETIME
	   )
GO

SET IDENTITY_INSERT StandardCode ON
GO

INSERT	INTO StandardCode
		(
		  ID ,
		  Category ,
		  ValueId ,
		  ValueDesc ,
		  Sequence ,
		  RowStatus ,
		  CreatedBy ,
		  CreatedTime ,
		  LastUpdateBy ,
		  LastUpdateTime
		)
SELECT	ID ,
		Category ,
		ValueId ,
		ValueDesc ,
		Sequence ,
		RowStatus ,
		CreatedBy ,
		CreatedTime ,
		LastUpdateBy ,
		LastUpdateTime
FROM	tmp__StandardCode_9
GO

SET IDENTITY_INSERT StandardCode OFF
GO

DROP TABLE tmp__StandardCode_9
GO

COMMIT
GO





PRINT '/*WSCHeader*/'
GO
SET XACT_ABORT ON
GO

BEGIN TRANSACTION
GO

ALTER TABLE WSCDamageRequestPart DROP
CONSTRAINT [FK_WSCDamageRequestPart[many]]_WSCHeader[one]]] 
GO

ALTER TABLE WSCDetail DROP
CONSTRAINT [FK_WSCDetail[many]]_WSCHeader[one]]] 
GO

ALTER TABLE WSCEvidence DROP
CONSTRAINT [FK_WSCEvidence[many]]_WSCHeader[one]]] 
GO

ALTER TABLE WSCHeader DROP
CONSTRAINT IX_WSCHeaderUnique ,
CONSTRAINT [FK_WSCHeader_ChassisMaster[one]]] ,
CONSTRAINT [FK_WSCHeader_Dealer[one]]] ,
CONSTRAINT [FK_WSCHeader_Reason[one]]] 
GO

EXEC sp_rename
	'dbo.PK_WSCHeader' ,
	'tmp__PK_WSCHeader' ,
	'OBJECT'
GO

EXEC sp_rename
	'dbo.WSCHeader' ,
	'tmp__WSCHeader_12' ,
	'OBJECT'
GO

CREATE TABLE WSCHeader
	   (
		 ID INT NOT NULL
				IDENTITY
				CONSTRAINT PK_WSCHeader PRIMARY KEY ,
		 ClaimType VARCHAR(2) ,
		 DealerID SMALLINT ,
		 DealerBranchID INT ,
		 ClaimNumber VARCHAR(13) ,
		 RefClaimNumber VARCHAR(6) ,
		 ChassisMasterID INT ,
		 FailureDate DATETIME ,
		 ServiceDate DATETIME ,
		 Miliage INT ,
		 PQR VARCHAR(20) ,
		 PQRStatus VARCHAR(1) ,
		 CodeA VARCHAR(4) ,
		 CodeB VARCHAR(4) ,
		 CodeC VARCHAR(4) ,
		 Description VARCHAR(100) ,
		 EvidencePhoto VARCHAR(1) ,
		 EvidenceInvoice VARCHAR(1) ,
		 EvidenceDmgPart VARCHAR(1) ,
		 EvidenceRepair VARCHAR(1) ,
		 EvidenceWSCLetter VARCHAR(1) ,
		 EvidenceWSCTechnical VARCHAR(1) ,
		 Causes VARCHAR(1000) ,
		 Results VARCHAR(1000) ,
		 Notes VARCHAR(1000) ,
		 ReqDmgPart VARCHAR(1) ,
		 ReqDmgPartBy VARCHAR(20) ,
		 ReqDmgPartTime DATETIME ,
		 NotificationNumber VARCHAR(10) ,
		 DecideDate DATETIME ,
		 Status VARCHAR(1) ,
		 ClaimStatus VARCHAR(4) ,
		 ReasonID SMALLINT ,
		 LaborAmount MONEY ,
		 PartAmount MONEY ,
		 PartReceiveBy VARCHAR(20) ,
		 PartReceiveTime DATETIME ,
		 DownLoadBy VARCHAR(20) ,
		 DownLoadTime DATETIME ,
		 ResponseTime DATETIME ,
		 WorkOrderNumber VARCHAR(50) ,
		 RowStatus SMALLINT ,
		 CreatedBy VARCHAR(50) ,
		 CreatedTime DATETIME ,
		 LastUpdateBy VARCHAR(50) ,
		 LastUpdateTime DATETIME ,
		 CONSTRAINT IX_WSCHeaderUnique UNIQUE ( DealerID, ClaimNumber )
	   )
GO

ALTER TABLE WSCHeader ADD
CONSTRAINT [FK_WSCHeader[many]]_ChassisMaster[one]]] FOREIGN KEY(ChassisMasterID) REFERENCES ChassisMaster(ID),
CONSTRAINT [FK_WSCHeader[many]]_Dealer[one]]] FOREIGN KEY(DealerID) REFERENCES Dealer(ID),
CONSTRAINT [FK_WSCHeader[one]]_Reason[one]]] FOREIGN KEY(ReasonID) REFERENCES Reason(ID)
GO

CREATE INDEX IX_WSCHeader_ChassisMasterID ON WSCHeader(ChassisMasterID)
GO

CREATE INDEX IX_WSCHeader_DealerID ON WSCHeader(DealerID)
GO

SET IDENTITY_INSERT WSCHeader ON
GO

INSERT	INTO WSCHeader
		(
		  ID ,
		  ClaimType ,
		  DealerID ,
		  ClaimNumber ,
		  RefClaimNumber ,
		  ChassisMasterID ,
		  FailureDate ,
		  ServiceDate ,
		  Miliage ,
		  PQR ,
		  PQRStatus ,
		  CodeA ,
		  CodeB ,
		  CodeC ,
		  Description ,
		  EvidencePhoto ,
		  EvidenceInvoice ,
		  EvidenceDmgPart ,
		  EvidenceRepair ,
		  EvidenceWSCLetter ,
		  EvidenceWSCTechnical ,
		  Causes ,
		  Results ,
		  Notes ,
		  ReqDmgPart ,
		  ReqDmgPartBy ,
		  ReqDmgPartTime ,
		  NotificationNumber ,
		  DecideDate ,
		  Status ,
		  ClaimStatus ,
		  ReasonID ,
		  LaborAmount ,
		  PartAmount ,
		  PartReceiveBy ,
		  PartReceiveTime ,
		  DownLoadBy ,
		  DownLoadTime ,
		  ResponseTime ,
		  RowStatus ,
		  CreatedBy ,
		  CreatedTime ,
		  LastUpdateBy ,
		  LastUpdateTime
		)
SELECT	ID ,
		ClaimType ,
		DealerID ,
		ClaimNumber ,
		RefClaimNumber ,
		ChassisMasterID ,
		FailureDate ,
		ServiceDate ,
		Miliage ,
		PQR ,
		PQRStatus ,
		CodeA ,
		CodeB ,
		CodeC ,
		Description ,
		EvidencePhoto ,
		EvidenceInvoice ,
		EvidenceDmgPart ,
		EvidenceRepair ,
		EvidenceWSCLetter ,
		EvidenceWSCTechnical ,
		Causes ,
		Results ,
		Notes ,
		ReqDmgPart ,
		ReqDmgPartBy ,
		ReqDmgPartTime ,
		NotificationNumber ,
		DecideDate ,
		Status ,
		ClaimStatus ,
		ReasonID ,
		LaborAmount ,
		PartAmount ,
		PartReceiveBy ,
		PartReceiveTime ,
		DownLoadBy ,
		DownLoadTime ,
		ResponseTime ,
		RowStatus ,
		CreatedBy ,
		CreatedTime ,
		LastUpdateBy ,
		LastUpdateTime
FROM	tmp__WSCHeader_12
GO

SET IDENTITY_INSERT WSCHeader OFF
GO

DROP TABLE tmp__WSCHeader_12
GO

ALTER TABLE WSCEvidence ADD
CONSTRAINT [FK_WSCEvidence[many]]_WSCHeader[one]]] FOREIGN KEY(WSCHeaderID) REFERENCES WSCHeader(ID)
GO

ALTER TABLE WSCDetail ADD
CONSTRAINT [FK_WSCDetail[many]]_WSCHeader[one]]] FOREIGN KEY(WSCHeaderID) REFERENCES WSCHeader(ID)
GO

ALTER TABLE WSCDamageRequestPart ADD
CONSTRAINT [FK_WSCDamageRequestPart[many]]_WSCHeader[one]]] FOREIGN KEY(WSCHeaderID) REFERENCES WSCHeader(ID)
GO

COMMIT
GO



 

PRINT '/*WSCDetail*/'
GO

SET XACT_ABORT ON
GO

BEGIN TRANSACTION
GO

ALTER TABLE WSCDetail DROP
CONSTRAINT [FK_WSCDetail[many]]_WSCHeader[one]]] ,
CONSTRAINT [FK_WSCDetail_LaborMaster[one]]] ,
CONSTRAINT [FK_WSCDetail_SparePartMaster[one]]] 
GO

EXEC sp_rename
	'dbo.PK_WSCDetail' ,
	'tmp__PK_WSCDetail' ,
	'OBJECT'
GO

EXEC sp_rename
	'dbo.WSCDetail' ,
	'tmp__WSCDetail_13' ,
	'OBJECT'
GO

CREATE TABLE WSCDetail
	   (
		 ID INT NOT NULL
				IDENTITY
				CONSTRAINT PK_WSCDetail PRIMARY KEY ,
		 WSCHeaderID INT ,
		 WSCType VARCHAR(1) ,
		 LaborMasterID INT ,
		 PositionCode VARCHAR(10) ,
		 WorkCode VARCHAR(6) ,
		 SparePartMasterID INT ,
		 Quantity REAL ,
		 PartPrice MONEY ,
		 MainPart SMALLINT ,
		 QuantityReceived REAL ,
		 ReceivedBy VARCHAR(20) ,
		 ReceivedDate DATETIME ,
		 Status SMALLINT ,
		 RowStatus SMALLINT ,
		 CreatedBy VARCHAR(20) ,
		 CreatedTime DATETIME ,
		 LastUpdateBy VARCHAR(20) ,
		 LastUpdateTime DATETIME
	   )
GO

ALTER TABLE WSCDetail ADD
CONSTRAINT [FK_WSCDetail[many]]_LaborMaster[one]]] FOREIGN KEY(LaborMasterID) REFERENCES LaborMaster(ID),
CONSTRAINT [FK_WSCDetail[many]]_SparePartMaster[one]]] FOREIGN KEY(SparePartMasterID) REFERENCES SparePartMaster(ID),
CONSTRAINT [FK_WSCDetail[many]]_WSCHeader[one]]] FOREIGN KEY(WSCHeaderID) REFERENCES WSCHeader(ID)
GO

CREATE INDEX IX_WSCDetail_WSCHeaderID ON WSCDetail(WSCHeaderID)
GO

SET IDENTITY_INSERT WSCDetail ON
GO

INSERT	INTO WSCDetail
		(
		  ID ,
		  WSCHeaderID ,
		  WSCType ,
		  LaborMasterID ,
		  PositionCode ,
		  WorkCode ,
		  SparePartMasterID ,
		  Quantity ,
		  PartPrice ,
		  MainPart ,
		  QuantityReceived ,
		  ReceivedBy ,
		  ReceivedDate ,
		  Status ,
		  RowStatus ,
		  CreatedBy ,
		  CreatedTime ,
		  LastUpdateBy ,
		  LastUpdateTime
		)
SELECT	ID ,
		WSCHeaderID ,
		WSCType ,
		LaborMasterID ,
		PositionCode ,
		WorkCode ,
		SparePartMasterID ,
		Quantity ,
		PartPrice ,
		MainPart ,
		QuantityReceived ,
		ReceivedBy ,
		ReceivedDate ,
		Status ,
		RowStatus ,
		CreatedBy ,
		CreatedTime ,
		LastUpdateBy ,
		LastUpdateTime
FROM	tmp__WSCDetail_13
GO

SET IDENTITY_INSERT WSCDetail OFF
GO

DROP TABLE tmp__WSCDetail_13
GO

COMMIT
GO



PRINT '/*WSCHeaderBB*/'
GO
SET XACT_ABORT ON
GO

BEGIN TRANSACTION
GO

ALTER TABLE WSCEvidenceBB DROP
CONSTRAINT [FK_WSCEvidenceBB[many]]_WSCHeaderBB[one]]] 
GO

ALTER TABLE WSCDamageRequestPartBB DROP
CONSTRAINT [FK_WSCDamageRequestPartBB[many]]_WSCHeaderBB[one]]] 
GO

ALTER TABLE WSCDetailBB DROP
CONSTRAINT [FK_WSCDetailBB[many]]_WSCHeaderBB[one]]] 
GO

ALTER TABLE WSCHeaderBB DROP
CONSTRAINT IX_WSCHeaderBBUnique ,
CONSTRAINT [FK_WSCHeaderBB_ChassisMasterBB[one]]] ,
CONSTRAINT [FK_WSCHeaderBB_Dealer[one]]] ,
CONSTRAINT [FK_WSCHeaderBB_Reason[one]]] 
GO

EXEC sp_rename
	'dbo.PK_WSCHeaderBB' ,
	'tmp__PK_WSCHeaderBB' ,
	'OBJECT'
GO

EXEC sp_rename
	'dbo.WSCHeaderBB' ,
	'tmp__WSCHeaderBB_14' ,
	'OBJECT'
GO

CREATE TABLE WSCHeaderBB
	   (
		 ID INT NOT NULL
				IDENTITY
				CONSTRAINT PK_WSCHeaderBB PRIMARY KEY ,
		 ClaimType VARCHAR(2) ,
		 DealerID SMALLINT ,
		 DealerBranchID INT ,
		 ClaimNumber VARCHAR(6) ,
		 RefClaimNumber VARCHAR(6) ,
		 ChassisMasterBBID INT ,
		 FailureDate DATETIME ,
		 ServiceDate DATETIME ,
		 Miliage INT ,
		 PQR VARCHAR(20) ,
		 PQRStatus VARCHAR(1) ,
		 CodeA VARCHAR(4) ,
		 CodeB VARCHAR(4) ,
		 CodeC VARCHAR(4) ,
		 Description VARCHAR(100) ,
		 EvidencePhoto VARCHAR(1) ,
		 EvidenceInvoice VARCHAR(1) ,
		 EvidenceDmgPart VARCHAR(1) ,
		 EvidenceRepair VARCHAR(1) ,
		 EvidenceWSCLetter VARCHAR(1) ,
		 EvidenceWSCTechnical VARCHAR(1) ,
		 Causes VARCHAR(1000) ,
		 Results VARCHAR(1000) ,
		 Notes VARCHAR(1000) ,
		 ReqDmgPart VARCHAR(1) ,
		 ReqDmgPartBy VARCHAR(20) ,
		 ReqDmgPartTime DATETIME ,
		 NotificationNumber VARCHAR(10) ,
		 DecideDate DATETIME ,
		 Status VARCHAR(1) ,
		 ClaimStatus VARCHAR(4) ,
		 ReasonID SMALLINT ,
		 LaborAmount MONEY ,
		 PartAmount MONEY ,
		 PartReceiveBy VARCHAR(20) ,
		 PartReceiveTime DATETIME ,
		 DownLoadBy VARCHAR(20) ,
		 DownLoadTime DATETIME ,
		 ResponseTime DATETIME ,
		 WorkOrderNumber VARCHAR(50) ,
		 RowStatus SMALLINT ,
		 CreatedBy VARCHAR(50) ,
		 CreatedTime DATETIME ,
		 LastUpdateBy VARCHAR(50) ,
		 LastUpdateTime DATETIME ,
		 CONSTRAINT IX_WSCHeaderBBUnique UNIQUE ( DealerID, ClaimNumber )
	   )
GO

ALTER TABLE WSCHeaderBB ADD
CONSTRAINT [FK_WSCHeaderBB[many]]_ChassisMasterBB[one]]] FOREIGN KEY(ChassisMasterBBID) REFERENCES ChassisMasterBB(ID),
CONSTRAINT [FK_WSCHeaderBB[many]]_Dealer[one]]] FOREIGN KEY(DealerID) REFERENCES Dealer(ID),
CONSTRAINT [FK_WSCHeaderBB[many]]_Reason[one]]] FOREIGN KEY(ReasonID) REFERENCES Reason(ID)
GO

SET IDENTITY_INSERT WSCHeaderBB ON
GO

INSERT	INTO WSCHeaderBB
		(
		  ID ,
		  ClaimType ,
		  DealerID ,
		  ClaimNumber ,
		  RefClaimNumber ,
		  ChassisMasterBBID ,
		  FailureDate ,
		  ServiceDate ,
		  Miliage ,
		  PQR ,
		  PQRStatus ,
		  CodeA ,
		  CodeB ,
		  CodeC ,
		  Description ,
		  EvidencePhoto ,
		  EvidenceInvoice ,
		  EvidenceDmgPart ,
		  EvidenceRepair ,
		  EvidenceWSCLetter ,
		  EvidenceWSCTechnical ,
		  Causes ,
		  Results ,
		  Notes ,
		  ReqDmgPart ,
		  ReqDmgPartBy ,
		  ReqDmgPartTime ,
		  NotificationNumber ,
		  DecideDate ,
		  Status ,
		  ClaimStatus ,
		  ReasonID ,
		  LaborAmount ,
		  PartAmount ,
		  PartReceiveBy ,
		  PartReceiveTime ,
		  DownLoadBy ,
		  DownLoadTime ,
		  ResponseTime ,
		  RowStatus ,
		  CreatedBy ,
		  CreatedTime ,
		  LastUpdateBy ,
		  LastUpdateTime
		)
SELECT	ID ,
		ClaimType ,
		DealerID ,
		ClaimNumber ,
		RefClaimNumber ,
		ChassisMasterBBID ,
		FailureDate ,
		ServiceDate ,
		Miliage ,
		PQR ,
		PQRStatus ,
		CodeA ,
		CodeB ,
		CodeC ,
		Description ,
		EvidencePhoto ,
		EvidenceInvoice ,
		EvidenceDmgPart ,
		EvidenceRepair ,
		EvidenceWSCLetter ,
		EvidenceWSCTechnical ,
		Causes ,
		Results ,
		Notes ,
		ReqDmgPart ,
		ReqDmgPartBy ,
		ReqDmgPartTime ,
		NotificationNumber ,
		DecideDate ,
		Status ,
		ClaimStatus ,
		ReasonID ,
		LaborAmount ,
		PartAmount ,
		PartReceiveBy ,
		PartReceiveTime ,
		DownLoadBy ,
		DownLoadTime ,
		ResponseTime ,
		RowStatus ,
		CreatedBy ,
		CreatedTime ,
		LastUpdateBy ,
		LastUpdateTime
FROM	tmp__WSCHeaderBB_14
GO

SET IDENTITY_INSERT WSCHeaderBB OFF
GO

DROP TABLE tmp__WSCHeaderBB_14
GO

ALTER TABLE WSCDetailBB ADD
CONSTRAINT [FK_WSCDetailBB[many]]_WSCHeaderBB[one]]] FOREIGN KEY(WSCHeaderBBID) REFERENCES WSCHeaderBB(ID)
GO

ALTER TABLE WSCDamageRequestPartBB ADD
CONSTRAINT [FK_WSCDamageRequestPartBB[many]]_WSCHeaderBB[one]]] FOREIGN KEY(WSCHeaderBBID) REFERENCES WSCHeaderBB(ID)
GO

ALTER TABLE WSCEvidenceBB ADD
CONSTRAINT [FK_WSCEvidenceBB[many]]_WSCHeaderBB[one]]] FOREIGN KEY(WSCHeaderBBID) REFERENCES WSCHeaderBB(ID)
GO

COMMIT
GO




PRINT '/*WSCDetailBB*/'
GO
SET XACT_ABORT ON
GO

BEGIN TRANSACTION
GO

ALTER TABLE WSCDetailBB DROP
CONSTRAINT [FK_WSCDetailBB[many]]_WSCHeaderBB[one]]] ,
CONSTRAINT [FK_WSCDetailBB_LaborMaster[one]]] ,
CONSTRAINT [FK_WSCDetailBB_SparePartMaster[one]]] 
GO

EXEC sp_rename
	'dbo.PK_WSCDetailBB' ,
	'tmp__PK_WSCDetailBB' ,
	'OBJECT'
GO

EXEC sp_rename
	'dbo.WSCDetailBB' ,
	'tmp__WSCDetailBB_15' ,
	'OBJECT'
GO

CREATE TABLE WSCDetailBB
	   (
		 ID INT NOT NULL
				IDENTITY
				CONSTRAINT PK_WSCDetailBB PRIMARY KEY ,
		 WSCHeaderBBID INT ,
		 WSCType VARCHAR(1) ,
		 LaborMasterID INT ,
		 PositionCode VARCHAR(10) ,
		 WorkCode VARCHAR(6) ,
		 SparePartMasterID INT ,
		 Quantity REAL ,
		 PartPrice MONEY ,
		 MainPart SMALLINT ,
		 QuantityReceived REAL ,
		 ReceivedBy VARCHAR(20) ,
		 ReceivedDate DATETIME ,
		 Status SMALLINT ,
		 RowStatus SMALLINT ,
		 CreatedBy VARCHAR(20) ,
		 CreatedTime DATETIME ,
		 LastUpdateBy VARCHAR(20) ,
		 LastUpdateTime DATETIME
	   )
GO

ALTER TABLE WSCDetailBB ADD
CONSTRAINT [FK_WSCDetailBB[many]]_LaborMaster[one]]] FOREIGN KEY(LaborMasterID) REFERENCES LaborMaster(ID),
CONSTRAINT [FK_WSCDetailBB[many]]_SparePartMaster[one]]] FOREIGN KEY(SparePartMasterID) REFERENCES SparePartMaster(ID),
CONSTRAINT [FK_WSCDetailBB[many]]_WSCHeaderBB[one]]] FOREIGN KEY(WSCHeaderBBID) REFERENCES WSCHeaderBB(ID)
GO

SET IDENTITY_INSERT WSCDetailBB ON
GO

INSERT	INTO WSCDetailBB
		(
		  ID ,
		  WSCHeaderBBID ,
		  WSCType ,
		  LaborMasterID ,
		  PositionCode ,
		  WorkCode ,
		  SparePartMasterID ,
		  Quantity ,
		  PartPrice ,
		  MainPart ,
		  QuantityReceived ,
		  ReceivedBy ,
		  ReceivedDate ,
		  Status ,
		  RowStatus ,
		  CreatedBy ,
		  CreatedTime ,
		  LastUpdateBy ,
		  LastUpdateTime
		)
SELECT	ID ,
		WSCHeaderBBID ,
		WSCType ,
		LaborMasterID ,
		PositionCode ,
		WorkCode ,
		SparePartMasterID ,
		Quantity ,
		PartPrice ,
		MainPart ,
		QuantityReceived ,
		ReceivedBy ,
		ReceivedDate ,
		Status ,
		RowStatus ,
		CreatedBy ,
		CreatedTime ,
		LastUpdateBy ,
		LastUpdateTime
FROM	tmp__WSCDetailBB_15
GO

SET IDENTITY_INSERT WSCDetailBB OFF
GO

DROP TABLE tmp__WSCDetailBB_15
GO

COMMIT
GO


PRINT '/*EstimationEquipHeader*/'
GO
SET XACT_ABORT ON
GO

BEGIN TRANSACTION
GO

ALTER TABLE EstimationEquipDetail DROP
CONSTRAINT FK_EstimationEquipDetail_EstimationEquipHeader 
GO

ALTER TABLE EstimationEquipHeader DROP
CONSTRAINT FK_EstimationEquipHeader_Dealer ,
CONSTRAINT FK_EstimationEquipHeader_DepositBKewajibanHeader 
GO

EXEC sp_rename
	'dbo.PK_EstimationEquipHeader' ,
	'tmp__PK_EstimationEquipHeader' ,
	'OBJECT'
GO

EXEC sp_rename
	'dbo.EstimationEquipHeader' ,
	'tmp__EstimationEquipHeader_17' ,
	'OBJECT'
GO

CREATE TABLE EstimationEquipHeader
	   (
		 ID INT NOT NULL
				IDENTITY
				CONSTRAINT PK_EstimationEquipHeader PRIMARY KEY ,
		 EstimationNumber VARCHAR(13) NOT NULL ,
		 DealerID SMALLINT NOT NULL ,
		 DepositBKewajibanHeaderID INT ,
		 Status SMALLINT NOT NULL ,
		 DMSPRNo VARCHAR(50) ,
		 RowStatus SMALLINT NOT NULL ,
		 CreatedBy VARCHAR(20) ,
		 CreatedTime DATETIME ,
		 LastUpdatedBy VARCHAR(20) ,
		 LastUpdatedTime DATETIME
	   )
GO

ALTER TABLE EstimationEquipHeader ADD
CONSTRAINT FK_EstimationEquipHeader_Dealer FOREIGN KEY(DealerID) REFERENCES Dealer(ID),
CONSTRAINT FK_EstimationEquipHeader_DepositBKewajibanHeader FOREIGN KEY(DepositBKewajibanHeaderID) REFERENCES DepositBKewajibanHeader(ID)
GO

SET IDENTITY_INSERT EstimationEquipHeader ON
GO

INSERT	INTO EstimationEquipHeader
		(
		  ID ,
		  EstimationNumber ,
		  DealerID ,
		  DepositBKewajibanHeaderID ,
		  Status ,
		  RowStatus ,
		  CreatedBy ,
		  CreatedTime ,
		  LastUpdatedBy ,
		  LastUpdatedTime
		)
SELECT	ID ,
		EstimationNumber ,
		DealerID ,
		DepositBKewajibanHeaderID ,
		Status ,
		RowStatus ,
		CreatedBy ,
		CreatedTime ,
		LastUpdatedBy ,
		LastUpdatedTime
FROM	tmp__EstimationEquipHeader_17
GO

SET IDENTITY_INSERT EstimationEquipHeader OFF
GO

DROP TABLE tmp__EstimationEquipHeader_17
GO

ALTER TABLE EstimationEquipDetail ADD
CONSTRAINT FK_EstimationEquipDetail_EstimationEquipHeader FOREIGN KEY(EstimationEquipHeaderID) REFERENCES EstimationEquipHeader(ID)
GO

COMMIT
GO






PRINT '/*EstimationEquipDetail*/'
SET XACT_ABORT ON
GO

BEGIN TRANSACTION
GO

ALTER TABLE EstimationEquipDetail DROP
CONSTRAINT FK_EstimationEquipDetail_EstimationEquipHeader ,
CONSTRAINT FK_EstimationEquipDetail_SparePartMaster 
GO

EXEC sp_rename
	'dbo.PK_EstimationEquipDetail' ,
	'tmp__PK_EstimationEquipDetail' ,
	'OBJECT'
GO

EXEC sp_rename
	'dbo.EstimationEquipDetail' ,
	'tmp__EstimationEquipDetail_19' ,
	'OBJECT'
GO

CREATE TABLE EstimationEquipDetail
	   (
		 ID INT NOT NULL
				IDENTITY
				CONSTRAINT PK_EstimationEquipDetail PRIMARY KEY ,
		 EstimationEquipHeaderID INT NOT NULL ,
		 SparePartMasterID INT NOT NULL ,
		 Harga DECIMAL(19, 4) NOT NULL ,
		 Discount DECIMAL(7, 2) ,
		 EstimationUnit INT NOT NULL ,
		 Status SMALLINT ,
		 ConfirmedDate DATETIME ,
		 Remark VARCHAR(500) ,
		 TotalForecast INT ,
		 RowStatus SMALLINT ,
		 CreatedBy VARCHAR(20) ,
		 CreatedTime DATETIME ,
		 LastUpdatedBy VARCHAR(20) ,
		 LastUpdatedTime DATETIME
	   )
GO

ALTER TABLE EstimationEquipDetail ADD
CONSTRAINT [FK_EstimationEquipDetail[many]]_SparePartMaster[one]]] FOREIGN KEY(SparePartMasterID) REFERENCES SparePartMaster(ID),
CONSTRAINT FK_EstimationEquipDetail_EstimationEquipHeader FOREIGN KEY(EstimationEquipHeaderID) REFERENCES EstimationEquipHeader(ID)
GO

SET IDENTITY_INSERT EstimationEquipDetail ON
GO

INSERT	INTO EstimationEquipDetail
		(
		  ID ,
		  EstimationEquipHeaderID ,
		  SparePartMasterID ,
		  Harga ,
		  Discount ,
		  EstimationUnit ,
		  Status ,
		  ConfirmedDate ,
		  Remark ,
		  RowStatus ,
		  CreatedBy ,
		  CreatedTime ,
		  LastUpdatedBy ,
		  LastUpdatedTime
		)
SELECT	ID ,
		EstimationEquipHeaderID ,
		SparePartMasterID ,
		Harga ,
		Discount ,
		EstimationUnit ,
		Status ,
		ConfirmedDate ,
		Remark ,
		RowStatus ,
		CreatedBy ,
		CreatedTime ,
		LastUpdatedBy ,
		LastUpdatedTime
FROM	tmp__EstimationEquipDetail_19
GO

SET IDENTITY_INSERT EstimationEquipDetail OFF
GO

DROP TABLE tmp__EstimationEquipDetail_19
GO

COMMIT
GO


PRINT '/*Customer Case*/'
SET XACT_ABORT ON
GO

BEGIN TRANSACTION
GO

ALTER TABLE CustomerCaseResponse DROP
CONSTRAINT [FK_CustomerCaseResponse[many]]_CustomerCase[one]]] 
GO

EXEC sp_rename
	'dbo.PK_CustomerCase' ,
	'tmp__PK_CustomerCase' ,
	'OBJECT'
GO

EXEC sp_rename
	'dbo.CustomerCase' ,
	'tmp__CustomerCase_21' ,
	'OBJECT'
GO

CREATE TABLE CustomerCase
	   (
		 ID INT NOT NULL
				IDENTITY
				CONSTRAINT PK_CustomerCase PRIMARY KEY ,
		 DealerID SMALLINT ,
		 SalesforceID NVARCHAR(255) ,
		 CaseNumber NVARCHAR(255) ,
		 CustomerName NVARCHAR(50) ,
		 Phone NVARCHAR(50) ,
		 Email NVARCHAR(50) ,
		 Category NVARCHAR(50) ,
		 SubCategory1 NVARCHAR(50) ,
		 SubCategory2 NVARCHAR(50) ,
		 SubCategory3 NVARCHAR(50) ,
		 SubCategory4 NVARCHAR(50) ,
		 CallerType NCHAR(10) ,
		 CarType NVARCHAR(50) ,
		 Variant NVARCHAR(50) ,
		 EngineNumber NVARCHAR(50) ,
		 ChassisNumber NVARCHAR(50) ,
		 Odometer INT ,
		 PlateNumber NVARCHAR(20) ,
		 Priority SMALLINT ,
		 CaseNumberReff NVARCHAR(255) ,
		 CaseDate DATETIME ,
		 Subject VARCHAR(255) ,
		 Description NVARCHAR(MAX) ,
		 Status SMALLINT ,
		 ReservationNumber VARCHAR(50) ,
		 RowStatus SMALLINT ,
		 CreatedBy VARCHAR(20) ,
		 CreatedTime DATETIME ,
		 LastUpdateBy VARCHAR(20) ,
		 LastUpdateTIme DATETIME
	   )
GO

SET IDENTITY_INSERT CustomerCase ON
GO

INSERT	INTO CustomerCase
		(
		  ID ,
		  DealerID ,
		  SalesforceID ,
		  CaseNumber ,
		  CustomerName ,
		  Phone ,
		  Email ,
		  Category ,
		  SubCategory1 ,
		  SubCategory2 ,
		  SubCategory3 ,
		  SubCategory4 ,
		  CallerType ,
		  CarType ,
		  Variant ,
		  EngineNumber ,
		  ChassisNumber ,
		  Odometer ,
		  PlateNumber ,
		  Priority ,
		  CaseNumberReff ,
		  CaseDate ,
		  Subject ,
		  Description ,
		  Status ,
		  RowStatus ,
		  CreatedBy ,
		  CreatedTime ,
		  LastUpdateBy ,
		  LastUpdateTime
		)
SELECT	ID ,
		DealerID ,
		SalesforceID ,
		CaseNumber ,
		CustomerName ,
		Phone ,
		Email ,
		Category ,
		SubCategory1 ,
		SubCategory2 ,
		SubCategory3 ,
		SubCategory4 ,
		CallerType ,
		CarType ,
		Variant ,
		EngineNumber ,
		ChassisNumber ,
		Odometer ,
		PlateNumber ,
		Priority ,
		CaseNumberReff ,
		CaseDate ,
		Subject ,
		Description ,
		Status ,
		RowStatus ,
		CreatedBy ,
		CreatedTime ,
		LastUpdateBy ,
		LastUpdateTime
FROM	tmp__CustomerCase_21
GO

SET IDENTITY_INSERT CustomerCase OFF
GO

DROP TABLE tmp__CustomerCase_21
GO

ALTER TABLE CustomerCaseResponse ADD
CONSTRAINT [FK_CustomerCaseResponse[many]]_CustomerCase[one]]] FOREIGN KEY(CustomerCaseID) REFERENCES CustomerCase(ID)
GO

COMMIT
GO


SET XACT_ABORT ON
GO

BEGIN TRANSACTION
GO

IF NOT EXISTS ( SELECT	''
				FROM	sys.[indexes] a
				WHERE	a.[name] = 'IX_DeliveryOrder_ChassisMasterID_RowStatus' )
   BEGIN
 
		
		 CREATE INDEX IX_DeliveryOrder_ChassisMasterID_RowStatus ON DeliveryOrder(ChassisMasterID,RowStatus)

   END

GO

COMMIT
GO



PRINT '/*DepositLine*/'
GO
SET XACT_ABORT ON
GO

BEGIN TRANSACTION
GO

ALTER TABLE DepositLine DROP
CONSTRAINT [FK_DepositLine[many]]_Deposit[one]]] 
GO

EXEC sp_rename
	'dbo.PK_DepositLine' ,
	'tmp__PK_DepositLine' ,
	'OBJECT'
GO

EXEC sp_rename
	'dbo.DepositLine' ,
	'tmp__DepositLine_25' ,
	'OBJECT'
GO

CREATE TABLE DepositLine
	   (
		 ID INT NOT NULL
				IDENTITY
				CONSTRAINT PK_DepositLine PRIMARY KEY ,
		 DepositID INT NOT NULL ,
		 DocumentNo VARCHAR(20) ,
		 PostingDate DATETIME ,
		 ClearingDate DATETIME ,
		 Debit MONEY ,
		 Credit MONEY ,
		 ReferenceNo VARCHAR(20) ,
		 InvoiceNo VARCHAR(20) ,
		 Remark VARCHAR(100) ,
		 PaymentType TINYINT NOT NULL
							 CONSTRAINT DF_DepositLine_PaymentType DEFAULT ( 0 ) ,
		 RowStatus SMALLINT ,
		 CreatedBy VARCHAR(20) ,
		 CreatedTime DATETIME ,
		 LastUpdateBy VARCHAR(20) ,
		 LastUpdateTime DATETIME
	   )
GO

ALTER TABLE DepositLine ADD
CONSTRAINT [FK_DepositLine[many]]_Deposit[one]]] FOREIGN KEY(DepositID) REFERENCES Deposit(ID)
GO

CREATE INDEX IX_DepositLine_DepositID ON DepositLine(DepositID)
GO

CREATE INDEX IX_DepositLine_DocumentNo ON DepositLine(DocumentNo)
GO

GRANT SELECT ON DepositLine TO bsi
GO

GRANT SELECT ON DepositLine TO monitoring
GO

GRANT SELECT ON DepositLine TO ccUser
GO

GRANT SELECT ON DepositLine TO analyst
GO

SET IDENTITY_INSERT DepositLine ON
GO

INSERT	INTO DepositLine
		(
		  ID ,
		  DepositID ,
		  DocumentNo ,
		  PostingDate ,
		  ClearingDate ,
		  Debit ,
		  Credit ,
		  ReferenceNo ,
		  InvoiceNo ,
		  Remark ,
		  RowStatus ,
		  CreatedBy ,
		  CreatedTime ,
		  LastUpdateBy ,
		  LastUpdateTime
		)
SELECT	ID ,
		DepositID ,
		DocumentNo ,
		PostingDate ,
		ClearingDate ,
		Debit ,
		Credit ,
		ReferenceNo ,
		InvoiceNo ,
		Remark ,
		RowStatus ,
		CreatedBy ,
		CreatedTime ,
		LastUpdateBy ,
		LastUpdateTime
FROM	tmp__DepositLine_25
GO

SET IDENTITY_INSERT DepositLine OFF
GO

DROP TABLE tmp__DepositLine_25
GO

COMMIT
GO


PRINT '/*Customer Request*/'
GO
SET XACT_ABORT ON
GO

BEGIN TRANSACTION
GO

ALTER TABLE CustomerRequest ADD
CONSTRAINT [FKX_CustomerRequest[one]]_City[many]]] FOREIGN KEY(CityID) REFERENCES City(ID),
CONSTRAINT [FKX_NewCustomerRequest[one]]_Dealer[one]]] FOREIGN KEY(DealerID) REFERENCES Dealer(ID)
GO

COMMIT
GO

PRINT 'EndCustomer'
/*EndCustomer*/
GO
SET XACT_ABORT ON
GO

BEGIN TRANSACTION
GO

ALTER TABLE RevisionChassisMasterProfile DROP
CONSTRAINT [FK_RevisionChassisMasterProfile[one]]_EndCustomer[one]]] 
GO

ALTER TABLE RevisionFaktur DROP
CONSTRAINT [FK_RevisionFaktur[one]]_EndCustomer[one]]] ,
CONSTRAINT [FK_RevisionFaktur[one]]_OldEndCustomer[one]]] 
GO

ALTER TABLE ChassisMaster DROP
CONSTRAINT [FK_ChassisMaster[one]]_EndCustomer[one]]] 
GO

ALTER TABLE RevisionSPKFaktur DROP
CONSTRAINT [FK_RevisionSPKFaktur[one]]_EndCustomer[one]]] 
GO

ALTER TABLE SPKFaktur DROP
CONSTRAINT [FK_SPKFaktur[one]]_EndCustomer[one]]] 
GO

ALTER TABLE EndCustomer DROP
CONSTRAINT [FK_EndCustomer[many]]_Customer[one]]] ,
CONSTRAINT FK_EndCustomer_LKPPHeader ,
CONSTRAINT FK_EndCustomer_PaymentMethodAreaViolation ,
CONSTRAINT FK_EndCustomer_PaymentMethodPenalty 
GO

EXEC sp_rename
	'dbo.PK_EndCustomer' ,
	'tmp__PK_EndCustomer' ,
	'OBJECT'
GO

EXEC sp_rename
	'dbo.EndCustomer' ,
	'tmp__EndCustomer_26' ,
	'OBJECT'
GO

CREATE TABLE EndCustomer
	   (
		 ID INT NOT NULL
				IDENTITY
				CONSTRAINT PK_EndCustomer PRIMARY KEY ,
		 ProjectIndicator VARCHAR(1) NOT NULL ,
		 RefChassisNumberID INT ,
		 CustomerID INT ,
		 Name1 VARCHAR(50) ,
		 FakturDate DATETIME NOT NULL ,
		 OpenFakturDate DATETIME NOT NULL ,
		 FakturNumber VARCHAR(18) NOT NULL ,
		 AreaViolationFlag VARCHAR(50) ,
		 AreaViolationPaymentMethodID TINYINT ,
		 AreaViolationyAmount MONEY ,
		 AreaViolationBankName VARCHAR(30) ,
		 AreaViolationGyroNumber VARCHAR(30) ,
		 PenaltyFlag VARCHAR(50) ,
		 PenaltyPaymentMethodID TINYINT ,
		 PenaltyAmount MONEY ,
		 PenaltyBankName VARCHAR(30) ,
		 PenaltyGyroNumber VARCHAR(30) ,
		 ReferenceLetterFlag VARCHAR(1) ,
		 ReferenceLetter VARCHAR(40) ,
		 SaveBy VARCHAR(20) NOT NULL ,
		 SaveTime DATETIME NOT NULL ,
		 ValidateBy VARCHAR(20) NOT NULL ,
		 ValidateTime DATETIME NOT NULL ,
		 ConfirmBy VARCHAR(20) NOT NULL ,
		 ConfirmTime DATETIME NOT NULL ,
		 DownloadBy VARCHAR(20) NOT NULL ,
		 DownloadTime DATETIME NOT NULL ,
		 PrintedBy VARCHAR(20) NOT NULL ,
		 PrintedTime DATETIME NOT NULL ,
		 CleansingCustomerID INT ,
		 MCPHeaderID INT ,
		 MCPStatus SMALLINT ,
		 LKPPHeaderID INT ,
		 LKPPStatus SMALLINT ,
		 Remark1 VARCHAR(255) ,
		 Remark2 VARCHAR(255) ,
		 HandoverDate DATETIME ,
		 IsTemporary SMALLINT ,
		 RowStatus SMALLINT NOT NULL ,
		 CreatedBy VARCHAR(20) NOT NULL ,
		 CreatedTime DATETIME NOT NULL ,
		 LastUpdateBy VARCHAR(20) NOT NULL ,
		 LastUpdateTime DATETIME NOT NULL
	   )
GO

ALTER TABLE EndCustomer ADD
CONSTRAINT [FK_EndCustomer[many]]_Customer[one]]] FOREIGN KEY(CustomerID) REFERENCES Customer(ID),
CONSTRAINT FK_EndCustomer_LKPPHeader FOREIGN KEY(LKPPHeaderID) REFERENCES LKPPHeader(ID),
CONSTRAINT FK_EndCustomer_PaymentMethodAreaViolation FOREIGN KEY(AreaViolationPaymentMethodID) REFERENCES PaymentMethod(ID),
CONSTRAINT FK_EndCustomer_PaymentMethodPenalty FOREIGN KEY(PenaltyPaymentMethodID) REFERENCES PaymentMethod(ID)
GO

CREATE INDEX IX_EndCustomer_CustomerID ON EndCustomer(CustomerID)
GO

CREATE INDEX IX_EndCustomer_FakturDate ON EndCustomer(FakturDate)
GO

CREATE INDEX IX_EndCustomer_FakturNumber ON EndCustomer(FakturNumber)
GO

CREATE INDEX IX_EndCustomer_OpenFakturDate ON EndCustomer(OpenFakturDate)
GO

CREATE INDEX IX_EndCustomer_ValidateTime ON EndCustomer(ValidateTime)
GO

SET IDENTITY_INSERT EndCustomer ON
GO

INSERT	INTO EndCustomer
		(
		  ID ,
		  ProjectIndicator ,
		  RefChassisNumberID ,
		  CustomerID ,
		  Name1 ,
		  FakturDate ,
		  OpenFakturDate ,
		  FakturNumber ,
		  AreaViolationFlag ,
		  AreaViolationPaymentMethodID ,
		  AreaViolationyAmount ,
		  AreaViolationBankName ,
		  AreaViolationGyroNumber ,
		  PenaltyFlag ,
		  PenaltyPaymentMethodID ,
		  PenaltyAmount ,
		  PenaltyBankName ,
		  PenaltyGyroNumber ,
		  ReferenceLetterFlag ,
		  ReferenceLetter ,
		  SaveBy ,
		  SaveTime ,
		  ValidateBy ,
		  ValidateTime ,
		  ConfirmBy ,
		  ConfirmTime ,
		  DownloadBy ,
		  DownloadTime ,
		  PrintedBy ,
		  PrintedTime ,
		  CleansingCustomerID ,
		  MCPHeaderID ,
		  MCPStatus ,
		  LKPPHeaderID ,
		  LKPPStatus ,
		  Remark1 ,
		  Remark2 ,
		  HandoverDate ,
		  RowStatus ,
		  CreatedBy ,
		  CreatedTime ,
		  LastUpdateBy ,
		  LastUpdateTime
		)
SELECT	ID ,
		ProjectIndicator ,
		RefChassisNumberID ,
		CustomerID ,
		Name1 ,
		FakturDate ,
		OpenFakturDate ,
		FakturNumber ,
		AreaViolationFlag ,
		AreaViolationPaymentMethodID ,
		AreaViolationyAmount ,
		AreaViolationBankName ,
		AreaViolationGyroNumber ,
		PenaltyFlag ,
		PenaltyPaymentMethodID ,
		PenaltyAmount ,
		PenaltyBankName ,
		PenaltyGyroNumber ,
		ReferenceLetterFlag ,
		ReferenceLetter ,
		SaveBy ,
		SaveTime ,
		ValidateBy ,
		ValidateTime ,
		ConfirmBy ,
		ConfirmTime ,
		DownloadBy ,
		DownloadTime ,
		PrintedBy ,
		PrintedTime ,
		CleansingCustomerID ,
		MCPHeaderID ,
		MCPStatus ,
		LKPPHeaderID ,
		LKPPStatus ,
		Remark1 ,
		Remark2 ,
		HandoverDate ,
		RowStatus ,
		CreatedBy ,
		CreatedTime ,
		LastUpdateBy ,
		LastUpdateTime
FROM	tmp__EndCustomer_26
GO

SET IDENTITY_INSERT EndCustomer OFF
GO

DROP TABLE tmp__EndCustomer_26
GO

ALTER TABLE SPKFaktur ADD
CONSTRAINT [FK_SPKFaktur[one]]_EndCustomer[one]]] FOREIGN KEY(EndCustomerID) REFERENCES EndCustomer(ID)
GO

ALTER TABLE RevisionSPKFaktur ADD
CONSTRAINT [FK_RevisionSPKFaktur[one]]_EndCustomer[one]]] FOREIGN KEY(EndCustomerID) REFERENCES EndCustomer(ID)
GO

ALTER TABLE ChassisMaster ADD
CONSTRAINT [FK_ChassisMaster[one]]_EndCustomer[one]]] FOREIGN KEY(EndCustomerID) REFERENCES EndCustomer(ID)
GO

ALTER TABLE RevisionFaktur ADD
CONSTRAINT [FK_RevisionFaktur[one]]_OldEndCustomer[one]]] FOREIGN KEY(OldEndCustomerID) REFERENCES EndCustomer(ID),
CONSTRAINT [FK_RevisionFaktur[one]]_EndCustomer[one]]] FOREIGN KEY(EndCustomerID) REFERENCES EndCustomer(ID)
GO

ALTER TABLE RevisionChassisMasterProfile ADD
CONSTRAINT [FK_RevisionChassisMasterProfile[one]]_EndCustomer[one]]] FOREIGN KEY(EndCustomerID) REFERENCES EndCustomer(ID)
GO

COMMIT
GO



PRINT 'EXEC FReeService'
GO
/*FS*/
SET XACT_ABORT ON
GO

BEGIN TRANSACTION
GO

ALTER TABLE FreeService DROP
CONSTRAINT [FK_FreeService[many]]_FleetRequest[one]]] ,
CONSTRAINT [FK_FreeService_ChassisMaster[one]]] ,
CONSTRAINT [FK_FreeService_Dealer[one]]] ,
CONSTRAINT [FK_FreeService_FSKind[one]]] ,
CONSTRAINT [FK_FreeService_Reason[one]]] 
GO

EXEC sp_rename
	'dbo.PK_FreeService' ,
	'tmp__PK_FreeService' ,
	'OBJECT'
GO

EXEC sp_rename
	'dbo.FreeService' ,
	'tmp__FreeService_27' ,
	'OBJECT'
GO

CREATE TABLE FreeService
	   (
		 ID INT NOT NULL
				IDENTITY
				CONSTRAINT PK_FreeService PRIMARY KEY ,
		 Status VARCHAR(1) ,
		 ChassisMasterID INT ,
		 FSKindID TINYINT ,
		 MileAge INT ,
		 ServiceDate DATETIME ,
		 ServiceDealerID SMALLINT ,
		 DealerBranchID INT ,
		 SoldDate SMALLDATETIME ,
		 NotificationNumber VARCHAR(20) ,
		 NotificationType VARCHAR(2) ,
		 TotalAmount MONEY ,
		 LabourAmount MONEY ,
		 PartAmount MONEY ,
		 PPNAmount MONEY ,
		 PPHAmount MONEY ,
		 Reject VARCHAR(4) ,
		 Reason SMALLINT ,
		 ReleaseBy VARCHAR(20) ,
		 ReleaseDate DATETIME ,
		 VisitType VARCHAR(20) ,
		 FleetRequestID INT ,
		 WorkOrderNumber VARCHAR(50) ,
		 RowStatus SMALLINT ,
		 CreatedBy VARCHAR(20) ,
		 CreatedTime DATETIME ,
		 LastUpdateBy VARCHAR(20) ,
		 LastUpdateTime DATETIME
	   )
GO

ALTER TABLE FreeService ADD
CONSTRAINT [FK_FreeService[many]]_FleetRequest[one]]] FOREIGN KEY(FleetRequestID) REFERENCES FleetRequest(ID),
CONSTRAINT [FK_FreeService_ChassisMaster[one]]] FOREIGN KEY(ChassisMasterID) REFERENCES ChassisMaster(ID),
CONSTRAINT [FK_FreeService_Dealer[one]]] FOREIGN KEY(ServiceDealerID) REFERENCES Dealer(ID),
CONSTRAINT [FK_FreeService_DealerBranch[one]]] FOREIGN KEY(DealerBranchID) REFERENCES DealerBranch(ID),
CONSTRAINT [FK_FreeService_FSKind[one]]] FOREIGN KEY(FSKindID) REFERENCES FSKind(ID),
CONSTRAINT [FK_FreeService_Reason[one]]] FOREIGN KEY(Reason) REFERENCES Reason(ID)
GO

CREATE INDEX IX_FreeService_ChassisMasterID ON FreeService(ChassisMasterID)
GO

CREATE INDEX IX_FreeService_Status ON FreeService(Status)
GO

CREATE INDEX IX_FreeService_ServiceDealerID ON FreeService(ServiceDealerID)
GO

CREATE INDEX IX_FreeService_ReleaseDate ON FreeService(ReleaseDate)
GO

SET IDENTITY_INSERT FreeService ON
GO

INSERT	INTO FreeService
		(
		  ID ,
		  Status ,
		  ChassisMasterID ,
		  FSKindID ,
		  MileAge ,
		  ServiceDate ,
		  ServiceDealerID ,
		  SoldDate ,
		  NotificationNumber ,
		  NotificationType ,
		  TotalAmount ,
		  LabourAmount ,
		  PartAmount ,
		  PPNAmount ,
		  PPHAmount ,
		  Reject ,
		  Reason ,
		  ReleaseBy ,
		  ReleaseDate ,
		  VisitType ,
		  FleetRequestID ,
		  RowStatus ,
		  CreatedBy ,
		  CreatedTime ,
		  LastUpdateBy ,
		  LastUpdateTime
		)
SELECT	ID ,
		Status ,
		ChassisMasterID ,
		FSKindID ,
		MileAge ,
		CONVERT(DATETIME, ServiceDate) ,
		ServiceDealerID ,
		SoldDate ,
		NotificationNumber ,
		NotificationType ,
		TotalAmount ,
		LabourAmount ,
		PartAmount ,
		PPNAmount ,
		PPHAmount ,
		Reject ,
		Reason ,
		ReleaseBy ,
		ReleaseDate ,
		VisitType ,
		FleetRequestID ,
		RowStatus ,
		CreatedBy ,
		CreatedTime ,
		LastUpdateBy ,
		LastUpdateTime
FROM	tmp__FreeService_27
GO

SET IDENTITY_INSERT FreeService OFF
GO

DROP TABLE tmp__FreeService_27
GO

COMMIT
GO



/*FreeServiceBB*/
PRINT 'FreeServiceBB'
GO
SET XACT_ABORT ON
GO

BEGIN TRANSACTION
GO

ALTER TABLE FreeServiceBB DROP
CONSTRAINT [FK_FreeServiceBB_ChassisMasterBB[one]]] 
GO

EXEC sp_rename
	'dbo.PK_FreeServiceBB' ,
	'tmp__PK_FreeServiceBB' ,
	'OBJECT'
GO

EXEC sp_rename
	'dbo.FreeServiceBB' ,
	'tmp__FreeServiceBB_28' ,
	'OBJECT'
GO

CREATE TABLE FreeServiceBB
	   (
		 ID INT NOT NULL
				IDENTITY
				CONSTRAINT PK_FreeServiceBB PRIMARY KEY ,
		 Status VARCHAR(1) ,
		 ChassisMasterID INT ,
		 FSKindID TINYINT ,
		 MileAge INT ,
		 ServiceDate DATETIME ,
		 ServiceDealerID SMALLINT ,
		 DealerBranchID INT ,
		 SoldDate DATETIME ,
		 NotificationNumber VARCHAR(20) ,
		 NotificationType VARCHAR(2) ,
		 TotalAmount MONEY ,
		 LabourAmount MONEY ,
		 PartAmount MONEY ,
		 PPNAmount MONEY ,
		 PPHAmount MONEY ,
		 Reject VARCHAR(4) ,
		 Reason SMALLINT ,
		 ReleaseBy VARCHAR(20) ,
		 ReleaseDate DATETIME ,
		 VisitType VARCHAR(20) ,
		 WorkOrderNumber VARCHAR(50) ,
		 RowStatus SMALLINT ,
		 CreatedBy VARCHAR(20) ,
		 CreatedTime DATETIME ,
		 LastUpdateBy VARCHAR(20) ,
		 LastUpdateTime DATETIME
	   )
GO

ALTER TABLE FreeServiceBB ADD
CONSTRAINT [FK_FreeServiceBB_ChassisMasterBB[one]]] FOREIGN KEY(ChassisMasterID) REFERENCES ChassisMasterBB(ID)
GO

SET IDENTITY_INSERT FreeServiceBB ON
GO

INSERT	INTO FreeServiceBB
		(
		  ID ,
		  Status ,
		  ChassisMasterID ,
		  FSKindID ,
		  MileAge ,
		  ServiceDate ,
		  ServiceDealerID ,
		  SoldDate ,
		  NotificationNumber ,
		  NotificationType ,
		  TotalAmount ,
		  LabourAmount ,
		  PartAmount ,
		  PPNAmount ,
		  PPHAmount ,
		  Reject ,
		  Reason ,
		  ReleaseBy ,
		  ReleaseDate ,
		  VisitType ,
		  RowStatus ,
		  CreatedBy ,
		  CreatedTime ,
		  LastUpdateBy ,
		  LastUpdateTime
		)
SELECT	ID ,
		Status ,
		ChassisMasterID ,
		FSKindID ,
		MileAge ,
		ServiceDate ,
		ServiceDealerID ,
		SoldDate ,
		NotificationNumber ,
		NotificationType ,
		TotalAmount ,
		LabourAmount ,
		PartAmount ,
		PPNAmount ,
		PPHAmount ,
		Reject ,
		Reason ,
		ReleaseBy ,
		ReleaseDate ,
		VisitType ,
		RowStatus ,
		CreatedBy ,
		CreatedTime ,
		LastUpdateBy ,
		LastUpdateTime
FROM	tmp__FreeServiceBB_28
GO

SET IDENTITY_INSERT FreeServiceBB OFF
GO

DROP TABLE tmp__FreeServiceBB_28
GO

COMMIT
GO




/*IndenPartDetail*/

PRINT '/*IndenPartDetail*/'
GO
SET XACT_ABORT ON
GO

BEGIN TRANSACTION
GO

ALTER TABLE IndentPartPO DROP
CONSTRAINT [FK_IndentPartPO[many]]_IndentPartDetail[one]]] 
GO

ALTER TABLE IndentPartDetail DROP
CONSTRAINT [FK_IndentPartDetail[many]]_IndentPartHeader[one]]] ,
CONSTRAINT [FK_IndentPartDetail[many]]_SparePartMaster[one]]] 
GO

EXEC sp_rename
	'dbo.IndentPartDetail' ,
	'tmp__IndentPartDetail_29' ,
	'OBJECT'
GO

CREATE TABLE IndentPartDetail
	   (
		 ID INT NOT NULL
				IDENTITY
				CONSTRAINT PK_IndentPartDetail PRIMARY KEY ,
		 IndentPartHeaderID INT ,
		 SparePartMasterID INT ,
		 Qty INT ,
		 Description VARCHAR(255) ,
		 AllocationQty INT ,
		 IsCompletedAllocation TINYINT ,
		 Price MONEY ,
		 TotalForecast INT ,
		 RowStatus SMALLINT ,
		 CreatedBy VARCHAR(20) ,
		 CreatedTime DATETIME ,
		 LastUpdateBy VARCHAR(20) ,
		 LastUpdateTime DATETIME
	   )
GO

ALTER TABLE IndentPartDetail ADD
CONSTRAINT [FK_IndentPartDetail[many]]_IndentPartHeader[one]]] FOREIGN KEY(IndentPartHeaderID) REFERENCES IndentPartHeader(ID),
CONSTRAINT [FK_IndentPartDetail[many]]_SparePartMaster[one]]] FOREIGN KEY(SparePartMasterID) REFERENCES SparePartMaster(ID)
GO

CREATE INDEX IX_IndentPartDetail_IndentPartHeaderID ON IndentPartDetail(IndentPartHeaderID)
GO

SET IDENTITY_INSERT IndentPartDetail ON
GO

INSERT	INTO IndentPartDetail
		(
		  ID ,
		  IndentPartHeaderID ,
		  SparePartMasterID ,
		  Qty ,
		  Description ,
		  AllocationQty ,
		  IsCompletedAllocation ,
		  Price ,
		  RowStatus ,
		  CreatedBy ,
		  CreatedTime ,
		  LastUpdateBy ,
		  LastUpdateTime
		)
SELECT	ID ,
		IndentPartHeaderID ,
		SparePartMasterID ,
		Qty ,
		Description ,
		AllocationQty ,
		IsCompletedAllocation ,
		Price ,
		RowStatus ,
		CreatedBy ,
		CreatedTime ,
		LastUpdateBy ,
		LastUpdateTime
FROM	tmp__IndentPartDetail_29
GO

SET IDENTITY_INSERT IndentPartDetail OFF
GO

DROP TABLE tmp__IndentPartDetail_29
GO

ALTER TABLE IndentPartPO ADD
CONSTRAINT [FK_IndentPartPO[many]]_IndentPartDetail[one]]] FOREIGN KEY(IndentPartDetailID) REFERENCES IndentPartDetail(ID)
GO

COMMIT
GO



PRINT '/*IndentPartHeader*/'
/*IndentPartHeader*/
GO
SET XACT_ABORT ON
GO

BEGIN TRANSACTION
GO

ALTER TABLE IndentPartDetail DROP
CONSTRAINT [FK_IndentPartDetail[many]]_IndentPartHeader[one]]] 
GO

ALTER TABLE DepositBPencairanHeader DROP
CONSTRAINT [FK_DepositBPencairanHeader[many]]_IndentPartHeader[one]]] 
GO

ALTER TABLE IndentPartHeader DROP
CONSTRAINT [FK_IndentPartHeader[many]]_Dealer[one]]] 
GO

EXEC sp_rename
	'dbo.IndentPartHeader' ,
	'tmp__IndentPartHeader_30' ,
	'OBJECT'
GO

CREATE TABLE IndentPartHeader
	   (
		 ID INT NOT NULL
				IDENTITY
				CONSTRAINT PK_IndentPartHeader PRIMARY KEY ,
		 DealerID SMALLINT ,
		 RequestNo VARCHAR(13) ,
		 RequestDate DATETIME ,
		 MaterialType INT ,
		 TermOfPaymentID INT ,
		 TOPBlockStatusID INT ,
		 Status TINYINT ,
		 StatusKTB TINYINT ,
		 SubmitFile VARCHAR(50) ,
		 PaymentType TINYINT ,
		 Price MONEY ,
		 KTBConfirmedDate DATETIME ,
		 DescID TINYINT ,
		 ChassisNumber VARCHAR(20) ,
		 DMSPRNo VARCHAR(50) ,
		 RowStatus SMALLINT ,
		 CreatedBy VARCHAR(20) ,
		 CreatedTime DATETIME ,
		 LastUpdateBy VARCHAR(20) ,
		 LastUpdateTime DATETIME
	   )
GO

ALTER TABLE IndentPartHeader ADD
CONSTRAINT [FK_IndentPartHeader[many]]_Dealer[one]]] FOREIGN KEY(DealerID) REFERENCES Dealer(ID),
CONSTRAINT [FK_IndentPartHeader[one]]_TOPBlockStatus[one]]] FOREIGN KEY(TOPBlockStatusID) REFERENCES TOPBlockStatus(ID),
CONSTRAINT [FK_TermOfPayment[one]]_IndentPartHeader[one]]] FOREIGN KEY(TermOfPaymentID) REFERENCES TermOfPayment(ID)
GO

SET IDENTITY_INSERT IndentPartHeader ON
GO

INSERT	INTO IndentPartHeader
		(
		  ID ,
		  DealerID ,
		  RequestNo ,
		  RequestDate ,
		  MaterialType ,
		  Status ,
		  StatusKTB ,
		  SubmitFile ,
		  PaymentType ,
		  Price ,
		  KTBConfirmedDate ,
		  DescID ,
		  ChassisNumber ,
		  RowStatus ,
		  CreatedBy ,
		  CreatedTime ,
		  LastUpdateBy ,
		  LastUpdateTime
		)
SELECT	ID ,
		DealerID ,
		RequestNo ,
		RequestDate ,
		MaterialType ,
		Status ,
		StatusKTB ,
		SubmitFile ,
		PaymentType ,
		Price ,
		KTBConfirmedDate ,
		DescID ,
		ChassisNumber ,
		RowStatus ,
		CreatedBy ,
		CreatedTime ,
		LastUpdateBy ,
		LastUpdateTime
FROM	tmp__IndentPartHeader_30
GO

SET IDENTITY_INSERT IndentPartHeader OFF
GO

DROP TABLE tmp__IndentPartHeader_30
GO

ALTER TABLE DepositBPencairanHeader ADD
CONSTRAINT [FK_DepositBPencairanHeader[many]]_IndentPartHeader[one]]] FOREIGN KEY(IndentPartEqHeaderID) REFERENCES IndentPartHeader(ID)
GO

ALTER TABLE IndentPartDetail ADD
CONSTRAINT [FK_IndentPartDetail[many]]_IndentPartHeader[one]]] FOREIGN KEY(IndentPartHeaderID) REFERENCES IndentPartHeader(ID)
GO

COMMIT
GO



PRINT '/*IndentPartPO*/'
GO
SET XACT_ABORT ON
GO

BEGIN TRANSACTION
GO

IF NOT EXISTS ( SELECT	''
				FROM	sys.[indexes] a
				WHERE	a.[name] = 'IX_IndentPartPO_IndentPartDetailID' )
   BEGIN
		 CREATE INDEX IX_IndentPartPO_IndentPartDetailID ON IndentPartPO(SparePartPODetailID,IndentPartDetailID)

   END
 
 
IF NOT EXISTS ( SELECT	''
				FROM	sys.[indexes] a
				WHERE	a.[name] = 'IX_IndentPartPO_SparePartPODetailID_RowStatus' )
   BEGIN
   
		 CREATE INDEX IX_IndentPartPO_SparePartPODetailID_RowStatus ON IndentPartPO(ID,IndentPartDetailID,CreatedBy,CreatedTime,LastUpdateBy,LastUpdateTime,SparePartPODetailID,RowStatus)
 

   END

IF NOT EXISTS ( SELECT	''
				FROM	sys.[indexes] a
				WHERE	a.[name] = 'IX_IndentPartPO_IndentPartDetailID_RowStatus' )
   BEGIN
		 CREATE INDEX IX_IndentPartPO_IndentPartDetailID_RowStatus ON IndentPartPO(IndentPartDetailID,RowStatus)
   END

GO

COMMIT
GO

IF NOT EXISTS ( SELECT	''
				FROM	sys.[indexes] a
				WHERE	a.[name] = 'IX_AppId' )
   BEGIN
		 CREATE INDEX IX_AppId ON MsApplicationPermission(AppID)
   END

IF NOT EXISTS ( SELECT	''
				FROM	sys.[indexes] a
				WHERE	a.[name] = 'IX_PermissionId' )
   BEGIN
		 CREATE INDEX IX_PermissionId ON MsApplicationPermission(PermissionId)
   END


GO


PRINT '/*PartShop*/'

GO
SET XACT_ABORT ON
GO

BEGIN TRANSACTION
GO

ALTER TABLE SalesmanPartShop DROP
CONSTRAINT [FK_SalesmanPartShop[many]]_PartShop[one]]] 
GO

ALTER TABLE PartShop DROP
CONSTRAINT [FK_PartShop[many]]_CityPart[one]]] ,
CONSTRAINT [FK_PartShop[many]]_Dealer[one]]] 
GO

EXEC sp_rename
	'dbo.PK_PartShop' ,
	'tmp__PK_PartShop' ,
	'OBJECT'
GO

EXEC sp_rename
	'dbo.PartShop' ,
	'tmp__PartShop_31' ,
	'OBJECT'
GO

CREATE TABLE PartShop
	   (
		 ID INT NOT NULL
				IDENTITY
				CONSTRAINT PK_PartShop PRIMARY KEY ,
		 DealerID SMALLINT ,
		 CityPartID INT ,
		 CityID INT ,
		 PartShopCode VARCHAR(10) ,
		 OldPartShopCode VARCHAR(10) ,
		 [Name] VARCHAR(50) ,
		 Address VARCHAR(100) ,
		 Phone VARCHAR(40) ,
		 Fax VARCHAR(40) ,
		 Email VARCHAR(50) ,
		 Status TINYINT ,
		 RowStatus SMALLINT ,
		 CreatedBy VARCHAR(20) ,
		 CreatedTime DATETIME ,
		 LastUpdateBy VARCHAR(20) ,
		 LastUpdateTime DATETIME
	   )
GO

ALTER TABLE PartShop ADD
CONSTRAINT [FK_PartShop[many]]_CityPart[one]]] FOREIGN KEY(CityPartID) REFERENCES CityPart(ID),
CONSTRAINT [FK_PartShop[many]]_Dealer[one]]] FOREIGN KEY(DealerID) REFERENCES Dealer(ID)
GO

SET IDENTITY_INSERT PartShop ON
GO

INSERT	INTO PartShop
		(
		  ID ,
		  DealerID ,
		  CityPartID ,
		  PartShopCode ,
		  [Name] ,
		  Address ,
		  Phone ,
		  Fax ,
		  Email ,
		  Status ,
		  RowStatus ,
		  CreatedBy ,
		  CreatedTime ,
		  LastUpdateBy ,
		  LastUpdateTime
		)
SELECT	ID ,
		DealerID ,
		CityPartID ,
		PartShopCode ,
		[Name] ,
		Address ,
		Phone ,
		Fax ,
		Email ,
		Status ,
		RowStatus ,
		CreatedBy ,
		CreatedTime ,
		LastUpdateBy ,
		LastUpdateTime
FROM	tmp__PartShop_31
GO

SET IDENTITY_INSERT PartShop OFF
GO

DROP TABLE tmp__PartShop_31
GO

ALTER TABLE SalesmanPartShop ADD
CONSTRAINT [FK_SalesmanPartShop[many]]_PartShop[one]]] FOREIGN KEY(PartshopID) REFERENCES PartShop(ID)
GO

COMMIT
GO




PRINT '/*PDI*/'
GO

SET XACT_ABORT ON
GO

BEGIN TRANSACTION
GO

ALTER TABLE PDI DROP
CONSTRAINT [FK_PDI_ChassisMaster[one]]] ,
CONSTRAINT [FK_PDI_Dealer[one]]] 
GO

EXEC sp_rename
	'dbo.PK_PDI' ,
	'tmp__PK_PDI' ,
	'OBJECT'
GO

EXEC sp_rename
	'dbo.PDI' ,
	'tmp__PDI_32' ,
	'OBJECT'
GO

CREATE TABLE PDI
	   (
		 ID INT NOT NULL
				IDENTITY
				CONSTRAINT PK_PDI PRIMARY KEY ,
		 ChassisMasterID INT ,
		 DealerID SMALLINT ,
		 DealerBranchID INT ,
		 Kind CHAR(1) ,
		 PDIStatus CHAR(1) ,
		 PDIDate DATETIME ,
		 ReleaseBy VARCHAR(20) ,
		 ReleaseDate DATETIME ,
		 WorkOrderNumber VARCHAR(50) ,
		 RowStatus SMALLINT ,
		 CreatedBy VARCHAR(20) ,
		 CreatedTime DATETIME ,
		 LastUpdateBy VARCHAR(20) ,
		 LastUpdateTime DATETIME
	   )
GO

ALTER TABLE PDI ADD
CONSTRAINT [FK_PDI_ChassisMaster[one]]] FOREIGN KEY(ChassisMasterID) REFERENCES ChassisMaster(ID),
CONSTRAINT [FK_PDI_Dealer[one]]] FOREIGN KEY(DealerID) REFERENCES Dealer(ID),
CONSTRAINT [FK_PDI_DealerBranch[one]]] FOREIGN KEY(DealerBranchID) REFERENCES DealerBranch(ID)
GO

CREATE INDEX IX_PDI_ReleaseDate ON PDI(ReleaseDate)
GO

CREATE INDEX IX_PDI_DealerID ON PDI(DealerID)
GO

CREATE INDEX IX_PDI_ChassisMasterID ON PDI(ChassisMasterID)
GO

SET IDENTITY_INSERT PDI ON
GO

INSERT	INTO PDI
		(
		  ID ,
		  ChassisMasterID ,
		  DealerID ,
		  Kind ,
		  PDIStatus ,
		  PDIDate ,
		  ReleaseBy ,
		  ReleaseDate ,
		  RowStatus ,
		  CreatedBy ,
		  CreatedTime ,
		  LastUpdateBy ,
		  LastUpdateTime
		)
SELECT	ID ,
		ChassisMasterID ,
		DealerID ,
		Kind ,
		PDIStatus ,
		PDIDate ,
		ReleaseBy ,
		ReleaseDate ,
		RowStatus ,
		CreatedBy ,
		CreatedTime ,
		LastUpdateBy ,
		LastUpdateTime
FROM	tmp__PDI_32
GO

SET IDENTITY_INSERT PDI OFF
GO

DROP TABLE tmp__PDI_32
GO

COMMIT
GO





PRINT '/*PMHEADER*/'

GO
SET XACT_ABORT ON
GO

BEGIN TRANSACTION
GO

ALTER TABLE PMDetail DROP
CONSTRAINT [FK_PMDetail[many]]_PMHeader[one]]] 
GO

ALTER TABLE PMHeader DROP
CONSTRAINT [FK_PMHeader[many]]_ChassisMaster[one]]] ,
CONSTRAINT [FK_PMHeader[one]]_Dealer[one]]] 
GO

EXEC sp_rename
	'dbo.PK_PMHeader' ,
	'tmp__PK_PMHeader' ,
	'OBJECT'
GO

EXEC sp_rename
	'dbo.PMHeader' ,
	'tmp__PMHeader_33' ,
	'OBJECT'
GO

CREATE TABLE PMHeader
	   (
		 ID INT NOT NULL
				IDENTITY
				CONSTRAINT PK_PMHeader PRIMARY KEY ,
		 DealerID SMALLINT ,
		 DealerBranchID INT ,
		 ChassisNumberID INT ,
		 PMKindID INT ,
		 StandKM INT ,
		 ServiceDate DATETIME ,
		 ReleaseDate DATETIME ,
		 PMStatus VARCHAR(4) ,
		 EntryType VARCHAR(20) ,
		 WorkOrderNumber VARCHAR(50) ,
		 BookingNo VARCHAR(50) ,
		 VisitType VARCHAR(5) ,
		 Remarks VARCHAR(250) ,
		 RowStatus SMALLINT ,
		 CreatedBy VARCHAR(20) ,
		 CreatedTime DATETIME ,
		 LastUpdateBy VARCHAR(20) ,
		 LastUpdateTime DATETIME
	   )
GO

ALTER TABLE PMHeader ADD
CONSTRAINT [FK_PMHeader[many]]_ChassisMaster[one]]] FOREIGN KEY(ChassisNumberID) REFERENCES ChassisMaster(ID),
CONSTRAINT [FK_PMHeader[many]]_DealerBranch[one]]] FOREIGN KEY(DealerBranchID) REFERENCES DealerBranch(ID),
CONSTRAINT [FK_PMHeader[many]]_PMKind[one]]] FOREIGN KEY(PMKindID) REFERENCES PMKind(ID),
CONSTRAINT [FK_PMHeader[one]]_Dealer[one]]] FOREIGN KEY(DealerID) REFERENCES Dealer(ID)
GO

CREATE INDEX IX_PMHeader_ChassisMasterID ON PMHeader(ChassisNumberID)
GO

SET IDENTITY_INSERT PMHeader ON
GO

INSERT	INTO PMHeader
		(
		  ID ,
		  DealerID ,
		  ChassisNumberID ,
		  PMKindID ,
		  StandKM ,
		  ServiceDate ,
		  ReleaseDate ,
		  PMStatus ,
		  EntryType ,
		  VisitType ,
		  Remarks ,
		  RowStatus ,
		  CreatedBy ,
		  CreatedTime ,
		  LastUpdateBy ,
		  LastUpdateTime
		)
SELECT	ID ,
		DealerID ,
		ChassisNumberID ,
		PMKindID ,
		StandKM ,
		ServiceDate ,
		ReleaseDate ,
		PMStatus ,
		EntryType ,
		VisitType ,
		Remarks ,
		RowStatus ,
		CONVERT(VARCHAR(20), CreatedBy) ,
		CreatedTime ,
		CONVERT(VARCHAR(20), LastUpdateBy) ,
		LastUpdateTime
FROM	tmp__PMHeader_33
GO

SET IDENTITY_INSERT PMHeader OFF
GO

DROP TABLE tmp__PMHeader_33
GO

ALTER TABLE PMDetail ADD
CONSTRAINT [FK_PMDetail[many]]_PMHeader[one]]] FOREIGN KEY(PMID) REFERENCES PMHeader(ID)
GO

COMMIT
GO




PRINT '/*PQRHEADER*/'

GO
SET XACT_ABORT ON
GO

BEGIN TRANSACTION
GO

ALTER TABLE PQRProfile DROP
CONSTRAINT [FK_PQRProfile[many]]_PQRHeader[one]]] 
GO

ALTER TABLE PQRAdditionalInfo DROP
CONSTRAINT [FK_PQRAdditionalInfo[many]]_PQRHeader[one]]] 
GO

ALTER TABLE PQRAttachment DROP
CONSTRAINT [FK_PQRAttachment[many]]_PQRHeader[one]]] 
GO

ALTER TABLE PQRChangesHistory DROP
CONSTRAINT [FK_PQRChangesHistory[many]]_PQRHeader[one]]] 
GO

ALTER TABLE PQRDamageCode DROP
CONSTRAINT [FK_PQRDamageCode[many]]_PQRHeader[one]]] 
GO

ALTER TABLE PQRDetail DROP
CONSTRAINT [FK_PQRDetail[many]]_PQRHeader[one]]] 
GO

ALTER TABLE PQRPartsCode DROP
CONSTRAINT [FK_PQRPartsCode[many]]_PQRHeader[one]]] 
GO

ALTER TABLE PQRQRS DROP
CONSTRAINT [FK_PQRQRS[many]]_PQRHeader[one]]] 
GO

ALTER TABLE PQRSolutionReferences DROP
CONSTRAINT [FK_PQRSolutionReferences[many]]_PQRHeader[one]]] 
GO

ALTER TABLE PQRHeader DROP
CONSTRAINT DF_PQRHeader_RowStatus ,
CONSTRAINT [FK_PQRHeader[main]]_ChassisMaster[one]]] ,
CONSTRAINT [FK_PQRHeader[many]]_Category[one]]] ,
CONSTRAINT [FK_PQRHeader[many]]_Dealer[one]]] 
GO

EXEC sp_rename
	'dbo.PK__PQRHeade__3214EC270DB3D0BA' ,
	'tmp__PK__PQRHeade__3214EC270DB3D0BA' ,
	'OBJECT'
GO

EXEC sp_rename
	'dbo.PQRHeader' ,
	'tmp__PQRHeader_34' ,
	'OBJECT'
GO

CREATE TABLE PQRHeader
	   (
		 ID INT NOT NULL
				IDENTITY
				CONSTRAINT PK_PQRHeader PRIMARY KEY ,
		 PQRNo VARCHAR(25) ,
		 PQRType INT ,
		 RefPQRNo VARCHAR(25) ,
		 DealerID SMALLINT ,
		 DealerBranchID INT ,
		 Year INT ,
		 SeqNo INT ,
		 CategoryID TINYINT ,
		 DocumentDate DATETIME ,
		 SoldDate DATETIME ,
		 ChassisMasterID INT ,
		 PQRDate DATETIME ,
		 OdoMeter INT ,
		 Velocity INT ,
		 CustomerName VARCHAR(40) ,
		 CustomerAddress VARCHAR(100) ,
		 ValidationTime DATETIME ,
		 ConfirmBy VARCHAR(20) ,
		 ConfirmTime DATETIME ,
		 RealeseTime DATETIME ,
		 IntervalProcess DATETIME ,
		 Complexity SMALLINT ,
		 Subject VARCHAR(50) ,
		 Symptomps VARCHAR(1000) ,
		 Causes VARCHAR(1000) ,
		 Results VARCHAR(1000) ,
		 Notes VARCHAR(1000) ,
		 Solutions VARCHAR(1000) ,
		 Bobot INT ,
		 ReleaseBy VARCHAR(50) ,
		 FinishBy VARCHAR(50) ,
		 FinishDate DATETIME ,
		 CodeA VARCHAR(4) ,
		 CodeB VARCHAR(4) ,
		 CodeC VARCHAR(4) ,
		 WorkOrderNumber VARCHAR(50) ,
		 RowStatus SMALLINT CONSTRAINT DF_PQRHeader_RowStatus DEFAULT ( 0 ) ,
		 CreatedBy VARCHAR(20) ,
		 CreatedTime DATETIME ,
		 LastUpdateBy VARCHAR(20) ,
		 LastUpdateTime DATETIME
	   )
GO

ALTER TABLE PQRHeader ADD
CONSTRAINT [FK_PQRHeader[main]]_ChassisMaster[one]]] FOREIGN KEY(ChassisMasterID) REFERENCES ChassisMaster(ID),
CONSTRAINT [FK_PQRHeader[many]]_Category[one]]] FOREIGN KEY(CategoryID) REFERENCES Category(ID),
CONSTRAINT [FK_PQRHeader[many]]_Dealer[one]]] FOREIGN KEY(DealerID) REFERENCES Dealer(ID)
GO

CREATE INDEX IX_PQRHeader_ChassisMasterID ON PQRHeader(ChassisMasterID)
GO

SET IDENTITY_INSERT PQRHeader ON
GO

INSERT	INTO PQRHeader
		(
		  ID ,
		  PQRNo ,
		  PQRType ,
		  RefPQRNo ,
		  DealerID ,
		  Year ,
		  SeqNo ,
		  CategoryID ,
		  DocumentDate ,
		  SoldDate ,
		  ChassisMasterID ,
		  PQRDate ,
		  OdoMeter ,
		  Velocity ,
		  CustomerName ,
		  CustomerAddress ,
		  ValidationTime ,
		  ConfirmBy ,
		  ConfirmTime ,
		  RealeseTime ,
		  IntervalProcess ,
		  Complexity ,
		  Subject ,
		  Symptomps ,
		  Causes ,
		  Results ,
		  Notes ,
		  Solutions ,
		  Bobot ,
		  ReleaseBy ,
		  FinishBy ,
		  FinishDate ,
		  CodeA ,
		  CodeB ,
		  CodeC ,
		  RowStatus ,
		  CreatedBy ,
		  CreatedTime ,
		  LastUpdateBy ,
		  LastUpdateTime
		)
SELECT	ID ,
		PQRNo ,
		PQRType ,
		RefPQRNo ,
		DealerID ,
		Year ,
		SeqNo ,
		CategoryID ,
		DocumentDate ,
		SoldDate ,
		ChassisMasterID ,
		PQRDate ,
		OdoMeter ,
		Velocity ,
		CustomerName ,
		CustomerAddress ,
		ValidationTime ,
		ConfirmBy ,
		ConfirmTime ,
		RealeseTime ,
		IntervalProcess ,
		Complexity ,
		Subject ,
		Symptomps ,
		Causes ,
		Results ,
		Notes ,
		Solutions ,
		Bobot ,
		ReleaseBy ,
		FinishBy ,
		FinishDate ,
		CodeA ,
		CodeB ,
		CodeC ,
		RowStatus ,
		CreatedBy ,
		CreatedTime ,
		LastUpdateBy ,
		LastUpdateTime
FROM	tmp__PQRHeader_34
GO

SET IDENTITY_INSERT PQRHeader OFF
GO

DROP TABLE tmp__PQRHeader_34
GO

ALTER TABLE PQRSolutionReferences ADD
CONSTRAINT [FK_PQRSolutionReferences[many]]_PQRHeader[one]]] FOREIGN KEY(PQRHeaderID) REFERENCES PQRHeader(ID)
GO

ALTER TABLE PQRQRS ADD
CONSTRAINT [FK_PQRQRS[many]]_PQRHeader[one]]] FOREIGN KEY(PQRHeaderID) REFERENCES PQRHeader(ID)
GO

ALTER TABLE PQRPartsCode ADD
CONSTRAINT [FK_PQRPartsCode[many]]_PQRHeader[one]]] FOREIGN KEY(PQRHeaderID) REFERENCES PQRHeader(ID)
GO

ALTER TABLE PQRDetail ADD
CONSTRAINT [FK_PQRDetail[many]]_PQRHeader[one]]] FOREIGN KEY(PQRHeaderID) REFERENCES PQRHeader(ID)
GO

ALTER TABLE PQRDamageCode ADD
CONSTRAINT [FK_PQRDamageCode[many]]_PQRHeader[one]]] FOREIGN KEY(PQRHeaderID) REFERENCES PQRHeader(ID)
GO

ALTER TABLE PQRChangesHistory ADD
CONSTRAINT [FK_PQRChangesHistory[many]]_PQRHeader[one]]] FOREIGN KEY(PQRHeaderID) REFERENCES PQRHeader(ID)
GO

ALTER TABLE PQRAttachment ADD
CONSTRAINT [FK_PQRAttachment[many]]_PQRHeader[one]]] FOREIGN KEY(PQRHeaderID) REFERENCES PQRHeader(ID)
GO

ALTER TABLE PQRAdditionalInfo ADD
CONSTRAINT [FK_PQRAdditionalInfo[many]]_PQRHeader[one]]] FOREIGN KEY(PQRHeaderID) REFERENCES PQRHeader(ID)
GO

ALTER TABLE PQRProfile ADD
CONSTRAINT [FK_PQRProfile[many]]_PQRHeader[one]]] FOREIGN KEY(PQRHeaderID) REFERENCES PQRHeader(ID)
GO

COMMIT
GO




PRINT '/*PQRHEDERBB*/'
GO
SET XACT_ABORT ON
GO

BEGIN TRANSACTION
GO

ALTER TABLE PQRDetailBB DROP
CONSTRAINT [FK_PQRDetailBB[many]]_PQRHeaderBB[one]]] 
GO

ALTER TABLE PQRDamageCodeBB DROP
CONSTRAINT [FK_PQRDamageCodeBB[many]]_PQRHeaderBB[one]]] 
GO

ALTER TABLE PQRChangesHistoryBB DROP
CONSTRAINT [FK_PQRChangesHistoryBB[many]]_PQRHeaderBB[one]]] 
GO

ALTER TABLE PQRAttachmentBB DROP
CONSTRAINT [FK_PQRAttachmentBB[many]]_PQRHeaderBB[one]]] 
GO

ALTER TABLE PQRAdditionalInfoBB DROP
CONSTRAINT [FK_PQRAdditionalInfoBB[many]]_PQRHeaderBB[one]]] 
GO

ALTER TABLE PQRProfileBB DROP
CONSTRAINT [FK_PQRProfileBB[many]]_PQRHeaderBB[one]]] 
GO

ALTER TABLE PQRPartsCodeBB DROP
CONSTRAINT [FK_PQRPartsCodeBB[many]]_PQRHeaderBB[one]]] 
GO

ALTER TABLE PQRQRSBB DROP
CONSTRAINT [FK_PQRQRSBB[many]]_PQRHeaderBB[one]]] 
GO

ALTER TABLE PQRHeaderBB DROP
CONSTRAINT DF_PQRHeaderBB_RowStatus ,
CONSTRAINT [FK_PQRHeaderBB[main]]_ChassisMasterBB[one]]] ,
CONSTRAINT [FK_PQRHeaderBB[many]]_Category[one]]] ,
CONSTRAINT [FK_PQRHeaderBB[many]]_Dealer[one]]] 
GO

EXEC sp_rename
	'dbo.PK__PQRHeade__3214EC271554F282' ,
	'tmp__PK__PQRHeade__3214EC271554F282' ,
	'OBJECT'
GO

EXEC sp_rename
	'dbo.PQRHeaderBB' ,
	'tmp__PQRHeaderBB_35' ,
	'OBJECT'
GO

CREATE TABLE PQRHeaderBB
	   (
		 ID INT NOT NULL
				IDENTITY
				CONSTRAINT PK_PQRHeaderBB PRIMARY KEY ,
		 PQRNo VARCHAR(25) ,
		 PQRType INT ,
		 RefPQRNo VARCHAR(25) ,
		 DealerID SMALLINT ,
		 DealerBranchID INT ,
		 Year INT ,
		 SeqNo INT ,
		 CategoryID TINYINT ,
		 DocumentDate DATETIME ,
		 SoldDate DATETIME ,
		 ChassisMasterBBID INT ,
		 PQRDate DATETIME ,
		 OdoMeter INT ,
		 Velocity INT ,
		 CustomerName VARCHAR(40) ,
		 CustomerAddress VARCHAR(100) ,
		 ValidationTime DATETIME ,
		 ConfirmBy VARCHAR(20) ,
		 ConfirmTime DATETIME ,
		 RealeseTime DATETIME ,
		 IntervalProcess DATETIME ,
		 Complexity SMALLINT ,
		 Subject VARCHAR(50) ,
		 Symptomps VARCHAR(1000) ,
		 Causes VARCHAR(1000) ,
		 Results VARCHAR(1000) ,
		 Notes VARCHAR(1000) ,
		 Solutions VARCHAR(1000) ,
		 Bobot INT ,
		 ReleaseBy VARCHAR(50) ,
		 FinishBy VARCHAR(50) ,
		 FinishDate DATETIME ,
		 CodeA VARCHAR(4) ,
		 CodeB VARCHAR(4) ,
		 CodeC VARCHAR(4) ,
		 WorkOrderNumber VARCHAR(50) ,
		 RowStatus SMALLINT CONSTRAINT DF_PQRHeaderBB_RowStatus DEFAULT ( 0 ) ,
		 CreatedBy VARCHAR(20) ,
		 CreatedTime DATETIME ,
		 LastUpdateBy VARCHAR(20) ,
		 LastUpdateTime DATETIME
	   )
GO

ALTER TABLE PQRHeaderBB ADD
CONSTRAINT [FK_PQRHeaderBB[many]]_Category[one]]] FOREIGN KEY(CategoryID) REFERENCES Category(ID),
CONSTRAINT [FK_PQRHeaderBB[many]]_ChassisMasterBB[one]]] FOREIGN KEY(ChassisMasterBBID) REFERENCES ChassisMasterBB(ID),
CONSTRAINT [FK_PQRHeaderBB[many]]_Dealer[one]]] FOREIGN KEY(DealerID) REFERENCES Dealer(ID)
GO

SET IDENTITY_INSERT PQRHeaderBB ON
GO

INSERT	INTO PQRHeaderBB
		(
		  ID ,
		  PQRNo ,
		  PQRType ,
		  RefPQRNo ,
		  DealerID ,
		  Year ,
		  SeqNo ,
		  CategoryID ,
		  DocumentDate ,
		  SoldDate ,
		  ChassisMasterBBID ,
		  PQRDate ,
		  OdoMeter ,
		  Velocity ,
		  CustomerName ,
		  CustomerAddress ,
		  ValidationTime ,
		  ConfirmBy ,
		  ConfirmTime ,
		  RealeseTime ,
		  IntervalProcess ,
		  Complexity ,
		  Subject ,
		  Symptomps ,
		  Causes ,
		  Results ,
		  Notes ,
		  Solutions ,
		  Bobot ,
		  ReleaseBy ,
		  FinishBy ,
		  FinishDate ,
		  CodeA ,
		  CodeB ,
		  CodeC ,
		  RowStatus ,
		  CreatedBy ,
		  CreatedTime ,
		  LastUpdateBy ,
		  LastUpdateTime
		)
SELECT	ID ,
		PQRNo ,
		PQRType ,
		RefPQRNo ,
		DealerID ,
		Year ,
		SeqNo ,
		CategoryID ,
		DocumentDate ,
		SoldDate ,
		ChassisMasterBBID ,
		PQRDate ,
		OdoMeter ,
		Velocity ,
		CustomerName ,
		CustomerAddress ,
		ValidationTime ,
		ConfirmBy ,
		ConfirmTime ,
		RealeseTime ,
		IntervalProcess ,
		Complexity ,
		Subject ,
		Symptomps ,
		Causes ,
		Results ,
		Notes ,
		Solutions ,
		Bobot ,
		ReleaseBy ,
		FinishBy ,
		FinishDate ,
		CodeA ,
		CodeB ,
		CodeC ,
		RowStatus ,
		CreatedBy ,
		CreatedTime ,
		LastUpdateBy ,
		LastUpdateTime
FROM	tmp__PQRHeaderBB_35
GO

SET IDENTITY_INSERT PQRHeaderBB OFF
GO

DROP TABLE tmp__PQRHeaderBB_35
GO

ALTER TABLE PQRQRSBB ADD
CONSTRAINT [FK_PQRQRSBB[many]]_PQRHeaderBB[one]]] FOREIGN KEY(PQRHeaderBBID) REFERENCES PQRHeaderBB(ID)
GO

ALTER TABLE PQRPartsCodeBB ADD
CONSTRAINT [FK_PQRPartsCodeBB[many]]_PQRHeaderBB[one]]] FOREIGN KEY(PQRHeaderBBID) REFERENCES PQRHeaderBB(ID)
GO

ALTER TABLE PQRProfileBB ADD
CONSTRAINT [FK_PQRProfileBB[many]]_PQRHeaderBB[one]]] FOREIGN KEY(PQRHeaderBBID) REFERENCES PQRHeaderBB(ID)
GO

ALTER TABLE PQRAdditionalInfoBB ADD
CONSTRAINT [FK_PQRAdditionalInfoBB[many]]_PQRHeaderBB[one]]] FOREIGN KEY(PQRHeaderBBID) REFERENCES PQRHeaderBB(ID)
GO

ALTER TABLE PQRAttachmentBB ADD
CONSTRAINT [FK_PQRAttachmentBB[many]]_PQRHeaderBB[one]]] FOREIGN KEY(PQRHeaderBBID) REFERENCES PQRHeaderBB(ID)
GO

ALTER TABLE PQRChangesHistoryBB ADD
CONSTRAINT [FK_PQRChangesHistoryBB[many]]_PQRHeaderBB[one]]] FOREIGN KEY(PQRHeaderBBID) REFERENCES PQRHeaderBB(ID)
GO

ALTER TABLE PQRDamageCodeBB ADD
CONSTRAINT [FK_PQRDamageCodeBB[many]]_PQRHeaderBB[one]]] FOREIGN KEY(PQRHeaderBBID) REFERENCES PQRHeaderBB(ID)
GO

ALTER TABLE PQRDetailBB ADD
CONSTRAINT [FK_PQRDetailBB[many]]_PQRHeaderBB[one]]] FOREIGN KEY(PQRHeaderBBID) REFERENCES PQRHeaderBB(ID)
GO

COMMIT
GO




PRINT '/*RECALLService*/'

GO
SET XACT_ABORT ON
GO

BEGIN TRANSACTION
GO

ALTER TABLE RecallService DROP
CONSTRAINT FK_RecallService_ChassisMaster ,
CONSTRAINT FK_RecallService_Dealer ,
CONSTRAINT FK_RecallService_RecallChassisMaster 
GO

EXEC sp_rename
	'dbo.PK__RecallSe__3214EC272D16A223' ,
	'tmp__PK__RecallSe__3214EC272D16A223' ,
	'OBJECT'
GO

EXEC sp_rename
	'dbo.RecallService' ,
	'tmp__RecallService_36' ,
	'OBJECT'
GO

CREATE TABLE RecallService
	   (
		 ID INT NOT NULL
				IDENTITY
				CONSTRAINT PK_RecallService PRIMARY KEY ,
		 ChassisMasterID INT ,
		 MileAge INT ,
		 ServiceDate DATETIME ,
		 ServiceDealerID SMALLINT ,
		 DealerBranchID INT ,
		 RecallChassisMasterID INT ,
		 WorkOrderNumber VARCHAR(50) ,
		 RowStatus SMALLINT ,
		 CreatedBy VARCHAR(20) ,
		 CreatedTime DATETIME ,
		 LastUpdateBy VARCHAR(20) ,
		 LastUpdateTime DATETIME
	   )
GO

ALTER TABLE RecallService ADD
CONSTRAINT [FK_RecallService[many]]_Dealer[one]]] FOREIGN KEY(ServiceDealerID) REFERENCES Dealer(ID),
CONSTRAINT [FK_RecallService[many]]_DealerBranch[one]]] FOREIGN KEY(DealerBranchID) REFERENCES DealerBranch(ID),
CONSTRAINT [FK_RecallService[one]]_ChassisMaster[one]]] FOREIGN KEY(ChassisMasterID) REFERENCES ChassisMaster(ID),
CONSTRAINT [FK_RecallService[one]]_RecallChassisMaster[one]]] FOREIGN KEY(RecallChassisMasterID) REFERENCES RecallChassisMaster(ID)
GO

SET IDENTITY_INSERT RecallService ON
GO

INSERT	INTO RecallService
		(
		  ID ,
		  ChassisMasterID ,
		  MileAge ,
		  ServiceDate ,
		  ServiceDealerID ,
		  RecallChassisMasterID ,
		  RowStatus ,
		  CreatedBy ,
		  CreatedTime ,
		  LastUpdateBy ,
		  LastUpdateTime
		)
SELECT	ID ,
		ChassisMasterID ,
		MileAge ,
		ServiceDate ,
		ServiceDealerID ,
		RecallChassisMasterID ,
		RowStatus ,
		CreatedBy ,
		CreatedTime ,
		LastUpdateBy ,
		LastUpdateTime
FROM	tmp__RecallService_36
GO

SET IDENTITY_INSERT RecallService OFF
GO

DROP TABLE tmp__RecallService_36
GO

COMMIT
GO




PRINT '/*SparePartMaster*/'

GO

SET XACT_ABORT ON
GO

BEGIN TRANSACTION
GO

ALTER TABLE PartIncidentalDetail DROP
CONSTRAINT [FK_PartIncidentalDetail]]_SparePartMaster[one]]] 
GO

ALTER TABLE SparepartMaxOrder DROP
CONSTRAINT [FK_SparepartMaxOrder_SparePartMaster[one]]] 
GO

ALTER TABLE PQRPartsCode DROP
CONSTRAINT [FK_PQRPartsCode[one]]_SparePartMaster[one]]] 
GO

ALTER TABLE PQRPartsCodeBB DROP
CONSTRAINT [FK_PQRPartsCodeBB[one]]_SparePartMaster[one]]] 
GO

ALTER TABLE SparePartDODetail DROP
CONSTRAINT [FK_SparePartDODetail[many]]_SparePartMaster[one]]] 
GO

ALTER TABLE SparePartPackingDetail DROP
CONSTRAINT [FK_SparePartPackingDetail[many]]_SparePartMaster[one]]] 
GO

ALTER TABLE SparePartMasterTOP DROP
CONSTRAINT [FK_SparePartMasterTOP[many]]_SparePartMaster[one]]] 
GO

ALTER TABLE WSCDetail DROP
CONSTRAINT [FK_WSCDetail[many]]_SparePartMaster[one]]] 
GO

ALTER TABLE SpecialItemDetail DROP
CONSTRAINT [FK_SpecialItemDetail_SparePartMaster[one]]] 
GO

ALTER TABLE WSCDetailBB DROP
CONSTRAINT [FK_WSCDetailBB[many]]_SparePartMaster[one]]] 
GO

ALTER TABLE EstimationEquipDetail DROP
CONSTRAINT [FK_EstimationEquipDetail[many]]_SparePartMaster[one]]] 
GO

ALTER TABLE DepositBKewajibanDetail DROP
CONSTRAINT [FK_DepositBKewajibanDetail[many]]_SparePartMaster[one]]] 
GO

ALTER TABLE IndentPartDetail DROP
CONSTRAINT [FK_IndentPartDetail[many]]_SparePartMaster[one]]] 
GO

EXEC sp_rename
	'dbo.PK_SparePartMaster' ,
	'tmp__PK_SparePartMaster' ,
	'OBJECT'
GO

EXEC sp_rename
	'dbo.SparePartMaster' ,
	'tmp__SparePartMaster_38' ,
	'OBJECT'
GO

CREATE TABLE SparePartMaster
	   (
		 ID INT NOT NULL
				IDENTITY
				CONSTRAINT PK_SparePartMaster PRIMARY KEY ,
		 ProductCategoryID SMALLINT ,
		 PartNumber VARCHAR(18) ,
		 PartName VARCHAR(50) ,
		 PartNumberReff VARCHAR(18) ,
		 UoM VARCHAR(18) ,
		 MaterialCategoryCode VARCHAR(18) ,
		 AltPartNumber VARCHAR(18) ,
		 AltPartName VARCHAR(50) ,
		 PartCode VARCHAR(1) ,
		 ModelCode VARCHAR(9) ,
		 SupplierCode VARCHAR(10) ,
		 TypeCode VARCHAR(5) ,
		 Stock INT ,
		 RetalPrice MONEY ,
		 PartStatus VARCHAR(1) ,
		 ActiveStatus SMALLINT ,
		 AccessoriesType SMALLINT ,
		 ProductType VARCHAR(100) ,
		 RowStatus SMALLINT ,
		 CreatedBy VARCHAR(20) ,
		 CreatedTime DATETIME ,
		 LastUpdateBy VARCHAR(20) ,
		 LastUpdateTime DATETIME
	   )
GO

CREATE INDEX IX_SparePartMaster_PartNumber ON SparePartMaster(PartNumber)
GO

CREATE INDEX IX_SparePartMaster_PartName ON SparePartMaster(PartName)
GO

SET IDENTITY_INSERT SparePartMaster ON
GO

INSERT	INTO SparePartMaster
		(
		  ID ,
		  ProductCategoryID ,
		  PartNumber ,
		  PartName ,
		  AltPartNumber ,
		  AltPartName ,
		  PartCode ,
		  ModelCode ,
		  SupplierCode ,
		  TypeCode ,
		  Stock ,
		  RetalPrice ,
		  PartStatus ,
		  ActiveStatus ,
		  AccessoriesType ,
		  RowStatus ,
		  CreatedBy ,
		  CreatedTime ,
		  LastUpdateBy ,
		  LastUpdateTime
		)
SELECT	ID ,
		ProductCategoryID ,
		PartNumber ,
		CONVERT(VARCHAR(50), PartName) ,
		AltPartNumber ,
		CONVERT(VARCHAR(50), AltPartName) ,
		PartCode ,
		ModelCode ,
		SupplierCode ,
		TypeCode ,
		Stock ,
		RetalPrice ,
		PartStatus ,
		ActiveStatus ,
		AccessoriesType ,
		RowStatus ,
		CreatedBy ,
		CreatedTime ,
		LastUpdateBy ,
		LastUpdateTime
FROM	tmp__SparePartMaster_38
GO

SET IDENTITY_INSERT SparePartMaster OFF
GO

DROP TABLE tmp__SparePartMaster_38
GO

ALTER TABLE IndentPartDetail ADD
CONSTRAINT [FK_IndentPartDetail[many]]_SparePartMaster[one]]] FOREIGN KEY(SparePartMasterID) REFERENCES SparePartMaster(ID)
GO

ALTER TABLE DepositBKewajibanDetail ADD
CONSTRAINT [FK_DepositBKewajibanDetail[many]]_SparePartMaster[one]]] FOREIGN KEY(SparePartMasterID) REFERENCES SparePartMaster(ID)
GO

ALTER TABLE EstimationEquipDetail ADD
CONSTRAINT [FK_EstimationEquipDetail[many]]_SparePartMaster[one]]] FOREIGN KEY(SparePartMasterID) REFERENCES SparePartMaster(ID)
GO

ALTER TABLE WSCDetailBB ADD
CONSTRAINT [FK_WSCDetailBB[many]]_SparePartMaster[one]]] FOREIGN KEY(SparePartMasterID) REFERENCES SparePartMaster(ID)
GO

ALTER TABLE SpecialItemDetail ADD
CONSTRAINT [FK_SpecialItemDetail_SparePartMaster[one]]] FOREIGN KEY(SparePartMasterID) REFERENCES SparePartMaster(ID)
GO

ALTER TABLE WSCDetail ADD
CONSTRAINT [FK_WSCDetail[many]]_SparePartMaster[one]]] FOREIGN KEY(SparePartMasterID) REFERENCES SparePartMaster(ID)
GO

ALTER TABLE SparePartMasterTOP ADD
CONSTRAINT [FK_SparePartMasterTOP[many]]_SparePartMaster[one]]] FOREIGN KEY(SparePartMasterID) REFERENCES SparePartMaster(ID)
GO

ALTER TABLE SparePartPackingDetail ADD
CONSTRAINT [FK_SparePartPackingDetail[many]]_SparePartMaster[one]]] FOREIGN KEY(SparePartMasterID) REFERENCES SparePartMaster(ID)
GO

ALTER TABLE SparePartDODetail ADD
CONSTRAINT [FK_SparePartDODetail[many]]_SparePartMaster[one]]] FOREIGN KEY(SparePartMasterID) REFERENCES SparePartMaster(ID)
GO

ALTER TABLE PQRPartsCodeBB ADD
CONSTRAINT [FK_PQRPartsCodeBB[one]]_SparePartMaster[one]]] FOREIGN KEY(SparePartMasterID) REFERENCES SparePartMaster(ID)
GO

ALTER TABLE PQRPartsCode ADD
CONSTRAINT [FK_PQRPartsCode[one]]_SparePartMaster[one]]] FOREIGN KEY(SparePartMasterID) REFERENCES SparePartMaster(ID)
GO

ALTER TABLE SparepartMaxOrder ADD
CONSTRAINT [FK_SparepartMaxOrder_SparePartMaster[one]]] FOREIGN KEY(SparepartID) REFERENCES SparePartMaster(ID)
GO

ALTER TABLE PartIncidentalDetail ADD
CONSTRAINT [FK_PartIncidentalDetail]]_SparePartMaster[one]]] FOREIGN KEY(SparePartMasterID) REFERENCES SparePartMaster(ID)
GO

COMMIT
GO




PRINT 'SParepartBilling'
GO

IF NOT EXISTS ( SELECT	''
				FROM	sys.[indexes] a
				WHERE	a.[name] = 'IX_SparePartBilling_DealerID_RowStatus' )
   BEGIN
   
		 CREATE INDEX IX_SparePartBilling_DealerID_RowStatus ON SparePartBilling(ID,BillingNumber,LastUpdateTime,BillingDate,TotalAmount,Tax,CreatedBy,CreatedTime,LastUpdateBy,DealerID,RowStatus)
 
   END


IF NOT EXISTS ( SELECT	''
				FROM	sys.[indexes] a
				WHERE	a.[name] = 'IX_SparePartBilling_BillingNumber_RowStatus' )
   BEGIN
   
		
		 CREATE INDEX IX_SparePartBilling_BillingNumber_RowStatus ON SparePartBilling(BillingDate,TotalAmount,Tax,BillingNumber,RowStatus)
 
   END

GO




PRINT 'SparePartBillingDetail'

GO


IF NOT EXISTS ( SELECT	''
				FROM	sys.[indexes] a
				WHERE	a.[name] = 'IX_SparePartBillingDetail_SparePartDODetailID_RowStatus' )
   BEGIN
   
		 CREATE INDEX IX_SparePartBillingDetail_SparePartDODetailID_RowStatus ON SparePartBillingDetail(SparePartDODetailID,RowStatus)

   END

IF NOT EXISTS ( SELECT	''
				FROM	sys.[indexes] a
				WHERE	a.[name] = 'IX_SparePartBillingDetail_SparePartBillingID' )
   BEGIN
   
		 CREATE INDEX IX_SparePartBillingDetail_SparePartBillingID ON SparePartBillingDetail(ID,BillingItemNo,SparePartDODetailID,CreatedTime,LastUpdateBy,LastUpdateTime,Quantity,ItemPrice,TotalPrice,Tax,RowStatus,CreatedBy,SparePartBillingID)
   END

 
IF NOT EXISTS ( SELECT	''
				FROM	sys.[indexes] a
				WHERE	a.[name] = 'IX_SparePartBillingDetail_SparePartBillingID_RowStatus' )
   BEGIN
   


		 CREATE INDEX IX_SparePartBillingDetail_SparePartBillingID_RowStatus ON SparePartBillingDetail(Tax,CreatedBy,CreatedTime,LastUpdateBy,LastUpdateTime,ID,BillingItemNo,SparePartDODetailID,Quantity,ItemPrice,TotalPrice,SparePartBillingID,RowStatus)
   END
    

	 
IF NOT EXISTS ( SELECT	''
				FROM	sys.[indexes] a
				WHERE	a.[name] = 'IX_SparePartBillingDetail_SparePartBillingID_RowStatus2' )
   BEGIN
   


	
		 CREATE INDEX IX_SparePartBillingDetail_SparePartBillingID_RowStatus2 ON SparePartBillingDetail(SparePartDODetailID,TotalPrice,Tax,SparePartBillingID,RowStatus)
   END

GO



PRINT '/*MSAPPLICATION*/'
GO
 
 set xact_abort on
go

begin transaction
go

create index IX_AppId on MsApplicationPermission(AppId)
go

create index IX_PermissionId on MsApplicationPermission(PermissionId)
go

commit
go




PRINT 'SparePartPO'
GO

SET xact_abort on
go

begin transaction
go

alter table PendingOrder drop
  constraint [FK_PendingOrder[many]]_SparePartPO[one]]] 
go

alter table SparePartOutstandingOrder drop
  constraint [FK_SparePartOutstandingOrder[many]]_SparePartPO[one]]] 
go

alter table SparePartPendingOrder drop
  constraint [FK_SparePartPendingOrder[one]]_SparePartPO[one]]] 
go

alter table SparePartPOEstimate drop
  constraint [FK_SparePartPOEstimate[one]]_SparePartPO[one]]] 
go

alter table SparePartPOStatus drop
  constraint [FK_SparePartPOStatus_SparePartPO[one]]] 
go

alter table SparePartPO drop
  constraint DF_SparePartPO_IndentTransfer ,
  constraint IPONumber_SparePartPO ,
  constraint [FK_SparePartPO_Dealer[one]]] 
go

exec sp_rename 'dbo.PK_SparePartPO', 'tmp__PK_SparePartPO', 'OBJECT'
go

exec sp_rename 'dbo.SparePartPO', 'tmp__SparePartPO_42', 'OBJECT'
go

create table SparePartPO(
  ID               int           not null identity constraint PK_SparePartPO primary key,
  PONumber         varchar(15)   constraint IPONumber_SparePartPO unique,
  OrderType        varchar(1),
  DealerID         smallint,
  PODate           smalldatetime,
  TermOfPaymentID  int,
  TOPBlockStatusID int,
  DeliveryDate     datetime,
  ProcessCode      varchar(1),
  CancelRequestBy  varchar(20),
  IndentTransfer   tinyint       constraint DF_SparePartPO_IndentTransfer default (0),
  PickingTicket    varchar(100),
  SentPODate       datetime,
  IsTransfer       bit,
  DMSPRNo          varchar(50),
  RowStatus        smallint,
  CreatedBy        varchar(20),
  CreatedTime      datetime,
  LastUpdateBy     varchar(20),
  LastUpdateTime   datetime
)
go

alter table SparePartPO add
  constraint [FK_SparePartPO[many]]_Dealer[one]]] foreign key(DealerID) references Dealer(ID),
  constraint [FK_SparePartPO[many]]_TermOfPayment[one]]] foreign key(TermOfPaymentID) references TermOfPayment(ID),
  constraint [FK_SparePartPO[one]]_TOPBlockStatus[one]]] foreign key(TOPBlockStatusID) references TOPBlockStatus(ID)
go

create index IX_SparePartPO_DealerID on SparePartPO(DealerID)
go

create index IX_SparePartPO_DealerID_PODate on SparePartPO(DealerID,PODate)
go

create index IX_SparePartPO_PODate on SparePartPO(PODate)
go

set identity_insert SparePartPO on
go

insert into SparePartPO(ID,PONumber,OrderType,DealerID,PODate,DeliveryDate,ProcessCode,CancelRequestBy,IndentTransfer,PickingTicket,SentPODate,IsTransfer,RowStatus,CreatedBy,CreatedTime,LastUpdateBy,LastUpdateTime) select ID,PONumber,OrderType,DealerID,PODate,DeliveryDate,ProcessCode,CancelRequestBy,IndentTransfer,PickingTicket,SentPODate,IsTransfer,RowStatus,CreatedBy,CreatedTime,LastUpdateBy,LastUpdateTime from tmp__SparePartPO_42
go

set identity_insert SparePartPO off
go

drop table tmp__SparePartPO_42
go

alter table SparePartPOStatus add
  constraint [FK_SparePartPOStatus_SparePartPO[one]]] foreign key(SparePartPOID) references SparePartPO(ID)
go

alter table SparePartPOEstimate add
  constraint [FK_SparePartPOEstimate[one]]_SparePartPO[one]]] foreign key(SparePartPOID) references SparePartPO(ID)
go

alter table SparePartPendingOrder add
  constraint [FK_SparePartPendingOrder[one]]_SparePartPO[one]]] foreign key(SparePartPOID) references SparePartPO(ID)
go

alter table SparePartOutstandingOrder add
  constraint [FK_SparePartOutstandingOrder[many]]_SparePartPO[one]]] foreign key(SparePartPOID) references SparePartPO(ID)
go

alter table PendingOrder add
  constraint [FK_PendingOrder[many]]_SparePartPO[one]]] foreign key(SparepartPOID) references SparePartPO(ID)
go

commit
go




PRINT 'SParepartPendingOrder'
GO
set xact_abort on
go

begin transaction
go

alter table SparePartPendingOrder drop
  constraint IX_SparePartPendingOrderPOID
go

alter table SparePartPendingOrder add
  constraint IX_SparePartPendingOrderPOID unique(SparePartPOID,SONumber)
go

commit
go



PRINT 'SAPCustomer'
GO


set xact_abort on
go

begin transaction
go

alter table SAPCustomerResponse drop
  constraint [FK_SAPCustomerResponse[many]]_SAPCustomer[one]]] 
go

alter table SPKCustomer drop
  constraint [FK_SPKCustomer[one]]_SAPCustomer[one]]] 
go

alter table SAPCustomer drop
  constraint [FK_SAPCustomer[many]]_SalesmanHeader[one]]] ,
  constraint [FK_SAPCustomer[many]]_VechileType[one]]] 
go

exec sp_rename 'dbo.PK__SAPCusto__3214EC2761604D30', 'tmp__PK__SAPCusto__3214EC2761604D30', 'OBJECT'
go

exec sp_rename 'dbo.SAPCustomer', 'tmp__SAPCustomer_44', 'OBJECT'
go

create table SAPCustomer(
  ID                     int              not null identity constraint PK_SAPCustomer primary key,
  SalesforceID           varchar(50),
  DealerID               smallint,
  SalesmanHeaderID       smallint,
  VechileTypeID          smallint,
  CustomerCode           varchar(8),
  CustomerName           varchar(50),
  CustomerType           smallint,
  CustomerAddress        varchar(100),
  Phone                  varchar(30),
  Email                  varchar(50),
  Sex                    tinyint,
  AgeSegment             tinyint,
  CustomerPurpose        smallint,
  InformationType        smallint,
  InformationSource      smallint,
  Status                 tinyint,
  Qty                    int,
  ProspectDate           datetime,
  isSPK                  bit,
  CurrVehicleBrand       varchar(50),
  CurrVehicleType        varchar(50),
  Note                   varchar(100),
  WebID                  varchar(20),
  BirthDate              Date,
  PreferedVehicleModel   varchar(100),
  Description            varchar(2000),
  EstimatedCloseDate     Date,
  OriginatingLeadId      uniqueidentifier,
  StatusCode             smallint,
  LeadStatus             tinyint,
  StateCode              tinyint,
  CampaignName           varchar(100),
  BusinessSectorDetailID int,
  RowStatus              smallint,
  CreatedBy              varchar(20),
  CreatedTime            datetime,
  LastUpdateBy           varchar(20),
  LastUpdateTime         datetime
)
go

alter table SAPCustomer add
  constraint [FK_SAPCustomer[many]]_BusinessSectorDetail[one]]] foreign key(BusinessSectorDetailID) references BusinessSectorDetail(ID),
  constraint [FK_SAPCustomer[many]]_Dealer[one]]] foreign key(DealerID) references Dealer(ID),
  constraint [FK_SAPCustomer[many]]_SalesmanHeader[one]]] foreign key(SalesmanHeaderID) references SalesmanHeader(ID),
  constraint [FK_SAPCustomer[many]]_VechileType[one]]] foreign key(VechileTypeID) references VechileType(ID)
go

create index IX_SAPCustomer_SalesmanHeaderID on SAPCustomer(SalesmanHeaderID)
go

set identity_insert SAPCustomer on
go

insert into SAPCustomer(ID,SalesforceID,DealerID,SalesmanHeaderID,VechileTypeID,CustomerCode,CustomerName,CustomerType,CustomerAddress,Phone,Email,Sex,AgeSegment,CustomerPurpose,InformationType,InformationSource,Status,Qty,ProspectDate,isSPK,CurrVehicleBrand,CurrVehicleType,Note,WebID,RowStatus,CreatedBy,CreatedTime,LastUpdateBy,LastUpdateTime) select ID,SalesforceID,DealerID,SalesmanHeaderID,VechileTypeID,CustomerCode,CustomerName,CustomerType,CustomerAddress,Phone,Email,Sex,AgeSegment,CustomerPurpose,InformationType,InformationSource,Status,Qty,ProspectDate,isSPK,CurrVehicleBrand,CurrVehicleType,Note,CONVERT(varchar(20), WebID),RowStatus,CreatedBy,CreatedTime,LastUpdateBy,LastUpdateTime from tmp__SAPCustomer_44
go

set identity_insert SAPCustomer off
go

drop table tmp__SAPCustomer_44
go

alter table SPKCustomer add
  constraint [FK_SPKCustomer[one]]_SAPCustomer[one]]] foreign key(SAPCustomerID) references SAPCustomer(ID)
go

alter table SAPCustomerResponse add
  constraint [FK_SAPCustomerResponse[many]]_SAPCustomer[one]]] foreign key(SAPCustomerID) references SAPCustomer(ID)
go

commit
go


PRINT 'SPKCustomer'

GO

set xact_abort on
go

begin transaction
go

alter table SPKCustomerProfile drop
  constraint [FK_SPKCustomerProfile[many]]_SPKCustomer[one]]] 
go

alter table SPKHeader drop
  constraint [FK_SPKHeader_SPKCustomer[one]]] 
go

alter table SPKCustomer drop
  constraint [FK_SPKCustomer[one]]_City[one]]] ,
  constraint [FK_SPKCustomer[one]]_SAPCustomer[one]]] 
go

exec sp_rename 'dbo.PK_SPKCustomer', 'tmp__PK_SPKCustomer', 'OBJECT'
go

exec sp_rename 'dbo.SPKCustomer', 'tmp__SPKCustomer_45', 'OBJECT'
go

create table SPKCustomer(
  ID                     int           not null identity constraint PK_SPKCustomer primary key,
  Code                   varchar(50),
  ReffCode               varchar(50),
  TipeCustomer           smallint,
  TipePerusahaan         smallint,
  Name1                  nvarchar(50),
  Name2                  nvarchar(50),
  Name3                  nvarchar(50),
  Alamat                 nvarchar(100),
  Kelurahan              nvarchar(50),
  Kecamatan              nvarchar(50),
  PostalCode             nvarchar(10),
  PreArea                varchar(20),
  CityID                 smallint,
  PrintRegion            varchar(1),
  PhoneNo                nvarchar(30),
  OfficeNo               nvarchar(30),
  HomeNo                 nvarchar(30),
  HpNo                   nvarchar(30),
  Email                  nvarchar(50),
  Status                 int,
  MCPStatus              smallint,
  LKPPStatus             smallint,
  SAPCustomerID          int,
  LKPPReference          varchar(50),
  BusinessSectorDetailID int,
  ImagePath              nvarchar(200),
  RowStatus              smallint,
  CreatedTime            datetime,
  CreatedBy              nvarchar(20),
  LastUpdateTime         datetime,
  LastUpdateBy           nvarchar(20)
)
go

alter table SPKCustomer add
  constraint [FK_SPKCustomer[many]]_BusinessSectorDetail[one]]] foreign key(BusinessSectorDetailID) references BusinessSectorDetail(ID),
  constraint [FK_SPKCustomer[one]]_City[one]]] foreign key(CityID) references City(ID),
  constraint [FK_SPKCustomer[one]]_SAPCustomer[one]]] foreign key(SAPCustomerID) references SAPCustomer(ID)
go

set identity_insert SPKCustomer on
go

insert into SPKCustomer(ID,Code,ReffCode,TipeCustomer,TipePerusahaan,Name1,Name2,Name3,Alamat,Kelurahan,Kecamatan,PostalCode,PreArea,CityID,PrintRegion,PhoneNo,OfficeNo,HomeNo,HpNo,Email,Status,MCPStatus,LKPPStatus,SAPCustomerID,ImagePath,RowStatus,CreatedTime,CreatedBy,LastUpdateTime,LastUpdateBy) select ID,Code,ReffCode,TipeCustomer,TipePerusahaan,Name1,Name2,Name3,Alamat,Kelurahan,Kecamatan,PostalCode,PreArea,CityID,PrintRegion,PhoneNo,OfficeNo,HomeNo,HpNo,Email,Status,MCPStatus,LKPPStatus,SAPCustomerID,CONVERT(nvarchar(200), ImagePath),RowStatus,CreatedTime,CreatedBy,LastUpdateTime,LastUpdateBy from tmp__SPKCustomer_45
go

set identity_insert SPKCustomer off
go

drop table tmp__SPKCustomer_45
go

alter table SPKHeader add
  constraint [FK_SPKHeader_SPKCustomer[one]]] foreign key(SPKCustomerID) references SPKCustomer(ID)
go

alter table SPKCustomerProfile add
  constraint [FK_SPKCustomerProfile[many]]_SPKCustomer[one]]] foreign key(SPKCustomerID) references SPKCustomer(ID)
go

commit
go


PRINT 'SPKHeader'
GO

SET XACT_ABORT ON
GO

BEGIN TRANSACTION
GO

ALTER TABLE SPKDetail DROP
CONSTRAINT [FK_SPKDetail[many]]_SPKHeader[one]]] 
GO

ALTER TABLE RevisionSPKFaktur DROP
CONSTRAINT [FK_RevisionSPKFaktur[many]]_SPKHeader[one]]] 
GO

ALTER TABLE SPKFaktur DROP
CONSTRAINT [FK_SPKFaktur[many]]_SPKHeader[one]]] 
GO

ALTER TABLE SPKHeader DROP
CONSTRAINT [FK_SPKHeader_Dealer[one]]] ,
CONSTRAINT [FK_SPKHeader_DealerBranch[one]]] ,
CONSTRAINT [FK_SPKHeader_SalesmanHeader[one]]] ,
CONSTRAINT [FK_SPKHeader_SPKCustomer[one]]] 
GO

EXEC sp_rename
	'dbo.PK_SPKHeader' ,
	'tmp__PK_SPKHeader' ,
	'OBJECT'
GO

EXEC sp_rename
	'dbo.SPKHeader' ,
	'tmp__SPKHeader_46' ,
	'OBJECT'
GO

CREATE TABLE SPKHeader
	   (
		 ID INT NOT NULL
				IDENTITY
				CONSTRAINT PK_SPKHeader PRIMARY KEY ,
		 DealerID SMALLINT ,
		 DealerBranchID INT ,
		 Status VARCHAR(2) ,
		 SPKNumber VARCHAR(15) ,
		 SPKReferenceNumber VARCHAR(15) ,
		 DealerSPKNumber VARCHAR(15) ,
		 IndentNumber VARCHAR(10) ,
		 PlanDeliveryMonth TINYINT ,
		 PlanDeliveryYear SMALLINT ,
		 PlanDeliveryDate DATETIME ,
		 PlanInvoiceMonth TINYINT ,
		 PlanInvoiceYear SMALLINT ,
		 PlanInvoiceDate DATETIME ,
		 CustomerRequestID INT ,
		 SPKCustomerID INT ,
		 ValidateTime DATETIME ,
		 ValidateBy NVARCHAR(20) ,
		 RejectedReason NVARCHAR(255) ,
		 SalesmanHeaderID SMALLINT ,
		 EvidenceFile NVARCHAR(255) ,
		 ValidationKey NVARCHAR(20) ,
		 FlagUpdate SMALLINT ,
		 IsSend SMALLINT ,
		 DealerSPKDate DATETIME ,
		 BenefitMasterHeaderID INT ,
		 RowStatus SMALLINT ,
		 CreatedTime DATETIME ,
		 CreatedBy NVARCHAR(20) ,
		 LastUpdateTime DATETIME ,
		 LastUpdateBy NVARCHAR(20)
	   )
GO

ALTER TABLE SPKHeader ADD
CONSTRAINT [FK_SPKHeader[many]]_BenefitMasterHeader[one]]] FOREIGN KEY(BenefitMasterHeaderID) REFERENCES BenefitMasterHeader(ID),
CONSTRAINT [FK_SPKHeader[many]]_Dealer[one]]] FOREIGN KEY(DealerID) REFERENCES Dealer(ID),
CONSTRAINT [FK_SPKHeader[many]]_DealerBranch[one]]] FOREIGN KEY(DealerBranchID) REFERENCES DealerBranch(ID),
CONSTRAINT [FK_SPKHeader[many]]_SalesmanHeader[one]]] FOREIGN KEY(SalesmanHeaderID) REFERENCES SalesmanHeader(ID),
CONSTRAINT [FK_SPKHeader[one]]_SPKCustomer[one]]] FOREIGN KEY(SPKCustomerID) REFERENCES SPKCustomer(ID)
GO

CREATE INDEX IX_SPKHeader_DealerID_RowStatus ON SPKHeader(Status,DealerID,RowStatus)
GO

CREATE INDEX IX_SPKHeader_CustomerRequestID_RowStatus ON SPKHeader(CustomerRequestID,RowStatus)
GO

CREATE INDEX IX_SPKHEADER_DEALERID ON SPKHeader(DealerID)
GO

CREATE INDEX IX_SPKHEADER_PLANDELIVERYDATE ON SPKHeader(PlanDeliveryDate)
GO

SET IDENTITY_INSERT SPKHeader ON
GO

INSERT	INTO SPKHeader
		(
		  ID ,
		  DealerID ,
		  DealerBranchID ,
		  Status ,
		  SPKNumber ,
		  DealerSPKNumber ,
		  IndentNumber ,
		  PlanDeliveryMonth ,
		  PlanDeliveryYear ,
		  PlanDeliveryDate ,
		  PlanInvoiceMonth ,
		  PlanInvoiceYear ,
		  PlanInvoiceDate ,
		  CustomerRequestID ,
		  SPKCustomerID ,
		  ValidateTime ,
		  ValidateBy ,
		  RejectedReason ,
		  SalesmanHeaderID ,
		  EvidenceFile ,
		  ValidationKey ,
		  FlagUpdate ,
		  IsSend ,
		  DealerSPKDate ,
		  RowStatus ,
		  CreatedTime ,
		  CreatedBy ,
		  LastUpdateTime ,
		  LastUpdateBy
		)
SELECT	ID ,
		DealerID ,
		DealerBranchID ,
		Status ,
		SPKNumber ,
		DealerSPKNumber ,
		IndentNumber ,
		PlanDeliveryMonth ,
		PlanDeliveryYear ,
		PlanDeliveryDate ,
		PlanInvoiceMonth ,
		PlanInvoiceYear ,
		PlanInvoiceDate ,
		CustomerRequestID ,
		SPKCustomerID ,
		ValidateTime ,
		ValidateBy ,
		RejectedReason ,
		SalesmanHeaderID ,
		EvidenceFile ,
		ValidationKey ,
		FlagUpdate ,
		IsSend ,
		DealerSPKDate ,
		RowStatus ,
		CreatedTime ,
		CreatedBy ,
		LastUpdateTime ,
		LastUpdateBy
FROM	tmp__SPKHeader_46
GO

SET IDENTITY_INSERT SPKHeader OFF
GO

DROP TABLE tmp__SPKHeader_46
GO

ALTER TABLE SPKFaktur ADD
CONSTRAINT [FK_SPKFaktur[many]]_SPKHeader[one]]] FOREIGN KEY(SPKHeaderID) REFERENCES SPKHeader(ID)
GO

ALTER TABLE RevisionSPKFaktur ADD
CONSTRAINT [FK_RevisionSPKFaktur[many]]_SPKHeader[one]]] FOREIGN KEY(SPKHeaderID) REFERENCES SPKHeader(ID)
GO

ALTER TABLE SPKDetail ADD
CONSTRAINT [FK_SPKDetail[many]]_SPKHeader[one]]] FOREIGN KEY(SPKHeaderID) REFERENCES SPKHeader(ID)
GO

COMMIT
GO


PRINT 'TRTrainee'

GO


set xact_abort on
go

begin transaction
go

alter table TrClassRegistration drop
  constraint [FK_TrClassRegistration[many]]_TrTrainee[one]]] 
go

alter table TrTrainee drop
  constraint [FK_TrTrainee_Dealer[one]]] 
go

exec sp_rename 'dbo.PK_TrTrainee', 'tmp__PK_TrTrainee', 'OBJECT'
go

exec sp_rename 'dbo.TrTrainee', 'tmp__TrTrainee_47', 'OBJECT'
go

create table TrTrainee(
  ID               int         not null identity constraint PK_TrTrainee primary key,
  SalesmanHeaderID smallint,
  [Name]           varchar(50),
  DealerID         smallint,
  DealerBranchID   int,
  BirthDate        datetime,
  Gender           tinyint,
  NoKTP            varchar(20),
  Email            varchar(50),
  StartWorkingDate datetime,
  Status           char(1),
  JobPosition      varchar(50),
  EducationLevel   varchar(50),
  Photo            image,
  ShirtSize        varchar(10),
  RowStatus        smallint,
  CreatedBy        varchar(20),
  CreatedTime      datetime,
  LastUpdateBy     varchar(20),
  LastUpdateTime   datetime
)
go

alter table TrTrainee add
  constraint [FK_TrTrainee[many]]_Dealer[one]]] foreign key(DealerID) references Dealer(ID),
  constraint [FK_TrTrainee[many]]_DealerBranch[one]]] foreign key(DealerBranchID) references DealerBranch(ID),
  constraint [FK_TrTrainee_SalesmanHeader[one]]] foreign key(SalesmanHeaderID) references SalesmanHeader(ID)
go

set identity_insert TrTrainee on
go

insert into TrTrainee(ID,SalesmanHeaderID,[Name],DealerID,BirthDate,Gender,StartWorkingDate,Status,JobPosition,EducationLevel,Photo,ShirtSize,RowStatus,CreatedBy,CreatedTime,LastUpdateBy,LastUpdateTime) select ID,CONVERT(smallint, SalesmanHeaderID),[Name],DealerID,BirthDate,Gender,StartWorkingDate,Status,JobPosition,EducationLevel,Photo,ShirtSize,RowStatus,CreatedBy,CreatedTime,LastUpdateBy,LastUpdateTime from tmp__TrTrainee_47
go

set identity_insert TrTrainee off
go

drop table tmp__TrTrainee_47
go

alter table TrClassRegistration add
  constraint [FK_TrClassRegistration[many]]_TrTrainee[one]]] foreign key(TraineeID) references TrTrainee(ID)
go

commit
go






 /* To prevent any potential data loss issues, you should review this script in detail before running it outside the context of the database designer.*/

 PRINT 'SparePartPODetail'
 GO
BEGIN TRANSACTION
SET QUOTED_IDENTIFIER ON
SET ARITHABORT ON
SET NUMERIC_ROUNDABORT OFF
SET CONCAT_NULL_YIELDS_NULL ON
SET ANSI_NULLS ON
SET ANSI_PADDING ON
SET ANSI_WARNINGS ON
COMMIT
BEGIN TRANSACTION
GO
CREATE TABLE dbo.Tmp_SparePartPODetail
	(
	ID int NOT NULL IDENTITY (1, 1),
	SparePartPOID int NULL,
	SparePartMasterID int NULL,
	CheckListStatus varchar(2) NULL,
	Quantity int NULL,
	RetailPrice money NULL,
	EstimateStatus varchar(1) NULL,
	StopMark smallint NULL,
	TotalForeCast int NULL,
	RowStatus smallint NULL,
	CreatedBy varchar(20) NULL,
	CreatedTime datetime NULL,
	LastUpdateBy varchar(20) NULL,
	LastUpdateTime datetime NULL
	)  ON [PRIMARY]
GO
ALTER TABLE dbo.Tmp_SparePartPODetail SET (LOCK_ESCALATION = TABLE)
GO
GRANT DELETE ON dbo.Tmp_SparePartPODetail TO bsi  AS dbo
GO
GRANT INSERT ON dbo.Tmp_SparePartPODetail TO bsi  AS dbo
GO
GRANT REFERENCES ON dbo.Tmp_SparePartPODetail TO bsi  AS dbo
GO
GRANT SELECT ON dbo.Tmp_SparePartPODetail TO bsi  AS dbo
GO
GRANT SELECT ON dbo.Tmp_SparePartPODetail TO monitoring  AS dbo
GO
GRANT SELECT ON dbo.Tmp_SparePartPODetail TO ccUser  AS dbo
GO
GRANT SELECT ON dbo.Tmp_SparePartPODetail TO analyst  AS dbo
GO
GRANT UPDATE ON dbo.Tmp_SparePartPODetail TO bsi  AS dbo
GO
SET IDENTITY_INSERT dbo.Tmp_SparePartPODetail ON
GO
IF EXISTS(SELECT * FROM dbo.SparePartPODetail)
	 EXEC('INSERT INTO dbo.Tmp_SparePartPODetail (ID, SparePartPOID, SparePartMasterID, CheckListStatus, Quantity, RetailPrice, EstimateStatus, StopMark, RowStatus, CreatedBy, CreatedTime, LastUpdateBy, LastUpdateTime)
		SELECT ID, SparePartPOID, SparePartMasterID, CheckListStatus, Quantity, RetailPrice, EstimateStatus, StopMark, RowStatus, CreatedBy, CreatedTime, LastUpdateBy, LastUpdateTime FROM dbo.SparePartPODetail WITH (HOLDLOCK TABLOCKX)')
GO
SET IDENTITY_INSERT dbo.Tmp_SparePartPODetail OFF
GO
ALTER TABLE dbo.EquipSPPOAlocation
	DROP CONSTRAINT FK_EstimationEquipSPPOAlocation_SparePartPODetail
GO
ALTER TABLE dbo.IndentPartPO
	DROP CONSTRAINT [FK_IndentPartPO[many]]_SparePartPODetail[one]]]
GO
DROP TABLE dbo.SparePartPODetail
GO
EXECUTE sp_rename N'dbo.Tmp_SparePartPODetail', N'SparePartPODetail', 'OBJECT' 
GO
ALTER TABLE dbo.SparePartPODetail ADD CONSTRAINT
	PK_SparePartPODetail PRIMARY KEY CLUSTERED 
	(
	ID
	) WITH( PAD_INDEX = OFF, FILLFACTOR = 90, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]

GO
CREATE NONCLUSTERED INDEX IX_SparePartPODetail_SparePartPOID ON dbo.SparePartPODetail
	(
	SparePartPOID
	) WITH( PAD_INDEX = OFF, FILLFACTOR = 90, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
CREATE NONCLUSTERED INDEX IX_SparePartPODetail_SparePartMasterID ON dbo.SparePartPODetail
	(
	SparePartMasterID
	) WITH( PAD_INDEX = OFF, FILLFACTOR = 90, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
COMMIT
BEGIN TRANSACTION
GO
ALTER TABLE dbo.IndentPartPO WITH NOCHECK ADD CONSTRAINT
	[FK_IndentPartPO[many]]_SparePartPODetail[one]]] FOREIGN KEY
	(
	SparePartPODetailID
	) REFERENCES dbo.SparePartPODetail
	(
	ID
	) ON UPDATE  NO ACTION 
	 ON DELETE  NO ACTION 
	
GO
ALTER TABLE dbo.IndentPartPO SET (LOCK_ESCALATION = TABLE)
GO
COMMIT
BEGIN TRANSACTION
GO
ALTER TABLE dbo.EquipSPPOAlocation WITH NOCHECK ADD CONSTRAINT
	FK_EstimationEquipSPPOAlocation_SparePartPODetail FOREIGN KEY
	(
	SparePartPODetailID
	) REFERENCES dbo.SparePartPODetail
	(
	ID
	) ON UPDATE  NO ACTION 
	 ON DELETE  NO ACTION 
	
GO
ALTER TABLE dbo.EquipSPPOAlocation SET (LOCK_ESCALATION = TABLE)
GO
COMMIT






IF NOT EXISTS ( SELECT	''
				FROM	sys.[indexes] a
				WHERE	a.[name] = 'IX_SparePartPOEstimateDetail_RowStatus' )
   BEGIN
 
		create index IX_SparePartPOEstimateDetail_RowStatus on SparePartPOEstimateDetail(Discount,SparePartPOEstimateID,AllocQty,RetailPrice,RowStatus)

   END




go