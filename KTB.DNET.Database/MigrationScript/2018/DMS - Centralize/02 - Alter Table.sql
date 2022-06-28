set xact_abort on
go

begin transaction
go

alter table Area2 drop
  constraint [FK_Area1[one]]_Area2[many]]] 
go

alter table Dealer drop
  constraint [FK_Dealer[many]]_Area1[one]]] 
go

alter table Area1 drop
  constraint [FK_MainArea[one]]_Area1[many]]] 
go

exec sp_rename 'dbo.PK_Area1', 'tmp__PK_Area1', 'OBJECT'
go

exec sp_rename 'dbo.Area1', 'tmp__Area1_0', 'OBJECT'
go

create table Area1(
  ID             int         not null identity constraint PK_Area1 primary key,
  AreaCode       varchar(10),
  Description    varchar(40),
  PICSales       varchar(50),
  PICServices    varchar(50),
  PICSpareparts  varchar(50),
  MainAreaID     int,
  RowStatus      smallint,
  CreatedBy      varchar(20),
  CreatedTime    datetime,
  LastUpdateBy   varchar(20),
  LastUpdateTime datetime
)
go

alter table Area1 add
  constraint [FK_MainArea[one]]_Area1[many]]] foreign key(MainAreaID) references MainArea(ID)
go

grant select,insert,update,delete on Area1 to bsi
go

grant select on Area1 to monitoring
go

grant select on Area1 to ccUser
go

grant select on Area1 to analyst
go

set identity_insert Area1 on
go

insert into Area1(ID,AreaCode,Description,MainAreaID,RowStatus,CreatedBy,CreatedTime,LastUpdateBy,LastUpdateTime) select ID,AreaCode,Description,MainAreaID,RowStatus,CreatedBy,CreatedTime,LastUpdateBy,LastUpdateTime from tmp__Area1_0
go

set identity_insert Area1 off
go

drop table tmp__Area1_0
go

alter table Area1 drop
  constraint [FK_MainArea[one]]_Area1[many]]] 
go

alter table Dealer drop
  constraint [FK_Dealer_MainArea[one]]] 
go

alter table MainArea drop
  constraint DF_MainArea_RowStatus 
go

exec sp_rename 'dbo.PK_MainArea', 'tmp__PK_MainArea', 'OBJECT'
go

exec sp_rename 'dbo.MainArea', 'tmp__MainArea_1', 'OBJECT'
go

create table MainArea(
  ID             int         not null identity constraint PK_MainArea primary key,
  AreaCode       varchar(20),
  Description    varchar(50),
  PICSales       varchar(50),
  PICServices    varchar(50),
  PICSpareparts  varchar(50),
  RowStatus      smallint    constraint DF_MainArea_RowStatus default (0),
  CreatedBy      varchar(20),
  CreatedTime    datetime,
  LastUpdateBy   varchar(20),
  LastUpdateTime datetime
)
go

set identity_insert MainArea on
go

insert into MainArea(ID,AreaCode,Description,RowStatus,CreatedBy,CreatedTime,LastUpdateBy,LastUpdateTime) select ID,AreaCode,Description,RowStatus,CreatedBy,CreatedTime,LastUpdateBy,LastUpdateTime from tmp__MainArea_1
go

set identity_insert MainArea off
go

drop table tmp__MainArea_1
go

alter table PartIncidentalDetail drop
  constraint [FK_PartIncidentalDetail]]_SparePartMaster[one]]] 
go

alter table WSCDetail drop
  constraint [FK_WSCDetail_SparePartMaster[one]]] 
go

alter table SparepartMaxOrder drop
  constraint [FK_SparepartMaxOrder_SparePartMaster[one]]] 
go

alter table WSCDetailBB drop
  constraint [FK_WSCDetailBB_SparePartMaster[one]]] 
go

alter table PQRPartsCode drop
  constraint [FK_PQRPartsCode[one]]_SparePartMaster[one]]] 
go

alter table PQRPartsCodeBB drop
  constraint [FK_PQRPartsCodeBB[one]]_SparePartMaster[one]]] 
go

alter table SparePartDODetail drop
  constraint [FK_SparePartDODetail[many]]_SparePartMaster[one]]] 
go

alter table SparePartPackingDetail drop
  constraint [FK_SparePartPackingDetail[many]]_SparePartMaster[one]]] 
go

alter table SpecialItemDetail drop
  constraint [FK_SpecialItemDetail_SparePartMaster[one]]] 
go

alter table IndentPartDetail drop
  constraint [FK_IndentPartDetail[many]]_SparePartMaster[one]]] 
go

alter table EstimationEquipDetail drop
  constraint FK_EstimationEquipDetail_SparePartMaster 
go

alter table DepositBKewajibanDetail drop
  constraint [FK_DepositBKewajibanDetail[many]]_SparePartMaster[one]]] 
go

exec sp_rename 'dbo.PK_SparePartMaster', 'tmp__PK_SparePartMaster', 'OBJECT'
go

exec sp_rename 'dbo.SparePartMaster', 'tmp__SparePartMaster_2', 'OBJECT'
go

create table SparePartMaster(
  ID                int         not null identity constraint PK_SparePartMaster primary key,
  ProductCategoryID smallint,
  PartNumber        varchar(18),
  PartName          varchar(30),
  PartNumberReff    varchar(18),
  UoM               varchar(18),
  AltPartNumber     varchar(18),
  AltPartName       varchar(30),
  PartCode          varchar(1),
  ModelCode         varchar(9),
  SupplierCode      varchar(10),
  TypeCode          varchar(5),
  Stock             int,
  RetalPrice        money,
  PartStatus        varchar(1),
  ActiveStatus      smallint,
  ProductType       varchar(25),
  AccessoriesType   smallint,
  RowStatus         smallint,
  CreatedBy         varchar(20),
  CreatedTime       datetime,
  LastUpdateBy      varchar(20),
  LastUpdateTime    datetime
)
go

create index IX_SparePartMaster_PartNumber on SparePartMaster(PartNumber)
go

create index IX_SparePartMaster_PartName on SparePartMaster(PartName)
go

grant select on SparePartMaster to bsi
go

grant select on SparePartMaster to monitoring
go

grant select on SparePartMaster to ccUser
go

grant select on SparePartMaster to analyst
go

set identity_insert SparePartMaster on
go

insert into SparePartMaster(ID,ProductCategoryID,PartNumber,PartName,AltPartNumber,AltPartName,PartCode,ModelCode,SupplierCode,TypeCode,Stock,RetalPrice,PartStatus,ActiveStatus,AccessoriesType,RowStatus,CreatedBy,CreatedTime,LastUpdateBy,LastUpdateTime) select ID,ProductCategoryID,PartNumber,PartName,AltPartNumber,AltPartName,PartCode,ModelCode,SupplierCode,TypeCode,Stock,RetalPrice,PartStatus,ActiveStatus,AccessoriesType,RowStatus,CreatedBy,CreatedTime,LastUpdateBy,LastUpdateTime from tmp__SparePartMaster_2
go

set identity_insert SparePartMaster off
go

drop table tmp__SparePartMaster_2
go

alter table VechileType drop
  constraint [FK_VechileType_VechileModel[one]]] 
go

alter table StockTarget drop
  constraint [FK_StockTarget_VechileModel[one]]] 
go

alter table TransactionControlPK drop
  constraint [FK_TransactionControlPK_VechileModel[one]]] 
go

alter table VechileModel drop
  constraint [FK_VechileModel_Category[one]]] 
go

exec sp_rename 'dbo.PK_Model', 'tmp__PK_Model', 'OBJECT'
go

exec sp_rename 'dbo.VechileModel', 'tmp__VechileModel_3', 'OBJECT'
go

create table VechileModel(
  ID               smallint    not null identity constraint PK_Model primary key,
  SAPCode          varchar(30),
  VechileModelCode varchar(4),
  CategoryID       tinyint,
  Description      varchar(40),
  VechileIndModel  varchar(30),
  IndDescription   varchar(40),
  RowStatus        smallint,
  CreatedBy        varchar(20),
  CreatedTime      datetime,
  LastUpdateBy     varchar(20),
  LastUpdateTime   datetime
)
go

alter table VechileModel add
  constraint [FK_VechileModel_Category[one]]] foreign key(CategoryID) references Category(ID)
go

grant select on VechileModel to bsi
go

grant select on VechileModel to ccUser
go

grant select on VechileModel to analyst
go

set identity_insert VechileModel on
go

insert into VechileModel(ID,VechileModelCode,CategoryID,Description,RowStatus,CreatedBy,CreatedTime,LastUpdateBy,LastUpdateTime) select ID,VechileModelCode,CategoryID,Description,RowStatus,CreatedBy,CreatedTime,LastUpdateBy,LastUpdateTime from tmp__VechileModel_3
go

set identity_insert VechileModel off
go

drop table tmp__VechileModel_3
go

alter table VechileColor drop
  constraint [FK_VechileColor_VechileType[one]]] 
go

alter table FSCampaignVehicle drop
  constraint [FK_FSCampaignVehicle[many]]_VechileType[one]]] 
go

alter table LaborMaster drop
  constraint [FK_LaborMaster_VechileType[one]]] 
go

alter table EventSales drop
  constraint [FK_EventSales[many]]_VechileType[one]]] 
go

alter table BPIklan drop
  constraint [FK_BPIklan[many]]_VechileType[one]]] 
go

alter table PameranDisplay drop
  constraint [FK_PameranDisplay[many]]_VechileType[one]]] 
go

alter table MCPDetail drop
  constraint [FK_MCPDetail[many]]_VechileType[one]]] 
go

alter table DealerStockReportHeader drop
  constraint [FK_DealerStockReportHeader[many]]_VechileType[one]]] 
go

alter table EventParameter drop
  constraint [FK_EventParameter[many]]_VechileType[one]]] 
go

alter table LeasingFee drop
  constraint [FK_LeasingFee[many]]_VechileType[one]]] 
go

alter table EventLaporanPenjualan drop
  constraint [FK_EventLaporanPenjualan[many]]_VechileType[one]]] 
go

alter table BenefitMasterVehicleType drop
  constraint [FK_BenefitMasterVehicleType[many]]_VechileType[one]]] 
go

alter table EventReport drop
  constraint [FK_EventReport[many]]_VechileType[one]]] 
go

alter table EventProposalDetail drop
  constraint [FK_EventProposalDetail[many]]_VechileType[one]]] 
go

alter table SAPCustomer drop
  constraint [FK_SAPCustomer[many]]_VechileType[one]]] 
go

alter table SPLDetail drop
  constraint [FK_SPLDetail[one]]_VechileType[one]]] 
go

alter table ConditionMaster drop
  constraint [FK_ConditionMaster[many]]_VechileType[one]]] 
go

alter table LKPPDetail drop
  constraint [FK_LKPPDetail[many]]_VechileType[one]]] 
go

alter table WSCParameterVehicle drop
  constraint FK_WSCParameterVehicle_VechileType 
go

alter table VechileType drop
  constraint [FK_VechileType[many]]_VehicleClass[one]]] ,
  constraint [FK_VechileType_Category[one]]] 
go

exec sp_rename 'dbo.PK_Type', 'tmp__PK_Type', 'OBJECT'
go

exec sp_rename 'dbo.VechileType', 'tmp__VechileType_4', 'OBJECT'
go

create table VechileType(
  ID                smallint     not null identity constraint PK_Type primary key,
  VechileTypeCode   varchar(4),
  ModelID           smallint,
  CategoryID        tinyint,
  ProductCategoryID smallint,
  Description       varchar(40),
  Status            varchar(1),
  VehicleClassID    int,
  IsVehicleKind1    tinyint,
  IsVehicleKind2    tinyint,
  IsVehicleKind3    tinyint,
  IsVehicleKind4    tinyint,
  SegmentType       varchar(40),
  VariantType       varchar(30),
  TransmitType      varchar(5),
  DriveSystemType   varchar(5),
  SpeedType         varchar(1),
  FuelType          varchar(10),
  MaxTOPDays        int,
  SAPModel          nvarchar(20),
  RowStatus         smallint,
  CreatedBy         varchar(20),
  CreatedTime       datetime,
  LastUpdateBy      varchar(20),
  LastUpdateTime    datetime
)
go

alter table VechileType add
  constraint [FK_VechileType[many]]_VehicleClass[one]]] foreign key(VehicleClassID) references VehicleClass(ID),
  constraint [FK_VechileType_Category[one]]] foreign key(CategoryID) references Category(ID),
  constraint [FK_VechileType_VechileModel[one]]] foreign key(ModelID) references VechileModel(ID)
go

create index IX_VechileType_VehicleTypeCode on VechileType(VechileTypeCode)
go

grant select on VechileType to ccUser
go

set identity_insert VechileType on
go

insert into VechileType(ID,VechileTypeCode,ModelID,CategoryID,ProductCategoryID,Description,Status,VehicleClassID,IsVehicleKind1,IsVehicleKind2,IsVehicleKind3,IsVehicleKind4,MaxTOPDays,SAPModel,RowStatus,CreatedBy,CreatedTime,LastUpdateBy,LastUpdateTime) select ID,VechileTypeCode,ModelID,CategoryID,ProductCategoryID,Description,Status,VehicleClassID,IsVehicleKind1,IsVehicleKind2,IsVehicleKind3,IsVehicleKind4,MaxTOPDays,SAPModel,RowStatus,CreatedBy,CreatedTime,LastUpdateBy,LastUpdateTime from tmp__VechileType_4
go

set identity_insert VechileType off
go

drop table tmp__VechileType_4
go

alter table WSCParameterVehicle add
  constraint FK_WSCParameterVehicle_VechileType foreign key(VechileTypeID) references VechileType(ID)
go

alter table LKPPDetail add
  constraint [FK_LKPPDetail[many]]_VechileType[one]]] foreign key(VehicleTypeID) references VechileType(ID)
go

alter table ConditionMaster add
  constraint [FK_ConditionMaster[many]]_VechileType[one]]] foreign key(VechileTypeID) references VechileType(ID)
go

alter table SPLDetail add
  constraint [FK_SPLDetail[one]]_VechileType[one]]] foreign key(VehicleTypeID) references VechileType(ID)
go

alter table SAPCustomer add
  constraint [FK_SAPCustomer[many]]_VechileType[one]]] foreign key(VechileTypeID) references VechileType(ID)
go

alter table EventProposalDetail add
  constraint [FK_EventProposalDetail[many]]_VechileType[one]]] foreign key(VechileTypeID) references VechileType(ID)
go

alter table EventReport add
  constraint [FK_EventReport[many]]_VechileType[one]]] foreign key(VechileTypeID) references VechileType(ID)
go

alter table BenefitMasterVehicleType add
  constraint [FK_BenefitMasterVehicleType[many]]_VechileType[one]]] foreign key(VechileTypeID) references VechileType(ID)
go

alter table EventLaporanPenjualan add
  constraint [FK_EventLaporanPenjualan[many]]_VechileType[one]]] foreign key(VechileTypeID) references VechileType(ID)
go

alter table LeasingFee add
  constraint [FK_LeasingFee[many]]_VechileType[one]]] foreign key(VehicleTypeID) references VechileType(ID)
go

alter table EventParameter add
  constraint [FK_EventParameter[many]]_VechileType[one]]] foreign key(VehicleTypeID) references VechileType(ID)
go

alter table DealerStockReportHeader add
  constraint [FK_DealerStockReportHeader[many]]_VechileType[one]]] foreign key(VechileTypeID) references VechileType(ID)
go

alter table MCPDetail add
  constraint [FK_MCPDetail[many]]_VechileType[one]]] foreign key(VehicleTypeID) references VechileType(ID)
go

alter table PameranDisplay add
  constraint [FK_PameranDisplay[many]]_VechileType[one]]] foreign key(VechileTypeID) references VechileType(ID)
go

alter table BPIklan add
  constraint [FK_BPIklan[many]]_VechileType[one]]] foreign key(VechileTypeID) references VechileType(ID)
go

alter table EventSales add
  constraint [FK_EventSales[many]]_VechileType[one]]] foreign key(VechileTypeID) references VechileType(ID)
go

alter table LaborMaster add
  constraint [FK_LaborMaster_VechileType[one]]] foreign key(VechileTypeID) references VechileType(ID)
go

alter table FSCampaignVehicle add
  constraint [FK_FSCampaignVehicle[many]]_VechileType[one]]] foreign key(VehicleTypeID) references VechileType(ID)
go

alter table VechileColor add
  constraint [FK_VechileColor_VechileType[one]]] foreign key(VechileTypeID) references VechileType(ID)
go

alter table TransactionControlPK add
  constraint [FK_TransactionControlPK_VechileModel[one]]] foreign key(ModelID) references VechileModel(ID)
go

alter table StockTarget add
  constraint [FK_StockTarget_VechileModel[one]]] foreign key(ModelID) references VechileModel(ID)
go

alter table DepositBKewajibanDetail add
  constraint [FK_DepositBKewajibanDetail[many]]_SparePartMaster[one]]] foreign key(SparePartMasterID) references SparePartMaster(ID)
go

alter table EstimationEquipDetail add
  constraint FK_EstimationEquipDetail_SparePartMaster foreign key(SparePartMasterID) references SparePartMaster(ID)
go

alter table IndentPartDetail add
  constraint [FK_IndentPartDetail[many]]_SparePartMaster[one]]] foreign key(SparePartMasterID) references SparePartMaster(ID)
go

alter table SpecialItemDetail add
  constraint [FK_SpecialItemDetail_SparePartMaster[one]]] foreign key(SparePartMasterID) references SparePartMaster(ID)
go

alter table SparePartPackingDetail add
  constraint [FK_SparePartPackingDetail[many]]_SparePartMaster[one]]] foreign key(SparePartMasterID) references SparePartMaster(ID)
go

alter table SparePartDODetail add
  constraint [FK_SparePartDODetail[many]]_SparePartMaster[one]]] foreign key(SparePartMasterID) references SparePartMaster(ID)
go

alter table PQRPartsCodeBB add
  constraint [FK_PQRPartsCodeBB[one]]_SparePartMaster[one]]] foreign key(SparePartMasterID) references SparePartMaster(ID)
go

alter table PQRPartsCode add
  constraint [FK_PQRPartsCode[one]]_SparePartMaster[one]]] foreign key(SparePartMasterID) references SparePartMaster(ID)
go

alter table WSCDetailBB add
  constraint [FK_WSCDetailBB_SparePartMaster[one]]] foreign key(SparePartMasterID) references SparePartMaster(ID)
go

alter table SparepartMaxOrder add
  constraint [FK_SparepartMaxOrder_SparePartMaster[one]]] foreign key(SparepartID) references SparePartMaster(ID)
go

alter table WSCDetail add
  constraint [FK_WSCDetail_SparePartMaster[one]]] foreign key(SparePartMasterID) references SparePartMaster(ID)
go

alter table PartIncidentalDetail add
  constraint [FK_PartIncidentalDetail]]_SparePartMaster[one]]] foreign key(SparePartMasterID) references SparePartMaster(ID)
go

alter table Dealer add
  constraint [FK_Dealer_MainArea[one]]] foreign key(MainAreaID) references MainArea(ID)
go

alter table Area1 add
  constraint [FK_MainArea[one]]_Area1[many]]] foreign key(MainAreaID) references MainArea(ID)
go

alter table Dealer add
  constraint [FK_Dealer[many]]_Area1[one]]] foreign key(Area1ID) references Area1(ID)
go

alter table Area2 add
  constraint [FK_Area1[one]]_Area2[many]]] foreign key(Area1ID) references Area1(ID)
go

commit
go


