
PRINT '/*AssistPartSales*/'
go
SET XACT_ABORT ON
GO

BEGIN TRANSACTION
GO

EXEC sp_rename
	'dbo.PK_AssistPartSales' ,
	'tmp__PK_AssistPartSales' ,
	'OBJECT'
GO

EXEC sp_rename
	'dbo.AssistPartSales' ,
	'tmp__AssistPartSales_22' ,
	'OBJECT'
GO

CREATE TABLE AssistPartSales
	   (
		 ID INT NOT NULL
				IDENTITY
				CONSTRAINT PK_AssistPartSales PRIMARY KEY ,
		 AssistUploadLogID INT ,
		 TglTransaksi DATE ,
		 DealerID INT ,
		 DealerCode VARCHAR(50) ,
		 DealerBranchID INT ,
		 DealerBranchCode VARCHAR(50) ,
		 KodeCustomer VARCHAR(80) ,
		 SalesChannelID INT ,
		 SalesChannelCode VARCHAR(50) ,
		 TrTraineeSalesSparepartID INT ,
		 SalesmanHeaderID INT ,
		 KodeSalesman VARCHAR(50) ,
		 NoWorkOrder VARCHAR(50) ,
		 SparepartMasterID INT ,
		 NoParts VARCHAR(50) ,
		 Qty FLOAT ,
		 HargaBeli MONEY ,
		 HargaJual MONEY ,
		 IsCampaign BIT ,
		 CampaignNo VARCHAR(20) ,
		 CampaignDescription VARCHAR(100) ,
		 RemarksSystem VARCHAR(MAX) ,
		 StatusAktif SMALLINT NOT NULL ,
		 ValidateSystemStatus SMALLINT NOT NULL ,
		 RowStatus SMALLINT NOT NULL ,
		 CreatedBy VARCHAR(50) NOT NULL ,
		 CreatedTime DATETIME NOT NULL ,
		 LastUpdateBy VARCHAR(50) ,
		 LastUpdateTime DATETIME
	   )
GO

ALTER TABLE AssistPartSales ADD
CONSTRAINT [FK_AssistPartSales[many]]_DealerBranch[one]]] FOREIGN KEY(DealerBranchID) REFERENCES DealerBranch(ID)
GO

IF NOT EXISTS ( SELECT	''
				FROM	sys.[indexes] a
				WHERE	a.[name] = 'IX_AssistPartSales_AssistUploadLogID_RowStatus' )
   BEGIN
 
		 CREATE INDEX IX_AssistPartSales_AssistUploadLogID_RowStatus ON AssistPartSales(ID,AssistUploadLogID,RowStatus)

   END
GO

SET IDENTITY_INSERT AssistPartSales ON
GO

INSERT	INTO AssistPartSales
		(
		  ID ,
		  AssistUploadLogID ,
		  TglTransaksi ,
		  DealerID ,
		  DealerCode ,
		  KodeCustomer ,
		  SalesChannelID ,
		  SalesChannelCode ,
		  TrTraineeSalesSparepartID ,
		  SalesmanHeaderID ,
		  KodeSalesman ,
		  NoWorkOrder ,
		  SparepartMasterID ,
		  NoParts ,
		  Qty ,
		  HargaBeli ,
		  HargaJual ,
		  RemarksSystem ,
		  StatusAktif ,
		  ValidateSystemStatus ,
		  RowStatus ,
		  CreatedBy ,
		  CreatedTime ,
		  LastUpdateBy ,
		  LastUpdateTime
		)
SELECT	ID ,
		AssistUploadLogID ,
		TglTransaksi ,
		DealerID ,
		DealerCode ,
		KodeCustomer ,
		SalesChannelID ,
		SalesChannelCode ,
		TrTraineeSalesSparepartID ,
		SalesmanHeaderID ,
		KodeSalesman ,
		NoWorkOrder ,
		SparepartMasterID ,
		NoParts ,
		Qty ,
		HargaBeli ,
		HargaJual ,
		RemarksSystem ,
		StatusAktif ,
		ValidateSystemStatus ,
		RowStatus ,
		CreatedBy ,
		CreatedTime ,
		LastUpdateBy ,
		LastUpdateTime
FROM	tmp__AssistPartSales_22
GO

SET IDENTITY_INSERT AssistPartSales OFF
GO

DROP TABLE tmp__AssistPartSales_22
GO

COMMIT
GO
 


PRINT ' /*Assist Part Stokc*/'
GO

 set xact_abort on
go

begin transaction
go

exec sp_rename 'dbo.PK_AssistPartStock', 'tmp__PK_AssistPartStock', 'OBJECT'
go

exec sp_rename 'dbo.AssistPartStock', 'tmp__AssistPartStock_23', 'OBJECT'
go

create table AssistPartStock(
  ID                   int          not null identity constraint PK_AssistPartStock primary key,
  AssistUploadLogID    int,
  Month                nchar(10),
  Year                 nchar(10),
  DealerID             int,
  DealerCode           varchar(30),
  DealerBranchID       int,
  DealerBranchCode     varchar(50),
  SparepartMasterID    int,
  NoParts              varchar(50),
  JumlahStokAwal       float,
  JumlahDatang         float,
  HargaBeli            money,
  RemarksSystem        varchar(max),
  StatusAktif          smallint     not null,
  ValidateSystemStatus smallint     not null,
  RowStatus            smallint     not null,
  CreatedBy            varchar(20)  not null,
  CreatedTime          datetime     not null,
  LastUpdateBy         varchar(20),
  LastUpdateTime       datetime
)
go

IF NOT EXISTS ( SELECT	''
				FROM	sys.[indexes] a
				WHERE	a.[name] = 'IX_AssistPartStock_AssistUploadLogID_RowStatus' )
   BEGIN
		create index IX_AssistPartStock_AssistUploadLogID_RowStatus on AssistPartStock(ID,AssistUploadLogID,RowStatus)

		 
		 

   END

go

set identity_insert AssistPartStock on
go

insert into AssistPartStock(ID,AssistUploadLogID,Month,Year,DealerID,DealerCode,SparepartMasterID,NoParts,JumlahStokAwal,JumlahDatang,HargaBeli,RemarksSystem,StatusAktif,ValidateSystemStatus,RowStatus,CreatedBy,CreatedTime,LastUpdateBy,LastUpdateTime) select ID,AssistUploadLogID,Month,Year,DealerID,DealerCode,SparepartMasterID,NoParts,JumlahStokAwal,JumlahDatang,HargaBeli,RemarksSystem,StatusAktif,ValidateSystemStatus,RowStatus,CreatedBy,CreatedTime,LastUpdateBy,LastUpdateTime from tmp__AssistPartStock_23
go

set identity_insert AssistPartStock off
go

drop table tmp__AssistPartStock_23
go

commit
go


PRINT '/*AssistServiceIncoming*/'
GO

 set xact_abort on
go

begin transaction
go

exec sp_rename 'dbo.PK_AssistServiceIncomingUploadTemp', 'tmp__PK_AssistServiceIncomingUploadTemp', 'OBJECT'
go

exec sp_rename 'dbo.AssistServiceIncoming', 'tmp__AssistServiceIncoming_24', 'OBJECT'
go

create table AssistServiceIncoming(
  ID                    int            not null identity constraint PK_AssistServiceIncoming primary key,
  AssistUploadLogID     int,
  TglBukaTransaksi      date,
  WaktuMasuk            time,
  TglTutupTransaksi    date,
  WaktuKeluar           time,
  DealerID              int,
  DealerCode            varchar(50),
  DealerBranchID        int,
  DealerBranchCode      varchar(50),
  TrTraineMekanikID     int,
  KodeMekanik           varchar(50),
  NoWorkOrder           varchar(50),
  ChassisMasterID       int,
  KodeChassis           varchar(50),
  WorkOrderCategoryID   int,
  WorkOrderCategoryCode varchar(50),
  KMService             int,
  ServicePlaceID        int,
  ServicePlaceCode      varchar(50),
  ServiceTypeID         int,
  ServiceTypeCode       varchar(100),
  TotalLC               money,
  MetodePembayaran      varchar(50),
  Model                 varchar(100),
  Transmition           varchar(30),
  DriveSystem           varchar(20),
  RemarksSystem         varchar(max),
  RemarksSpecial        varchar(300),
  RemarksBM             varchar(300),
  WOStatus              smallint       constraint DF_AssistServiceIncoming_WOStatus default (2),
  StatusAktif           smallint       not null,
  ValidateSystemStatus  smallint       not null,
  RowStatus             smallint       not null,
  CreatedBy             varchar(20)    not null,
  CreatedTime           datetime       not null,
  LastUpdateBy          varchar(20),
  LastUpdateTime        datetime
)
go

create index IX_AssistServiceIncoming_AssistUploadLogID_RowStatus on AssistServiceIncoming(ID,AssistUploadLogID,RowStatus)
go

create index IX_AssistServiceIncoming_DealerCode_DealerBranchCode_RowStatus on AssistServiceIncoming(ID,DealerCode,DealerBranchCode,RowStatus)
go

set identity_insert AssistServiceIncoming on
go

insert into AssistServiceIncoming(ID,AssistUploadLogID,TglBukaTransaksi,WaktuMasuk,TglTutupTransaksi,WaktuKeluar,DealerID,DealerCode,TrTraineMekanikID,KodeMekanik,NoWorkOrder,ChassisMasterID,KodeChassis,WorkOrderCategoryID,WorkOrderCategoryCode,KMService,ServicePlaceID,ServicePlaceCode,ServiceTypeID,ServiceTypeCode,TotalLC,MetodePembayaran,Model,Transmition,DriveSystem,RemarksSystem,RemarksSpecial,RemarksBM,StatusAktif,ValidateSystemStatus,RowStatus,CreatedBy,CreatedTime,LastUpdateBy,LastUpdateTime) select ID,AssistUploadLogID,TglBukaTransaksi,WaktuMasuk,TglTutupTransaksi,WaktuKeluar,DealerID,DealerCode,TrTraineMekanikID,KodeMekanik,CONVERT(varchar(50), NoWorkOrder),ChassisMasterID,KodeChassis,WorkOrderCategoryID,WorkOrderCategoryCode,KMService,ServicePlaceID,ServicePlaceCode,ServiceTypeID,ServiceTypeCode,TotalLC,MetodePembayaran,Model,Transmition,DriveSystem,RemarksSystem,RemarksSpecial,RemarksBM,StatusAktif,ValidateSystemStatus,RowStatus,CreatedBy,CreatedTime,LastUpdateBy,LastUpdateTime from tmp__AssistServiceIncoming_24
go

set identity_insert AssistServiceIncoming off
go

drop table tmp__AssistServiceIncoming_24
go

commit
go


